using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonOA
{
    using System.Linq;

    public class Trie
    {
        public class TrieNode
        {
            public Dictionary<char, TrieNode> set =
                new Dictionary<char, TrieNode>();
            public List<string> AllStrings = new List<string>();
        }

        public TrieNode Root;

        public Trie()
        {
            Root = new TrieNode();
        }

        public Trie(List<string> data)
        {
            Root = new TrieNode();
            foreach (string s in data)
            {
                Insert(s);
            }
        }

        public void Insert(string word)
        {
            TrieNode node = Root;
            foreach (char a in word)
            {
                char al = Char.ToLower(a);
                if (!node.set.ContainsKey(al))
                {
                    node.set[al] = new TrieNode();
                }

                node.AllStrings.Add(word);
                node = node.set[al];
            }

            node.AllStrings.Add(word);
        }

        public bool Search(string prefix, out List<string> words)
        {
            words = new List<string>();
            TrieNode node = Root;
            foreach (char c in prefix)
            {
                char al = Char.ToLower(c);
                if (!node.set.ContainsKey(al))
                {
                    return false;
                }

                node = node.set[al];
                words = node.AllStrings;
            }

            words.Sort();
            if (words.Count > 3)
            {
                words = words.Take(3).ToList();
            }
            
            return true;
        }
    }
}
