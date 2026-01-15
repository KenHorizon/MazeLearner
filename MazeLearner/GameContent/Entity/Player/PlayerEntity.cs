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
        private PlayerState _playerState = PlayerState.Walking;
        public PlayerState PlayerState
        {
            get { return _playerState; }
        }
        public PlayerEntity() { }

        public override void SetDefaults()
        {
            this.Health = 10;
            this.Damage = 1;
            this.Speed = 300;
            this.Armor = 0;
        }
        public override void Tick()
        {
            base.Tick();
            Vector2 velocity = Vector2.Zero;
            velocity = this.MovementPlayer(velocity);
            if (velocity != Vector2.Zero)
            {
                velocity.Normalize();
            }
            this.Position += ((velocity * this.Speed)) * Main.Instance.DeltaTime;
        }
        private Boolean PlayerRunning()
        {
            return Main.Keyboard.IsKeyDown(GameSettings.KeyRunning);
        }
        private Boolean DoInteract()
        {
            return Main.Keyboard.IsKeyDown(GameSettings.KeyInteract);
        }
        private Boolean DoInteractCancel()
        {
            return Main.Keyboard.IsKeyDown(GameSettings.KeyBack);
        }
        private Boolean OpenInventory()
        {
            return Main.Keyboard.IsKeyDown(GameSettings.KeyOpenInventory);
        }
        private Vector2 MovementPlayer(Vector2 velocity)
        {
            if (Main.Keyboard.IsKeyDown(GameSettings.KeyForward))
            {
                this.Facing = Facing.Up;
                velocity.Y -= 1;
            }
            if (Main.Keyboard.IsKeyDown(GameSettings.KeyDownward))
            {
                this.Facing = Facing.Down;
                velocity.Y += 1;
            }
            if (Main.Keyboard.IsKeyDown(GameSettings.KeyLeft))
            {
                this.Facing = Facing.Left;
                velocity.X -= 1;
            }
            if (Main.Keyboard.IsKeyDown(GameSettings.KeyRight))
            {
                this.Facing = Facing.Right;
                velocity.X += 1;
            }

            return velocity;
        }
    }
}
