using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.World.TilesetManager
{
    public class TilesetMap
    {
        public int Width;
        public int Height;
        public int TileWidth;
        public int TileHeight;
        public int[,] Ground;
        public int[,] Collision;
    }
}
