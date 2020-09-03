using LesSolver.GaussMethod;

namespace ConsoleApp
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            // Это пока не работает, просто пример использования.
            var solver = new LesSolver.LesSolver
            {
                SolutionMethod = new GaussSolutionMethod()
            };

            solver.Solve(new double[5, 5], new double[5], 0.5);
        }
    }
}