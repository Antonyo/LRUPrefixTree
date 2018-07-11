// <copyright file="LRUPrefixTree.cs">
// Copyright (c) 2017 All Rights Reserved
// <author>Antonyo</author>
// <summary>Represents LRU Prefix Tree</summary>
// </copyright>      

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("LRUPrefixTree.Tests")]
namespace LRUPrefixTree
{
    public class LRUPrefixTree
    {
        internal const int DEFAULT_CAPACITY = 100;

        internal LRUPrefixTreeNode _root;
        internal int _capacity;
        internal Dictionary<LRUPrefixTreeNode, LRUPrefixTreeMapNode> _map;
        internal LRUPrefixTreeMapNode _head = null;
        internal LRUPrefixTreeMapNode _tail = null;

        public LRUPrefixTree() : this(DEFAULT_CAPACITY)
        { }

        public LRUPrefixTree(int capacity)
        {
            _capacity = capacity;
            _map = new Dictionary<LRUPrefixTreeNode, LRUPrefixTreeMapNode>(capacity);
            _root = new LRUPrefixTreeNode(null);
        }
        
        public void Add(string word)
        {
            LRUPrefixTreeNode current = _root;

            foreach (var c in word.ToLower())
            {
                if (!current.ContainsKey(c))
                    current.Add(c, new LRUPrefixTreeNode(current, c));

                current = current[c];
            }

            current.IsWord = true;

            AddToMap(current);
        }

        public bool IsWord(string word)
        {
            LRUPrefixTreeNode current = _root;

            foreach (var c in word.ToLower())
            {
                if (!current.ContainsKey(c))
                    return false;

                current = current[c];
            }

            if (current.IsWord)
                MoveToFront(_map[current]);

            return current.IsWord;
        }

        public IEnumerable<string> Predict(string word)
        {
            List<string> words = new List<string>();

            LRUPrefixTreeNode current = _root;
            foreach (var c in word.ToLower())
            {
                if (!current.ContainsKey(c))
                    return words;

                current = current[c];
            }

            ExpandWordsFromNode(current, word.ToLower());

            return words;

            void ExpandWordsFromNode(LRUPrefixTreeNode node, string prefix)
            {
                if (node.IsWord)
                {
                    words.Add(prefix);
                    MoveToFront(_map[node]);
                }

                if (node.Count == 0)
                    return;

                foreach (var subC in node.Keys)
                    ExpandWordsFromNode(node[subC], prefix + subC);
            }
        }

        private void AddToMap(LRUPrefixTreeNode node)
        {
            if (_map.ContainsKey(node))
                MoveToFront(_map[node]);
            else
            {
                var mapNode = new LRUPrefixTreeMapNode(node);

                if (_tail != null)
                    RemoveLRU();
                else
                    _tail = mapNode;

                MoveToFront(mapNode);

                _map.Add(node, mapNode);
            }
        }

        private void MoveToFront(LRUPrefixTreeMapNode newNode)
        {
            if (_head == newNode) // If it's already the head, stop
                return;

            if (_tail == newNode && newNode.Prev != null) // If it's the tail and it has a previous node, aka is not also the head
            {
                _tail = newNode.Prev;
                _tail.Next = null;
            }
                
            if (_head != null)
            {
                _head.Prev = newNode;
                if(_head.Next == newNode)
                {
                    _head.Next = newNode.Next;
                    newNode.Next.Prev = _head;
                }
            }

            newNode.Prev = null;
            newNode.Next = _head;

            _head = newNode;
        }

        private void RemoveLRU()
        {
            if (_map.Count == _capacity)
            {
                RemoveWordFromTree(_tail.Value);
                _map.Remove(_tail.Value);

                _tail.Prev.Next = null;
                _tail = _tail.Prev;
            }
        }

        private void RemoveWordFromTree(LRUPrefixTreeNode node)
        {
            node.IsWord = false;

            while(node.Parent != null)
            {
                if (node.Count == 0)
                    node.Parent.Remove(node.Value);
                else
                    return;

                node = node.Parent;
            }
        }
    }
}
