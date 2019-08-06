using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph_3D
{
    class ChartFunctions
    {
        public ChartFunctions() { }

        public void Peack3D(DataSeries ds, ChartStyle cs)
        {
            cs.XMin = -3f;
            cs.XMax = 3f;
            cs.YMin = -3f;
            cs.YMax = 3f;
            cs.ZMin = -8f;
            cs.ZMax = 8f;
            cs.XTick = 1f;
            cs.YTick = 1f;
            cs.ZTick = 4f;

            ds.XDataMin = cs.XMin;
            ds.YDataMin = cs.YMin;
            ds.XSpacing = 0.1f;
            ds.YSpacing = 0.1f;
            ds.XNumber = Convert.ToInt16((cs.XMax - cs.XMin) / ds.XSpacing) + 1;
            ds.YNumber = Convert.ToInt16((cs.YMax - cs.YMin) / ds.YSpacing) + 1;

            Point3[,] pts = new Point3[ds.XNumber, ds.YNumber];

            for (int i = 0; i < ds.XNumber; i++)
            {
                for (int j = 0; j < ds.YNumber; j++)
                {
                    float x = ds.XDataMin + i * ds.XSpacing,
                        y = ds.YDataMin + j * ds.YSpacing;
                    double zz = 3 * Math.Pow((1 - x), 2) *
                        Math.Exp(-x * x - (y + 1) * (y + 1)) - 10 *
                        (0.2 * x - Math.Pow(x, 3) - Math.Pow(y, 5)) *
                        Math.Exp(-x * x - y * y) - 1 / 3 *
                        Math.Exp(-(x + 1) * (x + 1) - y * y);
                    float z = (float)zz;

                    pts[i, j] = new Point3(x, y, z, 1);
                }
            }

            ds.PointArray = pts;
        }

        public void SinROverR3D(DataSeries ds, ChartStyle cs)
        {
            cs.XMin = -8f;
            cs.XMax = 8f;
            cs.YMin = -8f;
            cs.YMax = 8f;
            cs.ZMin = -0.5f;
            cs.ZMax = 1f;
            cs.XTick = 4f;
            cs.YTick = 4f;
            cs.ZTick = 0.5f;

            ds.XDataMin = cs.XMin;
            ds.YDataMin = cs.YMin;
            ds.XSpacing = 0.25f;
            ds.YSpacing = 0.25f;
            ds.XNumber = Convert.ToInt16((cs.XMax - cs.XMin) / ds.XSpacing) + 1;
            ds.YNumber = Convert.ToInt16((cs.YMax - cs.YMin) / ds.YSpacing) + 1;

            Point3[,] pts = new Point3[ds.XNumber, ds.YNumber];

            for (int i = 0; i < ds.XNumber; i++)
            {
                for (int j = 0; j < ds.YNumber; j++)
                {
                    float x = ds.XDataMin + i * ds.XSpacing,
                        y = ds.YDataMin + j * ds.YSpacing,
                        r = (float)Math.Sqrt(x * x + y * y) + float.Epsilon,
                        z;
                        
                    if (r == 0)
                        z = 1f;
                    else
                        z =  (float)Math.Sin(r) / r;

                    pts[i, j] = new Point3(x, y, z, 1);
                }
            }

            ds.PointArray = pts;
        }

    }
}
