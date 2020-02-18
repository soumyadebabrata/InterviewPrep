using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonOA
{
    class SubArrWithKDIstInt
    {
        public static int SubarraysWithKDistinct(int[] A, int K)
        {
            if (A.Length < K)
            {
                return 0;
            }

            int sol = 0;
            for (int i = 0; i < A.Length; i++)
            {
                Dictionary<int, int> d = new Dictionary<int, int>();
                int diff = 0;
                for (int j = i; j < A.Length; j++)
                {
                    if (d.ContainsKey(A[j]))
                    {
                        d[A[j]] += 1;
                    }
                    else
                    {
                        diff += 1;
                        d[A[j]] = 1;
                    }

                    if (diff > K)
                    {
                        break;
                    }
                    if (diff == K)
                    {
                        sol += 1;
                        for (int k = i; k <= j; k++)
                        {
                            Console.Write($"{A[k]} ");
                        }
                        Console.WriteLine();
                    }
                }
            }

            return sol;
        }
    }
}
