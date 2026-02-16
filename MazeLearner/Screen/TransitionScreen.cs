using MazeLearner.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MazeLearner.Screen
{
    public class TransitionScreen : BaseScreen
    {
        private int fadeOut;
        private int time;
        private float factor;
        private Action end;
        public TransitionScreen(int fadeOut, Action end) : base("")
        {
            this.fadeOut = fadeOut;
            this.time = 0;
            this.factor = 0.0F;
            this.end = end;
        }

        public override void Update(GameTime gametime)
        {
            base.Update(gametime);
            this.time += 1;
            this.factor = ((float) this.time / this.fadeOut);
            Loggers.Info($"{time} {factor}");
            if (this.factor > 1.0F)
            {
                this.end?.Invoke();
            }
        }

        public override void Render(SpriteBatch sprite, Graphic graphic)
        {
            base.Render(sprite, graphic);
            sprite.Draw(AssetsLoader.Black.Value, Main.WindowScreen, Color.White * this.factor);

        }
        public override bool ShowOverlayKeybinds()
        {
            return false;
        }
    }
}
