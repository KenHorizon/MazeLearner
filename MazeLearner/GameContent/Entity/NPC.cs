using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.GameContent.Entity
{
    public class NPC : BaseEntity
    {
        private float health;
        private float armor;
        private float speed;
        private float damage;

        public int currentFrame;
        private float animationTimer;
        private const float FrameTime = 0.15f;
        private bool isMoving;
        private const int FrameCount = 4;
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
                if (value < 0) value = 0;
                armor = value;
            }
        }
        public float Damage
        {
            get { return damage; }
            set
            {
                if (value < 0) value = 0;
                damage = value;
            }
        }
        public float Speed
        {
            get { return speed; }
            set
            {
                if (value < 0) value = 0;
                speed = value;  
            }
        }
        public NPC() 
        {  
            this.SetDefaults();
        }

        public virtual void Tick()
        {

            Vector2 movement = Vector2.Zero;
            this.isMoving = movement != Vector2.Zero;
            if (isMoving)
            {
                movement.Normalize();
                animationTimer += Main.Instance.DeltaTime;
                if (animationTimer >= FrameTime)
                {
                    currentFrame = (currentFrame + 1) % FrameCount;
                    animationTimer = 0f;
                }
            }
            else
            {
                currentFrame = 0; // idle frame
            }
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

        public virtual void SetDefaults() { }
    }
}
