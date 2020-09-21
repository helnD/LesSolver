using LesSolver.SuccessiveIterationMethod;
using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            // Это пока не работает, просто пример использования.
            var solver = new LesSolver.LesSolver
            {
                SolutionMethod = new SuccessiveIterationMethod()
            };

            double[] solutions = solver.Solve(new double[4, 4] { {7, 0.9, 0.5, 0.6 },
                                                                 { 0.2, 12, 1.3, 1.1 },
                                                                 { 1.2, 0.9, 6.5, 0.4 },
                                                                 { 1, 0.5, 0.9, 10 }
                                                                }, new double [4] { 7, 11, -9, 8 }, 0.001);
            foreach (var s in solutions)
                Console.WriteLine(s);
            Console.ReadKey();
        }
    }
}