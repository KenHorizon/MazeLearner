using MazeLearner.Audio;
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
        private Direction _direction;
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
                    Main.ActivePlayer.SetPos(5, 54);

                    Main.Tiled.LoadMap(World.Get("library"));
                    PlayerEntity.SavePlayer(Main.ActivePlayer, Main.PlayerListPath[Main.PlayerListIndex]);
                    GameSettings.SaveSettings();
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

                Main.Tiled.LoadMap(World.Get("school"));
                PlayerEntity.SavePlayer(Main.ActivePlayer, Main.PlayerListPath[Main.PlayerListIndex]);
                GameSettings.SaveSettings();

                this.game.SetScreen(null);
            }));
        }

        private void MomCutscene(int x, int y)
        {
            this.tick++;
            var momNpc = Main.FindNpc(0, 0);
            Main.GameState = GameState.Cutscene;

            //if (this.tick == 1)
            //{
            //    momNpc.MoveTo(Main.ActivePlayer);
            //}
            //if (momNpc.GoalReached == true)
            //{
            //    this.Player.FacingAt(momNpc);
            //    momNpc.FacingAt(Main.ActivePlayer);
            //    this.game.SetScreen(new CutsceneScreen(2, () =>
            //    {
            //        this.Player.MomCutscene = true;
            //        Main.GameState = GameState.Play;
            //        this.game.SetScreen(null);
            //        momNpc.SetPos(10, 18);
            //    }));
            //}

            Main.FadeAwayBegin = true;
            Main.FadeAwayDuration = 40;
            Main.FadeAwayOnStart = () =>
            {
                Main.SoundEngine.Play(AudioAssets.FallSFX.Value);
            };
            Main.FadeAwayOnEnd = () =>
            {
                this.game.SetScreen(new CutsceneScreen(2, () =>
                {
                    this.Player.MomCutscene = true;
                    Main.GameState = GameState.Play;
                    this.Player.Objective = Objective.Get(0);
                    this.game.SetScreen(null);
                }));
            };
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
