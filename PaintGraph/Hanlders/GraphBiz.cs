using PaintGraph.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PaintGraph.Hanlders
{
    public static class GraphBiz
    {
        #region Support functions
        //DFS Traverse
        private static void Traverse(int u, bool[] visited, AdjacencyMatrix adjacencyMatrix)
        {
            visited[u] = true;

            for (int v = 0; v < adjacencyMatrix.N; v++)
            {
                if (adjacencyMatrix.Array[u, v] == 1)
                {
                    if (!visited[v])
                        Traverse(v, visited, adjacencyMatrix);
                }
            }
        }

        public static bool IsConnectedGraph(AdjacencyMatrix adjacencyMatrix)
        {
            bool[] visited = new bool[adjacencyMatrix.N];
            for (int u = 0; u < adjacencyMatrix.N; u++)
            {
                for (int i = 0; i < adjacencyMatrix.N; i++)
                    visited[i] = false;

                Traverse(u, visited, adjacencyMatrix);

                for (int i = 0; i < adjacencyMatrix.N; i++)
                {
                    if (!visited[i])
                        return false;
                }
            }
            return true;
        }

        private static int[] CountDegrees(AdjacencyMatrix adjacencyMatrix)
        {
            int[] degrees = new int[adjacencyMatrix.N];
            for (int i = 0; i < adjacencyMatrix.N; i++)
            {
                int count = 0;
                for (int j = 0; j < adjacencyMatrix.N; j++)
                {
                    if (adjacencyMatrix.Array[i, j] != 0)
                    {
                        count += adjacencyMatrix.Array[i, j];
                        if (i == j) // xet truong hop canh khuyen
                        {
                            count += adjacencyMatrix.Array[i, i];
                        }
                    }
                }

                degrees[i] = count;
            }

            return degrees;
        }

        public static bool IsUndirectedGraph(AdjacencyMatrix adjacencyMatrix)
        {
            var isSymmetric = true;

            for (int i = 0; i < adjacencyMatrix.N; i++)
            {
                for (int j = 0; j < adjacencyMatrix.N; j++)
                {
                    if (adjacencyMatrix.Array[i, j] != adjacencyMatrix.Array[j, i])
                        return false;
                }
            }

            return isSymmetric;
        }
        #endregion

        public static void MarkColorGraph(AdjacencyMatrix adjacencyMatrix)
        {
            int[] totalDegrees = CountDegrees(adjacencyMatrix);
            totalDegrees = totalDegrees.OrderByDescending(x => x).ToArray();

            IList<ColoredVertice> coloredVertices = new List<ColoredVertice>();
            int currentColor = 1;

            // mark color for all vertices by default
            for (int i = 0; i < totalDegrees.Length; i++)
            {
                var coloredVertice = new ColoredVertice
                {
                    Vertice = i,
                    Color = currentColor
                };

                if (coloredVertices.Where(x => x.Vertice == i).FirstOrDefault() == null)
                    coloredVertices.Add(coloredVertice);

                //print result
                if (coloredVertices.Count == totalDegrees.Length)
                {
                    Console.WriteLine($"So mau duoc to: {currentColor}");
                    var resultStr = string.Empty;
                    foreach(var cv in coloredVertices.OrderBy(x => x.Vertice).ToList())
                    {
                        resultStr += cv.Vertice + $"({cv.Color})" + " ";
                    }
                    
                    Console.WriteLine($"{resultStr.Substring(0, resultStr.Length - 1)}");
                    return;
                }

                for (int j = 0; j < adjacencyMatrix.N; j++)
                {
                    // check adjacent edge
                    if (adjacencyMatrix.Array[i, j] != 1 && coloredVertices.Where(x => x.Vertice == j).FirstOrDefault() == null)
                    {
                        var sameColor = new ColoredVertice
                        {
                            Vertice = j,
                            Color = currentColor
                        };

                        coloredVertices.Add(sameColor);
                    }
                }

                currentColor++;
            }
        }
    }
}
