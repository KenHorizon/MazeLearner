using MazeLeaner;
using MazeLearner.Audio;
using MazeLearner.GameContent.BattleSystems.Questions.English;
using MazeLearner.GameContent.Entity;
using MazeLearner.GameContent.Entity.Monster;
using MazeLearner.GameContent.Entity.Objects;
using MazeLearner.GameContent.Entity.Player;
using MazeLearner.Graphics;
using MazeLearner.Screen;
using MazeLearner.Worlds;
using MazeLearner.Worlds.Tilesets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.IO;

namespace MazeLearner
{
    public class Main : Game
    {
        public static GameState GameState = GameState.Title;
        private static Main _instance;
        public static UnifiedRandom rand;
        public const string GameID = "Maze Learner";
        public static bool GameInDevelopment = true;
        public static string GameVersion = "v1.0";
        public static string GameType = "Development";
        public static string GameDevelopmentVersion = "v0.42";
        public const string GameTitle = Main.GameID;
        private static bool _appOnBackground;
        public static Main Instance => _instance;
        public const int OriginalTiles = 16;
        public const int Scale = 3;
        public const int MaxScreenCol = 24;
        public const int MaxScreenRow = 14;
        public int MaxWorldCol = 50;
        public int MaxWorldRow = 50;
        public const int MaxTileSize = OriginalTiles * Scale;
        public const int ScreenWidth = MaxTileSize * MaxScreenCol;
        public const int ScreenHeight = MaxTileSize * MaxScreenRow;
        
