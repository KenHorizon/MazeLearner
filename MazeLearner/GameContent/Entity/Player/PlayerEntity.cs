using MazeLearner.Audio;
using MazeLearner.GameContent.Entity.Items;
using MazeLearner.GameContent.Entity.Objects;
using MazeLearner.Graphics;
using MazeLearner.Graphics.Particle;
using MazeLearner.Graphics.Particles;
using MazeLearner.Screen;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace MazeLearner.GameContent.Entity.Player
{
    public enum Gender
    {
        Male = 0,
        Female = 1
    }
    public class PlayerEntity : NPC
    {
        private int _scorePoints = 0;
        public int ScorePoints
        {
            get { return _scorePoints; }
            set { _scorePoints = value; }
        }
        private bool _playerWon = false;
        public bool PlayerWon
        {
            get { return _playerWon; }
            set { _playerWon = value; }
        }
        private static List<PlayerEntity> Players = new List<PlayerEntity>();
        internal static byte[] ENCRYPTION_KEY = new UnicodeEncoding().GetBytes("h3y_gUyZ");
        private int keyTime = 0; // this will tell if the player will move otherwise will just face to directions
        private const int keyTimeRespond = 8;  // this will tell if the player will move otherwise will just face to directions
        public Item[] Inventory = new Item[GameSettings.InventorySlot];
        public static Asset<Texture2D> WalkingM = Asset<Texture2D>.Request($"Player/Player_M_Walking");
        public static Asset<Texture2D> RunningM = Asset<Texture2D>.Request($"Player/Player_M_Running");
        public static Asset<Texture2D> WalkingF = Asset<Texture2D>.Request($"Player/Player_F_Walking");
        public static Asset<Texture2D> RunningF = Asset<Texture2D>.Request($"Player/Player_F_Running");
        public bool inventoryOpen;
        public int objectIndexs = -1;
        private int _prevMap;
        private static int PlayerIds = 0;
        private bool isDead;
        public int PrevMap
        {
            get
            {
                return _prevMap;
            }
            set
            {
                _prevMap = value;
            }
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

        // Handle all player default data
        public static PlayerEntity Get(int playerId)
        {
            return PlayerEntity.Players[playerId];
        }
        private static int CreateID()
        {
            return PlayerIds++;
        }
        public static void Register(PlayerEntity npc)
        {
            npc.whoAmI = CreateID();
            PlayerEntity.Players.Add(npc);
        }
        public static List<PlayerEntity> GetAll => PlayerEntity.Players;
        public static int Total=> PlayerEntity.Players.ToArray().Length;
        public override void Tick(GameTime gameTime)
        {
            base.Tick(gameTime);
            this.TilePosition = new Vector2(this.Position.X / (Main.TileSize / 2), this.Position.Y / (Main.TileSize / 2));
            if (this.OpenDebugOverlay() == true)
            {
                GameSettings.DebugScreen = GameSettings.DebugScreen != true;
            }

            if (this.PlayerWon == true)
            {
                Main.FadeAwayBegin = true;
                Main.FadeAwayDuration = 10;
                Main.FadeAwayOnStart = () =>
                {
                    Main.SoundEngine.Play(AudioAssets.FallSFX.Value);
                };
                Main.FadeAwayOnEnd = () =>
                {
                    Main.GameState = GameState.Play;
                    this.PlayerWon = false;
                };
            }
            // Player Reach Zero -> Player is Dead
            if (this.IsRemove == true)
            {
                if (this.isDead == false)
                {
                    this.Health = this.MaxHealth;
                }
                this.isDead = true;
            }
            else
            {
                if (this.DoInteract() && Main.GameState != GameState.Pause && this.game.currentScreen == null)
                {
                    var InteractedNpc = this.InteractedNpc;
                    var InteractedObject = this.InteractedObject;
                    if (InteractedNpc != null && this.cooldownInteraction <= 0 && InteractedNpc is InteractableNPC interactable0)
                    {
                        Main.GameState = GameState.Dialog;
                        interactable0.Interacted(this);
                    }
                    if (InteractedObject != null && InteractedObject is InteractableNPC objectInteract)
                    {
                        Main.GameState = GameState.Dialog;
                        objectInteract.Interacted(this);
                    }
                }
                EmoteHandleInput();
                this.isRunning = Main.Input.IsKeyDown(GameSettings.KeyRunning) && this.isMoving == true;
                if (this.isKeyPressed == true)
                {
                    this.keyTime++;
                }
                else
                {
                    this.keyTime = 0;
                }
                if (this.OpenInventory() && Main.IsPlay == true)
                {
                    Main.GameState = GameState.Pause;
                    this.game.SetScreen(new BagScreen());
                }
            }
        }

        private void EmoteHandleInput()
        {
            if (Main.Input.Pressed(Microsoft.Xna.Framework.Input.Keys.D1))
            {
                Particle.Play(ParticleType.Exclamation, this.Position);
            }
            if (Main.Input.Pressed(Microsoft.Xna.Framework.Input.Keys.D2))
            {
                Particle.Play(ParticleType.Silent, this.Position);
            }
            if (Main.Input.Pressed(Microsoft.Xna.Framework.Input.Keys.D2))
            {
                Particle.Play(ParticleType.Joy, this.Position);
            }
            if (Main.Input.Pressed(Microsoft.Xna.Framework.Input.Keys.D3))
            {
                Particle.Play(ParticleType.Mad, this.Position);
            }
            if (Main.Input.Pressed(Microsoft.Xna.Framework.Input.Keys.D4))
            {
                Particle.Play(ParticleType.Shocked, this.Position);
            }
            if (Main.Input.Pressed(Microsoft.Xna.Framework.Input.Keys.D5))
            {
                Particle.Play(ParticleType.Vibing, this.Position);
            }
            if (Main.Input.Pressed(Microsoft.Xna.Framework.Input.Keys.D6))
            {
                Particle.Play(ParticleType.Sad, this.Position);
            }
            if (Main.Input.Pressed(Microsoft.Xna.Framework.Input.Keys.D7))
            {
                Particle.Play(ParticleType.Grumpy, this.Position);
            }
            if (Main.Input.Pressed(Microsoft.Xna.Framework.Input.Keys.D8))
            {
                Particle.Play(ParticleType.Confused, this.Position);
            }
            if (Main.Input.Pressed(Microsoft.Xna.Framework.Input.Keys.D9))
            {
                Particle.Play(ParticleType.Love, this.Position);
            }
            if (Main.Input.Pressed(Microsoft.Xna.Framework.Input.Keys.D0))
            {
                Particle.Play(ParticleType.Happy, this.Position);
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
        public override void HandleInput()
        {
            if (Main.GameState == GameState.Pause) return;
            if (Main.GameState == GameState.Dialog) return;
            if (this.keyTime < PlayerEntity.keyTimeRespond) return;
            if (this.isKeyPressed == true)
            {
                Vector2 pos = this.Position + this.GetDirectionTarget(this.Direction);
                this.TargetPosition = this.Offset(pos);
                
                this.ApplyMovement();
            }
            else
            {
                this.isMoving = false;
            }
        }
        public bool isKeyPressed => Main.Input.IsKeyDown(GameSettings.KeyForward) || Main.Input.IsKeyDown(GameSettings.KeyDownward) || Main.Input.IsKeyDown(GameSettings.KeyLeft) || Main.Input.IsKeyDown(GameSettings.KeyRight);
        
        public override void UpdateFacing()
        {
            if (Main.IsPause || Main.IsDialog) return;
            if (this.MovementState != MovementState.Idle) return;
            if (Main.Input.IsKeyDown(GameSettings.KeyForward))
            {
                this.Direction = Direction.Up;
            }
            else if (Main.Input.IsKeyDown(GameSettings.KeyDownward))
            {
                this.Direction = Direction.Down;
            }
            else if (Main.Input.IsKeyDown(GameSettings.KeyLeft))
            {
                this.Direction = Direction.Left;
            }
            else if (Main.Input.IsKeyDown(GameSettings.KeyRight))
            {
                this.Direction = Direction.Right;
            }
        }

        public void Spawn(int x, int y)
        {
            if (this.IsAlive == false)
            {
                this.Health = 10;
                this.MaxHealth = 10;
            }
            this.Position = new Vector2(x, y);
        }

        public static void SavePlayer(PlayerEntity newPlayer, string playerPath)
        {
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
            using (FileStream fileStream = new FileStream(text, FileMode.Create))
            {
                using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
                {
                    binaryWriter.Write(Main.MapIds);
                    binaryWriter.Write(newPlayer.IsLoadedNow);
                    binaryWriter.Write(newPlayer.Name);
                    binaryWriter.Write(newPlayer.DisplayName);
                    binaryWriter.Write(newPlayer.MaxHealth);
                    binaryWriter.Write(newPlayer.Health);
                    binaryWriter.Write(newPlayer.Damage);
                    binaryWriter.Write(newPlayer.Armor);
                    binaryWriter.Write(newPlayer.Coin);
                    binaryWriter.Write(newPlayer.ScorePoints);
                    binaryWriter.Write(newPlayer.Position.X);
                    binaryWriter.Write(newPlayer.Position.Y);
                    binaryWriter.Write((int) newPlayer.Gender);
                    for (int i = 0; i < newPlayer.Inventory.Length; i++)
                    {
                        if (newPlayer.Inventory[i] == null) continue;
                        binaryWriter.Write(newPlayer.Inventory[i].GetItemId);
                    }
                    binaryWriter.Write(Main.Npcs[1].Length);
                    for (int i = 0; i < Main.Npcs[1].Length; i++)
                    {
                        if (Main.Npcs[Main.MapIds][i] == null) continue;
                        binaryWriter.Write(Main.Npcs[Main.MapIds][i].IsLoadedNow);
                        binaryWriter.Write(Main.Npcs[Main.MapIds][i].type);
                        binaryWriter.Write(Main.Npcs[Main.MapIds][i].Name);
                        binaryWriter.Write(Main.Npcs[Main.MapIds][i].DisplayName);
                        binaryWriter.Write(Main.Npcs[Main.MapIds][i].MaxHealth);
                        binaryWriter.Write(Main.Npcs[Main.MapIds][i].Health);
                        binaryWriter.Write(Main.Npcs[Main.MapIds][i].Damage);
                        binaryWriter.Write(Main.Npcs[Main.MapIds][i].Armor);
                        binaryWriter.Write(Main.Npcs[Main.MapIds][i].Coin);
                        binaryWriter.Write(Main.Npcs[Main.MapIds][i].ScorePointDrops);
                        binaryWriter.Write((int)Main.Npcs[Main.MapIds][i].NpcType);
                        binaryWriter.Write((int)Main.Npcs[Main.MapIds][i].QuestionCategory);
                        binaryWriter.Write(Main.Npcs[Main.MapIds][i].Position.X);
                        binaryWriter.Write(Main.Npcs[Main.MapIds][i].Position.Y);
                    }
                    binaryWriter.Write(Main.Particles[1].Length);
                    for (int i = 0; i < Main.Particles[1].Length; i++)
                    {
                        if (Main.Particles[Main.MapIds][i] == null) continue;
                        binaryWriter.Write(Main.Particles[Main.MapIds][i].Name);
                        binaryWriter.Write(Main.Particles[Main.MapIds][i].type);
                        binaryWriter.Write(Main.Particles[Main.MapIds][i].Active);
                        binaryWriter.Write(Main.Particles[Main.MapIds][i].whoAmI);
                        binaryWriter.Write(Main.Particles[Main.MapIds][i].Width);
                        binaryWriter.Write(Main.Particles[Main.MapIds][i].Height);
                        binaryWriter.Write(Main.Particles[Main.MapIds][i].Tick);
                        binaryWriter.Write(Main.Particles[Main.MapIds][i].Lifespan);
                        binaryWriter.Write(Main.Particles[Main.MapIds][i].Position.X);
                        binaryWriter.Write(Main.Particles[Main.MapIds][i].Position.Y);
                    }
                    binaryWriter.Close();
                }
            }
            PlayerEntity.EncryptFile(text, playerPath);
            FileUtils.Delete(text);
        }
        public static PlayerEntity LoadPlayer(string playerPath)
        {
            bool flag = false;
            if (Main.Random == null)
            {
                Main.Random = new UnifiedRandom((int)DateTime.Now.Ticks);
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
                            player.PrevMap = binaryReader.ReadInt32();
                            player.IsLoadedNow = binaryReader.ReadBoolean();
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
                            player.ScorePoints = binaryReader.ReadInt32();
                            var x = binaryReader.ReadInt32();
                            var y = binaryReader.ReadInt32();
                            player.Position = new Vector2((float)x, (float)y) / Main.TileSize;
                            player.Gender = (Gender)Enum.ToObject(typeof(Gender), binaryReader.ReadInt32());
                            for (int i = 0; i < player.Inventory.Length; i++)
                            {
                                if (player.Inventory[i] == null) continue;
                                player.Inventory[i].Get(binaryReader.ReadInt32());
                            }
                            int npcs = binaryReader.ReadInt32();
                            if (npcs > 0)
                            {
                                for (int i = 0; i < npcs; i++)
                                {
                                    if (Main.Npcs[Main.MapIds][i] == null) continue;
                                    Main.Npcs[Main.MapIds][i].IsLoadedNow = binaryReader.ReadBoolean();
                                    Main.Npcs[Main.MapIds][i].type = binaryReader.ReadInt32();
                                    Main.Npcs[Main.MapIds][i].Name = binaryReader.ReadString();
                                    Main.Npcs[Main.MapIds][i].DisplayName = binaryReader.ReadString();
                                    Main.Npcs[Main.MapIds][i].MaxHealth = binaryReader.ReadInt32();
                                    Main.Npcs[Main.MapIds][i].Health = binaryReader.ReadInt32();
                                    Main.Npcs[Main.MapIds][i].Damage = binaryReader.ReadInt32();
                                    Main.Npcs[Main.MapIds][i].Armor = binaryReader.ReadInt32();
                                    Main.Npcs[Main.MapIds][i].Coin = binaryReader.ReadInt32();
                                    Main.Npcs[Main.MapIds][i].ScorePointDrops = binaryReader.ReadInt32();
                                    Main.Npcs[Main.MapIds][i].NpcType = (NpcType)binaryReader.ReadInt32();
                                    Main.Npcs[Main.MapIds][i].QuestionCategory = (QuestionType)binaryReader.ReadInt32();
                                    var npcx = binaryReader.ReadInt64();
                                    var npcy = binaryReader.ReadInt64();
                                    Main.Npcs[Main.MapIds][i].Position = new Vector2((float)npcx, (float)npcy) / Main.TileSize;
                                }
                            }
                            int particles = binaryReader.ReadInt32();
                            if (particles > 0)
                            {
                                for (int i = 0; i < particles; i++)
                                {
                                    if (Main.Npcs[Main.MapIds][i] == null) continue;
                                    Main.Particles[Main.MapIds][i].Name = binaryReader.ReadString();
                                    Main.Particles[Main.MapIds][i].type = binaryReader.ReadInt32();
                                    Main.Particles[Main.MapIds][i].Active = binaryReader.ReadBoolean();
                                    Main.Particles[Main.MapIds][i].whoAmI = binaryReader.ReadInt32();
                                    Main.Particles[Main.MapIds][i].Width = binaryReader.ReadInt32();
                                    Main.Particles[Main.MapIds][i].Height = binaryReader.ReadInt32();
                                    Main.Particles[Main.MapIds][i].Tick = binaryReader.ReadInt32();
                                    Main.Particles[Main.MapIds][i].Lifespan = binaryReader.ReadInt32();
                                    var npcx = binaryReader.ReadInt64();
                                    var npcy = binaryReader.ReadInt64();
                                    Main.Npcs[Main.MapIds][i].Position = new Vector2((float)npcx, (float)npcy) / Main.TileSize;
                                }
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
                        result = PlayerEntity.LoadPlayer(playerPath);
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
