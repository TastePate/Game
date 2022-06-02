using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Model
{
    public class Camera : RigidBody
    {
        private Player player;
        public float X { get; set; }
        public float Y { get; set; }

        public Camera(Player player)
        {
            this.player = player;
            X = 1920 * 0.75f / 2 - player.Position.X;
            Y = 1080 * 0.75f / 2 - player.Position.Y;
        }

        public override void Update(float dt)
        {
            Velocity = player.Velocity;
            X += -Velocity.X * dt;
            Y += (float)Math.Round(-Velocity.Y * dt);
            if (Y - (1080 * 0.75f / 2 - player.Position.Y) > 1)
                Y = 1080 * 0.75f / 2 - player.Position.Y;
        }
    }
}
