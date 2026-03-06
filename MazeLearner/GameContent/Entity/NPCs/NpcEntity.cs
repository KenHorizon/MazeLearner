using MazeLearner.Audio;
using MazeLearner.GameContent.Entity.Objects;
using MazeLearner.GameContent.Entity.Player;
using MazeLearner.Screen;
using MazeLearner.Worlds;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.GameContent.Entity.NPCs
{
    public class NpcEntity : NPC
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            this.Name = $"Npc";
            this.CanCollide = true;
        }
        public override void Tick(GameTime gameTime)
        {
            base.Tick(gameTime);
            if (this.IsAlive == false)
            {
                this.DeathTimer++;
                if (this.DeathTimer > 60)
                {
                    this.IsRemove = true;
                }
            }
            if ((this.NoAI == false || this.IsRemove == false))
            {
                this.PrevFacing = this.Direction;
                this.PrevPosition = this.Position;
                this.CollideOn = false;
                switch (this.MovementState)
                {
                    case MovementState.Idle:
                        {
                            this.HandleInput();
                            break;
                        }
                    case MovementState.Walking:
                        {
                            this.UpdateMovement();
                            break;
                        }
                }
                this.UpdateFacingBox();
                this.UpdateFacing();
                this.UpdateAI();
                this.GetNpcInteracted(this.collisionBox.CheckNpc(this, this is PlayerEntity));
                if (this.MovementState == MovementState.Idle)
                {
                    this.animationState.Stop();
                }
                if (this.isMoving)
                {
                    this.animationState.Update();
                }
            }
        }
    }
}
