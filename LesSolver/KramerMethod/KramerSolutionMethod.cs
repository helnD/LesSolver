using System;
using MathNet.Numerics.LinearAlgebra;

namespace LesSolver.KramerMethod
{
    public class KramerSolutionMethod : ISolutionMethod
    {
        public double[] Solve(double[,] matrixA, double[] matrixB, double delta)
        {
            var M = Matrix<double>.Build.DenseOfArray(matrixA);
            var T = Matrix<double>.Build;
            int precision;
            double determinant = M.Determinant();
            double [] deterx = new double[matrixB.Length];
            double [] answer = new double[matrixB.Length];

            if (delta == 0.001) precision = 3;
            else precision = 4;
        
            for(int i=0;i< matrixB.Length; i++)
            {
                deterx[i] = T.DenseOfArray(transform(matrixA,matrixB,i)).Determinant();
                answer[i] = Math.Round(deterx[i] / determinant,precision);
            }
            
            return answer;
        }

        public double[,] transform(double[,] matrixA, double[] matrixB, int index)
        {
            double[,] temp = new double[matrixB.Length, matrixB.Length];

            for (int i = 0; i < matrixB.Length; i++)
            {
                for (int j=0;j< matrixB.Length; j++)
                {
                    if(j!=index)
                        temp[i, j] = matrixA[i, j];
                    else temp[i, j] = matrixB[i];
                }
            }
            return temp;
        }
    }
}