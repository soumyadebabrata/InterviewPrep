using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonOA
{
    using System.Linq;

    class Program
    {
        public static void Main()
        {
            Console.WriteLine("0. GetTopNCompetitiors");
            Console.WriteLine("1. RottingOranges");
            Console.WriteLine("2. ReorderLogs");
            Console.WriteLine("3. CriticalConnection");
            Console.WriteLine("4. OptimalUtilization");
            Console.WriteLine("5. IslandCount");
            Console.WriteLine("6. TwoSum");
            Console.WriteLine("7. ProdSuggestion");
            Console.WriteLine("8. TreasureIsland");
            Console.WriteLine("9. MinCostRopeConnect");
            Console.WriteLine("10. 2dMatrixSearch");
            Console.WriteLine("11. MinPathTo0");
            Console.WriteLine("12. DungeonGame");
            Console.WriteLine("13. KClosetPoints");
            Console.WriteLine("14. MergeSortedLL");
            Console.WriteLine("15. SubArrWithKDIstInt");
            Console.WriteLine("16. PathCountInGrid");
            Console.WriteLine("17. LongestPalindromeSubstring");
            Console.WriteLine("18. MostCommonWods");
            Console.WriteLine("19. LIPMatrix");
            Console.WriteLine("20. MinCostToRepairEdges");
            Console.WriteLine("21. PrisonAfterDayN");
            Console.WriteLine("22. PartitionLabel");
            Console.WriteLine("23. KthSmallestInSortedMatrix");
            Console.WriteLine("24. MaxAverageTree");
            Console.WriteLine("25. MergeIntervals");
            Console.WriteLine("26. LongestIncreasingSubsequence");
            Console.WriteLine("27. LongestVowelString");
            Console.WriteLine("28. ReorganizeString");
            Console.WriteLine("29. LongestSubStrWithDifferent3ConsesCharacters");
            Console.WriteLine("30. ThreeSum0");
            Console.WriteLine("31. SetMetixTo0");
            Console.WriteLine("32. LongestSubstringWithoutRepeatingCharacters");
            Console.WriteLine("33. IncreasingTripletSubsequence");
            Console.WriteLine("34. GroupAnagram");
            Console.WriteLine("35. AddTwoNumbers");
            Console.WriteLine("36. OddEvenLL");
            Console.WriteLine("37. GetIntersectionNode");
            Console.WriteLine("38. ReverseLLinKGroup");
            Console.WriteLine("39. LevelOrderTraversal");
            Console.WriteLine("40. NextNodeInSameLevel");
            Console.WriteLine("41. PrintVerticalSum");
            Console.WriteLine("42. PrintAllRootToLeaf");
            Console.WriteLine("43. PrintAllAncestors");
            Console.WriteLine("44. AreBinaryTreesIdentical");
            Console.WriteLine("45. FindLCA");
            Console.WriteLine("46. ConvertToSumTree");
            Console.WriteLine("47. ConvertToMirrorTree");
            Console.WriteLine("48. AreCousins");
            Console.WriteLine("49. SpiralOrderTraversal");
            Console.WriteLine("50. IsCompleteBinaryTree");
            Console.WriteLine("51. CreateBinaryTreeFromPreorderAndInorder");
            Console.WriteLine("52. ArrayQuestions");
            Console.WriteLine("53. StringQuestions");
            Console.WriteLine("54. LinkedListQuestions");
            Console.WriteLine("55. Others");
            Console.WriteLine("56. ConcurrentProgrammingQuestions");
            Console.WriteLine("57. DynamicProgrammingQuestions");
            Console.WriteLine("58. TreePathSum");
            Console.WriteLine("59. OtherQuestions2");
            Console.WriteLine("60. SerializeSeserializeTree");
            Console.WriteLine("61. GoogleQuestions");
            Console.WriteLine("62. FacebookQuestions");
            Console.WriteLine("63. RANDOMSTUFF");
            Console.WriteLine("64. FacebookQuestions2");
            Console.Write("Enter choice : ");
            int choice = Int32.Parse(Console.ReadLine() ?? throw new InvalidOperationException());
            Run(choice);
        }

        private static void Run(int choice)
        {
            switch (choice)
            {
                case 0:
                    #region GetTopNCompetitors

                    int numCompetitors = 6;
                    int topNCompetitors = 2;
                    List<string> competitors = new List<string>()
                        {"newshop", "shopnow", "afshion", "fashionbeats", "mymarket", "tcellular"};

                    List<string> reviews = new List<string>()
                    {
                        "newshop is afshion providing good services in the city; everyone should use newshop",
                        "best services by newshop",
                        "fashionbeats has great services in the city",
                        "i am proud to have fashionbeats",
                        "mymarket has awesome services",
                        "Thanks Newshop for the quick delivery afshion"
                    };

                    List<string> topCompetitors = TopNCompetitors.GetTopCompetitors(numCompetitors, topNCompetitors, competitors, reviews);
                    Console.WriteLine(string.Join(',', topCompetitors));

                    #endregion
                    break;

                case 1:
                    #region RottingOragnes

                    int[][] grid = new int[3][];
                    grid[0] = new[] { 2, 1, 1 };
                    grid[1] = new[] { 1, 1, 0 };
                    grid[2] = new[] { 0, 1, 1 };

                    int rottingTime = OrangesRotting.ProcessOrangeRotting(grid);
                    Console.WriteLine($"Total time to rot = {rottingTime}");

                    #endregion
                    break;

                case 2:
                    #region ReorderLogs

                    string[] logs = new[] {"dig1 8 1 5 1", "let1 art can", "dig2 3 6", "let2 own kit dig", "let3 art zero"};
                    string[] sortedLogs = ReorderLog.ReorderLogFiles(logs);
                    foreach (string s in sortedLogs)
                    {
                        Console.WriteLine(s);
                    }

                    #endregion
                    break;

                case 3:
                    #region CriticalConnection
                    IList<IList<int>> list = new List<IList<int>>();
                    list.Add(new List<int>() { 0, 1 });
                    list.Add(new List<int>() { 1, 2 });
                    list.Add(new List<int>() { 2, 0 });
                    list.Add(new List<int>() { 1, 3 });

                    IList<IList<int>> result = CriticalConnection.CriticalConnections(4, list);
                    foreach (var ints in result)
                    {
                        var l = (List<int>) ints;
                        Console.WriteLine($"{l[0]},{l[1]}");
                    }
                    #endregion
                    break;

                case 4:
                    #region OptimalUtilization
                    List<KeyValuePair<int, int>> a = new List<KeyValuePair<int, int>>();
                    List<KeyValuePair<int, int>> b = new List<KeyValuePair<int, int>>();

                    a.Add(new KeyValuePair<int, int>(1,3));
                    a.Add(new KeyValuePair<int, int>(2,5));
                    a.Add(new KeyValuePair<int, int>(3,7));
                    a.Add(new KeyValuePair<int, int>(4,10));

                    b.Add(new KeyValuePair<int, int>(1, 2));
                    b.Add(new KeyValuePair<int, int>(2, 3));
                    b.Add(new KeyValuePair<int, int>(3, 4));
                    b.Add(new KeyValuePair<int, int>(4, 5));

                    List<KeyValuePair<int, int>> res = OptimalUtilization.GetOptimization(a, b, 10);
                    foreach (KeyValuePair<int, int> pair in res)
                    {
                        Console.WriteLine($"{pair.Key},{pair.Value}");
                    }

                    Console.WriteLine("--------------------------------------------------------");

                    a = new List<KeyValuePair<int, int>>();
                    b = new List<KeyValuePair<int, int>>();

                    a.Add(new KeyValuePair<int, int>(1, 8));
                    a.Add(new KeyValuePair<int, int>(2, 15));
                    a.Add(new KeyValuePair<int, int>(3, 9));

                    b.Add(new KeyValuePair<int, int>(1, 8));
                    b.Add(new KeyValuePair<int, int>(2, 11));
                    b.Add(new KeyValuePair<int, int>(3, 12));

                    res = OptimalUtilization.GetOptimization(a, b, 20);
                    foreach (KeyValuePair<int, int> pair in res)
                    {
                        Console.WriteLine($"{pair.Key},{pair.Value}");
                    }
                    #endregion
                    break;

                case 5:
                    #region IslandCount
                    char[][] grid1 = new char[4][];
                    grid1[0] = new[] { '1', '1', '1', '1', '0' };
                    grid1[1] = new[] { '1', '1', '0', '1', '0' };
                    grid1[2] = new[] { '1', '1', '0', '0', '0' };
                    grid1[3] = new[] { '0', '0', '0', '0', '0' };

                    Console.WriteLine($"Num islands : {IslandsInGrid.NumIslands(grid1)}");

                    Console.WriteLine("---------------------------------------");

                    grid1 = new char[4][];
                    grid1[0] = new[] { '1', '1', '0', '0', '0' };
                    grid1[1] = new[] { '1', '1', '0', '0', '0' };
                    grid1[2] = new[] { '0', '0', '1', '0', '0' };
                    grid1[3] = new[] { '0', '0', '0', '1', '1' };

                    Console.WriteLine($"Num islands : {IslandsInGrid.NumIslands(grid1)}");
                    #endregion
                    break;

                case 6:
                    #region TwoSum

                    int[] sum = new[] {3, 2, 4};
                    int[] res1 = TwoSumFinder.TwoSum(sum, 6);
                    foreach (int v in res1)
                    {
                        Console.Write($"{v} ");
                    }
                    #endregion
                    break;

                case 7:
                    #region ProdSuggestion
                    Trie t = new Trie(new List<string>(){"mobile", "mouse", "moneypot", "monitor", "mousepad"});
                    Console.Write("Enter search string : ");
                    string pre = Console.ReadLine();
                    if (t.Search(pre, out var res3))
                    {
                        Console.WriteLine(string.Join(',', res3));
                    }
                    else
                    {
                        Console.WriteLine("Search not found");
                    }
                    #endregion
                    break;

                case 8:
                    #region TreasureIsland
                    char[][] grid2 = new char[4][];
                    grid2[0] = new[] {'O', 'O', 'O', 'O'};
                    grid2[1] = new[] {'D', 'O', 'D', 'O'};
                    grid2[2] = new[] {'O', 'O', 'O', 'O'};
                    grid2[3] = new[] {'X', 'D', 'D', 'O'};

                    Console.WriteLine($"Min cost = {TreasureIsland.MinStep(grid2)}");
                    #endregion
                    break;

                case 9:
                    #region MinCostRopeConnect
                    int[] dat = new[] {20, 4, 8, 2};
                    MinHeap h = new MinHeap(dat.ToList());
                    int cost = 0;
                    while (h.count > 1)
                    {
                        int a1 = h.Pop();
                        Console.Write($"a:{a1} ");
                        int b1 = h.Pop();
                        Console.Write($"b:{b1} ");
                        cost = cost + a1 + b1;
                        Console.Write($"cost:{cost} ");
                        h.Insert(a1 + b1);
                        Console.WriteLine($"insert:{a1 + b1} ");
                    }
                    Console.WriteLine($"Min cost is {cost}");
                    #endregion
                    break;

                case 10:
                    #region 2DMetrixSearch

                    int[,] array = new int[5,5]
                    {
                        {1,   4,  7, 11, 15 },
                        {2,   5,  8, 12, 19 },
                        {3,   6,  9, 16, 22 },
                        {10, 13, 14, 17, 24 },
                        {18, 21, 23, 26, 30 }
                    };
                    Console.WriteLine($"Find 5: {Search2DGrid.SearchMatrix(array, 5)}");

                    array = new int[,]
                    {
                        {-5}
                    };

                    Console.WriteLine($"Find 5: {Search2DGrid.SearchMatrix(array, 5)}");
                    #endregion
                    break;

                case 11:
                    #region MinPathTo0

                    int[][] mat = new int[3][];
                    mat[0] = new[] {0, 0, 0};
                    mat[1] = new[] {0, 1, 0};
                    mat[2] = new[] {1, 1, 1};

                    int[][] res2 = MinDistanceTo0.UpdateMatrix(mat);
                    for (int i = 0; i < res2.Length; i++)
                    {
                        for (int j = 0; j < res2[i].Length; j++)
                        {
                            Console.Write($"{res2[i][j]} ");
                        }
                        Console.WriteLine();
                    }
                    #endregion
                    break;

                case 12:
                    #region Dungeon
                    mat = new int[2][];
                    mat[0] = new[] { 3, -20, 30 };
                    mat[1] = new[] { -3, 4, 0 };

                    int req = DungeonGame.CalculateMinimumHP(mat);
                    Console.WriteLine($"min required value {req}");
                    #endregion
                    break;

                case 13:
                    #region KClosetPoints
                    int[][] p = new int[3][];
                    p[0] = new[] {3, 3};
                    p[1] = new[] {5, -1};
                    p[2] = new[] {-2, 4};

                    int[][] res4 = KClosestPointToOrigin.KClosest(p, 2);
                    foreach (int[] r in res4)
                    {
                        Console.WriteLine(string.Join(' ', r));
                    }

                    #endregion
                    break;

                case 14:
                    #region MergeSortedLL

                    ListNode n1 = LinkedList.CreateList(new[] {1, 2, 4});
                    ListNode n2 = LinkedList.CreateList(new[] {1, 3, 4});
                    ListNode s1 = MergeSortedLL.MergeTwoLists2(n1, n2);
                    LinkedList.PrintListNode(s1);
                    #endregion
                    break;

                case 15:
                    #region SubArrWithKDIstInt
                    int[] A = new[] {1, 2, 1, 2, 3};
                    Console.WriteLine($"Total sets: {SubArrWithKDIstInt.SubarraysWithKDistinct(A, 2)}");
                    #endregion
                    break;

                case 16:
                    #region PathCountInGrid
                    Console.WriteLine($"Path count in a 7X3 grid is {UniquePathInGrid.UniquePaths(7, 3)}");
                    #endregion
                    break;

                case 17:
                    #region LongestPalindromeSubstring

                    string s2 = "babad";
                    string res5 = LongestPalindromeSubstring.LongestPalindrome(s2);
                    Console.WriteLine($"Longest palindrome is {res5}");
                    #endregion
                    break;

                case 18:
                    #region MostCommonWods

                    string[] banned = new[] {"bob","hit"};
                    string res6 =
                        MostCommonWods.MostCommonWord("Bob. hIt, baLl",
                            banned);
                    Console.WriteLine($"Most common word: {res6}");
                    #endregion
                    break;

                case 19:
                    #region LIPMatrix
                    mat = new int[3][];
                    mat[0] = new[] {9, 9, 4};
                    mat[1] = new[] {6, 6, 8};
                    mat[2] = new[] {2, 1, 1};

                    Console.WriteLine($"Longest increasing path is {LongestIncreasingPathInMatrx.LongestIncreasingPath(mat)}");
                    #endregion
                    break;

                case 20:
                    #region MinCostToRepairEdges
                    int[][] edges = new int[7][];
                    edges[0] = new[] {1, 2};
                    edges[1] = new[] {2,3};
                    edges[2] = new[] {4,5};
                    edges[3] = new[] {5,6};
                    edges[4] = new[] {1,5};
                    edges[5] = new[] {2,4};
                    edges[6] = new[] {3,4};

                    int[][] costs = new int[3][];
                    costs[0] = new[] {1, 5, 110};
                    costs[1] = new[] {2, 4, 84};
                    costs[2] = new[] {3, 4, 79};

                    int repaircost = MinCostToRepairEdges.CalculateCost(6, edges, costs);
                    Console.WriteLine($"Repair cost is {repaircost}");
                    #endregion
                    break;

                case 21:
                    #region PrisonAfterDayN

                    int[] cells = new[] {0, 1, 0, 1, 1, 0, 0, 1};
                    int[] res7 = PrisonAfterDayN.PrisonAfterNDays(cells, 7);
                    #endregion
                    break;

                case 22:
                    #region PartitionLabel
                    string S = "ababcbacadefegdehijhklij";
                    List<int> res8 = PartitionLabel.PartitionLabels(S).ToList();
                    Console.WriteLine($"{string.Join(',', res8)}");
                    #endregion
                    break;

                case 23:
                    #region KthSmallestInSortedMatrix
                    int[][] matrix = new int[3][];
                    matrix[0] = new[] {1, 5, 9};
                    matrix[1] = new[] {10, 11, 13};
                    matrix[2] = new[] {12, 13, 15};
                    Console.WriteLine($"8th smallest number: {KthSmallestInSortedMatrix.kthSmallest(matrix, 8)}");
                    Console.WriteLine($"3rd smallest number: {KthSmallestInSortedMatrix.kthSmallest(matrix, 3)}");
                    #endregion
                    break;

                case 24:
                    #region MaxAverageTree

                    TreeNode t1 = Tree.CreateTree1();
                    Tree.TreeAvg(t1, out TreeNode resNode, out var maxValue);
                    Console.WriteLine("Max average tree is:");
                    Tree.Print(resNode);

                    TreeNode tn1 = new TreeNode(6);
                    TreeNode tn2 = new TreeNode(1);
                    TreeNode tn3 = new TreeNode(5);
                    tn3.left = tn1;
                    tn3.right = tn2;
                    double ress12 = Tree.MaximumAverageSubtree(tn3);
                    #endregion
                    break;

                case 25:
                    #region MergeIntervals
                    mat = new int[4][];
                    mat[0] = new[] {1, 3};
                    mat[1] = new[] {2, 6};
                    mat[2] = new[] {8, 10};
                    mat[3] = new[] {15, 18};
                    int[][] res10 = MergeIntervals.Merge(mat);
                    foreach (int[] i in res10)
                    {
                        Console.WriteLine($"{string.Join(',',i)}");
                    }
                    #endregion
                    break;

                case 26:
                    #region LongestIncreasingSubsequence
                    int[] arr = new[] {10, 9, 2, 5, 3, 7, 101, 18};
                    Console.WriteLine($"Max increasing subsequence length is {LongestIncreasingSubsequence.LengthOfLIS(arr)}");
                    #endregion
                    break;

                case 27:
                    #region LongestVowelString

                    string sr = "earthproblem";
                    Console.WriteLine($"Max vowel length is {LongestVowelString.GetLength(sr)}");
                    sr = "letsgosomewhere";
                    Console.WriteLine($"Max vowel length is {LongestVowelString.GetLength(sr)}");
                    #endregion
                    break;

                case 28:
                    #region ReorganizeString

                    string str = "abbabbaaab";
                    Console.WriteLine($"string after reorganisation is {ReorganizingString.ReorganizeString(str)}");

                    int[] arr1 = new[] {7, 7, 7, 8, 5, 7, 5, 5, 5, 8};
                    int[] res12 = ReorganizingString.RearrangeBarcodes(arr1);
                    Console.WriteLine($"Result after reorg {string.Join(',', res12)}");
                    #endregion
                    break;

                case 29:
                    #region LongestSubStrWithDifferent3ConsesCharacters

                    S = "baaabbabbb";
                    Console.WriteLine($"Longest sub string with no 3 same consecutive character is {LongestSubStrWithDifferent3ConsesCharacters.FindLength(S)}");
                    S = "babba";
                    Console.WriteLine($"Longest sub string with no 3 same consecutive character is {LongestSubStrWithDifferent3ConsesCharacters.FindLength(S)}");
                    #endregion
                    break;

                case 30:
                    #region ThreeSum0

                    int[] nums = new[] {-1, 0, 1, 2, -1, -4};
                    IList<IList<int>> res13 = Find3Sum.ThreeSum(nums);
                    foreach (IList<int> r in res13)
                    {
                        Console.WriteLine($"{string.Join(',',r)}");
                    }

                    nums = new[] {0, 0, 0};
                    res13 = Find3Sum.ThreeSum(nums);
                    foreach (IList<int> r in res13)
                    {
                        Console.WriteLine($"{string.Join(',', r)}");
                    }
                    #endregion
                    break;

                case 31:
                    #region SetMetixTo0
                    mat = new int[3][];
                    mat[0] = new[] {0, 1, 2, 0};
                    mat[1] = new[] {3, 4, 5, 2};
                    mat[2] = new[] {1, 3, 1, 5};
                    foreach (int[] arr2 in mat)
                    {
                        Console.WriteLine($"{string.Join(' ',arr2)}");
                    }
                    Console.WriteLine();
                    SetMetixTo0.SetZeroes(mat);
                    foreach (int[] arr2 in mat)
                    {
                        Console.WriteLine($"{string.Join(' ', arr2)}");
                    }

                    #endregion
                    break;

                case 32:
                    #region LongestSubstringWithoutRepeatingCharacters
                    string str1 = "pwwkew";
                    Console.WriteLine(
                        $"Length of longest substring without repeating char is {LongestSubstringWithoutRepeatingCharacters.LengthOfLongestSubstring(str1)}");
                    #endregion
                    break;

                case 33:
                    #region IncreasingTripletSubsequence

                    arr = new[] {1, 2, 3, 4, 5};
                    Console.WriteLine($"Does have increasing triplet sequence {IncreasingTripletSubsequence.IncreasingTriplet(arr)}");
                    #endregion
                    break;

                case 34:
                    #region GroupAnagram

                    string[] strarr = new[] {"eat", "tea", "tan", "ate", "nat", "bat"};
                    foreach (IList<string> slist in GroupAnagram.GroupAnagrams(strarr))
                    {
                        Console.WriteLine($"{string.Join(',', slist)}");
                    }
                    #endregion
                    break;

                case 35:
                    #region AddTwoNumbers

                    n1 = LinkedList.CreateList(new[] {2, 4, 3});
                    n2 = LinkedList.CreateList(new[] {5, 6, 4});
                    s1 = AddTwoNumbersLL.AddTwoNumbers(n1, n2);
                    LinkedList.PrintListNode(s1);
                    #endregion
                    break;

                case 36:
                    #region OddEvenLL

                    n1 = LinkedList.CreateList(new[] {1, 2, 3, 4, 5});
                    n1 = OddEvenLL.OddEvenList(n1);
                    LinkedList.PrintListNode(n1);

                    n1 = LinkedList.CreateList(new[] {2, 1, 3, 5, 6, 4, 7});
                    n1 = OddEvenLL.OddEvenList(n1);
                    LinkedList.PrintListNode(n1);
                    #endregion
                    break;

                case 37:
                    #region GetIntersectionNode

                    n1 = LinkedList.CreateList(new[] {4, 1, 8, 4, 5});
                    ListNode n3 = n1.next.next;
                    n2 = LinkedList.CreateList(new[] {5, 0, 1});
                    ListNode n4 = n2.next.next;
                    n4.next = n3;
                    LinkedList.PrintListNode(n1);
                    LinkedList.PrintListNode(n2);
                    ListNode n5 = IntersectionofLL.GetIntersectionNode2(n1, n2);
                    Console.Write($"Intersection is");
                    LinkedList.PrintListNode(n5);
                    #endregion
                    break;

                case 38:
                    #region ReverseLLinKGroup

                    n1 = LinkedList.CreateList(new[] {1, 2, 3, 4, 5});
                    s1 = ReverseLLinKGroup.ReverseKGroup(n1, 2);
                    LinkedList.PrintListNode(s1);

                    n1 = LinkedList.CreateList(new[] { 1, 2, 3, 4, 5 });
                    s1 = ReverseLLinKGroup.ReverseKGroup(n1, 3);
                    LinkedList.PrintListNode(s1);
                    #endregion
                    break;

                case 39:
                    #region LevelOrderTraversal
                    t1 = Tree.CreateTree2();
                    Console.WriteLine("Level order traversal");
                    Tree.LevelOrder(t1);
                    #endregion
                    break;

                case 40:
                    #region NextNodeInSameLevel

                    t1 = Tree.CreateTree2();
                    Console.WriteLine($"Next node of 4 is {Tree.NextNodeInSameLevel(t1, 4)?.val}");
                    Console.WriteLine($"Next node of 5 is {Tree.NextNodeInSameLevel(t1, 5)?.val}");
                    Console.WriteLine($"Next node of 6 is {Tree.NextNodeInSameLevel(t1, 6)?.val}");
                    #endregion
                    break;

                case 41:
                    #region PrintVerticalSum

                    t1 = Tree.CreateTree2();
                    Tree.PrintVerticalSum(t1);
                    #endregion
                    break;

                case 42:
                    #region PrintAllRootToLeaf

                    t1 = Tree.CreateTree2();
                    Tree.PrintAllRootToLeaf(t1, "");
                    #endregion
                    break;

                case 43:
                    #region PrintAllAncestors

                    t1 = Tree.CreateTree3();
                    Tree.PrintAllAncestors(t1, 9, new List<TreeNode>());
                    #endregion
                    break;

                case 44:
                    #region AreBinaryTreesIdentical

                    t1 = Tree.CreateBinCompTree1();
                    TreeNode t2 = Tree.CreateBinCompTree1();
                    Console.WriteLine($"{Tree.AreIdentical(t1, t2)}");
                    #endregion
                    break;

                case 45:
                    #region FindLCA

                    t1 = Tree.CreateTree2(6, 7, out t2, out TreeNode t3);
                    Console.WriteLine($"LCA of {t2.val}, {t3.val} is {Tree.FindLCA(t1, t2, t3).val}");
                    #endregion
                    break;

                case 46:
                    #region ConvertToSumTree

                    t1 = Tree.CreateBinCompTree1();
                    Tree.ConvertToSumTree(t1);
                    Tree.Print(t1);
                    #endregion
                    break;

                case 47:
                    #region ConvertToMirrorTree

                    t1 = Tree.CreateBinCompTree1();
                    Tree.ConvertToMirror(t1);
                    Tree.Print(t1);
                    #endregion

                    break;

                case 48:
                    #region AreCousins

                    t1 = Tree.CreateBinCompTree1();
                    Console.WriteLine($"Are 4,5 cousins: {Tree.AreCousin(t1, 4, 5)}");
                    Console.WriteLine($"Are 4,7 cousins: {Tree.AreCousin(t1, 4, 7)}");
                    #endregion

                    break;

                case 49:
                    #region SpiralOrderTraversal

                    t1 = Tree.CreateBinCompTree1();
                    Console.WriteLine($"Spiral order traversal : {Tree.SpiralOrder(t1)}");
                    #endregion

                    break;

                case 50:
                    #region IsCompleteBinaryTree

                    t1 = Tree.CreateBinCompTree1();
                    Console.WriteLine($"Is complete : {Tree.IsCompleteBinaryTree(t1)}");
                    t2 = Tree.CreateBinNonCompTree();
                    Console.WriteLine($"Is complete : {Tree.IsCompleteBinaryTree(t2)}");
                    #endregion

                    break;

                case 51:
                    int[] po = new[] {3, 1, 2, 4};
                    int[] io = new[] {1, 2, 3, 4};
                    int start = 0;
                    t1 = Tree.BuildTree(po, ref start, io);
                    Tree.Print(t1);
                    break;

                case 52:
                    ArrayQuestions aq = new ArrayQuestions();
                    aq.Run();
                    break;

                case 53:
                    StringQuestions sq = new StringQuestions();
                    sq.Run();
                    break;

                case 54:
                    LinkedListQuestions llq = new LinkedListQuestions();
                    llq.Run();
                    break;

                case 55:
                    Others ot = new Others();
                    ot.Run();
                    break;

                case 56:
                    ConcurrentProgrammingQuestions cpq = new ConcurrentProgrammingQuestions();
                    cpq.Run();
                    break;

                case 57:
                    DynamicProgrammingQuestions dpq = new DynamicProgrammingQuestions();
                    dpq.Run();
                    break;

                case 58:
                    t1 = Tree.CreateTree4();
                    int resSum = Tree.PathSum(t1, 8);
                    Console.WriteLine($"total path with sum 8 is {resSum}");
                    break;

                case 59:
                    OtherQuestions2 ot2 = new OtherQuestions2();
                    ot2.Run();
                    break;

                case 60:
                    t1 = Tree.CreateTree1();
                    string ser = Tree.serialize(t1);
                    Console.WriteLine($"Serialized tree: {ser}");
                    TreeNode dt1 = Tree.deserialize(ser);
                    Tree.Print(dt1);
                    break;

                case 61:
                    GoogleQuestions gq = new GoogleQuestions();
                    gq.Run();
                    break;

                case 62:
                    FacebookQuestions fq = new FacebookQuestions();
                    fq.Run();
                    break;

                case 63:
                    Run1();
                    break;

                case 64:
                    FacebookQuestions2 fbq2 = new FacebookQuestions2();
                    fbq2.Run();
                    break;

                default:
                    throw new InvalidOperationException("No choice match found");
            }
        }

        private static void Run1()
        {
            uint n = 43261596;
            uint res = 0;
            Console.WriteLine($"n = {ToBinString(n)}");
            while (n > 0)
            {
                if ((n & 1) != 1) res = res ^ 1;
                n = n >> 1;
                res = res << 1;
            }

            Console.WriteLine($"res = {ToBinString(res)}");
            Console.WriteLine($"n = {ToBinString(n)}");
            Console.WriteLine("-------------------------");
        }

        public static string ToBinString(uint n)
        {
            return Convert.ToString(n, 2).PadLeft(32, '0');
        }
    }
}
