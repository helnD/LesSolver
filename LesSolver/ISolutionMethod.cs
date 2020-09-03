namespace LesSolver
{
    public interface ISolutionMethod
    {
        /// <summary>
        /// Решает систему уравнений.
        /// </summary>
        /// <param name="matrixA">Матрица А (слева от =).</param>
        /// <param name="matrixB">Матрица B (справа от =).</param>
        /// <param name="delta">Погрешность.</param>
        /// <returns>Вектор-решение (x1, x2, x3, ...).</returns>
        double[] Solve(double[,] matrixA, double[] matrixB, double delta);
    }
}