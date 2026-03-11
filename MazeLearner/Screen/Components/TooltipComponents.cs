using MazeLeaner.Text;
using MazeLearner.Graphics.Asset;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.Screen.Components
{
    public class TooltipComponents
    {
        private Asset<SpriteFont> _font;
        private string description { get; set; } = "";
        public Vector2 Position { get; set; }
        public bool Visible { get; set; }
        public int Width { get; set; } = 150;
        public TooltipComponents(Asset<SpriteFont> font)
        {
            this._font = font;
        }
        public void Descriptions(string description = "") => this.description = description;

        public static TooltipComponents Create(Asset<SpriteFont> font)
        {
            return new TooltipComponents(font);
        }
         // Fecth from Solarized Game (other project game i made for thesis)
        public void Draw(SpriteBatch batch)
        {
            Vector2 descSize = Texts.MeasureString(this._font, description);
            float padding = 8.0F;

            List<string> lines = Texts.ListWrapText(this._font, description, Width);
            float lineheight = descSize.Y + 12;
            float width = descSize.X + padding * 2;
            float height = lines.Count * lineheight + padding * 2;

            int screenWidth = Main.Graphics.Viewport.Width;
            int screenHeight = Main.Graphics.Viewport.Height;

            Vector2 validatedPosition = Position;
            if (validatedPosition.X + width > screenWidth - padding)
                validatedPosition.X = screenWidth - width - padding;

            if (validatedPosition.Y + height > screenHeight - padding)
                validatedPosition.Y = screenHeight - height - padding;

            if (validatedPosition.X < padding)
                validatedPosition.X = padding;

            if (validatedPosition.Y < padding)
                validatedPosition.Y = padding;
            Rectangle background = new Rectangle((int)validatedPosition.X, (int)validatedPosition.Y, Width, (int)height);

            batch.Draw(Main.FlatTexture, background, new Color(20, 20, 20, 230));

            int border = 2;

            Vector2 topLeft = new Vector2(background.Left, background.Top);
            Vector2 topRight = new Vector2(background.Right, background.Top);
            Vector2 bottomLeft = new Vector2(background.Left, background.Bottom);
            Vector2 bottomRight = new Vector2(background.Right, background.Bottom);

            Color startBorderColor = Color.Black;
            Color endBorderColor = Color.Black;

            batch.DrawLine(topLeft, topRight, startBorderColor, border);
            batch.DrawLine(bottomLeft, topLeft, endBorderColor, startBorderColor, border);
            batch.DrawLine(topRight, bottomRight, startBorderColor, endBorderColor, border);
            batch.DrawLine(bottomRight, bottomLeft, endBorderColor, border);

            Vector2 textPos = new Vector2(background.X + padding, background.Y + padding);
            for (int i = 0; i < lines.Count; i++)
            {
                Texts.DrawString(this._font, lines[i], textPos + new Vector2(0, i * lineheight), Color.White);
            }
        }
    }
}
