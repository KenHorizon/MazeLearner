using MazeLearner.GameContent.Phys;
using MazeLearner.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace MazeLearner.GameContent.Entity
{
    public class BaseEntity
    {
        internal Main game = Main.Instance;
        public string[] Dialogs = new string[999];
        public int DialogueIndex = 0;
        public CollisionBox collisionBox;
        internal int whoAmI;
        internal int type;
        internal int tiledId;
        public int Width = 64;
        public int Height = 64;
        public int InteractionWidth;
        public int InteractionHeight;
        public int DetectionRangeWidth = 32;
        public int DetectionRangeHeight = 32;
        public int FacingBoxW = 4;
        public int FacingBoxH = 28;
        public Dictionary<(int, int), DialogueNode> Dialogues = new Dictionary<(int, int), DialogueNode>();
        public (int, int) CurrentDialogId;
        public int SelectedChoiceIndex;
        public string Name
        {
            get; set;
        } = "???";
        public string DisplayName
        {
            get; set;
        } = "???";
        public Vector2 TilePosition;
        public Vector2 Velocity;
        public Vector2 PrevVelocity;
        public Vector2 StartPosition;
        public Vector2 WantedPosition;
        public Vector2 TargetPosition;
        public Vector2 RangePosition;
        public Vector2 Position;
        public Vector2 FacingBoxPos;
        public Vector2 PrevPosition;
        public float MoveProgress;
        public const int FacingBoxSizeW = 16;
        public const int FacingBoxSizeH = 32;
        public const int InteractionSizeW = 32;
        public const int InteractionSizeH = 32;
        public bool CanCollide = false;
        private Direction _wantedDirection = Direction.Down;
        private Direction _targetDirection = Direction.Down;
        public Direction TargetDirection
        {
            get { return _targetDirection; }
            set { _targetDirection = value; }
        }
        private Direction _direction = Direction.Down;
        public Direction Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }
        public Direction WantedDirection
        {
            get { return _wantedDirection; }
            set { _wantedDirection = value; }
        }
        private bool _collideOn = false;
        public bool CollideOn
        {
            get { return _collideOn; }
            set { _collideOn = value; }
        }
        public Direction PrevFacing;
        public void SetPos(int x, int y)
        {
            this.Position = new Vector2((x * Main.TileSize) - (Main.TileSize / 2), y * Main.TileSize - Main.TileSize);
        }
        public Vector2 Offset(Vector2 position) => new Vector2(position.X + (Main.TileSize/2), position.Y + Main.TileSize);
        public Vector2 OffsetReverse(Vector2 position) => new Vector2(position.X - (Main.TileSize/2), position.Y - Main.TileSize);

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
                this.Position = new Vector2(value.X, value.Y);
                this.Width = value.Width;
                this.Height = value.Height;
            }
        }
        public Rectangle InteractionBox
        {
            get
            {
                return new Rectangle((int) this.Position.X + (Main.TileSize / 2), (int) this.Position.Y + (Main.TileSize), BaseEntity.InteractionSizeW, BaseEntity.InteractionSizeH);
            }
            set
            {
                this.Position = new Vector2(value.X, value.Y);
                this.InteractionWidth = value.Width;
                this.InteractionHeight = value.Height;
            }
        }
        public Rectangle DetectionBox;
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

