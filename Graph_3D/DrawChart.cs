using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Graph_3D
{
    class DrawChart
    {
        readonly Form1 form1;

        public int[,] CMap { get; set; }
        public bool IsColorMap { get; set; } = true;
        public bool IsHiddenLine { get; set; } = false;
        public bool IsInterp { get; set; } = false;
        public int NumberInterp { get; set; } = 2;
        public int NumberContours { get; set; } = 10;

        internal bool SlowShow { get; set; } = false;

        public DrawChart(Form1 form1)
        {
            this.form1 = form1;
        }

        internal void AddChart(Graphics g, DataSeries ds, 
            ChartStyle cs)
        {
            AddSurface(g, ds, cs);
            AddColorBar(g, ds, cs);
        }

        internal void AddColorBar(Graphics g, DataSeries ds,
            ChartStyle cs)
        {
            if (cs.IsColorBar && IsColorMap)
            {
                Pen aPen = new Pen(Color.Black, 1);
                SolidBrush aBrush = new SolidBrush(cs.TickColor);
                StringFormat sFormat = new StringFormat()
                { Alignment = StringAlignment.Near };
                SizeF size = g.MeasureString("A", cs.TickFont);

                int x, y, width, height;
                Point3[] pts = new Point3[64];
                PointF[] pta = new PointF[4];
                float zmin, zmax;

                zmin = ds.ZDataMinF();
                zmax = ds.ZDataMaxF();

                float dz = (zmax - zmin) / 63;
                
                x = 9 * form1.plotPicBox.Width / 10;
                y = form1.plotPicBox.Height / 10;
                width = form1.plotPicBox.Width / 25;
                height = 4 * form1.plotPicBox.Height / 5;

                for (int i = 0; i < pts.Length; i++)
                {
                    pts[i] = new Point3(x, y, zmin + i * dz, 1);
                }

                for (int i = 0; i < pts.Length - 1; i++)
                {
                    Color color = AddColor(cs, pts[i], zmin, zmax);
                    aBrush = new SolidBrush(color);

                    float y1 = y + height - (pts[i].Z - zmin) *
                        height / (zmax - zmin),
                        y2 = y + height - (pts[i + 1].Z - zmin) *
                        height / (zmax - zmin);

                    pta[0] = new PointF(x, y2);
                    pta[1] = new PointF(x + width, y2);
                    pta[2] = new PointF(x + width, y1);
                    pta[3] = new PointF(x, y1);

                    g.FillPolygon(aBrush, pta);

                    g.DrawLine(new Pen(color), pta[0], pta[1]);
                }

                g.DrawRectangle(aPen, x, y, width, height);

                float tickLength = 0.1f * width;

                for (float z = zmin; z <= zmax ; z += (zmax - zmin) / 6)
                    {
                        float yy = y + height - (z - zmin) *
                            height / (zmax - zmin);

                        g.DrawLine(aPen, x, yy, x + tickLength, yy);
                        g.DrawLine(aPen, x + width, yy, x + width - tickLength, yy);
                        g.DrawString(Math.Round(z, 2).ToString(),
                            cs.TickFont, Brushes.Black, new PointF(x + width + 5,
                            yy - size.Height / 2), sFormat);
                    }
                
            }
        }

        private Color AddColor(ChartStyle cs, Point3 pt,
            float zmin, float zmax)
        {
            int colorLength = CMap.GetLength(0),
                cindex = (int)Math.Round((colorLength *
                (pt.Z - zmin) + (zmax - pt.Z)) /
                (zmax - zmin));
            if (cindex < 1)
                cindex = 1;
            if (cindex > colorLength)
                cindex = colorLength;

            Color color = Color.FromArgb(CMap[cindex - 1, 0],
                CMap[cindex - 1, 1],
                CMap[cindex - 1, 2], 
                CMap[cindex - 1, 3]);

            return color;
        }

        private void AddSurface(Graphics g, DataSeries ds,
            ChartStyle cs)
        {
            Pen aPen = new Pen(ds.LineStyle.LineColor, ds.LineStyle.Thickness)
            { DashStyle = ds.LineStyle.Pattern };
            SolidBrush aBrush = new SolidBrush(Color.FromArgb(255, Color.White));

            Matrix3 m = Matrix3.AzimuthElevation(cs.Elevation, cs.Azimuth);
            PointF[] pta = new PointF[4];
            Point3[,] pts = ds.PointArray,
                pts1 = new Point3[pts.GetLength(0), pts.GetLength(1)];

            // find the min and max z value
            float zmin = ds.ZDataMinF(),
                zmax = ds.ZDataMaxF();

            for (int i = 0; i < pts.GetLength(0); i++)
            {
                for (int j = 0; j < pts.GetLength(1); j++)
                {
                    pts1[i, j] = new Point3(pts[i, j].X, pts[i, j].Y,
                        pts[i, j].Z, 1);
                    pts[i, j].Transform(m, form1, cs);
                }
            }

            // draw surface
            if (!IsInterp)
            {
                for (int i = 0; i < pts.GetLength(0) - 1; i++)
                {
                    for (int j = 0; j < pts.GetLength(1) - 1; j++)
                    {
                        int ii = i, jj = j;

                        if (cs.Elevation >= 0)
                        {
                            if (cs.Azimuth >= -180 && cs.Azimuth < -90)
                            {
                                ii = pts.GetLength(0) - 2 - i;
                                jj = j;
                            }
                            else if (cs.Azimuth >= -90 && cs.Azimuth < 0)
                            {
                                ii = pts.GetLength(0) - 2 - i;
                                jj = pts.GetLength(1) - 2 - j;
                            }
                            else if (cs.Azimuth >= 0 && cs.Azimuth < 90)
                            {
                                ii = i;
                                jj = pts.GetLength(1) - 2 - j;
                            }
                            else if (cs.Azimuth >= 90 && cs.Azimuth <= 180)
                            {
                                ii = i;
                                jj = j;
                            }
                        }
                        else if (cs.Elevation < 0)
                        {
                            if (cs.Azimuth >= -180 && cs.Azimuth < -90)
                            {
                                ii = pts.GetLength(0) - 2 - i;
                                jj = j;
                            }
                            else if (cs.Azimuth >= -90 && cs.Azimuth < 0)
                            {
                                ii = pts.GetLength(0) - 2 - i;
                                jj = pts.GetLength(1) - 2 - j;
                            }
                            else if (cs.Azimuth >= 0 && cs.Azimuth < 90)
                            {
                                ii = i;
                                jj = pts.GetLength(1) - 2 - j;
                            }
                            else if (cs.Azimuth >= 90 && cs.Azimuth <= 180)
                            {
                                ii = i;
                                jj = j;
                            }
                        }

                        pta[0] = new PointF(pts[ii, jj].X, pts[ii, jj].Y);
                        pta[1] = new PointF(pts[ii, jj + 1].X, pts[ii, jj + 1].Y);
                        pta[2] = new PointF(pts[ii + 1, jj + 1].X, pts[ii + 1, jj + 1].Y);
                        pta[3] = new PointF(pts[ii + 1, jj].X, pts[ii + 1, jj].Y);

                        Color color = AddColor(cs, pts[ii, jj], zmin, zmax);
                        aBrush = new SolidBrush(Color.FromArgb(255, color));

                        g.FillPolygon(aBrush, pta);
                        if ((form1.tabControl1.SelectedIndex == 1) && SlowShow)
                            Thread.Sleep(25);

                        if (ds.LineStyle.IsVisible)
                            g.DrawPolygon(aPen, pta);

                    }
                }
            }
            else if (IsInterp)
            {
                for (int i = 0; i < pts.GetLength(0) - 1; i++)
                {
                    for (int j = 0; j < pts.GetLength(1) - 1; j++)
                    {
                        int ii = i;
                        if (cs.Azimuth >= -180 && cs.Azimuth < 0)
                            ii = pts.GetLength(0) - 2 - i;

                        Point3[] points = new Point3[4];
                        points[0] = pts1[ii, j];
                        points[1] = pts1[ii, j + 1];
                        points[2] = pts1[ii + 1, j + 1];
                        points[3] = pts1[ii + 1, j];

                        Interp(g, cs, m, points, zmin, zmax, 1);

                        pta[0] = new PointF(pts[ii, j].X, pts[ii, j].Y);
                        pta[1] = new PointF(pts[ii, j + 1].X, pts[ii, j + 1].Y);
                        pta[2] = new PointF(pts[ii + 1, j + 1].X, pts[ii + 1, j + 1].Y);
                        pta[3] = new PointF(pts[ii + 1, j].X, pts[ii + 1, j].Y);

                        if (ds.LineStyle.IsVisible)
                            g.DrawPolygon(aPen, pta);
                    }
                }
            }
        }

        private void Interp(Graphics g, ChartStyle cs,
            Matrix3 m, Point3[] pta, float zmin, float zmax, int flag)
        {
            SolidBrush aBrush = new SolidBrush(Color.Black);
            PointF[] points = new PointF[4];
            int npoints = NumberInterp;

            Point3[,] pts = new Point3[npoints + 1, npoints + 1],
                pts1 = new Point3[npoints + 1, npoints + 1];

            float x0 = pta[0].X,
                y0 = pta[0].Y,
                x1 = pta[2].X,
                y1 = pta[2].Y,
                dx = (x1 - x0) / npoints,
                dy = (y1 - y0) / npoints,
                C00 = pta[0].Z,
                C10 = pta[3].Z,
                C11 = pta[2].Z,
                C01 = pta[1].Z,
                x, y, C;

            Color color;

            if (flag == 1)
            {
                for (int i = 0; i <= npoints; i++)
                {
                    x = x0 + i * dx;
                    for (int j = 0; j <= npoints; j++)
                    {
                        y = y0 + j * dy;

                        C = (y1 - y) * ((x1 - x) * C00 +
                            (x - x0) * C10) / (x1 - x0) / (y1 - y0) +
                            (y - y0) * ((x1 - x) * C01 +
                            (x - x0) * C11) / (x1 - x0) / (y1 - y0);

                        pts[i, j] = new Point3(x, y, C, 1);
                        pts[i, j].Transform(m, form1, cs);
                    }
                }
            }
            else if (flag == 2) // for XYcolor chart
            {
                for (int i = 0; i <= npoints; i++)
                {
                    x = x0 + i * dx;
                    for (int j = 0; j <= npoints; j++)
                    {
                        y = y0 + j * dy;

                        C = (y1 - y) * ((x1 - x) * C00 +
                            (x - x0) * C10) / (x1 - x0) / (y1 - y0) +
                            (y - y0) * ((x1 - x) * C01 +
                            (x - x0) * C11) / (x1 - x0) / (y1 - y0);

                        pts[i, j] = new Point3(x, y, C, 1);
                        pts[i, j].Transform(m, form1, cs);
                    }
                }
            }
            else if (flag == 3) // for XYColor3D chart
            {
                for (int i = 0; i <= npoints; i++)
                {
                    x = x0 + i * dx;
                    for (int j = 0; j <= npoints; j++)
                    {
                        y = y0 + j * dy;

                        C = (y1 - y) * ((x1 - x) * C00 +
                            (x - x0) * C10) / (x1 - x0) / (y1 - y0) +
                            (y - y0) * ((x1 - x) * C01 +
                            (x - x0) * C11) / (x1 - x0) / (y1 - y0);

                        pts1[i, j] = new Point3(x, y, C, 1);
                        pts[i, j] = new Point3(x, y, cs.ZMin, 1);
                        pts[i, j].Transform(m, form1, cs);
                    }
                }
            }

            for (int i = 0; i < npoints; i++)
            {
                for (int j = 0; j < npoints; j++)
                {
                    color = AddColor(cs, pts[i, j], zmin, zmax);
                    aBrush = new SolidBrush(Color.FromArgb(255, color));

                    points[0] = new PointF(pts[i, j].X, pts[i, j].Y);
                    points[1] = new PointF(pts[i + 1, j].X, pts[i + 1, j].Y);
                    points[2] = new PointF(pts[i + 1, j + 1].X, pts[i + 1, j + 1].Y);
                    points[3] = new PointF(pts[i, j + 1].X, pts[i, j + 1].Y);

                    g.FillPolygon(aBrush, points);
                    aBrush.Dispose();
                }
            }
        }


    }
}
