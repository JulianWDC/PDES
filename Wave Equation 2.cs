using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDE_Q6
{
    class Q6
    {
        public static void Main()                                         
        {
            double dy = 0.1;                                         // x step size
            double dx = 0.1;                                         // y step sizs
            double w = 0.5 * (1 - (Math.PI / 10));                   // relaxation value
            double v = 0;
            double[] diff = new double[3];
            long msteps = 1000;                                       // number of iterations
            long ysteps = Convert.ToInt64(1 / dy);                   // number of x steps
            long xsteps = Convert.ToInt64(1 / dx);                   // number of y steps

            for (int n = 0; n < 5; ++n)
            {

                double[, ,] u = new double[xsteps, ysteps, msteps];

                for (long y = 0; y < ysteps; ++y)                    // set initial conditions in y variable
                    u[0, y, 0] = 1;

                for (long y = 0; y < ysteps; ++y)                    // ste initial conditions in all other variables
                {
                    for (long x = 1; x < xsteps; ++x)
                    {
                        u[x, y, 0] = 0;
                    }
                }

                if (n == 0)
                {
                    for (long m = 0; m < msteps - 1; ++m)            // calculate using jacobi iteration
                    {
                        for (long x = 1; x < xsteps - 1; ++x)
                        {
                            for (long y = 1; y < ysteps - 1; ++y)
                            {
                                u[x, y, m + 1] = (u[x + 1, y, m] + u[x - 1, y, m] + u[x, y + 1, m] + u[x, y - 1, m]) / 4; // eq(6.6.4)
                            }
                        }
                    }
                    diff[n] = u[(xsteps / 2 -1), (ysteps / 4 -1), msteps -1];
                    Console.WriteLine("a) the value at (0.5,0.25) is: " + diff[n]); // print a result for comparison
                    Console.WriteLine();
                }

                if (n == 1)
                {
                    for (long m = 0; m < msteps - 1; ++m)                           // calculate using Gauss-Seidel iteration
                    {
                        for (long x = 1; x < xsteps - 1; ++x)
                        {
                            for (long y = 1; y < ysteps - 1; ++y)
                            {
                                u[x, y, m + 1] = (u[x + 1, y, m] + u[x - 1, y, m + 1] + u[x, y + 1, m] + u[x, y - 1, m + 1]) / 4;
                            }
                        }
                    }
                    diff[n] = u[(xsteps / 2 - 1), (ysteps / 4 - 1), msteps - 1];
                    Console.WriteLine("b) the value at (0.5,0.25) is: " + diff[n]);// print a result for comparison
                    Console.WriteLine();
                }

                if (n == 2)
                {
                    for (long m = 0; m < msteps - 1; ++m)                          // calculate isng SOR
                    {
                        for (long x = 1; x < xsteps - 1; ++x)
                        {
                            for (long y = 1; y < ysteps - 1; ++y)
                            {
                                u[x, y, m + 1] = u[x, y, m] + w * (u[x + 1, y, m] + u[x - 1, y, m] + u[x, y + 1, m] + u[x, y - 1, m] + 4 * u[x, y, m]);
                            }
                        }
                    }
                    diff[n] = u[(xsteps / 2 - 1), (ysteps / 4 - 1), msteps - 1];
                    Console.WriteLine("c) the value at (0.5,0.25) is: " + diff[n]);// print result for comparison
                    Console.WriteLine();
                }

                if (n == 4)
                {

                    for (int l = 1; l <= 20; ++l)                                   // calculate using fourier series
                    {
                        double A = (2 / (Math.Sinh(-l * Math.PI))) * (1 - Math.Cos(l * Math.PI)) * (1 / (l * Math.PI));
                        v = v + (A * Math.Sin(l * Math.PI * (ysteps / 4)*dy) * (Math.Sinh(l * Math.PI * (((xsteps / 2)*dx) - 1))));
                    }
                    Console.WriteLine("d) the value at (0.5,0.25) is: " + v);
                    Console.WriteLine();
                }
            }
            for (int i = 0; i < 3; ++i)                                              // compare iterative results with fourier series
            {
                char[] q = new char[3] { 'a', 'b', 'c' };
                Console.WriteLine("The error for " + q[i] + " is: " + Math.Abs(v - diff[i]));
                Console.WriteLine();
            }

            Console.ReadKey();
        }
    }
}
