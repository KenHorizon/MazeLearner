using MazeLearner.Graphics;
using MazeLearner.Screen.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MazeLearner.Screen.Widgets
{
    public class SimpleButton : BaseButtons
    {
        private Asset<Texture2D> ArrowPoint = Asset<Texture2D>.Request("UI/Arrow");
        private Asset<Texture2D> QuestionBox = Asset<Texture2D>.Request("UI/MenuButton");
        public SimpleButton(int x, int y, int width, int height, Action action) :
            base(x, y, width, height, action) 
        {
            this.TextColor = Color.Black;
        }

        public override void RenderBackground(SpriteBatch sprite, Vector2 mouse)
        {
            base.RenderBackground(sprite, mouse);
            //sprite.DrawFillRectangle(this.Bounds, Color.White, Color.Black * 0.5F);
            sprite.Draw(QuestionBox.Value, this.Bounds);
        }
        public override void RenderWhenHovered(SpriteBatch sprite, Vector2 mouse)
        {
            sprite.Draw(ArrowPoint.Value, new Rectangle(this.posX - ArrowPoint.Value.Width, this.posY + ((QuestionBox.Value.Height - ArrowPoint.Value.Height) / 2), ArrowPoint.Value.Width, ArrowPoint.Value.Height));
        }
    }
}
