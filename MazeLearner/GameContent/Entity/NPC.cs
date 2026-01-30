using MazeLearner.GameContent.Animation;
using MazeLearner.GameContent.Entity.Player;
using MazeLearner.GameContent.Phys;
using MazeLearner.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Reflection;

namespace MazeLearner.GameContent.Entity
{
    public abstract class NPC : BaseEntity
    {
        public NPC InteractedNpc { get; set; }
        public int NextDialog = 0;
        private float health = 20;
        private float armor = 0;
        private float damage = 1;
        private float speed = 128.0F;
        private int tilesize = Main.MaxTileSize;
        public int tick;
        public TypeWriterText TypeWriter {  get; private set; }

        public bool isMoving;
        public Vector2 Movement = Vector2.Zero;
        public string[] Dialogs = new string[999];
        public AnimationState animationState;
        public float Health
        {
            get { return health; }
            set
            {
                if (value  < 0) value = 0;
                health = value;
            }
        }
        public float Armor
        {
            get { return armor; }
            set
            {
                armor = value;
            }
        }
        public float Damage
        {
            get { return damage; }
            set
            {
                damage = value;
            }
        }
        public NPC()
        {
            this.collisionBox = new CollisionBox(Main.Instance);
            this.animationState = new AnimationState(this);
            this.TypeWriter = new TypeWriterText();
            this.SetDefaults();
        }
        public bool IsAlive => this.Health > 0;
        public virtual float GetX => this.Position.X + this.InteractionBox.Width;
        public virtual float GetY => this.Position.Y + this.InteractionBox.Height;
        public virtual void Tick(GameTime gameTime)
        {
            this.tick++;
            this.Movement = Vector2.Zero;
            this.PrevFacing = this.Facing;
            this.InteractedNpc = null;
            this.CanCollideEachOther = false;
            this.UpdateFacingBox();
            this.UpdateFacing();
            if (this.CanCollideEachOther == false)
            {
                this.Movement = this.ApplyMovement(this.Movement);
            }
            this.collisionBox.CheckTiles(this);
            this.GetNpcInteracted(this.collisionBox.CheckObjects(this, this is PlayerEntity));
            if (this.CanCollideEachOther == true)
            {
                this.Movement = Vector2.Zero;
            }
            // Rework the movement so everytime player move will move to every tiles
            // Cant pull out the thing!
            this.Position += (this.Movement * tilesize) * (this.RunningSpeed() * Main.Instance.DeltaTime);
            this.isMoving = this.Movement != Vector2.Zero;
            if (!this.isMoving == true || this.PrevFacing != this.Facing)
            {
                this.animationState.Stop();
            }
            if (this.isMoving)
            {
                this.Movement.Normalize();
                this.animationState.Update();
            }
        }

        public virtual void EntityMovement()
        {

            if (this.isMoving == false) return;
            Vector2 tile = GetTileCoord(this.Position);
            Vector2 targetTile = tile + this.Movement;

            this.TargetPosition = targetTile * tilesize;
            this.isMoving = true;
        }

        public virtual float RunningSpeed()
        {
            return 1.0F;
        }
        private Vector2 GetTileCoord(Vector2 worldPos)
        {
            return new Vector2((int) (worldPos.X / 32), (int) (worldPos.Y / 32));
        }
        public void UpdateFacingBox()
        {
            switch (this.Facing)
            {
                case Facing.Down:
                    {
                        int x = this.InteractionBox.X;
                        int y = (int)(this.InteractionBox.Y + this.FacingBoxH);
                        this.FacingBox = new Rectangle(x, y, this.FacingBoxH, this.FacingBoxW);
                        break;
                    }
                case Facing.Up:
                    {
                        int x = this.InteractionBox.X;
                        int y = (int)(this.InteractionBox.Y - this.FacingBoxW);
                        this.FacingBox = new Rectangle(x, y, this.FacingBoxH, this.FacingBoxW);
                        break;
                    }
                case Facing.Left:
                    {
                        int x = (int)(this.InteractionBox.X - this.FacingBoxW);
                        int y = this.InteractionBox.Y;
                        this.FacingBox = new Rectangle(x, y, this.FacingBoxW, this.FacingBoxH);
                        break;
                    }
                case Facing.Right:
                    {
                        int x = (int)(this.InteractionBox.X + this.FacingBoxH);
                        int y = this.InteractionBox.Y;
                        this.FacingBox = new Rectangle(x, y, this.FacingBoxW, this.FacingBoxH);
                        break;
                    }
            }
        }
        public virtual void UpdateFacing()
        {

        }

        
        public virtual Vector2 ApplyMovement(Vector2 movement)
        {
            return movement;
        }

        public void DealDamage(float damage = 0.0F)
        {
            float totalDamage = this.ArmorReduceDamage(damage, this.Armor);
            this.Health -= totalDamage;
        }

        public float ArmorReduceDamage(float damage, float armor)
        {
            float damageMultiplier;
            if (armor > 0)
            {
                damageMultiplier = (100 / (100 + armor));
            } else
            {
                damageMultiplier = (100 / (100 - armor));
            }
            return damage * damageMultiplier;
        }

        public void GetNpcInteracted(int id)
        {
            if (id == 999) return;
            this.InteractedNpc = Main.NPCS[id];
        }

        public virtual void SetDefaults() 
        {
        }
        public abstract Assets<Texture2D> GetTexture();

        public string GetDialog()
        {
            var getdialog = this.Dialogs[this.NextDialog];
            return getdialog;
        }
        public void FacingAt(NPC npc)
        {
            switch (npc.Facing)
            {
                case Facing.Up:
                    {
                        this.Facing = Facing.Down; break;
                    }
                case Facing.Down:
                    {
                        this.Facing = Facing.Up; break;
                    }
                case Facing.Right:
                    {
                        this.Facing = Facing.Left; break;
                    }
                case Facing.Left:
                    {
                        this.Facing = Facing.Right; break;
                    }
            }
        }

        public bool RenderDialogs() 
        {
            return !this.GetDialog().IsEmpty();
        }
    }
}
