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

            return FindSolution(leftMatrix, rightMatrix, delta);
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

        private double[] FindSolution(Matrix<double> leftMatrix, DenseVector rightMatrix, double delta)
        {
            bool isRightSolution = false;
            bool isFirstIteration = true;
            double[] solutions = new double[leftMatrix.ColumnCount];
            double[] solutionsTemp = new double[leftMatrix.ColumnCount];

            //Make sure that everything is set to zero 
            for (int i = 0; i < solutions.Length; i++)
                solutions[i] = 0;

            while (!isRightSolution)
            {
                for (int i = 0; i < leftMatrix.RowCount; i++)
                {
                    double sum = 0;
                    double divider = 0;
                    for (int j = 0; j < leftMatrix.ColumnCount; j++)
                    {
                        if (i != j)
                            sum -= solutions[j] * leftMatrix[i, j];
                        else
                            divider = leftMatrix[i, j];
                    }
                    solutionsTemp[i] = (sum + rightMatrix[i]) / divider;
                }

                if (isFirstIteration)
                    isFirstIteration = false;
                else
                {
                    double max = double.MaxValue;
                    for (int i = 0; i < solutionsTemp.Length; i++)
                    {
                        if (max > Math.Abs(solutionsTemp[i] - solutions[i]))
                            max = Math.Abs(solutionsTemp[i] - solutions[i]);
                        solutions[i] = solutionsTemp[i];
                    }

                    if (delta > max)
                        break;
                }
            }
            return solutions;
        }
    }
}