using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDE_Q4
{
    class Q4
    {
        static double s = 0.01;
        static long xsteps = 100;
        static long ysteps = 100;
        static long tsteps = 10000;
        

        public static void Main()
        {
            int t;
            List<double[,]> temp = new List<double[,]>();
            double[,] u = new double[xsteps, ysteps];
            
            for (long y = 0; y < ysteps; ++y)
                u[0, y] = 1;

            for (long y = 0; y < ysteps ; ++y)
            {
                for (long x = 1; x < xsteps ; ++x)
                {
                    u[x, y] = 0;
                }
            }

            temp.Add(u);
            
            TextFileWriter.FileWrite(u, "PDEQ4 t = 0", xsteps, ysteps);

            for (t = 1; t < tsteps; ++t)
            {
                double[,] v = new double[xsteps, ysteps];
                for (int y = 1; y < ysteps - 1; ++y)
                {
                    for (int x = 1; x < xsteps - 1; ++x)
                    {
                        double q = temp[t - 1][x + 1, y] + temp[t - 1][x - 1, y];
                        double p = temp[t - 1][x, y + 1] + temp[t - 1][x, y - 1];
                        v[x, y] = temp[t - 1][x, y] + s * (p + q - 4 * temp[t - 1][x, y]);
                    }
                }

                for (int y = 1; y < ysteps - 1; ++y)
                {
                    v[0, y] = 1;
                }
                temp.Add(v);
                if (t%500 == 0)
                    TextFileWriter.FileWrite(v, "PDEQ4 t = " + t, xsteps, ysteps);
            }

        }
    }
}
