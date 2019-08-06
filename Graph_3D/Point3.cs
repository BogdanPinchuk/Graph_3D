using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph_3D
{
    public class Point3
    {
        public float X, Y, Z, W = 1;

        public Point3() { }

        public Point3(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public void Transform(Matrix3 m, Form1 form1, ChartStyle cs)
        {
            //new version
            float x = 0.5f - (X - cs.XMin) / (cs.XMax - cs.XMin),
                y = 0.5f - (Y - cs.YMin) / (cs.YMax - cs.YMin),
                z = 0.5f - (Z - cs.ZMin) / (cs.ZMax - cs.ZMin);

            //// old version
            //float x = (X - cs.XMin) / (cs.XMax - cs.XMin) - 0.5f,
            //    y = (Y - cs.YMin) / (cs.YMax - cs.YMin) - 0.5f,
            //    z = (Z - cs.ZMin) / (cs.ZMax - cs.ZMin) - 0.5f;

            float k = 1f;

            float[] result = m.VectorMultiply(
                new float[4] { k * x, k * y, z, W });

            X = result[0];
            Y = result[1];

            float xShift = 1,       // 1.05f,   // зміщення по X
                yShift = 1.05f,     // 1.05f,   // зміщення по Y
                xScale = 0.9f,                  // масштаб по X
                yScale = 0.9f;                  // масштаб по Y

            if (cs.Title != "No Title")
            {
                yShift = 1f;
                yScale = 0.875f;
            }
            if (cs.IsColorBar)
            {
                xShift = 0.925f;
                xScale = 0.9f;
            }

            // Підтягує до розмірів вільного місця
            {
                X = (xShift + xScale * X) * form1.plotPicBox.Width / 2;
                Y = (yShift + yScale * Y) * form1.plotPicBox.Height / 2;
            }

            // Робить розміри квадратними
            {
                //int sizeGraph = Math.Min(form1.plotPicBox.Width, form1.plotPicBox.Height);

                //X = (xShift * form1.plotPicBox.Width + xScale * X * sizeGraph) / 2;
                //Y = (yShift * form1.plotPicBox.Height + yScale * Y * sizeGraph) / 2;
            }
        }

    }
}
