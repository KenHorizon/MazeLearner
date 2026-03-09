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
        private int _cutscene;
        private int _y;
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
        public int Cutscene
        {
            get { return _cutscene; }
            set { _cutscene = value; }
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
            if (Main.ActivePlayer.Hitbox.Intersects(this.InteractionBox) == true
                && this.Facing == Main.ActivePlayer.Direction)
            {
                Main.GameState = GameState.Pause;
                Main.FadeAwayBegin = true;
                Main.FadeAwayOnStart = () =>
                {
                    if (this.Cutscene <= 0)
                    {
                        Main.SoundEngine.Play(AudioAssets.WarpedSFX.Value);
                    }
                    Main.ActivePlayer.isMoving = false;
                };
                Main.FadeAwayOnEnd = () =>
                {
                    if (this.Cutscene > 0)
                    {
                        if (this.Cutscene == 1)
                        {
                            this.game.SetScreen(new CutsceneScreen(this.Cutscene, () =>
                            {

                                if (World.Get(this.MapName).Id != Main.MapIds)
                                {
                                    Loggers.Debug("Map loading!!");
                                    Main.Tiled.LoadMap(World.Get(this.MapName));
                                }
                                Main.ActivePlayer.SetPos(this.X, this.Y);
                                Main.GameState = GameState.Play;
                            }));
                        }
                    } 
                    else
                    {
                        if (World.Get(this.MapName).Id != Main.MapIds)
                        {
                            Loggers.Debug("Map loading!!");
                            Main.Tiled.LoadMap(World.Get(this.MapName));
                        }
                        Main.ActivePlayer.SetPos(this.X, this.Y);
                        Main.GameState = GameState.Play;
                    }
                };
            }
        }
    }
}
