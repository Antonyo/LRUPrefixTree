using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace LRUPrefixTree.Tests
{
    [TestClass]
    public class InitializationTests
    {
        [TestCategory("Initialization")]
        [TestMethod]
        public void ShouldDefaultCapacityTo100IfNotSpecified()
        {
            var tree = new LRUPrefixTree();
            Assert.AreEqual(LRUPrefixTree.DEFAULT_CAPACITY, tree._capacity, $"Capacity is not set to default {LRUPrefixTree.DEFAULT_CAPACITY} value.");
        }

        [TestCategory("Initialization")]
        [TestMethod]
        public void ShouldDefaultCapacityToSpecifiedCapacity()
        {
            var capacity = 255;
            var tree = new LRUPrefixTree(capacity);
            Assert.AreEqual(capacity, tree._capacity, $"Capacity is not set to specified {capacity} value");
        }

        [TestCategory("Initialization")]
        [TestMethod]
        public void ShouldHaveEmptyTreeWord()
        {
            var tree = new LRUPrefixTree();
            Assert.AreEqual(0, tree._root.Count, $"Tree should not have any word upon creation");
        }
    }
}
