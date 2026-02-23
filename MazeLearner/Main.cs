using MazeLeaner;
using MazeLearner.Audio;
using MazeLearner.GameContent;
using MazeLearner.GameContent.BattleSystems.Questions.English;
using MazeLearner.GameContent.Entity;
using MazeLearner.GameContent.Entity.AI;
using MazeLearner.GameContent.Entity.Objects;
using MazeLearner.GameContent.Entity.Player;
using MazeLearner.Graphics;
using MazeLearner.Graphics.Particle;
using MazeLearner.Screen;
using MazeLearner.Text;
using MazeLearner.Worlds;
using MazeLearner.Worlds.Tilesets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;

namespace MazeLearner
{
    public class Main : Game
    {
        public static GameState GameState = GameState.Title;
        private static Main _instance;
        public static UnifiedRandom rand = new UnifiedRandom();
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
        private const int _screenWidth = MaxTileSize * MaxScreenCol;
        private const int _screenHeight = MaxTileSize * MaxScreenRow;
        
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
        public static Viewport Viewport;
        public static Camera Camera;
        public static Rectangle WindowScreen;
        public static DayCycle DaylightCycle = DayCycle.Morning;
        public static ContentManager Content { get; set; }
        public static MouseHandler Mouse = new MouseHandler();
        public static KeyboardHandler Input = new KeyboardHandler();
        public float DeltaTime;
        public static Texture2D BlankTexture;
        public static Texture2D FlatTexture;
        public bool DrawOrUpdate;
        public  static Tiled Tiled { get; set; }
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
        public static string TextDialog; 
        public static int TextDialogNext = 0;
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
        public const int TileSize = 32;
        public static string[] PlayerListPath = new string[maxLoadPlayer];
        public static PlayerEntity[] PlayerList = new PlayerEntity[maxLoadPlayer];
        public static PlayerEntity GetActivePlayer = null;

        public static Particle[][] Particles;
        public static ObjectEntity[][] Objects;
        public static NPC[][] Npcs;
        public static ItemEntity[][] Items;
        public static PlayerEntity[] Players = new PlayerEntity[GameSettings.MultiplayerCap];
        public static bool IsPlay => Main.IsState(GameState.Play);
        public static bool IsPause => Main.IsState(GameState.Pause);
        public static bool IsCutscene => Main.IsState(GameState.Cutscene);
        public static bool IsDialog => Main.IsState(GameState.Dialog);
        public static bool IsTitle => Main.IsState(GameState.Title);
        // Originals
        //public static ObjectEntity[] Objects = new ObjectEntity[GameSettings.SpawnCap];
        //public static NPC[] NPCS = new NPC[GameSettings.SpawnCap];
        //public static ItemEntity[] Items = new ItemEntity[GameSettings.Item];
        //public static PlayerEntity[] Players = new PlayerEntity[GameSettings.MultiplayerCap];

        public static List<NPC> AllEntity = new List<NPC>();
        public static SpriteFont FontText;
        private static Asset<Texture2D>[] Background = new Asset<Texture2D>[5];
        private static Texture2D BackgroundToRender;
        public Random random = new Random();
        public static bool[] CollectiveAcquired;
        public static CollectiveItems[] Collective;
        public static Texture2D[] PlayerTexture;
        public static Texture2D[] ParticleTexture;
        public static Texture2D[] NPCTexture;
        public static int MapIds { get; set; } = 0;
        public static Pathfind PathFind;
        public static Action FadeAwayOnStart;
        public static Action FadeAwayOnEnd;
        public static bool FadeAwayBegin = false;
        public static int FadeAwayDuration = 100;
        private static int FadeAwayTick = 0; 
        private RenderTarget2D _renderTargetScreen;
        public static Cutscene[] Cutscene = new Cutscene[999];
        public static bool IsShiftPressed => Main.Input.Pressed(GameSettings.KeyRunning);
        public static bool IsSpacePressed => Main.Input.Pressed(GameSettings.KeyFastForward);

