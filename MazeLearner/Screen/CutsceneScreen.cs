using MazeLearner.Audio;
using MazeLearner.Graphics;
using MazeLearner.Graphics.Animation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.Screen
{
    public class CutsceneScreen : BaseScreen
    {
        private int TimerEnd = 0;
        private const int SplashTimerNext = 2;
        private int Scene = 0;
        private int Phase = 0;
        private double Timer = 9;
        private int delayMs = 0;

        string phase1 = "Year 1629, The planet earth are inhabitant of different races, humans, elf, dwarfes and many more, these races live in harmony and peace";
        string phase2 = "suddenly a dark forces arrive trembling all nation of races and destorying there homes and everything they have, the war last for 30 years";
        string phase3 = "so many people fallen in war and one day a hero arrive, with the hero's subordinate they able to fight back with the dark forces and the";
        string phase4 = "ended the war, but the hero fade before he fall the hero use last resort sealing everyone with different realms.";

        public CutsceneScreen(int scene) : base("")
        {
            this.Scene = scene;
        }
        public override void LoadContent()
        {
            base.LoadContent();
            if (this.Scene == 0)
            {
                this.TimerEnd = 5;
            }
        }
        public override void Update(GameTime gametime)
        {
            base.Update(gametime);
            if (this.Scene == 0)
            {
                this.Timer += gametime.ElapsedGameTime.TotalSeconds;
                if (this.delayMs > 0)
                {
                    this.delayMs--;
                }
                if (Main.Input.Pressed(GameSettings.KeyInteract) || Main.Input.Pressed(GameSettings.KeyConfirm))
                {
                    Main.SoundEngine.Play(AudioAssets.ClickedSFX.Value);
                    this.SplashStepNext();
                }
                if (this.Phase == 1)
                {
                    Main.SoundEngine.Play(AudioAssets.Intro0.Value);
                }
                if (this.Phase == 2)
                {
                    Main.SoundEngine.Play(AudioAssets.Intro1.Value);
                }
                if (this.Phase == 3)
                {
                    Main.SoundEngine.Play(AudioAssets.Intro2.Value);
                }
                if (this.Phase == 4)
                {
                    Main.SoundEngine.Play(AudioAssets.Intro3.Value);
                }
                if (this.Timer > SplashTimerNext && this.Phase <= this.TimerEnd)
                {
                    this.SplashStepNext();
                }
                if (this.Phase >= this.TimerEnd)
                {
                    Main.FadeAwayBegin = true;
                    Main.FadeAwayOnEnd = () =>
                    {
                        Main.GameState = GameState.Play;
                        this.game.SetScreen(null);
                    };
                }
            }
        }
        private void SplashStepNext(int forceIt = 0)
        {
            if (forceIt == 0)
            {
                this.Phase++;
            }
            else
            {
                this.Phase = forceIt;
            }
            this.Timer = 0;
            this.delayMs = 10;
        }

        public override void Render(SpriteBatch sprite, Graphic graphic)
        {
            if (this.Phase == 1)
            {
                graphic.RenderTransparentDialogs(sprite, phase1);
            }
            if (this.Phase == 2)
            {
                graphic.RenderTransparentDialogs(sprite, phase2);
            }
            if (this.Phase == 3)
            {
                graphic.RenderTransparentDialogs(sprite, phase3);
            }

            if (this.Phase == 4)
            {
                graphic.RenderTransparentDialogs(sprite, phase4);
            }
        }

        public override void RenderBackground(SpriteBatch sprite, Graphic graphic)
        {
            base.RenderBackground(sprite, graphic);
            sprite.Draw(AssetsLoader.IntroOverlay.Value, Main.WindowScreen);

            if (this.Phase == 1)
            {
                sprite.Draw(AssetsLoader.Intro0.Value, Main.WindowScreen);
            }
            if (this.Phase == 2)
            {
                sprite.Draw(AssetsLoader.Intro1.Value, Main.WindowScreen);
            }
            if (this.Phase == 3)
            {
                sprite.Draw(AssetsLoader.Intro2.Value, Main.WindowScreen);
            }

            if (this.Phase == 4)
            {
                sprite.Draw(AssetsLoader.Intro3.Value, Main.WindowScreen);
            }
        }
    }
}
