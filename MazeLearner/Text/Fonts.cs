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
        public static Asset<SpriteFont> DT_XL = Asset<SpriteFont>.Request("Fonts/DialogTextXL");
        public static Asset<SpriteFont> DT_L = Asset<SpriteFont>.Request("Fonts/DialogTextL");
        public static Asset<SpriteFont> DT_M = Asset<SpriteFont>.Request("Fonts/DialogTextM");
        public static Asset<SpriteFont> DT_S = Asset<SpriteFont>.Request("Fonts/DialogTextS");
        public static Asset<SpriteFont> Small = Asset<SpriteFont>.Request("Fonts/Small");
        public static Asset<SpriteFont> Large = Asset<SpriteFont>.Request("Fonts/Large");
        public static Asset<SpriteFont> Normal = Asset<SpriteFont>.Request("Fonts/Normal");
        public static Asset<SpriteFont> GameTitle = Asset<SpriteFont>.Request("Fonts/GameTitle");
    }
}
