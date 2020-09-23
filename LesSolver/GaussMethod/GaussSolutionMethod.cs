namespace LesSolver.GaussMethod
{
    public class GaussSolutionMethod : ISolutionMethod
    {
        public double[] Solve(double[,] matrixA, double[] matrixB, double delta)
        {
            var a = matrixA;
            var b = matrixB;
            var n = matrixA.GetLength(0);
            var s = 0.0;
            var result = new double[n];
            
            for (var index = 0; index < n; index++)
            {
                result[index] = 0.0;
            }
            
            for (int k = 0; k < n - 1; k++)
            {
                for (int i = k + 1; i < n; i++)
                {
                    for (int j = k + 1; j < n; j++)
                    {
                        a[i, j] = a[i, j] - a[k, j] * (a[i, k] / a[k, k]);
                    }
                    b[i] = b[i] - b[k] * a[i, k] / a[k, k];
                }
            }
            
            for (int k = n - 1; k >= 0; k--)
            {
                s = 0;
                for (int j = k + 1; j < n; j++)
                    s = s + a[k, j] * result[j];
                result[k] = (b[k] - s) / a[k, k];
            }

            return result;
        }
    }
}