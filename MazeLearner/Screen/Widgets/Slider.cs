using MazeLearner.Screen.Components;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.Screen.Widgets
{
    public class Slider : BaseSlider
    {
        public Slider(int min, int max, int defaultValue, int x, int y, int width, int height, Action onUpdate) : base(min, max, defaultValue,
            AssetsLoader.Slider_0_Overlay.Value, AssetsLoader.Slider_0.Value, x, y, width, height, onUpdate)
        {
        }
    }
}
