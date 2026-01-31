using MazeLearner.Screen.Components;
using MazeLearner.Text;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.Screen.Widgets
{
    public class Textbox : BaseTextbox
    {
        private static int padding = 40;
        private static int width = Main.Instance.WindowScreen.Width;
        private static int height = 132;
        
        public Textbox() : base(Fonts.DT_L, 0 + padding, 0, width - padding, height, 12)
        {
        }
    }
}
