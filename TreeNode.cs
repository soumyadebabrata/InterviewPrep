using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonOA
{
    using System.Linq;

    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;

        public TreeNode() { }
        public TreeNode(int v)
        {
            val = v;
            left = null;
            right = null;
        }

        public void PrintPretty(string indent, bool last)
        {

            Console.Write(indent);
            if (last)
            {
                Console.Write("└─");
                indent += "  ";
            }
            else
            {
                Console.Write("├─");
                indent += "| ";
            }
            Console.WriteLine(val);

            var children = new List<TreeNode>();
            if (this.left != null)
                children.Add(this.left);
            if (this.right != null)
                children.Add(this.right);

            for (int i = 0; i < children.Count; i++)
                children[i].PrintPretty(indent, i == children.Count - 1);

        }
    }

    class Tree
    {
        public static KeyValuePair<int, int> TreeAvg(TreeNode root, out TreeNode resNode, out KeyValuePair<int, int> maxValue)
        {
            if (root == null)
            {
                resNode = null;
                maxValue = new KeyValuePair<int, int>(1, 0);
                return new KeyValuePair<int, int>(0,0);
            }

            KeyValuePair<int, int> leftRes = TreeAvg(root.left, out resNode, out maxValue);
            KeyValuePair<int, int> rightRes = TreeAvg(root.right, out resNode, out maxValue);

            KeyValuePair<int, int> res = new KeyValuePair<int, int>(Math.Max(leftRes.Key, rightRes.Key) + 1,
                leftRes.Value + rightRes.Value + root.val);

            if (maxValue.Value / maxValue.Key < res.Value / res.Key)
            {
                maxValue = new KeyValuePair<int, int>(res.Key, res.Value);
                resNode = root;
            }

            return res;
        }

        public static void PreOrder(TreeNode r)
        {
            if (r == null) return;

            Console.Write(r.val);
            PreOrder(r.left);
            PreOrder(r.right);
        }

        public static void InOrder(TreeNode r, List<bool> pos, int i)
        {
            if (r == null) return;

            InOrder(r.left, pos, 2*i + 1);
            Console.Write(r.val);
            pos[i] = true;
            InOrder(r.right, pos, 2*i + 2);
        }

        public IList<int> InorderTraversal(TreeNode root)
        {
            if (root == null) return new List<int>();

            List<int> res = new List<int>();
            res.AddRange(InorderTraversal(root.left));
            res.Add(root.val);
            res.AddRange(InorderTraversal(root.right));
            return res;
        }

        public static void PostOrder(TreeNode r)
        {
            if (r == null) return;

            PostOrder(r.left);
            PostOrder(r.right);
            Console.Write(r.val);
        }

        public static void LevelOrder(TreeNode r)
        {
            if (r == null) return;

            Queue<TreeNode> q = new Queue<TreeNode>();
            q.Enqueue(r);

            while (q.Any())
            {
                int size = q.Count;
                while (size > 0)
                {
                    TreeNode n = q.Dequeue();
                    Console.Write(n.val);
                    if (n.left != null)
                    {
                        q.Enqueue(n.left);
                    }
                    if (n.right != null)
                    {
                        q.Enqueue(n.right);
                    }

                    size -= 1;
                }

                Console.WriteLine();
            }
        }

        public IList<IList<int>> LevelOrderToList(TreeNode root)
        {
            IList<IList<int>> res = new List<IList<int>>();
            if (root == null) return res;

            Queue<TreeNode> q = new Queue<TreeNode>();
            q.Enqueue(root);

            while (q.Any())
            {
                int size = q.Count;
                List<int> lres = new List<int>();
                while (size > 0)
                {
                    TreeNode n = q.Dequeue();
                    lres.Add(n.val);
                    if (n.left != null)
                    {
                        q.Enqueue(n.left);
                    }
                    if (n.right != null)
                    {
                        q.Enqueue(n.right);
                    }

                    size -= 1;
                }

                res.Add(lres);
            }

            return res;
        }

        public static TreeNode NextNodeInSameLevel(TreeNode r, int v)
        {
            if (r == null) return null;

            Queue<TreeNode> q = new Queue<TreeNode>();
            q.Enqueue(r);
            while (q.Any())
            {
                int size = q.Count;
                while (size-- > 0)
                {
                    TreeNode n = q.Dequeue();
                    if (n.val == v)
                    {
                        return size == 0 ? null : q.Peek();
                    }

                    if (n.left != null) q.Enqueue(n.left);
                    if (n.right != null) q.Enqueue(n.right);
                }
            }

            return null;
        }

        public static void VerticalSum(TreeNode r, int vlevel, Dictionary<int, int> map)
        {
            if (r == null) return;

            int v = map.GetValueOrDefault(vlevel, 0);
            map[vlevel] = v + r.val;

            VerticalSum(r.left, vlevel - 1, map);
            VerticalSum(r.right, vlevel + 1, map);
        }

        public static void PrintVerticalSum(TreeNode r)
        {
            if (r == null) return;
            Dictionary<int, int> map = new Dictionary<int, int>();
            VerticalSum(r, 0, map);
            foreach (KeyValuePair<int, int> p in map)
            {
                Console.WriteLine($"{p.Key} -> {p.Value}");
            }
        }

        public static void PrintAllRootToLeaf(TreeNode r, string s)
        {
            if (r == null)
            {
                return;
            }

            s = s + r.val;
            if (r.left == null && r.right == null)
            {
                Console.WriteLine(s);
            }

            PrintAllRootToLeaf(r.left, s + "->");
            PrintAllRootToLeaf(r.right, s + "->");
        }

        public static void PrintAllAncestors(TreeNode r, int target, List<TreeNode> ancestor)
        {
            if (r == null) return;

            if (r.val == target)
            {
                IEnumerable<int> val = ancestor.Select(a => a.val);
                Console.WriteLine($"{string.Join(',', val)}");
            }

            ancestor.Add(r);
            PrintAllAncestors(r.left, target, ancestor);
            PrintAllAncestors(r.right, target, ancestor);
            ancestor.Remove(r);
        }

        public static int CalculateHeight(TreeNode r)
        {
            if (r == null) return 0;

            int v = Math.Max(CalculateHeight(r.left), CalculateHeight(r.right)) + 1;
            return v;
        }

        public static bool AreIdentical(TreeNode r1, TreeNode r2)
        {
            if (r1 == null && r2 == null) return true;
            else if (r1 == null || r2 == null) return false;
            else
            {
                return r1.val == r2.val &&
                       AreIdentical(r1.left, r2.left) &&
                       AreIdentical(r1.right, r2.right);
            }
        }

        public static bool AreSymmetric(TreeNode r1, TreeNode r2)
        {
            if (r1 == null && r2 == null) return true;
            else if (r1 == null || r2 == null) return false;
            else
            {
                return r1.val == r2.val &&
                       AreSymmetric(r1.left, r2.right) &&
                       AreSymmetric(r1.right, r2.left);
            }
        }

        public static TreeNode FindLCA(TreeNode r, TreeNode x, TreeNode y)
        {
            if (r == null) return null;

            bool isPresent = IsNodePresent(r, x) && IsNodePresent(r, y);

            if (isPresent)
            {
                bool left = IsNodePresent(r.left, x) && IsNodePresent(r.left, y);
                bool right = IsNodePresent(r.right, x) && IsNodePresent(r.right, y);

                if (left) return FindLCA(r.left, x, y);
                else if (right) return FindLCA(r.right, x, y);
                else return r;
            }
            else
            {
                return null;
            }
        }

        public static bool IsNodePresent(TreeNode r, TreeNode n)
        {
            if (r == null) return false;

            if (r == n) return true;

            return IsNodePresent(r.left, n) || IsNodePresent(r.right, n);
        }

        public static int ConvertToSumTree(TreeNode r)
        {
            if (r == null) return 0;

            if (r.left == null && r.right == null) return r.val;

            int lv = ConvertToSumTree(r.left);
            int rv = ConvertToSumTree(r.right);

            r.val = lv + rv;
            return  2 * r.val;
        }

        public static void ConvertToMirror(TreeNode r)
        {
            if (r == null) return;

            ConvertToMirror(r.left);
            ConvertToMirror(r.right);
            TreeNode temp = r.left;
            r.left = r.right;
            r.right = temp;
        }

        public static int GetParentAndHeight(TreeNode r, int val, out TreeNode p)
        {
            if (r == null)
            {
                p = null;
                return 0;
            }

            if (r.val == val)
            {
                p = null;
                return 1;
            }

            int lh = GetParentAndHeight(r.left, val, out p);
            int rh = GetParentAndHeight(r.right, val, out p);
            if (p == null) p = r;

            return Math.Max(lh, rh) + 1;

        }

        public static bool AreCousin(TreeNode r ,int a, int b)
        {
            TreeNode pa, pb;
            int ah = GetParentAndHeight(r, a, out pa);
            int bh = GetParentAndHeight(r, b, out pb);

            if (ah == bh && pa.val != pb.val)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string SpiralOrder(TreeNode r)
        {
            if (r == null) return string.Empty;

            Queue<TreeNode> q = new Queue<TreeNode>();
            bool order = true;
            StringBuilder result = new StringBuilder();
            q.Enqueue(r);

            while (q.Any())
            {
                string lv = "";
                int size = q.Count;
                while (size-- > 0)
                {
                    var n = q.Dequeue();
                    lv += n.val;
                    if (n.left != null) q.Enqueue(n.left);
                    if (n.right != null) q.Enqueue(n.right);
                }

                if (order)
                {
                    result.AppendLine(lv);
                    order = false;
                }
                else
                {
                    for (int i = lv.Length-1; i >= 0; i--)
                    {
                        result.Append(lv[i]);
                    }

                    result.AppendLine();

                    order = true;
                }
            }

            return result.ToString();
        }

        public static bool IsCompleteBinaryTree(TreeNode r)
        {
            if (r == null) return true;

            Queue<TreeNode> q = new Queue<TreeNode>();
            int size = 0;
            q.Enqueue(r);

            while (q.Any())
            {
                TreeNode n = q.Dequeue();
                size += 1;
                if (n.left != null) q.Enqueue(n.left);
                if (n.right != null) q.Enqueue(n.right);
            }
            bool[] pos = new bool[size];
            for (int i = 0; i < size; i++)
            {
                pos[i] = false;
            }
            //InOrder(r, pos, 0);
            foreach (bool p in pos)
            {
                if (!p) return false;
            }

            return true;
        }

        public bool IsValidBST(TreeNode root)
        {
            return IsValidBST(root, double.MinValue, double.MaxValue);
        }
        public bool IsValidBST(TreeNode root, double min, double max)
        {
            if (root == null) return true;

            return root.val > min &&
                   root.val < max &&
                   IsValidBST(root.left, min, root.val) &&
                   IsValidBST(root.right, root.val, max);
        }

        public TreeNode SortedArrayToBST(int[] nums)
        {
            if (nums.Length < 1) return null;
            return SortedArrayToBST(nums, 0, nums.Length - 1);
        }
        public TreeNode SortedArrayToBST(int[] nums, int l, int r)
        {
            if (l == r) return new TreeNode(nums[l]);

            int m = (r + l) / 2;
            TreeNode n = new TreeNode(nums[m]);
            n.left = SortedArrayToBST(nums, l, m - 1);
            n.right = SortedArrayToBST(nums, m + 1, r);

            return n;
        }

        public IList<int> RightSideView(TreeNode root)
        {
            List<int> res = new List<int>();
            if (root == null) return res;
            Queue<TreeNode> q = new Queue<TreeNode>();
            q.Enqueue(root);
            while (q.Any())
            {
                res.Add(q.Peek().val);
                int size = q.Count;
                for (int i = 0; i < size; i++)
                {
                    TreeNode t = q.Dequeue();
                    if (t.right != null) q.Enqueue(t.right);
                    if (t.left != null) q.Enqueue(t.left);
                }
            }

            return res;
        }

        public TreeNode MergeTrees(TreeNode t1, TreeNode t2)
        {
            if (t1 == null) return t2;
            if (t2 == null) return t1;

            TreeNode res = new TreeNode(t1.val + t2.val);
            res.left = MergeTrees(t1.left, t2.left);
            res.right = MergeTrees(t1.right, t2.right);
            return res;
        }

        public TreeNode InvertTree(TreeNode root)
        {
            if (root == null) return root;

            TreeNode temp = root.left;
            root.left = root.right;
            root.right = temp;

            InvertTree(root.left);
            InvertTree(root.right);

            return root;
        }

        public int Rob(TreeNode root)
        {
            if (root == null) return 0;

            int a = Rob(root.left) + Rob(root.right);
            int b = root.val;
            if (root.left != null) b = b + Rob(root.left.left) + Rob(root.left.right);
            if (root.right != null) b = b + Rob(root.right.left) + Rob(root.right.right);

            return Math.Max(a, b);
        }

        public void Flatten(TreeNode root)
        {
            root = FlattenRoot(root);
        }
        public TreeNode FlattenRoot(TreeNode root)
        {
            if (root == null) return null;

            TreeNode l = FlattenRoot(root.left);
            TreeNode temp = root.right;
            root.right = l;
            root.left = null;
            l = root;
            while (l.right != null) l = l.right;
            l.right = FlattenRoot(temp);
            return root;
        }

        public static TreeNode BuildTree(int[] preorder, ref int start, int[] inorder)
        {
            if (start == preorder.Length || !inorder.Any()) return null;

            int v = preorder[start];
            TreeNode n = new TreeNode(v);
            
            List<int> l = new List<int>();
            List<int> r = new List<int>();
            int i = 0;
            bool found = false;
            for (i = 0; i < inorder.Length; i++)
            {
                if (inorder[i] == v) found = true;
                else if (!found) l.Add(inorder[i]);
                else r.Add(inorder[i]);
            }

            if (l.Any())
            {
                start += 1;
                n.left = BuildTree(preorder, ref start, l.ToArray());
            }

            if (r.Any())
            {
                start += 1;
                n.right = BuildTree(preorder, ref start, r.ToArray());
            }
            return n;
        }
          
        public int DiameterOfBinaryTree(TreeNode root)
        {
            if (root == null) return 0;

            int a = CalculateHeight(root.left) + CalculateHeight(root.right);
            return Math.Max(a, Math.Max(DiameterOfBinaryTree(root.left), DiameterOfBinaryTree(root.right)));
        }

        public static int PathSum(TreeNode root, int sum)
        {
            Console.Write($"SUM --- {sum}");
            if (root == null)
            {
                Console.WriteLine("");
                return 0;
            }
            if (sum == root.val)
            {
                Console.WriteLine($"sum is {sum} and value is {root.val}");
                return 1;
            }

            int pathSumIncluding = PathSum(root.left, sum - root.val) + PathSum(root.right, sum - root.val);
            int pathSumExcluding = PathSum(root.left, sum) + PathSum(root.right, sum);
            return pathSumIncluding + pathSumExcluding;
        }

        // Encodes a tree to a single string.
        public static string serialize(TreeNode root)
        {
            if (root == null) return "#";

            string res = "" + root.val;
            res = res + "," + serialize(root.left);
            res = res + "," + serialize(root.right);

            return res;
        }

        // Decodes your encoded data to tree.
        public static TreeNode deserializeRef(ref Queue<string> data)
        {
            string s = data.Dequeue();

            if (s.Equals("#")) return null;
            TreeNode n = new TreeNode(int.Parse(s));
            n.left = deserializeRef(ref data);
            n.right = deserializeRef(ref data);

            return n;
        }

        public static TreeNode deserialize(string data)
        {
            Queue<string> q = new Queue<string>(data.Split(','));
            return deserializeRef(ref q);
        }

        public int MaxPathSum(TreeNode root)
        {
            int max = int.MinValue;
            int v = MaxPathSum(root, ref max);
            return Math.Max(v, max);
        }
        public int MaxPathSum(TreeNode root, ref int max)
        {
            if (root == null)
            {
                return 0;
            }

            int l = Math.Max(0, MaxPathSum(root.left, ref max));
            int r = Math.Max(0, MaxPathSum(root.right, ref max));
            
            max = Math.Max(max, Math.Max(root.val, root.val + l + r));
            return root.val + Math.Max(l, r);
        }

        public IList<TreeNode> DelNodes(TreeNode root, int[] to_delete)
        {
            HashSet<int> to_del = new HashSet<int>();
            foreach (int i in to_delete) to_del.Add(i);
            List<TreeNode> res = new List<TreeNode>();
            TreeNode r = DelNodesAndGetForest(root, to_del, ref res);
            if (r != null) res.Add(r);
            return res;
        }

        public TreeNode DelNodesAndGetForest(TreeNode root, HashSet<int> to_del, ref List<TreeNode> res)
        {
            if (root == null) return null;

            root.left = DelNodesAndGetForest(root.left, to_del, ref res);
            root.right = DelNodesAndGetForest(root.right, to_del, ref res);
            if (to_del.Contains(root.val))
            {
                if (root.left != null) res.Add(root.left);
                if (root.right != null) res.Add(root.right);

                return null;
            }

            return root;
        }

        public static double MaximumAverageSubtree(TreeNode root)
        {
            double res = double.MinValue;
            CalculateAvg(root, ref res);
            return res;
        }

        public static KeyValuePair<double, double> CalculateAvg(TreeNode r, ref double max)
        {
            if (r == null) return new KeyValuePair<double, double>(0, 0);

            KeyValuePair<double, double> lv = CalculateAvg(r.left, ref max);
            KeyValuePair<double, double> rv = CalculateAvg(r.right, ref max);

            KeyValuePair<double, double> ret = new KeyValuePair<double, double>(lv.Key + rv.Key + 1, lv.Value + rv.Value + r.val);

            if ((ret.Value / ret.Key) > max) max = (ret.Value / ret.Key);
            return ret;
        }

        #region CreateTree

        public static void Print(TreeNode node)
        {
            node.PrintPretty("", true);
        }

        public static TreeNode CreateTree1()
        {
            TreeNode tn1 = new TreeNode(){val = 1, left = null, right = null};
            TreeNode tn2 = new TreeNode(){val = 2, left = null, right = null};
            TreeNode tn3 = new TreeNode(){val = 4, left = null, right = null};
            TreeNode tn4 = new TreeNode(){val = -2, left = null, right = null};
            TreeNode tn5 = new TreeNode(){val = -5, left = tn1, right = tn2};
            TreeNode tn6 = new TreeNode(){val = 11, left = tn3, right = tn4};
            TreeNode tn7 = new TreeNode(){val = 1, left = tn5, right = tn6};
            Console.WriteLine("Original tree is:");
            Tree.Print(tn7);
            return tn7;
        }

        public static TreeNode CreateTree2()
        {
            TreeNode tn1 = new TreeNode(){val = 4, left = null, right = null};
            TreeNode tn2 = new TreeNode(){val = 7, left = null, right = null};
            TreeNode tn3 = new TreeNode(){val = 8, left = null, right = null};
            TreeNode tn4 = new TreeNode(){val = 6, left = null, right = null};

            TreeNode tn5 = new TreeNode(){val = 2, left = tn1, right = null};

            TreeNode tn6 = new TreeNode(){val = 5, left = tn2, right = tn3};

            TreeNode tn7 = new TreeNode(){val = 3, left = tn6, right = tn4};

            TreeNode tn8 = new TreeNode(){val = 1, left = tn5, right = tn7};
            Console.WriteLine("Original tree is:");
            Tree.Print(tn8);
            return tn8;
        }

        public static TreeNode CreateTree2(int a, int b, out TreeNode an, out TreeNode bn)
        {
            an = null; bn = null;
            Dictionary<int, TreeNode> m = new Dictionary<int, TreeNode>();
            TreeNode tn1 = new TreeNode(){val = 4, left = null, right = null};
            m[4] = tn1;
            TreeNode tn2 = new TreeNode(){val = 7, left = null, right = null};
            m[7] = tn2;
            TreeNode tn3 = new TreeNode(){val = 8, left = null, right = null};
            m[8] = tn3;
            TreeNode tn4 = new TreeNode(){val = 6, left = null, right = null};
            m[6] = tn4;
            
            TreeNode tn5 = new TreeNode(){val = 2, left = tn1, right = null};
            m[2] = tn5;
            
            TreeNode tn6 = new TreeNode(){val = 5, left = tn2, right = tn3};
            m[5] = tn6;
            
            TreeNode tn7 = new TreeNode(){val = 3, left = tn6, right = tn4};
            m[3] = tn7;
            
            TreeNode tn8 = new TreeNode(){val = 1, left = tn5, right = tn7};
            m[1] = tn8;

            an = m[a];
            bn = m[b];
            
            Console.WriteLine("Original tree is:");
            Tree.Print(tn8);
            return tn8;
        }

        public static TreeNode CreateTree3()
        {
            TreeNode tn1 = new TreeNode(){val = 8, left = null, right = null};
            TreeNode tn2 = new TreeNode(){val = 9, left = null, right = null};

            TreeNode tn3 = new TreeNode(){val = 4, left = null, right = null};
            TreeNode tn4 = new TreeNode(){val = 5, left = null, right = null};
            TreeNode tn5 = new TreeNode(){val = 6, left = tn1, right = null};
            TreeNode tn6 = new TreeNode(){val = 7, left = null, right = tn2};

            TreeNode tn7 = new TreeNode(){val = 2, left = tn3, right = tn4};
            TreeNode tn8 = new TreeNode(){val = 3, left = tn5, right = tn6};

            TreeNode tn9 = new TreeNode(){val = 1, left = tn7, right = tn8};
            Console.WriteLine("Original tree is:");
            Tree.Print(tn9);
            return tn9;
        }

        public static TreeNode CreateTree4()
        {
            TreeNode tn1 = new TreeNode(){val = 3, left = null, right = null};
            TreeNode tn2 = new TreeNode(){val = -2, left = null, right = null};
            TreeNode tn3 = new TreeNode(){val = 1, left = null, right = null};

            TreeNode tn4 = new TreeNode(){val = 3, left = tn1, right = tn2};
            TreeNode tn5 = new TreeNode(){val = 2, left = null, right = tn3};
            TreeNode tn6 = new TreeNode(){val = 11, left = null, right = null};

            TreeNode tn7 = new TreeNode(){val = 5, left = tn4, right = tn5};
            TreeNode tn8 = new TreeNode(){val = -3, left = null, right = tn6};

            TreeNode tn9 = new TreeNode(){val = 10, left = tn7, right = tn8};
            Console.WriteLine("Original tree is:");
            Tree.Print(tn9);
            return tn9;
        }

        public static TreeNode CreateBinCompTree1()
        {
            TreeNode tn1 = new TreeNode(){val = 4, left = null, right = null};
            TreeNode tn2 = new TreeNode(){val = 5, left = null, right = null};
            TreeNode tn3 = new TreeNode(){val = 6, left = null, right = null};
            TreeNode tn4 = new TreeNode(){val = 7, left = null, right = null};

            TreeNode tn5 = new TreeNode(){val = 2, left = tn1, right = tn2};
            TreeNode tn6 = new TreeNode(){val = 3, left = tn3, right = tn4};

            TreeNode tn7 = new TreeNode(){val = 1, left = tn5, right = tn6};
            Console.WriteLine("Original tree is:");
            Tree.Print(tn7);
            return tn7;
        }
        public static TreeNode CreateBinCompTree2()
        {
            TreeNode tn1 = new TreeNode(){val = 4, left = null, right = null};
            TreeNode tn2 = new TreeNode(){val = 5, left = null, right = null};
            TreeNode tn3 = new TreeNode(){val = 6, left = null, right = null};

            TreeNode tn5 = new TreeNode(){val = 2, left = tn1, right = tn2};
            TreeNode tn6 = new TreeNode(){val = 3, left = tn3, right = null};

            TreeNode tn7 = new TreeNode(){val = 1, left = tn5, right = tn6};
            Console.WriteLine("Original tree is:");
            Tree.Print(tn7);
            return tn7;
        }
        public static TreeNode CreateBinNonCompTree()
        {
            TreeNode tn1 = new TreeNode(){val = 4, left = null, right = null};
            TreeNode tn2 = new TreeNode(){val = 5, left = null, right = null};
            TreeNode tn3 = new TreeNode(){val = 7, left = null, right = null};

            TreeNode tn5 = new TreeNode(){val = 2, left = tn1, right = tn2};
            TreeNode tn6 = new TreeNode(){val = 3, left = null, right = tn3};

            TreeNode tn7 = new TreeNode(){val = 1, left = tn5, right = tn6};
            Console.WriteLine("Original tree is:");
            Tree.Print(tn7);
            return tn7;
        }
        #endregion
    }
}
