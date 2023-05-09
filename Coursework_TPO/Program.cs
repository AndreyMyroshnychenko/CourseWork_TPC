using System;
using System.Threading.Tasks;
using Coursework_TPO;
class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            int verticlesCount = 0;
            bool isValid = false;

            while (!isValid)
            {
                Console.Write("\nEnter number of graph verticles: ");

                try
                {
                    verticlesCount = int.Parse(Console.ReadLine());

                    if (verticlesCount <= 0)
                    {
                        Console.WriteLine("Number of graph verticles should be positive");
                    }
                    else
                    {
                        isValid = true;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Number of graph verticles should be integer");
                }
            }

            int[,] adjacencyMatrix = new int[verticlesCount, verticlesCount];

            Random rnd = new Random();
            for (int i = 0; i < verticlesCount; i++)
            {
                for (int j = 0; j < verticlesCount; j++)
                {
                    if (i == j)
                    {
                        adjacencyMatrix[i, j] = 0;
                    }
                    else
                    {
                        adjacencyMatrix[i, j] = rnd.Next(1, 10);
                    }
                }
            }
            int[,] serialGraph = GenerateGraph(verticlesCount);
            Console.WriteLine("Entered graph: ");
            //PrintGraph(serialGraph);
            Floyd floyd = new Floyd(adjacencyMatrix, verticlesCount);

            DateTime start = DateTime.Now;
            int[,] shortestDistances = floyd.ShortestPath();
            TimeSpan timeTaken = DateTime.Now - start;

            Console.WriteLine("\nMatrix of shortest distances: ");
            for (int i = 0; i < verticlesCount; i++)
            {
                for (int j = 0; j < verticlesCount; j++)
                {
                    //Console.Write(shortestDistances[i, j] +" ");

                }
                //Console.WriteLine();
            }
          
            Console.WriteLine($"\nTime: {timeTaken.TotalMilliseconds} milliseconds");
            
            int[,] parallelGraph = GenerateGraph(verticlesCount);
            Console.WriteLine("\nEntered graph: ");
            //PrintGraph(parallelGraph);

            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            int[,] shortestPaths = FloydForOptimized.ShortestPathsParallel(parallelGraph);
            stopwatch.Stop();

            Console.WriteLine("\nMatrix of shortest distances: ");
            //PrintGraph(shortestPaths);
            Console.WriteLine($"Time: {stopwatch.ElapsedMilliseconds} milliseconds");
            

            double speedup = timeTaken.TotalMilliseconds / stopwatch.ElapsedMilliseconds;
            Console.WriteLine($"Speed up: {speedup:F2}");

        }

    }

    static int[,] GenerateGraph(int n)
    {
        int[,] graph = new int[n, n];
        Random rand = new Random();

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (i == j)
                {
                    graph[i, j] = 0;
                }
                else
                {
                    graph[i, j] = rand.Next(1, 10);
                }
            }
        }

        return graph;
    }

    static void PrintGraph(int[,] graph)
    {
        int n = graph.GetLength(0);
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                Console.Write($"{graph[i, j]} ");
            }
            Console.WriteLine();
        }
    }
}

