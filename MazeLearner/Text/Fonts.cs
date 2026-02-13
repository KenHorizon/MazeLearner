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
        public static Asset<SpriteFont> DT_XL;
        public static Asset<SpriteFont> DT_L;
        public static Asset<SpriteFont> DT_M;
        public static Asset<SpriteFont> DT_S;
        public static Asset<SpriteFont> Small;
        public static Asset<SpriteFont> Large;
        public static Asset<SpriteFont> Normal;
        public static Asset<SpriteFont> GameTitle;

        public static void LoadAll()
        {
            DT_XL = Asset<SpriteFont>.Request("Fonts/DialogTextXL");
            DT_L = Asset<SpriteFont>.Request("Fonts/DialogTextL");
            DT_M = Asset<SpriteFont>.Request("Fonts/DialogTextM");
            DT_S = Asset<SpriteFont>.Request("Fonts/DialogTextS");
            Small = Asset<SpriteFont>.Request("Fonts/Small");
            Large = Asset<SpriteFont>.Request("Fonts/Large");
            Normal = Asset<SpriteFont>.Request("Fonts/Normal");
            GameTitle = Asset<SpriteFont>.Request("Fonts/GameTitle");
        }
    }
}
