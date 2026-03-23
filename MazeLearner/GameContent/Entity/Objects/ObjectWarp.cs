using MazeLearner.Audio;
using MazeLearner.Screen;
using MazeLearner.Worlds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MazeLearner.GameContent.Entity.Objects
{
    public class ObjectWarp : ObjectEntity
    {
        private int _x;
        private int _y;
        private int facingAfterTeleport;
        private Direction facing;
        private string _mapName;
        public int X
        {
            get {  return _x; }
            set { _x = value; }
        }
        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }
        public int FacingAfterTeleport
        {
            get { return facingAfterTeleport; }
            set { facingAfterTeleport = value; }
        }
        public Direction Facing
        {
            get { return facing; }
            set { facing = value; }
        }
        public string MapName
        {
            get { return _mapName; }
            set { _mapName = value; }
        }
        public override void SetDefaults()
        {
            base.SetDefaults();

            this.CanCollide = false;
        }
        public override void Tick(GameTime gameTime)
        {
            base.Tick(gameTime);
            if (Main.ActivePlayer.InteractionBox.Intersects(this.InteractionBox) == true
                && this.Facing == Main.ActivePlayer.Direction)
            {
                Main.GameState = GameState.Pause;
                Threads.RunAsync(() =>
                {
                    Main.FadeAwayBegin = true;
                    Main.FadeAwayDuration = 80;
                    Main.FadeAwayOnStart = () =>
                    {
                        Main.SoundEngine.Play(AudioAssets.WarpedSFX.Value);
                        Main.ActivePlayer.isMoving = false;
                        if (World.Get(this.MapName).Id != Main.MapIds)
                        {
                            Main.Tiled.LoadMap(World.Get(this.MapName));
                            if (Main.MapIds == World.Get(4).Id)
                            {
                                Main.Tiled.LoadObjects();
                            }
                        }
                    };
                    Main.FadeAwayOnEnd = () =>
                    {
                        Main.ActivePlayer.SetPos(this.X, this.Y);
                        Main.GameState = GameState.Play;
                    };
                });
                //Main.FadeAwayBegin = true;
                //Main.FadeAwayDuration = 80;
                //Main.FadeAwayOnStart = () =>
                //{
                //    Main.SoundEngine.Play(AudioAssets.WarpedSFX.Value);
                //    Main.ActivePlayer.isMoving = false;
                //    if (World.Get(this.MapName).Id != Main.MapIds)
                //    {
                //        Main.Tiled.LoadMap(World.Get(this.MapName));
                //        if (Main.MapIds == World.Get(4).Id)
                //        {
                //            Main.Tiled.LoadObjects();
                //        }
                //    }
                //};
                //Main.FadeAwayOnEnd = () =>
                //{
                //    Main.ActivePlayer.SetPos(this.X, this.Y);
                //    Main.GameState = GameState.Play;
                //};
            }
        }
    }
}
