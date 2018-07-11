using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace LRUPrefixTree.Tests
{
    [TestClass]
    public class PrefixTreeTests : TestBase
    {
        [TestCategory("PrefixTree")]
        [TestMethod]
        public void ShouldLowerCaseCapitalizedLetters()
        {
            AddWords("A");

            Assert.AreEqual(1, Tree._root.Count, "First level of tree not filled properly");
            Assert.IsTrue(Tree._root.ContainsKey('a'), "Words are not being added in lower case");
            Assert.IsFalse(Tree._root.ContainsKey('A'), "Words are being added in capital letters");
        }

        [TestCategory("PrefixTree")]
        [TestMethod]
        public void ShouldAddWordsToTree()
        {
            AddWords("A", "B", "C", "AA");

            Assert.AreEqual(3, Tree._root.Count, "First level of tree not filled properly");
            Assert.AreEqual(1, Tree._root['a'].Count, "Second level is not filled properly");
        }

        [TestCategory("PrefixTree")]
        [TestMethod]
        public void ShouldRetrieveWordsFromTree()
        {
            AddWords("base", "baseball");

            Assert.IsTrue(Tree.IsWord("base"), "Added word is not retrieved");
            Assert.IsFalse(Tree.IsWord("game"), "Non added word is retrieved");
            Assert.IsTrue(Tree.IsWord("baseball"), "Added word with prefix is not retrieved");
            Assert.IsFalse(Tree.IsWord("bases"), "Non added prefix word is retrieved");
        }

        [TestCategory("PrefixTree")]
        [TestMethod]
        public void ShouldNotPredictNonExistingWords()
        {
            var words = new string[] { "base", "baseball", "bases" };
            AddWords(words);

            var predictedWords = Tree.Predict("c");
            Assert.AreEqual(0, predictedWords.Count(), "Wrong number of predicted words");
        }

        [TestCategory("PrefixTree")]
        [TestMethod]
        public void ShouldPredictWordsFromTree()
        {
            var words = new string[] { "base", "baseball", "bases" };
            AddWords(words);

            var predictedWords = Tree.Predict("ba");
            Assert.AreEqual(3, predictedWords.Count(), "Wrong number of predicted words");
            Assert.IsTrue(words.SequenceEqual(predictedWords), "Wrong predicted words");
        }

        [TestCategory("PrefixTree")]
        [TestMethod]
        public void ShouldPredictPrefixedWordsFromTree()
        {
            var words = new string[] { "base", "baseball", "bases" };
            AddWords(words);

            var predictedWords = Tree.Predict("baseb");
            Assert.AreEqual(1, predictedWords.Count(), "Wrong number of predicted words");
            Assert.IsTrue(new string[] { "baseball" }.SequenceEqual(predictedWords), "Wrong predicted words");
        }
    }
}
