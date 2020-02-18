using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonOA
{
    using System.Collections;
    using System.Linq;
    using Microsoft.VisualBasic.CompilerServices;

    class OtherQuestions2
    {
        public void Run()
        {
            string s = "catsandog";
            List<string> dic = new List<string>(){"cats", "dog", "sand", "and", "cat"};
            Console.WriteLine($"Result of word break is {WordBreak(s, dic)}");
            Console.WriteLine("_______________________________________________________");

            IList<int> res = FindAnagrams("cbaebabacd", "abc");
            Console.WriteLine(string.Join(',', res));
            Console.WriteLine("_______________________________________________________");

            int[][] pre = new int[1][];
            pre[0] = new[] {1, 0};
            Console.WriteLine($"Can finish course {CanFinish(2, pre)}");
            Console.WriteLine("_______________________________________________________");

            int[] num = new[] {5, 7, 7, 8, 8, 10};
            int[] res1 = SearchRange(num, 8);
            Console.WriteLine($"range of 8 is {string.Join(',', res1)}");
            num = new[] {1};
            res1 = SearchRange(num, 1);
            Console.WriteLine($"range of 1 is {string.Join(',', res1)}");
            Console.WriteLine("_______________________________________________________");

            num = new[] {1, 2, 3};
            NextPermutation(num);
            Console.WriteLine($"Next permutation is {string.Join(',', num)}");
            Console.WriteLine("_______________________________________________________");

            num = new[] {-4, -3, -2};
            Console.WriteLine($"Max product is {MaxProduct(num)}");
            Console.WriteLine("_______________________________________________________");

            LRUCache l = new LRUCache(2);
            l.Put(1,1);
            l.Put(2,2);
            Console.WriteLine($"{l.Get(1)}");
            l.Put(3,3);
            Console.WriteLine($"{l.Get(2)}");
            l.Put(4,4);
            Console.WriteLine($"{l.Get(1)}");
            Console.WriteLine($"{l.Get(3)}");
            Console.WriteLine($"{l.Get(4)}");
            Console.WriteLine("_______________________________________________________");


            IList<string> res2 = RemoveInvalidParentheses(")(f");
            Console.WriteLine(string.Join(',', res2));
            Console.WriteLine("_______________________________________________________");

            num = new[] {1, -1};
            res1 = MaxSlidingWindow(num, 1);
            Console.WriteLine(string.Join(',', res1));
            Console.WriteLine("_______________________________________________________");

            Console.WriteLine($"Longest valid paren in \")()())\" is {LongestValidParentheses(")()())")}");
            Console.WriteLine("_______________________________________________________");
        }

        public ListNode SortList(ListNode head)
        {
            if (head?.next == null) return head;

            ListNode slow = head, fast = head, prev = null;
            while (fast != null && fast.next != null)
            {
                prev = slow;
                slow = slow.next;
                fast = fast.next.next;
            }

            prev.next = null;
            ListNode a = SortList(head);
            ListNode b = SortList(slow);

            return MergeList(a, b);
        }

        public ListNode MergeList(ListNode a, ListNode b)
        {
            if (a == null) return b;
            if (b == null) return a;

            ListNode cur = null, res = null;
            while (a != null && b != null)
            {
                ListNode n;
                if (a.val < b.val)
                {
                    n = new ListNode(a.val);
                    a = a.next; 
                }
                else
                {
                    n = new ListNode(b.val);
                    b = b.next;
                }

                if (res == null)
                {
                    res = n;
                    cur = res;
                }
                else
                {
                    cur.next = n;
                    cur = cur.next;
                }
            }

            if (a != null) cur.next = a;
            if (b != null) cur.next = b;

            return res;
        }

        public bool WordBreakRecurse(string s, IList<string> wordDict)
        {
            if (string.IsNullOrEmpty(s)) return false;

            if (wordDict.Contains(s)) return true;

            bool res = false;
            for (int i = 1; i <= s.Length; i++)
            {
                string a = s.Substring(0, i);
                string b = s.Substring(i);
                bool temp = wordDict.Contains(a) && WordBreak(b, wordDict);
                if (temp) wordDict.Add(s);
                res = res || temp;
            }

            return res;
        }

        public bool WordBreak(string s, IList<string> wordDict)
        {
            if (string.IsNullOrEmpty(s) || wordDict.Count == 0) return false;

            bool[] dp = new bool[s.Length+1];
            dp[0] = false;

            for (int i = 1; i <= s.Length; i++)
            {
                if (!dp[i] && wordDict.Contains(s.Substring(0, i))) dp[i] = true;

                if (dp[i])
                {
                    if (i == s.Length) return true;

                    for (int j = i + 1; j <= s.Length; j++)
                    {
                        if (!dp[j] && wordDict.Contains(s.Substring(i, j-i))) dp[j] = true;

                        if (dp[j])
                        {
                            if (j == s.Length || wordDict.Contains(s.Substring(j))) return true;
                        }
                    }
                }
            }

            return false;
        }

        public int MaximalSquare(char[][] matrix)
        {
            if (matrix.Length == 0) return 0;

            int m = matrix.Length;
            int n = matrix[0].Length;
            int[,] dp = new int[m, n];

            for (int i = 0; i < m; i++) dp[i, 0] = matrix[i][0] - '0';
            for (int j = 1; j < n; j++) dp[0, j] = matrix[0][j] - '0';

            for (int i = 1; i < m; i++)
            {
                for (int j = 1; j < n; j++)
                {
                    if (matrix[i][j] == '1' && matrix[i - 1][j] == '1' && matrix[i - 1][j - 1] == '1' &&
                        matrix[i - 1][j] == '1')
                    {
                        int v = Math.Min(dp[i - 1, j], Math.Min(dp[i - 1, j - 1], dp[i, j - 1]));
                        dp[i, j] = v + 1;
                    }
                    else
                    {
                        dp[i, j] = matrix[i][j] - '0';
                    }
                }
            }

            for (int i = 0; i < dp.GetLength(0); i++)
            {
                for (int j = 0; j < dp.GetLength(1); j++)
                {
                    Console.Write(dp[i, j] + "\t");
                }
                Console.WriteLine();
            }
            return (int) Math.Pow(dp.Cast<int>().Max(), 2);
        }

        public int NumSquaresRecurse(int n)
        {
            if (n <= 3) return n;

            int res = int.MaxValue;
            for (int i = 1; i < n; i++)
            {
                int p = (int) Math.Pow(i, 2);
                if (p > n) break;
                res = Math.Min(res, 1 + NumSquaresRecurse(n - p));
            }

            return res;
        }

        public int NumSquares(int n)
        {
            if (n <= 3) return n;
            int[] dp = new int[n+1];
            dp[0] = 0;
            dp[1] = 1;
            dp[2] = 2;
            dp[3] = 3;

            for (int i = 4; i <= n; i++)
            {
                dp[i] = int.MaxValue;
                for (int j = 1; j < i; j++)
                {
                    int p = (int)Math.Pow(j, 2);
                    if (p > i) break;
                    dp[i] = Math.Min(dp[i], 1 + dp[i - p]);
                }
            }

            return dp[n];
        }

        public IList<int> FindAnagrams(string s, string p)
        {
            Dictionary<char, int> map = new Dictionary<char, int>();

            foreach (char c in p)
            {
                map[c] = map.GetValueOrDefault(c, 0) + 1;
            }

            int start = 0, end = 0;
            List<int> res = new List<int>();
            Dictionary<char, int> temp = new Dictionary<char, int>(map);
            while (end < s.Length)
            {
                if (temp.Keys.All(k => temp[k] == 0))
                {
                    res.Add(start);
                }

                if (end - start + 1 > p.Length)
                {
                    if (temp.ContainsKey(s[start])) temp[s[start]] += 1;
                    start += 1;
                }

                if (temp.ContainsKey(s[end])) temp[s[end]] -= 1;
                end += 1;
            }

            if (temp.Keys.All(k => temp[k] == 0))
            {
                res.Add(start);
            }

            return res;
        }

        public bool CanFinish(int numCourses, int[][] prerequisites)
        {
            Dictionary<int, List<int>> adj = new Dictionary<int, List<int>>();
            int[] indegree = new int[numCourses];
            bool[] v = new bool[numCourses];

            foreach (int[] pre in prerequisites)
            {
                int p = pre[0];
                int q = pre[1];

                indegree[q]++;
                if (!adj.ContainsKey(p)) adj[p] = new List<int>();
                adj[p].Add(q);
            }

            List<int> l = new List<int>();
            for (int i = 0; i < indegree.Length; i++)
            {
                if (indegree[i] == 0) l.Add(i);
            }
            if (l.Any())
            {
                Queue<int> q = new Queue<int>();
                foreach (int i in l) q.Enqueue(i);

                while (q.Any())
                {
                    int c = q.Dequeue();
                    v[c] = true;
                    List<int> neigh = adj.GetValueOrDefault(c, new List<int>());
                    foreach (int i in neigh)
                    {
                        if (!v[i])
                        {
                            indegree[i]--;
                            if (indegree[i] == 0) q.Enqueue(i);
                        }
                        else return false;
                    }
                }
            }
            else
            {
                return false;
            }

            return v.All(i => i);
        }

        public int[] SearchRange(int[] nums, int target)
        {
            int n = nums.Length;
            if (n == 0) return new[] { -1, -1 };
            int i = 0, j = n - 1;
            int pos = 0;
            
            while (i <= j)
            {
                pos = (i + j) / 2;
                if (nums[pos] == target) break;
                else if (nums[pos] < target) i = pos + 1;
                else j = pos - 1;
            }

            if (pos < 0 || pos >= n || nums[pos] != target) return new[] {-1, -1};

            i = pos;
            j = i;

            while (i > 0)
            {
                if (i > 0 && nums[i-1] != target) break;
                if (nums[i - 1] == target) i = i - 1;
            }
            while (j < n-1)
            {
                if (j < n - 1 && nums[j + 1] != target) break;
                if (nums[j + 1] == target) j = j + 1;
            }

            return new[] {i, j};
        }

        public int SearchRotated(int[] nums, int target)
        {
            int n = nums.Length;
            if (n <= 0) return -1;

            int i = 0, j = n - 1;
            int pos = -1;
            while (i <= j)
            {
                pos = (i + j) / 2;
                if (pos -1 >= 0 && nums[pos -1] > nums[pos]) break;
                if (nums[pos] <= nums[j]) j = pos - 1;
                else i = pos + 1;
            }

            if (target == nums[pos]) return pos;
            else if (target > nums[pos] && target <= nums[n - 1])
            {
                i = pos + 1;
                j = n - 1;
            }
            else
            {
                i = 0;
                j = pos - 1;
            }

            while (i <= j)
            {
                pos = (i + j) / 2;
                if (nums[pos] == target) break;
                else if (nums[pos] < target) i = pos + 1;
                else j = pos - 1;
            }
            return nums[pos] == target ? pos : -1;
        }

        public void NextPermutation(int[] nums)
        {
            int n = nums.Length;
            if (n <= 1) return;

            int a = 0, b = n - 1;
            int i = b;
            for (; i > 0; i--)
            {
                if (nums[i - 1] < nums[i])
                {
                    a = i;
                    i = i - 1;
                    break;
                }
            }

            int j;
            for (j = a; j < n-1; j++)
            {
                if (nums[j+1] <= nums[i]) break; 
            }
            Swap(nums, i, j);
            
            for (; a < b; a++, b--)
            {
                Swap(nums, a, b);
            }
        }

        public void Swap(int[] nums, int a, int b)
        {
            int t = nums[a];
            nums[a] = nums[b];
            nums[b] = t;
        }

        public int MaxProduct(int[] nums)
        {
            int r = int.MinValue;
            int max = 1;
            int min = 1;
            for (int i = 0; i < nums.Length; i++)
            {
                int a = min * nums[i];
                int b = max * nums[i];
                max = Math.Max(nums[i], Math.Max(a, b));
                min = Math.Min(nums[i], Math.Min(a, b));

                r = Math.Max(r, max);
            }

            return r;
        }

        public int Trap(int[] height)
        {
            int count = 0;
            if (height.Length == 0) return 0;
            int[] l = new int[height.Length];
            l[0] = height[0];
            for (int i = 1; i < height.Length; i++) l[i] = Math.Max(l[i - 1], height[i]);

            int[] r = new int[height.Length];
            r[height.Length - 1] = height[height.Length - 1];
            for (int i = height.Length - 2; i >= 0; i++) r[i] = Math.Max(r[i + 1], height[i]);

            for (int i = 0; i < height.Length; i++)
            {
                count = count + Math.Min(l[i], r[i]) - height[i];
            }

            return count;
        }

        public IList<string> RemoveInvalidParentheses(string s)
        {
            List<string> res = new List<string>();
            if (string.IsNullOrEmpty(s) || IsValidParen(s)) return res;

            Queue<string> q = new Queue<string>();
            HashSet<string> v = new HashSet<string>();
            bool level = false;
            q.Enqueue(s);
            v.Add(s);

            while (q.Any())
            {
                string temp = q.Dequeue();
                if (IsValidParen(temp))
                {
                    res.Add(temp);
                    level = true;
                }

                if (!level)
                {
                    for (int i = 0; i < temp.Length; i++)
                    {
                        if (temp[i] != '(' && temp[i] != ')') continue;
                        string s1 = temp.Substring(0, i) + temp.Substring(i + 1);
                        if (!v.Contains(s1))
                        {
                            q.Enqueue(s1);
                            v.Add(s1);
                        }
                    }
                }
            }

            return res;
        }

        public bool IsValidParen(string s)
        {
            int count = 0;
            foreach (char c in s)
            {
                if (c == '(') count++;
                if (c == ')') count--;
                if (count < 0) return false;
            }

            return count == 0;
        }

        public int LongestConsecutive(int[] nums)
        {
            HashSet<int> num_set = new HashSet<int>();
            foreach(int num in nums)
            {
                num_set.Add(num);
            }

            int longestStreak = 0;

            foreach (int num in num_set)
            {
                if (!num_set.Contains(num - 1))
                {
                    int currentNum = num;
                    int currentStreak = 1;

                    while (num_set.Contains(currentNum + 1))
                    {
                        currentNum += 1;
                        currentStreak += 1;
                    }

                    longestStreak = Math.Max(longestStreak, currentStreak);
                }
            }

            return longestStreak;
        }

        public bool Exist(char[][] board, string word)
        {
            if (string.IsNullOrEmpty(word)) return false;

            bool[][] v = new bool[board.Length][];
            for (int i = 0; i < board.Length; i++)
            {
                v[i] = new bool[board[i].Length];
                for (int j = 0; j < board[i].Length; j++)
                {
                    v[i][j] = false;
                }
            }

            for (int i = 0; i < board.Length; i++)
            {
                for (int j = 0; j < board[i].Length; j++)
                {
                    if (board[i][j] == word[0])
                    {
                        if (Travel(board, v, word, 0, i, j)) return true;
                    }
                }
            }

            return false;
        }

        public bool Travel(char[][] board, bool[][] v, string word, int pos, int i, int j)
        {
            if (i >= 0 && i < board.Length && j >= 0 && j < board[i].Length && board[i][j] == word[pos] && !v[i][j])
            {
                if (pos == word.Length - 1) return true;

                int[] r = new[] {1, 0, 0, -1};
                int[] c = new[] {0, 1, -1, 0};
                v[i][j] = true;
                bool res = false;
                for (int k = 0; k < 4; k++)
                {
                    res = Travel(board, v, word, pos + 1, i + r[k], j + c[k]);
                    if (res) break;
                }

                v[i][j] = false;
                return res;
            }
            else
            {
                return false;
            }
        }

        public string MinWindow(string s, string t)
        {
            if (string.IsNullOrEmpty(t) || string.IsNullOrEmpty(s) || t.Length > s.Length) return string.Empty;
            if (string.Equals(s, t)) return s;

            Dictionary<char, int> act = new Dictionary<char, int>();
            Dictionary<char, int> exp = new Dictionary<char, int>();
            foreach (char c in t) exp[c] = exp.GetValueOrDefault(c, 0) + 1;

            string res = string.Empty;
            int i = 0, j = 0, req = exp.Count, got = 0;
            while (j < s.Length)
            {
                act[s[j]] = act.GetValueOrDefault(s[j], 0) + 1;
                if (exp.ContainsKey(s[j]) && act[s[j]] == exp[s[j]]) got++;

                while (i <= j && got == req)
                {
                    string temp = s.Substring(i, j - i + 1);
                    if (string.IsNullOrEmpty(res) || temp.Length < res.Length) res = temp;

                    act[s[i]]--;
                    if (exp.ContainsKey(s[i]) && act[s[i]] < exp[s[i]]) got--;
                    i++;
                }

                j++;
            }

            return res;
        }

        public double FindMedianSortedArrays(int[] nums1, int[] nums2)
        {
            if (nums1 == null || nums2 == null) return 0.0;
            int n = nums1.Length, m = nums2.Length;
            if (n == 0 && m == 0) return 0.0;

            int target = ((m + n) / 2) + 1;
            int i = -1, j = -1;
            while ((i+j+2) < target)
            {
                if (i == n - 1) j++;
                else if (j == m - 1) i++;
                else if (nums1[i + 1] <= nums2[j + 1]) i++;
                else j++;
            }

            if ((m + n) % 2 != 0)
            {
                int res = int.MinValue;
                if (i >= 0) res = Math.Max(res, nums1[i]);
                if (j >= 0) res = Math.Max(res, nums2[j]);
                return res;
            }
            else
            {
                int a = int.MinValue;
                int b = int.MinValue;
                
                if (i == -1) {a = nums2[j]; j--;} 
                else if (j == -1) {a = nums1[i]; i--;}
                else if (nums1[i] >= nums2[j]) { a = nums1[i]; i--;}
                else { a = nums2[j]; j--; }

                if (i == -1) { b = nums2[j]; j--; }
                else if (j == -1) { b = nums1[i]; i--; }
                else if (nums1[i] >= nums2[j]) { b = nums1[i]; i--; }
                else { b = nums2[j]; j--; }

                return (a + b) / 2.0;
            }
        }

        public int[] MaxSlidingWindow(int[] nums, int k)
        {
            if (nums.Length <= 1) return nums;
            if (k >= nums.Length) return new[] {nums.Max()};
            LinkedList<int> ll = new LinkedList<int>();
            List<int> res = new List<int>();

            int i = 1;
            ll.AddFirst(0);
            while (i < k)
            {
                if (ll.Count == 0 || nums[i] < nums[ll.Last.Value])
                {
                    ll.AddLast(i);
                    i++;
                }
                else
                {
                    ll.RemoveLast();
                }
            }

            while (i < nums.Length)
            {
                res.Add(nums[ll.First.Value]);

                while (ll.Count > 0 && ll.First.Value <= (i-k)) ll.RemoveFirst();
                while (ll.Count > 0 && nums[ll.Last.Value] <= nums[i]) ll.RemoveLast();
                ll.AddLast(i);
                i++;
            }
            res.Add(nums[ll.First.Value]);
            return res.ToArray();
        }

        public int FirstMissingPositive(int[] nums)
        {
            if (nums.Length <= 0) return 1;
            List<int> l = new List<int>();
            foreach (int i in nums)
            {
                if (i > 0) l.Add(i);
            }

            for (int i = 0; i < l.Count; i++)
            {
                int pos = Math.Abs(l[i]) - 1;
                if (pos >= 0 && pos < l.Count && l[pos] > 0) l[pos] = -1 * l[pos];
            }

            int j = 0;
            for (; j < l.Count; j++)
            {
                if (l[j] > 0) return j + 1;
            }

            return j+1;
        }

        // TODO
        public int LongestValidParentheses(string s)
        {
            if (string.IsNullOrEmpty(s)) return 0;

            int maxLen = 0;

            int lbCount = 0;
            int rbCount = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '(') lbCount++;
                if (s[i] == ')') rbCount++;
                if (lbCount == rbCount)
                {
                    maxLen = Math.Max(maxLen, lbCount + rbCount);
                }
                else if (rbCount > lbCount)
                {
                    rbCount = 0;
                    lbCount = 0;
                }
            }

            lbCount = 0;
            rbCount = 0;
            for (int i = s.Length-1; i >= 0; i--)
            {
                if (s[i] == '(') lbCount++;
                if (s[i] == ')') rbCount++;
                if (lbCount == rbCount)
                {
                    maxLen = Math.Max(maxLen, lbCount + rbCount);
                }
                else if (lbCount > rbCount)
                {
                    rbCount = 0;
                    lbCount = 0;
                }
            }

            return maxLen;
        }
    }

    public class LRUCache
    {
        class Node
        {
            public int val;
            public int key;
            public Node pre;
            public Node post;
            public Node(int k, int v)
            {
                val = v;
                key = k;
                pre = null;
                post = null;
            }
        }

        private int cap;
        private int size;
        private Node head;
        private Node tail;
        private Dictionary<int, Node> map;

        public LRUCache(int capacity)
        {
            this.cap = capacity;
            this.size = 0;
            this.head = new Node(-1, -1);
            this.tail = new Node(-1, -1);
            this.map = new Dictionary<int, Node>();

            this.head.post = tail;
            this.tail.pre = head;
        }

        private Node AddToHead(Node n)
        {
            n.post = head.post;
            n.pre = head;

            head.post.pre = n;
            head.post = n;
            size += 1;
            return n;
        }

        private void RemoveNode(Node n)
        {
            if (size == 0) return;

            Node nPre = n.pre;
            Node nPost = n.post;

            nPre.post = nPost;
            nPost.pre = nPre;
            size -= 1;
        }

        private void RemoveFromTail()
        {
            Node n = tail.pre;
            RemoveNode(n);
            map.Remove(n.key);
        }

        public void Put(int key, int value)
        {
            Node n;
            if (map.ContainsKey(key))
            {
                n = map[key];
                RemoveNode(n);
                AddToHead(n);
                n.val = value;
            }
            else
            {
                n = new Node(key, value);
                AddToHead(n);
                map[key] = n;
            }
            
            if (size > cap) RemoveFromTail();
        }

        public int Get(int key)
        {
            Node n = map.GetValueOrDefault(key, null);
            if (n == null) return -1;

            RemoveNode(n);
            AddToHead(n);
            return n.val;
        }
    }

    public class MedianFinder
    {
        private List<int> lo;
        private List<int> hi;

        /** initialize your data structure here. */
        public MedianFinder()
        {
            lo = new List<int>();
            hi = new List<int>();
        }

        public void AddNum(int num)
        {
            lo.Add(num);
            lo = lo.OrderByDescending(i => i).ToList();
            int a = lo[0];
            lo.RemoveAt(0);

            hi.Add(a);
            hi = hi.OrderBy(i => i).ToList();
            if (hi.Count > lo.Count)
            {
                int b = hi[0];
                hi.RemoveAt(0);
                lo.Add(b);
                lo = lo.OrderByDescending(i => i).ToList();
            }
        }

        public double FindMedian()
        {
            if (lo.Count > hi.Count) return lo[0];
            else return (lo[0] + hi[0]) / 2.0;
        }
    }
}
 