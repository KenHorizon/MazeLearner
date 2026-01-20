using MazeLeaner;
using MazeLearner.GameContent.Entity;
using MazeLearner.GameContent.Entity.Player;
using MazeLearner.GameContent.Setter;
using MazeLearner.Screen;
using MazeLearner.World.TilesetManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Solarized;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MazeLearner
{
    public class Main : Game
    {
        private static Main instance;
        public const string GameID = "Maze Learner";
        public const string GameTitle = Main.GameID;
        public static Main Instance => instance;
        public const int OriginalTiles = 16;
        public const int Scale = 3;
        public const int MaxScreenCol = 24;
        public const int MaxScreenRow = 14;
        public const int MaxTileSize = OriginalTiles * Scale;
        public const int ScreenWidth = MaxTileSize * MaxScreenCol;
        public const int ScreenHeight = MaxTileSize * MaxScreenRow;
        public string GameVersion = "v1.0";
        public int WorldX;
        public int WorldY;
        public int MaxWorldCol = 50;
        public int MaxWorldRow = 50;
        public int WorldWidth = MaxTileSize * MaxScreenCol;
        public int WorldHeight = MaxTileSize * MaxScreenRow;
        public static GraphicsDeviceManager GraphicsManager { get; private set; }
        public static GraphicsDevice Graphics { get; private set; }
        public static SpriteBatch SpriteBatch { get; private set; }
        public Camera Camera;
        public Rectangle WindowScreen;
        public static ContentManager Content { get; set; }
        public static MouseHandler Mouse = new MouseHandler();
        public static KeyboardHandler Keyboard = new KeyboardHandler();
        public float DeltaTime;
        public static Texture2D BlankTexture;
        public static Texture2D FlatTexture;
        public bool DrawOrUpdate;
        private TilesetManager tilesetManager;
        public GraphicRenderer graphicRenderer;
        private GameCursorState gameCursor;
        public static GameState GameState = GameState.Title;
        public GameSetter gameSetter;
        private BaseScreen currentScreen;
        public static string SavePath => Program.SavePath;
        public static Preferences Settings = new Preferences(Main.SavePath + Path.DirectorySeparatorChar + "config.json");
        //
        public PlayerEntity ActivePlayer = null;
        public static NPC[] NPCS = new NPC[GameSettings.SpawnCap];
        public static ItemEntity[] Items = new ItemEntity[GameSettings.Item];
        public static PlayerEntity[] Players = new PlayerEntity[GameSettings.MultiplayerCap];
        public static List<NPC> AllEntity = new List<NPC>();
        //
        public static bool IsGraphicsDeviceAvailable
        {
            get
            {
                if (!instance.GraphicsDevice.IsDisposed)
                {
                    return instance.GraphicsDevice.GraphicsDeviceStatus == GraphicsDeviceStatus.Normal;
                }
                return false;
            }
        }
        public Main()
        {
            if (instance != null)
            {
                throw new InvalidOperationException($"Only a single Instance can be created");
            }
            instance = this;
            GraphicsManager = new GraphicsDeviceManager(this);
            GraphicsManager.GraphicsProfile = GraphicsProfile.HiDef;
            Services.AddService(typeof(GraphicsDeviceManager), GraphicsManager);
            GraphicsManager.PreferredBackBufferWidth = ScreenWidth;
            GraphicsManager.PreferredBackBufferHeight = ScreenHeight;
            this.WindowScreen = new Rectangle(0, 0, ScreenWidth, ScreenHeight);
            GraphicsManager.IsFullScreen = false;
            IsMouseVisible = false;
            TargetElapsedTime = TimeSpan.FromSeconds(1.0F / 60.0F);
            GraphicsManager.ApplyChanges();
            Content = base.Content;
            Content.RootDirectory = "Content";
            this.Window.Title = Main.GameTitle;
            this.tilesetManager = new TilesetManager(this);
            this.gameCursor = new GameCursorState(this);
            this.graphicRenderer = new GraphicRenderer(this);
            this.gameSetter = new GameSetter(this);
            Exiting += OnGameExiting;
        }

        [DllImport("kernel32.dll")]
        private static extern bool AllocConsole();

        [DllImport("kernel32.dll")]
        private static extern bool FreeConsole();

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            GameSettings.LoadSettings();
            if (GameSettings.AllowConsole)
            {
                AllocConsole();
            }
            Loggers.Msg(GraphicsAdapter.DefaultAdapter.Description);
            Loggers.Msg("Syncing the settings from config.files from docs");
            base.Initialize();
        }

        public TilesetManager TilesetManager => this.tilesetManager;
        protected override void LoadContent()
        {
            Assets<SpriteFont>.LoadAll();
            Assets<Texture2D>.LoadAll();
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            this.Camera = new Camera(GraphicsDevice.Viewport);
            Main.Graphics = base.GraphicsDevice;
            Main.SpriteBatch = new SpriteBatch(Graphics);
            Main.BlankTexture = new Texture2D(Main.Graphics, 1, 1);
            Main.BlankTexture.SetData(new[] { Color.Transparent });
            Main.FlatTexture = new Texture2D(Main.Graphics, 1, 1);
            Main.FlatTexture.SetData(new[] { Color.White });
            this.graphicRenderer.Load();
            Main.AddPlayer(new PlayerEntity());
            Loggers.Msg("All assets and core function are now loaded!");
            this.SetScreen(new TitleScreen());
        }

        protected override void Update(GameTime gameTime)
        {
            if (!this.DrawOrUpdate)
            {
                this.DrawOrUpdate = true;
                this.DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                Main.Mouse.Update();
                Main.Keyboard.Update();
                this.gameCursor.Update(gameTime);
                this.ActivePlayer = Main.Players[0];
                // Camera Logic
                // TODO: I need to fix whenever the player is running the camera start to doing back and forth!
                // Update: for some reason during running state of player look fine
                // :)
                if (Main.GameState == GameState.Play)
                {
                    Vector2 centerized = new Vector2((this.GetScreenWidth() - this.ActivePlayer.Width) / 2, (this.GetScreenHeight() - this.ActivePlayer.Height) / 2);
                    this.Camera.SetFollow(this.ActivePlayer, centerized);
                    if (Main.Mouse.ScrollWheelDelta > 0) this.Camera.SetZoom(MathHelper.Clamp(this.Camera.Zoom + 0.2F, 1.0F, 2.0F));
                    if (Main.Mouse.ScrollWheelDelta < 0) this.Camera.SetZoom(MathHelper.Clamp(this.Camera.Zoom - 0.2F, 1.0F, 2.0F));

                    foreach (PlayerEntity player in Main.Players)
                    {
                        if (player != null)
                        {
                            player.Tick();
                        }
                    }
                    foreach (ItemEntity item in Main.Items)
                    {
                        if (item != null)
                        {
                            item.Tick();
                        }
                    }
                    foreach (NPC npc in Main.NPCS)
                    {
                        if (npc != null)
                        {
                            npc.Tick();
                        }
                    }
                }  else
                {
                    this.currentScreen?.Update(gameTime);
                }
                base.Update(gameTime);
                this.DrawOrUpdate = false;
            }
        }
        public bool IsGamePlaying()
        {
            return Main.GameState == GameState.Play || Main.GameState == GameState.Pause;
        }
        protected override void Draw(GameTime gameTime)
        {
            try
            {
                this.ValidateDraw(gameTime);
            }
            catch (Exception e)
            {
                Loggers.Msg($"Error {e}");
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
                if (Main.GameState == GameState.Title)
                {
                    Main.Draw();
                    // Put everything here for related screen and guis only
                    this.currentScreen?.Draw(Main.SpriteBatch);
                    Main.SpriteBatch.End();
                }
                // Put everything here for sprites only
                if (this.IsGamePlaying())
                {
                    this.graphicRenderer.Draw();
                }
                Main.Draw();
                this.gameCursor.Draw(Main.SpriteBatch);
                Main.SpriteBatch.End();
                this.DrawOrUpdate = false;
            }
        }
        public static void DrawSprites()
        {
            Main.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, transformMatrix: Main.Instance.Camera.GetViewMatrix());
        }
        public static void DrawAlpha()
        {
            Main.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, transformMatrix: null);
        }
        public static void Draw()
        {
            Main.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);

        }
        public int GetScreenWidth()
        {
            return this.WindowScreen.Width;
        }
        public int GetScreenHeight()
        {
            return this.WindowScreen.Height;
        }
        public static void AddPlayer(PlayerEntity player)
        {
            // Since this game is have no multiplayer therefore its set to zero
            Main.Players[0] = player;
        }
        public static void AddItem(ItemEntity item)
        {
            Main.Items[Items.Length - 1] = item;
        }
        public static void AddEntity(NPC npc)
        {
            Main.NPCS[NPCS.Length - 1] = npc;
        }

        public void QuitGame()
        {
            GameSettings.SaveSettings();
            Console.WriteLine("Game exiting...");
            this.Exit();
        }
        private void OnGameExiting(object sender, EventArgs e)
        {
            this.QuitGame();
        }
        public void SetScreen(BaseScreen screen)
        {
            this.currentScreen = screen;
            this.currentScreen.LoadContent();
        }
    }
}
