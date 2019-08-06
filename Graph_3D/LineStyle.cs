using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Graph_3D
{
    public class LineStyle
    {
        public LineStyle() { }

        public DashStyle Pattern { get; set; } =
            DashStyle.Solid;
        public Color LineColor { get; set; } =
            Color.Black;
        public float Thickness { get; set; } = 1;
        public bool IsVisible { get; set; } = true;
    }
}
