using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonOA
{
    class MinPathMatrix
    {
        public int MinPathSum(int[][] grid)
        {
            int rn = grid.Length;
            int cn = grid[0].Length;

            int[][] res = new int[rn][];

            for (int i = 0; i < rn; i++)
            {
                res[i] = new int[cn];
                for (int j = 0; j < cn; j++)
                {
                    res[i][j] = 0;
                }
            }

            for (int i = rn - 1; i >= 0; i--)
            {
                for (int j = cn - 1; j >= 0 ; j--)
                {
                    int r = i + 1, c = j + 1;
                    if (r >= rn && c >= cn)
                    {
                        res[i][j] = grid[i][j];
                    }
                    else if (r >= rn)
                    {
                        res[i][j] = res[i][c] + grid[i][j];
                    }
                    else if (c >= cn)
                    {
                        res[i][j] = res[r][j] + grid[i][j];
                    }
                    else
                    {
                        res[i][j] = Math.Min(res[i][c], res[r][j]) + grid[i][j];
                    }
                }
            }

            return res[0][0];
        }
    }
}
