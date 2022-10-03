using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDE_Q3
{
    class Q3
    {
        static double s = 0.4;
        static double dx = 0.01;
        

        public static void Main()
        {

            for (int i = 0; i < 4; ++i)
            {
                double dt = s * Math.Pow(dx, 2);                   // Calculate dt based on s and delta x
                long xsteps = Convert.ToInt64(1 / dx);
                long tsteps = Convert.ToInt64(1 / dt);
                double[,] u = new double[xsteps, tsteps];

                for (long x = 0; x < xsteps / 2; ++x)              // set bcs for t = 0
                    u[x, 0] = Math.Sin(Math.PI * dx * x);

                for (long t = 0; t < tsteps; ++t)                  // set bcs for x = 0
                    u[0, t] = 0;

                for (long t = 0; t < tsteps; ++t)                  // set bcs for x = L
                    u[xsteps - 1, t] = 0;

                for (long t = 0; t < tsteps - 1; ++t)              // solve PDE
                {
                    for (long x = 1; x < xsteps - 1; ++x)
                    {
                        u[x, t + 1] = u[x, t] + s * (u[x + 1, t] - 2 * u[x, t] + u[x - 1, t]);
                    }
                }

                

                if (i == 0)                                         // print results for part a
                {
                    s = 0.6;
                    Console.WriteLine("a) for s=0.4, " + '\u25B2' + "x=1/100 u(0.5,1) = " + u[xsteps / 2, tsteps - 1]);
                    TextFileWriter.FileWrite(u, "PDEQ3", xsteps, tsteps);
                }

                if (i == 1)                                         // print results for part b
                {
                    s = 0.4;
                    dx = 0.005;
                    Console.WriteLine("b) for s=0.6, " + '\u25B2' + "x=1/100 u(0.5,1) = " + u[xsteps / 2, tsteps -1]);
                }

                if (i == 2)                                         // print results for part c
                {
                    Console.WriteLine("c) for s=0.4, " + '\u25B2' + "x=1/200 u(0.5,1) = " + u[xsteps / 2, tsteps - 1]);
                }
            }

           
            //Console.WriteLine("u(0.5,1) = " + u[xsteps / 2, 1]);
            Console.ReadKey();
            
        }
    }
}

 