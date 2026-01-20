using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MazeLeaner.Text
{
    public class DynamicSpriteFont
    {
        private SpriteFont fontAssets;

        public DynamicSpriteFont(SpriteFont font)
        {
            fontAssets = font;
        }
        public void DrawString(SpriteBatch spriteBatch, string text, Vector2 position, Vector2 origins, Color color, float rotation = 0.0F, float maxWidth = 0F)
        {
            if (maxWidth > 0f)
            {
                text = BreakText(text, maxWidth);
            }
            spriteBatch.DrawString(fontAssets, text, position, color, rotation, origins, 1.0F, SpriteEffects.None, 0.0F);
        }
        public Vector2 MeasureString(string text)
        {
            return fontAssets.MeasureString(text);
        }
        private string BreakText(string text, float maxWidth)
        {
            string[] words = text.Split(' ');
            string result = "";
            float lineWidth = 0f;

            foreach (string word in words)
            {
                Vector2 size = fontAssets.MeasureString(word + " ");
                if (lineWidth + size.X > maxWidth)
                {
                    result += "\n";
                    lineWidth = 0f;
                }
                result += word + " ";
                lineWidth += size.X;
            }

            return result.TrimEnd();
        }
    }
}
