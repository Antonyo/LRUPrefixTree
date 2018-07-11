using System;
using System.Collections.Generic;
using System.Text;

namespace LRUPrefixTree.Tests.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class DefaultTreeCapacityAttribute : Attribute
    {
        public int Capacity { get; set; }

        public DefaultTreeCapacityAttribute(int capacity)
        {
            Capacity = capacity;
        }
    }
}
