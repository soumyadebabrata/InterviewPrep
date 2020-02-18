using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonOA
{
    using System.Linq;

    class DynamicProgrammingQuestions
    {
        public void Run()
        {
            // int[] arr = new[] {-2, 1, -3, 4, -1, 2, 1, -5, 4};
            int[] arr = new[] {-1};
            Console.WriteLine($"Max contiguous array sum is {MaxSubArray(arr)}");
            Console.WriteLine("_______________________________________________________");


            //arr = new[] {2, 1, 5, 6, 2, 3};
            arr = new[] {2, 1, 2};
            Console.WriteLine($"Max area is {LargestRectangleArea(arr)}");
            Console.WriteLine("_______________________________________________________");


            Console.WriteLine($"Ways to climb 3 stairs are {ClimbStairs(3)}");
            Console.WriteLine("_______________________________________________________");


            arr = new[] {1, 5, 8, 9, 10, 17, 17, 20};
            Console.WriteLine($"Max profit after cutting rod is {CuttingRod(arr)}");
            Console.WriteLine("_______________________________________________________");


            arr = new[] {3, 5, 8, 9, 10, 17, 17, 20};
            Console.WriteLine($"Max profit after cutting rod is {CuttingRod(arr)}");
            Console.WriteLine("_______________________________________________________");

            int[] money = new[] {2, 3, 5, 6};
            Console.WriteLine($"Ways to make change for 10 is {CoinChangeCount(money, 10)}");
            Console.WriteLine("_______________________________________________________");


            int []val = new int[]{60, 100, 120};
            int []wt = new int[]{10, 20, 30};
            int W = 50;
            Console.WriteLine($"Max value under weight 50 is {KnapSack(val, wt, W)}");
            Console.WriteLine("_______________________________________________________");


            arr = new[] {3, 34, 4, 12, 5, 2};
            Console.WriteLine($"Does subset exist with sum 9: {SubsetSumRecursion(arr, arr.Length, 9)}");
            Console.WriteLine("_______________________________________________________");


            arr = new[] {3, 1, 4, 2, 2, 1};
            Console.WriteLine($"Min difference in subset sum is {SubsetSumMinDiff(arr)}");
            Console.WriteLine("_______________________________________________________");


            Console.WriteLine($"Ways to cover distance 4 is {CoverDistance(4)}");
            Console.WriteLine("_______________________________________________________");


            string s1 = "sunday", s2 = "saturday";
            Console.WriteLine($"Edit distance between {s1} and {s2} is {Editdistance(s1, s2)}");
            Console.WriteLine("_______________________________________________________");


            s1 = "abcde"; s2 = "ace";
            int len = LongestCommonSubsequence(s1, s2, out string res);
            Console.WriteLine($"Longest comm subseq. of {s1} and {s2} is {res}");
            Console.WriteLine("_______________________________________________________");

        }

        public int Rob(int[] nums)
        {
            if (nums == null || nums.Length <= 0) return 0;
            if (nums.Length == 1) return nums[0];

            int[] dp = new int[nums.Length];
            dp[0] = nums[0];
            dp[1] = Math.Max(nums[0], nums[1]);
            for (int i = 2; i < nums.Length; i++)
            {
                dp[i] = Math.Max(nums[i] + dp[i - 2], dp[i - 1]);
            }

            return dp[nums.Length - 1];
        }

        public int MaxSubArray(int[] nums)
        {
            int maxSum = int.MinValue;
            int sumTillHere = 0;

            for (int i = 0; i < nums.Length; i++)
            {
                sumTillHere = sumTillHere + nums[i];
                maxSum = Math.Max(maxSum, sumTillHere);

                if (sumTillHere < 0)
                {   
                    sumTillHere = 0;
                }
            }

            return maxSum;
        }

        public int LargestRectangleArea(int[] heights)
        {
            Stack<int> s = new Stack<int>();
            int maxArea = 0;
            for (int i = 0; i < heights.Length; i++)
            {
                if (s.Count == 0 || heights[s.Peek()] <= heights[i])
                {
                    s.Push(i);
                }
                else
                {
                    while (s.Count > 0 && heights[s.Peek()] >= heights[i])
                    {
                        int pos = s.Pop();
                        int area = heights[pos] * (s.Count == 0 ? i : i - s.Peek() - 1);
                        maxArea = Math.Max(maxArea, area);
                    }

                    s.Push(i);
                }
            }

            while (s.Count > 0)
            {
                int pos = s.Pop();
                int area = heights[pos] * (s.Count == 0 ? heights.Length : heights.Length - s.Peek() - 1);
                maxArea = Math.Max(maxArea, area);
            }

            return maxArea;
        }

        public int ClimbStairs(int n)
        {
            if (n < 0) return 0;
            if (n == 1 || n == 0) return 1;
            
            int[] dp = new int[n+1];
            dp[0] = 1;
            dp[1] = 1;

            for (int i = 2; i <= n; i++)
            {
                dp[i] = dp[i - 1] + dp[i - 2];
            }

            return dp[n];
        }

        public int MaxProfit(int[] prices)
        {
            int n = prices.Length;
            if (n <= 1) return 0;
            int[] dp = new int[n];
            for (int i = 0; i < n; i++) dp[i] = 0;
            for (int i = 1; i < n; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (prices[j] < prices[i])
                    {
                        dp[i] = Math.Max(dp[i], prices[i] - prices[j]);
                    }
                }
            }

            return dp.Max();
        }

        public int CuttingRod(int[] prices)
        {
            int n = prices.Length;
            if (n <= 0) return 0;
            if (n == 1) return prices[0];

            int[] dp = new int[n+1];
            dp[0] = 0;
            for (int i = 1; i <= n; i++) dp[i] = prices[i-1];

            for (int i = 2; i <= n; i++)
            {
                for (int j = 1; j < i; j++)
                {
                    dp[i] = Math.Max(dp[i], dp[j] + dp[i - j]);
                }
            }

            return dp[n];
        }

        public int CoinChangeCount(int[] s, int n)
        {
            if (n == 0) return 1;
            if (n < 0) return 0;

            int[] dp = new int[n+1];
            dp[0] = 1;
            foreach (var t in s)
            {
                for (int j = t; j <= n; j++)
                {
                    dp[j] = dp[j] + dp[j - t];
                }
            }

            return dp[n];
        }

        public int LongestCommonSubsequence(string text1, string text2, out string res)
        {
            int m = text1.Length, n = text2.Length;
            string[,] dp = new String[m+1, n+1];
            for (int i = 0; i <= m; i++)
            {
                for (int j = 0; j <= n; j++)
                {
                    if (i == 0 || j == 0) dp[i, j] = string.Empty;
                    else if (text1[i - 1] == text2[j - 1])
                    {
                        dp[i, j] = dp[i - 1, j - 1] + text1[i - 1];
                    }
                    else
                    {
                        if (dp[i - 1, j].Length > dp[i, j - 1].Length) dp[i, j] = dp[i - 1, j];
                        else dp[i, j] = dp[i, j - 1];
                    }
                }
            }

            res = dp[m, n];
            return res.Length;
        }

        public string ShortestCommonSupersequence(string str1, string str2)
        {
            LongestCommonSubsequence(str1, str2, out string lcs);
            string res = string.Empty;
            int i = 0, j = 0;
            foreach (char c in lcs)
            {
                while (i < str1.Length && str1[i] != c) res += str1[i++];
                while (j < str2.Length && str2[j] != c) res += str2[j++];
                res += c;
                i += 1;
                j += 1;
            }

            if (i < str1.Length) res = res + str1.Substring(i);
            if (j < str2.Length) res = res + str2.Substring(j);

            return res;
        }

        public int LengthOfLIS(int[] nums)
        {
            if (nums.Length < 2) return nums.Length;

            int[] dp = new int[nums.Length];
            for (int i = 0; i < nums.Length; i++) dp[i] = 1;
            for (int i = 1; i < nums.Length; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (nums[i] > nums[j]) dp[i] = Math.Max(dp[i], dp[j] + 1);
                }
            }

            return dp.Max();
        }

        public int KnapSack(int[] vals, int[] wts, int W)
        {
            int n = vals.Length;
            int[,] dp = new int[n+1, W+1];
            for (int i = 0; i <= n; i++)
            {
                for (int j = 0; j <= W; j++)
                {
                    if (i == 0 || j == 0) dp[i, j] = 0;
                    else if (wts[i - 1] > j) dp[i, j] = dp[i - 1, j];
                    else
                    {
                        dp[i, j] = Math.Max(vals[i - 1] + dp[i - 1, j - wts[i - 1]], dp[i - 1, j]);
                    }
                }
            }

            return dp[n, W];
        }

        public bool CanPartition(int[] nums)
        {
            int total = 0;
            foreach (int i in nums) total = total + i;
            if (total % 2 != 0) return false;
            int sum = total / 2;
            bool[,] dp = new bool[sum + 1, nums.Length + 1];
            for (int i = 1; i <= sum; i++) dp[i, 0] = false;
            for (int j = 0; j <= nums.Length; j++) dp[0, j] = true;
            for (int i = 1; i <= sum; i++)
            {
                for (int j = 1; j <= nums.Length; j++)
                {
                    dp[i, j] = dp[i, j - 1];
                    if (i - nums[j-1] >= 0)
                    {
                        dp[i, j] = dp[i, j] || dp[i - nums[j - 1], j - 1];
                    }
                }
            }

            return dp[sum, nums.Length];
        }

        public bool SubsetSumRecursion(int[] nums, int n, int sum)
        {
            if (sum == 0) return true;
            if (n <= 0 && sum != 0) return false;

            return SubsetSumRecursion(nums, n - 1, sum) || SubsetSumRecursion(nums, n - 1, sum - nums[n - 1]);
        }

        public int SubsetSumMinDiffRecursion(int[] nums, int n, int calcSum, int totalSum)
        {
            if (n == 0) return Math.Abs(totalSum - 2*calcSum);

            return Math.Min(SubsetSumMinDiffRecursion(nums, n - 1, calcSum + nums[n - 1], totalSum),
                SubsetSumMinDiffRecursion(nums, n - 1, calcSum, totalSum));
        }

        public int SubsetSumMinDiff(int[] nums)
        {
            int n = nums.Length;
            int sum = 0;
            foreach (int i in nums) sum = sum + i;
            int tot = sum / 2;

            bool[,] dp = new bool[n+1,tot+1];
            for (int i = 0; i <= n; i++) dp[i, 0] = true;
            for (int j = 0; j <= tot; j++) dp[0, j] = false;

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= tot; j++)
                {
                    dp[i, j] = dp[i - 1, j];
                    if (j - nums[i - 1] >= 0) dp[i, j] = dp[i, j] || dp[i - 1, j - nums[i - 1]];
                }
            }

            int diff = 0;
            for (int j = tot; j >= 0; j--)
            {
                if (dp[n, j])
                {
                    diff = sum - 2 * j;
                    break;
                }
            }

            return diff;
        }

        public int CoverDistance(int n)
        {
            int[] dp = new int[n+1];
            dp[0] = 1;
            for (int i = 1; i <= n; i++)
            {
                dp[i] += dp[i - 1];
                if (i - 2 >= 0) dp[i] += dp[i - 2];
                if (i - 3 >= 0) dp[i] += dp[i - 3];
            }

            return dp[n];
        }

        public int Editdistance(string word1, string word2)
        {
            int m = word1.Length, n = word2.Length;
            int[,] dp = new int[m + 1, n + 1];
            for (int i = 0; i <= m; i++) dp[i, 0] = i;
            for (int j = 0; j <= n; j++) dp[0, j] = j;

            for (int i = 1; i <= m; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    if (word1[i - 1] == word2[j - 1]) dp[i, j] = dp[i - 1, j - 1];
                    else
                    {
                        dp[i, j] = 1 + Math.Min(dp[i - 1, j], Math.Min(dp[i, j - 1], dp[i - 1, j - 1]));
                    }
                }
            }

            return dp[m, n];
        }


    }
}
