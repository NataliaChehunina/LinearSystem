using System;
using System.Collections.Generic;
using System.Text;

namespace Lab3
{
    public class NoRootsException : System.ApplicationException
    {
        public NoRootsException() { }
        public NoRootsException(string message) : base(message) { }
    }

    class SystemEquat
    {
        double epsilon;
        static int N;

        string s1 = "The initial matrix";
        string s2 = "Gauss-Jordan's method";
        string s3 = "Method of the direct iteration";

        double[] B;
        double[,] A;
        double[] IterB;
        double[,] IterA;
        double[] X;
        double[] X0;

        public SystemEquat(double[,] Ac, double[] Bc, double[,] IterAc, double[] IterBc, double eps)
        {
            epsilon = eps;
            N = Bc.Length;
            B = new double[N];
            A = new double[N, N];
            IterB = new double[N];
            IterA = new double[N, N];
            X = new double[N];
            X0 = new double[N];
            Bc.CopyTo(B,0);
            Array.Copy(Ac, A, Ac.LongLength);
            IterBc.CopyTo(IterB, 0);
            Array.Copy(IterAc, IterA, IterAc.LongLength);
        }

        void PrintMatrix(int n, string str, double[,] Ac, double[] Bc)
        {
            Console.WriteLine(str);
            for (int i=0; i<n; i++)
            {
                for (int j=0 ; j<n; j++)
                {
                    Console.Write("{0,5}", Ac[i, j]);
                }
                Console.Write(" |{0,5}", Bc[i]);
                Console.WriteLine();
            }
        }

        void PrintRoots(int n, string str, int flag)
        {
            
            Console.WriteLine(str);
            for (int i = 0; i < n; i++)
            {
                if (flag == 1)
                {
                    Console.Write("x{0} = {1,10}",i+1,B[i]);
                    Console.WriteLine();
                }
                else{
                    Console.Write("x{0} = {1,10}",i+1,X[i]);
                    Console.WriteLine();
                }
            }
        }

        void GaussJordan(int n)
        {
            for (int k=0; k<n; k++)
            {
                if (A[k, k] == 0) { throw new NoRootsException("There is no roots in this system of equations !"); }
                else
                {
                    double elem = A[k,k];
                    for (int j = 0; j < n; j++)
                    {
                        A[k, j] /= elem;
                    }
                    B[k] /= elem;
                    for (int i = 0; i < n; i++)
                    {
                        if (i == k) { continue; }
                        double buf = A[i, k];
                        for (int j = 0; j < n; j++)
                        {
                            A[i, j] =A[i, j] - A[k, j] * buf;
                        }
                        B[i] =B[i] - B[k] * buf;
                    }
                }
            }
        }

        double MnormDx(int n)
        {
            double buf = 0,mnorm = Math.Abs(X[0] - X0[0]);
            for (int i = 1; i < n; i++)
            {
                buf = Math.Abs(X[i] - X0[i]);
                if (buf > mnorm) { mnorm = buf; }
            }
            return mnorm;
        }

        void DirectIter(int n, double eps)
        {
            double q;
            IterMatrix(n,out q);
            IterB.CopyTo(X0, 0);
            while (true)
            {
                for (int i = 0; i < n; i++)
                {                   
                    X[i] = IterB[i];
                    for (int j = 0; j < n; j++)
                        X[i] += IterA[i, j] * X0[j];                                      
                }
                if (MnormDx(n) <= eps * (1 - q) / q)
                    break;
                X.CopyTo(X0, 0);
            }
        }

        void IterMatrix(int n, out double q)
        {
            double buf,summ;
            q = 0;
            for (int i = 0; i < n; i++)
            {
                buf = IterA[i,i];
                IterB[i] = IterB[i]/buf;
                summ = 0;
                for (int j = 0; j < n; j++)
                {
                    if (i != j)
                    {
                        IterA[i, j] = (-IterA[i, j]) / buf;
                        summ += Math.Abs(IterA[i, j]);
                    }
                    else { IterA[i, j] = 0; }
                }
                if (i == 0)
                    q = summ;
                if (summ > q)
                    q = summ; 
            }
        }

        public void Out()
        {
            PrintMatrix(N, s1, A, B);
            GaussJordan(N);
            PrintRoots(N, s2,1);
            DirectIter(N,epsilon);
            PrintRoots(N, s3,2);
        }
    }
}
