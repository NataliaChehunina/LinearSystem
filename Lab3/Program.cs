using System;
using System.Collections.Generic;
using System.Text;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            double[] Vector = new double[] { 209, 301, 94, 118 };
            double[,] Matrix = new double[,] { { 15, 12, 18, 14 }, { 18, 36, 10, 7 }, { 1, 5, 10, 3 }, { 19, 2, 13, 9 } };
            //double[] IterVector = new double[] { 24, 301, 94, 17 };
            //double[,] IterMatrix = new double[,] { { 18, -3, 3, 5 }, { 18, 36, 10, 7 }, { 1, 5, 10, 3 }, { 1, -7, 6, 22 } };
            double[] IterVector = new double[] { 5, 10, 20, 19 };
            double[,] IterMatrix = new double[,] { { 20, 5, 3, 6 }, { 2, 25, 3, 4 }, { 2, 3, 27, 6 }, { 9, 1, 1, 30 } };
            int length = Vector.Length;
            double eps = 1e-6;
            SystemEquat matrix = new SystemEquat(IterMatrix, IterVector, IterMatrix, IterVector, eps);
            matrix.Out();
        }
    }
}
