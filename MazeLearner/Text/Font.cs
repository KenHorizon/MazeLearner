using MazeLearner.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MazeLeaner.Text
{
    public class Font
    {
        private Asset<SpriteFont> fontStyle;
        private bool outlined;
        public bool Outlined
        {
            get { return outlined; }
            set { outlined = value; }
        }
        public Asset<SpriteFont> FontStyle
        {
            get { return fontStyle; }
            set { fontStyle = value; }
        }
        public Font(Asset<SpriteFont> fontStyle, bool outline = true) 
        {
            this.fontStyle = fontStyle;
            this.outlined = outline;
        }

        public void Style(Asset<SpriteFont> fontStyle)
        {
            this.fontStyle = fontStyle;
        }

        public Vector2 GetLength(string text)
        {
            return Texts.MeasureString(this.FontStyle, text);
        }
    }
}
