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
        public Textbox() : base(Fonts.DT_L, 0, 0, Main.Instance.WindowScreen.Width, 500, 12)
        {
        }
    }
}
