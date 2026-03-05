using MazeLearner.GameContent.Entity;
using MazeLearner.Worlds;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
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
        private bool _canEventTouch;
        public bool CanEventTouch
        {
            get { return _canEventTouch; } 
            set { _canEventTouch = value; }
        }
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

        public void CheckEvent()
        {
            int xDist = Math.Abs(Main.GetActivePlayer.X - this.PrevX);
            int yDist = Math.Abs(Main.GetActivePlayer.Y - this.PrevY);
            int dist = Math.Max(xDist, yDist);
            if (dist > Main.TileSize)
            {
                this.CanEventTouch = true;
            }
            if (this.CanEventTouch == true)
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
        }
        private void MomCutscene(int x, int y)
        {
            Main.GameState = GameState.Cutscene;
            var momNpc = Main.FindNpc(0, 0);

        }


        public bool Stepped(World world, int x, int y, Direction direction)
        {
            this.CanEventTouch = false;
            return Main.MapIds == world.Id && Main.GetActivePlayer.Direction == direction && Main.GetActivePlayer.InteractionBox.Contains(this.Box);
        }
        public bool Stepped(World world, int x, int y)
        {
            this.CanEventTouch = false;
            return Main.MapIds == world.Id && Main.GetActivePlayer.InteractionBox.Contains(this.Box);
        }
    }
}
