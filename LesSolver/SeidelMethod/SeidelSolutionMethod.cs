using System;
using System.Collections.Generic;
using System.Linq;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace LesSolver.SeidelMethod
{
    public class SeidelSolutionMethod : ISolutionMethod
    {
        public double[] Solve(double[,] matrixA, double[] matrixB, double delta)
        {
            var leftMatrix = DenseMatrix.OfArray(matrixA);
            var rightMatrix = DenseVector.OfArray(matrixB);

            var C = CreateC(leftMatrix);
            if (C >= 1)
            {
                throw new InvalidOperationException("Условие сходимости не выполнилось");
            }

            var B = CreateB(leftMatrix);
            var e = CreateE(leftMatrix, rightMatrix);
            var X = e.Clone();
            var K = IterationNumber(C, e, delta);

            for (var iteration = 0; iteration < K; iteration++)
            {
                X = GetNextApproximation(X, B, e);
            }
            
            return X.ToArray();
        }

        private double CreateC(Matrix<double> leftMatrix)
        {
            var c = DenseVector.Create(leftMatrix.RowCount, 0);

            for (var rowIndex = 0; rowIndex < leftMatrix.RowCount; rowIndex++)
            {
                var row = leftMatrix.Row(rowIndex).Map(Math.Abs);
                var sumWithoutDiagonal = row.Where((it, index) => index != rowIndex).Sum();
                c[rowIndex] = sumWithoutDiagonal / row[rowIndex];
            }

            return c.Max();
        }

        private Matrix<double> CreateB(Matrix<double> leftMatrix)
        {
            var result = DenseMatrix.Create(leftMatrix.RowCount, leftMatrix.ColumnCount, 0);
            
            for (var row = 0; row < leftMatrix.RowCount; row++)
            {
                for (var column = 0; column < leftMatrix.ColumnCount; column++)
                {
                    var newElement = row == column ? 0 : -leftMatrix[row, column] / leftMatrix[row, row];
                    result[row, column] = newElement;
                }
            }

            return result;
        }

        private Vector<double> CreateE(Matrix<double> leftMatrix, Vector<double> rightMatrix)
        {
            var result = DenseVector.Create(rightMatrix.Count, 0);

            for (var row = 0; row < leftMatrix.RowCount; row++)
            {
                result[row] = rightMatrix[row] / leftMatrix[row, row];
            }

            return result;
        }

        delegate double Logarithm(double number);
        private int IterationNumber(double C, Vector<double> e, double delta)
        {
            
            Logarithm lg = it => Math.Log(it, 10);
            var absMaxE = e.Clone().Map(Math.Abs).Max();
            var iterationNumber = (lg(absMaxE) - lg(delta) - lg(1 - C)) / lg(C) - 1;
            return (int)Math.Abs(Math.Round(iterationNumber));
        }

        private Vector<double> GetNextApproximation(Vector<double> X, Matrix<double> B, Vector<double> e)
        {
            var newX = X.Clone();

            for (var row = 0; row < B.RowCount; row++)
            {
                var v = 0.0;
                for (var column = 0; column < B.ColumnCount; column++)
                {
                    v += B[row, column] * X[column];
                }

                newX[row] = v + e[row];
            }

            return newX;
        }
    }
}