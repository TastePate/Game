using System.Drawing;

namespace Game.Model
{
    public class Platform
    {
        public Bitmap Sprite { get; set; }
        public string Tag { get; }
        public static float Height = 20;
        public float Width { get; }
        public PointF Position { get; }
        public SizeF Size
        {
            get => new SizeF(Width, Height);
        }
        public RectangleF Hitbox
        {
            get => new RectangleF(Position, Size);
        }

        public Platform(string tag, PointF position, float width)
        {
            Tag = tag;
            Position = position;
            Width = width;
        }
    }
}
