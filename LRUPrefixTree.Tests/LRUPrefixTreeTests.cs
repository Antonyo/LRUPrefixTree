using LRUPrefixTree.Tests.CustomAttributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace LRUPrefixTree.Tests
{
    [TestClass]
    public class LRUPrefixTreeTests : TestBase
    {
        [TestCategory("LRUPrefixTree")]
        [TestMethod]
        public void ShouldMoveToFrontPredictedWords()
        {
            AddWords("Baseball");
            AddWords("Base");
            AddWords("Game");

            Assert.AreEqual("game", Tree._head.Value.GetWord(), "Latest added word is not at the head");

            var predicted = Tree.Predict("b");

            Assert.AreNotEqual("game", Tree._head.Value.GetWord(), "Non predicted remains at the head");
            Assert.AreEqual("game", Tree._tail.Value.GetWord(), "Non predicted words are not moved to the tail");
        }

        [TestCategory("LRUPrefixTree")]
        [TestMethod]
        [DefaultTreeCapacity(5)]
        public void ShouldNotPredictRemovedWordsWhenOvercapacity()
        {
            AddWords("AB");
            AddWords("AA");
            AddWords("ABA");
            AddWords("ABC");
            AddWords("ABB");

            var predictedWords = Tree.Predict("A");
            Assert.AreEqual(5, predictedWords.Count());

            predictedWords = Tree.Predict("AB");
            Assert.AreEqual(4, predictedWords.Count());
            Assert.IsFalse(predictedWords.Contains("aa"));

            AddWords("B");

            // AA should be gone since it was the LRU
            predictedWords = Tree.Predict("A");
            Assert.AreEqual(4, predictedWords.Count());
            Assert.IsFalse(predictedWords.Contains("aa"));
        }
    }
}
