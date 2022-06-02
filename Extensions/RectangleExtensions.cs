using Game.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Extensions
{
    public static class RectangleExtensions
    {
        public static Line[] GetRectangleSides(this RectangleF rectangle)
        {
            var lines = new Line[4];
            lines[0] = new Line(new PointF(rectangle.Left, rectangle.Top), new PointF(rectangle.Right, rectangle.Top));
            lines[1] = new Line(new PointF(rectangle.Right, rectangle.Top), new PointF(rectangle.Right, rectangle.Bottom));
            lines[2] = new Line(new PointF(rectangle.Right, rectangle.Bottom), new PointF(rectangle.Left, rectangle.Bottom));
            lines[3] = new Line(new PointF(rectangle.Left, rectangle.Bottom), new PointF(rectangle.Left, rectangle.Top));
            return lines;
        }
    }
}
