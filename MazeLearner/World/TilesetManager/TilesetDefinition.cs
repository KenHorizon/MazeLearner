using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.World.TilesetManager
{
    public sealed class TilesetDefinition
    {
        public int TileId;
        public bool Passable = true;
        public string Terrain;
        public string EventId;

    }
}
