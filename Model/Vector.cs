using System;
using System.Drawing;

namespace Game.Model
{
    public struct Vector
    {
        public float X { get; private set; }
        public float Y { get; private set; }
        public float Length { get { return (float)Math.Sqrt(X * X + Y * Y); } }
        public static Vector Zero { get { return new Vector(0, 0); } }

        public Vector(float x, float y)
        {
            X = x;
            Y = y;
        }

        public static Vector operator +(Vector v1, Vector v2) => new Vector(v1.X + v2.X, v1.Y + v2.Y);
        public static Vector operator -(Vector v1, Vector v2) => new Vector(v1.X - v2.X, v1.Y - v2.Y);
        public static Vector operator *(Vector v, float m) => new Vector(v.X * m, v.Y * m);
        public static Vector operator /(Vector v, float m) => new Vector(v.X / m, v.Y / m);
        public static float InnerProduct(Vector v1, Vector v2) => v1.X * v2.X + v1.Y * v2.Y;
        public static float Distance(Vector v1, Vector v2) => (float)Math.Sqrt(Math.Pow(v1.X - v2.X, 2) + Math.Pow(v1.Y - v2.Y, 2));
        public static float Distance(PointF p1, PointF p2) => (float)Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        public Vector Normalize() => Length > 0 ? this * (float)(1 / Length) : this;
    }
}
