using MazeLearner.GameContent.Entity.Items;
using MazeLearner.Screen;
using Microsoft.VisualBasic;
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
    public enum Gender
    {
        Male,
        Female
    }
    public class PlayerEntity : NPC
    {
        private int keyTime = 0; // this will tell if the player will move otherwise will just face to directions
        private const int keyTimeRespond = 8;
        public Item[] Inventory = new Item[GameSettings.InventorySlot];
        public static Assets<Texture2D> Walking = Assets<Texture2D>.Request("Player/Player_Walking");
        public static Assets<Texture2D> Running = Assets<Texture2D>.Request("Player/Player_Running");
        public bool inventoryOpen;
        private PlayerState _playerState = PlayerState.Walking;
        public int objectIndexs = -1;
        public PlayerState PlayerState
        {
            get { return _playerState; }
            set { _playerState = value; }
        }

        public Gender Gender { get; set; }
        public override void SetDefaults()
        {
            this.langName = "Player";
            this.Health = 10;
            this.MaxHealth = 10;
            this.Damage = 1;
            this.Armor = 0;
        }
        public override void Tick(GameTime gameTime)
        {
            base.Tick(gameTime);
            GameSettings.DebugScreen = !this.OpenDebugOverlay();
            if (this.IsRemove == true)
            {
                this.GameIsntance.SetScreen(null);
            }
            if (this.DoInteract() && Main.GameState != GameState.Pause)
            {
                var InteractedNpc = this.InteractedNpc;
                if (InteractedNpc != null && InteractedNpc.cooldownInteraction <= 0 && InteractedNpc is InteractableNPC interactable)
                {
                    if (InteractedNpc.Dialogs.Length > 0)
                    {
                        Main.GameState = GameState.Dialog;
                        interactable.Interacted(this);
                    }
                    else
                    {
                        interactable.Interacted(this);
                    }
                }
            }
            if (this.isKeyPressed == true)
            {
                this.keyTime += 1;
            } else
            {
                this.keyTime = 0;
            }
            if (this.PlayerRunning())
            {
                this.PlayerState = PlayerState.Running;
            }
            else if (this.DoInteract())
            {
                this.PlayerState = PlayerState.Interacting;
            }
            else
            {
                this.PlayerState = PlayerState.Walking;
            }
            if (this.OpenInventory())
            {
                Main.GameState = GameState.Pause;
                this.GameIsntance.SetScreen(new BagScreen());
            }
        }

        public void AddInventory(Item item)
        {
            this.Inventory[this.Inventory.Length] = item;
        }

        public void RemoveItemInventory(int slot)
        {
            this.Inventory[slot] = null;
        }

        public Boolean PlayerRunning()
        {
            return Main.Keyboard.IsKeyDown(GameSettings.KeyRunning) && this.Movement != Vector2.Zero;
        }
        public Boolean OpenDebugOverlay()
        {
            return Main.Keyboard.Pressed(GameSettings.KeyDebug);
        }
        public Boolean DoInteract()
        {
            return Main.Keyboard.Pressed(GameSettings.KeyInteract);
        }
        public Boolean DoInteractCancel()
        {
            return Main.Keyboard.Pressed(GameSettings.KeyBack);
        }
        public Boolean OpenInventory()
        {
            return Main.Keyboard.Pressed(GameSettings.KeyOpenInventory0) || Main.Keyboard.Pressed(GameSettings.KeyOpenInventory1);
        }

        public PlayerEntity CopyFrom(PlayerEntity player)
        {
            PlayerEntity copy = new PlayerEntity();
            copy.Gender = player.Gender;
            copy.Facing = player.Facing;
            copy.Health = player.Health;
            copy.Damage = player.Damage;
            copy.Position = player.Position;
            copy.Inventory = player.Inventory;
            copy.InteractionBox = player.InteractionBox;
            copy.FacingBox = player.FacingBox;
            return copy;
        }
        public override Vector2 ApplyMovement(Vector2 movement)
        {
            if (Main.GameState == GameState.Pause) return Vector2.Zero;
            if (this.keyTime <= PlayerEntity.keyTimeRespond) return Vector2.Zero;
            if (this.isKeyPressed == true) {
                if (this.Facing == Facing.Up)
                {
                    movement.Y -= 1;
                }
                else if (this.Facing == Facing.Down)
                {
                    movement.Y += 1;
                }
                else if (this.Facing == Facing.Left)
                {
                    movement.X -= 1;
                }
                else if (this.Facing == Facing.Right)
                {
                    movement.X += 1;
                }
            }

            return movement;
        }
        public bool isKeyPressed => Main.Keyboard.IsKeyDown(GameSettings.KeyForward) || Main.Keyboard.IsKeyDown(GameSettings.KeyDownward) || Main.Keyboard.IsKeyDown(GameSettings.KeyLeft) || Main.Keyboard.IsKeyDown(GameSettings.KeyRight);
        public override void UpdateFacing()
        {
            if (Main.GameState == GameState.Pause) return;
            if (Main.Keyboard.IsKeyDown(GameSettings.KeyForward))
            {
                this.Facing = Facing.Up;
            }
            else if (Main.Keyboard.IsKeyDown(GameSettings.KeyDownward))
            {
                this.Facing = Facing.Down;
            }
            else if (Main.Keyboard.IsKeyDown(GameSettings.KeyLeft))
            {
                this.Facing = Facing.Left;
            }
            else if (Main.Keyboard.IsKeyDown(GameSettings.KeyRight))
            {
                this.Facing = Facing.Right;
            }
        }

        public override float RunningSpeed()
        {
            return this.PlayerRunning() ? 2.5F : 1.0F;
        }
        public override Assets<Texture2D> GetTexture()
        {
            return this.PlayerRunning() ? PlayerEntity.Running : PlayerEntity.Walking;
        }
    }
}
