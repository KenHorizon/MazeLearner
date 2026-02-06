using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner
{
    public class ShaderLoader
    {
        public static Assets<Effect> LightShaders = Assets<Effect>.Request("Shaders/LightEffect");
        public static Assets<Effect> ScreenShaders = Assets<Effect>.Request("Shaders/ScreenEffect");
    }
}
