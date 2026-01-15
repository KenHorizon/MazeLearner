using MazeLeaner;
using MazeLeaner.Text;
using MazeLearner.GameContent.Entity;
using MazeLearner.GameContent.Entity.Player;
using MazeLearner.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Solarized;
using System;
using System.Diagnostics;
using System.IO;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;

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
        private Rectangle WindowScreen;
        public static ContentManager Content { get; set; }
        public static MouseHandler Mouse = new MouseHandler();
        public static KeyboardHandler Keyboard = new KeyboardHandler();
        public float DeltaTime;
        public static Texture2D BlankTexture;
        public static Texture2D FlatTexture;
        public bool DrawOrUpdate;
        public GraphicRenderer graphicRenderer;
        public static string SavePath => Program.SavePath;
        public static Preferences Settings = new Preferences(Main.SavePath + Path.DirectorySeparatorChar + "config.json");
        //
        public static NPC[] NPCS = new NPC[GameSettings.SpawnCap];
        public static ItemEntity[] Items = new ItemEntity[GameSettings.Item];
        public static PlayerEntity[] Players = new PlayerEntity[GameSettings.MultiplayerCap];
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
            GraphicsManager.IsFullScreen = false;
            IsMouseVisible = true;
            GraphicsManager.ApplyChanges();
            Content = base.Content;
            Content.RootDirectory = "Content";
            this.Window.Title = Main.GameTitle;
            this.graphicRenderer = new GraphicRenderer(this);
        }

        [DllImport("kernel32.dll")]
        private static extern bool AllocConsole();

        [DllImport("kernel32.dll")]
        private static extern bool FreeConsole();

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            Loggers.Msg(GraphicsAdapter.DefaultAdapter.Description);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"Time: {DateTime.Now}");
            Loggers.Msg(stringBuilder.ToString());

            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            this.Camera = new Camera(GraphicsDevice.Viewport);
            Main.Graphics = base.GraphicsDevice;
            Main.SpriteBatch = new SpriteBatch(Graphics);
            Main.BlankTexture = new Texture2D(Main.Graphics, 1, 1);
            Main.BlankTexture.SetData(new[] { Color.Transparent });
            Main.FlatTexture = new Texture2D(Main.Graphics, 1, 1);
            Main.FlatTexture.SetData(new[] { Color.White });
            this.WindowScreen = new Rectangle(0, 0, Main.ScreenWidth, Main.ScreenHeight);
            Main.AddPlayer(new PlayerEntity());
            this.RegisterQuestions();
        }

        private void RegisterQuestions()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (!this.DrawOrUpdate)
            {
                this.DrawOrUpdate = true;
                this.DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
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
                base.Update(gameTime);
                this.DrawOrUpdate = false;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            try
            {
                this.ValidateDraw(gameTime);
            }
            catch (Exception e)
            {
                Debugs.Msg($"Error {e}");
                throw;
            }
        }
        private void ValidateDraw(GameTime gameTime)
        {
            if (!this.DrawOrUpdate && IsGraphicsDeviceAvailable == true)
            {
                this.DrawOrUpdate = true;

                Graphics.Clear(Color.Black);
                Main.DrawScreens();
                // Put everything here for related screen and guis only
                //
                Main.SpriteBatch.End();

                Main.Draw();
                // Put everything here for sprites only
                this.graphicRenderer.Draw();
                //
                Main.SpriteBatch.End();
                this.DrawOrUpdate = false;
            }
        }
        public static void DrawScreens()
        {
            Main.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, transformMatrix: Main.Instance.Camera.GetViewMatrix());
        }
        public static void DrawBilinear()
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
            Console.WriteLine("Game exiting...");
            this.Exit();
        }
    }
}
