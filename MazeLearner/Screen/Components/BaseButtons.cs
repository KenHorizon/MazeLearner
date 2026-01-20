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
            int x = this.posX + ((this.Width - this.Text.Length) / 2);
            int y = this.posY;
            Vector2 pos = new Vector2(x, y);
            TextManager.Text(Fonts.Normal, this.Text, pos);
        }
        public virtual void RenderBackground(SpriteBatch sprite, Vector2 mouse) { }
    }
}
