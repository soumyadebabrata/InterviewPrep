using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonOA
{
    class ArrayQuestions
    {
        public void Run()
        {
            #region Remove Duplicates from Sorted Array

            int[] nums = new[] {1, 1, 2};
            Console.WriteLine($"New length is {RemoveDuplicates(nums)}");
            nums = new[] {0, 0, 1, 1, 1, 2, 2, 3, 3, 4};
            Console.WriteLine($"New length is {RemoveDuplicates(nums)}");

            #endregion

            Console.WriteLine("_______________________________________________________");

            #region Best Time to Buy and Sell Stock II

            int[] prices = new[] {7, 1, 5, 3, 6, 4};
            Console.WriteLine($"Max price is {MaxProfit(prices)}");

            prices = new[] {7, 6, 4, 3, 1};
            Console.WriteLine($"Max price is {MaxProfit(prices)}");

            #endregion

            Console.WriteLine("_______________________________________________________");

            #region Rotate Array By K Steps

            nums = new[] {1, 2, 3, 4, 5, 6, 7};
            Rotate(nums, 3);
            Console.WriteLine($"After 3 rotation {string.Join(',', nums)}");

            #endregion

            Console.WriteLine("_______________________________________________________");

            #region Indices Of Two Numbers Adding up to a specific target.
            nums = new[] {2, 7, 11, 15};
            int[] indices = TwoSum(nums, 9);
            Console.WriteLine($"Indices of numbers adding up to 9 are {string.Join(',', indices)}");
            #endregion

            Console.WriteLine("_______________________________________________________");

            #region Add one to the integer represented by array

            nums = new[] {9};
            int[] res = PlusOne(nums);
            Console.WriteLine($"Number after adding 1 is {string.Join(',',res)}");
            #endregion

            Console.WriteLine("_______________________________________________________");

            #region Rotate image 90 degrees clock wise.

            int[][] m = new int[3][];
            m[0] = new[] {1, 2, 3};
            m[1] = new[] {4, 5, 6};
            m[2] = new[] {7, 8, 9};

            foreach (int[] i in m)
            {
                Console.WriteLine($"{string.Join(',',i)}");
            }

            Console.WriteLine("After 90 degree clock wise rotation");
            Rotate90Clockwise(m);
            #endregion

            Console.WriteLine("_______________________________________________________");

            #region Rotate image 90 degrees anti-clock wise.

            m = new int[3][];
            m[0] = new[] { 1, 2, 3 };
            m[1] = new[] { 4, 5, 6 };
            m[2] = new[] { 7, 8, 9 };

            foreach (int[] i in m)
            {
                Console.WriteLine($"{string.Join(',', i)}");
            }

            Console.WriteLine("After 90 degree anti-clock wise rotation");
            Rotate90AntiClockwise(m);
            #endregion

            Console.WriteLine("_______________________________________________________");
        }

        public int RemoveDuplicates(int[] nums)
        {
            if (nums.Length < 2) return nums.Length;

            int i = 0, j = 0;
            while (j < nums.Length)
            {
                if (nums[i] == nums[j]) j++;
                else
                {
                    i += 1;
                    nums[i] = nums[j];
                    j++;
                }
            }

            return i + 1;
        }

        public int MaxProfit(int[] prices)
        {
            int maxProit = 0;
            for (int i = 1; i < prices.Length; i++)
            {
                if (prices[i] > prices[i-1])
                {
                    maxProit += (prices[i] - prices[i - 1]);
                }
            }

            return maxProit;
        }

        public void Rotate(int[] nums, int k)
        {
            k = k % nums.Length;
            /*Rotate(nums, 0, nums.Length - 1);
            Rotate(nums, 0, k - 1);
            Rotate(nums, k, nums.Length - 1);*/

            Rotate(nums, 0, nums.Length-k-1);
            Rotate(nums, k+1, nums.Length-1);
            Rotate(nums, 0, nums.Length-1);
        }

        public int[] TwoSum(int[] nums, int target)
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

        public int[] PlusOne(int[] digits)
        {
            int c = 1;
            for (int i = digits.Length - 1; i >= 0; i--)
            {
                int sum = digits[i] + c;
                digits[i] = sum % 10;
                c = sum / 10;
            }

            if (c != 0)
            {
                int[] res = new int[digits.Length + 1];
                res[0] = c;
                digits.CopyTo(res, 1);
                return res;
            }

            return digits;
        }

        public void Rotate90Clockwise(int[][] matrix)
        {
            if (matrix.Length == 0 ||
                (matrix.Length == 1 && matrix[0].Length == 1))
            {
                return;
            }

            TransposeMatrix(matrix, matrix.Length, matrix[0].Length);
            ReverseRow(matrix, matrix.Length, matrix[0].Length);
            foreach (int[] i in matrix)
            {
                Console.WriteLine($"{string.Join(',', i)}");
            }
        }

        public void Rotate90AntiClockwise(int[][] matrix)
        {
            if (matrix.Length == 0 ||
                (matrix.Length == 1 && matrix[0].Length == 1))
            {
                return;
            }

            TransposeMatrix(matrix, matrix.Length, matrix[0].Length);
            ReverseCol(matrix, matrix.Length, matrix[0].Length);
            foreach (int[] i in matrix)
            {
                Console.WriteLine($"{string.Join(',', i)}");
            }
        }

        private void Rotate(int[] nums, int i, int j)
        {
            while (i < j)
            {
                int temp = nums[i];
                nums[i] = nums[j];
                nums[j] = temp;

                i++;
                j--;
            }
        }

        private void TransposeMatrix(int[][] m, int R, int C)
        {
            for (int i = 0; i < R; i++)
            {
                for (int j = i; j < C; j++)
                {
                    int t = m[i][j];
                    m[i][j] = m[j][i];
                    m[j][i] = t;
                }
            }
        }

        private void ReverseCol(int[][] m, int R, int C)
        {
            for (int i = 0; i < C; i++)
            {
                for (int j = 0, k = R -1; j < k; j++, k--)
                {
                    int t = m[j][i];
                    m[j][i] = m[k][i];
                    m[k][i] = t;
                }
            }
        }

        private void ReverseRow(int[][] m, int R, int C)
        {
            for (int i = 0; i < R; i++)
            {
                for (int j = 0, k = C - 1; j < k; j++, k--)
                {
                    int t = m[i][j];
                    m[i][j] = m[i][k];
                    m[i][k] = t;
                }
            }
        }

    }
}
