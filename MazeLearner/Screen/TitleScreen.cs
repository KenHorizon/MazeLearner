using MazeLeaner.Text;
using MazeLearner.Audio;
using MazeLearner.Localization;
using MazeLearner.Screen.Widgets;
using MazeLearner.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MazeLearner.Screen.Components;
using MazeLearner.Graphics;

namespace MazeLearner.Screen
{
    public enum TitleSequence
    {
        Splash,
        Title
    }
    public class TitleScreen : BaseScreen
    {
        private TitleSequence _titleSequence = TitleSequence.Splash;
        public TitleSequence TitleSequence
        {
            get { return _titleSequence; } 
            set { _titleSequence = value; }
        }
        private const int SplashTimerEnd = 4;
        private const int SplashTimerNext = 2;
        private int SplashSteps = 0;
        private double SplashTimer = 9;
        private int delayMs = 0;
        private SimpleButton StartButton;
        private SimpleButton SettingsButton;
        private SimpleButton CollectablesButton;
        private SimpleButton ExitButton;
        public TitleScreen(TitleSequence titleSequence) : base("")
        {
            TitleSequence = titleSequence;
        }
        public override void LoadContent()
        {
            base.LoadContent();
            int TSScale = 1;
            int TSW = 240 * TSScale;
            int TSH = 40 * TSScale;
            this.posX = (this.game.GetScreenWidth() - TSW) / 2;
            this.posY = this.game.GetScreenHeight() / 2 - 92;
            if (this.TitleSequence == TitleSequence.Title)
            {
                int entryMenuSizeW = 240;
                int entryMenuSizeH = 60;
                int entryX = (this.game.GetScreenWidth() - entryMenuSizeW) / 2;
                int entryY = 320;
                int entryPadding = entryMenuSizeH + 12;
                this.EntryMenus.Add(new MenuEntry(0, Resources.PlayGame, new Rectangle(entryX, entryY, entryMenuSizeW, entryMenuSizeH), () =>
                {
                    this.game.SetScreen(new PlayerCreationScreen());
                }, AssetsLoader.MenuBtn0.Value));

                entryY += entryPadding;
                this.EntryMenus.Add(new MenuEntry(1, Resources.Collectables, new Rectangle(entryX, entryY, entryMenuSizeW, entryMenuSizeH), () =>
                {
                    this.game.SetScreen(new CollectiveScreen());
                }, AssetsLoader.MenuBtn0.Value));

                entryY += entryPadding;
                this.EntryMenus.Add(new MenuEntry(2, Resources.Settings, new Rectangle(entryX, entryY, entryMenuSizeW, entryMenuSizeH), () =>
                {
                    this.game.SetScreen(new OptionScreen());
                }, AssetsLoader.MenuBtn0.Value));

                entryY += entryPadding;
                this.EntryMenus.Add(new MenuEntry(3, Resources.Exit, new Rectangle(entryX, entryY, entryMenuSizeW, entryMenuSizeH), () =>
                {
                    this.game.QuitGame();
                }, AssetsLoader.MenuBtn0.Value));
            }
        }

        public override void Update(GameTime gametime)
        {
            base.Update(gametime);
            if (this.TitleSequence == TitleSequence.Splash)
            {
                if (this.SplashSteps == 1)
                {
                    if (Main.Input.Pressed(GameSettings.KeyInteract) || Main.Input.Pressed(GameSettings.KeyConfirm))
                    {
                        Main.SoundEngine.Play(AudioAssets.ClickedSFX.Value);
                        this.SplashStepNext();
                    }
                } 
                else
                {
                    delayMs++;
                    if (delayMs > 5 && Main.Input.Pressed(GameSettings.KeyInteract))
                    {
                        Main.SoundEngine.Play(AudioAssets.ClickedSFX.Value);
                        this.SplashStepNext(TitleScreen.SplashTimerEnd);
                    }
                    this.SplashTimer += gametime.ElapsedGameTime.TotalSeconds;
                    if (this.SplashTimer > SplashTimerNext && this.SplashSteps <= TitleScreen.SplashTimerEnd)
                    {
                        this.SplashStepNext();
                    }
                    if (this.SplashSteps >= TitleScreen.SplashTimerEnd)
                    {
                        this.game.SetScreen(new TitleScreen(TitleSequence.Title));
                    }
                }
            }
        }

        public override void Render(SpriteBatch sprite, Graphic graphic)
        {
            if (this.SplashSteps == 1)
            {
                graphic.RenderKeybindInstruction(sprite);
            }
            if (this.SplashSteps == 2)
            {
                this.SplashScreen(sprite, AssetsLoader.Splash_Layer_0.Value, AssetsLoader.Splash_Layer_1.Value, AssetsLoader.Splash_Layer_2.Value, 1.0F);

            }
            if (this.SplashSteps == 3)
            {
                this.SplashScreen(sprite, AssetsLoader.Splash_Layer_0.Value, AssetsLoader.Splash_Layer_1.Value, AssetsLoader.Splash_Layer_2.Value, 1.0F);

            }
            if (this.TitleSequence == TitleSequence.Title)
            {
                base.Render(sprite, graphic);
                float scale = 1.5F;
                int logoX = (int)((Main.WindowScreen.Width - AssetsLoader.Logo.Value.Width * scale) / 2);
                int logoY = 80;
                var LogoRect = new Rectangle(logoX, logoY, (int)(AssetsLoader.Logo.Value.Width * scale), (int)(AssetsLoader.Logo.Value.Height * scale));
                sprite.Draw(AssetsLoader.Logo.Value, LogoRect);
            }
        }

        public override void RenderBackground(SpriteBatch sprite, Graphic graphic)
        {
            base.RenderBackground(sprite, graphic);

            if (this.TitleSequence == TitleSequence.Title)
            {
                this.game.RenderBackground(sprite);
            }
        }

        private void SplashScreen(SpriteBatch sprite, Texture2D splash0, Texture2D splash1, Texture2D splash2, float alpha)
        {
            sprite.Draw(splash1, Main.WindowScreen, Color.White * alpha);
            sprite.Draw(splash0, Main.WindowScreen, Color.White * alpha);
            sprite.Draw(splash2, Main.WindowScreen, Color.White * alpha);
        }

        private void SplashStepNext(int forceIt = 0)
        {
            if (forceIt == 0)
            {
                this.SplashSteps++;
            }
            else {
                this.SplashSteps = forceIt;
            }
            this.SplashTimer = 0;
        }
        public override bool ShowOverlayKeybinds()
        {
            return base.ShowOverlayKeybinds() && this.TitleSequence == TitleSequence.Title;
        }
    }
}
