using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonOA
{
    using System.Linq;
    using System.Runtime.InteropServices;

    class FacebookQuestions2
    {
        public void Run()
        {
            int[][] rooms = new int[1][];
            rooms[0] = new[] {2147483647, 0, 2147483647, 2147483647, 0, 2147483647, -1, 2147483647};
            WallsAndGates(rooms);
            foreach (int[] r in rooms)
            {
                Console.WriteLine(string.Join(',', r));
            }
            Console.WriteLine("_______________________________________________________");


            int[][] g = new int[4][];
            g[0] = new[] {1, 2, 3};
            g[1] = new[] {0, 2};
            g[2] = new[] {0, 1, 3};
            g[3] = new[] {0, 2};
            Console.WriteLine($"Is bipartitite: {IsBipartite(g)}");
            Console.WriteLine("_______________________________________________________");

            Node n = new Node(1);
            n.next = n;
            Insert(n, 2);
            Console.WriteLine("_______________________________________________________");

            string[] strs = new[] {"abc", "bcd", "acef", "xyz", "az", "ba", "a", "z"};
            IList<IList<string>> l = GroupStrings(strs);
            foreach (IList<string> list in l)
            {
                Console.WriteLine(string.Join(',',list));
            }
            Console.WriteLine("_______________________________________________________");

            ListNode head = LinkedList.CreateList(new[] {1, 2, 3, 4, 5});
            ReorderList(head);
            LinkedList.PrintListNode(head);
            Console.WriteLine("_______________________________________________________");

            string a = "123";
            string b = "456";
            string c = MultiplyString(a, b);
            Console.WriteLine($"{a}*{b} = {c}");
            Console.WriteLine("_______________________________________________________");

            a = "3+2*2";
            a = " 3/2 ";
            Console.WriteLine($"result of {a} is {Calculate(a)}");
            Console.WriteLine("_______________________________________________________");
        }

        public IList<string> FindStrobogrammatic(int n)
        {
            return FindStrobogrammatic(n, n);
        }
        private IList<string> FindStrobogrammatic(int n, int m)
        {
            if (n == 0) return new List<string>() {""};
            if (n == 1) return new List<string>() {"0", "1", "8"};

            List<string> nums = FindStrobogrammatic(n - 2, m).ToList();

            List<string> res = new List<string>();
            foreach (string s in nums)
            {
                if (n != m) res.Add("0"+s+"0");

                res.Add("1" + s + "1");
                res.Add("6" + s + "9");
                res.Add("8" + s + "8");
                res.Add("9" + s + "6");
            }

            return res;
        }

        // Maximum age is 120
        public int NumFriendRequests(int[] ages)
        {
            int[] ageBuckets = new int[121];
            Array.Fill(ageBuckets, 0);
            foreach (int a in ages) ageBuckets[a]++;

            int count = 0;
            for (int i = 0; i < ageBuckets.Length; i++)
            {
                if (ageBuckets[i] == 0) continue;
                for (int j = 0; j < ageBuckets.Length; j++)
                {
                    if (ageBuckets[j] == 0) continue;
                    else if (j <= ((i * 0.5) + 7)) continue;
                    else if (j > i) continue;
                    else if (j > 100 && 100 > i) continue;
                    else
                    {
                        count = count + (ageBuckets[i] * ageBuckets[j]);
                        if (i == j) count = count - ageBuckets[i];
                    }
                }
            }

            return count;
        }

        public int MissingElement(int[] nums, int k)
        {
            int i = 0;
            for (; i < nums.Length-1; i++)
            {
                if (nums[i+1] == nums[i]+1) continue;
                else
                {
                    int diff = nums[i + 1] - nums[i];
                    if (diff >= k)
                    {
                        return nums[i] + k;
                    }
                    else
                    {
                        k = k - diff;
                    }
                }
            }

            return nums[i] + k;
        }

        public bool IsCompleteTree(TreeNode root)
        {
            int i = 0;
            List<KeyValuePair<TreeNode, int>> l = new List<KeyValuePair<TreeNode, int>>();
            l.Add(new KeyValuePair<TreeNode, int>(root, 0));
            while (i < l.Count)
            {
                int lch = (2 * i) + 1;
                int rch = (2 * i) + 2;

                TreeNode n = l[i].Key;
                if (n.left != null) l.Add(new KeyValuePair<TreeNode, int>(n.left, lch));
                if (n.right != null) l.Add(new KeyValuePair<TreeNode, int>(n.right, rch));

                i++;
            }

            return l[l.Count - 1].Value == i-1;
        }

        public void WallsAndGates(int[][] rooms)
        {
            for (int i = 0; i < rooms.Length; i++)
            {
                for (int j = 0; j < rooms[i].Length; j++)
                {
                    if (rooms[i][j] == 0) WallAndGates(rooms, i, j, 0);
                }
            }
        }
        private void WallAndGates(int[][] mat, int i, int j, int d)
        {
            if (i < 0 || i >= mat.Length || j < 0 || j >= mat[i].Length || mat[i][j] < d) return;

            mat[i][j] = d;
            WallAndGates(mat, i-1, j, d+1);
            WallAndGates(mat, i+1, j, d+1);
            WallAndGates(mat, i, j-1, d+1);
            WallAndGates(mat, i, j+1, d+1);
        }

        // Max consecutive ones.
        public int LongestOnes(int[] A, int K)
        {
            int count = 0;
            int i = 0;
            int j = 0;

            while (j < A.Length)
            {
                if (K < 0)
                {
                    if (A[i] == 0) K += 1;
                    i += 1;
                    continue;
                }
                else count = Math.Max(count, j - i);

                if (A[j] == 0) K -= 1;
                j++;
            }

            if (K >= 0) count = Math.Max(count, j - i);
            return count;
        }

        public bool CheckSubarraySum(int[] nums, int k)
        {
            Dictionary<int, int> map = new Dictionary<int, int>();
            map[0] = -1;
            int sum = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                sum += nums[i];
                if (k != 0) sum = sum % k;
                if (map.ContainsKey(sum))
                {
                    if (i - map[sum] >= 2) return true;
                }
                else map[sum] = i;
            }

            return false;
        }

        public int[] Intersection(int[] nums1, int[] nums2)
        {
            HashSet<int> set1 = new HashSet<int>(nums1);
            HashSet<int> set2 = new HashSet<int>(nums2);
            
            List<int> res = new List<int>();
            foreach (int i in set1)
            {
                if (set2.Contains(i)) res.Add(i);
            }

            return res.ToArray();
        }

        public int[] Intersection2(int[] nums1, int[] nums2)
        {
            Dictionary<int, int> map = new Dictionary<int, int>();
            foreach (int i in nums1)
            {
                map[i] = map.GetValueOrDefault(i, 0) + 1;
            }

            List<int> res = new List<int>();
            foreach (int i in nums2)
            {
                if (map.ContainsKey(i))
                {
                    res.Add(i);
                    map[i] -= 1;
                    if (map[i] == 0) map.Remove(i);
                }
            }

            return res.ToArray();
        }

        public bool IsBipartite(int[][] graph)
        {
            int[] color = new int[graph.Length];
            Array.Fill(color, -1);
            Stack<int> s = new Stack<int>();

            for (int i = 0; i < graph.Length; i++)
            {
                if(color[i] != -1) continue;

                s.Push(i);
                color[i] = 0;
                while (s.Any())
                {
                    int cur = s.Pop();
                    int newCol = color[cur] == 0 ? 1 : 0;
                    
                    foreach (int n in graph[cur])
                    {
                        if (color[n] == -1)
                        {
                            color[n] = newCol;
                            s.Push(n);
                        }
                        else if (color[n] == color[cur]) return false;
                    }
                }
            }

            return true;
        }

        public string ToGoatLatin(string S)
        {
            string[] words = S.Trim().Split(' ');
            string sb = string.Empty;
            HashSet<char> v = new HashSet<char>() {'a', 'e', 'i', 'o', 'u', 'A', 'E', 'I', 'O', 'U'};

            for (int i = 0; i < words.Length; i++)
            {
                string w = words[i];
                if (!v.Contains(w[0])) w = w.Substring(1) + w.Substring(0, 1);

                string append = "ma";
                int c = i+1;
                while (c > 0)
                {
                    append += "a";
                    c--;
                }

                sb += (w + append + " ");
            }

            return sb.Trim();
        }

        public Node Insert(Node head, int insertVal)
        {
            Node n = new Node(insertVal);
            if (head == null)
            {
                head = n;
                head.next = head;
                return head;
            }

            Node cur = head;
            Node next = cur.next;
            while (true)
            {
                if (cur.val <= insertVal && insertVal <= next.val ||
                    cur.val > next.val && insertVal > cur.val ||
                    cur.val > next.val && insertVal < next.val)
                {
                    break;
                }

                cur = next;
                next = next.next;

                if (cur == head) break;
            }

            n.next = next;
            cur.next = n;
            return head;
        }

        public bool IsNumber(string s)
        {
            if (string.IsNullOrEmpty(s)) return false;

            s = s.Trim();
            bool pointSeen = false;
            bool numSeen = false;
            bool numSeenAfterE = false;
            bool eSeen = false;
            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];
                if ('0' <= c && c <= '9')
                {
                    numSeen = true;
                    numSeenAfterE = true;
                }
                else if (c == '-' || c == '+')
                {
                    if ((i != 0 && s[i - 1] != 'e') || s.Length == 1) return false;
                }
                else if (c == '.')
                {
                    if (pointSeen || eSeen) return false;
                    pointSeen = true;
                }
                else if (c == 'e')
                {
                    if (eSeen || !numSeen) return false;
                    eSeen = true;
                    numSeenAfterE = false;
                }
                else
                {
                    return false;
                }
            }

            return numSeen && numSeenAfterE;
        }

        public int MinKnightMoves(int x, int y)
        {
            return MinKnightMovedfs(Math.Abs(x), Math.Abs(y), new Dictionary<int, int>(), 333);
        }

        public int MinKnightMovedfs(int x, int y, Dictionary<int, int> map, int MOD)
        {
            int index = x * MOD + y;
            if (map.ContainsKey(index))
            {
                return map[index];
            }
            int ans = 0;
            if (x + y == 0)
            {
                ans = 0;
            }
            else if (x + y == 2)
            {
                ans = 2;
            }
            else
            {
                ans = Math.Min(MinKnightMovedfs(Math.Abs(x - 1), Math.Abs(y - 2), map, MOD),
                          MinKnightMovedfs(Math.Abs(x - 2), Math.Abs(y - 1), map, MOD)) + 1;
            }
            map[index] = ans;
            return ans;
        }

        public bool IsStrobogrammatic(string num)
        {
            Dictionary<char, char> map = new Dictionary<char, char>();
            map['0'] = '0';
            map['1'] = '1';
            map['6'] = '9';
            map['8'] = '8';
            map['9'] = '6';

            string res = "";
            foreach (char c in num)
            {
                if (!map.ContainsKey(c)) return false;
                res = map[c] + res;
            }

            return num.Equals(res);
        }

        public int FindPeakElement(int[] nums)
        {
            if (nums == null || nums.Length == 0) return -1;
            if (nums.Length == 1) return 0;
            bool[] l = new bool[nums.Length];
            bool[] r = new bool[nums.Length];
            Array.Fill(l, false);
            Array.Fill(r, false);
            for (int i = 1; i < nums.Length; i++)
            {
                if (nums[i] > nums[i - 1]) l[i] = true;
            }

            for (int i = nums.Length-2; i >= 0; i--)
            {
                if (nums[i] > nums[i + 1]) r[i] = true;
            }

            if (l[0] != r[0]) return 0;
            if (l[nums.Length - 1] != r[nums.Length - 1]) return nums.Length - 1;
            for (int i = 1; i < nums.Length-1; i++)
            {
                if (l[i] && r[i]) return i;
            }

            return -1;
        }

        public bool IsOneEditDistance(string s, string t)
        {
            int ns = s.Length, nt = t.Length;
            int diff = Math.Abs(ns - nt);
            if (diff >= 2) return false;

            for (int i = 0; i < Math.Min(ns, nt); i++)
            {
                if (s[i] != t[i])
                {
                    if (diff == 0) return s.Substring(i + 1).Equals(t.Substring(i + 1));
                    else return s.Substring(i).Equals(t.Substring(i + 1)) || s.Substring(i + 1).Equals(t.Substring(i));
                }
            }

            return diff == 1;
        }

        public int MaxVacationDays(int[][] flights, int[][] days)
        {
            Dictionary<int, int> memo = new Dictionary<int, int>();
            return MaxVacationDaysRecur(flights, days, memo, 0, 0);
        }
        public int MaxVacationDaysRecur(int[][] flights, int[][] days, Dictionary<int, int> memo, int c, int w)
        {
            if (w == days[0].Length) return 0;
            if (memo.ContainsKey((c*333)+w)) return memo[(c * 333) + w];

            int res = 0;
            for (int i = 0; i < flights.Length; i++)
            {
                if (flights[c][i] == 1 || i == c)
                {
                    int t = days[i][w] + MaxVacationDaysRecur(flights, days, memo, i, w + 1);
                    res = Math.Max(res, t);
                }
            }

            memo[(c * 333) + w] = res;
            return res;
        }

        public IList<IList<string>> GroupStrings(string[] strings)
        {
            Dictionary<string, List<string>> map = new Dictionary<string, List<string>>();
            foreach (string s in strings)
            {
                string key = GetKey(s);
                if (!map.ContainsKey(key)) map[key] = new List<string>();
                map[key].Add(s);
            }

            IList<IList<string>> res = new List<IList<string>>();
            foreach (List<string> l in map.Values)
            {
                l.Sort();
                res.Add(l);
            }

            return res;
        }

        private string GetKey(string s)
        {
            List<int> l = new List<int>();
            for (int i = 1; i < s.Length; i++)
            {
                int d = s[i] - s[i - 1];
                l.Add(d < 0 ? d + 26 : d);
            }

            return string.Join(',', l);
        }

        public int RangeSumBST(TreeNode root, int L, int R)
        {
            if (root == null) return 0;

            int lsum = root.val <= L ? 0 : RangeSumBST(root.left, L, R);
            int rsum = root.val >= R ? 0 : RangeSumBST(root.right, L, R);

            int cur = (L <= root.val && root.val <= R) ? root.val : 0;

            return cur + lsum + rsum;
        }

        public void ReorderList(ListNode head)
        {
            if (head == null || head.next == null) return;

            ListNode s = head, f = head;
            while (f.next != null && f.next.next != null)
            {
                s = s.next;
                f = f.next.next;
            }

            ListNode headb = s.next;
            s.next = null;
            headb = ReverseList(headb);

            while (head != null && headb != null)
            {
                ListNode t = head.next;
                ListNode tb = headb.next;

                headb.next = head.next;
                head.next = headb;

                head = t;
                headb = tb;
            }

            return;
        }

        public ListNode ReverseList(ListNode n)
        {
            if (n == null || n.next == null) return n;
            ListNode prev = n;
            ListNode next = n.next;
            prev.next = null;

            while (next != null)
            {
                ListNode nnext = next.next;
                next.next = prev;
                prev = next;
                next = nnext;
            }

            return prev;
        }

        public bool IsValidPalindrome(string s, int k)
        {
            char[] arr = s.ToCharArray();
            IEnumerable<char> e = arr.Reverse();
            string revs = new string(e.ToArray());

            int lcsLen = GetLcs(s, revs);
            return s.Length - lcsLen <= k;
        }

        public int GetLcs(string a, string b)
        {
            int m = a.Length, n = b.Length;
            int[][] dp = new int[m+1][];
            for (int i = 0; i < m + 1; i++)
            {
                dp[i] = new int[n+1];
            }

            for (int i = 0; i <= m; i++)
            {
                for (int j = 0; j <= n; j++)
                {
                    if (i == 0 || j == 0) dp[i][j] = 0;
                    else if (a[i - 1] == b[i - 1]) dp[i][j] = dp[i - 1][j - 1] + 1;
                    else dp[i][j] = Math.Max(dp[i - 1][j], dp[i][j - 1]);
                }
            }

            return dp[m][n];
        }

        public bool IsMonotonic(int[] A)
        {
            if (A == null || A.Length < 2) return true;

            int i = 1;
            while (i < A.Length-1 && A[i] == A[i - 1]) i++;
            bool isInc = A[i] > A[i-1];
            for (; i < A.Length; i++)
            {
                if (isInc && A[i] < A[i - 1]) return false;
                if (!isInc && A[i] > A[i - 1]) return false;
            }

            return true;
        }

        public int ClosestValue(TreeNode root, double target)
        {
            if (root == null) return 0;
            int res = root.val;
            ClosestValue(root, target, null, ref res);
            return res;
        }

        public void ClosestValue(TreeNode root, double target, TreeNode prev, ref int res)
        {
            if (root == null) return;
            if (target.Equals(root.val))
            {
                res = root.val;
                return;
            }

            if (target < root.val) ClosestValue(root.left, target, root, ref res);
            else ClosestValue(root.right, target, root, ref res);

            if (Math.Abs(target - root.val) < Math.Abs(target - res)) res = root.val;
        }

        public int LongestArithSeqLength(int[] A)
        {
            List<Dictionary<int, int>> map = new List<Dictionary<int, int>>();

            int res = 2;
            for (int i = 0; i < A.Length; i++)
            {
                map.Add(new Dictionary<int, int>());
                for (int j = 0; j < i; j++)
                {
                    int d = A[i] - A[j];
                    int val = map[j].GetValueOrDefault(d, 1) + 1;
                    map[i][d] = val;
                    res = Math.Max(res, val);
                }
            }

            return res;
        }

        public IList<IList<string>> FindLadders(string beginWord, string endWord, IList<string> wordList)
        {
            HashSet<string> dict = new HashSet<string>(wordList);
            dict.Add(beginWord);
            Dictionary<string, int> dist = new Dictionary<string, int>();
            Dictionary<string, List<string>> neighbours = new Dictionary<string, List<string>>();
            IList<IList<string>> res = new List<IList<string>>();

            GetWordLadder(beginWord, endWord, dict, ref dist, ref neighbours);
            GetOtherWordLadders(beginWord, endWord, neighbours, dist, ref res, new List<string>());
            return res;
        }

        private void GetOtherWordLadders(string cur, string end, Dictionary<string, List<string>> n,
            Dictionary<string, int> dist, ref IList<IList<string>> res, List<string> curList)
        {
            curList.Add(cur);
            if (cur.Equals(end)) res.Add(new List<string>(curList));

            foreach (string s in n[cur])
            {
                if (dist[cur] + 1 == dist[s])
                {
                    GetOtherWordLadders(s, end, n, dist, ref res, curList);
                }
            }
        }

        private void GetWordLadder(string b, string e, HashSet<string> dict, ref Dictionary<string, int> dist,
            ref Dictionary<string, List<string>> neighbours)
        {
            Queue<string> q = new Queue<string>();
            q.Enqueue(b);
            dist[b] = 0;
            bool isFound = false;

            while (q.Any())
            {
                int c = q.Count;
                for (int i = 0; i < c; i++)
                {
                    string s = q.Dequeue();
                    List<string> neigh = GetOneDist(s, dict);
                    neighbours[s] = neigh;
                    int curDist = dist[s];
                    foreach (string n in neigh)
                    {
                        if (n.Equals(e)) isFound = true;
                        if (!dist.ContainsKey(n))
                        {
                            dist[n] = curDist + 1;
                            q.Enqueue(n);
                        }
                    }
                }

                if (isFound) break;
            }
        }

        private List<string> GetOneDist(string s, HashSet<string> dict)
        {
            List<string> res = new List<string>();
            char[] carr = s.ToCharArray();

            for (int i = 0; i < s.Length; i++)
            {
                for (char c = 'a'; c <= 'z'; c++)
                {
                    if (carr[i] == c) continue;

                    carr[i] = c;
                    string nstr = new string(carr);
                    if (dict.Contains(nstr)) res.Add(nstr);
                }
            }

            return res;
        }

        public int[][] Multiply(int[][] A, int[][] B)
        {
            if (A == null || B == null) return null;
            if (A.Length == 0) return B;
            if (B.Length == 0) return A;

            int ma = A.Length, na = A[0].Length;
            int mb = B.Length, nb = B[0].Length;
            int[][] res = new int[ma][];

            for (int i = 0; i < ma; i++)
            {
                res[i] = new int[nb];
                for (int j = 0; j < nb; j++)
                {
                    int r = 0;
                    for (int k = 0; k < mb; k++)
                    {
                        r += A[i][k] * B[k][j];
                    }

                    res[i][j] = r;
                }
            }

            return res;
        }

        public GNode CloneGraph(GNode node)
        {
            if (node == null) return null;
            GNode r = new GNode(node.val, new List<GNode>());
            Dictionary<GNode, GNode> v = new Dictionary<GNode, GNode>();
            v[node] = r;
            Queue<GNode> q = new Queue<GNode>();
            q.Enqueue(node);

            while (q.Any())
            {
                GNode c = q.Dequeue();
                foreach (GNode n in c.neighbors)
                {
                    if (!v.ContainsKey(n))
                    {
                        v[n] = new GNode(n.val, new List<GNode>());
                        q.Enqueue(n);
                    }

                    v[c].neighbors.Add(v[n]);
                }
            }

            return r;
        }

        public TreeNode TreeToDoublyList(TreeNode root)
        {
            if (root == null) return null;

            TreeNode h = null, t = null;
            Stack<TreeNode> s = new Stack<TreeNode>();
            Populate(s, root);

            while (s.Any())
            {
                TreeNode c = s.Pop();
                Populate(s, c.right);
                if (h == null)
                {
                    h = c;
                    t = c;
                }
                else
                {
                    t.right = c;
                    c.left = t;
                    t = t.right;
                }
            }

            h.left = t;
            t.right = h;
            return h;
        }

        private void Populate(Stack<TreeNode> s, TreeNode n)
        {
            while (n != null)
            {
                s.Push(n);
                n = n.left;
            }
        }

        public IList<string> TopKFrequent(string[] words, int k)
        {
            Dictionary<string, int> map = new Dictionary<string, int>();
            foreach (string w in words) map[w] = map.GetValueOrDefault(w, 0) + 1;
            map = new Dictionary<string, int>(map.OrderByDescending(pair => pair.Value));
            
            Dictionary<int, List<string>> f = new Dictionary<int, List<string>>();
            foreach (var p in map)
            {
                if (!f.ContainsKey(p.Value)) f[p.Value] = new List<string>();
                f[p.Value].Add(p.Key);
            }

            IList<string> res = new List<string>();
            foreach (var p in f)
            {
                p.Value.Sort();
                foreach (string s in p.Value)
                {
                    res.Add(s);
                    k -= 1;
                    if (k == 0) break;
                }

                if(k == 0) break;
            }

            return res;
        }

        public bool IsToeplitzMatrix(int[][] matrix)
        {
            if (matrix == null || matrix.Length == 0) return false;

            int m = matrix.Length, n = matrix[0].Length;
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i+1 < m && j+1 < n && matrix[i][j] != matrix[i + 1][j + 1]) return false;
                }
            }

            return true;
        }

        public int MinDepth(TreeNode root)
        {
            if (root == null) return 0;
            if (root.left == null && root.right == null) return 1;

            int min = int.MaxValue;
            if (root.left != null) min = Math.Min(min, MinDepth(root.left)); 
            if (root.right != null) min = Math.Min(min, MinDepth(root.right));
            return min + 1;
        }

        public IList<string> BinaryTreePaths(TreeNode root)
        {
            List<string> res = new List<string>();
            BinaryTreePaths(root, "", ref res);
            return res;
        }

        public void BinaryTreePaths(TreeNode root, string s, ref List<string> res)
        {
            if (root == null) return;
            if (root.left == null && root.right == null)
            {
                res.Add(s + root.val);
                return;
            }

            BinaryTreePaths(root.left, s + root.val + "->", ref res);
            BinaryTreePaths(root.right, s + root.val + "->", ref res);
        }

        public string MultiplyString(string num1, string num2)
        {
            int n1 = num1.Length;
            int n2 = num2.Length;

            if (n1 == 0 || n2 == 0) return "0";
            int[] res = new int[n1+n2];
            Array.Fill(res, 0);
            int i1 = 0, i2 = 0;
            for (int i = n1-1; i >=0; i--)
            {
                int a = num1[i] - '0';
                int c = 0;
                i2 = 0;
                for (int j = n2-1; j >= 0; j--)
                {
                    int b = num2[j] - '0';
                    int sum = (a * b) + c + res[i1 + i2];
                    c = sum / 10;
                    res[i1 + i2] = sum % 10;

                    i2++;
                }

                if (c > 0)
                {
                    res[i1 + i2] = c;
                }
                i1++;
            }

            string s = "";
            int pos = res.Length-1;
            while (pos >= 0 && res[pos] == 0) pos--;
            if (pos == -1) return "0";
            for (; pos >= 0; pos--) s = s + res[pos];

            return s;
        }

        public int LeastInterval(char[] tasks, int n)
        {
            int[] count = new int[26];
            Array.Fill(count, 0);
            foreach (char c in tasks) count[c - 'A']++;

            Array.Sort(count);
            int time = 0;
            while (count[25] > 0)
            {
                int i = 0;
                while (i <= n)
                {
                    if (count[25] == 0) break;
                    if (i < 26 && count[25 - i] > 0) count[25 - i]--;
                    i++;
                    time++;
                }

                Array.Sort(count);
            }

            return time;
        }

        public int Calculate(string s)
        {
            char sign = '+';
            Stack<int> stk = new Stack<int>();
            int num = 0;
            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];
                if (char.IsNumber(c)) num = num * 10 + (c - '0');
                if (i == s.Length - 1 || (c != ' ' && !char.IsNumber(c)))
                {
                    switch (sign)
                    {
                        case '+':
                            stk.Push(num);
                            break;
                        case '-':
                            stk.Push(-num);
                            break;
                        case '*':
                            stk.Push(stk.Pop() * num);
                            break;
                        case '/':
                            stk.Push(stk.Pop() / num);
                            break;
                        default:
                            throw new InvalidOperationException();
                    }

                    sign = c;
                    num = 0;
                }
            }

            int res = 0;
            while (stk.Any()) res += stk.Pop();
            return res;
        }

        public bool HasPath(int[][] maze, int[] start, int[] destination)
        {
            bool[][] v = new bool[maze.Length][];
            for (int i = 0; i < maze.Length; i++)
            {
                v[i] = new bool[maze[i].Length];
                Array.Fill(v[i], false);
            }

            int[][] dir = new int[4][];
            dir[0] = new[] {-1, 0};
            dir[1] = new[] {1, 0};
            dir[2] = new[] {0, -1};
            dir[3] = new[] {0, 1};
            Queue<int[]> q = new Queue<int[]>();
            q.Enqueue(start);
            v[start[0]][start[1]] = true;

            while (q.Any())
            {
                int[] c = q.Dequeue();
                if (c[0] == destination[0] && c[1] == destination[1]) return true;
                for (int i = 0; i < 4; i++)
                {
                    int x = c[0] + dir[i][0];
                    int y = c[1] + dir[i][1];
                    while (x >= 0 && x < maze.Length && y >= 0 && y < maze[0].Length && maze[x][y] == 0)
                    {
                        x += dir[i][0];
                        y += dir[i][1];
                    }

                    x -= dir[i][0];
                    y -= dir[i][1];
                    if (!v[x][y])
                    {
                        q.Enqueue(new[] {x, y});
                        v[x][y] = true;
                    }
                }
            }

            return false;
        }

        public int FindCelebrity(int n)
        {
            int candidate = 0;
            for (int i = 1; i < n; i++)
            {
                if (Knows(candidate, i)) candidate = i;
            }

            for (int i = 0; i < n; i++)
            {
                if (i != candidate && (Knows(candidate, i) || !Knows(i, candidate))) return -1;
            }

            return candidate;
        }

        public int FindJudge(int N, int[][] trust)
        {
            int[] count = new int[N+1];
            foreach (int[] t in trust)
            {
                count[t[0]]--;
                count[t[1]]++;
            }
            for (int i = 1; i <= N; ++i)
            {
                if (count[i] == N - 1) return i;
            }
            return -1;
        }

        private bool Knows(int candidate, int p1)
        {
            return false;
        }

        public double MyPow(double x, int n)
        {
            if (n < 0)
            {
                x = 1 / x;
                n = -n;
            }

            return MyPowRecur(x, n);
        }
        public double MyPowRecur(double x, int n)
        {
            if (n == 0) return 1.0;
            if (n == 1) return x;

            double h = MyPowRecur(x, n / 2);
            if (n % 2 == 0) return h * h;
            else return h * h * x;
        }

        public bool CarPooling(int[][] trips, int capacity)
        {
            int[] stops = new int[1001];
            Array.Fill(stops, 0);
            foreach (int[] t in trips)
            {
                stops[t[1]] -= t[0];
                stops[t[2]] += t[0];
            }

            for (int i = 0; capacity >= 0 && i < 1001; i++)
            {
                capacity += stops[i];
            }

            return capacity >= 0;
        }

        public TreeNode LcaDeepestLeaves(TreeNode root)
        {
            KeyValuePair<TreeNode, int> res = LcaDeepestLeaves(root, 0);
            return res.Key;
        }

        public KeyValuePair<TreeNode, int> LcaDeepestLeaves(TreeNode root, int d)
        {
            if (root == null) return new KeyValuePair<TreeNode, int>(null, d);

            KeyValuePair<TreeNode, int> l = LcaDeepestLeaves(root.left, d + 1);
            KeyValuePair<TreeNode, int> r = LcaDeepestLeaves(root.right, d + 1);
            if (l.Value == r.Value) return new KeyValuePair<TreeNode, int>(root, l.Value);
            return l.Value > r.Value ? l : r;
        }

        public int NumDecodings(string s)
        {
            int[] dp = new int[s.Length+1];
            Array.Fill(dp, 0);
            dp[0] = 1;
            dp[1] = s[0] - '0' > 0 ? 1 : 0;
            for (int i = 2; i <= s.Length; i++)
            {
                int a = Convert.ToInt32(s.Substring(i - 1, 1));
                int b = Convert.ToInt32(s.Substring(i - 2, 2));
                if (a > 0 && a <= 9) dp[i] += dp[i - 1];
                if (b >= 10 && b <= 26) dp[i] += dp[i - 2];
            }

            return dp[s.Length];
        }

        public IList<int> PancakeSort(int[] A)
        {
            for (int i = 0; i < A.Length; i++)
            {
                int j = FindMin(A, i);
                if (j == i) continue;
                Flip(A, j, A.Length - 1);
                Flip(A, i, A.Length-1);
            }

            return A;
        }

        private void Flip(int[] A, int i, int j)
        {
            while (i <= j)
            {
                int temp = A[i];
                A[i] = A[j];
                A[j] = temp;
                i++;
                j--;
            }
        }

        private int FindMin(int[] A, int i)
        {
            int min = int.MaxValue;
            int pos = -1;
            for (int j = i; j < A.Length; j++)
            {
                if (A[j] < min)
                {
                    min = A[j];
                    pos = j;
                } 
            }

            return pos;
        }

        public class NumMatrix
        {
            private int[][] mat;
            private int m;
            private int n;

            public NumMatrix(int[][] matrix)
            {
                if (matrix == null || matrix.Length == 0)
                {
                    m = 0;
                    n = 0;
                    return;
                }
                m = matrix.Length;
                n = matrix[0].Length;
                mat = new int[m][];

                for (int i = 0; i < m; i++)
                {
                    mat[i] = new int[n+1];
                    int c = 0;
                    mat[i][0] = 0;
                    for (int j = 0; j < n; j++)
                    {
                        c = c + matrix[i][j];
                        mat[i][j + 1] = c;
                    }

                    Console.WriteLine(string.Join(',', mat[i]));
                }
            }

            public int SumRegion(int row1, int col1, int row2, int col2)
            {
                if (m == 0 || n == 0) return 0;
                int res = 0;
                for (int i = row1; i <= row2; i++)
                {
                    res += (mat[i][col2+1] - mat[i][col1]);
                }

                return res;
            }
        }

        public class RandomPickIndex
        {
            private Dictionary<int, List<int>> map;
            private Random rand;

            public RandomPickIndex(int[] nums)
            {
                rand = new Random();
                map = new Dictionary<int, List<int>>();
                for (int i = 0; i < nums.Length; i++)
                {
                    if (!map.ContainsKey(nums[i])) map[nums[i]] = new List<int>();
                    map[nums[i]].Add(i);
                }
            }

            public int Pick(int target)
            {
                List<int> l = map[target];
                int r = rand.Next(l.Count);
                return l[r];
            }
        }

        public class Read4
        {
            private char[] buff = new char[4];
            private int ptr = 0;
            private int count = 0;

            public int Read(char[] buf, int n)
            {
                int curptr = 0;
                while (curptr < n)
                {
                    if (ptr >= count)
                    {
                        //count = Read4(buff);
                        ptr = 0;
                    }

                    if (count == 0) break;
                    buf[curptr++] = buff[ptr++];
                }

                return curptr;
            }
        }

        public class Node
        {
            public int val;
            public Node next;

            public Node() { }

            public Node(int _val)
            {
                val = _val;
                next = null;
            }

            public Node(int _val, Node _next)
            {
                val = _val;
                next = _next;
            }
        }

        public class GNode
        {
            public int val;
            public IList<GNode> neighbors;

            public GNode()
            {
                val = 0;
                neighbors = new List<GNode>();
            }

            public GNode(int _val)
            {
                val = _val;
                neighbors = new List<GNode>();
            }

            public GNode(int _val, List<GNode> _neighbors)
            {
                val = _val;
                neighbors = _neighbors;
            }
        }

        public class RandomizedSet
        {
            private Dictionary<int, int> map;
            private List<int> list;
            private Random r;

            /** Initialize your data structure here. */
            public RandomizedSet()
            {
                map = new Dictionary<int, int>();
                list = new List<int>();
                r = new Random();
            }

            /** Inserts a value to the set. Returns true if the set did not already contain the specified element. */
            public bool Insert(int val)
            {
                if (map.ContainsKey(val)) return false;
                map[val] = list.Count;
                list.Add(val);
                return true;
            }

            /** Removes a value from the set. Returns true if the set contained the specified element. */
            public bool Remove(int val)
            {
                if (!map.ContainsKey(val)) return false;
                int pos = map[val];
                list[pos] = -1;
                map.Remove(val);
                return true;
            }

            /** Get a random element from the set. */
            public int GetRandom()
            {
                List<int> p = new List<int>(map.Values);
                int pos = r.Next(p.Count);
                return list[p[pos]];
            }
        }

        public class SnapshotArray
        {
            private List<Dictionary<int, int>> map;
            private Dictionary<int, int> diff;

            public SnapshotArray(int length)
            {
                map = new List<Dictionary<int, int>>(length);
                diff = new Dictionary<int, int>();
            }

            public void Set(int index, int val)
            {
                diff[index] = val;
            }

            public int Snap()
            {
                map.Add(new Dictionary<int, int>(diff));
                diff = new Dictionary<int, int>();
                return map.Count - 1;
            }

            public int Get(int index, int snap_id)
            {
                for (int i = snap_id; i >= 0; i--)
                {
                    if (map[i].ContainsKey(index)) return map[i][index];
                }

                return 0;
            }
        }
    }
}