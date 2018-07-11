using LRUPrefixTree.Tests.CustomAttributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace LRUPrefixTree.Tests
{
    [TestClass]
    public class LRUCacheTests : TestBase
    {
        [TestCategory("LRUCache")]
        [TestMethod]
        public void ShouldSetHeadToLatestAddedWord()
        {
            AddWords("Baseball");
            AddWords("Base");

            Assert.AreEqual(2, Tree._map.Count);
            Assert.AreEqual("base", Tree._head.Value.GetWord());
        }

        [TestCategory("LRUCache")]
        [TestMethod]
        public void ShouldSetTailToFirstAddedWord()
        {
            AddWords("Baseball");
            AddWords("Base");

            Assert.AreEqual(2, Tree._map.Count);
            Assert.AreEqual("baseball", Tree._tail.Value.GetWord());
        }

        [TestCategory("LRUCache")]
        [TestMethod]
        public void ShouldLinkAllWords()
        {
            AddWords("Baseball");
            AddWords("Base");
            AddWords("Game");

            Assert.AreEqual(3, Tree._map.Count);
            Assert.AreEqual("game", Tree._head.Value.GetWord());

            Assert.AreEqual("base", Tree._head.Next.Value.GetWord());
            Assert.AreEqual("game", Tree._head.Next.Prev.Value.GetWord());

            Assert.AreEqual("baseball", Tree._head.Next.Next.Value.GetWord());
            Assert.AreEqual("base", Tree._head.Next.Next.Prev.Value.GetWord());

            Assert.AreEqual("baseball", Tree._tail.Value.GetWord());
        }

        [TestCategory("LRUCache")]
        [TestMethod]
        public void ShouldRewireHeadWhenHeadNextWordIsMovedToFront()
        {
            AddWords("A");
            AddWords("B");
            AddWords("C");

            // 0 <- C <-> B <-> A -> 0
            Tree.IsWord("B"); // Move B to front the 

            // 0 <- B <-> C <-> A -> 0
            Assert.AreEqual('b', Tree._head.Value.Value);

            Assert.AreEqual('c', Tree._head.Next.Value.Value);
            Assert.AreEqual('b', Tree._head.Next.Prev.Value.Value);

            Assert.AreEqual('a', Tree._head.Next.Next.Value.Value);
            Assert.AreEqual('c', Tree._head.Next.Next.Prev.Value.Value);

            Assert.AreEqual('a', Tree._tail.Value.Value);
        }

        [TestCategory("LRUCache")]
        [TestMethod]
        public void ShouldMoveToFrontAlreadyAddedWords()
        {
            AddWords("A");
            AddWords("B");

            Assert.AreEqual(2, Tree._map.Count);
            Assert.AreEqual(Tree._root['a'], Tree._tail.Value); // TAIL => A
            Assert.AreEqual(Tree._root['b'], Tree._head.Value); // HEAD => B

            AddWords("A");
            Assert.AreEqual(Tree._root['b'], Tree._tail.Value); // TAIL => B
            Assert.AreEqual(Tree._root['a'], Tree._head.Value); // HEAD => A
        }

        [TestCategory("LRUCache")]
        [TestMethod]
        [DefaultTreeCapacity(2)]
        public void ShouldRemoveLastUsedWordWhenOvercapacity()
        {
            AddWords("A");
            AddWords("B");

            Assert.AreEqual(2, Tree._map.Count);
            Assert.AreEqual(Tree._root['a'], Tree._tail.Value); // TAIL => A
            Assert.AreEqual(Tree._root['b'], Tree._head.Value); // HEAD => B

            AddWords("C");
            Assert.AreEqual(2, Tree._map.Count);
            Assert.AreEqual(Tree._root['b'], Tree._tail.Value); // TAIL => B
            Assert.AreEqual(Tree._root['c'], Tree._head.Value); // HEAD => A
        }
    }
}
