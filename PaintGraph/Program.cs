using PaintGraph.Hanlders;
using PaintGraph.Helpers;
using System;

namespace PaintGraph
{
    class Program
    {
        private const string ADJACENCY_MATRIX_INPUT_FILE = @".\Source\input.txt";
        static void Main(string[] args)
        {
            var matrix = Helper.InitAdjacencyMatrix(ADJACENCY_MATRIX_INPUT_FILE);
            Helper.PrintMatrixToScreen(matrix);
            Console.WriteLine();

            if (!(GraphBiz.IsUndirectedGraph(matrix) && GraphBiz.IsConnectedGraph(matrix)))
            {
                Console.WriteLine("Khong phai do thi vo huong lien thong. Dung.");
                return;
            }

            GraphBiz.MarkColorGraph(matrix);
        }
    }
}
