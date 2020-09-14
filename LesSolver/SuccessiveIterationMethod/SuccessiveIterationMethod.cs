using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra;
using System;

namespace LesSolver.SuccessiveIterationMethod
{
    public class SuccessiveIterationMethod : ISolutionMethod
    {
        public double[] Solve(double[,] matrixA, double[] matrixB, double delta)
        {
            var leftMatrix = DenseMatrix.OfArray(matrixA);
            var rightMatrix = DenseVector.OfArray(matrixB);

            if (!CheckConvergence(leftMatrix))
            {
                throw new InvalidOperationException("Условие сходимости не выполнилось");
            }

            //TODO
            return null;
        }

        private bool CheckConvergence(Matrix<double> leftMatrix)
        {
            for (int i = 0; i < leftMatrix.RowCount; i++)
            {
                double sum = 0;
                double mainElement = 0;
                for (int j = 0; j < leftMatrix.ColumnCount; j++)
                {
                    if (i == j)
                        mainElement = leftMatrix[i, j];
                    else
                        sum += Math.Abs(leftMatrix[i, j]);

                }
                if (Math.Abs(mainElement) <= sum)
                    return false;
            }
            return true;
        }
    }
}