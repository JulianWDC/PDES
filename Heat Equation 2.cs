using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDE_Q5
{
    class Q5
    {
        static double s = 0.4;
        static double dx = 0.005;

        public static void Main()
        {
                double dt = 0.01;
                long xsteps = 200;
                long tsteps = 200;
                double[,] u = new double[xsteps, tsteps];

                for (long x = 0; x < xsteps ; ++x)
                    u[x, 0] = Math.Sin(Math.PI *dx* x);

                for (long t = 0; t < tsteps; ++t)
                    u[0, t] = 0;

                for (long t = 0; t < tsteps; ++t)
                    u[xsteps - 1, t] = 0;

                for (long x = 1; x < xsteps - 1; ++x)
                {
                    u[x, 1] = u[x, 0] + Math.Pow(1 / 1.5, 2) * (u[x + 1, 0] - 2 * u[x, 0] + u[x - 1, 0]);
                }

                for (long t = 1; t < tsteps - 1; ++t)
                {
                    for (long x = 1; x < xsteps - 1; ++x)
                    {
                        u[x, t + 1] = 2*u[x, t] - u[x,t-1] +Math.Pow(1/1.5,2) * (u[x + 1, t] - 2 * u[x, t] + u[x - 1, t]);
                    }
                }

                


                TextFileWriter.FileWrite(u, "PDEQ5", xsteps, tsteps);
            //Console.WriteLine("u(0.5,1) = " + u[xsteps / 2, 1]);
            //Console.ReadKey();

        }
    }
}
