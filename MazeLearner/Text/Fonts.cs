using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.Text
{
    public class Fonts
    {
        public static Assets<SpriteFont> Normal = Assets<SpriteFont>.Request("Fonts/Normal");
        public static Assets<SpriteFont> GameTitle = Assets<SpriteFont>.Request("Fonts/GameTitle");
    }
}
