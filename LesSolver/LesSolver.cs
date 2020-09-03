namespace LesSolver
{
    public class LesSolver
    {
        public ISolutionMethod SolutionMethod { get; set; }

        public double[] Solve(double[,] matrixA, double[] matrixB, double delta) =>
            SolutionMethod.Solve(matrixA, matrixB, delta);
    }
}