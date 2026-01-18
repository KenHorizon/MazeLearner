using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.World.TilesetManager
{
    public class Tilesets
    {
        public int FirstGid;
        public Dictionary<int, TilesetDefinition> Tiles = new();

        public TilesetDefinition Get(int tileId)
        {
            int localId = tileId - FirstGid;
            if (Tiles.TryGetValue(localId, out var def))
                return def;

            return new TilesetDefinition { TileId = localId };
        }
    }
}
