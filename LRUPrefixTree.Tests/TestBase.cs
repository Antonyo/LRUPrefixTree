using LRUPrefixTree.Tests.CustomAttributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Text;

namespace LRUPrefixTree.Tests
{    
    public class TestBase
    {
        public TestContext TestContext { get; set; }

        protected LRUPrefixTree Tree { get; private set; }

        [TestInitialize]
        public void OnInitialize()
        {
            var capacity = GetTestSpecifiedTreeCapacity();
            Tree = capacity.HasValue ? new LRUPrefixTree(capacity.Value) : new LRUPrefixTree();
        }

        protected void AddWords(params string[] words)
        {
            if (words.Length > 0)
                words.ToList().ForEach(x => Tree.Add(x));
        }

        Type _cachedTestClassType;
        private int? GetTestSpecifiedTreeCapacity()
        {
            var testClassType = GetTestClassType();

            var testMethodInfo = testClassType.GetMethod(TestContext.TestName);
            var capacityAttribute = testMethodInfo.GetCustomAttributes(typeof(DefaultTreeCapacityAttribute), false);

            return capacityAttribute.Count() > 0 ? ((DefaultTreeCapacityAttribute)capacityAttribute.First()).Capacity : (int?)null;

            Type GetTestClassType()
            {
                if (_cachedTestClassType == null || _cachedTestClassType.Name != TestContext.FullyQualifiedTestClassName)
                    _cachedTestClassType = Type.GetType(TestContext.FullyQualifiedTestClassName);

                return _cachedTestClassType;
            }
        }
    }
}
