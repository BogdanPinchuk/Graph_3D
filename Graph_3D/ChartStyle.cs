//#define a0    // верх - зліва
//#define a1    // верх - справа
//#define a2    // низ - зліва
//#define a3    // низ - справа
#define a4      // testing

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Graph_3D
{
    public class ChartStyle
    {
        private enum Axis3D
        {
            X,
            Y,
            Z,
            W
        }

        private enum TickOrLabel
        {
            Tick,
            Label
        }

        private readonly Form1 form1;

        public ChartStyle(Form1 form1)
        {
            this.form1 = form1;
        }

        private Point3[] CoordinateOfChartBox()
        {
            // create coordinate of the axes
            Point3[] pta = new Point3[8];

#if true   // new
            pta[0] = new Point3(XMin, YMin, ZMin, 1);
            pta[1] = new Point3(XMax, YMin, ZMin, 1);
            pta[2] = new Point3(XMax, YMax, ZMin, 1);
            pta[3] = new Point3(XMin, YMax, ZMin, 1);
            pta[4] = new Point3(XMin, YMin, ZMax, 1);
            pta[5] = new Point3(XMax, YMin, ZMax, 1);
            pta[6] = new Point3(XMax, YMax, ZMax, 1);
            pta[7] = new Point3(XMin, YMax, ZMax, 1);
#endif

            Point3[] pts = new Point3[4];
            int[] npts = new int[4] { 0, 1, 2, 3 };

            #region
#if a0
            // низ - справа
            if (Elevation >= 0)
            {
                if (Azimuth >= -180 && Azimuth < -90)
                    npts = new int[4] { 6, 5, 4, 0 };
                else if (Azimuth >= -90 && Azimuth < 0)
                    npts = new int[4] { 7, 6, 5, 1 };
                else if (Azimuth >= 0 && Azimuth < 90)
                    npts = new int[4] { 4, 7, 6, 2 };
                else if (Azimuth >= 90 && Azimuth <= 180)
                    npts = new int[4] { 5, 4, 7, 3 };
            }
            else if (Elevation < 0)
            {
                if (Azimuth >= -180 && Azimuth < -90)
                    npts = new int[4] { 6, 7, 4, 0 };
                else if (Azimuth >= -90 && Azimuth < 0)
                    npts = new int[4] { 7, 4, 5, 1 };
                else if (Azimuth >= 0 && Azimuth < 90)
                    npts = new int[4] { 4, 5, 6, 2 };
                else if (Azimuth >= 90 && Azimuth <= 180)
                    npts = new int[4] { 5, 6, 7, 3 };
            }
#elif a1
            // низ - зліва
            if (Elevation >= 0)
            {
                if (Azimuth >= -180 && Azimuth < -90)
                    npts = new int[4] { 2, 6, 5, 4 };
                else if (Azimuth >= -90 && Azimuth < 0)
                    npts = new int[4] { 3, 7, 6, 5 };
                else if (Azimuth >= 0 && Azimuth < 90)
                    npts = new int[4] { 0, 4, 7, 6 };
                else if (Azimuth >= 90 && Azimuth <= 180)
                    npts = new int[4] { 1, 5, 4, 7 };
            }
            else if (Elevation < 0)
            {
                if (Azimuth >= -180 && Azimuth < -90)
                    npts = new int[4] { 2, 6, 7, 4 };
                else if (Azimuth >= -90 && Azimuth < 0)
                    npts = new int[4] { 3, 7, 4, 5 };
                else if (Azimuth >= 0 && Azimuth < 90)
                    npts = new int[4] { 0, 4, 5, 6 };
                else if (Azimuth >= 90 && Azimuth <= 180)
                    npts = new int[4] { 1, 5, 6, 7 };
            }
#elif a2
            // верх - справа
            if (Elevation >= 0)
            {
                if (Azimuth >= -180 && Azimuth < -90)
                    npts = new int[4] { 2, 3, 0, 4 };
                else if (Azimuth >= -90 && Azimuth < 0)
                    npts = new int[4] { 3, 0, 1, 5 }; 
                else if (Azimuth >= 0 && Azimuth < 90)
                    npts = new int[4] { 0, 1, 2, 6 };
                else if (Azimuth >= 90 && Azimuth <= 180)
                    npts = new int[4] { 1, 2, 3, 7 };
            }
            else if (Elevation < 0)
            {
                if (Azimuth >= -180 && Azimuth < -90)
                    npts = new int[4] { 2, 1, 0, 4 };
                else if (Azimuth >= -90 && Azimuth < 0)
                    npts = new int[4] { 3, 2, 1, 5 };
                else if (Azimuth >= 0 && Azimuth < 90)
                    npts = new int[4] { 0, 3, 2, 6 };
                else if (Azimuth >= 90 && Azimuth <= 180)
                    npts = new int[4] { 1, 0, 3, 7 };
            }
#elif a3
            // верх - зліва
            if (Elevation >= 0)
            {
                if (Azimuth >= -180 && Azimuth < -90)
                    npts = new int[4] { 6, 2, 3, 0 };
                else if (Azimuth >= -90 && Azimuth < 0)
                    npts = new int[4] { 7, 3, 0, 1 };
                else if (Azimuth >= 0 && Azimuth < 90)
                    npts = new int[4] { 4, 0, 1, 2 };
                else if (Azimuth >= 90 && Azimuth <= 180)
                    npts = new int[4] { 5, 1, 2, 3 };
            }
            else if (Elevation < 0)
            {
                if (Azimuth >= -180 && Azimuth < -90)
                    npts = new int[4] { 6, 2, 1, 0 };
                else if (Azimuth >= -90 && Azimuth < 0)
                    npts = new int[4] { 7, 3, 2, 1 };
                else if (Azimuth >= 0 && Azimuth < 90)
                    npts = new int[4] { 4, 0, 3, 2 };
                else if (Azimuth >= 90 && Azimuth <= 180)
                    npts = new int[4] { 5, 1, 0, 3 };
            }
#endif
#endregion

#if true
            // верх - справа
            if (Elevation >= 0)
            {
                if (Azimuth >= -180 && Azimuth < -90)
                    npts = new int[4] { 2, 3, 0, 4 };
                else if (Azimuth >= -90 && Azimuth < 0)
                    npts = new int[4] { 3, 0, 1, 5 };
                else if (Azimuth >= 0 && Azimuth < 90)
                    npts = new int[4] { 0, 1, 2, 6 };
                else if (Azimuth >= 90 && Azimuth <= 180)
                    npts = new int[4] { 1, 2, 3, 7 };
            }
            else if (Elevation < 0)
            {
                if (Azimuth >= -180 && Azimuth < -90)
                {
                    npts = new int[4] { 2, 1, 0, 4 };
                    npts = new int[4] { 6, 7, 4, 0 };
                }
                else if (Azimuth >= -90 && Azimuth < 0)
                {
                    npts = new int[4] { 3, 2, 1, 5 };
                    npts = new int[4] { 7, 4, 5, 1 };
                }
                else if (Azimuth >= 0 && Azimuth < 90)
                {
                    npts = new int[4] { 0, 3, 2, 6 };
                    npts = new int[4] { 4, 5, 6, 2 };
                }
                else if (Azimuth >= 90 && Azimuth <= 180)
                {
                    npts = new int[4] { 1, 0, 3, 7 };
                    npts = new int[4] { 5, 6, 7, 3 };
                }
            }
#endif

            for (int i = 0; i < pts.Length; i++)
                pts[i] = pta[npts[i]];

            return pts;
        }

        private void AddAxes(Graphics g)
        {
            Matrix3 m = Matrix3.AzimuthElevation(Elevation, Azimuth);
            Point3[] pts = CoordinateOfChartBox();

            Pen aPen = new Pen(AxisStyle.LineColor, AxisStyle.Thickness)
            { DashStyle = AxisStyle.Pattern };

            for (int i = 0; i < pts.Length; i++)
                pts[i].Transform(m, form1, this);

            g.DrawLine(aPen, pts[0].X, pts[0].Y, pts[1].X, pts[1].Y);
            g.DrawLine(aPen, pts[1].X, pts[1].Y, pts[2].X, pts[2].Y);
            g.DrawLine(aPen, pts[2].X, pts[2].Y, pts[3].X, pts[3].Y);
            aPen.Dispose();
        }

        private void AddTicks(Graphics g)
        {
            // штрихи біля нумерації

            Matrix3 m = Matrix3.AzimuthElevation(Elevation, Azimuth);
            Point3[] pta = new Point3[2],
                pts = CoordinateOfChartBox();

            Pen aPen = new Pen(AxisStyle.LineColor, AxisStyle.Thickness)
            { DashStyle = AxisStyle.Pattern };

            // Add x ticks
            float offset = (YMax - YMin) / 30f,
                tickLength = offset;
#if true
            for (float x = XMin; x <= XMax; x += XTick)
            {
                int k = 0;

                PointAndLength(ref k, ref tickLength, offset, Axis3D.X);

                pta[0] = new Point3(x, pts[k].Y - tickLength, pts[k].Z, pts[k].W);
                pta[1] = new Point3(x, pts[k].Y, pts[k].Z, pts[k].Z);

                for (int i = 0; i < pta.Length; i++)
                    pta[i].Transform(m, form1, this);

                g.DrawLine(aPen, pta[0].X, pta[0].Y, pta[1].X, pta[1].Y);
            }
#endif
            // Add y ticks
            offset = (XMax - XMin) / 30f;
            tickLength = offset;
#if true
            for (float y = YMin; y <= YMax; y += YTick)
            {
                int k = 0;

                PointAndLength(ref k, ref tickLength, offset, Axis3D.Y);

                pta[0] = new Point3(pts[k].X - tickLength, y, pts[k].Z, pts[k].W);
                pta[1] = new Point3(pts[k].X, y, pts[k].Z, pts[k].Z);

                for (int i = 0; i < pta.Length; i++)
                    pta[i].Transform(m, form1, this);

                g.DrawLine(aPen, pta[0].X, pta[0].Y, pta[1].X, pta[1].Y);
            }
#endif

            // Add z ticks
            float xoffset = (XMax - XMin) / 30f,
            yoffset = (YMax - YMin) / 30f,
            xticklength = xoffset,
            yticklength = yoffset;
#if true
            for (float z = ZMin; z <= ZMax; z += ZTick)
            {
                int k = 0;

                PointAndLength(ref k, ref xticklength, ref yticklength, 
                    xoffset, yoffset, TickOrLabel.Tick);

                pta[0] = new Point3(pts[k].X - xticklength, pts[k].Y - yticklength, z, pts[k].W);
                pta[1] = new Point3(pts[k].X, pts[k].Y, z, pts[k].W);

                for (int i = 0; i < pta.Length; i++)
                    pta[i].Transform(m, form1, this);

                g.DrawLine(aPen, pta[0].X, pta[0].Y, pta[1].X, pta[1].Y);
            }
#endif

                aPen.Dispose();
        }

        private void AddGrids(Graphics g)
        {
            Matrix3 m = Matrix3.AzimuthElevation(Elevation, Azimuth);
            Point3[] pta = new Point3[3],
                pts = CoordinateOfChartBox();

            Pen aPen = new Pen(GridStyle.LineColor, GridStyle.Thickness)
            { DashStyle = GridStyle.Pattern };

            // Draw x gridlines
            int k0 = 0,
                k1 = 2,
                k2 = 3;
#if true
            if (IsXGrid)
            {
                for (float x = XMin; x <= XMax; x += XTick)
                {
                   #region
#if a0      // верх - зліва a0
                    k2 = 3;

                    if (Elevation >= 0)
                    {
                        if ((Azimuth >= -180 && Azimuth < -90) ||
                            (Azimuth >= 0 && Azimuth < 90))
                        {
                            k0 = 2;
                            k1 = 0;

                            pta[0] = new Point3(x, pts[k0].Y, pts[k0].Z, pts[k0].W);
                            pta[1] = new Point3(x, pts[k2].Y, pts[k2].Z, pts[k2].W);
                            pta[2] = new Point3(x, pts[k1].Y, pts[k2].Z, pts[k1].W);
                        }
                        else if ((Azimuth >= -90 && Azimuth < 0) ||
                            (Azimuth >= 90 && Azimuth <= 180))
                        {
                            k0 = 0;
                            k1 = 1;

                            pta[0] = new Point3(x, pts[k0].Y, pts[k0].Z, pts[k0].W);
                            pta[1] = new Point3(x, pts[k1].Y, pts[k2].Z, pts[k1].W);
                            pta[2] = new Point3(x, pts[k2].Y, pts[k2].Z, pts[k2].W);
                        }
                    }
                    else if (Elevation < 0)
                    {
                        if ((Azimuth >= -180 && Azimuth < -90) ||
                            (Azimuth >= 0 && Azimuth < 90))
                        {
                            k0 = 0;
                            k1 = 1;

                            pta[0] = new Point3(x, pts[k0].Y, pts[k0].Z, pts[k0].W);
                            pta[1] = new Point3(x, pts[k1].Y, pts[k2].Z, pts[k1].W);
                            pta[2] = new Point3(x, pts[k2].Y, pts[k2].Z, pts[k2].W);
                        }
                        else if ((Azimuth >= -90 && Azimuth < 0) ||
                            (Azimuth >= 90 && Azimuth <= 180))
                        {
                            k0 = 2;
                            k1 = 0;

                            pta[0] = new Point3(x, pts[k0].Y, pts[k0].Z, pts[k0].W);
                            pta[1] = new Point3(x, pts[k2].Y, pts[k2].Z, pts[k2].W);
                            pta[2] = new Point3(x, pts[k1].Y, pts[k2].Z, pts[k1].W);
                        }
                    }
#elif a1    // верх - справа a1
                    k2 = 0;

                    if (Elevation >= 0)
                    {
                        if ((Azimuth >= -180 && Azimuth < -90) ||
                            (Azimuth >= 0 && Azimuth < 90))
                        {
                            k0 = 3;
                            k1 = 2;

                            pta[0] = new Point3(x, pts[k0].Y, pts[k0].Z, pts[k0].W);
                            pta[1] = new Point3(x, pts[k1].Y, pts[k2].Z, pts[k1].W);
                            pta[2] = new Point3(x, pts[k2].Y, pts[k2].Z, pts[k2].W);
                        }
                        else if ((Azimuth >= -90 && Azimuth < 0) ||
                            (Azimuth >= 90 && Azimuth <= 180))
                        {
                            k0 = 3;
                            k1 = 1;

                            pta[0] = new Point3(x, pts[k0].Y, pts[k2].Z, pts[k0].W);
                            pta[1] = new Point3(x, pts[k2].Y, pts[k2].Z, pts[k2].W);
                            pta[2] = new Point3(x, pts[k1].Y, pts[k1].Z, pts[k1].W);
                        }
                    }
                    else if (Elevation < 0)
                    {
                        if ((Azimuth >= -180 && Azimuth < -90) ||
                            (Azimuth >= 0 && Azimuth < 90))
                        {
                            k0 = 3;
                            k1 = 1;

                            pta[0] = new Point3(x, pts[k0].Y, pts[k2].Z, pts[k0].W);
                            pta[1] = new Point3(x, pts[k2].Y, pts[k2].Z, pts[k2].W);
                            pta[2] = new Point3(x, pts[k1].Y, pts[k1].Z, pts[k1].W);
                        }
                        else if ((Azimuth >= -90 && Azimuth < 0) ||
                            (Azimuth >= 90 && Azimuth <= 180))
                        {
                            k0 = 3;
                            k1 = 2;

                            pta[0] = new Point3(x, pts[k0].Y, pts[k0].Z, pts[k0].W);
                            pta[1] = new Point3(x, pts[k1].Y, pts[k2].Z, pts[k1].W);
                            pta[2] = new Point3(x, pts[k2].Y, pts[k2].Z, pts[k2].W);
                        }
                    }
#elif a2    // низ - зліва a2
                    k2 = 3;

                    if (Elevation >= 0)
                    {
                        if ((Azimuth >= -180 && Azimuth < -90) ||
                            (Azimuth >=0 && Azimuth < 90))
                        {
                            k0 = 0;
                            k1 = 2;

                            pta[0] = new Point3(x, pts[k0].Y, pts[k0].Z, pts[k0].W);
                            pta[1] = new Point3(x, pts[k1].Y, pts[k1].Z, pts[k1].W);
                            pta[2] = new Point3(x, pts[k2].Y, pts[k2].Z, pts[k2].W);
                        }
                        else if ((Azimuth >= -90 && Azimuth < 0) ||
                            (Azimuth >= 90 && Azimuth <= 180))
                        {
                            k0 = 1;
                            k1 = 0;

                            pta[0] = new Point3(x, pts[k0].Y, pts[k0].Z, pts[k0].W);
                            pta[1] = new Point3(x, pts[k1].Y, pts[k1].Z, pts[k1].W);
                            pta[2] = new Point3(x, pts[k1].Y, pts[k2].Z, pts[k2].W);
                        }
                    }
                    else if (Elevation < 0)
                    {
                        if ((Azimuth >= -180 && Azimuth < -90) ||
                            (Azimuth >= 0 && Azimuth < 90))
                        {
                            k0 = 1;
                            k1 = 0;

                            pta[0] = new Point3(x, pts[k0].Y, pts[k0].Z, pts[k0].W);
                            pta[1] = new Point3(x, pts[k1].Y, pts[k1].Z, pts[k1].W);
                            pta[2] = new Point3(x, pts[k1].Y, pts[k2].Z, pts[k2].W);
                        }
                        else if ((Azimuth >= -90 && Azimuth < 0)||
                            (Azimuth >= 90 && Azimuth <= 180))
                        {
                            k0 = 0;
                            k1 = 2;

                            pta[0] = new Point3(x, pts[k0].Y, pts[k0].Z, pts[k0].W);
                            pta[1] = new Point3(x, pts[k1].Y, pts[k1].Z, pts[k1].W);
                            pta[2] = new Point3(x, pts[k2].Y, pts[k2].Z, pts[k2].W);
                        }
                    }
#elif a3    // низ - справа a3
                    k2 = 0;

                    if (Elevation >= 0)
                    {
                        if ((Azimuth >= -180 && Azimuth < -90) ||
                            (Azimuth >= 0 && Azimuth < 90))
                        {
                            k0 = 1;
                            k1 = 3;

                            pta[0] = new Point3(x, pts[k0].Y, pts[k0].Z, pts[k0].W);
                            pta[1] = new Point3(x, pts[k1].Y, pts[k1].Z, pts[k1].W);
                            pta[2] = new Point3(x, pts[k1].Y, pts[k2].Z, pts[k2].W);
                        }
                        else if ((Azimuth >= -90 && Azimuth < 0) ||
                            (Azimuth >= 90 && Azimuth <= 180))
                        {
                            k0 = 2;
                            k1 = 1;

                            pta[0] = new Point3(x, pts[k0].Y, pts[k0].Z, pts[k0].W);
                            pta[1] = new Point3(x, pts[k1].Y, pts[k1].Z, pts[k1].W);
                            pta[2] = new Point3(x, pts[k2].Y, pts[k2].Z, pts[k2].W);
                        }
                    }
                    else if (Elevation < 0)
                    {
                        if ((Azimuth >= -180 && Azimuth < -90) ||
                            (Azimuth >= 0 && Azimuth < 90))
                        {
                            k0 = 2;
                            k1 = 1;

                            pta[0] = new Point3(x, pts[k0].Y, pts[k0].Z, pts[k0].W);
                            pta[1] = new Point3(x, pts[k1].Y, pts[k1].Z, pts[k1].W);
                            pta[2] = new Point3(x, pts[k2].Y, pts[k2].Z, pts[k2].W);
                        }
                        else if ((Azimuth >= -90 && Azimuth < 0) ||
                            (Azimuth >= 90 && Azimuth <= 180))
                        {
                            k0 = 1;
                            k1 = 3;

                            pta[0] = new Point3(x, pts[k0].Y, pts[k0].Z, pts[k0].W);
                            pta[1] = new Point3(x, pts[k1].Y, pts[k1].Z, pts[k1].W);
                            pta[2] = new Point3(x, pts[k1].Y, pts[k2].Z, pts[k2].W);
                        }
                    }
#endif
                    #endregion

                    // низ - зліва a2
                    if ((Azimuth >= -180 && Azimuth < -90) ||
                            (Azimuth >= 0 && Azimuth < 90))
                    {
                        pta[0] = new Point3(x, pts[k0].Y, pts[k0].Z, pts[k0].W);
                        pta[1] = new Point3(x, pts[k1].Y, pts[k1].Z, pts[k1].W);
                        pta[2] = new Point3(x, pts[k2].Y, pts[k2].Z, pts[k2].W);
                    }
                    else if ((Azimuth >= -90 && Azimuth < 0) ||
                        (Azimuth >= 90 && Azimuth <= 180))
                    {
                        pta[0] = new Point3(x, pts[k1].Y, pts[k1].Z, pts[k1].W);
                        pta[1] = new Point3(x, pts[k0].Y, pts[k0].Z, pts[k0].W);
                        pta[2] = new Point3(x, pts[k0].Y, pts[k2].Z, pts[k2].W);
                    }

                    for (int i = 0; i < pta.Length; i++)
                        pta[i].Transform(m, form1, this);

                    g.DrawLine(aPen, pta[0].X, pta[0].Y, pta[1].X, pta[1].Y);
                    g.DrawLine(aPen, pta[1].X, pta[1].Y, pta[2].X, pta[2].Y);
                }
            }
#endif

                    // Draw y gridlines
#if true
            if (IsYGrid)
            {
                for (float y = YMin; y <= YMax; y += YTick)
                {
                    #region
#if a0
                    k2 = 3;

                    if (Elevation >= 0)
                    {
                        if ((Azimuth >= -180 && Azimuth < -90) ||
                            (Azimuth >= 0 && Azimuth < 90))
                        {
                            k0 = 0;
                            k1 = 1;

                            pta[0] = new Point3(pts[k0].X, y, pts[k0].Z, pts[k0].W);
                            pta[1] = new Point3(pts[k1].X, y, pts[k2].Z, pts[k1].W);
                            pta[2] = new Point3(pts[k2].X, y, pts[k2].Z, pts[k2].W);
                        }
                        else if ((Azimuth >= -90 && Azimuth < 0) ||
                            (Azimuth >= 90 && Azimuth <= 180))
                        {
                            k0 = 0;
                            k1 = 2;

                            pta[0] = new Point3(pts[k0].X, y, pts[k2].Z, pts[k0].W);
                            pta[1] = new Point3(pts[k2].X, y, pts[k2].Z, pts[k2].W);
                            pta[2] = new Point3(pts[k1].X, y, pts[k1].Z, pts[k1].W);
                        }
                    }
                    else if (Elevation < 0)
                    {
                        if ((Azimuth >= -180 && Azimuth < -90) ||
                            (Azimuth >= 0 && Azimuth < 90))
                        {
                            k0 = 0;
                            k1 = 2;

                            pta[0] = new Point3(pts[k0].X, y, pts[k2].Z, pts[k0].W);
                            pta[1] = new Point3(pts[k2].X, y, pts[k2].Z, pts[k2].W);
                            pta[2] = new Point3(pts[k1].X, y, pts[k1].Z, pts[k1].W);
                        }
                        else if ((Azimuth >= -90 && Azimuth < 0) ||
                            (Azimuth >= 90 && Azimuth <= 180))
                        {
                            k0 = 0;
                            k1 = 1;

                            pta[0] = new Point3(pts[k0].X, y, pts[k0].Z, pts[k0].W);
                            pta[1] = new Point3(pts[k1].X, y, pts[k2].Z, pts[k1].W);
                            pta[2] = new Point3(pts[k2].X, y, pts[k2].Z, pts[k2].W);
                        }
                    }
#elif a1
                    k2 = 0;

                    if (Elevation >= 0)
                    {
                        if ((Azimuth >= -180 && Azimuth < -90) ||
                            (Azimuth >= 0 && Azimuth < 90))
                        {
                            k0 = 1;
                            k1 = 3;

                            pta[0] = new Point3(pts[k0].X, y, pts[k0].Z, pts[k0].W);
                            pta[1] = new Point3(pts[k2].X, y, pts[k2].Z, pts[k2].W);
                            pta[2] = new Point3(pts[k1].X, y, pts[k2].Z, pts[k1].W);
                        }
                        else if ((Azimuth >= -90 && Azimuth < 0) ||
                            (Azimuth >= 90 && Azimuth <= 180))
                        {
                            k0 = 3;
                            k1 = 2;

                            pta[0] = new Point3(pts[k0].X, y, pts[k0].Z, pts[k0].W);
                            pta[1] = new Point3(pts[k1].X, y, pts[k2].Z, pts[k1].W);
                            pta[2] = new Point3(pts[k2].X, y, pts[k2].Z, pts[k2].W);
                        }
                    }
                    else if (Elevation < 0)
                    {
                        if ((Azimuth >= -180 && Azimuth < -90) ||
                            (Azimuth >= 0 && Azimuth < 90))
                        {
                            k0 = 3;
                            k1 = 2;

                            pta[0] = new Point3(pts[k0].X, y, pts[k0].Z, pts[k0].W);
                            pta[1] = new Point3(pts[k1].X, y, pts[k2].Z, pts[k1].W);
                            pta[2] = new Point3(pts[k2].X, y, pts[k2].Z, pts[k2].W);
                        }
                        else if ((Azimuth >= -90 && Azimuth < 0) ||
                            (Azimuth >= 90 && Azimuth <= 180))
                        {
                            k0 = 1;
                            k1 = 3;

                            pta[0] = new Point3(pts[k0].X, y, pts[k0].Z, pts[k0].W);
                            pta[1] = new Point3(pts[k2].X, y, pts[k2].Z, pts[k2].W);
                            pta[2] = new Point3(pts[k1].X, y, pts[k2].Z, pts[k1].W);
                        }
                    }
#elif a2
                    k2 = 3;

                    if (Elevation >= 0)
                    {
                        if ((Azimuth >= -180 && Azimuth < -90) ||
                            (Azimuth >= 0 && Azimuth < 90))
                        {
                            k0 = 1;
                            k1 = 0;

                            pta[0] = new Point3(pts[k0].X, y, pts[k0].Z, pts[k0].W);
                            pta[1] = new Point3(pts[k1].X, y, pts[k1].Z, pts[k1].W);
                            pta[2] = new Point3(pts[k1].X, y, pts[k2].Z, pts[k2].W);
                        }
                        else if ((Azimuth >= -90 && Azimuth < 0) ||
                            (Azimuth >= 90 && Azimuth <= 180))
                        {
                            k0 = 0;
                            k1 = 2;

                            pta[0] = new Point3(pts[k0].X, y, pts[k0].Z, pts[k0].W);
                            pta[1] = new Point3(pts[k1].X, y, pts[k1].Z, pts[k1].W);
                            pta[2] = new Point3(pts[k2].X, y, pts[k2].Z, pts[k2].W);
                        }
                    }
                    else if (Elevation < 0)
                    {
                        if ((Azimuth >= -180 && Azimuth < -90) ||
                            (Azimuth >= 0 && Azimuth < 90))
                        {
                            k0 = 0;
                            k1 = 2;

                            pta[0] = new Point3(pts[k0].X, y, pts[k0].Z, pts[k0].W);
                            pta[1] = new Point3(pts[k1].X, y, pts[k1].Z, pts[k1].W);
                            pta[2] = new Point3(pts[k2].X, y, pts[k2].Z, pts[k2].W);
                        }
                        else if ((Azimuth >= -90 && Azimuth < 0) ||
                            (Azimuth >= 90 && Azimuth <= 180))
                        {
                            k0 = 1;
                            k1 = 0;

                            pta[0] = new Point3(pts[k0].X, y, pts[k0].Z, pts[k0].W);
                            pta[1] = new Point3(pts[k1].X, y, pts[k1].Z, pts[k1].W);
                            pta[2] = new Point3(pts[k1].X, y, pts[k2].Z, pts[k2].W);
                        }
                    }
#elif a3
                    k2 = 0;

                    if (Elevation >= 0)
                    {
                        if ((Azimuth >= -180 && Azimuth < -90) ||
                            (Azimuth >= 0 && Azimuth < 90))
                        {
                            k0 = 3;
                            k1 = 1;

                            pta[0] = new Point3(pts[k0].X, y, pts[k0].Z, pts[k0].W);
                            pta[1] = new Point3(pts[k1].X, y, pts[k1].Z, pts[k1].W);
                            pta[2] = new Point3(pts[k2].X, y, pts[k2].Z, pts[k2].W);
                        }
                        else if ((Azimuth >= -90 && Azimuth < 0) ||
                            (Azimuth >= 90 && Azimuth <= 180))
                        {
                            k0 = 2;
                            k1 = 3;

                            pta[0] = new Point3(pts[k0].X, y, pts[k0].Z, pts[k0].W);
                            pta[1] = new Point3(pts[k1].X, y, pts[k1].Z, pts[k1].W);
                            pta[2] = new Point3(pts[k1].X, y, pts[k2].Z, pts[k2].W);
                        }
                    }
                    else if (Elevation < 0)
                    {
                        if ((Azimuth >= -180 && Azimuth < -90) ||
                            (Azimuth >= 0 && Azimuth < 90))
                        {
                            k0 = 2;
                            k1 = 3;

                            pta[0] = new Point3(pts[k0].X, y, pts[k0].Z, pts[k0].W);
                            pta[1] = new Point3(pts[k1].X, y, pts[k1].Z, pts[k1].W);
                            pta[2] = new Point3(pts[k1].X, y, pts[k2].Z, pts[k2].W);
                        }
                        else if ((Azimuth >= -90 && Azimuth < 0) ||
                            (Azimuth >= 90 && Azimuth <= 180))
                        {
                            k0 = 3;
                            k1 = 1;

                            pta[0] = new Point3(pts[k0].X, y, pts[k0].Z, pts[k0].W);
                            pta[1] = new Point3(pts[k1].X, y, pts[k1].Z, pts[k1].W);
                            pta[2] = new Point3(pts[k2].X, y, pts[k2].Z, pts[k2].W);
                        }
                    }
#endif
                    #endregion
                    
                    if ((Azimuth >= -180 && Azimuth < -90) ||
                            (Azimuth >= 0 && Azimuth < 90))
                    {
                        pta[0] = new Point3(pts[k1].X, y, pts[k1].Z, pts[k1].W);
                        pta[1] = new Point3(pts[k0].X, y, pts[k0].Z, pts[k0].W);
                        pta[2] = new Point3(pts[k0].X, y, pts[k2].Z, pts[k2].W);
                    }
                    else if ((Azimuth >= -90 && Azimuth < 0) ||
                        (Azimuth >= 90 && Azimuth <= 180))
                    {
                        pta[0] = new Point3(pts[k0].X, y, pts[k0].Z, pts[k0].W);
                        pta[1] = new Point3(pts[k1].X, y, pts[k1].Z, pts[k1].W);
                        pta[2] = new Point3(pts[k2].X, y, pts[k2].Z, pts[k2].W);
                    }

                    for (int i = 0; i < pta.Length; i++)
                        pta[i].Transform(m, form1, this);

                    g.DrawLine(aPen, pta[0].X, pta[0].Y, pta[1].X, pta[1].Y);
                    g.DrawLine(aPen, pta[1].X, pta[1].Y, pta[2].X, pta[2].Y);
                }
            }
#endif

                    // Draw z gridlines
#if true
            if (IsZGrid)
            {
                for (float z = ZMin; z <= ZMax; z += ZTick)
                {
                    #region
#if a0
                    k2 = 3;
                    k0 = 0;
                    k1 = 1;

                    pta[0] = new Point3(pts[k0].X, pts[k0].Y, z, pts[k0].W);
                    pta[1] = new Point3(pts[k1].X, pts[k1].Y, z, pts[k1].W);
                    pta[2] = new Point3(pts[k2].X, pts[k2].Y, z, pts[k2].W);
#elif a1
                    k2 = 0;
                    k0 = 3;
                    k1 = 2;

                    pta[0] = new Point3(pts[k0].X, pts[k0].Y, z, pts[k0].W);
                    pta[1] = new Point3(pts[k1].X, pts[k1].Y, z, pts[k1].W);
                    pta[2] = new Point3(pts[k2].X, pts[k2].Y, z, pts[k2].W);
#elif a2
                    k2 = 3;
                    k0 = 0;
                    k1 = 2;

                    if (Elevation >= 0)
                    {
                        if ((Azimuth >= -180 && Azimuth < -90) ||
                            (Azimuth >= 0 && Azimuth < 90))
                        {
                            pta[0] = new Point3(pts[k0].X, pts[k0].Y, z, pts[k0].W);
                            pta[1] = new Point3(pts[k0].X, pts[k1].Y, z, pts[k1].W);
                            pta[2] = new Point3(pts[k2].X, pts[k2].Y, z, pts[k2].W);
                        }
                        else if ((Azimuth >= -90 && Azimuth < 0) ||
                            (Azimuth >= 90 && Azimuth <= 180))
                        {
                            pta[0] = new Point3(pts[k0].X, pts[k0].Y, z, pts[k0].W);
                            pta[1] = new Point3(pts[k1].X, pts[k0].Y, z, pts[k1].W);
                            pta[2] = new Point3(pts[k2].X, pts[k2].Y, z, pts[k2].W);
                        }
                    }
                    else if (Elevation < 0)
                    {
                        if ((Azimuth >= -180 && Azimuth < -90) ||
                            (Azimuth >= 0 && Azimuth < 90))
                        {
                            pta[0] = new Point3(pts[k0].X, pts[k0].Y, z, pts[k0].W);
                            pta[1] = new Point3(pts[k1].X, pts[k0].Y, z, pts[k1].W);
                            pta[2] = new Point3(pts[k2].X, pts[k2].Y, z, pts[k2].W);
                        }
                        else if ((Azimuth >= -90 && Azimuth < 0) ||
                            (Azimuth >= 90 && Azimuth <= 180))
                        {
                            pta[0] = new Point3(pts[k0].X, pts[k0].Y, z, pts[k0].W);
                            pta[1] = new Point3(pts[k0].X, pts[k1].Y, z, pts[k1].W);
                            pta[2] = new Point3(pts[k2].X, pts[k2].Y, z, pts[k2].W);
                        }
                    }
#elif a3
                    k2 = 0;
                    k0 = 3;
                    k1 = 1;

                    if (Elevation >= 0)
                    {
                        if ((Azimuth >= -180 && Azimuth < -90) ||
                            (Azimuth >= 0 && Azimuth < 90))
                        {
                            pta[0] = new Point3(pts[k0].X, pts[k0].Y, z, pts[k0].W);
                            pta[1] = new Point3(pts[k1].X, pts[k0].Y, z, pts[k1].W);
                            pta[2] = new Point3(pts[k2].X, pts[k2].Y, z, pts[k2].W);
                        }
                        else if ((Azimuth >= -90 && Azimuth < 0) ||
                            (Azimuth >= 90 && Azimuth <= 180))
                        {
                            pta[0] = new Point3(pts[k0].X, pts[k0].Y, z, pts[k0].W);
                            pta[1] = new Point3(pts[k0].X, pts[k1].Y, z, pts[k1].W);
                            pta[2] = new Point3(pts[k2].X, pts[k2].Y, z, pts[k2].W);
                        }
                    }
                    else if (Elevation < 0)
                    {
                        if ((Azimuth >= -180 && Azimuth < -90) ||
                            (Azimuth >= 0 && Azimuth < 90))
                        {
                            pta[0] = new Point3(pts[k0].X, pts[k0].Y, z, pts[k0].W);
                            pta[1] = new Point3(pts[k0].X, pts[k1].Y, z, pts[k1].W);
                            pta[2] = new Point3(pts[k2].X, pts[k2].Y, z, pts[k2].W);
                        }
                        else if ((Azimuth >= -90 && Azimuth < 0) ||
                            (Azimuth >= 90 && Azimuth <= 180))
                        {
                            pta[0] = new Point3(pts[k0].X, pts[k0].Y, z, pts[k0].W);
                            pta[1] = new Point3(pts[k1].X, pts[k0].Y, z, pts[k1].W);
                            pta[2] = new Point3(pts[k2].X, pts[k2].Y, z, pts[k2].W);
                        }
                    }
#endif
                    #endregion

                    if ((Azimuth >= -180 && Azimuth < -90) ||
                            (Azimuth >= 0 && Azimuth < 90))
                    {
                        pta[0] = new Point3(pts[k0].X, pts[k0].Y, z, pts[k0].W);
                        pta[1] = new Point3(pts[k0].X, pts[k1].Y, z, pts[k1].W);
                        pta[2] = new Point3(pts[k2].X, pts[k2].Y, z, pts[k2].W);
                    }
                    else if ((Azimuth >= -90 && Azimuth < 0) ||
                        (Azimuth >= 90 && Azimuth <= 180))
                    {
                        pta[0] = new Point3(pts[k0].X, pts[k0].Y, z, pts[k0].W);
                        pta[1] = new Point3(pts[k1].X, pts[k0].Y, z, pts[k1].W);
                        pta[2] = new Point3(pts[k2].X, pts[k2].Y, z, pts[k2].W);
                    }

                    for (int i = 0; i < pta.Length; i++)
                        pta[i].Transform(m, form1, this);

                    g.DrawLine(aPen, pta[0].X, pta[0].Y, pta[1].X, pta[1].Y);
                    g.DrawLine(aPen, pta[1].X, pta[1].Y, pta[2].X, pta[2].Y);
                }
            }
#endif
                }

        private void AddLabels(Graphics g)
        {
            Matrix3 m = Matrix3.AzimuthElevation(Elevation, Azimuth);
            Point3 pt = new Point3();
            Point3[] pts = CoordinateOfChartBox();

            SolidBrush aBrush = new SolidBrush(LabelColor);
            StringFormat sf = new StringFormat()
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            // Add x tick labels

            float offset = (YMax - YMin) / 10f,
                labelSpace = offset;

            for (float x = XMin; x <= XMax; x += XTick)
            {
                int k = 0;

                PointAndLength(ref k, ref labelSpace, offset, Axis3D.X);

                pt = new Point3(x, pts[k].Y - labelSpace, pts[k].Z, pts[k].W);

                pt.Transform(m, form1, this);

                g.DrawString(x.ToString(), TickFont, aBrush,
                    new PointF(pt.X, pt.Y), sf);
            }

            // Add y tick labels

            offset = (XMax - XMin) / 10f;
            labelSpace = offset;

            for (float y = YMin; y <= YMax; y += YTick)
            {
                int k = 0;

                PointAndLength(ref k, ref labelSpace, offset, Axis3D.Y);

                pt = new Point3(pts[k].X - labelSpace, y, pts[k].Z, pts[k].W);

                pt.Transform(m, form1, this);

                g.DrawString(y.ToString(), TickFont, aBrush,
                    new PointF(pt.X, pt.Y), sf);
            }

            // Add z tick labels

            float xoffset = (XMax - XMin) / 10f,
                yoffset = (YMax - YMin) / 10f,
                xlabelSpace = xoffset,
                ylabelSpace = yoffset;
            SizeF s = g.MeasureString("A", TickFont);

            for (float z = ZMin; z <= ZMax; z += ZTick)
            {
                int k = 0;

                PointAndLength(ref k, ref xlabelSpace, ref ylabelSpace, 
                    xoffset, yoffset, TickOrLabel.Tick);

                pt = new Point3(pts[k].X - xlabelSpace, pts[k].Y - ylabelSpace, z, pts[k].W);

                pt.Transform(m, form1, this);

                g.DrawString(z.ToString(), TickFont, aBrush, new PointF(pt.X, pt.Y), sf);
            }

            aBrush = new SolidBrush(TitleColor);

            if (Title != "No Title")
            {
                g.DrawString(Title, TitleFont, aBrush,
                    new PointF(form1.plotPicBox.Width / 2f,
                    form1.Height / 30f), sf);
            }

            // Add x axis labels
            offset = (YMax - YMin) / 3f;
            labelSpace = offset;
            aBrush = new SolidBrush(LabelColor);

            {
                int k = 0;
                float x = XMin + (XMax - XMin) / 2f;

                PointAndLength(ref k, ref labelSpace, offset, Axis3D.X);

                pt = new Point3(x, pts[k].Y - labelSpace, pts[k].Z, pts[k].W);

                pt.Transform(m, form1, this);

                g.DrawString(XLabel, LabelFont, aBrush,
                    new PointF(pt.X, pt.Y), sf);
            }

            // Add y axis labels
            offset = (XMax - XMin) / 3f;
            labelSpace = offset;

            {
                int k = 0;
                float y = YMin + (YMax - YMin) / 2f;

                PointAndLength(ref k, ref labelSpace, offset, Axis3D.Y);

                pt = new Point3(pts[k].X - labelSpace, y, pts[k].Z, pts[k].W);

                pt.Transform(m, form1, this);

                g.DrawString(YLabel, LabelFont, aBrush,
                    new PointF(pt.X, pt.Y), sf);
            }


            // Add z axis labels
            xoffset = (XMax - XMin) / 4f;
            yoffset = (YMax - YMin) / 4f;
            xlabelSpace = xoffset;
            ylabelSpace = yoffset;

            {
                int k = 0;
                float z = ZMin + (ZMax - ZMin) / 2f;

                PointAndLength(ref k, ref xlabelSpace, ref ylabelSpace, 
                    xoffset, yoffset, TickOrLabel.Label);

                pt = new Point3(pts[k].X - xlabelSpace, pts[k].Y - ylabelSpace, z, pts[k].W);

                pt.Transform(m, form1, this);

                g.DrawString(ZLabel, LabelFont, aBrush,
                    new PointF(pt.X, pt.Y), sf);
            }
        }

        public void AddChartStyle(Graphics g)
        {
            AddTicks(g);
            AddGrids(g);
            AddAxes(g);
            AddLabels(g);
        }

        private void PointAndLength(ref int k, ref float length, float offset, Axis3D axis)
        {
            switch (axis)
            {
                case Axis3D.X:
                    #region

                    #region
#if a0
                    if (Elevation >= 0)
                    {
                        if (Azimuth >= -180 && Azimuth < -90)
                        {
                            k = 1;
                            length = offset;
                        }
                        else if (Azimuth >= -90 && Azimuth < 0)
                        {
                            k = 0;
                            length = -offset;
                        }
                        else if (Azimuth >= 0 && Azimuth < 90)
                        {
                            k = 1;
                            length = -offset;
                        }
                        else if (Azimuth >= 90 && Azimuth <= 180)
                        {
                            k = 0;
                            length = offset;
                        }
                    }
                    else if (Elevation < 0)
                    {
                        if (Azimuth >= -180 && Azimuth < -90)
                        {
                            k = 0;
                            length = -offset;
                        }
                        else if (Azimuth >= -90 && Azimuth < 0)
                        {
                            k = 1;
                            length = offset;
                        }
                        else if (Azimuth >= 0 && Azimuth < 90)
                        {
                            k = 0;
                            length = offset;
                        }
                        else if (Azimuth >= 90 && Azimuth <= 180)
                        {
                            k = 1;
                            length = -offset;
                        }
                    }
#elif a1
                    if (Elevation >= 0)
                    {
                        if (Azimuth >= -180 && Azimuth < -90)
                        {
                            k = 2;
                            length = offset;
                        }
                        else if (Azimuth >= -90 && Azimuth < 0)
                        {
                            k = 1;
                            length = -offset;
                        }
                        else if (Azimuth >= 0 && Azimuth < 90)
                        {
                            k = 2;
                            length = -offset;
                        }
                        else if (Azimuth >= 90 && Azimuth <= 180)
                        {
                            k = 1;
                            length = offset;
                        }
                    }
                    else if (Elevation < 0)
                    {
                        if (Azimuth >= -180 && Azimuth < -90)
                        {
                            k = 1;
                            length = -offset;
                        }
                        else if (Azimuth >= -90 && Azimuth < 0)
                        {
                            k = 2;
                            length = offset;
                        }
                        else if (Azimuth >= 0 && Azimuth < 90)
                        {
                            k = 1;
                            length = offset;
                        }
                        else if (Azimuth >= 90 && Azimuth <= 180)
                        {
                            k = 2;
                            length = -offset;
                        }
                    }
#elif a2
                    if (Elevation >= 0)
                    {
                        if (Azimuth >= -180 && Azimuth < -90)
                        {
                            k = 0;
                            length = -offset;
                        }
                        else if (Azimuth >= -90 && Azimuth < 0)
                        {
                            k = 1;
                            length = offset;
                        }
                        else if (Azimuth >= 0 && Azimuth < 90)
                        {
                            k = 0;
                            length = offset;
                        }
                        else if (Azimuth >= 90 && Azimuth <= 180)
                        {
                            k = 1;
                            length = -offset;
                        }
                    }
                    else if (Elevation < 0)
                    {
                        if (Azimuth >= -180 && Azimuth < -90)
                        {
                            k = 1;
                            length = offset;
                        }
                        else if (Azimuth >= -90 && Azimuth < 0)
                        {
                            k = 0;
                            length = -offset;
                        }
                        else if (Azimuth >= 0 && Azimuth < 90)
                        {
                            k = 1;
                            length = -offset;
                        }
                        else if (Azimuth >= 90 && Azimuth <= 180)
                        {
                            k = 0;
                            length = offset;
                        }
                    }
#elif a3
                    if (Elevation >= 0)
                    {
                        if (Azimuth >= -180 && Azimuth < -90)
                        {
                            k = 1;
                            length = -offset;
                        }
                        else if (Azimuth >= -90 && Azimuth < 0)
                        {
                            k = 2;
                            length = offset;
                        }
                        else if (Azimuth >= 0 && Azimuth < 90)
                        {
                            k = 1;
                            length = offset;
                        }
                        else if (Azimuth >= 90 && Azimuth <= 180)
                        {
                            k = 2;
                            length = -offset;
                        }
                    }
                    else if (Elevation < 0)
                    {
                        if (Azimuth >= -180 && Azimuth < -90)
                        {
                            k = 2;
                            length = offset;
                        }
                        else if (Azimuth >= -90 && Azimuth < 0)
                        {
                            k = 1;
                            length = -offset;
                        }
                        else if (Azimuth >= 0 && Azimuth < 90)
                        {
                            k = 2;
                            length = -offset;
                        }
                        else if (Azimuth >= 90 && Azimuth <= 180)
                        {
                            k = 1;
                            length = offset;
                        }
                    }
#endif
                    #endregion

#if a4

                    if (Azimuth >= -180 && Azimuth < -90)
                    {
                        k = 0;
                        length = -offset;
                    }
                    else if (Azimuth >= -90 && Azimuth < 0)
                    {
                        k = 1;
                        length = offset;
                    }
                    else if (Azimuth >= 0 && Azimuth < 90)
                    {
                        k = 0;
                        length = offset;
                    }
                    else if (Azimuth >= 90 && Azimuth <= 180)
                    {
                        k = 1;
                        length = -offset;
                    }

#endif

                    #endregion
                    break;
                case Axis3D.Y:
                    #region

                    #region
#if a0
                    if (Elevation >= 0)
                    {
                        if (Azimuth >= -180 && Azimuth < -90)
                        {
                            k = 0;
                            length = -offset;
                        }
                        else if (Azimuth >= -90 && Azimuth < 0)
                        {
                            k = 1;
                            length = -offset;
                        }
                        else if (Azimuth >= 0 && Azimuth < 90)
                        {
                            k = 0;
                            length = offset;
                        }
                        else if (Azimuth >= 90 && Azimuth <= 180)
                        {
                            k = 1;
                            length = offset;
                        }
                    }
                    else if (Elevation < 0)
                    {
                        if (Azimuth >= -180 && Azimuth < -90)
                        {
                            k = 1;
                            length = offset;
                        }
                        else if (Azimuth >= -90 && Azimuth < 0)
                        {
                            length = offset;
                            k = 0;
                        }
                        else if (Azimuth >= 0 && Azimuth < 90)
                        {
                            k = 1;
                            length = -offset;
                        }
                        else if (Azimuth >= 90 && Azimuth <= 180)
                        {
                            k = 0;
                            length = -offset;
                        }
                    }
#elif a1
                    if (Elevation >= 0)
                    {
                        if (Azimuth >= -180 && Azimuth < -90)
                        {
                            k = 1;
                            length = -offset;
                        }
                        else if (Azimuth >= -90 && Azimuth < 0)
                        {
                            k = 2;
                            length = -offset;
                        }
                        else if (Azimuth >= 0 && Azimuth < 90)
                        {
                            k = 1;
                            length = offset;
                        }
                        else if (Azimuth >= 90 && Azimuth <= 180)
                        {
                            k = 2;
                            length = offset;
                        }
                    }
                    else if (Elevation < 0)
                    {
                        if (Azimuth >= -180 && Azimuth < -90)
                        {
                            k = 2;
                            length = offset;
                        }
                        else if (Azimuth >= -90 && Azimuth < 0)
                        {
                            k = 1;
                            length = offset;
                        }
                        else if (Azimuth >= 0 && Azimuth < 90)
                        {
                            k = 2;
                            length = -offset;
                        }
                        else if (Azimuth >= 90 && Azimuth <= 180)
                        {
                            k = 1;
                            length = -offset;
                        }
                    }
#elif a2
                    if (Elevation >= 0)
                    {
                        if (Azimuth >= -180 && Azimuth < -90)
                        {
                            k = 1;
                            length = offset;
                        }
                        else if (Azimuth >= -90 && Azimuth < 0)
                        {
                            k = 0;
                            length = offset;
                        }
                        else if (Azimuth >= 0 && Azimuth < 90)
                        {
                            k = 1;
                            length = -offset;
                        }
                        else if (Azimuth >= 90 && Azimuth <= 180)
                        {
                            k = 0;
                            length = -offset;
                        }
                    }
                    else if (Elevation < 0)
                    {
                        if (Azimuth >= -180 && Azimuth < -90)
                        {
                            k = 0;
                            length = -offset;
                        }
                        else if (Azimuth >= -90 && Azimuth < 0)
                        {
                            length = -offset;
                            k = 1;
                        }
                        else if (Azimuth >= 0 && Azimuth < 90)
                        {
                            k = 0;
                            length = offset;
                        }
                        else if (Azimuth >= 90 && Azimuth <= 180)
                        {
                            k = 1;
                            length = offset;
                        }
                    }
#elif a3
                    if (Elevation >= 0)
                    {
                        if (Azimuth >= -180 && Azimuth < -90)
                        {
                            k = 2;
                            length = offset;
                        }
                        else if (Azimuth >= -90 && Azimuth < 0)
                        {
                            k = 1;
                            length = offset;
                        }
                        else if (Azimuth >= 0 && Azimuth < 90)
                        {
                            k = 2;
                            length = -offset;
                        }
                        else if (Azimuth >= 90 && Azimuth <= 180)
                        {
                            k = 1;
                            length = -offset;
                        }
                    }
                    else if (Elevation < 0)
                    {
                        if (Azimuth >= -180 && Azimuth < -90)
                        {
                            k = 1;
                            length = -offset;
                        }
                        else if (Azimuth >= -90 && Azimuth < 0)
                        {
                            k = 2;
                            length = -offset;
                        }
                        else if (Azimuth >= 0 && Azimuth < 90)
                        {
                            k = 1;
                            length = offset;
                        }
                        else if (Azimuth >= 90 && Azimuth <= 180)
                        {
                            k = 2;
                            length = offset;
                        }
                    }
#endif
                    #endregion

#if a4

                    if (Azimuth >= -180 && Azimuth < -90)
                    {
                        k = 1;
                        length = offset;
                    }
                    else if (Azimuth >= -90 && Azimuth < 0)
                    {
                        k = 0;
                        length = offset;
                    }
                    else if (Azimuth >= 0 && Azimuth < 90)
                    {
                        k = 1;
                        length = -offset;
                    }
                    else if (Azimuth >= 90 && Azimuth <= 180)
                    {
                        k = 0;
                        length = -offset;
                    }
#endif

                    #endregion
                    break;
                case Axis3D.Z:
#region
#endregion
                    break;
            }
        }

        private void PointAndLength(ref int k, ref float lengthX, ref float lengthY,
            float offsetx, float offsety, TickOrLabel tol)
        {
            switch (tol)
            {
                case TickOrLabel.Tick:
                    #region

                    #region
#if a0
                    k = 2;

                    if (Elevation >= 0)
                    {
                        if (Azimuth >= -180 && Azimuth < -90)
                        {
                            lengthX = offsetx;
                            lengthY = 0;
                        }
                        else if (Azimuth >= -90 && Azimuth < 0)
                        {
                            lengthX = 0;
                            lengthY = offsety;
                        }
                        else if (Azimuth >= 0 && Azimuth < 90)
                        {
                            lengthX = -offsetx;
                            lengthY = 0;
                        }
                        else if (Azimuth >= 90 && Azimuth <= 180)
                        {
                            lengthX = 0;
                            lengthY = -offsety;
                        }
                    }
                    else if (Elevation < 0)
                    {
                        if (Azimuth >= -180 && Azimuth < -90)
                        {
                            lengthX = 0;
                            lengthY = offsety;
                        }
                        else if (Azimuth >= -90 && Azimuth < 0)
                        {
                            lengthX = -offsetx;
                            lengthY = 0;
                        }
                        else if (Azimuth >= 0 && Azimuth < 90)
                        {
                            lengthX = 0;
                            lengthY = -offsety;
                        }
                        else if (Azimuth >= 90 && Azimuth <= 180)
                        {
                            lengthX = offsetx;
                            lengthY = 0;
                        }
                    }
#elif a1
                    k = 0;

                    if (Elevation >= 0)
                    {
                        if (Azimuth >= -180 && Azimuth < -90)
                        {
                            lengthX = 0;
                            lengthY = -offsety;
                        }
                        else if (Azimuth >= -90 && Azimuth < 0)
                        {
                            lengthX = offsetx;
                            lengthY = 0;
                        }
                        else if (Azimuth >= 0 && Azimuth < 90)
                        {
                            lengthX = 0;
                            lengthY = offsety;
                        }
                        else if (Azimuth >= 90 && Azimuth <= 180)
                        {
                            lengthX = -offsetx;
                            lengthY = 0;
                        }
                    }
                    else if (Elevation < 0)
                    {
                        if (Azimuth >= -180 && Azimuth < -90)
                        {
                            lengthX = -offsetx;
                            lengthY = 0;
                        }
                        else if (Azimuth >= -90 && Azimuth < 0)
                        {
                            lengthX = 0;
                            lengthY = -offsety;
                        }
                        else if (Azimuth >= 0 && Azimuth < 90)
                        {
                            lengthX = offsetx;
                            lengthY = 0;
                        }
                        else if (Azimuth >= 90 && Azimuth <= 180)
                        {
                            lengthX = 0;
                            lengthY = offsety;
                        }
                    }
#elif a2
                    k = 2;

                    if (Elevation >= 0)
                    {
                        if (Azimuth >= -180 && Azimuth < -90)
                        {
                            lengthX = 0;
                            lengthY = offsety;
                        }
                        else if (Azimuth >= -90 && Azimuth < 0)
                        {
                            lengthX = -offsetx;
                            lengthY = 0;
                        }
                        else if (Azimuth >= 0 && Azimuth < 90)
                        {
                            lengthX = 0;
                            lengthY = -offsety;
                        }
                        else if (Azimuth >= 90 && Azimuth <= 180)
                        {
                            lengthX = offsetx;
                            lengthY = 0;
                        }
                    }
                    else if (Elevation < 0)
                    {
                        if (Azimuth >= -180 && Azimuth < -90)
                        {
                            lengthX = offsetx;
                            lengthY = 0;
                        }
                        else if (Azimuth >= -90 && Azimuth < 0)
                        {
                            lengthX = 0;
                            lengthY = offsety;
                        }
                        else if (Azimuth >= 0 && Azimuth < 90)
                        {
                            lengthX = -offsetx;
                            lengthY = 0;
                        }
                        else if (Azimuth >= 90 && Azimuth <= 180)
                        {
                            lengthX = 0;
                            lengthY = -offsety;
                        }
                    }
#elif a3
                k = 0;
                    if (Elevation >= 0)
                    {
                        if (Azimuth >= -180 && Azimuth < -90)
                        {
                            lengthX = -offsetx;
                            lengthY = 0;
                        }
                        else if (Azimuth >= -90 && Azimuth < 0)
                        {
                            lengthX = 0;
                            lengthY = -offsety;
                        }
                        else if (Azimuth >= 0 && Azimuth < 90)
                        {
                            lengthX = offsetx;
                            lengthY = 0;
                        }
                        else if (Azimuth >= 90 && Azimuth <= 180)
                        {
                            lengthX = 0;
                            lengthY = offsety;
                        }
                    }
                    else if (Elevation < 0)
                    {
                        if (Azimuth >= -180 && Azimuth < -90)
                        {
                            lengthX = 0;
                            lengthY = -offsety;
                        }
                        else if (Azimuth >= -90 && Azimuth < 0)
                        {
                            lengthX = offsetx;
                            lengthY = 0;
                        }
                        else if (Azimuth >= 0 && Azimuth < 90)
                        {
                            lengthX = 0;
                            lengthY = offsety;
                        }
                        else if (Azimuth >= 90 && Azimuth <= 180)
                        {
                            lengthX = -offsetx;
                            lengthY = 0;
                        }
                    }
#endif
                    #endregion

#if a4
                    k = 2;

                    if (Azimuth >= -180 && Azimuth < -90)
                    {
                        lengthX = 0;
                        lengthY = offsety;
                    }
                    else if (Azimuth >= -90 && Azimuth < 0)
                    {
                        lengthX = -offsetx;
                        lengthY = 0;
                    }
                    else if (Azimuth >= 0 && Azimuth < 90)
                    {
                        lengthX = 0;
                        lengthY = -offsety;
                    }
                    else if (Azimuth >= 90 && Azimuth <= 180)
                    {
                        lengthX = offsetx;
                        lengthY = 0;
                    }

#endif

                    #endregion
                    break;
                case TickOrLabel.Label:
                    #region

                    #region
#if a0
                    k = 2;

                    if (Elevation >= 0)
                    {
                        if (Azimuth >= -180 && Azimuth < -90)
                        {
                            lengthX = 0;
                            lengthY = offsety;
                        }
                        else if (Azimuth >= -90 && Azimuth < 0)
                        {
                            lengthX = -offsetx;
                            lengthY = 0;
                        }
                        else if (Azimuth >= 0 && Azimuth < 90)
                        {
                            lengthX = 0;
                            lengthY = -offsety;
                        }
                        else if (Azimuth >= 90 && Azimuth <= 180)
                        {
                            lengthX = offsetx;
                            lengthY = 0;
                        }
                    }
                    else if (Elevation < 0)
                    {
                        if (Azimuth >= -180 && Azimuth < -90)
                        {
                            lengthX = offsetx;
                            lengthY = 0;
                        }
                        else if (Azimuth >= -90 && Azimuth < 0)
                        {
                            lengthX = 0;
                            lengthY = offsety;
                        }
                        else if (Azimuth >= 0 && Azimuth < 90)
                        {
                            lengthX = -offsetx;
                            lengthY = 0;
                        }
                        else if (Azimuth >= 90 && Azimuth <= 180)
                        {
                            lengthX = 0;
                            lengthY = -offsety;
                        }
                    }
#elif a1
                k = 0;

                    if (Elevation >= 0)
                    {
                        if (Azimuth >= -180 && Azimuth < -90)
                        {
                            lengthX = -offsetx;
                            lengthY = 0;
                        }
                        else if (Azimuth >= -90 && Azimuth < 0)
                        {
                            lengthX = 0;
                            lengthY = -offsety;
                        }
                        else if (Azimuth >= 0 && Azimuth < 90)
                        {
                            lengthX = offsetx;
                            lengthY = 0;
                        }
                        else if (Azimuth >= 90 && Azimuth <= 180)
                        {
                            lengthX = 0;
                            lengthY = offsety;
                        }
                    }
                    else if (Elevation < 0)
                    {
                        if (Azimuth >= -180 && Azimuth < -90)
                        {
                            lengthX = 0;
                            lengthY = -offsety;
                        }
                        else if (Azimuth >= -90 && Azimuth < 0)
                        {
                            lengthX = offsetx;
                            lengthY = 0;
                        }
                        else if (Azimuth >= 0 && Azimuth < 90)
                        {
                            lengthX = 0;
                            lengthY = offsety;
                        }
                        else if (Azimuth >= 90 && Azimuth <= 180)
                        {
                            lengthX = -offsetx;
                            lengthY = 0;
                        }
                    }
#elif a2
                    k = 2;

                    if (Elevation >= 0)
                    {
                        if (Azimuth >= -180 && Azimuth < -90)
                        {
                            lengthX = 0;
                            lengthY = offsety;
                        }
                        else if (Azimuth >= -90 && Azimuth < 0)
                        {
                            lengthX = -offsetx;
                            lengthY = 0;
                        }
                        else if (Azimuth >= 0 && Azimuth < 90)
                        {
                            lengthX = 0;
                            lengthY = -offsety;
                        }
                        else if (Azimuth >= 90 && Azimuth <= 180)
                        {
                            lengthX = offsetx;
                            lengthY = 0;
                        }
                    }
                    else if (Elevation < 0)
                    {
                        if (Azimuth >= -180 && Azimuth < -90)
                        {
                            lengthX = offsetx;
                            lengthY = 0;
                        }
                        else if (Azimuth >= -90 && Azimuth < 0)
                        {
                            lengthX = 0;
                            lengthY = offsety;
                        }
                        else if (Azimuth >= 0 && Azimuth < 90)
                        {
                            lengthX = -offsetx;
                            lengthY = 0;
                        }
                        else if (Azimuth >= 90 && Azimuth <= 180)
                        {
                            lengthX = 0;
                            lengthY = -offsety;
                        }
                    }
#elif a3
                k = 0;
                    if (Elevation >= 0)
                    {
                        if (Azimuth >= -180 && Azimuth < -90)
                        {
                            lengthX = -offsetx;
                            lengthY = 0;
                        }
                        else if (Azimuth >= -90 && Azimuth < 0)
                        {
                            lengthX = 0;
                            lengthY = -offsety;
                        }
                        else if (Azimuth >= 0 && Azimuth < 90)
                        {
                            lengthX = offsetx;
                            lengthY = 0;
                        }
                        else if (Azimuth >= 90 && Azimuth <= 180)
                        {
                            lengthX = 0;
                            lengthY = offsety;
                        }
                    }
                    else if (Elevation < 0)
                    {
                        if (Azimuth >= -180 && Azimuth < -90)
                        {
                            lengthX = 0;
                            lengthY = -offsety;
                        }
                        else if (Azimuth >= -90 && Azimuth < 0)
                        {
                            lengthX = offsetx;
                            lengthY = 0;
                        }
                        else if (Azimuth >= 0 && Azimuth < 90)
                        {
                            lengthX = 0;
                            lengthY = offsety;
                        }
                        else if (Azimuth >= 90 && Azimuth <= 180)
                        {
                            lengthX = -offsetx;
                            lengthY = 0;
                        }
                    }
#endif
                    #endregion

#if a4
                    k = 2;

                    if (Azimuth >= -180 && Azimuth < -90)
                    {
                        lengthX = 0;
                        lengthY = offsety;
                    }
                    else if (Azimuth >= -90 && Azimuth < 0)
                    {
                        lengthX = -offsetx;
                        lengthY = 0;
                    }
                    else if (Azimuth >= 0 && Azimuth < 90)
                    {
                        lengthX = 0;
                        lengthY = -offsety;
                    }
                    else if (Azimuth >= 90 && Azimuth <= 180)
                    {
                        lengthX = offsetx;
                        lengthY = 0;
                    }

#endif

                    #endregion
                    break;
            }

        }

        public float XMin { get; set; } = -5f;
        public float XMax { get; set; } = 5f;
        public float YMin { get; set; } = -3f;
        public float YMax { get; set; } = 3f;
        public float ZMin { get; set; } = -6f;
        public float ZMax { get; set; } = 6f;
        public float XTick { get; set; } = 1f;
        public float YTick { get; set; } = 1f;
        public float ZTick { get; set; } = 1f;
        public Font TickFont { get; set; } =
            new Font("Arial Narrow", 8, FontStyle.Regular);
        public Color TickColor { get; set; } = Color.Black;
        public string Title { get; set; } = "My 3D Chart";
        public Font TitleFont { get; set; } =
            new Font("Arial Narrow", 14, FontStyle.Regular);
        public Color TitleColor { get; set; } = Color.Black;
        public string XLabel { get; set; } = "X Axis";
        public string YLabel { get; set; } = "Y Axis";
        public string ZLabel { get; set; } = "Z Axis";
        public Font LabelFont { get; set; } =
            new Font("Arial Narrow", 10, FontStyle.Regular);
        public Color LabelColor { get; set; } = Color.Black;
        public float Elevation { get; set; } = 30f;
        public float Azimuth { get; set; } = -30f;
        public bool IsXGrid { get; set; } = true;
        public bool IsYGrid { get; set; } = true;
        public bool IsZGrid { get; set; } = true;
        public LineStyle GridStyle { get; set; } =
            new LineStyle();
        public LineStyle AxisStyle { get; set; } =
            new LineStyle();
        public bool IsColorBar { get; set; } = false;

    }
}
