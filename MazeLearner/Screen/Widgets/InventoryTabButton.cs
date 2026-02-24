using MazeLearner.Graphics;
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
    public class InventoryTabButton : BaseButtons
    {
        private Asset<Texture2D> ArrowPoint = Asset<Texture2D>.Request("Battle/Arrow");
        private Asset<Texture2D> QuestionBox = Asset<Texture2D>.Request("Battle/QuestionBox");
        public InventoryTabButton(int x, int y, int width, int height, Action action) :
            base(x, y, width, height, action)
        { }

        public override void RenderBackground(SpriteBatch sprite, Vector2 mouse)
        {
            base.RenderBackground(sprite, mouse);
            sprite.Draw(QuestionBox.Value, this.Bounds);
            if (this.IsHovered)
            {
                sprite.Draw(ArrowPoint.Value, new Rectangle(this.posX + ArrowPoint.Value.Width, this.posY + 10, ArrowPoint.Value.Width, ArrowPoint.Value.Height));
            }
        }
    }
}
