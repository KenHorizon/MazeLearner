using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.Worlds.Tilesets
{
    public class TiledOrderedLayer
    {
        public TiledLayer Layer { get; set; }
        public int Order { get; set; }
    }
}
