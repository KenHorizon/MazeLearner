using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner
{
    public class AssetsLoader
    {
        public static Assets<Texture2D> HealthText = Assets<Texture2D>.Request("UI/Entity/HealthText");
        public static Assets<Texture2D> Health = Assets<Texture2D>.Request("UI/Entity/Health");
        public static Assets<Texture2D> Box0 = Assets<Texture2D>.Request("UI/Box_0");
        public static Assets<Texture2D> Box1 = Assets<Texture2D>.Request("UI/Box_1");
        public static Assets<Texture2D> MessageBox = Assets<Texture2D>.Request("UI/MessageBox");
    }
}