        public int ViewportWidth => Main.Graphics.Viewport.Width;
        public int ViewportHeight => Main.Graphics.Viewport.Height;
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
            this.Services.AddService(typeof(GraphicsDeviceManager), GraphicsManager);
            Main.GraphicsManager.HardwareModeSwitch = false;
            Main.GraphicsManager.PreferredBackBufferWidth = _screenWidth;
            Main.GraphicsManager.PreferredBackBufferHeight = _screenHeight;
            Main.WindowScreen = new Rectangle(0, 0, _screenWidth, _screenHeight);
            Main.GraphicsManager.IsFullScreen = false;
            this.IsMouseVisible = false;
            Main.GraphicsManager.ApplyChanges();
            Main.Content = base.Content;
            Main.Content.RootDirectory = "Content";
            this.Window.Title = Main.GameTitle;
            Main.Tiled = new Tiled(this);
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
            AudioAssets.LoadAll();
            AssetsLoader.LoadAll();
            ShaderLoader.LoadAll();
            Fonts.LoadAll();
            RegisterContent.Items();
            RegisterContent.NPCs();
            RegisterContent.Objects();
            RegisterContent.Maps();
            RegisterContent.Particles();
            EnglishQuestionBuilder.Register();
            CollectiveBuilder.Register();
            _renderTargetScreen = new RenderTarget2D(GraphicsDevice, Main.WindowScreen.Width, Main.WindowScreen.Height);
            Loggers.Debug($"Total Maps Registered: {World.Count}");
            Main.Objects = new ObjectEntity[World.Count][];
            Main.Npcs = new NPC[World.Count][];
            Main.Items = new ItemEntity[World.Count][];
            Main.Particles = new Particle[World.Count][];
            for (int i = 0; i < Main.Npcs.Length; i++)
            {
                Main.Npcs[i] = new NPC[GameSettings.SpawnCap];
            }
            for (int i = 0; i < Main.Items.Length; i++)
            {
                Main.Items[i] = new ItemEntity[GameSettings.Item];
            }
            for (int i = 0; i < Main.Objects.Length; i++)
            {
                Main.Objects[i] = new ObjectEntity[GameSettings.SpawnCap];
            }
            for (int i = 0; i < Main.Particles.Length; i++)
            {
                Main.Particles[i] = new Particle[GameSettings.SpawnCap];
            }

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
                if (Directory.Exists(Main.PlayerPath) == false)
                {
                    Directory.CreateDirectory(Main.PlayerPath);
                }
            }
            Main.NPCTexture = new Texture2D[NPC.GetAll.ToArray().Length];
            for (int i = 0; i < NPC.GetAll.ToArray().Length; i++)
            {
                Main.NPCTexture[i] = Asset<Texture2D>.Request($"NPC/NPC_{NPC.Get(i).type}").Value;
                Loggers.Debug($"{NPC.Get(i).type}|{NPC.Get(i).Name} is Registered | Texture:{Main.NPCTexture[NPC.Get(i).type].ToString()}");
            }
            Main.ParticleTexture = new Texture2D[Particle.GetCount];
            for (int i = 0; i < Particle.GetCount; i++)
            {
                Main.ParticleTexture[i] = Asset<Texture2D>.Request($"Particle_{Particle.Get(i).type}").Value;
                Loggers.Debug($"{Particle.Get(i).type}|{Particle.Get(i).Name} is Registered | Texture:{Main.ParticleTexture[Particle.Get(i).type].ToString()}");
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
            Main.Graphics = base.GraphicsDevice;
            Main.SpriteBatch = new SpriteBatch(Graphics);
            Main.BlankTexture = new Texture2D(Main.Graphics, 1, 1);
            Main.BlankTexture.SetData(new[] { Color.Transparent });
            Main.FlatTexture = new Texture2D(Main.Graphics, 1, 1);
            Main.FlatTexture.SetData(new[] { Color.White });
            Loggers.Info("All assets and core function are now loaded!");
            Main.ApplyGraphicWindowOptions();
            Main.Camera = new Camera(Main.Viewport);
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
        public static void ApplyGraphicWindowOptions()
        {
            var GMD = Main.GraphicsManager;
            var displayMode = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode;
            switch (GameSettings.WindowModeType)
            {
                case 0:
                    GMD.IsFullScreen = false;
                    Main.Instance.Window.IsBorderless = false;
                    GMD.PreferredBackBufferWidth = _screenWidth;
                    GMD.PreferredBackBufferHeight = _screenHeight;
                    Main.Viewport = new Viewport(0, 0, _screenWidth, _screenHeight);
                    GMD.ApplyChanges();
                    break;
                case 1:
                    GMD.IsFullScreen = false;
                    Main.Instance.Window.IsBorderless = true;
                    GMD.PreferredBackBufferWidth = displayMode.Width;
                    GMD.PreferredBackBufferHeight = displayMode.Height;
                    Main.Viewport = new Viewport(0, 0, displayMode.Width, displayMode.Height);
                    GMD.ApplyChanges();
                    break;
                case 2:
                    GMD.IsFullScreen = false;
                    Main.Instance.Window.IsBorderless = true;
                    GMD.PreferredBackBufferWidth = _screenWidth;
                    GMD.PreferredBackBufferHeight = _screenHeight;
                    Main.Viewport = new Viewport(0, 0, _screenWidth, _screenHeight);
                    GMD.ApplyChanges();
                    break;
            }
        }
        protected override void Update(GameTime gameTime)
        {
            if (!this.DrawOrUpdate)
            {
                Main.Mouse.Update();
                Main.Input.Update();
                Main.SoundEngine.Update();
                this.DrawOrUpdate = true;
                this.DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                this.gameCursor.Update(gameTime);
                Main.Camera.UpdateViewport(Main.Viewport);
                this.currentScreen?.Update(gameTime);
                Main.Camera.SetZoom(1.5F);
                if (Main.FadeAwayBegin == true)
                {
                    Main.FadeAwayTick++;
                    if (Main.FadeAwayTick == 1)
                    {

                        Main.FadeAwayOnStart?.Invoke();
                    }
                    if (Main.FadeAwayTick > Main.FadeAwayDuration)
                    {
                        Main.FadeAwayOnEnd?.Invoke();
                        Main.FadeAwayBegin = false;
                        Main.FadeAwayTick = 0;
                    }
                }
                this.DayAndNight();
                if (this.IsGamePlaying && Main.GetActivePlayer != null && Main.AppOnBackground == false)
                {
                    this.delayTimeToPlay++;
                    Vector2 centerized;
                    if (GameSettings.WindowModeType == (int)WindowMode.Fullscreen)
                    {
                       centerized = new Vector2(Main.WindowScreen.Width + Main.GetActivePlayer.Width, Main.WindowScreen.Height + Main.GetActivePlayer.Height) * 0.5F;
                    }
                    else
                    {
                        centerized = new Vector2(Main.Viewport.Width - Main.GetActivePlayer.Width, Main.Viewport.Height - Main.GetActivePlayer.Height) * 0.5F;
                    }
                        //Vector2 centerized = new Vector2(Main.Viewport.Width - Main.GetActivePlayer.Width, Main.Viewport.Height - Main.GetActivePlayer.Height) / 2.0F;
                    Main.Camera.SetFollow(Main.GetActivePlayer.Position - centerized);
                    if (this.delayTimeToPlay > delayTimeToPlayEnd)
                    {
                        this.delayTimeToPlay = delayTimeToPlayEnd;
                        for (int is1 = 0; is1 < Main.GameSpeed; is1++)
                        {
                            Main.Tiled.Update(gameTime);
                            for (int i = 0; i < Main.Items[1].Length; i++)
                            {
                                var items = Main.Items[Main.MapIds][i];
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
                                    if (Main.IsPause || Main.IsDialog)
                                    {
                                        player.isMoving = false;
                                    }
                                    player.Tick(gameTime);
                                }
                                else
                                {
                                    Main.Players[i] = null;
                                }
                            }
                            for (int i = 0; i < Main.Npcs[1].Length; i++)
                            {
                                var npc = Main.Npcs[Main.MapIds][i];
                                if (npc == null) continue;
                                if (npc.IsAlive)
                                {
                                    if (Main.IsPause || Main.IsDialog)
                                    {
                                        npc.isMoving = false;
                                    }
                                    npc.Tick(gameTime);
                                }
                                else
                                {
                                    Main.Npcs[Main.MapIds][i] = null;
                                }
                            }

                            for (int i = 0; i < Main.Objects[1].Length; i++)
                            {
                                var objects = Main.Objects[Main.MapIds][i];

                                objects?.Tick(gameTime);
                            }
                            for (int i = 0; i < Main.Particles[1].Length; i++)
                            {
                                var particles = Main.Particles[Main.MapIds][i];
                                if (particles == null) continue;
                                if (particles.Active == true)
                                { 
                                    particles.Update(gameTime);
                                } else
                                {
                                    Main.Particles[Main.MapIds][i] = null;
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
            if (World.Get(Main.MapIds).WorldType == WorldType.Cave)
            {
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

        public static int GameSpeed => Main.Input.IsKeyDown(GameSettings.KeyFastForward) ? 2 : 1;
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
        // Dev Note: Instead of using the viewmatrix to sync the aspect i used the rectangle to fully fit and not ruined the resolution of actual
        // widht and height of the game.
        private void ValidateDraw(GameTime gameTime)
        {
            if (!this.DrawOrUpdate && IsGraphicsDeviceAvailable == true)
            {
                this.DrawOrUpdate = true;
                GraphicsDevice.SetRenderTarget(_renderTargetScreen);
                Graphics.Clear(Color.Black);
                for (int i = 0; i < Main.Players.Length; i++)
                {
                    if (Main.Players[i] != null)
                    {
                        Main.AllEntity.Add(Main.Players[i]);
                    }
                }
                for (int i = 0; i < Main.Npcs[Main.MapIds].Length; i++)
                {
                    if (Main.Npcs[Main.MapIds][i] != null)
                    {
                        Main.AllEntity.Add(Main.Npcs[Main.MapIds][i]);
                    }
                }
                for (int i = 0; i < Main.Items[Main.MapIds].Length; i++)
                {
                    if (Main.Items[Main.MapIds][i] != null)
                    {
                        Main.AllEntity.Add(Main.Items[Main.MapIds][i]);
                    }
                }
                Main.AllEntity.Sort((a, b) => a.GetY.CompareTo(b.GetY));
                // Put everything here for sprites only
                if (this.IsGamePlaying)
                {
                    Main.DrawScreen();
                    this.graphicRenderer.Draw();
                    Main.SpriteBatch.End();

                    Main.DrawUIs();
                    this.graphicRenderer.DrawGameUIs();
                    Main.SpriteBatch.End();

                }
                Main.Draw();
                // Put everything here for related screen only
                this.currentScreen?.Draw(Main.SpriteBatch);
                Main.SpriteBatch.End();
                Main.DrawUIs();
                Main.SpriteBatch.Draw(AssetsLoader.Black.Value, Main.WindowScreen, Color.White * (MathHelper.Clamp(((float) Main.FadeAwayTick / FadeAwayDuration), 0.0F, 1.0F)));
                Main.SpriteBatch.End();
                Main.Draw();
                this.gameCursor.Draw(Main.SpriteBatch);
                Main.SpriteBatch.End();
                GraphicsDevice.SetRenderTarget(null);
                GraphicsDevice.Clear(Color.Black);
                //
                Main.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
                Main.SpriteBatch.Draw(_renderTargetScreen, new Rectangle(
                    0,
                    0,
                    Main.GraphicsManager.PreferredBackBufferWidth,
                    Main.GraphicsManager.PreferredBackBufferHeight
                    ),
                    Color.White);
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
            Main.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, transformMatrix: Main.Camera.GetViewMatrix(), effect: ShaderLoader.SpriteShaders.Value);
        }
        public static void DrawLight()
        {
            Main.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, transformMatrix: Main.Camera.GetViewMatrix(), effect: ShaderLoader.LightShaders.Value);
        }
        public static void DrawUIs()
        {
            Main.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, transformMatrix: null);
        }
        public static void Draw()
        {
            Main.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
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
            if (Main.ItemIndex < Main.Npcs.Length)
            {
                Main.Items[Main.MapIds][Main.ItemIndex] = item;
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
        public static int AddEntity(NPC npc)
        {
            //if (Main.NpcIndex < Main.Npcs[1].Length)
            //{
            //    Main.Npcs[Main.MapIds][Main.NpcIndex] = npc;
            //    Main.NpcIndex++;
            //}
            // Old;
            int num = -1;
            for (int i = 0; i < GameSettings.SpawnCap; i++)
            {
                if (Main.Npcs[Main.MapIds][i] == null)
                {
                    num = i;
                    break;
                }
            }
            if (num >= 0)
            {
                npc.whoAmI = num;
                npc.IsLoadedNow = true;
                Main.Npcs[Main.MapIds][num] = npc;
                //Loggers.Info($"Added Entity in games {Main.Npcs[Main.MapIds][num]} Unique ID:{Main.Npcs[Main.MapIds][num].whoAmI} Type ID:{Main.Npcs[Main.MapIds][num].type}");
                return num;
            }
            return GameSettings.SpawnCap;
        }
        public static int AddObject(ObjectEntity objectEntity)
        {
            int num = -1;
            for (int i = 0; i < GameSettings.SpawnCap; i++)
            {
                if (Main.Objects[Main.MapIds][i] == null)
                {
                    num = i;
                    break;
                }
            }
            if (num >= 0)
            {
                objectEntity.whoAmI = num;
                Main.Objects[Main.MapIds][num] = objectEntity;
                //Loggers.Info($"Added Object in games {Main.Objects[Main.MapIds][num]} {Main.Objects[Main.MapIds][num].whoAmI}");
                return num;
            }
            return GameSettings.SpawnCap;
        }
        public static int AddParticle(Particle particle)
        {
            int num = -1;
            for (int i = 0; i < GameSettings.SpawnCap; i++)
            {
                if (Main.Particles[Main.MapIds][i] == null || Main.Particles[Main.MapIds][i].Active == false)
                {
                    num = i;
                    break;
                }
            }
            if (num >= 0)
            {
                particle.whoAmI = num;
                particle.Active = true;
                particle.SetDefaults(particle.type);
                Main.Particles[Main.MapIds][num] = particle;
                return num;
            }
            return GameSettings.SpawnCap;
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
                    Main.PlayerList[i] = PlayerEntity.LoadPlayer(Main.PlayerListPath[i]);
                    Loggers.Debug($"Player data now loaded and read! {Main.PlayerList[i].DisplayName} | {Main.PlayerList[i].Position} | {Main.PlayerList[i].Health} {Main.PlayerList[i].MaxHealth}");
                }
            }
            Main.PlayerListLoad = num;
        }

        public static void SpawnAtIntro(PlayerEntity playerEntity)
        {
            Main.GetActivePlayer = playerEntity;
            Main.AddPlayer(playerEntity);
            Main.GameState = GameState.Play;
            Main.Tiled.LoadMap(World.Get(0));
            Main.GetActivePlayer.IsLoadedNow = true;
            Main.Instance.SetScreen(null);
        }   
        public static void Spawn(PlayerEntity playerEntity)
        {
            playerEntity.SetPos((int) playerEntity.Position.X / 32, (int) playerEntity.Position.Y / 32);
            Main.GetActivePlayer = playerEntity;
            Main.AddPlayer(playerEntity);
            Main.GameState = GameState.Play;
            Main.Tiled.LoadMap(World.Get(playerEntity.PrevMap));
            Main.Instance.SetScreen(null);
        }
        internal static void ClearEntities()
        {
            Main.AllEntity.Clear();
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
