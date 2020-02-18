using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonOA
{
    using System.Linq;

    class Solution
    {
        public int ConnectSticks(int[] sticks)
        {
            MinHeap m = new MinHeap(sticks.ToList());
            int count = 0;
            while (m.count > 1)
            {
                int a = m.Pop();
                int b = m.Pop();
                int t = a + b;
                count += t;
                m.Insert(t);
            }

            return m.Pop();
        }
    }
    
    class TopNCompetitors
    {
        public static List<string> GetTopCompetitors(int numCompetitors, int topNCompetitors, List<string> competitors, List<string> reviews)
        {
            Dictionary<string, int> competitorBuzzCount = new Dictionary<string, int>(StringComparer.InvariantCultureIgnoreCase);
            foreach (string competitor in competitors)
            {
                competitorBuzzCount[competitor] = 0;
            }

            foreach (string review in reviews)
            {
                string company = GetCompanyFromReview(review, competitors);
                if (!string.IsNullOrEmpty(company) && competitorBuzzCount.ContainsKey(company))
                {
                    int curVal = competitorBuzzCount[company];
                    competitorBuzzCount[company] = curVal + 1;
                }
            }

            var orderedSet = competitorBuzzCount.OrderByDescending(pair => pair.Value);
            return orderedSet.Take(topNCompetitors).Select(pair => pair.Key).ToList();
        }

        private static string GetCompanyFromReview(string review, List<string> companies)
        {
            foreach (string word in review.Trim().Split(' '))
            {
                if (companies.Any() && companies.Contains(word, StringComparer.InvariantCultureIgnoreCase))
                {
                    return word;
                }
            }

            return string.Empty;
        }
    }

    class OrangesRotting
    {
        public static int ProcessOrangeRotting(int[][] grid)
        {
            if (grid == null)
            {
                return 0;
            }

            int R = grid.Length;
            int C = grid[0].Length;

            if (R == 0 && C == 0)
            {
                return 0;
            }

            if (R == 1 && C == 1)
            {
                return grid[0][0] != 1 ? 0 : -1;
            }

            int turnCount = 0;
            while (CanRot(grid))
            {
                int newlyRotten = ConvertToRotten(grid);
                if (newlyRotten == 0)
                {
                    // Choose to rot.
                    turnCount = -1;
                    break;
                }

                turnCount += 1;
                Console.WriteLine($"turn count {turnCount}");
                for (int i = 0; i < grid.Length; i++)
                {
                    for (int j = 0; j < grid[i].Length; j++)
                    {
                        Console.Write(grid[i][j]);
                    }
                    Console.WriteLine();
                }
            }

            return turnCount;
        }

        private static bool CanRot(int[][] grid)
        {
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    if (grid[i][j] == 1)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static int ConvertToRotten(int[][] grid)
        {
            int convertCount = 0;
            List<int> rows = new List<int>();
            List<int> cols = new List<int>();

            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    if (grid[i][j] == 2)
                    {
                        rows.Add(i);
                        cols.Add(j);
                    }
                }
            }

            for (int i = 0; i < rows.Count; i++)
            {
                int r = rows[i];
                int c = cols[i];

                if (IsValid(grid, r - 1, c))
                {
                    convertCount += 1;
                    grid[r - 1][c] = 2;
                }

                if (IsValid(grid, r + 1, c))
                {
                    convertCount += 1;
                    grid[r + 1][c] = 2;
                }

                if (IsValid(grid, r, c - 1))
                {
                    convertCount += 1;
                    grid[r][c - 1] = 2;
                }

                if (IsValid(grid, r, c + 1))
                {
                    convertCount += 1;
                    grid[r][c + 1] = 2;
                }
            }

            return convertCount;

        }

        private static bool IsValid(int[][] grid, int i, int j)
        {
            if (i < 0 || j < 0 || i >= grid.Length || j >= grid[i].Length)
            {
                return false;
            }

            return grid[i][j] == 1;
        }
    }

    class ReorderLog
    {
        public static string[] ReorderLogFiles(string[] logs)
        {
            List<string> letterLogs = new List<string>();
            List<string> digitLogs = new List<string>();

            foreach (string log in logs)
            {
                if (!string.IsNullOrEmpty(log))
                {
                    string[] parts = log.Split(' ');
                    if (parts.Length > 1 && parts[1][0] < 'a')
                    {
                        digitLogs.Add(log);
                    }
                    else if (parts.Length > 1 && parts[1][0] >= 'a' && parts[1][0] >= 'Z')
                    {
                        letterLogs.Add(log);
                    }
                }
            }

            letterLogs.Sort((a, b) =>
            {
                string c = a.Substring(a.IndexOf(' '));
                string d = b.Substring(b.IndexOf(' '));

                string ci = a.Split(' ')[0];
                string di = b.Split(' ')[0];

                for (int i = 0; i < Math.Min(c.Length, d.Length); i++)
                {
                    if (c[i] != d[i])
                    {
                        return c[i].CompareTo(d[i]);
                    }
                }

                for (int i = 0; i < Math.Min(ci.Length, di.Length); i++)
                {
                    if (ci[i] != di[i])
                    {
                        return ci[i].CompareTo(di[i]);
                    }
                }

                return 0;
            });

            string[] result = new string[logs.Length];
            int j = 0;
            foreach (string s in letterLogs)
            {
                result[j] = s;
                j += 1;
            }

            foreach (string s in digitLogs)
            {
                result[j] = s;
                j += 1;
            }

            return result;
        }
    }

    class CriticalConnection
    {
        public static IList<IList<int>> CriticalConnections(int n, IList<IList<int>> connections)
        {
            Graph g = new Graph(n, connections);
            g.Show();
            IList<IList<int>> result = new List<IList<int>>();

            foreach (var ints in connections)
            {
                var connection = (List<int>) ints;
                g.RemoveConnection(connection[0], connection[1]);

                int components = g.GetConnectedComponents();
                Console.WriteLine($"components = {components}");
                if (components > 1)
                {
                    result.Add(connection);
                }

                g.AddConnection(connection[0], connection[1]);
            }

            return result;
        }
    }

    class OptimalUtilization
    {
        public static List<KeyValuePair<int, int>> GetOptimization(List<KeyValuePair<int, int>> a,
            List<KeyValuePair<int, int>> b, int target)
        {
            a.Sort((x, y) => { return x.Value.CompareTo(y.Value); });
            b.Sort((x, y) => { return x.Value.CompareTo(y.Value); });

            int diff = int.MaxValue;
            List<KeyValuePair<int, int>> res = new List<KeyValuePair<int, int>>();

            int i = 0, j = b.Count - 1;
            while (i < a.Count && j >= 0)
            {
                int addition = a[i].Value + b[j].Value;
                if (addition > target)
                {
                    j = j - 1;
                }
                else
                {
                    if ((target - addition) < diff)
                    {
                        res = new List<KeyValuePair<int, int>>();
                        diff = target - addition;
                    }

                    res.Add(new KeyValuePair<int, int>(a[i].Key, b[j].Key));
                    i = i + 1;
                }
            }

            return res;
        }
    }

    class TwoSumFinder
    {
        public static int[] TwoSum(int[] nums, int target)
        {
            int[] result = new int[2];
            List<int> sortedNums = new List<int>(nums);
            sortedNums.Sort();
            int i = 0, j = nums.Length - 1;

            while (i < j)
            {
                int val = sortedNums[i] + sortedNums[j];

                if (val > target)
                {
                    j = j - 1;
                }
                else if (val < target)
                {
                    i = i + 1;
                }
                else
                {
                    if (sortedNums[i] == sortedNums[j])
                    {
                        result[0] = Array.IndexOf(nums, sortedNums[i]);
                        result[1] = Array.LastIndexOf(nums, sortedNums[i]);
                    }
                    else
                    {
                        result[0] = Array.IndexOf(nums, sortedNums[i]);
                        result[1] = Array.IndexOf(nums, sortedNums[j]);
                    }
                    break;
                }
            }

            return result;
        }
    }

    class TreasureIsland
    {
        public static int MinStep(char[][] grid)
        {
            bool[][] visited = new bool[grid.Length][];
            for (int i = 0; i < grid.Length; i++)
            {
                visited[i] = new bool[grid[i].Length];
                for (int j = 0; j < grid[i].Length; j++)
                {
                    visited[i][j] = false;
                }
            }

            return Traverse(grid, 0, 0, visited, int.MaxValue, 0);
        }

        private static int Traverse(char[][] grid, int r, int c, bool[][] visited, int minCost, int cur)
        {
            if (grid[r][c] == 'X')
            {
                Console.WriteLine($"returning {cur}");
                return cur;
            }

            int[] rf = new[] {-1, 0, 0, 1};
            int[] cf = new[] {0, 1, -1, 0};
            visited[r][c] = true;

            for (int k = 0; k < 4; k++)
            {
                int i = r + rf[k];
                int j = c + cf[k];

                if (i >= 0 && j >= 0 && i < grid.Length && j < grid[c].Length && grid[i][j] != 'D' && !visited[i][j])
                {
                    Console.WriteLine($"checking for {i},{j}");
                    int v = Traverse(grid, i, j, visited, minCost, cur+1);
                    minCost = Math.Min(minCost, v);
                }
            }

            visited[r][c] = false;
            return minCost;
        }
    }

    class IslandsInGrid
    {
        public static int NumIslands(char[][] grid)
        {
            int count = 0;

            if (grid == null ||
                grid.Length == 0)
            {
                return count;
            }

            if (grid.Length == 1 && grid[0].Length == 1)
            {
                return grid[0][0] == '1' ? 1 : 0;
            }

            bool[][] visited = new bool[grid.Length][];
            for (int i = 0; i < grid.Length; i++)
            {
                visited[i] = new bool[grid[i].Length];
                for (int j = 0; j < grid[i].Length; j++)
                {
                    visited[i][j] = false;
                }
            }

            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    if (CanVisit(grid, i, j, visited))
                    {
                        Visit(grid, i, j, visited);
                        count += 1;
                    }
                }
            }

            return count;
        }

        private static bool CanVisit(char[][] grid, int r, int c, bool[][] visited)
        {
            return r >= 0 && r < grid.Length && c >= 0 && c < grid[r].Length && grid[r][c] == '1' && !visited[r][c];
        }

        private static void Visit(char[][] grid, int r, int c, bool[][] visited)
        {
            visited[r][c] = true;

            int[] rowOff = new[] {-1, 0, 0, 1};
            int[] colOff = new[] {0, -1, 1, 0};

            for (int i = 0; i < 4; i++)
            {
                if (CanVisit(grid, r + rowOff[i], c + colOff[i], visited))
                {
                    Visit(grid, r + rowOff[i], c + colOff[i], visited);
                }
            }
        }
    }

    class Search2DGrid
    {
        public static bool SearchMatrix(int[,] matrix, int target)
        {
            if (matrix.Length == 0)
            {
                return false;
            }

            int rowcount = matrix.GetLength(0);
            int colcount = matrix.GetLength(1);

            int i = rowcount - 1;
            int j = 0;

            while (i >= 0 && j < colcount)
            {
                if (matrix[i, j] < target)
                {
                    j = j + 1;
                }
                else if (matrix[i, j] > target)
                {
                    i = i - 1;
                }
                else if (matrix[i, j] == target)
                {
                    return true;
                }
            }

            return false;
        }
    }

    class MinDistanceTo0
    {
        public static int MinDist(int[][] matrix, int[][] res, int r, int c)
        {
            int[] rOff = new[] {0, -1, 0, 1};
            int[] cOff = new[] {-1, 0, 1, 0};

            int min = int.MaxValue;
            for (int k = 0; k < 4; k++)
            {
                int i = r + rOff[k], j = c + cOff[k];
                if (i >= 0 && i < matrix.Length && j >= 0 && j < matrix[i].Length)
                {
                    min = Math.Min(min, res[i][j]);
                }
            }

            return min;
        }

        public static int[][] UpdateMatrix(int[][] matrix)
        {
            int[][] res = new int[matrix.Length][];

            for (int i = 0; i < matrix.Length; i++)
            {
                res[i] = new int[matrix[i].Length];
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    res[i][j] = int.MaxValue - 111;
                }
            }

            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    if (matrix[i][j] != 0)
                    {
                        if (i > 0)
                        {
                            res[i][j] = Math.Min(res[i][j], res[i - 1][j] + 1);
                        }
                        if (j > 0)
                        {
                            res[i][j] = Math.Min(res[i][j], res[i][j - 1] + 1);
                        }
                    }
                    else
                    {
                        res[i][j] = 0;
                    }
                }
            }

            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    if (matrix[i][j] != 0)
                    {
                        if (i < matrix.Length - 1)
                        {
                            res[i][j] = Math.Min(res[i][j], res[i + 1][j] + 1);
                        }
                        if (j < matrix[i].Length - 1)
                        {
                            res[i][j] = Math.Min(res[i][j], res[i][j + 1] + 1);
                        }
                    }
                }
            }

            return res;
        }
    }

    class DungeonGame
    {
        public static int CalculateMinimumHP(int[][] dungeon)
        {
            if (dungeon == null || dungeon.Length == 0) return 0;
            int m = dungeon.Length;
            int n = dungeon[0].Length;
            if (n == 0) return 0;
            Dictionary<string, int> mem = new Dictionary<string, int>();
            int res = Calculate(dungeon, ref mem, 0, 0, m, n);
            return res;
        }

        private static int Calculate(int[][] dungeon, ref Dictionary<string, int> mem, int i, int j, int m, int n)
        {
            if (i < 0 || i >= m || j < 0 || j >= n) return int.MaxValue;
            if (i == m - 1 && j == n - 1) return Math.Max(1 - dungeon[i][j], 1);
            if (mem.ContainsKey($"{i}-{j}")) return mem[$"{i}-{j}"];

            int health = int.MaxValue;
            health = Math.Min(health, Calculate(dungeon, ref mem, i + 1, j, m, n));
            health = Math.Min(health, Calculate(dungeon, ref mem, i, j + 1, m, n));
            int res = health > dungeon[i][j] ? health - dungeon[i][j] : 1;
            mem[$"{i}-{j}"] = res;
            return res;
        }
    }

    class KClosestPointToOrigin
    {
        public static int[][] KClosest(int[][] points, int K)
        {
            if (points.Length <= K)
            {
                return points;
            }

            int[] dists = new int[points.Length];
            int i = 0;
            foreach (int[] point in points)
            {
                dists[i] = dist(point);
                i += 1;
            }

            Array.Sort(dists);
            int val = dists[K];

            int[][] result = new int[K][];
            i = 0;
            foreach (int[] point in points)
            {
                if (i < K && dist(point) < val)
                {
                    result[i] = new int[2];
                    result[i][0] = point[0];
                    result[i][1] = point[1];
                    i = i + 1;
                }
            }

            return result;
        }

        public static int dist(int[] point)
        {
            return point[0] * point[0] + point[1] * point[1];
        }
    }

    class LongestPalindromeSubstring
    {
        public static string ExpandAroundCenter(string s, int l, int r)
        {
            while (l >= 0 && r < s.Length)
            {
                if (s[l] == s[r])
                {
                    l--;
                    r++;
                }
                else
                {
                    break;
                }
            }

            return r > l ? s.Substring(l + 1, r - l - 1) : string.Empty;
        }

        public static string LongestPalindrome(string s)
        {
            if (s.Length < 2)
            {
                return s;
            }

            int max = 0;
            string res = string.Empty;

            for (int i = 0; i < s.Length; i++)
            {
                string str1 = ExpandAroundCenter(s, i, i);
                string str2 = ExpandAroundCenter(s, i, i + 1);

                if (str1.Length > max)
                {
                    max = str1.Length;
                    res = str1;
                }

                if (str2.Length > max)
                {
                    max = str2.Length;
                    res = str2;
                }
            }

            return res;
        }

        public static int CountSubstrings(string s)
        {
            int res = s.Length;
            for (int i = 0; i < s.Length; i++)
            {
                string temp = ExpandAroundCenter(s, i, i);
                if (!string.IsNullOrEmpty(temp)) res += 1;

                temp = ExpandAroundCenter(s, i, i + 1);
                if (!string.IsNullOrEmpty(temp)) res += 1;
            }

            return res;
        }
    }

    class MostCommonWods
    {
        public static string MostCommonWord(string paragraph, string[] banned)
        {
            foreach (char c in paragraph)
            {
                if (!char.IsLetterOrDigit(c))
                {
                    paragraph = paragraph.Replace(c, ' ');
                }
            }

            Dictionary<string, int> d = new Dictionary<string, int>();
            foreach (string s in banned)
            {
                d[s.ToLower()] = -1;
            }

            foreach (string s in paragraph.Split(' '))
            {
                string s1 = s.ToLower().Trim();
                if (string.IsNullOrEmpty(s1))
                {
                    continue;
                }
                if (!d.ContainsKey(s1))
                {
                    d[s1] = 1;
                }
                else if (d[s1] != -1)
                {
                    d[s1] = d[s1] + 1;
                }
            }

            int max = 0;
            string res = string.Empty;
            foreach (KeyValuePair<string, int> p in d)
            {
                if (p.Value > max)
                {
                    max = p.Value;
                    res = p.Key;
                }
            }

            return res;
        }
    }

    class MergeIntervals
    {
        public static int[][] Merge(int[][] intervals)
        {
            if (intervals.Length <= 1)
            {
                return intervals;
            }
            int[][] sorted = intervals.OrderBy(ints => ints[0]).ToArray();
            List<int[]> res = new List<int[]>();
            int cur = 0;
            res.Add(sorted[0]);
            for (int i = 1; i < sorted.Length; i++)
            {
                if (sorted[i][0] <= res[cur][1])
                {
                    if (sorted[i][1] > res[cur][1])
                    {
                        res[cur][1] = sorted[i][1];
                    }
                }
                else
                {
                    res.Add(sorted[i]);
                    cur += 1;
                }
            }

            return res.ToArray();
        }
    }

    class LongestVowelString
    {
        public static int GetLength(string s)
        {
            int cstart = 0, cend = s.Length - 1;
            while (cstart < s.Length)
            {
                if (!IsVowel(s[cstart]))
                {
                    break;
                }

                cstart += 1;
            }

            while (cend >= 0)
            {
                if (!IsVowel(s[cend]))
                {
                    break;
                }

                cend -= 1;
            }

            int maxLen = 0, vstart = cstart;
            for (int i = cstart; i <= cend; i++)
            {
                if (IsVowel(s[i]))
                {
                    int size = i - vstart + 1;
                    if (size > maxLen)
                    {
                        maxLen = size;
                    }
                }
                vstart += 1;
            }

            return (cstart - 0) + maxLen + (s.Length - 1 - cend);
        }

        private static bool IsVowel(char c)
        {
            char[] vowels = new[] {'a', 'e', 'i', 'o', 'u'};
            return vowels.Contains(c);
        }
    }

    class ReorganizingString
    {
        public static string ReorganizeString(string S)
        {
            Dictionary<char, int> count = new Dictionary<char, int>();
            foreach (char c in S)
            {
                int v = count.GetValueOrDefault(c, 0);
                count[c] = v + 1;
            }

            count = new Dictionary<char, int>(count.OrderByDescending(pair => pair.Value));

            StringBuilder res = new StringBuilder();
            bool canPossible = true;
            for (int i = 0; i < S.Length; i = i + 2)
            {
                char a = count.ElementAt(0).Key;

                if (count.Count < 2 && count[a] > 1)
                {
                    canPossible = false;
                    break;
                }

                res.Append(a);
                count[a] = count[a] - 1;

                if (count.Count >= 2)
                {
                    char b = count.ElementAt(1).Key;
                    res.Append(b);
                    count[b] = count[b] - 1;
                    if (count[b] == 0)
                    {
                        count.Remove(b);
                    }
                }
            }

            return canPossible ? res.ToString() : string.Empty;
        }

        public static int[] RearrangeBarcodes(int[] barcodes)
        {
            Dictionary<int, int> count = new Dictionary<int, int>();
            foreach (int c in barcodes)
            {
                int v = count.GetValueOrDefault(c, 0);
                count[c] = v + 1;
            }

            count = new Dictionary<int, int>(count.OrderByDescending(pair => pair.Value));

            List<int> res = new List<int>();
            bool canPossible = true;
            for (int i = 0; i < barcodes.Length; i = i + 2)
            {
                int a = count.ElementAt(0).Key;

                if (count.Count < 2 && count[a] > 1)
                {
                    canPossible = false;
                    break;
                }

                res.Add(a);
                count[a] = count[a] - 1;

                if (count.Count >= 2)
                {
                    int b = count.ElementAt(1).Key;
                    res.Add(b);
                    count[b] = count[b] - 1;
                    if (count[b] == 0)
                    {
                        count.Remove(b);
                    }
                }

                if (count[a] == 0)
                {
                    count.Remove(a);
                }
            }

            return canPossible ? res.ToArray() : new int[0];
        }
    }

    class LongestSubStrWithDifferent3ConsesCharacters
    {
        public static int FindLength(string s)
        {
            if (s.Length < 3)
            {
                return s.Length;
            }

            int maxLen = 2;
            int start = 0;
            for (int i = 2; i < s.Length; i++)
            {
                if (s[i] == s[i - 1] && s[i] == s[i - 2])
                {
                    start = i - 1;
                }
                else
                {
                    maxLen = Math.Max(maxLen, (i - start + 1));
                }
            }

            return maxLen;
        }
    }

    class Find3Sum
    {
        public static IList<IList<int>> ThreeSum(int[] nums)
        {
            IList<IList<int>> res = new List<IList<int>>();
            Array.Sort(nums);
            for (int i = 0; i < nums.Length; i++)
            {
                int j = i + 1;
                int k = nums.Length - 1;
                if (i > 0 && nums[i] == nums[i - 1])
                {
                    continue;
                }

                while (j < k)
                {
                    if (k < nums.Length - 1 && nums[k] == nums[k + 1])
                    {
                        k -= 1;
                        continue;
                    }

                    int v = nums[i] + nums[j] + nums[k];
                    if (v == 0)
                    {
                        List<int> temp = new List<int>() {nums[i], nums[j], nums[k]};
                        res.Add(temp);
                        j += 1;
                        k -= 1;
                    }
                    else if (v > 0)
                    {
                        k -= 1;
                    }
                    else
                    {
                        j += 1;
                    }
                }
            }

            return res;
        }
    }

    class SetMetixTo0
    {
        public static void SetZeroes(int[][] matrix)
        {
            bool[] rows = new bool[matrix.Length];
            bool[] cols = new bool[matrix[0].Length];

            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    if (matrix[i][j] == 0)
                    {
                        rows[i] = true;
                        cols[j] = true;
                    }
                }
            }

            for (int i = 0; i < rows.Length; i++)
            {
                if (rows[i])
                {
                    for (int j = 0; j < matrix[i].Length; j++)
                    {
                        matrix[i][j] = 0;
                    }
                }
            }

            for (int j = 0; j < cols.Length; j++)
            {
                if (cols[j])
                {
                    for (int i = 0; i < matrix.Length; i++)
                    {
                        matrix[i][j] = 0;
                    }
                }
            }
        }
    }

    class LongestSubstringWithoutRepeatingCharacters
    {
        public static int LengthOfLongestSubstring(string s)
        {
            if (s.Length < 2)
            {
                return s.Length;
            }

            Dictionary<char, int> map = new Dictionary<char, int>();
            int maxSize = 0;
            int start = 0;

            for (int i = 0; i < s.Length; i++)
            {
                int p = map.GetValueOrDefault(s[i], -1);
                if (p == -1 || p < start)
                {
                    maxSize = Math.Max(maxSize, i - start + 1);
                }
                else
                {
                    start = map[s[i]] + 1;
                }

                map[s[i]] = i;
            }

            return maxSize;
        }
    }

    class GroupAnagram
    {
        public static IList<IList<string>> GroupAnagrams(string[] strs)
        {
            IList<IList<string>> res = new List<IList<string>>();
            Dictionary<string, List<string>> map = new Dictionary<string, List<string>>();

            foreach (string s in strs)
            {
                char[] arr = s.ToCharArray();
                Array.Sort(arr);
                string sorted = string.Join(' ', arr);

                if (map.ContainsKey(sorted))
                {
                    map[sorted].Add(s);
                }
                else
                {
                    List<string> temp = new List<string>() {s};
                    map[sorted] = temp;
                }
            }

            foreach (List<string> list in map.Values)
            {
                res.Add(list);
            }

            return res;
        }
    }

    class LongestIncreasingPathInMatrx
    {
        public static int LongestIncreasingPath(int[][] matrix)
        {
            int m = matrix.Length;
            int n = m > 0 ? matrix[0].Length : 0;

            int[][] res = new int[m][];

            for (int i = 0; i < m; i++)
            {
                res[i] = new int[n];
                for (int j = 0; j < n; j++)
                {
                    res[i][j] = 0;
                }
            }

            int val = 0;
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    val = Math.Max(val, calculate(matrix, res, i, j, m, n));
                }
            }

            return val;
        }

        public static int calculate(int[][] mat, int[][] res, int i, int j, int m, int n)
        {
            if (res[i][j] == 0)
            {
                res[i][j] = 1 + new[]
                {
                    i-1 >= 0 && mat[i-1][j] < mat[i][j] ? calculate(mat, res, i-1, j, m, n) : 0,
                    j-1 >= 0 && mat[i][j-1] < mat[i][j] ? calculate(mat, res, i, j-1, m, n) : 0,
                    i+1 < m && mat[i+1][j] < mat[i][j] ? calculate(mat, res, i+1, j, m, n) : 0,
                    j+1 < n && mat[i][j+1] < mat[i][j] ? calculate(mat, res, i, j+1, m, n) : 0,
                }.Max();
            }

            return res[i][j];
        }
    }

    class MinCostToRepairEdges
    {
        public static int[] GenerateMST(int[][] graph, int V)
        {
            int[] parents = new int[V];
            int[] keys = new int[V];
            bool[] mst = new bool[V];

            for (int i = 0; i < V; i++)
            {
                keys[i] = int.MaxValue - 10;
                mst[i] = false;
            }

            keys[0] = 0;
            parents[0] = -1;
            mst[0] = true;
            for (int i = 0; i < V - 1; i++)
            {
                int u = FindMinVertex(mst, keys);
                mst[u] = true;

                for (int v = 0; v < V; v++)
                {
                    if (!mst[v] && graph[u][v] != int.MaxValue - 10 && graph[u][v] < keys[v])
                    {
                        keys[v] = graph[u][v];
                        parents[v] = u;
                    }
                }
            }

            return parents;
        }

        public static int FindMinVertex(bool[] mst, int[] keys)
        {
            int min = int.MaxValue;
            int res = 0;

            for (int i = 0; i < mst.Length; i++)
            {
                if (!mst[i] && keys[i] < min)
                {
                    min = keys[i];
                    res = i;
                }
            }

            return res;
        }

        public static void PrintMst(int[] p, int[][] graph)
        {
            for (int i = 1; i < p.Length; i++)
            {
                Console.WriteLine($"{i} -> {p[i]} ---- {graph[p[i]][i]}");
            }
        }

        public static int CalculateCost(int V, int[][] edges, int[][] repair)
        {
            int[][] graph = new int[V][];

            for (int i = 0; i < V; i++)
            {
                graph[i] = new int[V];
                for (int j = 0; j < V; j++)
                {
                    graph[i][j] = int.MaxValue - 10;
                }
            }

            foreach (int[] edge in edges)
            {
                int u = edge[0]-1;
                int v = edge[1]-1;

                graph[u][v] = 0;
                graph[v][u] = 0;
            }

            foreach (int[] edge in repair)
            {
                int u = edge[0]-1;
                int v = edge[1]-1;
                int s = edge[2];

                graph[u][v] = s;
                graph[v][u] = s;
            }

            foreach (int[] i in graph)
            {
                Console.WriteLine($"{string.Join(' ', i)}");
            }

            int[] mst = GenerateMST(graph, V);
            PrintMst(mst, graph);
            int cost = 0;
            for (int i = 1; i < mst.Length; i++)
            {
                cost += graph[mst[i]][i];
            }

            return cost;
        }
    }

    class PrisonAfterDayN
    {
        public static int[] PrisonAfterNDays(int[] cells, int N)
        {
            Dictionary<int, int> map = new Dictionary<int, int>();

            int state = 0;
            for (int i = 0; i < 8; i++)
            {
                if (cells[i] > 0)
                {
                    state = state ^ (1 << i);
                }
            }

            PrintState(state);

            while (N > 0)
            {
                if (map.ContainsKey(state))
                {
                    int period = map[state] - N;
                    N = N % period;
                }

                if (N > 0)
                {
                    state = SimulateNext(state);
                    Console.Write($"Day:{N}  ");
                    PrintState(state);
                    map[state] = N;
                    N -= 1;
                }
            }

            return PrintState(state);
        }
        public static int[] PrintState(int state)
        {
            int[] ans = new int[8];
            for (int i = 0; i < 8; i++)
            {
                if ((state & (1 << i)) > 0)
                {
                    ans[i] = 1;
                }
            }
            Console.WriteLine($"{string.Join(' ', ans)}");
            return ans;
        }

        public static int SimulateNext(int state)
        {
            int ans = 0;

            for (int i = 1; i < 7; i++)
            {
                if ((state >> i - 1 & 1) == (state >> i + 1 & 1))
                {
                    ans = ans ^ 1;
                    ans = ans << i;
                }
            }

            return ans;
        }
    }

    class PartitionLabel
    {
        public static IList<int> PartitionLabels(string S)
        {
            List<int> res = new List<int>();

            Dictionary<char, int> lastindex = new Dictionary<char, int>();
            for (int i = 0; i < S.Length; i++)
            {
                lastindex[S[i]] = i;
            }

            int start = 0;
            while (start < S.Length)
            {
                int end = lastindex[S[start]];
                int runner = start;
                while (runner < end)
                {
                    end = Math.Max(end, lastindex[S[runner]]);
                    runner++;
                }

                res.Add(end - start + 1);
                start = end + 1;
            }

            return res;
        }
    }

    class LongestIncreasingSubsequence
    {
        public static int LengthOfLIS(int[] nums)
        {
            if (nums.Length <= 1)
            {
                return nums.Length;
            }

            int[] res = new int[nums.Length];
            res[0] = 1;
            int maxLength = 0;
            for (int i = 1; i < nums.Length; i++)
            {
                int curMax = 0;
                for (int j = 0; j < i; j++)
                {
                    if (nums[j] < nums[i])
                    {
                        curMax = Math.Max(curMax, res[j]);
                    }
                }

                res[i] = curMax + 1;
                maxLength = Math.Max(maxLength, res[i]);
            }

            return maxLength;
        }
    }

    class KthSmallestInSortedMatrix
    {
        public static int kthSmallest(int[][] matrix, int k)
        {
            int m = matrix.Length;
            int l = matrix[0][0], u = matrix[m - 1][m - 1];
            while (l < u)
            {
                int mid = l + (u - l) / 2;
                int count = Count(matrix, mid);
                if (k > count)
                {
                    l = mid + 1;
                }
                else
                {
                    u = mid;
                }
            }

            return u;
        }

        public static int Count(int[][] matrix, int target)
        {
            int i = matrix.Length - 1, j = 0, count = 0;
            while (i >= 0 && j < matrix.Length)
            {
                if (matrix[i][j] <= target)
                {
                    count += i + 1;
                    j += 1;
                }
                else
                {
                    i -= 1;
                }
            }

            return count;
        }
    }

    class IncreasingTripletSubsequence
    {
        public static bool IncreasingTriplet(int[] nums)
        {
            if (nums.Length < 3)
            {
                return false;
            }

            int a = int.MaxValue;
            int b = int.MaxValue;
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] < a)
                {
                    a = nums[i];
                }

                if (nums[i] > a)
                {
                    b = Math.Min(b, nums[i]);
                }

                if (nums[i] > b)
                {
                    return true;
                }
            }

            return false;
        }
    }

    class UpdateServers
    {
        public static int minimumDays(int rows, int columns, int[,] grid)
        {
            if (rows == 0 && columns == 0) return 0;

            if (rows == 1 && columns == 1)
            {
                if (grid[0, 0] == 0) return int.MaxValue;
                else return 0;
            }

            int count = 0;
            if (IfAllZero(grid, rows, columns))
            {
                count = 1;
                grid[0, 0] = 1;
            }

            while (!AreAllUpdated(grid, rows, columns))
            {
                count += 1;
                ProcessUpdate(grid, rows, columns);
            }

            return count;
        }

        public static void ProcessUpdate(int[,] matrix, int rowCount, int colCount)
        {
            List<int> rows = new List<int>();
            List<int> cols = new List<int>();

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    if (matrix[i, j] == 1)
                    {
                        rows.Add(i);
                        cols.Add(j);
                    }
                }
            }

            for (int i = 0; i < rows.Count; i++)
            {
                int r = rows[i];
                int c = cols[i];

                SetOne(matrix, r - 1, c, rowCount, colCount);
                SetOne(matrix, r + 1, c, rowCount, colCount);
                SetOne(matrix, r, c - 1, rowCount, colCount);
                SetOne(matrix, r, c + 1, rowCount, colCount);
            }
        }

        public static void SetOne(int[,] mat, int r, int c, int rowCount, int colCount)
        {
            if (r >= 0 && r < rowCount && c >= 0 && c < colCount)
            {
                mat[r, c] = 1;
            }
        }

        public static bool AreAllUpdated(int[,] mat, int rowCount, int colCount)
        {
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    if (mat[i, j] == 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static bool IfAllZero(int[,] mat, int rowCount, int colCount)
        {
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    if (mat[i, j] == 1)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }

    class UniquePathInGrid
    {
        public static int UniquePaths(int m, int n)
        {
            int[][] v = new int[m][];
            for (int i = 0; i < m; i++)
            {
                v[i] = new int[n];
                for (int j = 0; j < n; j++)
                {
                    v[i][j] = 0;
                }
            }

            for (int i = m - 1; i >= 0; i--)
            {
                for (int j = n - 1; j >= 0; j--)
                {
                    int r = i + 1, c = j + 1;
                    if (r >= m || c >= n)
                    {
                        v[i][j] = 1;
                    }
                    else
                    {
                        v[i][j] = v[r][j] + v[i][c];
                    }
                }
            }

            return v[0][0];
        }
    }
}
