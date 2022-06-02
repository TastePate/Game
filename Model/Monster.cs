using Game.Control;
using Game.Model.Enums;
using Game.View;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Model
{
    public class Monster : RigidBody, IEntity
    {
        public Dictionary<Direction, Dictionary<EntityState, string[]>> Animations { get { return EnemySprites.Animations; } }
        public MonsterAnimation Animation { get; set; }
        public string Tag { get; }
        public int Damage { get; set; }
        public int HealthPoint { get; set; } = 60;
        public float Width { get; } = 20;
        public float Height { get; } = 20;
        public float Speed { get; set; } = 10f;
        public RectangleF Hitbox { get; set; }
        public Level CurrentLevel { get; }
        public Direction Direction { get; set; } = Direction.Left;
        public bool IsFalling { get { return Velocity.Y > 2; } }
        public bool IsJumping { get { return Velocity.Y < -2; } }
        public bool IsMoving { get { return Velocity.X > 5 || Velocity.X < -5; } }
        public bool IsAttacked { get; set; }
        public Platform CurrentPlatform { get; set; }

        public bool OnGround { get { return Collisions.HasMonsterCollisionWithPlatforms(this).OnGround; } }

        public EntityState State { get; set; } = EntityState.Idle;
        public float JumpForce { get; set; } = 30;
        public bool IgnoreGroundCollision { get; internal set; }
        public MonsterAI AI { get; set; }
        public bool IsDead { get; set; }

        public Monster(string tag, PointF startPoint, Level level) : base()
        {
            Tag = tag;
            CurrentLevel = level;
            Hitbox = new RectangleF(Position, new SizeF(Width, Height));
            Position = startPoint;
            AI = new MonsterAI(this);
            Animation = new MonsterAnimation(this);
        }

        public void Run()
        {
            Force = Direction == Direction.Right ? new Vector(Speed, 0) : new Vector(-Speed, 0);
        }

        public void StopRun()
        {
            Force = new Vector(0, 0);
        }

        public void Jump()
        {
            if (Collisions.HasMonsterCollisionWithPlatforms(this).OnGround)
                Velocity = new Vector(Velocity.X, Velocity.Y - JumpForce);
        }

        public void Attack()
        {
            var collision = Collisions.HasCollisionWithPlayer(Hitbox, CurrentLevel);
            if (collision.IsCollised)
                collision.player.Velocity = new Vector(-(int)Direction * 50, -20);
            StopRun();
        }

        public override void Update(float dt)
        {
            Hitbox = new RectangleF(new PointF(Position.X, Position.Y),
                                            new SizeF(Width, Height));
            var collision = Collisions.HasMonsterCollisionWithPlatforms(this);
            if (collision.OnGround)
                OnGroundCollision(collision.Platform.Position.Y - Height);
            if (HealthPoint <= 0 || Position.Y > CurrentLevel.Platforms.Last().Position.Y + 200)
                IsDead = true;
            base.Update(dt);
            AI.UpdateAI();
        }
    }
}
