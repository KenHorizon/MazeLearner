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
            var momNpc = Main.FindNpc(0, 0);
            momNpc.SetPos(x, y + 1);
            this.Player.FacingAt(momNpc);
            momNpc.FacingAt(this.Player);
            Main.FadeAwayBegin = true;
            Main.GameState = GameState.Cutscene;
            Main.FadeAwayDuration = 10;
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
                    this.game.SetScreen(null);
                    momNpc.SetPos(10, 18);
                }));
            };
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
