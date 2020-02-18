using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonOA
{
    using System.Collections;
    using System.Linq;
    using Microsoft.VisualBasic;

    class Others
    {
        public void Run()
        {
            #region Sorting And Searching

            int[] num1 = new[] {0};
            int[] num2 = new[] {1};
            Sorting.Merge(num1, 0, num2, 1);
            Console.WriteLine($"{string.Join(',', num1)}");
            Console.WriteLine("_______________________________________________________");

            #endregion

            Console.WriteLine($"Numbers of primes between 0 to 50 is {CountPrimes(50)}");
            Console.WriteLine("_______________________________________________________");

            Console.WriteLine($"Check if 27 is power of 3: {IsPowerOfThree(27)}");
            Console.WriteLine($"Check if 30 is power of 3: {IsPowerOfThree(30)}");
            Console.WriteLine("_______________________________________________________");

            Console.WriteLine($"Decimal value of roman number MCMXCIV is {RomanToInt("MCMXCIV")}");
            Console.WriteLine($"Decimal value of roman number LVIII is {RomanToInt("LVIII")}");
            Console.WriteLine("_______________________________________________________");

            Console.WriteLine($"Number of bit set in 11 is {HammingWeight(11)}");
            Console.WriteLine("_______________________________________________________");

            Console.WriteLine($"after reversing bits of 43261596 result is {reverseBits(43261596)}");
            Console.WriteLine("_______________________________________________________");


            Console.WriteLine($"Is parenthesis valid (] : {IsValid("(]")}");
            Console.WriteLine("_______________________________________________________");

            IList<IList<int>> mat = GeneratePascalTriangle(5);
            foreach (IList<int> i in mat)
            {
                Console.WriteLine($"{string.Join(' ',i)}");
            }
            Console.WriteLine("_______________________________________________________");

            int[][] people = new int[6][];
            people[0] = new[] {7, 0};
            people[1] = new[] {4, 4};
            people[2] = new[] {7, 1};
            people[3] = new[] {5, 0};
            people[4] = new[] {6, 1};
            people[5] = new[] {5, 2};
            ReconstructQueue(people);
            Console.WriteLine("_______________________________________________________");

            int[] nums = new[] {1, 2, 3};
            IList<IList<int>> perm = Permute(nums);
            foreach (IList<int> list in perm)
            {
                Console.WriteLine(string.Join(',', list));
            }
            Console.WriteLine("_______________________________________________________");


            IList<string> res = GenerateParenthesis(3);
            foreach (string s in res)
            {
                Console.WriteLine(s);
            }
            Console.WriteLine("_______________________________________________________");


            Console.WriteLine($"Num palindromes in aaa: {CountSubstrings("aaa")}");
            Console.WriteLine("_______________________________________________________");


            IList<IList<int>> res1 = Subsets(nums);
            Console.WriteLine($"All subsets of [{string.Join(',', nums)}] are:");
            foreach (IList<int> list in res1)
            {
                Console.WriteLine($"[{string.Join(',', list)}]");
            }
            Console.WriteLine("_______________________________________________________");


            nums = new[] {2, 3, 6, 7};
            res1 = CombinationSum(nums, 7);
            Console.WriteLine($"Combination sum for 7 is");
            foreach (IList<int> list in res1)
            {
                Console.WriteLine($"{string.Join(',', list)}");
            }
            Console.WriteLine("_______________________________________________________");


            nums = new[] {3, 2, 1, 5, 6, 4};
            Console.WriteLine($"2nd largest int is {FindKthLargest(nums, 2)}");
            Console.WriteLine("_______________________________________________________");


            Console.WriteLine($"Decoding string 2[abc]3[cd]ef = {DecodeString("2[abc]3[cd]ef")}");
            Console.WriteLine("_______________________________________________________");


            nums = new[] {1, 2, 3, 4};
            Console.WriteLine($"Product except self = {string.Join(',', ProductExceptSelf(nums))}");
            Console.WriteLine("_______________________________________________________");


            // nums = new[] {2, 6, 4, 8, 10, 9, 15};
            nums = new[] {2, 1};
            Console.WriteLine($"Unsorted sub array length is {FindUnsortedSubarray(nums)}");
            Console.WriteLine("_______________________________________________________");
        }

        public int CountPrimes(int n)
        {
            bool[] flags = new bool[n];

            for (int i = 2; i*i < n; i++)
            {
                if (!flags[i])
                {
                    for (int j = i; i*j < n; j++)
                    {
                        flags[i * j] = true;
                    }
                }
            }

            int count = 0;
            for (int i = 2; i < n; i++)
            {
                if (!flags[i]) count++;
            }

            return count;
        }

        public bool IsPowerOfThree(int n)
        {
            if (n <= 0) return false;
            int val = (int) Math.Pow(3, 19);
            return val % n == 0;
        }

        public int RomanToInt(string s)
        {
            Dictionary<char, int> map = new Dictionary<char, int>();
            map['I'] = 1;
            map['V'] = 5;
            map['X'] = 10;
            map['L'] = 50;
            map['C'] = 100;
            map['D'] = 500;
            map['M'] = 1000;

            int count = 0;
            for (int i = 0; i < s.Length; i++)
            {
                int a = map[char.ToUpper(s[i])];
                if (i+1 < s.Length)
                {
                    int b = map[char.ToUpper(s[i+1])];
                    if (b > a)
                    {
                        count += (b - a);
                        i += 1;
                        continue;
                    }
                }

                count += a;
            }

            return count;
        }

        public int HammingWeight(uint n)
        {
            int count = 0;
            while (n > 0)
            {
                if ((n & 1) == 1) count++;
                n = n >> 1;
            }

            return count;
        }

        public int HammingDistance(int x, int y)
        {
            int n = x ^ y;
            int count = 0;
            while (n > 0)
            {
                if ((n & 1) == 1) count++;
                n = n >> 1;
            }

            return count;
        }

        public uint reverseBits(uint n)
        {
            uint res = 0;
            Console.WriteLine($"n = {ToBinString(n)}");
            for (int i = 0; i < 32; i++)
            {
                res = res << 1;
                if ((n & 1) == 1) res = res ^ 1;
                n = n >> 1;

                Console.WriteLine($"res = {ToBinString(res)}");
                Console.WriteLine($"n = {ToBinString(n)}");
                Console.WriteLine("-------------------------");
            }

            return res;
        }

        public string ToBinString(uint n)
        {
            return Convert.ToString(n, 2).PadLeft(32, '0');
        }

        public bool IsValid(string s)
        {
            Stack<char> st = new Stack<char>();
            foreach (char c in s)
            {
                if (c == '(' || c == '[' || c == '{') st.Push(c);
                else
                {
                    if (st.Count == 0) return false;
                    if (!IsParenMatch(c, st.Pop())) return false;
                }
            }

            return st.Count == 0;
        }

        public bool IsParenMatch(char l, char r)
        {
            switch (l)
            {
                case '}':
                    return r == '{';
                case ']':
                    return r == '[';
                case ')':
                    return r == '(';
                default: return false;
            }
        }

        public IList<IList<int>> GeneratePascalTriangle(int numRows)
        {
            int[][] mat = new int[numRows][];
            for (int i = 0; i < numRows; i++)
            {
                mat[i] = new int[numRows];
                mat[i][0] = 1;
                mat[i][i] = 1; 
                int j;
                for (j = 1; j < i; j++)
                {
                    mat[i][j] = mat[i - 1][j - 1] + mat[i - 1][j];
                }

                for (j = i+1; j < numRows; j++)
                {
                    mat[i][j] = 0;
                }
            }

            List<IList<int>> res = new List<IList<int>>();
            foreach (int[] i in mat)
            {
                List<int> r = new List<int>();
                foreach (int a in i)
                {
                    if (a != 0) r.Add(a);
                }

                res.Add(r);
            }

            return res;
        }

        public int MissingNumber(int[] nums)
        {
            int n = nums.Length;
            int expSum = (n * (n + 1)) / 2;
            int actSum = 0;
            foreach (int i in nums) actSum += i;

            return expSum - actSum;
        }

        public IList<string> FizzBuzz(int n)
        {
            List<string> res = new List<string>();
            for (int i = 1; i <= n; i++)
            {
                res.Add(GetFizzBuzz(i));
            }

            return res;
        }

        public string GetFizzBuzz(int n)
        {
            string res = string.Empty;

            if (n % 3 == 0) res += "Fizz";
            if (n % 5 == 0) res += "Buzz";
            if (string.IsNullOrEmpty(res)) res += n.ToString();

            return res;
        }

        public int SingleNumber(int[] nums)
        {
            int res = 0;
            foreach (int n in nums)
            {
                res = res ^ n;
            }

            return res;
        }

        public void MoveZeroes(int[] nums)
        {
            int i = 0, j = 0;
            while (j < nums.Length)
            {
                if (nums[j] == 0)
                {
                    j += 1;
                }
                else
                {
                    int temp = nums[i];
                    nums[i] = nums[j];
                    nums[j] = temp;
                    i += 1;
                    j += 1;
                }
            }
        }

        public int[][] ReconstructQueue(int[][] people)
        {
            List<int[]> res = new List<int[]>(people.Length);
            if (people.Length < 1) return res.ToArray();
            Array.Sort(people, (a, b) => a[0] == b[0] ? b[1].CompareTo(a[1]) : a[0].CompareTo(b[0]));
            res.Add(people[people.Length -1]);
            for (int i = people.Length-2; i >= 0; i--)
            {
                res.Insert(people[i][1], people[i]);
            }

            return res.ToArray();
        }

        public int[] DailyTemperatures(int[] T)
        {
            int[] res = new int[T.Length];
            Stack<int[]> s = new Stack<int[]>();
            int i = 0;
            while (i < T.Length)
            {
                if (!s.Any() || T[i] <= s.Peek()[0])
                {
                    s.Push(new []{T[i], i});
                    i += 1;
                }
                else
                {
                    int[] v = s.Pop();
                    res[v[1]] = i - v[1];
                }
            }

            return res;
        }

        public IList<IList<int>> Permute(int[] nums)
        {
            IList<IList<int>> res = new List<IList<int>>();
            Permute(nums, 0, new List<int>(), res);
            return res;
        }

        public void Permute(int[] nums, int n, int[] cur, IList<IList<int>> res)
        {
            if (n == cur.Length)
            {
                res.Add(new List<int>(cur));
                return;
            }

            /*foreach (int i in nums)
            {
                cur[n] = i;
                int[] nnums = nums.Where(no => no != i).ToArray();
                Permute(nnums, n + 1, cur, res);
            }*/

            for (int i = n; i < nums.Length; i++)
            {
                cur[n] = nums[i];
                Permute(nums, n + 1, cur, res);
            }
        }

        public void Permute(int[] nums, int n, List<int> cur, IList<IList<int>> res)
        {
            if (n == nums.Length)
            {
                res.Add(new List<int>(cur));
                return;
            }

            for (int i = n; i < nums.Length; i++)
            {
                cur.Add(nums[i]);
                Permute(nums, n + 1, cur, res);
                cur.Remove(cur.Count - 1);
            }

            
        }

        public IList<string> GenerateParenthesis(int n)
        {
            List<string> res = new List<string>();
            GenerateParenthesis(0, 0, n, string.Empty, res);
            return res;
        }

        public void GenerateParenthesis(int l, int r, int max, string s, IList<string> res)
        {
             if (s.Length == 2*max)
            {
                res.Add(s);
                return;
            }

            if (l < max)
            {
                s = s + '(';
                GenerateParenthesis(l+1, r, max, s, res);
            }

            if (r < l)
            {
                s = s + ')';
                GenerateParenthesis(l, r + 1, max, s, res);
            }
        }

        public string ExpandAroundCenter(string s, int l, int r, out int temp)
        {
            temp = 0;
            while (l >= 0 && r < s.Length)
            {
                if (s[l] == s[r])
                {
                    temp++;
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

        public int CountSubstrings(string s)
        {
            int res = 0;
            for (int i = 0;  i < s.Length; i++)
            {
                ExpandAroundCenter(s, i, i, out int temp);
                res += temp;
                temp = 0;
                ExpandAroundCenter(s, i, i + 1, out temp);
                res += temp;
            }

            return res;
        }

        public int FindDuplicate(int[] nums)
        {
            for (int i = 0; i < nums.Length; i++)
            {
                int pos = Math.Abs(nums[i]);
                if (nums[pos] < 0) return pos;
                nums[pos] = nums[pos] * -1;
            }

            return int.MinValue;
        }

        public IList<IList<int>> Subsets(int[] nums)
        {
            IList<IList<int>> res = new List<IList<int>>();
            Subsets(nums, 0, new List<int>(), res);
            return res;
        }

        public void Subsets(int[] nums, int start, IList<int> temp, IList<IList<int>> res)
        {
            res.Add(new List<int>(temp));
            for (int i = start; i < nums.Length; i++)
            {
                temp.Add(nums[i]);
                Subsets(nums, i+1, temp, res);
                temp.RemoveAt(temp.Count - 1);
            }
        }

        public IList<IList<int>> CombinationSum(int[] candidates, int target)
        {
            IList<IList<int>> res = new List<IList<int>>();
            CombinationSum(candidates, target, 0, new List<int>(), res);
            return res;
        }

        public void CombinationSum(int[] candidates, int target, int start, IList<int> temp, IList<IList<int>> res)
        {
            if (target < 0) return;
            if (target == 0)
            {
                res.Add(new List<int>(temp));
                return;
            }

            for (int i = start; i < candidates.Length; i++)
            {
                temp.Add(candidates[i]);
                CombinationSum(candidates, target - candidates[i], i, temp, res);
                temp.RemoveAt(temp.Count - 1);
            }
        }

        public int FindKthLargest(int[] nums, int k)
        {
            List<int> s = new List<int>(k);
            foreach (int i in nums)
            {
                if (s.Count == 0) s.Add(i);
                else if (i < s[0])
                {
                    for (int j = s.Count -1; j >= 0; j--)
                    {
                        if (s[j] >= i)
                        {
                            if (j+1 < k) s.Insert(j+1, i);
                            break;
                        }
                    }
                }
                else
                {
                    s.Insert(0, i);
                }
            }

            return s.ElementAt(k-1);
        }

        public string DecodeString(string s)
        {
            string res = string.Empty;
            Stack<char> stk = new Stack<char>();
            for (int i = s.Length - 1; i >= 0; i--)
            {
                if (char.IsDigit(s[i]))
                {
                    int i1 = i;
                    while (i1 >= 0 && char.IsDigit(s[i1])) i1--;
                    int n = Int32.Parse(s.Substring(i1+1, i - i1));
                    i = i1 + 1;
                    stk.Pop();
                    string stemp = string.Empty;
                    while (char.IsLetter(stk.Peek()))
                    {
                        stemp = stemp + stk.Pop();
                    }

                    stk.Pop();

                    string res2 = string.Empty;
                    for (int j = 0; j < n; j++)
                    {
                        res2 = stemp + res2;
                    }

                    for (int j = res2.Length -1; j >= 0; j--)
                    {
                        stk.Push(res2[j]);
                    }
                }
                else
                {
                    stk.Push(s[i]);
                }
            }

            while (stk.Any())
            {
                res = res + stk.Pop();
            }

            return res;
        }

        public int MajorityElement(int[] nums)
        {
            int res = nums[0], sum = 1;
            for (int i = 1; i < nums.Length; i++)
            {
                if (nums[i] == res) sum += 1;
                else
                {
                    sum -= 1;
                    if (sum == 0)
                    {
                        res = nums[i];
                        sum = 1;
                    }
                }
            }

            return res;
        }

        public IList<int> FindDisappearedNumbers(int[] nums)
        {
            foreach (int i in nums)
            {
                int pos = Math.Abs(i) - 1;
                if (nums[pos] > 0) nums[pos] = -1 * nums[pos];
            }

            List<int> res = new List<int>();
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i] > 0) res.Add(i+1);
            }

            return res;
        }

        public int[] CountBits(int num)
        {
            if (num < 0) return new int[0];
            if (num == 0) return new[] {0};

            int[] res = new int[num+1];
            res[0] = 0;
            res[1] = 1;
            for (int i = 2; i <= num; i++)
            {
                res[i] = res[i >> 1] + ((i & 1) == 1 ? 1 : 0);
            }

            return res;
        }

        public int FindUnsortedSubarray(int[] nums)
        {
            int min = int.MaxValue, max = int.MinValue;
            bool flag = false;
            for (int i = 1; i < nums.Length; i++)
            {
                if (nums[i] < nums[i - 1])
                    flag = true;
                if (flag)
                    min = Math.Min(min, nums[i]);
            }
            flag = false;
            for (int i = nums.Length - 2; i >= 0; i--)
            {
                if (nums[i] > nums[i + 1])
                    flag = true;
                if (flag)
                    max = Math.Max(max, nums[i]);
            }
            int l, r;
            for (l = 0; l < nums.Length; l++)
            {
                if (min < nums[l])
                    break;
            }
            for (r = nums.Length - 1; r >= 0; r--)
            {
                if (max > nums[r])
                    break;
            }
            return r - l < 0 ? 0 : r - l + 1;
        }

        public int[] ProductExceptSelf(int[] nums)
        {
            int n = nums.Length;
            if (n <= 0) return new int[0];
            if (n == 1) return new[] {1};

            int[] l = new int[n];
            l[0] = 1;
            int[] r = new int[n];
            r[n - 1] = 1;
            int[] res = new int[n];

            for (int i = 1; i < n; i++)
            {
                l[i] = l[i - 1] * nums[i-1];
            }

            for (int i = n-2; i >= 0; i--)
            {
                r[i] = r[i + 1] * nums[i+1];
            }

            for (int i = 0; i < n; i++)
            {
                res[i] = l[i] * r[i];
            }

            return res;
        }

        public IList<int> TopKFrequent(int[] nums, int k)
        {
            Dictionary<int, int> dic= new Dictionary<int, int>();
            foreach (int i in nums)
            {
                dic[i] = dic.GetValueOrDefault(i, 0) + 1;
            }

            List<List<int>> bucket = new List<List<int>>(nums.Length);
            for (int i = 0; i < nums.Length; i++) bucket.Add(new List<int>());
            foreach (int key in dic.Keys)
            {
                int f = dic[key];
                bucket[f-1].Add(key);
            }

            List<int> res = new List<int>();
            int j = bucket.Count - 1;
            while (k > 0 && j >= 0)
            {
                if (bucket[j].Count == 0) j--;
                else
                {
                    res.AddRange(bucket[j]);
                    k = k - bucket[j].Count;
                    j--;
                }
            }

            return res.ToArray();
        }

        public int FindTargetSumWays(int[] nums, int S)
        {
            return FindTargetSumWays(nums, 0, S);
        }
        public int FindTargetSumWays(int[] nums, int start, int S)
        {
            if (start == nums.Length) return (S == 0) ? 1 : 0;

            return FindTargetSumWays(nums, start + 1, S - nums[start]) +
                   FindTargetSumWays(nums, start + 1, S + nums[start]);
        }

        public int MaxArea(int[] height)
        {
            int max = int.MinValue;
            int n = height.Length, i = 0, j = n - 1;
            while (i < j)
            {
                int area = Math.Min(height[i], height[j]) * (j - i);
                max = Math.Max(max, area);

                if (height[j] <= height[i]) j--;
                else i++;
            }

            return max;
        }

        public int NumTrees(int n)
        {
            int[] dp = new int[n+1];
            dp[0] = 1;
            dp[1] = 1;
            for (int i = 2; i <= n; i++)
            {
                for (int j = 1; j <= i; j++)
                {
                    dp[i] = dp[i] + (dp[j - 1] * dp[i - j]);
                }
            }

            return dp[n];
        }

        public int MaxProfit(int[] prices)
        {
            if (prices.Length <= 1) return 0;

            int[] b = new int[prices.Length+1];
            int[] s = new int[prices.Length+1];
            s[1] = 0;
            b[1] = -prices[0];

            for (int i = 2; i <= prices.Length; i++)
            {
                s[i] = Math.Max(s[i - 1], prices[i - 1] + b[i - 1]);
                b[i] = Math.Max(b[i - 1], s[i - 2] - prices[i-1]);
            }

            return Math.Max(s.Max(), b.Max());
        }

        public IList<string> LetterCombinations(string digits)
        {
            IList<string> res = new List<string>();
            LetterCombinations(digits, 0, string.Empty, res);
            return res;
        }

        public void LetterCombinations(string digits, int pos, string cur, IList<string> res)
        {
            if (pos == digits.Length)
            {
                if (!string.IsNullOrEmpty(cur)) res.Add(cur);
                return;
            }

            Dictionary<int, char[]> map = new Dictionary<int, char[]>();
            map.Add(2, new []{'a', 'b', 'c'});
            map.Add(3, new []{'d', 'e', 'f'});
            map.Add(4, new []{'g', 'h', 'i'});
            map.Add(5, new []{'j', 'k', 'l'});
            map.Add(6, new []{'m', 'n', 'o'});
            map.Add(7, new []{'p', 'q', 'r', 's'});
            map.Add(8, new []{'t', 'u', 'v'});
            map.Add(9, new []{'w', 'x', 'y', 'z'});

            int t = digits[pos] - '0';
            char[] arr = map.GetValueOrDefault(t, new char[0]);
            foreach (char c in arr)
            {
                LetterCombinations(digits, pos + 1, cur + c, res);
            }
        }

        public void SortColors(int[] nums)
        {
            int zeros = 0, ones = 0, twos = 0;
            foreach (int i in nums)
            {
                if (i == 0) zeros++;
                if (i == 1) ones++;
                if (i == 2) twos++;
            }

            int j = 0;
            while (j < nums.Length)
            {
                if (zeros > 0)
                {
                    nums[j] = 0;
                    j++;
                    zeros--;
                }
                else if (ones > 0)
                {
                    nums[j] = 1;
                    j++;
                    ones--;
                }
                else if (twos > 0)
                {
                    nums[j] = 2;
                    j++;
                    twos--;
                }
            }
        }

        public void SortColors2(int[] nums)
        {
            int s = 0, e = nums.Length - 1;
            for (int i = 0; i <= e; i++)
            {
                int temp;
                if (nums[i] == 0)
                {
                    temp = nums[s];
                    nums[s] = nums[i];
                    nums[i] = temp;
                    s++;
                }

                else if (nums[i] == 2)
                {
                    temp = nums[e];
                    nums[e] = nums[i];
                    nums[i] = temp;
                    e--;
                    i--;
                }
            }
        }

        public int SubarraySum(int[] nums, int k)
        {
            int count = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                int sum = 0;
                for (int j = i; j < nums.Length; j++)
                {
                    sum += nums[j];
                    if (sum == k) count++ ;
                }
            }

            return count;
        }
    }

    class Sorting
    {
        public static void Merge(int[] nums1, int m, int[] nums2, int n)
        {
            int pos = nums1.Length - 1;

            int i = m-1, j = n-1;
            while (i >= 0 && j >= 0)
            {
                if (nums2[j] > nums1[i])
                {
                    nums1[pos] = nums2[j];
                    j -= 1;
                }
                else
                {
                    nums1[pos] = nums1[i];
                    i -= 1;
                }

                pos -= 1;
            }

            if (i >= 0)
            {
                while (i >= 0)
                {
                    nums1[pos] = nums1[i];
                    i -= 1;
                    pos -= 1;
                }
            }

            if (j >= 0)
            {
                while (j >= 0)
                {
                    nums1[pos] = nums2[j];
                    j -= 1;
                    pos -= 1;
                }
            }
        }

        public int FirstBadVersion(int n)
        {
            int i=0, j =n, mid=0;
            while (i <= j)
            {
                mid = i + (j - i) / 2;
                if (!IsBadVersion(mid)) i = mid + 1;
                else j = mid - 1;
            }
            if (!IsBadVersion(mid)) return mid + 1;
            return mid;
        }

        public bool IsBadVersion(int l)
        {
            return true;
        }
    }

    public class MinStack
    {
        private Stack<int[]> stk;
        /** initialize your data structure here. */
        public MinStack()
        {
            stk = new Stack<int[]>();
        }

        public void Push(int x)
        {
            if (!stk.Any()) stk.Push(new []{x, x});
            else
            {
                int min = stk.Peek()[1];
                stk.Push(new []{x, Math.Min(min, x)});
            }
        }

        public void Pop()
        {
            stk.Pop();
        }

        public int Top()
        {
            if (stk.Any()) return stk.Peek()[0];
            return 0;
        }

        public int GetMin()
        {
            if (stk.Any()) return stk.Peek()[1];
            return 0;
        }
    }
}
