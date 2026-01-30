using MazeLearner.GameContent.Phys;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json.Linq;
using System;

namespace MazeLearner.GameContent.Entity
{
    public class BaseEntity
    {
        internal Main GameIsntance = Main.Instance;
        internal long entityId = 0;
        public CollisionBox collisionBox;
        private bool canCollideEachOther = false;
        public int whoAmI;
        public int Width = 64;
        public int Height = 64;
        public int InteractionWidth;
        public int InteractionHeight;
        public int DetectionRangeWidth = 32;
        public int DetectionRangeHeight = 32;
        public int FacingBoxW = 4;
        public int FacingBoxH = 32;
        public string langName = "";
        public Vector2 Velocity;
        public Vector2 PrevVelocity;
        public Vector2 TargetPosition;
        public Vector2 Position;
        public Vector2 FacingBoxPos;
        public Vector2 PrevPosition;
        public const int FacingBoxSizeW = 16;
        public const int FacingBoxSizeH = 32;
        public const int InteractionSizeW = 32;
        public const int InteractionSizeH = 32;
        private Facing _facing = Facing.Down; // Default
        public Facing Facing
        {
            get { return _facing; }
            set { _facing = value; }
        }
        public bool CanCollideEachOther
        {
            get { return canCollideEachOther; }
            set { canCollideEachOther = value; }
        }
        public Facing PrevFacing;
        public void SetPos(int x, int y)
        {
            this.Position = new Vector2((x * 32) - 32 / 2, (y * 32) - 32 / 2);
        }

        public Vector2 Center
        {
            get
            {
                return new Vector2(this.Position.X + (float)(this.Width / 2), this.Position.Y + (float)(this.Height / 2));
            }
            set
            {
                this.Position = new Vector2(value.X + (float)(this.Width / 2), value.Y + (float)(this.Height / 2));
            }
        }
        public Vector2 Size
        {
            get
            {
                return new Vector2(this.Width, this.Height);
            }
            set
            {
                this.Width = (int)value.X;
                this.Height = (int)value.Y;
            }
        }
        public Rectangle DrawingBox
        {
            get
            {
                return new Rectangle((int) this.Position.X, (int) this.Position.Y, this.Width, this.Height);
            }
            set
            {
                this.Position = new Vector2((value.X * 32) - 32 / 2, (value.Y * 32) - 32 / 2);
                this.Width = value.Width;
                this.Height = value.Height;
            }
        }
        public Rectangle InteractionBox
        {
            get
            {
                return new Rectangle((int) this.Position.X + 17, (int) this.Position.Y + 32, BaseEntity.InteractionSizeW, BaseEntity.InteractionSizeH);
            }
            set
            {
                this.Position = new Vector2(value.X, value.Y);
                this.InteractionWidth = value.Width;
                this.InteractionHeight = value.Height;
            }
        }
        public Rectangle DetectionBox
        {
            get
            {
                return new Rectangle((int)this.Position.X, (int)this.Position.Y, this.DetectionRangeWidth, this.DetectionRangeHeight);
            }
            set
            {
                this.Position = new Vector2(value.X, value.Y);
                this.DetectionRangeWidth = value.Width;
                this.DetectionRangeHeight = value.Height;
            }
        }
        public Rectangle FacingBox;
        public Rectangle Hitbox
        {
            get
            {
                return new Rectangle((int) this.Position.X, (int) this.Position.Y, this.Width, this.Height);
            }
            set
            {
                this.Position = new Vector2(value.X, value.Y);
                this.Width = value.Width;
                this.Height = value.Height;
            }
        }
        public Rectangle AttackArea
        {
            get
            {
                return new Rectangle((int)this.Position.X, (int)this.Position.Y, 0, 0);
            }
            set
            {
                this.Position = new Vector2(value.X, value.Y);
            }
        }

        public float AngleTo(Vector2 Destination) => (float)Math.Atan2(Destination.Y - Center.Y, Destination.X - Center.X);
        public float AngleFrom(Vector2 Source) => (float)Math.Atan2(Center.Y - Source.Y, Center.X - Source.X);
        public void Distance(Vector2 other) => Vector2.Distance(Center, other);
        public void DistanceSqr(Vector2 other) => Vector2.DistanceSquared(Center, other);
        public Vector2 DirectionTo(Vector2 Destination) => Vector2.Normalize(Destination - Center);
        public Vector2 DirectionFrom(Vector2 Source) => Vector2.Normalize(Center - Source);
        public bool WithinRange(Vector2 Target, float MaxRange) => Vector2.DistanceSquared(Center, Target) <= MaxRange * MaxRange;
    }
}

