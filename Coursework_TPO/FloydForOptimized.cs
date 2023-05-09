using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework_TPO
{
    class FloydForOptimized
    {
        public static int[,] ShortestPathsParallel(int[,] graph)
        {
            int n = graph.GetLength(0);
            int[,] dist = new int[n, n];

            Parallel.For(0, n, i =>
            {
                for (int j = 0; j < n; j++)
                {
                    dist[i, j] = graph[i, j];
                }
            });

            Parallel.For(0, n, new ParallelOptions { MaxDegreeOfParallelism = 3 }, k =>
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (dist[i, k] != int.MaxValue && dist[k, j] != int.MaxValue && dist[i, j] > dist[i, k] + dist[k, j])
                        {
                            dist[i, j] = dist[i, k] + dist[k, j];
                        }
                    }
                }
            });

            return dist;
        }
    }
}
