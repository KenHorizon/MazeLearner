using MazeLearner.Audio;
using MazeLearner.GameContent.Entity.Items;
using MazeLearner.GameContent.Entity.Objects;
using MazeLearner.Graphics;
using MazeLearner.Graphics.Particle;
using MazeLearner.Graphics.Particles;
using MazeLearner.Screen;
using MazeLearner.Worlds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.PortableExecutable;
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
                this.GetObjectInteracted(this.collisionBox.CheckObject(this, false));
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
                //Particle.Play(ParticleType.Happy, this.Position);
                
                Main.Npcs[Main.MapIds][4].FollowTarget(this, 15, 100);
                Main.Npcs[Main.MapIds][4].MoveTo(this.Position);
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
                    binaryWriter.Write(newPlayer.Inventory.Length);
                    for (int i = 0; i < newPlayer.Inventory.Length; i++)
                    {
                        if (newPlayer.Inventory[i] == null) continue;
                        binaryWriter.Write(newPlayer.Inventory[i].GetItemId);
                    }
                    for (int j = 0; j < World.Count; j++)
                    {
                        for (int i = 0; i < GameSettings.SpawnCap; i++)
                        {
                            var saveNpcData = Main.Npcs[j][i];
                            binaryWriter.Write(saveNpcData != null);
                            binaryWriter.Write(saveNpcData.IsLoadedNow);
                            binaryWriter.Write(saveNpcData.type);
                            binaryWriter.Write(saveNpcData.whoAmI);
                            binaryWriter.Write(saveNpcData.Name);
                            binaryWriter.Write(saveNpcData.DisplayName);
                            binaryWriter.Write(saveNpcData.MaxHealth);
                            binaryWriter.Write(saveNpcData.Health);
                            binaryWriter.Write(saveNpcData.Damage);
                            binaryWriter.Write(saveNpcData.Armor);
                            binaryWriter.Write(saveNpcData.Coin);
                            binaryWriter.Write(saveNpcData.ScorePointDrops);
                            binaryWriter.Write((int)saveNpcData.NpcType);
                            binaryWriter.Write((int)saveNpcData.QuestionCategory);
                            binaryWriter.Write(saveNpcData.Position.X);
                            binaryWriter.Write(saveNpcData.Position.Y);
                        }
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
                            if (player.MaxHealth > NPC.CapMaxHealth)
                            {
                                player.MaxHealth = NPC.CapMaxHealth;
                            }
                            if (player.Health > player.MaxHealth)
                            {
                                player.Health = player.MaxHealth;
                            }
                            player.Damage = binaryReader.ReadInt32();
                            player.Armor = binaryReader.ReadInt32();
                            player.Coin = binaryReader.ReadInt32();
                            player.ScorePoints = binaryReader.ReadInt32();
                            var x = binaryReader.ReadSingle();
                            var y = binaryReader.ReadSingle();
                            player.Position = new Vector2((float)x, (float)y);
                            player.Gender = (Gender)Enum.ToObject(typeof(Gender), binaryReader.ReadInt32());
                            int invL = binaryReader.ReadInt32();
                            for (int i = 0; i < invL; i++)
                            {
                                if (player.Inventory[i] == null) continue;
                                player.Inventory[i].Get(binaryReader.ReadInt32());
                            }
                            for (int j = 0; j < World.Count; j++)
                            {
                                for (int i = 0; i < GameSettings.SpawnCap; i++)
                                {
                                    bool npcs = binaryReader.ReadBoolean();
                                    if (npcs == true)
                                    {
                                        NPC npc = new NPC();
                                        npc.IsLoadedNow = binaryReader.ReadBoolean();
                                        npc.type = binaryReader.ReadInt32();
                                        npc.whoAmI = binaryReader.ReadInt32();
                                        npc.Name = binaryReader.ReadString();
                                        npc.DisplayName = binaryReader.ReadString();
                                        npc.MaxHealth = binaryReader.ReadInt32();
                                        npc.Health = binaryReader.ReadInt32();
                                        npc.Damage = binaryReader.ReadInt32();
                                        npc.Armor = binaryReader.ReadInt32();
                                        npc.Coin = binaryReader.ReadInt32();
                                        npc.ScorePointDrops = binaryReader.ReadInt32();
                                        npc.NpcType = (NpcType) Enum.ToObject(typeof(NpcType), binaryReader.ReadInt32());
                                        npc.QuestionCategory = (QuestionType)Enum.ToObject(typeof(QuestionType), binaryReader.ReadInt32());

                                        var x1 = binaryReader.ReadSingle();
                                        var y1 = binaryReader.ReadSingle();
                                        npc.Position = new Vector2((float)x1, (float)y1);
                                        Main.Npcs[i][npc.whoAmI] = npc;
                                    }
                                    else
                                    {
                                        Main.Npcs[i][j] = null;
                                    }
                                }
                            }
                            binaryReader.Close();
                        }
                    }
                    FileUtils.Delete(text);
                    PlayerEntity result = player;
                    //Loggers.Info($"Player has been loaded: Player: Name:{result.Name} Max Health: {result.MaxHealth} Health:{result.Health} Coin:{result.Coin} Gender:{result.Gender}");
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
            //string s = "h3y_gUyZ";
            //UnicodeEncoding unicodeEncoding = new UnicodeEncoding();
            //byte[] bytes = unicodeEncoding.GetBytes(s);

            FileStream fileStream = new FileStream(outputFile, FileMode.Create);
            RijndaelManaged rijndaelManaged = new RijndaelManaged();
            CryptoStream cryptoStream = new CryptoStream(fileStream, rijndaelManaged.CreateEncryptor(Utils.ENCRYPTION_KEY, Utils.ENCRYPTION_KEY), CryptoStreamMode.Write);
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
            //string s = "h3y_gUyZ";
            //UnicodeEncoding unicodeEncoding = new UnicodeEncoding();
            //byte[] bytes = unicodeEncoding.GetBytes(s);

            FileStream fileStream = new FileStream(inputFile, FileMode.Open);
            RijndaelManaged rijndaelManaged = new RijndaelManaged();
            CryptoStream cryptoStream = new CryptoStream(fileStream, rijndaelManaged.CreateDecryptor(Utils.ENCRYPTION_KEY, Utils.ENCRYPTION_KEY), CryptoStreamMode.Read);
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
