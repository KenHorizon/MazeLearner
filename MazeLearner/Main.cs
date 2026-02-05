using MazeLeaner;
using MazeLearner.Audio;
using MazeLearner.GameContent.BattleSystems.Questions.English;
using MazeLearner.GameContent.Entity;
using MazeLearner.GameContent.Entity.Player;
using MazeLearner.GameContent.Setter;
using MazeLearner.Screen;
using MazeLearner.Worlds.Tilesets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Solarized;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace MazeLearner
{
    public class Main : Game
    {
        public static GameState GameState = GameState.Title;
        private static Main instance;
        public static UnifiedRandom rand;
        public const string GameID = "Maze Learner";
        public const string GameTitle = Main.GameID;
        public static Main Instance => instance;
        public const int OriginalTiles = 16;
        public const int Scale = 3;
        public const int MaxScreenCol = 24;
        public const int MaxScreenRow = 14;
        public int MaxWorldCol = 50;
        public int MaxWorldRow = 50;
        public const int MaxTileSize = OriginalTiles * Scale;
        public const int ScreenWidth = MaxTileSize * MaxScreenCol;
        public const int ScreenHeight = MaxTileSize * MaxScreenRow;
        public static bool GameInDevelopment = true;
        public static string GameVersion = "v1.0";
        public static string GameType = "Development";
        public static string GameDevelopmentVersion = "v0.33";
        public int WorldWidth = MaxTileSize * MaxScreenCol;
        public int WorldHeight = MaxTileSize * MaxScreenRow;
        public static GraphicsDeviceManager GraphicsManager { get; private set; }
        public static GraphicsDevice Graphics { get; private set; }
        public static SpriteBatch SpriteBatch { get; private set; }
        public static AudioController Audio { get; private set; }
        public Camera Camera;
        public Rectangle WindowScreen;
        public static ContentManager Content { get; set; }
        public static MouseHandler Mouse = new MouseHandler();
        public static KeyboardHandler Keyboard = new KeyboardHandler();
        public float DeltaTime;
        public static Texture2D BlankTexture;
        public static Texture2D FlatTexture;
        public bool DrawOrUpdate;
        public TilesetRenderer TilesetManager { get; set; }
        public GraphicRenderer graphicRenderer;
        private GameCursorState gameCursor;
        public GameSetter gameSetter;
        private BaseScreen currentScreen;
        public static string SavePath => Program.SavePath;
        public static string PlayerPath = Path.Combine(SavePath, "Players");
        public static string LogPath = Path.Combine(SavePath, "Logs");
        public static Preferences Settings = new Preferences(Main.SavePath + Path.DirectorySeparatorChar + "config.json");
        //
        private int delayTimeToPlay = 0;
        private const int delayTimeToPlayEnd = 20;
        private static int BgIndex = 0;
        private static int ItemIndex = 0;
        private static int PlayerIndex = 0;
        private static int NpcIndex = 0;
        private static int CollectiveIndex = 0;
        public static int MyPlayer;
        public static PlayerEntity PendingPlayer = null;
        public static int maxLoadPlayer = 5;
        public static int PlayerListLoad = 0;
        public static int PlayerListIndex = 0;
        public static string[] PlayerListPath = new string[maxLoadPlayer];
        public static PlayerEntity[] PlayerList = new PlayerEntity[maxLoadPlayer];
        public static PlayerEntity GetActivePlayer = null;
       
        public static NPC[] NPCS = new NPC[GameSettings.SpawnCap];
        public static ItemEntity[] Items = new ItemEntity[GameSettings.Item];
        public static PlayerEntity[] Players = new PlayerEntity[GameSettings.MultiplayerCap];
        public static List<NPC> AllEntity = new List<NPC>();
        private static Assets<Texture2D>[] Background = new Assets<Texture2D>[5];
        private static Texture2D BackgroundToRender;
        public Random random = new Random();
        //
        public static bool[] CollectiveAcquired;
        public static CollectiveItems[] Collective;
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
            Audio = new AudioController();
            GraphicsManager.GraphicsProfile = GraphicsProfile.HiDef;
            Services.AddService(typeof(GraphicsDeviceManager), GraphicsManager);
            GraphicsManager.PreferredBackBufferWidth = ScreenWidth;
            GraphicsManager.PreferredBackBufferHeight = ScreenHeight;
            this.WindowScreen = new Rectangle(0, 0, ScreenWidth, ScreenHeight);
            GraphicsManager.IsFullScreen = false;
            IsMouseVisible = false;
            GraphicsManager.ApplyChanges();
            Content = base.Content;
            Content.RootDirectory = "Content";
            this.Window.Title = Main.GameTitle;
            this.TilesetManager = new TilesetRenderer(this);
            this.gameCursor = new GameCursorState(this);
            this.graphicRenderer = new GraphicRenderer(this);
            this.gameSetter = new GameSetter(this);
            Exiting += OnGameExiting;
        }

        [DllImport("kernel32.dll")]
        private static extern bool AllocConsole();

        [DllImport("kernel32.dll")]
        private static extern bool FreeConsole();
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);


        public static bool IsState(GameState gameState)
        {
            return Main.GameState == gameState;
        }
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            GameSettings.LoadSettings();
            Main.LoadPlayers();
            if (GameSettings.AllowConsole)
            {
                AllocConsole();
            }
            Loggers.Msg(GraphicsAdapter.DefaultAdapter.Description);
            Loggers.Msg("Syncing the settings from config.files from docs");
            base.Initialize();
        }
        protected override void UnloadContent()
        {
            Audio.Dispose();
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
            Assets<SoundEffect>.LoadAll();
            Assets<Song>.LoadAll();
            Assets<SpriteFont>.LoadAll();
            Assets<Texture2D>.LoadAll();
            EnglishQuestionBuilder.Register();
            CollectiveBuilder.Register();
            CollectiveAcquired = new bool[CollectiveItems.CollectableItem.ToArray().Length];
            CollectiveAcquired[0] = true;
            Collective = new CollectiveItems[CollectiveItems.CollectableItem.ToArray().Length];
            Main.AddBackground(Assets<Texture2D>.Request("BG_0_0"));
            Main.AddBackground(Assets<Texture2D>.Request("BG_0_1"));
            Main.AddBackground(Assets<Texture2D>.Request("BG_0_2"));
            Main.AddBackground(Assets<Texture2D>.Request("BG_0_3"));
            Main.AddBackground(Assets<Texture2D>.Request("BG_0_4"));
            Main.AddBackground(Assets<Texture2D>.Request("BG_0_5"));
            Main.BackgroundToRender = Main.Background[random.Next(Main.Background.Length)].Value;
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            this.Camera = new Camera(GraphicsDevice.Viewport);
            Main.Graphics = base.GraphicsDevice;
            Main.SpriteBatch = new SpriteBatch(Graphics);
            Main.BlankTexture = new Texture2D(Main.Graphics, 1, 1);
            Main.BlankTexture.SetData(new[] { Color.Transparent });
            Main.FlatTexture = new Texture2D(Main.Graphics, 1, 1);
            Main.FlatTexture.SetData(new[] { Color.White });
            var player = new PlayerEntity();
            player.SetPos(29, 30);
            Main.AddPlayer(player);
            this.gameSetter.SetupGame();
            Loggers.Msg("All assets and core function are now loaded!");
            if (Main.GameState == GameState.Title)
            {
                //this.SetScreen(new PlayerCreationScreen(PlayerCreationScreen.PlayerCreationState.Play));
                this.SetScreen(new TitleScreen(TitleSequence.Splash));
            }
            if (Main.GameState == GameState.Play)
            {
                this.TilesetManager.LoadMap("lobby", AudioAssets.LobbyBGM.Value);
                Main.GameState = GameState.Play;
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (!this.DrawOrUpdate)
            {
                Main.Mouse.Update();
                Main.Keyboard.Update();
                Main.Audio.Update();
                this.DrawOrUpdate = true;
                this.DeltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                this.gameCursor.Update(gameTime);
                this.Camera.UpdateViewport(GraphicsDevice.Viewport);
                // Camera Logic
                // TODO: I need to fix whenever the player is running the camera start to doing back and forth!
                // Update: for some reason during running state of player look fine
                // :)
                this.currentScreen?.Update(gameTime);

                if (Main.IsState(GameState.Pause))
                {
                    ShaderLoader.ScreenShaders.Value.Parameters["Red"].SetValue(0.05F);
                    ShaderLoader.ScreenShaders.Value.Parameters["Green"].SetValue(0.05F);
                    ShaderLoader.ScreenShaders.Value.Parameters["Blue"].SetValue(0.05F);
                }
                else
                {
                    ShaderLoader.ScreenShaders.Value.Parameters["Red"].SetValue(1F);
                    ShaderLoader.ScreenShaders.Value.Parameters["Green"].SetValue(1F);
                    ShaderLoader.ScreenShaders.Value.Parameters["Blue"].SetValue(1F);
                }
                if (this.IsGamePlaying && Main.GetActivePlayer != null)
                {
                    this.delayTimeToPlay++;
                    Vector2 centerized = new Vector2((this.GetScreenWidth() - Main.GetActivePlayer.Width) / 2, (this.GetScreenHeight() - Main.GetActivePlayer.Height) / 2);
                    this.Camera.SetFollow(Main.GetActivePlayer.Position, centerized);
                    if (this.delayTimeToPlay > delayTimeToPlayEnd)
                    {
                        for (int is1 = 0; is1 < Main.GameSpeed; is1++)
                        {
                            if (Main.Mouse.ScrollWheelDelta > 0) this.Camera.SetZoom(MathHelper.Clamp(this.Camera.Zoom + 0.2F, 1.0F, 2.0F));
                            if (Main.Mouse.ScrollWheelDelta < 0) this.Camera.SetZoom(MathHelper.Clamp(this.Camera.Zoom - 0.2F, 1.0F, 2.0F));

                            this.TilesetManager.Update(gameTime);
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
                        }

                    }

                }
                base.Update(gameTime);
                this.DrawOrUpdate = false;
            }
        }

        private void LevelTickUpdate<T>(T[] arrays) where T : NPC
        {
            for (int i = 0; i < arrays.Length; i++)
            {
                if (!arrays[i].IsAlive)
                {
                    arrays[i] = null;
                }
            }
        }

        public static int GameSpeed => Main.Keyboard.IsKeyDown(GameSettings.KeyFastForward) ? 3 : 1;
        public bool IsGamePlaying => Main.GameState == GameState.Play || Main.GameState == GameState.Pause || Main.GameState == GameState.Dialog;
        
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
        public static void DrawSprites()
        {
            Main.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, transformMatrix: Main.Instance.Camera.GetViewMatrix(), effect: ShaderLoader.ScreenShaders.Value);

        }
        public static void DrawAlpha()
        {
            Main.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, transformMatrix: null);
        }
        public static void Draw()
        {
            Main.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
        }
        public static bool IsSpacePressed => Main.Keyboard.Pressed(GameSettings.KeyFastForward);

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
        public static void AddBackground(Assets<Texture2D> texture)
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

        public void QuitGame()
        {
            GameSettings.SaveSettings();
            Console.WriteLine("Game exiting...");
            TextWriter loggerHistory = Console.Out;
            string pathFile = Program.SavePath + Path.DirectorySeparatorChar;
            try
            {
                string fileName = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
                if (!Directory.Exists(pathFile))
                {
                    Directory.CreateDirectory(pathFile);
                }
                FileStream fs = new FileStream(LogPath, FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                Console.SetOut(sw);
                Console.WriteLine(Loggers.loggerHistory);
                Console.SetOut(loggerHistory);
                sw.Close();
                fs.Close();
            }
            catch (Exception e)
            {
                Loggers.Msg($"An exception occurred: {e}");
            }
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
            sprite.Draw(Main.BackgroundToRender, this.WindowScreen);
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
            Loggers.Msg($"Loaded files database get {num}");
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
                Loggers.Msg($"Loaded Players => Index:{PlayerList.Length} Slot:{i} Name:{PlayerList[i].name}");
            }
            Main.PlayerListLoad = num;
        }

        internal static void SpawnAtLobby(PlayerEntity playerEntity)
        {
            playerEntity.SetPos(29, 30);
            Main.GetActivePlayer = playerEntity;
            Main.AddPlayer(playerEntity);
            Main.GameState = GameState.Play;
            Main.instance.TilesetManager.LoadMap("lobby", AudioAssets.LobbyBGM.Value);
            Main.instance.SetScreen(null);

        }
    }
}
