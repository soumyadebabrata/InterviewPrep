using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonOA
{
    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int x) { val = x; }
    }

    class LinkedList
    {
        public static void PrintListNode(ListNode l)
        {
            while (l != null)
            {
                Console.Write($"{l.val} ");
                l = l.next;
            }
            Console.WriteLine();
        }

        public static ListNode CreateList(int[] val)
        {
            ListNode head = new ListNode(val[0]);
            ListNode cur = head;
            for (int i = 1; i < val.Length; i++)
            {
                ListNode n = new ListNode(val[i]);
                cur.next = n;
                cur = cur.next;
            }

            return head;
        }
    }

    class MergeSortedLL
    {
        public static ListNode MergeTwoLists(ListNode l1, ListNode l2)
        {
            if (l1 == null && l2 == null)
            {
                return l1;
            }
            else if (l1 == null)
            {
                return l2;
            }
            else if (l2 == null)
            {
                return l1;
            }

            ListNode result = null;
            if (l1.val < l2.val)
            {
                result = new ListNode(l1.val);
                l1 = l1.next;
            }
            else
            {
                result = new ListNode(l2.val);
                l2 = l2.next;
            }
            ListNode cur = result;

            while (l1 != null && l2 != null)
            {
                ListNode node;
                if (l1.val < l2.val)
                {
                    node = new ListNode(l1.val);
                    l1 = l1.next;
                }
                else
                {
                    node = new ListNode(l2.val);
                    l2 = l2.next;
                }

                cur.next = node;
                cur = cur.next;
            }

            if (l1 != null)
            {
                while (l1 != null)
                {
                    ListNode node = new ListNode(l1.val);
                    l1 = l1.next;
                    cur.next = node;
                    cur = cur.next;
                }
            }

            if (l2 != null)
            {
                while (l2 != null)
                {
                    ListNode node = new ListNode(l2.val);
                    l2 = l2.next;
                    cur.next = node;
                    cur = cur.next;
                }
            }

            return result;
        }

        public static ListNode MergeTwoLists2(ListNode l1, ListNode l2)
        {
            if (l1 == null && l2 == null) return l1;
            if (l1 == null) return l2;
            if (l2 == null) return l1;

            ListNode res = null, cur = null;
            while (l1 != null && l2 != null)
            {
                if (res == null)
                {
                    if (l1.val <= l2.val)
                    {
                        res = l1;
                        l1 = l1.next;
                    }
                    else
                    {
                        res = l2;
                        l2 = l2.next;
                    }

                    cur = res;
                }
                else
                {
                    if (l1.val <= l2.val)
                    {
                        cur.next = l1;
                        l1 = l1.next;
                    }
                    else
                    {
                        cur.next = l2;
                        l2 = l2.next;
                    }

                    cur = cur.next;
                }
            }

            if (l1 != null) cur.next = l1;
            if (l2 != null) cur.next = l2;
            return res;
        }
    }

    class AddTwoNumbersLL
    {
        public static ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            if (l1 == null && l2 == null)
            {
                return l1;
            }
            else if (l1 == null)
            {
                return l2;
            }
            else if (l2 == null)
            {
                return l1;
            }

            ListNode result = new ListNode((l1.val + l2.val) % 10);
            int carry = (l1.val + l2.val) / 10;
            l1 = l1.next;
            l2 = l2.next;
            ListNode cur = result;

            while (l1 != null || l2 != null)
            {
                int a = l1 == null ? 0 : l1.val;
                int b = l2 == null ? 0 : l2.val;
                int v = a + b + carry;
                ListNode n = new ListNode(v % 10);
                carry = v / 10;
                cur.next = n;
                cur = cur.next;
                if (l1 != null) l1 = l1.next;
                if (l2 != null) l2 = l2.next;
            }

            if (carry != 0)
            {
                ListNode n = new ListNode(carry);
                cur.next = n;
            }

            return result;
        }
    }

    class OddEvenLL
    {
        public static ListNode OddEvenList(ListNode head)
        {
            if (head == null || head.next == null)
            {
                return head;
            }

            ListNode odd = head;
            ListNode even = odd.next;
            ListNode evenStart = even;

            while (odd != null && even != null)
            {
                odd.next = even.next;
                if (odd.next != null)
                {
                    odd = odd.next;
                }

                even.next = even.next != null ? even.next.next : null;
                even = even.next;
            }

            odd.next = evenStart;
            return head;
        }
    }

    class IntersectionofLL
    {
        public static ListNode GetIntersectionNode(ListNode headA, ListNode headB)
        {
            if (headA == null || headB == null)
            {
                return null;
            }

            ListNode runA = headA;
            ListNode runB = headB;

            while (runA.next != null)
            {
                runA = runA.next;
            }

            while (runB.next != null)
            {
                runB = runB.next;
            }

            if (runA != runB)
            {
                return null;
            }

            runA = headA;
            runB = headB;

            char dirA = 'a';
            char dirB = 'b';

            while (runA != runB)
            {
                runA = runA.next;
                runB = runB.next;

                if (runA == null)
                {
                    if (dirA == 'a')
                    {
                        runA = headB;
                        dirA = 'b';
                    }
                    else
                    {
                        runA = headA;
                        dirA = 'a';
                    }
                }

                if (runB == null)
                {
                    if (dirB == 'b')
                    {
                        runB = headA;
                        dirB = 'a';
                    }
                    else
                    {
                        runB = headB;
                        dirB = 'b';
                    }
                }
            }

            return runA;
        }

        public static ListNode GetIntersectionNode2(ListNode headA, ListNode headB)
        {
            if (headA == null || headB == null) return null;

            ListNode runA = headA;
            ListNode runB = headB;
            while (runA != null && runB != null)
            {
                runA = runA.next;
                runB = runB.next;
            }

            if (runA == null)
            {
                runA = headB;
                while (runB != null)
                {
                    runA = runA.next;
                    runB = runB.next;
                }

                runB = headA;
            }

            if (runB == null)
            {
                runB = headA;
                while (runA != null)
                {
                    runA = runA.next;
                    runB = runB.next;
                }

                runA = headB;
            }

            while (runA != null && runB != null && runA != runB)
            {
                runA = runA.next;
                runB = runB.next;
            }

            return runA;
        }
    }

    class ReverseLLinKGroup
    {
        public static ListNode ReverseKGroup(ListNode head, int k)
        {
            if (head == null || head.next == null || k < 2)
            {
                return head;
            }

            ListNode c = head, n = c, p = null;
            
            ListNode res = null;
            int i = 1;

            while (n != null)
            {
                if (i % k == 0)
                {
                    if (res == null)
                    {
                        res = Reverse(c, n);
                    }
                    else
                    {
                        p.next = Reverse(c, n);
                    }

                    p = c;
                    c = c.next;
                    n = c;
                    i++;
                }
                else
                {
                    n = n.next;
                    i++;
                }
            }

            if (res == null) res = head;
            return res;
        }

        public static ListNode Reverse(ListNode h, ListNode t)
        {
            if (h == null || h.next == null)
            {
                return h;
            }

            ListNode curTNxt = t.next;
            t.next = null;

            ListNode c = h;
            ListNode n = h.next;
            ListNode nn = n.next;

            while (nn != null)
            {
                n.next = c;
                c = n;
                n = nn;
                nn = nn.next;
            }

            n.next = c;
            h.next = curTNxt;
            return n;
        }
    }

    class CopyRandomLinkedList
    {
        public class Node
        {
            public int val;
            public Node next;
            public Node random;

            public Node()
            {
            }

            public Node(int _val, Node _next, Node _random)
            {
                val = _val;
                next = _next;
                random = _random;
            }
        }

        public static Node CopyRandomList(Node head)
        {
            if (head == null)
            {
                return head;
            }

            Dictionary<Node, Node> copyList = new Dictionary<Node, Node>();
            Node copyHead = new Node(head.val, null, null);
            copyList[head] = copyHead;

            Node curPtr = head;
            Node copyPtr = copyHead;

            while (curPtr.next != null)
            {
                Node next = curPtr.next;
                Node copyNode = new Node(next.val, null, null);
                copyPtr.next = copyNode;
                copyList[next] = copyNode;

                curPtr = curPtr.next;
                copyPtr = copyPtr.next;
            }

            curPtr = head;
            copyPtr = copyHead;

            while (curPtr != null)
            {
                Node curRand = curPtr.random;
                if (curRand != null)
                {
                    Node copyRand = copyList[curRand];
                    copyPtr.random = copyRand;
                }

                curPtr = curPtr.next;
                copyPtr = copyPtr.next;
            }

            return copyHead;
        }
    }
}
