using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.Worlds.Tilesets
{
    public class TilesetSlice
    {
        private Texture2D texture;
        private List<int> tileSourceRects;

        public const int TileSize = 32;
        public TilesetSlice(Texture2D texture)
        {
            this.texture = texture;
            tileSourceRects = new List<int>();
            this.SliceTexture();
        }

        private void SliceTexture()
        {
            int columns = texture.Width / TileSize;
            int rows = texture.Height / TileSize;

            int id = 0;

            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    id++;
                }
            }
        }

        public int Get(int id)
        {
            return tileSourceRects[id];
        }
    }
}
