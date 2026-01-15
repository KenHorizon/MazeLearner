using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.GameContent.Entity.Player
{
    public enum PlayerState
    {
        Walking,
        Running,
        Interacting
    }

    public class PlayerEntity : NPC
    {   
        public static Assets<Texture2D> Walking = Assets<Texture2D>.Request("Player/Player_Walking");
        public bool inventoryOpen;
        private PlayerState _playerState = PlayerState.Walking;
        public PlayerState PlayerState
        {
            get { return _playerState; }
        }

        public override void SetDefaults()
        {
            this.Health = 10;
            this.Damage = 1;
            this.Speed = 30;
            this.Armor = 0;
        }
        public override void Tick()
        {
            base.Tick();
            if (this.OpenInventory())
            {
                this.inventoryOpen = !this.inventoryOpen;
                Debugs.Msg($"Player Open a Inventory");
            }
        }

        public Boolean PlayerRunning()
        {
            return Main.Keyboard.IsKeyDown(GameSettings.KeyRunning);
        }
        public Boolean DoInteract()
        {
            return Main.Keyboard.IsKeyDown(GameSettings.KeyInteract);
        }
        public Boolean DoInteractCancel()
        {
            return Main.Keyboard.IsKeyDown(GameSettings.KeyBack);
        }
        public Boolean OpenInventory()
        {
            return Main.Keyboard.IsKeyDown(GameSettings.KeyOpenInventory);
        }
        public override Vector2 ApplyMovement(Vector2 velocity)
        {
            if (Main.Keyboard.IsKeyDown(GameSettings.KeyForward))
            {
                this.Facing = Facing.Up;
                velocity.Y -= 1 ;
            }
            else if (Main.Keyboard.IsKeyDown(GameSettings.KeyDownward))
            {
                this.Facing = Facing.Down;
                velocity.Y += 1;
            }
            else if (Main.Keyboard.IsKeyDown(GameSettings.KeyLeft))
            {
                this.Facing = Facing.Left;
                velocity.X -= 1;
            }
            else if (Main.Keyboard.IsKeyDown(GameSettings.KeyRight))
            {
                this.Facing = Facing.Right;
                velocity.X += 1;
            }

            return velocity;
        }
        public override float RunningSpeed()
        {
            return this.PlayerRunning() ? 2.5F : 1.0F;
        }
    }
}
