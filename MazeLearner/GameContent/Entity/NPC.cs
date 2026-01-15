using MazeLearner.GameContent.Entity.Player;
using MazeLearner.GameContent.Phys;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.GameContent.Entity
{
    public abstract class NPC : BaseEntity
    {
        public int NextDialog = 0;
        private float health = 20;
        private float armor = 0;
        private float speed = 300;
        private float damage = 1;

        public int tick;
        public int currentFrame = 1;
        private float animationTimer;
        private const float FrameTime = 0.15F;
        private bool isMoving;
        private const int FrameCount = 3;
        public Vector2 Movement = Vector2.Zero;
        public String[] IntroDialogs = new String[999];
        public String[] WinDialogs = new String[999];
        public String[] DefeatDialogs = new String[999];
        public String[] EpilogueDialogs = new String[999];
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
        public float Speed
        {
            get { return speed; }
            set
            {
                speed = value;  
            }
        }
        public NPC()
        {
            this.collisionBox = new CollisionBox(Main.Instance);
            this.SetDefaults();
        }

        public virtual void Tick()
        {
            this.tick++;
            this.Movement = Vector2.Zero;
            this.PrevFacing = this.Facing;
            this.CanCollideEachOther = false;
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
            this.Position += ((this.Movement * this.Speed) * RunningSpeed()) * Main.Instance.DeltaTime;
            this.isMoving = this.Movement != Vector2.Zero;
            if (!this.isMoving || this.PrevFacing != this.Facing)
            {
                this.currentFrame = 1;
                this.animationTimer = 0.0F;
            }
            if (this.isMoving)
            {
                this.Movement.Normalize();
                this.animationTimer += Main.Instance.DeltaTime;
                if (this.animationTimer >= FrameTime)
                {
                    this.currentFrame = (this.currentFrame + 1) % FrameCount;
                    this.animationTimer = 0.0F;
                }
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

        public String GetIntroDialog()
        {
            return this.IntroDialogs[this.NextDialog];
        }
        public String GetWinDialog()
        {
            return this.WinDialogs[this.NextDialog];
        }
        public String GetDefeatDialog()
        {
            return this.DefeatDialogs[this.NextDialog];
        }
        public String GetEpilogueDialog()
        {
            return this.EpilogueDialogs[this.NextDialog];
        }
        public bool RenderDialogs() 
        {
            return !this.GetIntroDialog().IsEmpty() || !this.GetWinDialog().IsEmpty() || !this.GetDefeatDialog().IsEmpty() || !this.GetEpilogueDialog().IsEmpty();
        }
        public void AddIntro(String dialog)
        {
            this.IntroDialogs[this.IntroDialogs.Length - 1] = dialog;
        }
        public void AddWinDialog(String dialog)
        {
            this.WinDialogs[this.WinDialogs.Length - 1] = dialog;
        }
        public void AddDefeatedDialog(String dialog)
        {
            this.DefeatDialogs[this.DefeatDialogs.Length - 1] = dialog;
        }
        public void AddEpilogue(String dialog)
        {
            this.EpilogueDialogs[this.EpilogueDialogs.Length - 1] = dialog;
        }
    }
}
