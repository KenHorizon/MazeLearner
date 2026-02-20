using MazeLearner.Audio;
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
        private Facing facing;
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
        public Facing Facing
        {
            get { return facing; }
            set { facing = value; }
        }
        public string MapName
        {
            get { return _mapName; }
            set { _mapName = value; }
        }

        public override void Tick(GameTime gameTime)
        {
            base.Tick(gameTime);
            if (Main.GetActivePlayer.InteractionBox.Intersects(this.InteractionBox) == true && this.Facing == Main.GetActivePlayer.Facing)
            {
                Main.GameState = GameState.Pause;
                Main.FadeAwayBegin = true;
                Main.FadeAwayOnStart = () =>
                {
                    Main.SoundEngine.Play(AudioAssets.WarpedSFX.Value);
                    Main.GetActivePlayer.isMoving = false;
                };
                Main.FadeAwayOnEnd = () =>
                {
                    if (World.Get(this.MapName).Id != Main.MapIds)
                    {
                        Loggers.Debug("Map loading!!");
                        Main.Tiled.LoadMap(World.Get(this.MapName));
                    }
                    Main.GetActivePlayer.SetPos(this.X, this.Y);
                    Main.GameState = GameState.Play;
                };
            }
        }
    }
}
