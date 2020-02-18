using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonOA
{
    using System.Linq;

    public class GoogleQuestions
    {
        public class Node
        {
            public int val;
            public Node prev;
            public Node next;
            public Node child;
        }

        public void Run()
        {
            string[] words = new[] {"This", "is", "an", "example", "of", "text", "justification."};
            IList<string> res = FullJustify(words, 16);
            foreach (string s in res) Console.WriteLine($"\"{s}\"");
            Console.WriteLine("_______________________________________________________");

            int[] num = new[] {5, 2, 6, 1};
            IList<int> res1 = CountSmaller(num);
            Console.WriteLine($"[{string.Join(',', num)}] -> [{string.Join(',', res1)}]");
            Console.WriteLine("_______________________________________________________");


            string sec = "1807", guess = "7810";
            Console.WriteLine($"GetHint: {GetHint(sec, guess)}");
            Console.WriteLine("_______________________________________________________");

            string S = "ab##", T = "c#d#";
            Console.WriteLine($"Are backspace same: {BackspaceCompare(S, T)}");
            Console.WriteLine("_______________________________________________________");
        }

        public int MinDominoRotations(int[] A, int[] B)
        {
            if (A == null || B == null) return -1;
            
            int n = A.Length, m = B.Length;
            if (n == 0 || m == 0) return -1;

            Dictionary<int, int> map = new Dictionary<int, int>();
            foreach (int i in A) map[i] = map.GetValueOrDefault(i, 0) + 1;
            foreach (int i in B) map[i] = map.GetValueOrDefault(i, 0) + 1;

            int target = -1;
            foreach (int key in map.Keys)
            {
                if (map[key] >= (m + n) / 2) target = key;
            }

            int aTargetCount = 0, aRotCount = 0;
            int bTargetCount = 0, bRotCount = 0;
            for (int i = 0; i < n; i++)
            {
                if (A[i] == target)
                {
                    aTargetCount++;
                    if (B[i] != target) bRotCount++;
                }
                if (B[i] == target)
                {
                    bTargetCount++;
                    if (A[i] != target) aRotCount++;
                }
            }

            if ((aTargetCount + aRotCount != n) || (bTargetCount + bRotCount != m)) return -1;
            else return Math.Min(aRotCount, bRotCount);
        }

        public static string ReorganizeString(string S)
        {
            Dictionary<char, int> count = new Dictionary<char, int>();
            foreach (char c in S)
            {
                int v = count.GetValueOrDefault(c, 0);
                count[c] = v + 1;
            }

            count = new Dictionary<char, int>(count.OrderBy(pair => pair.Value));

            char[] res = new char[S.Length];
            int t = 1;
            foreach (char c in count.Keys)
            {
                if (count[c] > (S.Length + 1) / 2) return string.Empty;

                int num = count[c];
                while (num > 0)
                {
                    if (t >= S.Length) t = 0;
                    res[t] = c;
                    t += 2;
                    num--;
                }
            }

            return new string(res);
        }

        public int LongestStrChain(string[] words)
        {
            int n = words.Length;
            if (n < 2) return n;

            words = words.OrderBy(s => s.Length).ToArray();
            Dictionary<string, int> map = new Dictionary<string, int>();

            int res = 0;
            foreach (string s in words)
            {
                int maxFreq = 0;
                for (int i = 0; i < s.Length; i++)
                {
                    string temp = s.Substring(0, i) + s.Substring(i + 1);
                    int f = map.GetValueOrDefault(temp, 0) + 1;
                    maxFreq = Math.Max(maxFreq, f);
                }

                map[s] = maxFreq;
                res = Math.Max(res, maxFreq);
            }

            Console.WriteLine(string.Join(Environment.NewLine, map));
            return res;
        }

        public IList<string> FullJustify(string[] words, int maxWidth)
        {
            List<string> res = new List<string>();
            if (words.Length < 1) return res;
            int left = 0, right = left;

            while (left < words.Length)
            {
                string temp = string.Empty;
                right = PackWords(words, left, maxWidth);

                if (right == left)
                {
                    temp = PadSpace(words[left], maxWidth);
                }
                else
                {
                    int numSpaces = right - left;
                    int curLen = 0;
                    for (int i = left; i <= right; i++) curLen += words[i].Length;

                    bool isLastLine = right == words.Length - 1;
                    int adnSpace = maxWidth - curLen;
                    string spaceStr = isLastLine ? " " : GenSpace(adnSpace / numSpaces);
                    int extraSpace = isLastLine ? 0 : adnSpace % numSpaces;

                    for (int i = left; i <= right; i++)
                    {
                        temp += words[i] + spaceStr + (extraSpace-- > 0 ? " " : "");
                    }
                }

                res.Add(PadSpace(temp.TrimEnd(), maxWidth));
                left = right + 1;
            }

            return res;
        }

        private int PackWords(string[] words, int left, int maxWidth)
        {
            int i = left;
            int curLen = words[left].Length + 1;
            while (curLen < maxWidth && i < words.Length)
            {
                i++;
                if (i < words.Length) curLen = curLen + words[i].Length + 1;
            }

            return i - 1;
        }

        private string PadSpace(string s, int maxWidth)
        {
            string genSpace = GenSpace(maxWidth - s.Length);
            return s + genSpace;
        }

        private string GenSpace(int n)
        {
            string res = string.Empty;
            for (int i = 0; i < n; i++) res = res + " ";
            return res;
        }

        public Node Flatten(Node head)
        {
            if (head == null) return head;

            Node cur = head;
            while (cur != null)
            {
                Node nextNode = cur.next;
                if (cur.child != null)
                {
                    Node fhead = Flatten(cur.child);
                    Node ftail = fhead;
                    while (ftail.next != null) ftail = ftail.next;

                    fhead.prev = cur;
                    ftail.next = cur.next;

                    if (cur.next != null) cur.next.prev = ftail;
                    cur.next = fhead;
                    cur.child = null;
                }

                cur = nextNode;
            }

            return head;
        }

        public IList<int> CountSmaller(int[] nums)
        {
            int n = nums.Length;
            if (n < 1) return new List<int>();
            if (n == 1) return new List<int>{0};

            List<KeyValuePair<int, int>> pos = new List<KeyValuePair<int, int>>();
            List<int> res = new List<int>();
            for (int i = 0; i < n; i++)
            {
                pos.Add(new KeyValuePair<int, int>(nums[i], i));
                res.Add(0);
            }

            MergeSortPair(pos, ref res);
            return res;
        }

        private List<KeyValuePair<int, int>> MergeSortPair(List<KeyValuePair<int, int>> pos, ref List<int> res)
        {
            if (pos.Count <= 1) return pos;

            int mid = pos.Count / 2;
            
            List<KeyValuePair<int, int>> a = new List<KeyValuePair<int, int>>();
            for (int k = 0; k < mid; k++) a.Add(pos[k]);
            a = MergeSortPair(a, ref res);

            List<KeyValuePair<int, int>> b = new List<KeyValuePair<int, int>>();
            for (int k = mid; k < pos.Count; k++) b.Add(pos[k]);
            b = MergeSortPair(b, ref res);

            int i = 0, j = 0;
            int ind = 0;
            while (i < a.Count || j < b.Count)
            {
                if (j == b.Count || i < a.Count && a[i].Key <= b[j].Key)
                {
                    res[a[i].Value] += j;
                    pos[ind] = a[i];
                    i++;
                    ind++;
                }
                else
                {
                    pos[ind] = b[j];
                    j++;
                    ind++;
                }
            }

            return pos;
        }

        public int[] AssignBikes(int[][] workers, int[][] bikes)
        {
            List<int[]> l = new List<int[]>();
            for (int i = 0; i < workers.Length; i++)
            {
                for (int j = 0; j < bikes.Length; j++)
                {
                    int d = Math.Abs(workers[i][0] - bikes[j][0]) + Math.Abs(workers[i][1] - bikes[j][1]);
                    l.Add(new[] {d, i, j});
                }
            }

            l.Sort((a, b) =>
            {
                if (a[0] == b[0])
                {
                    if (a[1] == b[1])
                    {
                        return a[2].CompareTo(b[2]);
                    }
                    else return a[1].CompareTo(b[1]);
                }
                else return a[0].CompareTo(b[0]);
            });

            HashSet<int> assignedBikes = new HashSet<int>();
            int[] res = new int[workers.Length];
            Array.Fill(res, -1);

            foreach (int[] i in l)
            {
                int worker = i[1];
                int bike = i[2];
                if (res[worker] == -1 && !assignedBikes.Contains(bike))
                {
                    res[worker] = bike;
                    assignedBikes.Add(bike);
                }
            }

            return res;
        }

        public int SplitArray(int[] nums, int m)
        {
            if (nums == null || nums.Length < 1) return 0;

            int min = 1, max = 0;
            foreach (int i in nums)
            {
                if (i == int.MaxValue) return int.MaxValue;
                max += i;
            }

            int res = 0;
            while (min <= max)
            {
                int mid = (min + max) / 2;
                if (IsPossible(nums, m, mid))
                {
                    res = mid;
                    max = mid - 1;
                }
                else min = mid + 1;
            }

            return res;
        }

        private bool IsPossible(int[] nums, int m, int mid)
        {
            int count = 1;
            int sum = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                sum += nums[i];
                if (sum > mid)
                {
                    count++;
                    sum = nums[i];
                }
            }

            if (count <= m) return true;
            else return false;
        }

        public int MinTransfers(int[][] transactions)
        {
            Dictionary<int, int> exp = new Dictionary<int, int>();
            foreach (int[] t in transactions)
            {
                int lender = t[0];
                int receiver = t[1];
                exp[lender] = exp.GetValueOrDefault(lender, 0) - t[2];
                exp[receiver] = exp.GetValueOrDefault(receiver, 0) + t[2];
            }

            return Settle(exp.Values.ToList(), 0);
        }

        private int Settle(List<int> exp, int st)
        {
            while (st < exp.Count && exp[st] == 0) st++;
            if (st == exp.Count) return 0;

            int count = int.MaxValue;
            for (int i = st+1; i < exp.Count; i++)
            {
                if (exp[st] * exp[i] < 0)
                {
                    exp[i] = exp[i] + exp[st];
                    count = Math.Min(count, 1 + Settle(exp, st+1));
                    exp[i] = exp[i] - exp[st];
                }
            }

            return count;
        }

        public int CountNodes(TreeNode root)
        {
            int d = GetDepth(root);
            int last = (int) Math.Pow(2, d) - 1;
            int i = 0, j = last;
            int p = 0;
            while (i <= j)
            {
                p = (i + j) / 2;
                if (BinTreeBinSearch(root, p, d)) i = p + 1;
                else j = p - 1;
            }

            return last + i;
        }

        private bool BinTreeBinSearch(TreeNode root, int idx, int d)
        {
            int i = 0, j = (int) Math.Pow(2, d) - 1;
            int pivot;

            for (int l = 0; l < d; l++)
            {
                pivot = (i + j) / 2;
                if (idx <= pivot)
                {
                    root = root.left;
                    j = pivot;
                }
                else
                {
                    root = root.right;
                    i = pivot + 1;
                }
            }

            return root != null;
        }

        private int GetDepth(TreeNode root)
        {
            if (root == null) return 0;

            int d = 0;
            while (root.left != null)
            {
                root = root.left;
                d++;
            }

            return d;
        }

        public int ShortestWay(string source, string target)
        {
            List<int>[] pos = new List<int>[26];
            int i;
            for (i = 0; i < 26; i++) { pos[i] = new List<int>();}
            for (i = 0; i < source.Length; i++) pos[source[i] - 'a'].Add(i);

            i = 0;
            int j = 0;
            int count = 1;
            while (i < target.Length)
            {
                List<int> p = pos[target[i] - 'a'];
                if (p.Count == 0) return -1;
                int k = p.BinarySearch(j);
                if (k < 0) k = -k - 1;
                if (k == p.Count)
                {
                    count++;
                    j = 0;
                }
                else
                {
                    j = p[k] + 1;
                    i++;
                }
            }

            return count;
        }

        public int MinKnightMoves(int x, int y)
        {
            int MOD = Math.Abs(y) + 2;
            return dfs(Math.Abs(x), Math.Abs(y), new Dictionary<int, int>(), MOD);
        }

        public int dfs(int x, int y, Dictionary<int, int> map, int MOD)
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
                ans = Math.Min(dfs(Math.Abs(x - 1), Math.Abs(y - 2), map, MOD),
                          dfs(Math.Abs(x - 2), Math.Abs(y - 1), map, MOD)) + 1;
            }
            map[index] = ans;
            return ans;
        }

        public bool CanConvert(string str1, string str2)
        {
            if (str1.Equals(str2)) return true;
            Dictionary<char, char> d = new Dictionary<char, char>();
            for (int i = 0; i < str1.Length; i++)
            {
                if (!d.ContainsKey(str1[i]))
                {
                    d[str1[i]] = str2[i];
                }
                else
                {
                    char c = d[str1[i]];
                    if (c != str2[i]) return false;
                }
            }

            return true;
        }

        public int MinMalwareSpread(int[][] graph, int[] initial)
        {
            if (initial == null || initial.Length < 1) return 0;
            int N = graph.Length;
            int[] colors = new int[N];
            Array.Fill(colors, -1);
            int color = 0;
            for (int i = 0; i < N; i++)
            {
                if (colors[i] == -1)
                {
                    color++; 
                    ColorGraph(graph, colors, color, i);
                }
            }

            int[] colorSize = new int[color+1];
            for (int i = 0; i < colors.Length; i++) colorSize[colors[i]]++;

            int[] infectedColorCount = new int[color+1];
            foreach (int i1 in initial) infectedColorCount[colors[i1]]++;

            int maxGroup = int.MinValue;
            int res = -1;
            foreach (int i in initial)
            {
                int c = colors[i];
                if (infectedColorCount[c] == 1)
                {
                    if (colorSize[c] == maxGroup)
                    {
                        res = Math.Min(res, i);
                    }

                    else if (colorSize[c] > maxGroup)
                    {
                        maxGroup = colorSize[c];
                        res = i;
                    }
                }
            }

            return res == -1 ? initial.Min() : res;
        }

        private void ColorGraph(int[][] graph, int[] colors, int color, int node)
        {
            colors[node] = color;
            int[] neigh = graph[node];
            for (int i = 0; i < neigh.Length; i++)
            {
                if (graph[node][i] == 1 && colors[i] == -1) ColorGraph(graph, colors, color, i);
            }
        }

        public void FindSecretWord(string[] wordlist, Master master)
        {
            int N = wordlist.Length;
            int[][] mat = new int[N][];
            int[] row = new int[N];
            Array.Fill(row, -1);
            Array.Fill(mat, row);

            for (int i = 0; i < N; i++)
            {
                string a = wordlist[i];
                for (int j = i; j < N; j++)
                {
                    string b = wordlist[j];
                    int c = 0;
                    for (int k = 0; k < a.Length; k++)
                    {
                        if (a[k] == b[k]) c++;
                    }

                    mat[i][j] = c;
                    mat[j][i] = c;
                }
            }

            List<int> possibility = new List<int>();
            for (int i = 0; i < wordlist.Length; i++) possibility.Add(i);
            List<int> alreadyGuessed = new List<int>();

            while (possibility.Any())
            {
                int guess = GuessAString(mat, possibility, alreadyGuessed);
                int match = master.Guess(wordlist[guess]);
                alreadyGuessed.Add(guess);
                if (match == wordlist[0].Length) return;
                List<int> newPossibility = new List<int>();
                foreach (int i1 in possibility) if (mat[guess][i1] == match) newPossibility.Add(i1);
                possibility = new List<int>(newPossibility);
            }
        }

        private int GuessAString(int[][] mat, List<int> possibility, List<int> alreadyGuessed)
        {
            if (possibility.Count <= 2) return possibility[0];

            int ansGroup = -1, ans = -1;
            foreach (int p in possibility)
            {
                if (!alreadyGuessed.Contains(p))
                {
                    int[] diffFreq = new int[7];
                    int[] arr = mat[p];
                    for (int i = 0; i < arr.Length; i++)
                    {
                        if (i == p) continue;
                        diffFreq[arr[i]]++;
                    }

                    int max = diffFreq.Max();
                    if (max > ansGroup)
                    {
                        ansGroup = max;
                        ans = p;
                    }
                }
            }

            return ans;
        }

        public string MinWindow(string S, string T)
        {
            int N = S.Length;
            int[][] dp = new int[N][];

            for (int i = N-1; i >= 0; i--)
            {
                dp[i] = new int[26];
                Array.Fill(dp[i], -1);
                for (int j = N-1; j >= i; j--)
                {
                    int p = S[j] - 'a';
                    dp[i][p] = j;
                }
            }

            List<int[]> windows = new List<int[]>();
            for (int i = 0; i < N; i++)
            {
                if (S[i] == T[0]) windows.Add(new []{i, i});
            }

            for (int i = 1; i < T.Length; i++)
            {
                int pos = T[i] - 'a';
                foreach (int[] w in windows)
                {
                    if (w[1] < N - 1 && dp[w[1]+1][pos] >= 0) w[1] = dp[w[1]+1][pos];
                    else
                    {
                        w[0] = w[1] = -1;
                        break;
                    }
                }
            }

            int[] res = new[] {-1, N};
            foreach (int[] w in windows)
            {
                if (w[0] == -1) break;
                else
                {
                    if (w[1] - w[0] < res[1] - res[0]) res = w;
                }
            }

            return res[0] == -1 ? "" : S.Substring(res[0], res[1] - res[0] + 1);
        }

        public int MinAreaRect(int[][] points)
        {
            Dictionary<int, List<int>> map = new Dictionary<int, List<int>>();
            foreach (int[] p in points)
            {
                List<int> l = map.GetValueOrDefault(p[0], new List<int>());
                l.Add(p[1]);
                map[p[0]] = l;
            }

            int area = int.MaxValue;
            Dictionary<int, int> prevX = new Dictionary<int, int>();
            foreach (int key in map.Keys)
            {
                List<int> yCoordinates = map[key];
                yCoordinates.Sort();
                for (int i = 0; i < yCoordinates.Count; i++)
                {
                    for (int j = i+1; j < yCoordinates.Count; j++)
                    {
                        int hash = yCoordinates[i] * 33 + yCoordinates[j];
                        if (prevX.ContainsKey(hash))
                        {
                            area = Math.Min(area, Math.Abs(key - prevX[hash]) * Math.Abs(yCoordinates[j] - yCoordinates[i]));
                        }

                        prevX[hash] = key;
                    }
                }
            }

            return area == int.MaxValue ? 0 : area;
        }

        public int MaxSumSubmatrix(int[][] matrix, int k)
        {
            if (matrix == null || matrix.Length == 0) return 0;

            int res = 0;
            for (int l = 0; l < matrix.Length; l++)
            {
                int[] sum = new int[matrix.Length];
                Array.Fill(sum, 0);
                for (int r = l; r < matrix.Length; r++)
                {
                    for (int i = 0; i < sum.Length; i++) sum[i] += matrix[i][r];
                }

                SortedList<int, int> set = new SortedList<int, int>();
                int curSum = 0;
                set.Add(0,0);
                foreach (int s in sum)
                {
                    curSum += s;
                    if (set.Any())
                    {
                        int i = 0;
                        while (i < set.Count && set.ElementAt(i).Key < curSum - k) i++;
                        if (i != set.Count) res = Math.Max(res, Math.Abs(curSum - set.ElementAt(i).Key));
                    }
                    set[curSum] = 0;
                }
            }

            return res;
        }

        public int[] NumSmallerByFrequency(string[] queries, string[] words)
        {
            List<int> qFreq = new List<int>();
            foreach (string q in queries)
            {
                qFreq.Add(SmallestLetterFreq(q));
            }

            List<int> wFreq = new List<int>();
            foreach (string w in words)
            {
                wFreq.Add(SmallestLetterFreq(w));
            }
            wFreq.Sort();

            List<int> res = new List<int>();
            for (int i = 0; i < qFreq.Count; i++)
            {
                int l = 0, r = wFreq.Count;
                while (l <= r)
                {
                    int mid = (l + r) / 2;
                    if (wFreq[mid] > qFreq[i]) r = mid - 1;
                    else l = mid + 1;
                }

                res.Add(wFreq.Count - l);
            }

            return res.ToArray();
        }

        private int SmallestLetterFreq(string s)
        {
            int[] f = new int[26];
            Array.Fill(f, 0);
            foreach (char c in s)
            {
                f[c - 'a']++;
            }

            for (int i = 0; i < 26; i++)
            {
                if (f[i] != 0) return f[i];
            }

            return 0;
        }

        public int MinSwap(int[] A, int[] B)
        {
            int[] swap = new int[A.Length];
            int[] no_swap = new int[A.Length];

            no_swap[0] = 0;
            swap[0] = 1;
            for (int i = 1; i < A.Length; i++)
            {
                swap[i] = no_swap[i] = A.Length;
                if (A[i-1] < A[i] && B[i-1] < B[i])
                {
                    no_swap[i] = no_swap[i - 1];
                    swap[i] = swap[i - 1] + 1;
                }

                if (A[i-1] < B[i] && B[i-1] < A[i])
                {
                    no_swap[i] = Math.Min(no_swap[i], swap[i - 1]);
                    swap[i] = Math.Min(swap[i], no_swap[i - 1] + 1);
                }
            }

            return Math.Min(swap[A.Length - 1], no_swap[A.Length - 1]);
        }

        public int ShipWithinDays(int[] weights, int D)
        {
            int minWt = int.MinValue;
            int maxWt = 0;
            foreach (int w in weights)
            {
                minWt = Math.Max(minWt, w);
                maxWt += w;
            }

            int l = minWt, r = maxWt;
            while (l < r)
            {
                int mid = (l + r) / 2;
                int t = CountShipment(weights, mid);
                if (t > D) l = mid + 1;
                else r = mid;
            }

            return l;
        }

        private int CountShipment(int[] weights, int cap)
        {
            int cur = 0;
            int count = 1;
            for (int i = 0; i < weights.Length; i++)
            {
                if (cur + weights[i] > cap)
                {
                    count++;
                    cur = 0;
                }

                cur += weights[i];
            }

            return count;
        }

        public void CleanRoom(Robot robot)
        {
            HashSet<string> v = new HashSet<string>();
            BacktrackRobotCleaner(v, robot, 0, 0, 0);
        }

        private void BacktrackRobotCleaner(HashSet<string> hashSet, Robot r, int x, int y, int dir)
        {
            int[,] dirs = new int[,] {{1, 0}, {0, 1}, {-1, 0}, {0, -1}};

            string s = $"{x}-{y}";
            if (hashSet.Contains(s)) return;

            hashSet.Add(s);
            r.Clean();
            for (int i = 0; i < 4; i++)
            {
                if (r.Move())
                {
                    int nx = x + dirs[dir,0];
                    int ny = y + dirs[dir,1];

                    BacktrackRobotCleaner(hashSet, r, nx, ny, dir);

                    r.TurnLeft();
                    r.TurnLeft();
                    r.Move();
                    r.TurnLeft();
                    r.TurnLeft();
                }

                r.TurnRight();
                dir = (dir + 1) % 4;
            }
        }

        public string GetHint(string secret, string guess)
        {
            int bullCount = 0;
            int[] cowS = new int[10];
            Array.Fill(cowS, 0);
            int[] cowG = new int[10];
            Array.Fill(cowG, 0);

            for (int i = 0; i < secret.Length; i++)
            {
                if (secret[i] == guess[i]) bullCount++;
                else
                {
                    int sPos = Convert.ToInt32(secret[i])-'0';
                    int gPos = Convert.ToInt32(guess[i])-'0';
                    cowS[sPos]++;
                    cowG[gPos]++;
                }
            }

            int cowCount = 0;
            for (int i = 0; i < 10; i++)
            {
                if (cowS[i] < cowG[i]) cowCount += cowS[i];
                else cowCount += cowG[i];
            }

            return $"{bullCount}A{cowCount}B";
        }

        public int LadderLength(string beginWord, string endWord, IList<string> wordList)
        {
            Dictionary<string, List<string>> adj = new Dictionary<string, List<string>>();
            Dictionary<string, List<string>> replaced = new Dictionary<string, List<string>>();

            wordList.Add(beginWord);
            foreach (string w in wordList)
            {
                List<string> str = GetOneDiffStr(w);
                replaced[w] = str;
                foreach (string s in str)
                {
                    if (!adj.ContainsKey(s)) adj[s] = new List<string>();
                    adj[s].Add(w);
                }
            }

            HashSet<string> v = new HashSet<string>();
            Queue<KeyValuePair<string, int>> q = new Queue<KeyValuePair<string, int>>();
            int count = 0;
            q.Enqueue(new KeyValuePair<string, int>(beginWord, 1));
            v.Add(beginWord);
            while (q.Any())
            {
                KeyValuePair<string, int> p = q.Dequeue();
                if (p.Key.Equals(endWord)) return p.Value;
                else
                {
                    List<string> neig = new List<string>();
                    List<string> oneDiff = replaced[p.Key];
                    foreach (string s in oneDiff)
                    {
                        neig.AddRange(adj[s]);
                    }
                    foreach (string s in neig)
                    {
                        if (!v.Contains(s)) q.Enqueue(new KeyValuePair<string, int>(s, p.Value + 1));
                    }
                }
            }

            return 0;
        }

        private List<string> GetOneDiffStr(string s)
        {
            char[] arr = s.ToCharArray();
            List<string> res = new List<string>();
            for (int i = 0; i < arr.Length; i++)
            {
                char t = arr[i];
                arr[i] = '*';
                res.Add(new string(arr));
                arr[i] = t;
            }

            return res;
        }

        public bool BackspaceCompare(string S, string T)
        {
            int n = S.Length, m = T.Length;
            int i = n - 1, j = m - 1;
            int sSkip = 0, tSkip = 0;

            while (i >= 0 || j >= 0)
            {
                if (i >= 0 && S[i] == '#') { sSkip++; i--; }
                else if (j >= 0 && T[j] == '#') { tSkip++; j--; }
                else if (sSkip > 0) { i--; sSkip--; }
                else if (tSkip > 0) { j--; tSkip--; }
                else if (i >= 0 && j >= 0 && S[i] == T[j]) { i--; j--; }
                else return false;
            }

            if (i >= 0 || j >= 0) return false;
            else return true;
        }

        public string LicenseKeyFormatting(string S, int K)
        {
            List<char> sb = new List<char>();
            int count = 0;
            for (int i = S.Length-1; i >= 0; i--)
            {
                if (S[i] != '-')
                {
                    if (count == K)
                    {
                        sb.Add('-');
                        count = 0;
                    }

                    sb.Add(char.ToUpperInvariant(S[i]));
                    count++;
                }
            }

            sb.Reverse();
            return new string(sb.ToArray());
        }

        public TreeNode ConstructFromPrePost(int[] pre, int[] post)
        {
            if (pre == null || pre.Length == 0) return null;

            TreeNode n = new TreeNode(pre[0]);
            if (pre.Length == 1) return n;

            int size = 0;
            for (int i = 0; i < post.Length; i++)
            {
                if (post[i] == pre[1])
                {
                    size = i + 1;
                    break;
                }
            }

            int[] preLeft = new int[size];
            Array.Copy(pre,1,preLeft,0,size);
            int[] postLeft = new int[size];
            Array.Copy(post, 0, postLeft, 0, size);
            n.left = ConstructFromPrePost(preLeft, postLeft);

            int[] preRight = new int[pre.Length-size-1];
            Array.Copy(pre, size+1, preRight, 0, pre.Length - size - 1);
            int[] postRight = new int[pre.Length-size-1];
            Array.Copy(post, size, postRight, 0, post.Length-size-1);
            n.right = ConstructFromPrePost(preRight, postRight);

            return n;
        }

        public int GetMaximumGold(int[][] grid)
        {
            int max = 0;
            int m = grid.Length;
            for (int i = 0; i < m; i++)
            {
                int n = grid[i].Length;
                for (int j = 0; j < n; j++)
                {
                    max = Math.Max(max, CollectGold(grid, i, j, m, n, 0));
                }
            }

            return max;
        }

        public int CollectGold(int[][] g, int i, int j, int m, int n, int sum)
        {
            if (i < 0 || i >= m || j < 0 || j >= n || g[i][j] == 0 || g[i][j] > 1000) return sum;

            int max = 0;
            sum += g[i][j];
            g[i][j] += 1000;

            max = Math.Max(max, CollectGold(g, i + 1, j, m, n, sum));
            max = Math.Max(max, CollectGold(g, i - 1, j, m, n, sum));
            max = Math.Max(max, CollectGold(g, i, j + 1, m, n, sum));
            max = Math.Max(max, CollectGold(g, i, j - 1, m, n, sum));

            g[i][j] -= 1000;
            return max;
        }

        public int ConfusingNumberII(int N)
        {
            Dictionary<int, int> map = new Dictionary<int, int>();
            map[0] = 0;
            map[1] = 1;
            map[6] = 9;
            map[8] = 8;
            map[9] = 6;

            int count = 0;
            Dictionary<int, int> v = new Dictionary<int, int>();
            for (int i = 1; i <= N; i++)
            {
                if (v.ContainsKey(i)) count++;
                else
                {
                    int rot = GetRotatedNum(map, i);
                    if (rot == -1) continue;
                    if (i != rot)
                    {
                        count++;
                        v[i] = rot;
                        v[rot] = i;
                    }
                }
            }

            return count;
        }

        public int GetRotatedNum(Dictionary<int, int> map, int n)
        {
            int res = 0;
            while (n > 0)
            {
                if (!map.ContainsKey(n % 10)) return -1;
                res = (res * 10) + map[n % 10];
                n = n / 10;
            }

            return res;
        }
    }

    public class TimeMap
    {
        private Dictionary<string, SortedDictionary<int, string>> map;

        /** Initialize your data structure here. */
        public TimeMap()
        {
            map = new Dictionary<string, SortedDictionary<int, string>>();
        }

        public void Set(string key, string value, int timestamp)
        {
            if (map.ContainsKey(key))
            {
                map[key].Add(timestamp, value);
            }
            else
            {
                SortedDictionary<int, string> t = new SortedDictionary<int, string>();
                t.Add(timestamp, value);
                map.Add(key, t);
            }
        }

        public string Get(string key, int timestamp)
        {
            SortedDictionary<int, string> t = map.GetValueOrDefault(key, null);
            if (t == null) return string.Empty;

            string res = t.GetValueOrDefault(timestamp, string.Empty);
            if (!string.IsNullOrEmpty(res)) return res;

            List<int> timestamps = t.Keys.ToList();
            if (!timestamps.Any()) return string.Empty;

            int maxTimestamp = GetTimeStamp(timestamps, timestamp);
            if (maxTimestamp == -1) return string.Empty;
            return map[key][maxTimestamp];
        }

        private int GetTimeStamp(List<int> list, int i)
        {
            if (i < list[0]) return -1;
            if (i >= list[list.Count - 1]) return list[list.Count - 1];

            int a = 0, b = list.Count - 1;
            while (a < b)
            {
                int mid = (a + b) / 2;
                if (list[mid] < i)
                {
                    if (list[mid + 1] > i) return list[mid];
                    a = mid + 1;
                }
                else
                {
                    b = mid - 1;
                }
            }

            return -1;
        }
    }

    public class AutocompleteSystem
    {
        class Node
        {
            public Dictionary<char, Node> map = new Dictionary<char, Node>();
            public Dictionary<string, int> list = new Dictionary<string, int>();
        }

        private Node root;

        private string searchStr;

        private Dictionary<string, int> localMap;

        public AutocompleteSystem(string[] sentences, int[] times)
        {
            root = new Node();
            searchStr = string.Empty;
            localMap = new Dictionary<string, int>();
            for (int i = 0; i < sentences.Length; i++)
            {
                localMap[sentences[i]] = times[i];
                Insert(sentences[i], times[i]);
            }
        }

        public IList<string> Input(char c)
        {
            if (c == '#')
            {
                localMap[searchStr] = localMap.GetValueOrDefault(searchStr, 0) + 1;
                Insert(searchStr, localMap[searchStr]);
                searchStr = string.Empty;
                return new List<string>();
            }
            searchStr = searchStr + c;
            Search(searchStr, out List<string> res);
            return res;
        }

        private void Insert(string s, int val)
        {
            Node cur = root;
            foreach (char c in s)
            {
                char cl = char.ToLower(c);
                if (!cur.map.ContainsKey(cl)) cur.map[cl] = new Node();
                cur.list[s] = val;
                cur = cur.map[cl];
            }

            cur.list[s] = val;
        }

        private bool Search(string prefix, out List<string> words)
        {
            words = new List<string>();
            Node node = root;
            foreach (char c in prefix)
            {
                char al = Char.ToLower(c);
                if (!node.map.ContainsKey(al))
                {
                    return false;
                }

                node = node.map[al];
            }

            List<KeyValuePair<string, int>> wordList = node.list.ToList();
            wordList.Sort((a, b) =>
            {
                if (a.Value == b.Value)
                {
                    int len = Math.Min(a.Key.Length, b.Key.Length);
                    for (int i = 0; i < len; i++)
                    {
                        if ((int) a.Key[i] < (int) b.Key[i]) return -1;
                        if ((int) a.Key[i] > (int) b.Key[i]) return 1;
                    }

                    if (a.Key.Length > b.Key.Length) return 1;
                    else return -1;
                }
                else
                {
                    if (a.Value > b.Value) return -1;
                    else return 1;
                }
            });

            if (wordList.Count >= 3)
            {
                words = wordList.Take(3).Select(i => i.Key).ToList();
            }
            else
            {
                words = wordList.Select(i => i.Key).ToList();
            }

            return true;
        }
    }

    public abstract class Master
    {
        public abstract int Guess(string word);
    }

    public class Logger
    {
        private Dictionary<string, int> map;

        /** Initialize your data structure here. */
        public Logger()
        {
            map = new Dictionary<string, int>();
        }

        /** Returns true if the message should be printed in the given timestamp, otherwise returns false.
            If this method returns false, the message will not be printed.
            The timestamp is in seconds granularity. */
        public bool ShouldPrintMessage(int timestamp, string message)
        {
            if (map.ContainsKey(message))
            {
                int prevTime = map[message];
                if (timestamp - prevTime > 9)
                {
                    map[message] = timestamp;
                    return true;
                }

                return false;
            }
            else
            {
                map[message] = timestamp;
                return true;
            }
        }
    }

    public class Codec
    {
        private string IntToString(string s)
        {
            int x = s.Length;
            char[] arr = new char[4];
            Array.Fill(arr, '0');
            for (int i = 3; i >= 0; i--)
            {
                char c = Convert.ToChar(x % 10);
                x = x / 10;
                arr[i] = c;
            }

            return new string(arr);
        }

        private int StringToInt(string s)
        {

            int r = 0;
            for (int i = 0; i < s.Length; i++)
            {
                r = (r * 10) + Convert.ToInt32(s[i]);
            }

            return r;
        }

        // Encodes a list of strings to a single string.
        public string encode(IList<string> strs)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string s in strs)
            {
                sb.Append(IntToString(s));
                sb.Append(s);
            }

            return sb.ToString();
        }

        // Decodes a single string to a list of strings.
        public IList<string> decode(string s)
        {
            List<string> res = new List<string>();
            for (int i = 0; i < s.Length;)
            {
                string sub = s.Substring(i, 4);
                int size = StringToInt(sub);
                i = i + 4;
                sub = s.Substring(i, size);
                res.Add(sub);
                i = i + size;
            }

            return res;
        }
    }

    public interface Robot
    {
        // Returns true if the cell in front is open and robot moves into the cell.
        // Returns false if the cell in front is blocked and robot stays in the current cell.
        bool Move();
   
        // Robot will stay in the same cell after calling turnLeft/turnRight.
        // Each turn will be 90 degrees.
        void TurnLeft();
        void TurnRight();
   
        // Clean the current cell.
        void Clean();
    }

    public class RandomPick
    {
        private int[] contSum;
        private int total;
        private Random r;

        public RandomPick(int[] w)
        {
            r = new Random();
            contSum = new int[w.Length];
            contSum[0] = w[0];
            total = w[0];
            for (int i = 1; i < w.Length; i++)
            {
                contSum[i] = contSum[i - 1] + w[i];
                total += w[i];
            }
        }

        public int PickIndex()
        {
            int t = r.Next(total);
            int i = 0, j = contSum.Length-1;
            while (i < j)
            {
                int mid = (i + j) / 2;
                if (contSum[mid] <= t) i = mid + 1;
                else j = mid;
            }

            return i;
        }
    }
}
