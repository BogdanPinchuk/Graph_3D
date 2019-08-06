using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Graph_3D
{
    class DataSeries
    {
        public DataSeries() { }

        public LineStyle LineStyle { get; set; } =
            new LineStyle();
        public ArrayList PointList { get; set; } =
            new ArrayList();

        public float XDataMin { get; set; } = -5f;
        public float YDataMin { get; set; } = -5f;
        public float ZDataMin { get; set; } = -5f;
        public float XSpacing { get; set; } = 1f;
        public float YSpacing { get; set; } = 1f;
        public float ZSpacing { get; set; } = 1f;
        public int XNumber { get; set; } = 10;
        public int YNumber { get; set; } = 10;
        public int ZNumber { get; set; } = 10;
        public Point3[,] PointArray { get; set; }
        public Point4[,,] Point4Array { get; set; }

        public void AddPoint(Point3 pt)
        {
            PointList.Add(pt);
        }

        public float ZDataMinF()
        {
            float zmin = 0;

            for (int i = 0; i < PointArray.GetLength(0); i++)
            {
                for (int j = 0; j < PointArray.GetLength(1); j++)
                {
                    zmin = Math.Min(zmin, PointArray[i, j].Z);
                }
            }

            return zmin;
        }

        public float ZDataMaxF()
        {
            float zmax = 0;

            for (int i = 0; i < PointArray.GetLength(0); i++)
            {
                for (int j = 0; j < PointArray.GetLength(1); j++)
                {
                    zmax = Math.Max(zmax, PointArray[i, j].Z);
                }
            }

            return zmax;
        }

        public float VDataMinF()
        {
            float vmin = 0;

            for (int i = 0; i < Point4Array.GetLength(0); i++)
            {
                for (int j = 0; j < Point4Array.GetLength(1); j++)
                {
                    for (int k = 0; k < Point4Array.GetLength(2); k++)
                    {
                        vmin = Math.Min(vmin, Point4Array[i, j, k].V);
                    }
                }
            }

            return vmin;
        }

        public float VDataMaxF()
        {
            float vmax = 0;

            for (int i = 0; i < Point4Array.GetLength(0); i++)
            {
                for (int j = 0; j < Point4Array.GetLength(1); j++)
                {
                    for (int k = 0; k < Point4Array.GetLength(2); k++)
                    {
                        vmax = Math.Max(vmax, Point4Array[i, j, k].V);
                    }
                }
            }

            return vmax;
        }
        
    }
}
