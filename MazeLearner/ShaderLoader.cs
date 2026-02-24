using MazeLearner.Graphics;
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
        public static Asset<Effect> SpriteShaders;
        public static Asset<Effect> ScreenShaders;
        public static Asset<Effect> LightShaders;

        public static void LoadAll()
        {
            SpriteShaders = Asset<Effect>.Request("Shaders/PixelShader");
            ScreenShaders = Asset<Effect>.Request("Shaders/ScreenShader");
            LightShaders = Asset<Effect>.Request("Shaders/BloomEffect");
        }
    }
}
