using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonOA
{
    using System.Linq;
    using System.Security.Cryptography;

    class FacebookQuestions
    {
        public void Run()
        {
            TreeNode t1 = new TreeNode(2);
            
            TreeNode t2 = new TreeNode(3);
            TreeNode t3 = new TreeNode(1);
            t3.left = t2;

            TreeNode t4 = new TreeNode(0);
            t4.left = t1;
            t4.right = t3;


            Console.WriteLine("_______________________________________________________");


            WordDictionary wd = new WordDictionary();
            wd.AddWord("bad");
            Console.WriteLine($"Search b.. {wd.Search("b..")}");
            Console.WriteLine($"Search .ad {wd.Search(".ad")}");
            Console.WriteLine($"Search bad {wd.Search("bad")}");
            Console.WriteLine("_______________________________________________________");

            Console.WriteLine($"Length of longest substring with unique chars {LengthOfLongestSubstringKDistinct("eceba", 2)}");
            Console.WriteLine("_______________________________________________________");

            string[] arr = new String[]{"0:start:0","1:start:2","1:end:5","0:end:6"};
            int[] res3 = ExclusiveTime(2, arr);
            Console.WriteLine(string.Join(',', res3));
            Console.WriteLine("_______________________________________________________");


            Node n1 = new Node(1);
            Node n2 = new Node(3);
            Node n3 = new Node(2, n1, n2);
            Node n4 = new Node(5);
            Node n5 = new Node(4, n3, n4);

            Node res4 = TreeToDoublyList(n5);

            Console.WriteLine("_______________________________________________________");

        }

        public int DiameterOfBinaryTree(TreeNode root)
        {
            if (root == null) return 0;
            int l = GetHeight(root.left) + GetHeight(root.right);
            return Math.Max(l, Math.Max(DiameterOfBinaryTree(root.left), DiameterOfBinaryTree(root.right)));
        }

        public int GetHeight(TreeNode r)
        {
            if (r == null) return 0;

            int lh = GetHeight(r.left);
            int rh = GetHeight(r.right);

            return 1 + Math.Max(lh, rh);
        }

        public int[][] IntervalIntersection(int[][] A, int[][] B)
        {
            int n = A.Length, m = B.Length;
            int i = 0, j = 0;

            List<int[]> res = new List<int[]>();
            while (i < n && j < m)
            {
                int p1 = Math.Max(A[i][0], B[j][0]);
                int p2 = Math.Min(A[i][1], B[j][1]);

                if (p1 <= p2) res.Add(new[] {p1, p2});
                if (A[i][1] < B[j][1]) i++;
                else j++;
            }

            return res.ToArray();
        }

        public bool IsValidBST(TreeNode root)
        {
            return IsValidBST(root, long.MaxValue, long.MinValue);
        }

        private bool IsValidBST(TreeNode r, long max, long min)
        {
            if (r == null) return true;
            return r.val > min &&
                   r.val < max &&
                   IsValidBST(r.left, r.val, min) &&
                   IsValidBST(r.right, max, r.val);
        }

        public IList<string> WordBreak2(string s, IList<string> wordDict)
        {
            Dictionary<int, List<string>> map = new Dictionary<int, List<string>>();
            return WordBreakRecur(s, new HashSet<string>(wordDict), ref map, 0);
        }

        public List<string> WordBreakRecur(string s, HashSet<string> dict, ref Dictionary<int, List<string>> map, int start)
        {
            if (map.ContainsKey(start)) return map[start];

            List<string> res = new List<string>();
            if (s.Length == start)
            {
                res.Add("");
                return res;
            }

            for (int i = start+1; i <= s.Length; i++)
            {
                string subStr = s.Substring(start, i-start);
                if (dict.Contains(subStr))
                {
                    List<string> t = WordBreakRecur(s, dict, ref map, i);

                    foreach (string s1 in t)
                    {
                        res.Add(subStr + (string.IsNullOrEmpty(s1) ? "" : " " + s1));
                    }
                }
            }

            map[start] = res;
            return res;
        }

        public IList<int> RightSideView(TreeNode root)
        {
            List<int> res = new List<int>();
            if (root == null) return res;

            Queue<TreeNode> q = new Queue<TreeNode>();
            q.Enqueue(null);
            q.Enqueue(root);
            while (q.Any())
            {
                TreeNode t = q.Dequeue();
                if (t == null)
                {
                    if (q.Any())
                    {
                        q.Enqueue(null);
                        res.Add(q.Peek().val);
                    }
                }
                else
                {
                    if (t.right != null) q.Enqueue(t.right);
                    if (t.left != null) q.Enqueue(t.left);
                }
            }

            return res;
        }

        public bool WordBreak(string s, IList<string> wordDict)
        {
            Dictionary<int, bool> map = new Dictionary<int, bool>();
            return WordBreakRecur2(s, new HashSet<string>(wordDict), ref map, 0);
        }

        private bool WordBreakRecur2(string s, HashSet<string> dict, ref Dictionary<int, bool> map, int start)
        {
            if (map.ContainsKey(start)) return map[start];

            if (start == s.Length) return true;

            bool res = false;
            for (int i = start+1; i <= s.Length; i++)
            {
                if (dict.Contains(s.Substring(start, i-start)))
                {
                    res = WordBreakRecur2(s, dict, ref map, i);
                    if (res) break;
                }
            }

            map[start] = res;
            return res;
        }

        public int[][] KClosest(int[][] points, int K)
        {
            List<int> dist = new List<int>();
            foreach (int[] i in points)
            {
                dist.Add(CalculateDist(i));
            }

            dist.Sort();
            int v = dist[K-1];
            int[][] res = new int[K][];
            int pos = 0;
            foreach (int[] p in points)
            {
                if (CalculateDist(p) <= v)
                {
                    res[pos] = p;
                    pos++;
                }
            }

            return res;
        }

        private int CalculateDist(int[] A)
        {
            return (int) (Math.Pow(A[0], 2) + Math.Pow(A[1], 2));
        }

        public string NumberToWords(int num)
        {
            if (num == 0) return "Zero";

            int bil = num / 1000000000;
            int mil = (num - (bil * 1000000000)) / 1000000;
            int thou = (num - (bil * 1000000000) - (mil * 1000000)) / 1000;
            int hun = num - (bil * 1000000000) - (mil * 1000000) - (thou * 1000);

            string res = "";
            if (bil != 0) res += HandleThree(bil) + " Billion ";
            if (mil != 0) res += HandleThree(mil) + " Million ";
            if (thou != 0) res += HandleThree(thou) + " Thousand ";
            if (hun != 0) res += HandleThree(hun);

            return res.Trim();
        }

        private string Ones(int i)
        {
            switch (i)
            {
                case 1: return "One";
                case 2: return "Two";
                case 3: return "Three";
                case 4: return "Four";
                case 5: return "Five";
                case 6: return "Six";
                case 7: return "Seven";
                case 8: return "Eight";
                case 9: return "Nine";
                default: return "";
            }
        }

        private string Twos(int i)
        {
            switch (i)
            {
                case 10: return "Ten";
                case 11: return "Eleven";
                case 12: return "Twelve";
                case 13: return "Thirteen";
                case 14: return "Fourteen";
                case 15: return "Fifteen";
                case 16: return "Sixteen";
                case 17: return "Seventeen";
                case 18: return "Eighteen";
                case 19: return "Nineteen";
                case 2: return "Twenty";
                case 3: return "Thirty";
                case 4: return "Forty";
                case 5: return "Fifty";
                case 6: return "Sixty";
                case 7: return "Seventy";
                case 8: return "Eighty";
                case 9: return "Ninety";
                default: return "";
            }
        }

        private string HandleTwo(int i)
        {
            if (i < 10) return Ones(i);
            else if (i < 20) return Twos(i);
            else
            {
                int ones = i % 10;
                int tens = i / 10;
                string res = Twos(tens);
                if (ones > 0) res += " " + Ones(ones);
                return res;
            }
        }

        private string HandleThree(int i)
        {
            if (i > 99)
            {
                int h = i / 100;
                int t = i % 100;
                string res = Ones(h) + " Hundred";
                if (t > 0) res += " " + HandleTwo(t);
                return res;
            }
            else
            {
                return HandleTwo(i);
            }
        }

        public string SimplifyPath(string path)
        {
            string[] paths = path.Split('/');
            Stack<string> stk = new Stack<string>();
            foreach (string s in paths)
            {
                if (s.Equals(".."))
                {
                    if (stk.Any()) stk.Pop();
                }
                else
                {
                    if (!string.IsNullOrEmpty(s) && !s.Equals(".") && !s.Equals("/")) stk.Push(s.Trim());
                }
            }

            string res = "";
            while (stk.Any())
            {
                res = "/" + stk.Pop() + res;
            }

            return string.IsNullOrEmpty(res) ? "/" : res;
        }

        public int[] SearchRange(int[] nums, int target)
        {
            if (nums == null || nums.Length <= 0) return new[] { -1, -1 };
            int l = 0, r = nums.Length - 1;
            int pos = 0;
            while (l <= r)
            {
                pos = (l + r) / 2;
                if (nums[pos] == target) break;
                if (nums[pos] < target) l = pos + 1;
                else r = pos - 1;
            }

            if (nums[pos] != target) return new[] {-1, -1};
            r = l = pos;
            while (r < nums.Length - 1 && nums[r + 1] == target) r++;
            while (l > 0 && nums[l - 1] == target) l--;
            return new[] {l, r};
        }

        public int MinMeetingRooms(int[][] intervals)
        {
            if (intervals == null || intervals.Length <= 0) return 0;

            List<int> start = new List<int>();
            List<int> end = new List<int>();
            foreach (int[] interval in intervals)
            {
                start.Add(interval[0]);
                end.Add(interval[1]);
            }

            start.Sort();
            end.Sort();
            int count = 0;
            int i = 0, j = 0;
            while (i < intervals.Length && j < intervals.Length)
            {
                if (start[i] >= end[j]) j++;
                else count++;
                i++;
            }

            return count;
        }

        public bool IsAlienSorted(string[] words, string order)
        {
            Dictionary<char, int> map = new Dictionary<char, int>();
            for (int i = 0; i < order.Length; i++)
            {
                map[order[i]] = i;
            }

            for (int i = 1; i < words.Length; i++)
            {
                string a = words[i - 1];
                string b = words[i];
                bool diffFound = false;
                for (int j = 0; j < Math.Min(a.Length, b.Length); j++)
                {
                    if (a[j] != b[j])
                    {
                        diffFound = true;
                        if (map[a[j]] > map[b[j]]) return false;
                        else break;
                    }
                }

                if (!diffFound && a.Length > b.Length) return false;
            }

            return true;
        }

        // TODO
        public IList<int> FindAnagrams(string s, string p)
        {
            List<int> res = new List<int>();
            if (s.Length < p.Length) return res;

            Dictionary<char, int> count = new Dictionary<char, int>();
            foreach (char c in p) count[c] = count.GetValueOrDefault(c, 0) + 1;
            int start = 0, end = 0;
            while (end < s.Length)
            {
                if (count.Values.All(i => i == 0)) res.Add(start);

                if (end - start + 1 > p.Length)
                {
                    if (count.ContainsKey(s[start])) count[s[start]]++;
                    start++;
                }

                if (count.ContainsKey(s[end])) count[s[end]]--;
                end++;
            }

            while (start < s.Length)
            {
                if (count.Values.All(i => i == 0)) res.Add(start);
                if (count.ContainsKey(s[start])) count[s[start]]--;
                start++;
            }

            return res;
        }

        // TODO
        public IList<int> DistanceK(TreeNode root, TreeNode target, int K)
        {
            List<int> res = new List<int>();
            if (root == null) return res;
            if (K == 0)
            {
                res.Add(target.val);
                return res;
            }

            Dictionary<TreeNode, TreeNode> map = new Dictionary<TreeNode, TreeNode>();
            Queue<TreeNode> q = new Queue<TreeNode>();
            map[root] = null;
            q.Enqueue(root);
            while (q.Any())
            {
                TreeNode t = q.Dequeue();
                if (t.left != null)
                {
                    q.Enqueue(t.left);
                    map[t.left] = t;
                }
                if (t.right != null)
                {
                    q.Enqueue(t.right);
                    map[t.right] = t;
                }
            }

            res = GetAtDistFromTarget(target, target, K, map);
            return res;
        }

        private List<int> GetAtDistFromTarget(TreeNode r, TreeNode t, int K, Dictionary<TreeNode, TreeNode> map)
        {
            if (r == null) return new List<int>();
            List<int> res = new List<int>();
            GetAtDist(r, t, K, ref res);
            res.AddRange(GetAtDistFromTarget(map[r], r, K-1, map));
            return res;
        }
        private void GetAtDist(TreeNode r, TreeNode t, int K, ref List<int> res)
        {
            if (r == null) return;
            if (K == 0)
            {
                if (r != t && r.val != -1) res.Add(r.val);
                return;
            }

            r.val = -1;
            GetAtDist(r.left, t, K-1, ref res);
            GetAtDist(r.right, t, K-1, ref res);
        }

        public bool ValidPalindromeWith1Remove(string s)
        {
            if (string.IsNullOrEmpty(s)) return true;

            for (int i = 0, j = s.Length-1; i <= j; i++, j--)
            {
                if (s[i] != s[j])
                {
                    return IsValidPalindrome(s, i + 1, j) || IsValidPalindrome(s, i, j - 1);
                }
            }

            return true;
        }

        private bool IsValidPalindrome(string s, int a, int b)
        {
            while (a <= b)
            {
                if (s[a] != s[b]) return false;
                a++;
                b--;
            }

            return true;
        }

        public string AddStrings(string num1, string num2)
        {
            int c = 0;
            int s = 0;
            string res = "";
            int i = num1.Length-1, j = num2.Length-1;

            while (i >= 0 || j >= 0)
            {
                int a = i >= 0 ? num1[i] - '0' : 0;
                int b = j >= 0 ? num2[j] - '0' : 0;

                s = a + b + c;
                res = (s % 10) + res;
                c = s / 10;

                i--;
                j--;
            }

            if (c != 0) res = c + res;
            return res;
        }

        public int SubarraySum(int[] nums, int k)
        {
            Dictionary<int, List<int>> map = new Dictionary<int, List<int>>();
            int s = 0;
            int count = 0;
            map[0] = new List<int>(){-1};
            for (int i = 0; i < nums.Length; i++)
            {
                s = s + nums[i];
                if (map.ContainsKey(s - k))
                {
                    count += map[s - k].Count;
                }

                if (!map.ContainsKey(s)) map[s] = new List<int>();
                map[s].Add(i);
            }

            return count;
        }

        public string MinWindow(string s, string t)
        {
            if (string.IsNullOrEmpty(t) || string.IsNullOrEmpty(s) || t.Length > s.Length) return string.Empty;
            if (string.Equals(s, t)) return s;

            Dictionary<char, int> tMap = new Dictionary<char, int>();
            Dictionary<char, int> sMap = new Dictionary<char, int>();

            foreach (char c in t) tMap[c] = tMap.GetValueOrDefault(c, 0) + 1;
            int[] res = new[] {int.MaxValue, -1, -1};
            int req = tMap.Keys.Count;
            int got = 0;
            int start = 0, end = 0;
            while (end < s.Length)
            {
                sMap[s[end]] = sMap.GetValueOrDefault(s[end], 0) + 1;
                if (tMap.ContainsKey(s[end]) && sMap[s[end]] == tMap[s[end]]) got += 1;

                while (start <= end && got == req)
                {
                    if (end - start + 1 < res[0])
                    {
                        res[0] = end - start + 1;
                        res[1] = start;
                        res[2] = end;
                    }

                    sMap[s[start]]--;
                    if (tMap.ContainsKey(s[start]) && sMap[s[start]] < tMap[s[start]]) got--;
                    start += 1;
                }

                end++;
            }

            return res[1] == -1 ? "" : s.Substring(res[1], res[0]);
        }

        public IList<IList<string>> AccountsMerge(IList<IList<string>> accounts)
        {
            Dictionary<string, int> emailToId = new Dictionary<string, int>();
            Dictionary<string, string> emailToName = new Dictionary<string, string>();
            int id = 0;
            foreach (IList<string> list in accounts)
            {
                string name = list[0];
                for (int i = 1; i < list.Count; i++)
                {
                    string acnt = list[i];
                    if (!emailToId.ContainsKey(acnt))
                    {
                        emailToId[list[i]] = id;
                        id++;
                    }

                    if (!emailToName.ContainsKey(acnt)) emailToName[acnt] = name;
                }
            }

            UnionFind uf = new UnionFind(id);
            foreach (IList<string> account in accounts)
            {
                for (int i = 2; i < account.Count; i++)
                {
                    uf.Union(emailToId[account[1]], emailToId[account[i]]);
                }
            }

            Dictionary<int, List<string>> res = new Dictionary<int, List<string>>();
            foreach (KeyValuePair<string, int> p in emailToId)
            {
                int par = uf.Find(p.Value);
                if (!res.ContainsKey(par)) res[par] = new List<string>();
                res[par].Add(p.Key);
            }

            IList<IList<string>> res1 = new List<IList<string>>();
            foreach (List<string> value in res.Values)
            {
                value.Sort(StringComparer.Ordinal);
                List<string> newList = new List<string>();
                newList.Add(emailToName[value[0]]);
                newList.AddRange(value);
                res1.Add(newList);
            }

            return res1;
        }

        public string AddBinary(string a, string b)
        {
            string res = string.Empty;
            int s = 0;
            int i = a.Length - 1, j = b.Length - 1;
            while (i >= 0 || j >= 0)
            {
                s = s + (i >= 0 ? a[i] - '0' : 0);
                s = s + (j >= 0 ? b[j] - '0' : 0);

                res = (s % 2) + res;
                s = s / 2;
                i--;
                j--;
            }

            if (s > 0) res = s + res;
            return res;
        }

        public int FindKthLargest(int[] nums, int k)
        {
            return SelectionSort(nums, 0, nums.Length - 1, nums.Length - k);
        }

        private int SelectionSort(int[] nums, int l, int r, int t)
        {
            if (l == r) return nums[l];

            Random rand = new Random();
            int randPos = (l+r)/2;
            int pivotPos = Partition(nums, l, r, randPos);
            if (pivotPos == t) return nums[t];
            else if (pivotPos > t) return SelectionSort(nums, l, pivotPos - 1, t);
            else return SelectionSort(nums, pivotPos + 1, r, t);
        }

        private int Partition(int[] nums, int l, int r, int randPos)
        {
            int randNum = nums[randPos];
            Swap(nums, randPos, r);
            int start = l;
            for (int i = l; i < r; i++)
            {
                if (nums[i] < randNum)
                {
                    Swap(nums, start, i);
                    start++;
                }
            }

            Swap(nums, start, r);
            return start;
        }

        private void Swap(int[] nums, int i, int j)
        {
            int t = nums[i];
            nums[i] = nums[j];
            nums[j] = t;
        }

        // TODO
        public void NextPermutation(int[] nums)
        {
            if (nums.Length < 2) return;
            int n = nums.Length;
            int i = n - 2;
            int j;
            while (i >= 0 && nums[i + 1] <= nums[i]) i--;

            if (i >= 0)
            {
                j = n-1;
                while (j >= 0 && nums[j] <= nums[i]) j--;

                Swap(nums, i, j);
            }

            i = i + 1; j = n - 1;
            while (i < j)
            {
                Swap(nums, i, j);
                i++;
                j--;
            }
        }

        public string AlienOrder(string[] words)
        {
            Dictionary<char, List<char>> map = new Dictionary<char, List<char>>();
            Dictionary<char, int> degree = new Dictionary<char, int>();

            foreach (string w in words)
            {
                foreach (char c in w)
                {
                    map[c] = new List<char>();
                    degree[c] = 0;
                }
            }
            for (int i = 1; i < words.Length; i++)
            {
                string a = words[i - 1];
                string b = words[i];
                for (int j = 0; j < Math.Min(a.Length, b.Length); j++)
                {
                    if (a[j] != b[j])
                    {
                        map[a[j]].Add(b[j]);
                        degree[b[j]]++;
                        break;
                    }
                }
            }

            string res = string.Empty;
            Queue<char> q = new Queue<char>();
            foreach (KeyValuePair<char, int> p in degree)
            {
                if(p.Value == 0) q.Enqueue(p.Key);
            }

            while (q.Any())
            {
                char c = q.Dequeue();
                res += c;
                List<char> adj = map[c];
                foreach (char ch in adj)
                {
                    int d = degree[ch];
                    degree[ch] = d - 1;
                    if (d-1 == 0) q.Enqueue(ch);
                }
            }

            return res;
        }

        // TODO
        public string MinRemoveToMakeValid(string s)
        {
            List<char> sb = s.ToCharArray().ToList();
            sb = MakeValidParen(sb, '(', ')');
            sb.Reverse();
            sb = MakeValidParen(sb, ')', '(');
            sb.Reverse();
            return new string(sb.ToArray());
        }

        private List<char> MakeValidParen(List<char> s, char openBrace, char closeBrace)
        {
            List<char> res = new List<char>();
            int balance = 0;
            foreach (char c in s)
            {
                if (c == openBrace) balance++;
                if (c == closeBrace)
                {
                    if (balance == 0) continue;
                    balance--;
                }

                res.Add(c);
            }

            return res;
        }

        // TODO
        public int MinAddToMakeValid(string S)
        {
            int curCount = 0;
            int count = 0;
            foreach (char c in S)
            {
                if (c == '(') curCount++;
                if (c == ')')
                {
                    if (curCount == 0) count++;
                    else curCount--;
                }
            }

            return count + curCount;
        }

        public string ReorganizeString(string S)
        {
            Dictionary<char, int> map = new Dictionary<char, int>();
            foreach (char c in S) map[c] = map.GetValueOrDefault(c, 0) + 1;

            map = new Dictionary<char, int>(map.OrderBy(i => i.Value));
            char[] res = new char[S.Length];
            int pos = 1;
            foreach (KeyValuePair<char, int> p in map)
            {
                if (p.Value > (S.Length+1) / 2) return string.Empty;
                int i = p.Value;
                while (i > 0)
                {
                    res[pos] = p.Key;
                    i--;
                    pos += 2;
                    if (pos >= S.Length) pos = 0;
                }
            }

            return new string(res);
        }

        // TODO
        public int LengthOfLongestSubstringKDistinct(string s, int k)
        {
            if (k == 0) return 0;
            Dictionary<char, int> map = new Dictionary<char, int>();

            int res = 0;
            int i = 0;
            for (int j = i; j < s.Length; j++)
            {
                map[s[j]] = j;
                if (map.Count == k) res = Math.Max(res, j - i + 1);
                if (map.Count > k)
                {
                    KeyValuePair<char, int> p = map.Aggregate((l, r) => l.Value < r.Value ? l : r);
                    i = p.Value + 1;
                    map.Remove(p.Key);
                }
            }

            return res == 0 ? s.Length : res;
        }

        public int[] ExclusiveTime(int n, IList<string> logs)
        {
            int[] res = new int[n];
            Array.Fill(res, 0);
            Stack<int[]> stk = new Stack<int[]>();
            foreach (string l in logs)
            {
                string[] arr = l.Split(':');
                int pnum = Convert.ToInt32(arr[0]);
                string op = arr[1];
                int time = Convert.ToInt32(arr[2]);
                if (op.Equals("start"))
                {
                    if (stk.Any())
                    {
                        int[] t = stk.Peek();
                        res[t[0]] += time - t[1];
                        t[1] = -1;
                    }

                    stk.Push(new[] { pnum, time });
                }
                else if (op.Equals("end"))
                {
                    if (stk.Any())
                    {
                        int[] t = stk.Peek();
                        if (t[0] == pnum)
                        {
                            res[pnum] += time - t[1] + 1;
                            stk.Pop();
                            if (stk.Any() && stk.Peek()[1] == -1) stk.Peek()[1] = time+1;
                        }
                    }
                }
            }

            return res;
        }

        public Node TreeToDoublyList(Node root)
        {
            Node first = null;
            Node last = null;

            Stack<Node> s = new Stack<Node>();
            Populate(s, root);

            while (s.Any())
            {
                Node n = s.Pop();
                if (n.right != null) Populate(s, n.right);

                if (first == null)
                {
                    first = n;
                    last = first;
                }
                else
                {
                    last.left = n;
                    n.right = last;
                    last = last.left;
                }
            }

            last.right = first;
            first.left = last;
            return first;
        }

        private void Populate(Stack<Node> s, Node r)
        {
            Node n = r;
            while (n != null)
            {
                s.Push(n);
                n = n.left;
            }
        }

        public IList<IList<int>> VerticalOrder(TreeNode root)
        {
            SortedDictionary<int, List<int>> map = new SortedDictionary<int, List<int>>();
            VerticalTraversalBFS(root, 0, ref map);
            IList<IList<int>> res = new List<IList<int>>();
            foreach (KeyValuePair<int, List<int>> pair in map)
            {
                res.Add(new List<int>(pair.Value));
            }

            return res;
        }

        private void VerticalTraversal(TreeNode r, int l, ref SortedDictionary<int, List<int>> res)
        {
            if (r == null) return;

            if (!res.ContainsKey(l)) res[l] = new List<int>();
            res[l].Add(r.val);
            VerticalTraversal(r.left, l-1, ref res);
            VerticalTraversal(r.right, l+1, ref res);
        }

        private void VerticalTraversalBFS(TreeNode r, int l, ref SortedDictionary<int, List<int>> res)
        {
            if (r == null) return;

            Queue<KeyValuePair<TreeNode, int>> q = new Queue<KeyValuePair<TreeNode, int>>();
            q.Enqueue(new KeyValuePair<TreeNode, int>(r, l));
            while (q.Any())
            {
                TreeNode cur = q.Peek().Key;
                int lev = q.Peek().Value;
                q.Dequeue();

                if (!res.ContainsKey(lev)) res[lev] = new List<int>();
                res[lev].Add(cur.val);

                if (cur.left != null) q.Enqueue(new KeyValuePair<TreeNode, int>(cur.left, lev-1));
                if (cur.right != null) q.Enqueue(new KeyValuePair<TreeNode, int>(cur.right, lev+1));
            }
        }

        public class BSTIterator
        {
            private Stack<TreeNode> stk;
            public BSTIterator(TreeNode root)
            {
                stk = new Stack<TreeNode>();
                TraverseLeft(root);
            }

            /** @return the next smallest number */
            public int Next()
            {
                TreeNode n = stk.Pop();
                TraverseLeft(n.right);
                return n.val;
            }

            /** @return whether we have a next smallest number */
            public bool HasNext()
            {
                return stk.Any();
            }

            private void TraverseLeft(TreeNode r)
            {
                if (r == null) return;

                while (r != null)
                {
                    stk.Push(r);
                    r = r.left;
                }
            }
        }

        public class UnionFind
        {
            private int[] parent;
            public UnionFind(int size)
            {
                parent = new int[size];
                for (int i = 0; i < size; i++)
                {
                    parent[i] = i;
                }
            }

            public int Find(int id)
            {
                int i = id;
                while (parent[i] != i)
                {
                    int p = parent[i];
                    parent[id] = p;
                    i = p;
                }

                return parent[id];
            }

            public void Union(int x, int y)
            {
                int parX = Find(x);
                int parY = Find(y);
                if (parX == parY) return;
                parent[Find(x)] = Find(y);
            }
        }

        public class WordDictionary
        {
            public class TNode
            {
                public Dictionary<char, TNode> map;
                public bool isTerminal;
                public TNode()
                {
                    map = new Dictionary<char, TNode>();
                    isTerminal = false;
                }
            }

            private TNode root;

            /** Initialize your data structure here. */
            public WordDictionary()
            {
                root = new TNode();
            }

            /** Adds a word into the data structure. */
            public void AddWord(string word)
            {
                TNode n = root;
                foreach (char c in word)
                {
                    if (!n.map.ContainsKey(c))
                    {
                        n.map[c] = new TNode();
                    }

                    n = n.map[c];
                }

                n.isTerminal = true;
            }

            /** Returns if the word is in the data structure. A word could contain the dot character '.' to represent any one letter. */
            public bool Search(string word)
            {
                return Search(root, word, 0);
            }

            private bool Search(TNode n, string word, int p)
            {
                if (p == word.Length) return n.isTerminal;

                if (word[p] == '.')
                {
                    foreach (TNode node in n.map.Values)
                    {
                        if(Search(node, word, p + 1)) return true;
                    }
                }
                else
                {
                    if (!n.map.ContainsKey(word[p])) return false;
                    return Search(n.map[word[p]], word, p + 1);
                }

                return false;
            }
        }

        public class Node
        {
            public int val;
            public Node left;
            public Node right;

            public Node() { }

            public Node(int _val)
            {
                val = _val;
                left = null;
                right = null;
            }

            public Node(int _val, Node _left, Node _right)
            {
                val = _val;
                left = _left;
                right = _right;
            }
        }
    }
}
