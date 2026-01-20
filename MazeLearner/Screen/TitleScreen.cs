using MazeLeaner.Text;
using MazeLearner.Screen.Widgets;
using MazeLearner.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private const int SplashTimerEnd = 3;
        private const int SplashTimerNext = 2;
        private int SplashSteps = 0;
        private double SplashTimer = 9;
        private SimpleButton StartButton;
        private SimpleButton SettingsButton;
        private SimpleButton CollectablesButton;
        private SimpleButton ExitButton;
        private static Assets<Texture2D> Background = Assets<Texture2D>.Request("BG_0_0");
        private static Assets<Texture2D> Logo = Assets<Texture2D>.Request("Logo");
        private static Assets<Texture2D> Splash_Layer_1 = Assets<Texture2D>.Request("SplashScreen/Splash_0_1");
        private static Assets<Texture2D> Splash_Layer_2 = Assets<Texture2D>.Request("SplashScreen/Splash_0_2");
        private static Assets<Texture2D> Splash_Layer_0 = Assets<Texture2D>.Request("SplashScreen/Splash_0_0");
        public TitleScreen(TitleSequence titleSequence) : base("")
        {
            TitleSequence = titleSequence;
        }
        public override void LoadContent()
        {
            base.LoadContent();
            this.posX = this.game.WindowScreen.Width / 2;
            this.posY = 120;
            if (this.TitleSequence == TitleSequence.Title)
            {
                this.StartButton = new SimpleButton(this.posX, this.posY, 180, 40, () =>
                {
                    Main.GameState = GameState.Play;
                    this.game.SetScreen((BaseScreen) null);
                });
                this.StartButton.Text = "Start";
                this.AddRenderableWidgets(this.StartButton);
            }
        }

        public override void Update(GameTime gametime)
        {
            base.Update(gametime);
            if (this.TitleSequence == TitleSequence.Splash)
            {
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
            else
            {

            }
        }

        public override void Render(SpriteBatch sprite)
        {
            base.Render(sprite);
            if (this.SplashSteps == 1)
            {
                this.SplashScreen(sprite, Splash_Layer_0.Value, Splash_Layer_1.Value, Splash_Layer_2.Value, 1.0F);

                float alpha = (float)(this.SplashTimer / 2.0F);
                sprite.Draw(Main.FlatTexture, this.game.WindowScreen, Color.Black * (1.0F - alpha));
            }
            if (this.SplashSteps == 2)
            {
                this.SplashScreen(sprite, Splash_Layer_0.Value, Splash_Layer_1.Value, Splash_Layer_2.Value, 1.0F);

            }
            if (this.TitleSequence == TitleSequence.Title)
            {
                //int logoX = (this.game.WindowScreen.Width - Logo.Value.Width) / 2;
                int logoX = 120;
                int logoY = 80;
                var LogoRect = new Rectangle(logoX, logoY, Logo.Value.Width, Logo.Value.Height);
                sprite.Draw(Background.Value, this.game.WindowScreen);
                sprite.Draw(Logo.Value, LogoRect);
            }
        }

        private void SplashScreen(SpriteBatch sprite, Texture2D splash0, Texture2D splash1, Texture2D splash2, float alpha)
        {
            sprite.Draw(splash1, this.game.WindowScreen, Color.White * alpha);
            sprite.Draw(splash0, this.game.WindowScreen, Color.White * alpha);
            sprite.Draw(splash2, this.game.WindowScreen, Color.White * alpha);
        }

        private void SplashStepNext()
        {
            this.SplashSteps++;
            this.SplashTimer = 0;
        }
    }
}
