using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonOA
{
    using System.Linq;

    class TrieCustomerSuggestion
    {
        public List<List<string>> threeKeywordSuggestions(int numreviews, List<string> repository, string customerQuery)
        {
            List<List<string>> res = new List<List<string>>();
            if (repository.Count == 0 || customerQuery.Length < 2)
            {
                return res;
            }

            Trie t = new Trie(repository);

            for (int i = 2; i <= customerQuery.Length; i++)
            {
                string searchString = customerQuery.Substring(0, i);
                List<string> searchRes = new List<string>();
                if (t.Search(searchString, out searchRes))
                {
                    res.Add(searchRes);
                }
                else
                {
                    searchRes.Add(searchString);
                    res.Add(searchRes);
                }
            }

            return res;
        }
    }
}