        public int WorldWidth = MaxTileSize * MaxScreenCol;
        public int WorldHeight = MaxTileSize * MaxScreenRow;
        public static bool AppOnBackground
        {
            get
            {
                return _appOnBackground;
            }
            set
            {
                _appOnBackground = value;
            }
        }
        public static GraphicsDeviceManager GraphicsManager { get; private set; }
        public static GraphicsDevice Graphics { get; private set; }
        public static SpriteBatch SpriteBatch { get; private set; }
        public static SoundEngine SoundEngine { get; private set; }
        public static Camera Camera;
        public static Rectangle WindowScreen;
        public static DayCycle DaylightCycle = DayCycle.Morning;
        public static ContentManager Content { get; set; }
        public static MouseHandler Mouse = new MouseHandler();
        public static KeyboardHandler Keyboard = new KeyboardHandler();
        public float DeltaTime;
        public static Texture2D BlankTexture;
        public static Texture2D FlatTexture;
        public bool DrawOrUpdate;
        public  static Tiled TilesetManager { get; set; }
        public Graphic graphicRenderer;
        private GameCursorState gameCursor;
        private BaseScreen currentScreen;
        private Loggers loggers = new Loggers();
        public static string SavePath => Program.SavePath;
        public static string PlayerPath = Path.Combine(SavePath, "Players");
        public static string LogPath = Path.Combine(SavePath, "Logs");
        public static Preferences Settings = new Preferences(Main.SavePath + Path.DirectorySeparatorChar + "config.json");
        //
        private static int _worldTime = 0;
        public static int WorldTime
        {
            get => _worldTime;
            set => _worldTime = value;
        }
        public const int MaxWorldTime = 24000;
        private int delayTimeToPlay = 0;
        private const int delayTimeToPlayEnd = 20;
        private static int BgIndex = 0;
        private static int ItemIndex = 0;
        private static int NpcIndex = 0;
        private static int CollectiveIndex = 0;
        private static int ObjectIndex = 0;
        public static int MyPlayer;
        public static PlayerEntity PendingPlayer = null;
        public static int maxLoadPlayer = 1000;
        public static int PlayerListLoad = 0;
        public static int PlayerListIndex = 0;
        public static string[] PlayerListPath = new string[maxLoadPlayer];
        public static PlayerEntity[] PlayerList = new PlayerEntity[maxLoadPlayer];
        public static PlayerEntity GetActivePlayer = null;
        public static ObjectEntity[] Objects = new ObjectEntity[GameSettings.SpawnCap];
         public static NPC[] NPCS = new NPC[GameSettings.SpawnCap];
        public static ItemEntity[] Items = new ItemEntity[GameSettings.Item];
        public static PlayerEntity[] Players = new PlayerEntity[GameSettings.MultiplayerCap];
        //public static NPC[,] NPCS = new NPC[9999, GameSettings.SpawnCap];
        //public static ItemEntity[,] Items = new ItemEntity[9999, GameSettings.Item];
        //public static PlayerEntity[] Players = new PlayerEntity[GameSettings.MultiplayerCap];
        public static List<NPC> AllEntity = new List<NPC>();
        private static Asset<Texture2D>[] Background = new Asset<Texture2D>[5];
        private static Texture2D BackgroundToRender;
        public Random random = new Random();
        //
        public static bool[] CollectiveAcquired;
        public static CollectiveItems[] Collective;
        public static Texture2D[] PlayerTexture = new Texture2D[Main.maxLoadPlayer];
        public static Texture2D[] NPCTexture;
        public static int MapIds { get; set; } = 0;
        public static SpriteViewMatrix GameViewMatrix;
        public static bool IsGraphicsDeviceAvailable
        {
            get
            {
                if (!_instance.GraphicsDevice.IsDisposed)
                {
                    return _instance.GraphicsDevice.GraphicsDeviceStatus == GraphicsDeviceStatus.Normal;
                }
                return false;
            }
        }
        public static Vector2 ViewPosition => new Vector2(WindowScreen.X, WindowScreen.Y) + GameViewMatrix.Translation;
        public static Vector2 ViewSize => new Vector2(ScreenWidth, ScreenHeight) / GameViewMatrix.Zoom;
        public Main()
        {
            if (_instance != null)
            {
                Loggers.Error($"Only a single Instance can be created");
                throw new InvalidOperationException($"Only a single Instance can be created");
            }
            _instance = this;
            Main.GraphicsManager = new GraphicsDeviceManager(this);
            Main.SoundEngine = new SoundEngine();
            Main.GraphicsManager.GraphicsProfile = GraphicsProfile.HiDef;
            this.Services.AddService(typeof(GraphicsDeviceManager), GraphicsManager);
            Main.GraphicsManager.PreferredBackBufferWidth = ScreenWidth;
            Main.GraphicsManager.PreferredBackBufferHeight = ScreenHeight;
            Main.WindowScreen = new Rectangle(0, 0, ScreenWidth, ScreenHeight);
            Main.GraphicsManager.IsFullScreen = false;
            this.IsMouseVisible = false;
            Main.GraphicsManager.ApplyChanges();
            Main.Content = base.Content;
            Main.Content.RootDirectory = "Content";
            this.Window.Title = Main.GameTitle;
            Main.TilesetManager = new Tiled(this);
            this.gameCursor = new GameCursorState(this);
            this.graphicRenderer = new Graphic(this);
            this.Exiting += OnGameExiting;
            this.Activated += OnGameActivated;
            this.Deactivated += OnGameDeactivated;
        }
        public static bool IsState(GameState gameState)
        {
            return Main.GameState == gameState;
        }
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            GameSettings.LoadSettings();
            Main.LoadPlayers();
            Loggers.Info(GraphicsAdapter.DefaultAdapter.Description);
            Loggers.Info("Syncing the settings from config.files from docs");
            NPC.Register(new Gloos());
            NPC.Register(new Knight(0));
            NPC.Register(new Knight(1));
            NPC.Register(new Knight(2));
            NPC.Register(new Knight(3));
            ObjectEntity.Register(new ObjectSign());
            World.Add(new World("lobby", WorldType.Outside));
            World.Add(new World("cave_entrance", WorldType.Cave));
            base.Initialize();
        }
        protected override void UnloadContent()
        {
            SoundEngine.Dispose();
            base.UnloadContent();
        }

