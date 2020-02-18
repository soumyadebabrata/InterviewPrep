using System;
using System.Collections.Generic;
using System.Text;

namespace AmazonOA
{
    using System.Linq;

    class Graph
    {
        public int v;

        public List<List<int>> adjList;

        public Graph(int n)
        {
            this.v = n;
            adjList = new List<List<int>>();
            for (int i = 0; i < v; i++)
            {
                adjList[i] = (new List<int>());
            }
        }

        public Graph(int n, IList<IList<int>> connections)
        {
            this.v = n;
            this.adjList = new List<List<int>>(v);
            for (int i = 0; i < v; i++)
            {
                adjList.Add(new List<int>());
            }

            foreach (IList<int> connection in connections)
            {
                AddConnection(connection[0], connection[1]);
            }
        }

        public void Show()
        {
            int i = 0;
            foreach (List<int> conn in adjList)
            {
                Console.WriteLine($"{i} -- {string.Join(',', conn)}");

                i += 1;
            }
        }

        public void AddConnection(int a, int b)
        {
            adjList[a].Add(b);
            adjList[b].Add(a);
        }

        public void RemoveConnection(int a, int b)
        {
            adjList[a].Remove(b);
            adjList[b].Remove(a);
        }

        public int GetConnectedComponents()
        {
            int numComponents = 0;
            bool[] visited = new bool[v];
            for (int i = 0; i < v; i++)
            {
                visited[i] = false;
            }

            for (int i = 0; i < v; i++)
            {
                Queue<int> q = new Queue<int>();
                if (!visited[i])
                {
                    numComponents += 1;
                    q.Enqueue(i);
                    visited[i] = true;
                }

                while (q.Any())
                {
                    int cur = q.Dequeue();
                    List<int> set = adjList[cur];
                    foreach (int i1 in set)
                    {
                        if (!visited[i1])
                        {
                            visited[i1] = true;
                            q.Enqueue(i1);
                        }
                    }
                }
            }

            return numComponents;
        }
    }
}
