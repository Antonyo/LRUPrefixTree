# LRUPrefixTree

A Trie (Prefix tree) which only stores the latest N most used words.
Support predict words based on a prefix used in autocomplete fields.

It uses a double a Trie in combination with a double linked list to store only an specific number of words, if more words are added
over the capacity, the least used words will be removed from the trie.

