using MazeLearner.GameContent.Data;
using MazeLearner.GameContent.Entity.Items;
using MazeLearner.Screen;
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.GameContent.Entity.Player
{
    public enum PlayerState
    {
        Walking = 0,
        Running = 1,
        Interacting = 2
    }
    public enum Gender
    {
        Male = 0,
        Female = 1
    }
    public class PlayerEntity : NPC
    {
        internal static byte[] ENCRYPTION_KEY = new UnicodeEncoding().GetBytes("h3y_gUyZ");
        private int keyTime = 0; // this will tell if the player will move otherwise will just face to directions
        private const int keyTimeRespond = 8;  // this will tell if the player will move otherwise will just face to directions
        public Item[] Inventory = new Item[GameSettings.InventorySlot];
        public static Asset<Texture2D> Walking = Asset<Texture2D>.Request("Player/Player_0");
        public static Asset<Texture2D> Running = Asset<Texture2D>.Request("Player/Player_1");
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
            this.Health = 10;
            this.MaxHealth = 10;
            this.Damage = 1;
            this.Armor = 0;
            this.Coin = 300;
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

        public bool PlayerRunning()
        {
            return Main.Input.IsKeyDown(GameSettings.KeyRunning) && this.Movement != Vector2.Zero;
        }
        public bool OpenDebugOverlay()
        {
            return Main.Input.Pressed(GameSettings.KeyDebug);
        }
        public bool DoInteract()
        {
            return Main.Input.Pressed(GameSettings.KeyInteract);
        }
        public bool DoInteractCancel()
        {
            return Main.Input.Pressed(GameSettings.KeyBack);
        }
        public bool OpenInventory()
        {
            return Main.Input.Pressed(GameSettings.KeyOpenInventory0) || Main.Input.Pressed(GameSettings.KeyOpenInventory1);
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
            if (Main.GameState == GameState.Dialog) return Vector2.Zero;
            if (this.keyTime <= PlayerEntity.keyTimeRespond) return Vector2.Zero;
            if (this.isKeyPressed == true) {
                if (this.Facing == Facing.Up)
                {
                    //movement.Y -= 1;
                    this.isMoving = true;
                }
                else if (this.Facing == Facing.Down)
                {
                    //movement.Y += 1;
                    this.isMoving = true;
                }
                else if (this.Facing == Facing.Left)
                {
                    //movement.X -= 1;
                    this.isMoving = true;
                }
                else if (this.Facing == Facing.Right)
                {
                    //movement.X += 1;
                    this.isMoving = true;
                }
            } else
            {
                this.isMoving = false;
            }
            if (this.isMoving == true)
            {
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
            } else
            {
                return Vector2.Zero;
            }
            return movement;
        }
        public bool isKeyPressed => Main.Input.IsKeyDown(GameSettings.KeyForward) || Main.Input.IsKeyDown(GameSettings.KeyDownward) || Main.Input.IsKeyDown(GameSettings.KeyLeft) || Main.Input.IsKeyDown(GameSettings.KeyRight);
        public override void UpdateFacing()
        {
            if (Main.GameState == GameState.Dialog) return;
            if (Main.GameState == GameState.Pause) return;
            if (Main.Input.IsKeyDown(GameSettings.KeyForward))
            {
                this.Facing = Facing.Up;
            }
            else if (Main.Input.IsKeyDown(GameSettings.KeyDownward))
            {
                this.Facing = Facing.Down;
            }
            else if (Main.Input.IsKeyDown(GameSettings.KeyLeft))
            {
                this.Facing = Facing.Left;
            }
            else if (Main.Input.IsKeyDown(GameSettings.KeyRight))
            {
                this.Facing = Facing.Right;
            }
        }

        public override float RunningSpeed()
        {
            return this.PlayerRunning() ? 2.5F : 1.0F;
        }
        public override Asset<Texture2D> GetTexture()
        {
            return this.PlayerRunning() ? PlayerEntity.Running : PlayerEntity.Walking;
        }


        public static void SavePlayerData(PlayerEntity newPlayer, string playerPath)
        {
            Loggers.Info($"Player Data has been saved {newPlayer.DisplayName} - {playerPath}");
            try
            {
                Directory.CreateDirectory(Main.PlayerPath);
            }
            catch
            {
            }
            if (playerPath.IsEmpty())
            {
                return;
            }
            string destFileName = playerPath + ".bak";
            if (FileUtils.Exists(playerPath))
            {
                FileUtils.Copy(playerPath, destFileName, true);
            }
            string text = playerPath + ".dat";
            newPlayer.SetDefaults();
            using (FileStream fileStream = new FileStream(text, FileMode.Create))
            {
                using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
                {
                    binaryWriter.Write(Main.WorldTime);
                    binaryWriter.Write(newPlayer.Name);
                    binaryWriter.Write(newPlayer.DisplayName);
                    binaryWriter.Write(newPlayer.MaxHealth);
                    binaryWriter.Write(newPlayer.Health);
                    binaryWriter.Write(newPlayer.Damage);
                    binaryWriter.Write(newPlayer.Armor);
                    binaryWriter.Write(newPlayer.Coin);
                    binaryWriter.Write(newPlayer.Position.X);
                    binaryWriter.Write(newPlayer.Position.Y);
                    binaryWriter.Write((int) newPlayer.Gender);
                    for (int i = 0; i < newPlayer.Inventory.Length; i++)
                    {
                        if (newPlayer.Inventory[i] == null) continue;
                        binaryWriter.Write(newPlayer.Inventory[i].GetItemId);
                    }
                    Loggers.Info($"PlayerData has been saved");
                    binaryWriter.Close();
                }
            }
            PlayerEntity.EncryptFile(text, playerPath);
            FileUtils.Delete(text);
        }
        public static PlayerEntity LoadPlayerData(string playerPath)
        {
            bool flag = false;
            if (Main.rand == null)
            {
                Main.rand = new UnifiedRandom((int)DateTime.Now.Ticks);
            }
            PlayerEntity player = new PlayerEntity();
            try
            {
                string text = playerPath + ".dat";
                flag = PlayerEntity.DecryptFile(playerPath, text);
                if (!flag)
                {
                    using (FileStream fileStream = new FileStream(text, FileMode.Open))
                    {

                        using (BinaryReader binaryReader = new BinaryReader(fileStream))
                        {
                            Main.WorldTime = binaryReader.ReadInt32();
                            player.Name = binaryReader.ReadString();
                            player.DisplayName = binaryReader.ReadString();
                            player.MaxHealth = binaryReader.ReadInt32();
                            player.Health = binaryReader.ReadInt32();
                            if (player.MaxHealth > NPC.LimitedMaxHealth)
                            {
                                player.MaxHealth = NPC.LimitedMaxHealth;
                            }
                            if (player.Health > player.MaxHealth)
                            {
                                player.Health = player.MaxHealth;
                            }
                            player.Damage = binaryReader.ReadInt32();
                            player.Armor = binaryReader.ReadInt32();
                            player.Coin = binaryReader.ReadInt32();
                            player.Position = new Vector2(binaryReader.ReadInt32(), binaryReader.ReadInt32());
                            player.Gender = (Gender)Enum.ToObject(typeof(Gender), binaryReader.ReadInt32());
                            for (int i = 0; i < player.Inventory.Length; i++)
                            {
                                if (player.Inventory[i] == null) continue;
                                player.Inventory[i].Get(binaryReader.ReadInt32());
                            }
                            binaryReader.Close();
                        }
                    }
                    FileUtils.Delete(text);
                    PlayerEntity result = player;
                    Loggers.Info($"Player has been loaded: Player: Name:{result.Name} Max Health: {result.MaxHealth} Health:{result.Health} Coin:{result.Coin} Gender:{result.Gender}");

                    return result;
                }
            }
            catch
            {
                flag = true;
            }
            if (flag)
            {
                try
                {
                    string text2 = playerPath + ".bak";
                    PlayerEntity result;
                    if (FileUtils.Exists(text2))
                    {
                        FileUtils.Delete(playerPath);
                        FileUtils.Move(text2, playerPath);
                        result = PlayerEntity.LoadPlayerData(playerPath);
                        return result;
                    }
                    result = new PlayerEntity();
                    return result;
                }
                catch
                {
                    PlayerEntity result = new PlayerEntity();
                    return result;
                }
            }
            return new PlayerEntity();
        }
        private static void EncryptFile(string inputFile, string outputFile)
        {
            string s = "h3y_gUyZ";
            UnicodeEncoding unicodeEncoding = new UnicodeEncoding();
            byte[] bytes = unicodeEncoding.GetBytes(s);
            FileStream fileStream = new FileStream(outputFile, FileMode.Create);
            RijndaelManaged rijndaelManaged = new RijndaelManaged();
            CryptoStream cryptoStream = new CryptoStream(fileStream, rijndaelManaged.CreateEncryptor(bytes, bytes), CryptoStreamMode.Write);
            FileStream fileStream2 = new FileStream(inputFile, FileMode.Open);
            int num;
            while ((num = fileStream2.ReadByte()) != -1)
            {
                cryptoStream.WriteByte((byte)num);
            }
            fileStream2.Close();
            cryptoStream.Close();
            fileStream.Close();
        }
        private static bool DecryptFile(string inputFile, string outputFile)
        {
            string s = "h3y_gUyZ";
            UnicodeEncoding unicodeEncoding = new UnicodeEncoding();
            byte[] bytes = unicodeEncoding.GetBytes(s);
            FileStream fileStream = new FileStream(inputFile, FileMode.Open);
            RijndaelManaged rijndaelManaged = new RijndaelManaged();
            CryptoStream cryptoStream = new CryptoStream(fileStream, rijndaelManaged.CreateDecryptor(bytes, bytes), CryptoStreamMode.Read);
            FileStream fileStream2 = new FileStream(outputFile, FileMode.Create);
            try
            {
                int num;
                while ((num = cryptoStream.ReadByte()) != -1)
                {
                    fileStream2.WriteByte((byte)num);
                }
                fileStream2.Close();
                cryptoStream.Close();
                fileStream.Close();
            }
            catch
            {
                fileStream2.Close();
                fileStream.Close();
                File.Delete(outputFile);
                return true;
            }
            return false;
        }
        public bool HasItem(int type)
        {
            for (int i = 0; i < Inventory.Length; i++)
            {
                if (type == this.Inventory[i].GetItemId)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