        protected override void LoadContent()
        {
            if (Directory.Exists(Program.SavePath) == false)
            {
                Directory.CreateDirectory(Program.SavePath);
                if (Directory.Exists(Program.SavePath + "/players") == false)
                {
                    Directory.CreateDirectory(Program.SavePath + "/players");
                }
            }
            Asset<SoundEffect>.LoadAll();
            Asset<Song>.LoadAll();
            Asset<SpriteFont>.LoadAll();
            Asset<Texture2D>.LoadAll();
            EnglishQuestionBuilder.Register();
            CollectiveBuilder.Register();
            AudioAssets.LoadAll();
            AssetsLoader.LoadAll();
            ShaderLoader.LoadAll();
            Main.NPCTexture = new Texture2D[NPC.GetAll.ToArray().Length];
            Main.PlayerTexture = new Texture2D[Main.PlayerList.Length];

            for (int i = 0; i < NPC.GetAll.ToArray().Length; i++)
            {
                NPC.Get(i).whoAmI = i;
                NPC.Get(i).SetDefaults();
                Main.NPCTexture[i] = Asset<Texture2D>.Request($"NPC/NPC_{NPC.Get(i).whoAmI}").Value;
                Loggers.Debug($"{NPC.Get(i).whoAmI} {NPC.Get(i).Name} is Registered | Texture:{Main.NPCTexture[NPC.Get(i).whoAmI].ToString()}");
            }
            CollectiveAcquired = new bool[CollectiveItems.CollectableItem.ToArray().Length];
            CollectiveAcquired[0] = true;
            Collective = new CollectiveItems[CollectiveItems.CollectableItem.ToArray().Length];
            Main.AddBackground(Asset<Texture2D>.Request("BG_0_0"));
            Main.AddBackground(Asset<Texture2D>.Request("BG_0_1"));
            Main.AddBackground(Asset<Texture2D>.Request("BG_0_2"));
            Main.AddBackground(Asset<Texture2D>.Request("BG_0_3"));
            Main.AddBackground(Asset<Texture2D>.Request("BG_0_4"));
            Main.AddBackground(Asset<Texture2D>.Request("BG_0_5"));
            Main.BackgroundToRender = Main.Background[random.Next(Main.Background.Length)].Value;
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            Main.Camera = new Camera(GraphicsDevice.Viewport);
            Main.GameViewMatrix = new SpriteViewMatrix(base.GraphicsDevice);
            Main.Graphics = base.GraphicsDevice;
            Main.SpriteBatch = new SpriteBatch(Graphics);
            Main.BlankTexture = new Texture2D(Main.Graphics, 1, 1);
            Main.BlankTexture.SetData(new[] { Color.Transparent });
            Main.FlatTexture = new Texture2D(Main.Graphics, 1, 1);
            Main.FlatTexture.SetData(new[] { Color.White });
            Loggers.Info("All assets and core function are now loaded!");
            if (Main.GameState == GameState.Title)
            {
                Main.SoundEngine.Play(AudioAssets.MainMenuBGM.Value, true);
                this.SetScreen(new TitleScreen(TitleSequence.Splash));
            }
            if (Main.GameState == GameState.Play)
            {
                this.SetScreen(new TitleScreen(TitleSequence.Splash));
            }
        }
        //public void ApplyGraphicWindowOptions(WindowMode option)
        //{
        //    var GMD = Main.GraphicsManager;
        //    switch (GameSetting.WindowMode)
        //    {
        //        case WindowMode.Windowed:
        //            GMD.IsFullScreen = false;
        //            Main.Instance.Window.IsBorderless = false;
        //            GMD.ApplyChanges();
        //            break;
        //        case WindowMode.Fullscreen:
        //            GMD.IsFullScreen = true;
        //            Main.Instance.Window.IsBorderless = false;
        //            GMD.ApplyChanges();
        //            break;
        //        case WindowMode.Borderless:
        //            GMD.IsFullScreen = false;
        //            Main.Instance.Window.IsBorderless = true;
        //            GMD.ApplyChanges();
        //            break;
        //    }
        //}
        protected override void Update(GameTime gameTime)
        {
            if (!this.DrawOrUpdate)
            {
                Main.Mouse.Update();
                Main.Keyboard.Update();
                Main.SoundEngine.Update();
                this.DrawOrUpdate = true;
                this.DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                this.gameCursor.Update(gameTime);
                Main.Camera.UpdateViewport(GraphicsDevice.Viewport);
                // Camera Logic
                // TODO: I need to fix whenever the player is running the camera start to doing back and forth!
                // Update: for some reason during running state of player look fine
                // :)
                this.currentScreen?.Update(gameTime);

                this.DayAndNight();
                if (this.IsGamePlaying && Main.GetActivePlayer != null && Main.AppOnBackground == false)
                {
                    this.delayTimeToPlay++;
                    if (this.delayTimeToPlay == 1)
                    {
                        Main.WorldTime = 5000;
                    }
                    
                    Vector2 centerized = new Vector2((this.GetScreenWidth() - Main.GetActivePlayer.Width) / 2, (this.GetScreenHeight() - Main.GetActivePlayer.Height) / 2);
                    Main.Camera.SetFollow(Main.GetActivePlayer.Position, centerized);
                    if (this.delayTimeToPlay > delayTimeToPlayEnd)
                    {
                        for (int is1 = 0; is1 < Main.GameSpeed; is1++)
                        {
                            if (Main.WorldTime > Main.MaxWorldTime)
                            {
                                Main.WorldTime = 0;
                            }
                            if (Main.Mouse.ScrollWheelDelta > 0) Main.Camera.SetZoom(MathHelper.Clamp(Main.Camera.Zoom + 0.2F, 1.0F, 2.0F));
                            if (Main.Mouse.ScrollWheelDelta < 0) Main.Camera.SetZoom(MathHelper.Clamp(Main.Camera.Zoom - 0.2F, 1.0F, 2.0F));

                            Main.TilesetManager.Update(gameTime);
                            for (int i = 0; i < Main.Items.Length; i++)
                            {
                                var items = Main.Items[i];
                                if (items == null) continue;
                                if (items.IsAlive)
                                {
                                    items.Tick(gameTime);
                                }
                                else
                                {
                                    Main.Items[i] = null;
                                }
                            }
                            for (int i = 0; i < Main.Players.Length; i++)
                            {
                                var player = Main.Players[i];
                                if (player == null) continue;
                                if (player.IsAlive)
                                {
                                    player.Tick(gameTime);
                                }
                                else
                                {
                                    Main.Players[i] = null;
                                }
                            }
                            for (int i = 0; i < Main.NPCS.Length; i++)
                            {
                                var npc = Main.NPCS[i];
                                if (npc == null) continue;
                                if (npc.IsAlive)
                                {
                                    npc.Tick(gameTime);
                                }
                                else
                                {
                                    Main.NPCS[i] = null;
                                }
                            }
                            for (int i = 0; i < Main.Objects.Length; i++)
                            {
                                var @object = Main.Objects[i];
                                if (@object == null) continue;
                                if (@object.IsAlive)
                                {
                                    @object.Tick(gameTime);
                                }
                                else
                                {
                                    Main.Objects[i] = null;
                                }
                            }
                        }

                    }

                }
                base.Update(gameTime);
                this.DrawOrUpdate = false;
            }
        }

