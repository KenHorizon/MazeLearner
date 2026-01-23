using MazeLeaner.Text;
using MazeLearner.GameContent.Entity.Monster;
using MazeLearner.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MazeLearner.Screen.Components
{
    public abstract class BaseButtons : BaseWidgets
    {
        public Action OnPress;

        public BaseButtons(int x, int y, int width, int height, Action action) : base(x, y, width, height)
        {
            this.OnPress = action;
        }
        public override void OnClick(Vector2 mouse)
        {
            this.OnPress?.Invoke();
        }

        public override void Render(SpriteBatch sprite, Vector2 mouse)
        {
            base.Render(sprite, mouse);
            this.RenderBackground(sprite, mouse);
            Vector2 TextSize = TextManager.MeasureString(Fonts.Normal, this.Text);
            int x = (int) (this.posX + ((this.Width - TextSize.X) / 2));
            int y = (int) (this.posY + ((this.Height - TextSize.Y) / 2));
            Vector2 pos = new Vector2(x, y);
            TextManager.Text(Fonts.Normal, this.Text, pos, this.TextColor);
            if (this.IsHovered)
            {
                this.RenderWhenHovered(sprite, mouse);
            }
        }
        public virtual void RenderBackground(SpriteBatch sprite, Vector2 mouse) { }
        public virtual void RenderWhenHovered(SpriteBatch sprite, Vector2 mouse) { }
    }
}
