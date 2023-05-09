using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coursework_TPO
{
    class Floyd
    {
        private int[,] graph;
        private int verticesCount;

        public Floyd(int[,] adjacencyMatrix, int verticesCount)
        {
            this.graph = adjacencyMatrix;
            this.verticesCount = verticesCount;
        }

        public int[,] ShortestPath()
        {
            int[,] dist = new int[verticesCount, verticesCount];
            int i, j, k;

            for (i = 0; i < verticesCount; i++)
            {
                for (j = 0; j < verticesCount; j++)
                {
                    dist[i, j] = graph[i, j];
                }
            }

            for (k = 0; k < verticesCount; k++)
            {
                for (i = 0; i < verticesCount; i++)
                {
                    for (j = 0; j < verticesCount; j++)
                    {
                        if (dist[i, k] + dist[k, j] < dist[i, j])
                        {
                            dist[i, j] = dist[i, k] + dist[k, j];
                        }
                    }
                }
            }

            return dist;
        }
    }
}