        private void DayAndNight()
        {
            //if (World.Get(Main.MapIds).WorldType == WorldType.Outside)
            //{
            //    float timeRatio = ((float)Main.WorldTime / time);
            //    Main.DaylightCycle = dayCycle;
            //    Color timeColor = Color.Lerp(start, end, timeRatio);
            //    ShaderLoader.ScreenShaders.Value.Parameters["Red"].SetValue((float)timeColor.R / 255);
            //    ShaderLoader.ScreenShaders.Value.Parameters["Green"].SetValue((float)timeColor.G / 255);
            //    ShaderLoader.ScreenShaders.Value.Parameters["Blue"].SetValue((float)timeColor.B / 255);
            //}
            if (World.Get(Main.MapIds).WorldType == WorldType.Cave)
            {
                Color nightC = new Color(41, 41, 101);
                Color timeColor = new Color(41, 41, 101);
                ShaderLoader.ScreenShaders.Value.Parameters["Red"].SetValue((float) timeColor.R / 255);
                ShaderLoader.ScreenShaders.Value.Parameters["Green"].SetValue((float) timeColor.G / 255);
                ShaderLoader.ScreenShaders.Value.Parameters["Blue"].SetValue((float) timeColor.B / 255);
            }
            else
            {
                ShaderLoader.ScreenShaders.Value.Parameters["Red"].SetValue(1.0F);
                ShaderLoader.ScreenShaders.Value.Parameters["Green"].SetValue(1.0F);
                ShaderLoader.ScreenShaders.Value.Parameters["Blue"].SetValue(1.0F);
            }
            //Loggers.Msg($"Day And Night: {Main.DaylightCycle} {timeRatio}");
        }

        public static int GameSpeed => Main.Keyboard.IsKeyDown(GameSettings.KeyFastForward) ? 4 : 1;
        public bool IsGamePlaying => Main.GameState == GameState.Play || Main.GameState == GameState.Pause || Main.GameState == GameState.Dialog;
        
