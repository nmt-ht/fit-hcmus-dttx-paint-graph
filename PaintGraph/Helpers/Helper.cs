using PaintGraph.Models;
using System;
using System.IO;

namespace PaintGraph.Helpers
{
    public static class Helper
    {
        public static AdjacencyMatrix InitAdjacencyMatrix(string filePath)
        {
            try
            {
                var lines = File.ReadAllLines(filePath);

                int n = int.Parse(lines[0]);
                int[,] arr = new int[n, n];

                for (int i = 0; i < n; i++)
                {
                    string[] row = lines[i + 1].Split(" ");

                    for (int j = 0; j < n; j++)
                    {
                        arr[i, j] = int.Parse(row[j]);
                    }
                }

                return new AdjacencyMatrix(n, arr);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.Message.ToString());
                Console.ReadLine();
            }

            return null;
        }

        public static void PrintMatrixToScreen(AdjacencyMatrix adjacencyMatrix)
        {
            Console.WriteLine(adjacencyMatrix.N);

            for (int i = 0; i < adjacencyMatrix.N; i++)
            {
                for (int j = 0; j < adjacencyMatrix.N; j++)
                {
                    Console.Write(adjacencyMatrix.Array[i, j]);
                    Console.Write(" ");
                }

                Console.WriteLine("");
            }
        }
    }
}
