using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonOA
{
    using System.Linq;

    class StringQuestions
    {
        public void Run()
        {
            #region Count and Say number.

            Console.WriteLine($"Count and say 4 is {CountAndSay(4)}");

            #endregion

            Console.WriteLine("_______________________________________________________");

            #region String to Integer (atoi)
            Console.WriteLine($"converting  -6147483648 to {MyAtoi("-6147483648")}");
            #endregion

            Console.WriteLine("_______________________________________________________");

            #region Implement strStr()
            Console.WriteLine($"Location of ll in hello is {StrStr("hello", "ll")}");
            #endregion

            Console.WriteLine("_______________________________________________________");

            #region Valid Palindrome Considering Only Case-Insensitive AlphaNumeric characters
            Console.WriteLine($"\"A man, a plan, a canal: Panama\" is a palindrome ? : {IsPalindrome("A man, a plan, a canal: Panama")}");
            #endregion

            Console.WriteLine("_______________________________________________________");
        }

        public string CountAndSay(int n)
        {
            if (n < 1) return string.Empty;
            if (n == 1) return "1";

            string res = CountAndSay(n - 1);
            return Say(res);
        }

        public string Say(string res)
        {
            StringBuilder sb = new StringBuilder();
            int c = 1;
            for (int i = 1; i < res.Length; i++)
            {
                if (res[i] == res[i-1])
                {
                    c++;
                }
                else
                {
                    sb.Append(c);
                    c = 1;
                    sb.Append(res[i - 1]);
                }
            }

            sb.Append(c);
            sb.Append(res[res.Length - 1]);

            return sb.ToString();
        }

        public int MyAtoi(string str)
        {
            if (str == null || str.Trim().Length < 1) return 0;
            str = str.Trim();
            
            int i = 0, j;
            bool isNeg = false;
            if (str[i] == '+' || str[i] == '-')
            {
                isNeg = str[i] == '-';
                i = 1;
            }
            
            for (j = i; j < str.Length; j++)
            {
                if (!char.IsNumber(str[j])) break;
            }

            if (j == i) return 0;

            str = str.Substring(i, j - i);

            double sum = 0;
            foreach (char c in str)
            {
                sum = (sum * 10) + (c - '0');
            }

            if (isNeg)
            {
                sum = sum * -1;
            }

            if (sum > int.MaxValue) return int.MaxValue;
            if (sum < int.MinValue) return int.MinValue;

            return (int) sum;
        }

        public int StrStr(string haystack, string needle)
        {
            if (needle.Length <= 0)
            {
                return 0;
            }
            int res = -1;
            for (int i = 0; i < haystack.Length; i++)
            {
                if (haystack.Length - i < needle.Length)
                {
                    break;
                }

                int k = 0;
                for (int j = i; j < i + needle.Length; j++)
                {
                    if (haystack[j] == needle[k]) k++;
                    else break;
                }

                if (k == needle.Length)
                {
                    res = i;
                    break;
                }
            }

            return res;
        }

        public string LongestCommonPrefix(string[] strs)
        {
            if (strs.Length < 1) return string.Empty;
            return LongestCommonPrefix(strs, 0, strs.Length - 1);
        }

        public string LongestCommonPrefix(string[] strs, int l, int r)
        {
            if (l > r) return null;

            if (l == r) return strs[l];

            int m = (r + l) / 2;
            string lprefix = LongestCommonPrefix(strs, l, m);
            string rprefix = LongestCommonPrefix(strs, m+1, r);

            return CommonPrefix(lprefix, rprefix);
        }

        public string CommonPrefix(string a, string b)
        {
            if (a.Length == 0 || b.Length == 0) return string.Empty;

            int i, j;
            for (i = 0, j = 0; i < a.Length && j < b.Length; i++, j++)
            {
                if (a[i] != b[j]) break;
            }

            if (i == 0) return string.Empty;
            else return a.Substring(0, i);
        }

        public bool IsPalindrome(string s)
        {
            if (s.Length < 2) return true;

            int i = 0, j = s.Length -1;
            while (i <= j)
            {
                if (!char.IsLetterOrDigit(s[i]))
                {
                    i++;
                }
                else if (!char.IsLetterOrDigit(s[j]))
                {
                    j--;
                }
                else
                {
                    if (char.ToLower(s[i]) != char.ToLower(s[j])) break;
                    else
                    {
                        i++;
                        j--;
                    }
                }
            }

            return i > j;
        }

        public string LongestPalindrome(string s)
        {
            if (s.Length < 2) return s;

            string res = string.Empty;

            for (int i = 0; i < s.Length; i++)
            {
                string ss1 = ExpandAroundCenter(s, i, i);
                string ss2 = ExpandAroundCenter(s, i, i+1);

                if (ss1.Length > res.Length) res = ss1;
                if (ss2.Length > res.Length) res = ss2;
            }

            return res;
        }

        public string ExpandAroundCenter(string s, int l, int r)
        {
            while (l >= 0 && r < s.Length && s[l] == s[r])
            {
                l--;
                r++;
            }

            return s.Substring(l + 1, r - l - 1);
        }

        public int LengthOfLIS(int[] nums)
        {
            if (nums.Length < 2) return nums.Length;

            int[] res = new int[nums.Length];
            for (int i = 0; i < res.Length; i++)
            {
                res[i] = 1;
            }

            for (int i = 1; i < nums.Length; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (nums[j] < nums[i]) res[i] = Math.Max(res[i], res[j] + 1);
                }
            }

            return res.Max();
        }
    }
}
