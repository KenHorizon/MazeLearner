using Microsoft.Xna.Framework.Graphics;

namespace MazeLearner
{
    public class AssetsLoader
    {
        public static Assets<Texture2D> CursorTexture;
        public static Assets<Texture2D> TextSelected;
        public static Assets<Texture2D> BattleBG_0;
        public static Assets<Texture2D> HeartRight;
        public static Assets<Texture2D> HeartMiddle;
        public static Assets<Texture2D> HeartLeft;
        public static Assets<Texture2D> Heart;
        public static Assets<Texture2D> HealthText;
        public static Assets<Texture2D> Health;
        public static Assets<Texture2D> SelectionPlayer;
        public static Assets<Texture2D> BagMenu;
        public static Assets<Texture2D> Box0;
        public static Assets<Texture2D> Box1;
        public static Assets<Texture2D> Box2;
        public static Assets<Texture2D> Box3;
        public static Assets<Texture2D> Box4;
        public static Assets<Texture2D> Arrow;
        public static Assets<Texture2D> MalePickBox;
        public static Assets<Texture2D> FemalePickBox;
        public static Assets<Texture2D> MenuBtn0;
        public static Assets<Texture2D> MessageBox;
        public static Assets<Texture2D> InstructionBox;
        public static Assets<Texture2D> PanelBox;
        public static Assets<Texture2D> Logo;
        public static Assets<Texture2D> Splash_Layer_1;
        public static Assets<Texture2D> Splash_Layer_2;
        public static Assets<Texture2D> Splash_Layer_0;
        public static Assets<Texture2D> Slider_0;
        public static Assets<Texture2D> Slider_0_Overlay;

        public static void LoadAll()
        {
            CursorTexture = Assets<Texture2D>.Request("UI/Cursor");
            TextSelected = Assets<Texture2D>.Request("UI/TextSelected");
            BattleBG_0 = Assets<Texture2D>.Request("Battle/BattleBG_0");
            HeartRight = Assets<Texture2D>.Request("UI/Entity/Heart_Right");
            HeartMiddle = Assets<Texture2D>.Request("UI/Entity/Heart_Middle");
            HeartLeft = Assets<Texture2D>.Request("UI/Entity/Heart_Left");
            Heart = Assets<Texture2D>.Request("UI/Entity/Heart");
            HealthText = Assets<Texture2D>.Request("UI/Entity/HealthText");
            Health = Assets<Texture2D>.Request("UI/Entity/Health");
            SelectionPlayer = Assets<Texture2D>.Request("UI/SelectionPlayer");
            BagMenu = Assets<Texture2D>.Request("UI/BagMenu");
            Box0 = Assets<Texture2D>.Request("UI/Box_0");
            Box1 = Assets<Texture2D>.Request("UI/Box_1");
            Box2 = Assets<Texture2D>.Request("UI/Box_2");
            Box3 = Assets<Texture2D>.Request("UI/Box_3");
            Box4 = Assets<Texture2D>.Request("UI/Box_4");
            Arrow = Assets<Texture2D>.Request("UI/Arrow");
            MalePickBox = Assets<Texture2D>.Request("UI/MaleButton");
            FemalePickBox = Assets<Texture2D>.Request("UI/FemaleButton");
            MenuBtn0 = Assets<Texture2D>.Request("UI/MenuButton");
            MessageBox = Assets<Texture2D>.Request("UI/MessageBox");
            InstructionBox = Assets<Texture2D>.Request("UI/InstructionUI");
            PanelBox = Assets<Texture2D>.Request("UI/PanelBackground");
            Logo = Assets<Texture2D>.Request("Logo");
            Splash_Layer_1 = Assets<Texture2D>.Request("SplashScreen/Splash_0_1");
            Splash_Layer_2 = Assets<Texture2D>.Request("SplashScreen/Splash_0_2");
            Splash_Layer_0 = Assets<Texture2D>.Request("SplashScreen/Splash_0_0");
            Slider_0 = Assets<Texture2D>.Request("UI/Slider_0");
            Slider_0_Overlay = Assets<Texture2D>.Request("UI/Slider_0_Overlay");
        }
    }
}
