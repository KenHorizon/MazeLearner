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
        public static Assets<SpriteFont> DT_L = Assets<SpriteFont>.Request("Fonts/DialogTextL");
        public static Assets<SpriteFont> DT_M = Assets<SpriteFont>.Request("Fonts/DialogTextM");
        public static Assets<SpriteFont> DT_S = Assets<SpriteFont>.Request("Fonts/DialogTextS");
        public static Assets<SpriteFont> Small = Assets<SpriteFont>.Request("Fonts/Small");
        public static Assets<SpriteFont> Large = Assets<SpriteFont>.Request("Fonts/Large");
        public static Assets<SpriteFont> Normal = Assets<SpriteFont>.Request("Fonts/Normal");
        public static Assets<SpriteFont> GameTitle = Assets<SpriteFont>.Request("Fonts/GameTitle");
    }
}
