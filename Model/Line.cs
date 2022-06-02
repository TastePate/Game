using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Model
{
    public class Line
    {
        public PointF Start { get; set; }
        public PointF End { get; set; }

        public Line(PointF start, PointF end)
        {
            Start = start;
            End = end;
        }

        public Line()
        {

        }

        public void Rotate(float angle)
        {
            var m = new Matrix();
            m.RotateAt(angle, Start);
            Transform(m);
        }

        private void Transform(Matrix m)
        {
            var arr = new PointF[] { Start, End };
            m.TransformPoints(arr);
            Start = arr[0];
            End = arr[1];
        }
    }
}
