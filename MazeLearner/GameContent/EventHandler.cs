using MazeLearner.Audio;
using MazeLearner.GameContent.BattleSystems.Questions;
using MazeLearner.GameContent.BattleSystems.Questions.English;
using MazeLearner.GameContent.Entity;
using MazeLearner.GameContent.Entity.Player;
using MazeLearner.Graphics.Particle;
using MazeLearner.Graphics.Particles;
using MazeLearner.Screen;
using MazeLearner.Worlds;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.GameContent
{
    public class EventHandler
    {
        private Main game;
        private int _x;
        private int _y;
        private int _prevx;
        private int _prevy;
        private int tick = 0;
        private int delayms = 0;
        private Direction _direction;
        private bool doGuardianScene = false;

        public Rectangle Box
        {
            get
            {
                return new Rectangle(this.X, this.Y, Main.TileSize, Main.TileSize);
            }
            set
            {
                this.X = value.X;
                this.Y = value.Y;
            }
        }
        public Direction Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }
        public int X
        {
            get { return _x; }
            set { _x = value; }
        }
        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }
        public int PrevX
        {
            get { return _prevx; }
            set { _prevx = value; }
        }
        public int PrevY
        {
            get { return _prevy; }
            set { _prevy = value; }
        }
        public EventHandler(Main game) 
        {
            this.game = game;
        }
        private PlayerEntity Player => Main.ActivePlayer;
        public void CheckEvent()
        {
            if (this.delayms > 0) this.delayms--;

            if (this.Stepped(World.Get(0), 68, 10) == true)
            {
                if (this.Player.Direction == Direction.Right && this.Player.DoInteract())
                {
                    Main.FadeAwayBegin = true;
                    Main.GameState = GameState.Cutscene;
                    Main.FadeAwayDuration = 20;
                    Main.FadeAwayOnStart = () =>
                    {
                        this.Player.Health = this.Player.MaxHealth;
                    };
                    Main.FadeAwayOnEnd = () =>
                    {
                        Main.GameState = GameState.Play;
                    };
                }
            }
            if (this.Player.MomCutscene == false && Main.GameState != GameState.Cutscene)
            {
                if (this.Stepped(World.Get(0), 15, 17) == true)
                {
                    this.MomCutscene(15, 17);
                }
                if (this.Stepped(World.Get(0), 16, 17) == true)
                {
                    this.MomCutscene(16, 17);
                }
                if (this.Stepped(World.Get(0), 17, 17) == true)
                {
                    this.MomCutscene(17, 17);
                }
                if (this.Stepped(World.Get(0), 18, 17) == true)
                {
                    this.MomCutscene(18, 17);
                }
                if (this.Stepped(World.Get(0), 19, 17) == true)
                {
                    this.MomCutscene(19, 17);
                }
            }
            if (this.Player.GoingSchoolCutscene == false && Main.GameState != GameState.Cutscene)
            {
                if (this.Stepped(World.Get(1), 42, 39) == true)
                {
                    this.SchoolCutscene(42, 39);
                }
            }
            if (this.Player.GoingSchoolCutscene == false && Main.GameState != GameState.Cutscene)
            {
                if (this.Stepped(World.Get(1), 42, 39) == true)
                {
                    this.SchoolCutscene(42, 39);
                }
            }
            if (this.Player.InSchoolCutscene == false && Main.GameState != GameState.Cutscene)
            {
                if (this.Stepped(World.Get(2), 55, 29) == true)
                {
                    this.InSchoolCutscene(55, 29);
                }
                if (this.Stepped(World.Get(2), 54, 30) == true)
                {
                    this.InSchoolCutscene(54, 30);
                }
                if (this.Stepped(World.Get(2), 54, 31) == true)
                {
                    this.InSchoolCutscene(54, 31);
                }
                if (this.Stepped(World.Get(2), 55, 32) == true)
                {
                    this.InSchoolCutscene(55, 32);
                }
            }
            if (this.Player.OnSchoolCutscene == false && Main.GameState != GameState.Cutscene)
            {
                if (this.Stepped(World.Get(3), 23, 17) == true)
                {
                    this.OnSchoolCutscene(23, 17);
                }
                if (this.Stepped(World.Get(3), 24, 17) == true)
                {
                    this.OnSchoolCutscene(24, 17);
                }
            }
            if (this.Player.GameIntroduction == false && Main.GameState != GameState.Cutscene)
            {
                if (this.Stepped(World.Get(4), 10, 51) == true)
                {
                    this.FirstMapMaze(10, 51);
                }
                if (this.Stepped(World.Get(4), 10, 52) == true)
                {
                    this.FirstMapMaze(10, 52);
                }
                if (this.Stepped(World.Get(4), 10, 53) == true)
                {
                    this.FirstMapMaze(10, 53);
                }
            }
            if (this.Player.FinishedMap0 == false && (this.Stepped(World.Get(3), 14, 19) == true || this.Stepped(World.Get(3), 13, 19) == true || this.Stepped(World.Get(3), 14, 20) || this.Stepped(World.Get(3), 14, 21) == true
                || this.Stepped(World.Get(3), 14, 22) == true))
            {
                this.Player.Objective = Objective.Get(5);
            }
            if (Main.MapIds == World.Get(3).Id)
            {
                var brendan = Main.FindNpc(3, 8);
                if (this.Player.FinishedMap0 == true && this.Player.TeacherAsking == false &&
                    (this.Stepped(World.Get(3), 23, 17) == true || this.Stepped(World.Get(3), 24, 17) == true) &&
                    Main.GameState != GameState.Cutscene)
                {
                    if (this.game.CurrentScreen == null)
                    {
                        Main.GameState = GameState.Cutscene;
                        this.game.SetScreen(new CutsceneScreen(8, () =>
                        {
                            this.Player.TeacherAsking = true;
                            this.Player.FinishedMap0 = true;
                            Main.GameState = GameState.Play;
                            PlayerEntity.SavePlayer(this.Player, Main.PlayerListPath[Main.PlayerListLoad]);
                            GameSettings.SaveSettings();
                            this.game.SetScreen(null);
                            this.tick = 0;
                        }));
                    }
                }
                if (this.Player.FinishedMap0 == false && this.Player.TeacherAsking == false &&
                    (this.Stepped(World.Get(3), 23, 17) == true || this.Stepped(World.Get(3), 24, 17) == true) &&
                    Main.GameState != GameState.Cutscene)
                {
                    Main.ActivePlayer.SetPos(5, 54);
                    Main.Tiled.LoadMap(World.Get("library"));
                    PlayerEntity.SavePlayer(Main.ActivePlayer, Main.PlayerListPath[Main.PlayerListLoad]);
                    GameSettings.SaveSettings();
                }
                if (brendan.Defeated == true)
                {
                    Main.TeacherQuestion.Clear();
                    Main.GameState = GameState.Play;
                    this.Player.TeacherAsking = false;
                    this.Player.FinishedMap0 = false;
                    this.Player.FinalBattle = false;
                    this.Player.Health = this.Player.MaxHealth;
                    this.Player.SetPos(68, 10);
                    this.Player.Day += 1;
                    this.Player.Direction = Direction.Left;
                    Main.Tiled.LoadMap(World.Get("interior"));
                    PlayerEntity.SavePlayer(this.Player, Main.PlayerListPath[Main.PlayerListLoad]);
                    GameSettings.SaveSettings();
                    this.game.SetScreen(null);
                }
                if (this.Player.TeacherAsking == true &&
                    (this.Stepped(World.Get(3), 23, 17) == true || this.Stepped(World.Get(3), 24, 17) == true) &&
                    Main.GameState != GameState.Cutscene)
                {
                    if (brendan.Defeated == false)
                    {
                        this.tick++;
                        if (this.tick == 1)
                        {
                            brendan.NpcType = NpcType.Battle;
                            brendan.BattleLevel = 2;
                            brendan.MaxHealth = 15;
                            brendan.Health = 15;
                            brendan.SetPos(22, 17);
                            brendan.ScorePointDrops = 500;
                            brendan.FacingAt(this.Player);
                            brendan.Portfolio = 8;
                            Main.FadeAwayBegin = true;
                            Main.GameState = GameState.Cutscene;
                            Main.FadeAwayDuration = 20;
                            Main.FadeAwayOnStart = () =>
                            {
                                Main.SoundEngine.Play(AudioAssets.HeavyRain.Value);
                                this.game.SetScreen(new CutsceneScreen(9, () =>
                                {
                                    if (Main.TeacherQuestion.Count > 0)
                                    {
                                        Loggers.Debug($"Adding all question has been ask during in maze");
                                        brendan.Questionaire = Main.TeacherQuestion;
                                    }
                                    else
                                    {
                                        Loggers.Debug($"Failed to create all question from previous battle, Creating random question!");

                                        for (int i = 0; i < brendan.MaxHealth; i++)
                                        {
                                            var subject = new EnglishSubject((QuestionLevel)Enum.ToObject(typeof(QuestionLevel), brendan.BattleLevel));
                                            brendan.Questionaire.Add(subject);
                                        }
                                    }
                                    Main.FinalBattle = true;
                                    Main.HideInstructionOverlay = true;
                                    this.game.SetScreen(new BattleScreen(brendan, Main.ActivePlayer));
                                }));
                            };
                        }
                    }
                }
            }
            if (Main.MapIds == World.Get(4).Id)
            {
                var books = Main.FindNpc(4, 12);
                var obstacle = Main.FindNpc(4, 11);
                var guardian = Main.FindNpc(4, 10);
                var door = Main.FindNpc(4, 11);
                if (books != null && books.Defeated == true)
                {
                    obstacle.Invisible = true;
                    this.Player.Puzzle01 = true;
                    obstacle.Active = false;
                    obstacle.SetPos(0, 0);
                }
                if (guardian != null && guardian.Defeated == true)
                {
                    Main.SoundEngine.Play(AudioAssets.ShadowCorridor.Value);
                    this.Player.FinishedMap0 = true;
                    this.Player.SetPos(23, 17);
                    this.Player.Direction = Direction.Up;
                    this.Player.Objective = Objective.Get(0);
                    Main.Tiled.LoadMap(World.Get("interior_1"));
                }
                if (door != null && books != null && books.Defeated == false && this.delayms <= 0 && this.Player.InteractedNpc != null && this.Player.InteractedNpc.whoAmI == door.whoAmI && this.Player.DoInteract())
                {
                    this.Player.Objective = Objective.Get(4);
                }
            }
        }
        private void FirstMapMaze(int x, int y)
        {
            Main.GameState = GameState.Cutscene;
            this.game.SetScreen(new CutsceneScreen(6, () =>
            {
                Main.GameState = GameState.Play;
                this.Player.GameIntroduction = true;
                this.Player.Objective = Objective.Get(3);
                this.game.SetScreen(null);
            }));
        }
        private void OnSchoolCutscene(int x, int y)
        {
            Main.FadeAwayBegin = true;
            Main.GameState = GameState.Cutscene;
            Main.FadeAwayDuration = 20;
            Main.FadeAwayOnStart = () =>
            {
                Main.SoundEngine.Play(AudioAssets.HeavyRain.Value);
            };
            Main.FadeAwayOnEnd = () =>
            {
                this.game.SetScreen(new CutsceneScreen(5, () =>
                {
                    Main.GameState = GameState.Play;
                    this.Player.OnSchoolCutscene = true;
                    Main.Tiled.LoadMap(World.Get("library"));
                    Main.ActivePlayer.SetPos(5, 54);
                    PlayerEntity.SavePlayer(Main.ActivePlayer, Main.PlayerListPath[Main.PlayerListLoad]);
                    GameSettings.SaveSettings();
                    Main.CheckpointList = Main.ActivePlayer;
                    Main.ActivePlayer.Invisible = false;
                    this.game.SetScreen(null);
                }));
            };
        }
        private void InSchoolCutscene(int x, int y)
        {
            Main.FadeAwayBegin = true;
            Main.GameState = GameState.Cutscene;
            Main.FadeAwayDuration = 20;
            Main.FadeAwayOnStart = () =>
            {
                Main.SoundEngine.Play(AudioAssets.FallSFX.Value);
            };
            Main.FadeAwayOnEnd = () =>
            {
                this.game.SetScreen(new CutsceneScreen(4, () =>
                {
                    Main.GameState = GameState.Play;
                    this.Player.InSchoolCutscene = true;
                    this.Player.Objective = Objective.Get(2);
                    Main.ActivePlayer.SetPos(x, y);
                    Main.ActivePlayer.Invisible = false;
                    this.game.SetScreen(null);
                }));
            };
        }
        private void SchoolCutscene(int x, int y)
        {
            Main.GameState = GameState.Cutscene;
            var staff = Main.FindNpc(1, 7);
            this.Player.FacingAt(staff);
            staff.FacingAt(this.Player);
            Particle.Play(ParticleType.Shocked, staff.Position);
            this.game.SetScreen(new CutsceneScreen(3, () =>
            {
                this.Player.GoingSchoolCutscene = true;

                Main.GameState = GameState.Play;
                Main.ActivePlayer.SetPos(55, 30);
                Main.ActivePlayer.Direction = Direction.Left;
                Main.Tiled.LoadMap(World.Get("school"));
                PlayerEntity.SavePlayer(Main.ActivePlayer, Main.PlayerListPath[Main.PlayerListLoad]);
                GameSettings.SaveSettings();
                this.Player.Objective = Objective.Get(2);
                this.game.SetScreen(null);
            }));
        }

        private void MomCutscene(int x, int y)
        {
            var momNpc = Main.FindNpc(0, 0);
            Main.GameState = GameState.Cutscene;
            this.game.SetScreen(new CutsceneScreen(2, () =>
            {
                this.Player.MomCutscene = true;
                Main.GameState = GameState.Play;
                this.Player.Objective = Objective.Get(1);
                this.game.SetScreen(null);
            }));
        }

        private void OnGoingSchoolCutscene()
        {

        }

        public bool Stepped(World world, int x, int y, Direction direction)
        {
            return Main.MapIds == world.Id && this.Player.Direction == direction && this.Player.InteractionBox.X / Main.TileSize == x && this.Player.InteractionBox.Y / Main.TileSize == y;
        }
        public bool Stepped(World world, int x, int y)
        {
            return Main.MapIds == world.Id && this.Player.InteractionBox.X / Main.TileSize == x && this.Player.InteractionBox.Y / Main.TileSize == y;
        }
    }
}
