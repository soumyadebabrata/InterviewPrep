using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonOA
{
    using System.Linq;

    class LinkedListQuestions
    {
        public void Run()
        {
            int[] vals = new[] {4, 5, 1, 9};
            ListNode h = CreateLinkedList(vals);
            PrintLinkedList(h);
            ListNode n = GetNodeAt(h, 3);
            Console.WriteLine($"Node at position 3 is: {(n == null ? "null" : n.val.ToString())}");
            Console.WriteLine("________________________________________________________________");

            DeleteNode(n);
            Console.WriteLine($"After deleting node {n.val}");
            PrintLinkedList(h);
            Console.WriteLine("________________________________________________________________");

            //vals = new[] {1, 2, 3, 4, 5};
            vals = new[] {1, 2, 3};
            h = CreateLinkedList(vals);
            PrintLinkedList(h);
            Console.WriteLine($"Deleting node at location 2");
            h = RemoveNthFromEnd(h, 3);
            PrintLinkedList(h);
            Console.WriteLine("________________________________________________________________");

            vals = new[] { 1, 2, 3, 4, 5 };
            h = CreateLinkedList(vals);
            PrintLinkedList(h);
            h = ReverseList(h);
            PrintLinkedList(h);
            Console.WriteLine("________________________________________________________________");

            vals = new[] {1, 2, 3, 2, 1};
            h = CreateLinkedList(vals);
            Console.WriteLine($"Is LL {string.Join("->", vals)} Palindrome: {IsPalindrome(h)}");
            Console.WriteLine("________________________________________________________________");

            ListNode n1 = new ListNode(3);
            ListNode n2 = new ListNode(2);
            ListNode n3 = new ListNode(0);
            ListNode n4 = new ListNode(-4);

            n1.next = n2;
            n2.next = n3;
            n3.next = n4;
            n4.next = n2;
            ListNode cyc = DetectCycle(n1);
            Console.WriteLine($"cycle detected at {cyc.val}");
            Console.WriteLine("________________________________________________________________");
        }

        public void PrintLinkedList(ListNode node)
        {
            while (node != null)
            {
                Console.Write(node.val);
                if (node.next != null) Console.Write("->");
                node = node.next;
            }
            Console.WriteLine();
        }

        public ListNode CreateLinkedList(int[] vals)
        {
            if (vals.Length < 1) return null;

            ListNode head = new ListNode(vals[0]);
            ListNode cur = head;
            for (int i = 1; i < vals.Length; i++)
            {
                ListNode node = new ListNode(vals[i]);
                cur.next = node;
                cur = cur.next;
            }

            return head;
        }

        public ListNode GetNodeAt(ListNode head, int pos)
        {
            if (head == null) return null;

            ListNode res = head;
            while (--pos > 0 && res != null)
            {
                res = res.next;
            }

            return res;
        }

        public void DeleteNode2(ListNode node)
        {
            if (node == null) return;

            ListNode next = node.next;
            ListNode prev = null;
            while (next != null)
            {
                node.val = next.val;
                prev = node;
                node = next;
                next = node.next;
            }

            if (prev != null) prev.next = null;
        }

        public void DeleteNode(ListNode node)
        {
            if (node == null) return;

            ListNode n = node.next;
            if (n == null) return;

            node.val = n.val;
            node.next = n.next;
            n.next = null;
        }

        public ListNode RemoveNthFromEnd(ListNode head, int n)
        {
            if (head == null) return null;

            ListNode next = head;
            while (--n> 0 && next != null)
            {
                next = next.next;
            }

            if (next == null) return null;

            ListNode prev = null;
            ListNode cur = head;
            while (next.next != null)
            {
                prev = cur;
                cur = cur.next;
                next = next.next;
            }

            if (prev == null) return head.next;
            else
            {
                prev.next = cur.next;
                cur.next = null;
                return head;
            }
        }

        public ListNode ReverseList2(ListNode head)
        {
            if (head == null || head.next == null) return head;

            ListNode prev = null;
            ListNode cur = head;
            ListNode next = head.next;

            while (next != null)
            {
                cur.next = prev;
                prev = cur;
                cur = next;
                next = next.next;
            }

            cur.next = prev;

            return cur;
        }

        public ListNode ReverseList(ListNode head)
        {
            if (head == null || head.next == null) return head;

            ListNode prev = null, cur = head, n = cur.next;
            while (n != null)
            {
                cur.next = prev;
                prev = cur;
                cur = n;
                n = n.next;
            }

            cur.next = prev;
            return cur;
        }

        public bool HasCycle(ListNode head)
        {
            if (head == null || head.next == null) return false;

            ListNode s = head, f = head;
            while (s != null && f != null && f.next != null)
            {
                s = s.next;
                f = f.next.next;

                if (s == f) return true;
            }

            return false;
        }

        public bool IsPalindrome(ListNode head)
        {
            Stack<int> st = new Stack<int>();
            ListNode cur = head;
            while (cur != null)
            {
                st.Push(cur.val);
                cur = cur.next;
            }

            cur = head;
            while (cur != null)
            {
                if (cur.val != st.Pop()) return false;
                cur = cur.next;
            }

            return st.Count == 0;
        }

        public ListNode DetectCycle(ListNode head)
        {
            if (head == null || head.next == null) return null;

            ListNode s = head;
            ListNode f = head;

            while (f != null && f.next != null)
            {
                s = s.next;
                f = f.next.next;
                if (s == f) break;
            }

            if (f == null || s != f) return null;
            s = head;
            while (s != f)
            {
                s = s.next;
                f = f.next;
            }

            return s;
        }

        public int CoinChangeRecurse(int[] coins, int amount)
        {
            if (coins.Length == 0 || amount < coins.Min()) return -1;

            if (coins.Contains(amount)) return 1;

            int min = int.MaxValue;
            for (int i = 0; i < coins.Length; i++)
            {
                int c = CoinChangeRecurse(coins, amount - coins[i]);
                if (c != -1) min = Math.Min(min, c + 1);
            }

            return min == int.MaxValue ? -1 : min;
        }

        public int CoinChange(int[] coins, int amount)
        {
            if (coins.Length <= 0 || amount < coins.Min()) return -1;

            int[] dp = new int[amount + 1];
            dp[0] = 0;
            for (int i = 1; i <= amount; i++) dp[i] = 10000;
            for (int i = 1; i <= amount; i++)
            {
                foreach (int coin in coins)
                {
                    if (i >= coin)
                    {
                        dp[i] = Math.Min(dp[i], 1 + dp[i - coin]);
                    }
                }
            }

            return dp[amount] == 10000 ? -1 : dp[amount];
        }

        public bool CanJump(int[] nums)
        {
            int n = nums.Length;
            if (n <= 0) return false;

            bool[] v = new bool[n];
            v[n - 1] = true;
            for (int i = n-2; i >= 0; i--)
            {
                int j = nums[i];
                for (int k = i+1; k < n && k < i+j+1; k++)
                {
                    v[i] = v[i] || v[k];
                }
            }

            return v[0];
        }

        public ListNode MergeKLists(ListNode[] lists)
        {
            if (!lists.Any()) return null;

            Queue<ListNode> q = new Queue<ListNode>(lists);
            while (q.Count > 1)
            {
                ListNode a = q.Dequeue();
                ListNode b = q.Dequeue();
                q.Enqueue(MergeSortedLL(a, b));
            }

            return q.Dequeue();
        }

        public ListNode MergeSortedLL(ListNode a, ListNode b)
        {
            if (a == null) return b;
            if (b == null) return a;

            ListNode res = null;
            if (a.val <= b.val)
            {
                res = a;
                a = a.next;
            }
            else
            {
                res = b;
                b = b.next;
            }

            ListNode cur = res;
            while (a != null && b != null)
            {
                if (a.val <= b.val)
                {
                    cur.next = a;
                    a = a.next;
                }
                else
                {
                    cur.next = b;
                    b = b.next;
                }

                cur = cur.next;
            }

            while (a != null)
            {
                cur.next = a;
                a = a.next;
                cur = cur.next;
            }

            while (b != null)
            {
                cur.next = b;
                b = b.next;
                cur = cur.next;
            }

            return res;
        }
    }
}
