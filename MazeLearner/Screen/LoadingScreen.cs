using MazeLeaner.Text;
using MazeLearner.Audio;
using MazeLearner.Graphics;
using MazeLearner.Graphics.Animation;
using MazeLearner.Graphics.Asset;
using MazeLearner.Localization;
using MazeLearner.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.Screen
{
    public class LoadingScreen : BaseScreen
    {
        public LoadingScreen() : base("")
        {

        }
        public override void LoadContent()
        {

        }
        public override void Update(GameTime gametime)
        {

        }

        public override void Render(SpriteBatch sprite, Graphic graphic)
        {
            var vctxt = Texts.MeasureString(Fonts.Dialog, Resources.Loading);
            var vc2 = new Vector2(Main.WindowScreen.Width - vctxt.X, Main.WindowScreen.Height - vctxt.Y);
            Texts.DrawString(Fonts.Dialog, Resources.Loading, vc2);
        }

        public override bool ShowOverlayKeybinds()
        {
            return false;
        }
        public override void RenderBackground(SpriteBatch sprite, Graphic graphic)
        {
            sprite.Screen(Color.Black);
        }
    }
}
