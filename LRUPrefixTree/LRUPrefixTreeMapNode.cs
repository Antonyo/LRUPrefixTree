// <copyright file="LRUPrefixTreeCacheNode.cs">
// Copyright (c) 2017 All Rights Reserved
// <author>Antonyo</author>
// <summary>Represents a node in a LRU Cache</summary>
// </copyright>      

using System;
using System.Collections.Generic;
using System.Text;

namespace LRUPrefixTree
{
    internal class LRUPrefixTreeMapNode
    {
        internal LRUPrefixTreeNode Value { get; set; }
        internal LRUPrefixTreeMapNode Prev { get; set; }
        internal LRUPrefixTreeMapNode Next { get; set; }

        internal LRUPrefixTreeMapNode(LRUPrefixTreeNode value)
        {
            Value = value;
        }
    }
}
