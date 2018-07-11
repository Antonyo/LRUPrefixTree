// <copyright file="LRUPrefixTree.cs">
// Copyright (c) 2017 All Rights Reserved
// <author>Antonyo</author>
// <summary>Represents a node in a Prefix Tree</summary>
// </copyright>      

using System;
using System.Collections.Generic;
using System.Text;

namespace LRUPrefixTree
{
    internal class LRUPrefixTreeNode : Dictionary<char, LRUPrefixTreeNode>
    {
        public char Value { get; }

        internal LRUPrefixTreeNode(LRUPrefixTreeNode parent, char value) : this(parent)
        {
            Value = value;
        }

        internal LRUPrefixTreeNode(LRUPrefixTreeNode parent)
        {
            Parent = parent;
        }

        internal bool IsWord { get; set; }
        internal LRUPrefixTreeNode Parent { get; private set; }

        internal string GetWord()
        {
            if (Parent == null)
                return string.Empty;

            var builder = new StringBuilder();

            var current = this;
            while (current.Parent != null)
            {
                builder.Insert(0, current.Value);
                current = current.Parent;
            }

            return builder.ToString();
        }
    }
}
