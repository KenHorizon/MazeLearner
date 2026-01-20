using MazeLeaner.Text;
using MazeLearner.GameContent.Entity.Monster;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MazeLearner.Screen.Components
{
    public class BaseButtons : BaseWidgets
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
        public override void Render(SpriteBatch batch, Vector2 mouse)
        {
            base.Render(batch, mouse);

        }
    }
}
