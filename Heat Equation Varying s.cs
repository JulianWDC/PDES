using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDE_Project_Q2
{
    class Q2
    {
        static double s = 0.52;                             // s value        
        static long xsteps = 100;
        static long tsteps = 200;
        static double[,] u = new double[xsteps, tsteps];
  

        public static void Main()
        {
            for (long x = 0; x < xsteps / 2; ++x)                 // set bcs on t = 0
                u[x,0] = 0;

            for (long x = xsteps / 2; x < (3 * xsteps) / 4; ++x)  // set bcs on t = 0
                u[x, 0] = x - xsteps / 2;

            for (long x = (3 * xsteps) / 4; x < xsteps; ++x)      // set bcs on t = 0
                u[x, 0] = xsteps - x;

            for (long t = 0; t < tsteps; ++t)                     // set bcs at x = 0
                u[0, t] = 0;

            for (long t = 0; t < tsteps; ++t)                     // set bcs at x = L
                u[xsteps - 1, t] = 0;

            for (long t = 0; t < tsteps - 1; ++t)
            {
                for (long x = 1; x < xsteps - 1; ++x)             // calculate
                {
                    u[x, t + 1] = u[x, t] + s * (u[x + 1, t] - 2 * u[x, t] + u[x - 1, t]);
                }
            }

            TextFileWriter.FileWrite(u,"PDEQ2", xsteps, tsteps);  // graph results
            
        }
    }
}
