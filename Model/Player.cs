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
    public class Player : RigidBody, IEntity
    {
        public Dictionary<Direction, Dictionary<EntityState, string[]>> Animations { get { return PlayerSprites.Animations; } }
        public PlayerAnimation Animation { get; set; }
        public bool IgnoreGroundCollision { get; set; }
        public Level CurrentLevel { get; }
        public EntityState State { get; set; } = EntityState.Idle;
        public int Damage { get; set; } = 10;
        public int HealthPoint { get; set; } = 100;
        public float Width { get; } = 30;
        public float Height { get; } = 40;
        public float Speed { get; set; } = 20f;
        public RectangleF Hitbox { get; set; }
        public Line AttackLine { get; set; }
        public float JumpForce { get; set; } = 50;
        public float RollForce { get; set; } = 100;
        public Direction Direction { get; set; } = Direction.Left;
        public bool IsFalling { get { return Velocity.Y > 2; } }
        public bool IsJumping { get { return Velocity.Y < -2; } }
        public bool IsMoving { get { return Velocity.X > 5 || Velocity.X < -5; } }
        public bool IsRolling { get; set; }
        public bool IsAttacking { get; set; }
        public bool OnGround { get { return Collisions.HasPlayerCollisionWithPlatforms(this).OnGround && !IsJumping; } }
        public Camera Camera { get; }
        public Platform CurrentPlatform { get; set; }
        public bool IsDead { get; set; }
        public bool IsAttacked { get { return attacked; } set { IsAttacked = value && !OnGround; } }
        private bool attacked;

        public Player(PointF startPoint, Level level) : base()
        {
            Hitbox = new RectangleF(Position, new SizeF(Width, Height));
            Position = startPoint;
            CurrentLevel = level;
            Camera = new Camera(this);
            AttackLine = new Line();
            Animation = new PlayerAnimation(this);
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
            if (Collisions.HasPlayerCollisionWithPlatforms(this).OnGround)
                Velocity = new Vector(Velocity.X, Velocity.Y - JumpForce);
        }

        public void Roll()
        {
            if (Collisions.HasPlayerCollisionWithPlatforms(this).OnGround)
                Velocity = Direction == Direction.Right
                    ? new Vector(RollForce, Velocity.Y)
                    : new Vector(-RollForce, Velocity.Y);
        }

        public void Attack()
        {
            AttackLine = new Line(new PointF(Position.X + Width / 2, Position.Y + Height / 2),
                                  new PointF(Position.X + Width / 2, Position.Y - 50));
            AttackLine.Rotate(-(int)Direction * PlayerAnimation.Frame * 13);
            var collision = Collisions.WasMonsterAttacked(AttackLine, CurrentLevel);
            if (collision.IsCollised)
            {
                collision.Monster.Velocity = new Vector(-(int)Direction * 50, -20);
                collision.Monster.HealthPoint -= new Random().Next(1, 10);
            }
            StopRun();
        }

        public override void Update(float dt)
        {
            Hitbox = new RectangleF(new PointF(Position.X, Position.Y),
                                           new SizeF(Width, Height));
            var collision = Collisions.HasPlayerCollisionWithPlatforms(this);
            if (collision.OnGround && !IgnoreGroundCollision)
                OnGroundCollision(collision.Platform.Position.Y - Height);
            if (State == EntityState.Down)
                IgnoreGroundCollision = false;
            if (HealthPoint < 0 || Position.Y > CurrentLevel.Platforms.Last().Position.Y + 200)
                IsDead = true;
            CollisionWithMonster();
            Camera.Update(dt);
            base.Update(dt);
        }

        private void CollisionWithMonster()
        {
            var collision = Collisions.HasCollisionWithMonsters(Hitbox, CurrentLevel);
            if (collision.IsCollised && !IsRolling && !IsDead)
            {
                IsAttacking = false;
                attacked = true;
                if (IsAttacked)
                    HealthPoint -= new Random().Next(5, 10);
                AttackLine = new Line();
                int velocityX;
                if (Position.X > collision.Monster.Position.X)
                    velocityX = 40;
                else
                    velocityX = -40;

                int velocityY;
                if (Position.Y > collision.Monster.Position.Y)
                    velocityY = 40;
                else
                    velocityY = -40;

                Velocity = new Vector(velocityX, velocityY);
            }
        }
    }
}
