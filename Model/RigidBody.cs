using System;
using System.Drawing;

namespace Game.Model
{
    public class RigidBody
    {
        public float FrictionConst { get; set; } = 0.7f;
        public PointF Position { get; set; }
        public Vector Velocity { get; set; }
        public Vector Force { get; set; }
        public float Mass { get; set; }
        public bool Gravity { get; set; }
        public Vector Resistance { get; set; }

        public RigidBody()
        {
            Mass = 1;
            Gravity = true;
        }

        public virtual void Update(float dt)
        {
            var force = Force;
            if (Gravity)
                force = new Vector(force.X, force.Y + 9.8f * Mass);

            Resistance = new Vector(-Velocity.X * FrictionConst, 0);

            force += Resistance;
            var ax = force.X / Mass;
            var ay = force.Y / Mass;

            Velocity = new Vector(Velocity.X + ax * dt, Velocity.Y + ay * dt);
            Position = new PointF(Position.X + Velocity.X * dt, Position.Y + Velocity.Y * dt);
            if (Math.Abs(Velocity.X) < 1e-2)
                Velocity = new Vector(0, Velocity.Y);
        }

        public void OnGroundCollision(float groundY)
        {
            if (Velocity.Y < -float.Epsilon)
                return;
            Position = new PointF(Position.X, groundY - 0.001f);
            Velocity = new Vector(Velocity.X, 0);
        }
    }
}
