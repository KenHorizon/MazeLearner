using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.Screen
{
    public class TitleScreen : BaseScreen
    {
        private const int SplashTimerEnd = 300;
        private const int SplashTimerNext = 2;
        private int SplashSteps;
        private double SplashTimer;

        public TitleScreen(string name) : base(name)
        {
        }

        public override void Update(GameTime gametime)
        {
            base.Update(gametime);
            this.SplashTimer = gametime.ElapsedGameTime.TotalSeconds;
            if (this.SplashTimer > SplashTimerNext)
            {
                this.SplashStepNext();
            }
        }

        public override void Render(SpriteBatch sprite)
        {
            base.Render(sprite);
            if (this.SplashSteps == 0)
            {
                float alpha = (float)(this.SplashTimer / SplashTimerNext);

            }
            if (this.SplashSteps == 1)
            {

            }
        }

        private void SplashStepNext()
        {
            this.SplashSteps++;
            this.SplashTimer = 0;
        }
    }
}