        protected override void Draw(GameTime gameTime)
        {
            try
            {
                this.ValidateDraw(gameTime);
            }
            catch (Exception e)
            {
                Loggers.Error($"Error {e}");
                throw;
            }
        }
        private void ValidateDraw(GameTime gameTime)
        {
            if (!this.DrawOrUpdate && IsGraphicsDeviceAvailable == true)
            {
                this.DrawOrUpdate = true;
                Graphics.Clear(Color.Black);
                for (int i = 0; i < Main.Players.Length; i++)
                {
                    if (Main.Players[i] != null)
                    {
                        Main.AllEntity.Add(Main.Players[i]);
                    }
                }
                for (int i = 0; i < Main.NPCS.Length; i++)
                {
                    if (Main.NPCS[i] != null)
                    {
                        Main.AllEntity.Add(Main.NPCS[i]);
                    }
                }
                for (int i = 0; i < Main.Items.Length; i++)
                {
                    if (Main.Items[i] != null)
                    {
                        Main.AllEntity.Add(Main.Items[i]);
                    }
                }
                Main.AllEntity.Sort((a, b) => a.GetY.CompareTo(b.GetY));
                // Put everything here for sprites only
                if (this.IsGamePlaying)
                {
                    this.graphicRenderer.Draw();
                }
                Main.Draw();
                // Put everything here for related screen and guis only
                this.currentScreen?.Draw(Main.SpriteBatch);
                this.gameCursor.Draw(Main.SpriteBatch);
                Main.SpriteBatch.End();
                this.DrawOrUpdate = false;
            }
        }
        public static void DrawScreen()
        {
            Main.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, transformMatrix: Main.Camera.GetViewMatrix(), effect: ShaderLoader.ScreenShaders.Value);
        }
        public static void DrawSprites()
        {
            Main.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, transformMatrix: Main.Camera.GetViewMatrix(), effect: ShaderLoader.ScreenShaders.Value);
        }
        public static void DrawTiles()
        {
            Main.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, transformMatrix: Main.Camera.GetViewMatrix(), effect: ShaderLoader.TilesShaders.Value);
        }
        public static void DrawUIs()
        {
            Main.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, transformMatrix: null);

        }
        public static void Draw()
        {
            Main.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
        }
        public static bool IsShiftPressed => Main.Keyboard.Pressed(GameSettings.KeyRunning);
        public static bool IsSpacePressed => Main.Keyboard.Pressed(GameSettings.KeyFastForward);

        public int GetScreenWidth()
        {
            return Main.WindowScreen.Width;
        }
        public int GetScreenHeight()
        {
            return Main.WindowScreen.Height;
        }
        public static void AddPlayer(PlayerEntity player)
        {
            // Since this game is have no multiplayer therefore its set to zero
            Main.Players[0] = player;
        }
        public static void AddCollectables(CollectiveItems collectable)
        {
            if (Main.CollectiveIndex < Main.Collective.Length)
            {
                Main.Collective[Main.CollectiveIndex] = collectable;
                Main.CollectiveIndex++;
            }
        }
        public static void AddItem(ItemEntity item)
        {
            if (Main.ItemIndex < Main.NPCS.Length)
            {
                Main.Items[Main.ItemIndex] = item;
                Main.ItemIndex++;
            }
        }
        public static void AddBackground(Asset<Texture2D> texture)
        {
            if (Main.BgIndex < Main.Background.Length)
            {
                Main.Background[Main.BgIndex] = texture;
                Main.BgIndex++;
            }
        }
        public static void AddEntity(NPC npc)
        {
            if (Main.NpcIndex < Main.NPCS.Length)
            {
                Main.NPCS[Main.NpcIndex] = npc;
                Main.NpcIndex++;
            }
        }
        public static void AddObject(ObjectEntity objectEntity)
        {
            if (Main.ObjectIndex < Main.Objects.Length)
            {
                Main.Objects[Main.ObjectIndex] = objectEntity;
                Main.ObjectIndex++;
            }
        }
        public void QuitGame()
        {
            GameSettings.SaveSettings();
            this.loggers.Init();
            this.Exit();
        }
        private void OnGameExiting(object sender, EventArgs e)
        {
            this.QuitGame();
        }
        public void SetScreen(BaseScreen screen)
        {
            this.currentScreen = screen;
            this.currentScreen?.LoadContent();
        }
        public void RenderBackground(SpriteBatch sprite)
        {
            sprite.Draw(Main.BackgroundToRender, Main.WindowScreen);
        }
        public static string GetPlayerPathName(string playerName)
        {
            string text = "";
            for (int i = 0; i < playerName.Length; i++)
            {
                string text2 = playerName.Substring(i, 1);
                string str;
                if (text2 == "a" || text2 == "b" || text2 == "c" || text2 == "d" || text2 == "e" || text2 == "f" || text2 == "g" || text2 == "h" || text2 == "i" || text2 == "j" || text2 == "k" || text2 == "l" || text2 == "m" || text2 == "n" || text2 == "o" || text2 == "p" || text2 == "q" || text2 == "r" || text2 == "s" || text2 == "t" || text2 == "u" || text2 == "v" || text2 == "w" || text2 == "x" || text2 == "y" || text2 == "z" || text2 == "A" || text2 == "B" || text2 == "C" || text2 == "D" || text2 == "E" || text2 == "F" || text2 == "G" || text2 == "H" || text2 == "I" || text2 == "J" || text2 == "K" || text2 == "L" || text2 == "M" || text2 == "N" || text2 == "O" || text2 == "P" || text2 == "Q" || text2 == "R" || text2 == "S" || text2 == "T" || text2 == "U" || text2 == "V" || text2 == "W" || text2 == "X" || text2 == "Y" || text2 == "Z" || text2 == "1" || text2 == "2" || text2 == "3" || text2 == "4" || text2 == "5" || text2 == "6" || text2 == "7" || text2 == "8" || text2 == "9" || text2 == "0")
                {
                    str = text2;
                }
                else
                {
                    if (text2 == " ")
                    {
                        str = "_";
                    }
                    else
                    {
                        str = "-";
                    }
                }
                text += str;
            }
            if (File.Exists(string.Concat(new object[]
            {
                Main.PlayerPath,
                Path.DirectorySeparatorChar,
                text,
                ".plr"
            })))
            {
                int num = 2;
                while (File.Exists(string.Concat(new object[]
                {
                    Main.PlayerPath,
                    Path.DirectorySeparatorChar,
                    text,
                    num,
                    ".plr"
                })))
                {
                    num++;
                }
                text += num;
            }
            return string.Concat(new object[]
            {
                Main.PlayerPath,
                Path.DirectorySeparatorChar,
                text,
                ".plr"
            });
        }
        public static void LoadPlayers()
        {
            Directory.CreateDirectory(Main.PlayerPath);
            string[] files = Directory.GetFiles(Main.PlayerPath, "*.plr");
            int num = files.Length;
            if (num > Main.maxLoadPlayer)
            {
                num = Main.maxLoadPlayer;
            }
            for (int i = 0; i < num; i++)
            {
                Main.PlayerList[i] = new PlayerEntity();
                if (i < num)
                {
                    Main.PlayerListPath[i] = files[i];
                    Main.PlayerList[i] = PlayerEntity.LoadPlayerData(Main.PlayerListPath[i]);
                }
            }
            for (int i = 0; i <  Main.maxLoadPlayer; i++)
            {
                if (Main.PlayerList[i] == null) continue;
                Loggers.Info($"Loaded Players => Index:{PlayerList.Length} Slot:{i} Name:{PlayerList[i].Name}");
            }
            Main.PlayerListLoad = num;
        }

        internal static void SpawnAtLobby(PlayerEntity playerEntity)
        {
            playerEntity.SetPos(29, 30);
            Main.GetActivePlayer = playerEntity;
            Main.AddPlayer(playerEntity);
            Main.GameState = GameState.Play;
            Main.TilesetManager.LoadMap(World.Get(0), AudioAssets.LobbyBGM.Value);
            Main._instance.SetScreen(null);

        }

        internal static void ClearEntities()
        {
            AllEntity.Clear();
        }

        internal static void ClearObjects()
        {
            for (int i = 0; i < Main.Objects.Length; i++)
            {
                Main.Objects[i] = null;
            }
            Main.ObjectIndex = 0;
        }
        private void OnGameActivated(object sender, EventArgs e)
        {
            Main.AppOnBackground = false;
        }

        private void OnGameDeactivated(object sender, EventArgs e)
        {
            Main.AppOnBackground = true;
        }
    }
}
