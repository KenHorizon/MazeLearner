using MazeLearner.Graphics;
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
        public static Asset<SpriteFont> Text;
        public static Asset<SpriteFont> Dialog;

        public static void LoadAll()
        {
            Text = Asset<SpriteFont>.Request("Fonts/Text");
            Dialog = Asset<SpriteFont>.Request("Fonts/InputBoxText");
        }
    }
}
