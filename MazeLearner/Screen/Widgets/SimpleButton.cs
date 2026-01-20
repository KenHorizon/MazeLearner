using MazeLearner.Screen.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.Screen.Widgets
{
    public class SimpleButton : BaseButtons
    {
        public SimpleButton(int x, int y, int width, int height, Action action) :
            base(x, y, width, height, action) {}

        public override void RenderBackground(SpriteBatch sprite, Vector2 mouse)
        {
            base.RenderBackground(sprite, mouse);
            sprite.DrawFillRectangle(this.Bounds, Color.White, Color.Black * 0.5F);
        }
    }
}
