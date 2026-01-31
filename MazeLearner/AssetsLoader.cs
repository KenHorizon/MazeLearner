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
        public static Assets<Texture2D> Box2 = Assets<Texture2D>.Request("UI/Box_2");
        public static Assets<Texture2D> Arrow = Assets<Texture2D>.Request("UI/Arrow");
        public static Assets<Texture2D> MenuBtn0 = Assets<Texture2D>.Request("UI/MenuButton");
        public static Assets<Texture2D> MessageBox = Assets<Texture2D>.Request("UI/MessageBox");
        public static Assets<Texture2D> InstructionBox = Assets<Texture2D>.Request("UI/InstructionUI");
        public static Assets<Texture2D> PanelBox = Assets<Texture2D>.Request("UI/PanelBackground");
        public static Assets<Texture2D> Logo = Assets<Texture2D>.Request("Logo");
        public static Assets<Texture2D> Splash_Layer_1 = Assets<Texture2D>.Request("SplashScreen/Splash_0_1");
        public static Assets<Texture2D> Splash_Layer_2 = Assets<Texture2D>.Request("SplashScreen/Splash_0_2");
        public static Assets<Texture2D> Splash_Layer_0 = Assets<Texture2D>.Request("SplashScreen/Splash_0_0");

    }
}
