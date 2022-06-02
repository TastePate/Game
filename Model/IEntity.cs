using Game.Model.Enums;
using System.Collections.Generic;
using System.Drawing;

namespace Game.Model
{
    public interface IEntity
    {
        bool IsFalling { get; }
        bool IsJumping { get; }
        bool IsMoving { get; }
        bool OnGround { get; }
        Level CurrentLevel { get; }
        EntityState State { get; set; }
        int HealthPoint { get; set; }
        int Damage { get; set; }
        RectangleF Hitbox { get; set; }
        float JumpForce { get; set; }
        Direction Direction { get; set; }
        float Width { get; }
        float Height { get; }
        #region Physics
        float FrictionConst { get; set; }
        PointF Position { get; set; }
        Vector Velocity { get; set; }
        Vector Force { get; set; }
        float Mass { get; set; }
        bool Gravity { get; set; }
        Vector Resistance { get; set; }
        #endregion
    }
}
