using MazeLearner.Graphics;
using Microsoft.Xna.Framework.Graphics;

namespace MazeLearner
{
    public class AssetsLoader
    {
        public static Asset<Texture2D> SelectedBox;
        public static Asset<Texture2D> IntroOverlay;
        public static Asset<Texture2D> Intro0;
        public static Asset<Texture2D> Intro1;
        public static Asset<Texture2D> Intro2;
        public static Asset<Texture2D> Intro3;
        public static Asset<Texture2D> PortfolioBox;
        public static Asset<Texture2D> PlayerM;
        public static Asset<Texture2D> PlayerF;
        public static Asset<Texture2D> Circle;
        public static Asset<Texture2D> Checkbox;
        public static Asset<Texture2D> CoinIcon;
        public static Asset<Texture2D> Button0;
        public static Asset<Texture2D> CursorTexture;
        public static Asset<Texture2D> TextSelected;
        public static Asset<Texture2D> BattleBG_0;
        public static Asset<Texture2D> HeartRight;
        public static Asset<Texture2D> HeartMiddle;
        public static Asset<Texture2D> HeartLeft;
        public static Asset<Texture2D> Heart;
        public static Asset<Texture2D> HealthText;
        public static Asset<Texture2D> Health;
        public static Asset<Texture2D> HealthBar;
        public static Asset<Texture2D> HealthBarOverlay;
        public static Asset<Texture2D> SelectionPlayer;
        public static Asset<Texture2D> BagPocket;
        public static Asset<Texture2D> BagMenu;
        public static Asset<Texture2D> BagBackground;
        public static Asset<Texture2D> Box0;
        public static Asset<Texture2D> Box1;
        public static Asset<Texture2D> Box2;
        public static Asset<Texture2D> Box3;
        public static Asset<Texture2D> Box4;
        public static Asset<Texture2D> Box5;
        public static Asset<Texture2D> Arrow;
        public static Asset<Texture2D> MalePickBox;
        public static Asset<Texture2D> FemalePickBox;
        public static Asset<Texture2D> MenuBtn0;
        public static Asset<Texture2D> MessageBox;
        public static Asset<Texture2D> InstructionBox;
        public static Asset<Texture2D> PanelBox;
        public static Asset<Texture2D> Logo;
        public static Asset<Texture2D> Splash_Layer_1;
        public static Asset<Texture2D> Splash_Layer_2;
        public static Asset<Texture2D> Splash_Layer_0;
        public static Asset<Texture2D> Slider_0;
        public static Asset<Texture2D> Slider_0_Overlay;
        public static Asset<Texture2D> Black;
        public static Asset<Texture2D> White;
        public static Asset<Texture2D> Red;

        public static void LoadAll()
        {
            SelectedBox = Asset<Texture2D>.Request("UI/SelectedBox");
            PortfolioBox = Asset<Texture2D>.Request("PortfolioBox");
            PlayerF = Asset<Texture2D>.Request("Player/Player_F");
            PlayerM = Asset<Texture2D>.Request("Player/Player_M");
            Checkbox = Asset<Texture2D>.Request("UI/Checkbox");
            CoinIcon = Asset<Texture2D>.Request("UI/GoldUI");
            Button0 = Asset<Texture2D>.Request("UI/Button_0");
            CursorTexture = Asset<Texture2D>.Request("UI/Cursor");
            TextSelected = Asset<Texture2D>.Request("UI/TextSelected");
            BattleBG_0 = Asset<Texture2D>.Request("Battle/BattleBG_0");
            HeartRight = Asset<Texture2D>.Request("UI/Entity/Heart_Right");
            HeartMiddle = Asset<Texture2D>.Request("UI/Entity/Heart_Middle");
            HeartLeft = Asset<Texture2D>.Request("UI/Entity/Heart_Left");
            Heart = Asset<Texture2D>.Request("UI/Entity/Heart");
            HealthText = Asset<Texture2D>.Request("UI/Entity/HealthText");
            Health = Asset<Texture2D>.Request("UI/Entity/Health");
            HealthBar = Asset<Texture2D>.Request("UI/Entity/HealthBar");
            HealthBarOverlay = Asset<Texture2D>.Request("UI/Entity/HealthBar_Overlay");
            SelectionPlayer = Asset<Texture2D>.Request("UI/SelectionPlayer");
            BagPocket = Asset<Texture2D>.Request("UI/Bag_Pocket");
            BagMenu = Asset<Texture2D>.Request("UI/BagMenu");
            BagBackground = Asset<Texture2D>.Request("UI/BagBackground");
            Box0 = Asset<Texture2D>.Request("UI/Box_0");
            Box1 = Asset<Texture2D>.Request("UI/Box_1");
            Box2 = Asset<Texture2D>.Request("UI/Box_2");
            Box3 = Asset<Texture2D>.Request("UI/Box_3");
            Box4 = Asset<Texture2D>.Request("UI/Box_4");
            Box5 = Asset<Texture2D>.Request("UI/Box_5");
            Arrow = Asset<Texture2D>.Request("UI/Arrow");
            MalePickBox = Asset<Texture2D>.Request("UI/MaleButton");
            FemalePickBox = Asset<Texture2D>.Request("UI/FemaleButton");
            MenuBtn0 = Asset<Texture2D>.Request("UI/MenuButton");
            MessageBox = Asset<Texture2D>.Request("UI/MessageBox");
            InstructionBox = Asset<Texture2D>.Request("UI/InstructionUI");
            PanelBox = Asset<Texture2D>.Request("UI/PanelBackground");
            Logo = Asset<Texture2D>.Request("Logo");
            Splash_Layer_1 = Asset<Texture2D>.Request("SplashScreen/Splash_0_1");
            Splash_Layer_2 = Asset<Texture2D>.Request("SplashScreen/Splash_0_2");
            Splash_Layer_0 = Asset<Texture2D>.Request("SplashScreen/Splash_0_0");
            Slider_0 = Asset<Texture2D>.Request("UI/Slider_0");
            Slider_0_Overlay = Asset<Texture2D>.Request("UI/Slider_0_Overlay");
            White = Asset<Texture2D>.Request("White");
            Black = Asset<Texture2D>.Request("Black");
            Red = Asset<Texture2D>.Request("Red");
            Circle = Asset<Texture2D>.Request("Circle");
            IntroOverlay = Asset<Texture2D>.Request("Intro_Overlay");
            Intro0 = Asset<Texture2D>.Request("Intro_0");
            Intro1 = Asset<Texture2D>.Request("Intro_1");
            Intro2 = Asset<Texture2D>.Request("Intro_2");
            Intro3 = Asset<Texture2D>.Request("Intro_3");
        }
    }
}
