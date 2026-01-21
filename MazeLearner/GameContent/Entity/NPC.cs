using MazeLearner.GameContent.Animation;
using MazeLearner.GameContent.Entity.Player;
using MazeLearner.GameContent.Phys;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MazeLearner.GameContent.Entity
{
    public abstract class NPC : BaseEntity
    {
        public int NextDialog = 0;
        private float health = 20;
        private float armor = 0;
        private float damage = 1;
        public int tick;

        protected bool isMoving;
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
            this.SetDefaults();
        }
        public bool IsAlive => this.Health > 0;
        public virtual float GetX => this.Position.X + this.InteractionBox.Height;
        public virtual float GetY => this.Position.Y + this.InteractionBox.Height;
        public virtual void Tick()
        {
            this.tick++;
            this.Movement = Vector2.Zero;
            this.PrevFacing = this.Facing;
            this.CanCollideEachOther = false;
            this.UpdateFacing();
            if (this.CanCollideEachOther == false)
            {
                this.Movement = this.ApplyMovement(this.Movement);
            }
            int objectIndex = this.collisionBox.CheckObjects(this, this is PlayerEntity);
            if (this.CanCollideEachOther == true)
            {
                this.Movement = Vector2.Zero;
            }
            if (this.Movement != Vector2.Zero)
            {
                this.Movement.Normalize();
            }
            this.Position += (this.Movement * Main.MaxTileSize) * (RunningSpeed() * Main.Instance.DeltaTime);
            this.isMoving = this.Movement != Vector2.Zero;
            if (!this.isMoving || this.PrevFacing != this.Facing)
            {
                //this.currentFrame = 1;
                //this.animationTimer = 0.0F;
                this.animationState.Stop();
            }
            if (this.isMoving)
            {
                this.Movement.Normalize();
                this.animationState.Update();
                //this.animationTimer += Main.Instance.DeltaTime;
                //if (this.animationTimer >= FrameTime)
                //{
                //    this.currentFrame = (this.currentFrame + 1) % FrameCount;
                //    this.animationTimer = 0.0F;
                //}
            }
        }
        public virtual float RunningSpeed()
        {
            return 1.0F;
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

        public virtual void SetDefaults() 
        {
        }
        public abstract Assets<Texture2D> GetTexture();

        public string GetDialog()
        {
            return this.Dialogs[this.NextDialog];
        }
        public bool RenderDialogs() 
        {
            return !this.GetDialog().IsEmpty();
        }
    }
}
