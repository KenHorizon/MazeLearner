using MazeLearner;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MazeLeaner.Text
{
    public class Font
    {
        private Assets<SpriteFont> fontStyle;
        private bool outlined;
        public bool Outlined
        {
            get { return outlined; }
            set { outlined = value; }
        }
        public Assets<SpriteFont> FontStyle
        {
            get { return fontStyle; }
            set { fontStyle = value; }
        }
        public Font(Assets<SpriteFont> fontStyle, bool outline = true) 
        {
            this.fontStyle = fontStyle;
            this.outlined = outline;
        }

        public void Style(Assets<SpriteFont> fontStyle)
        {
            this.fontStyle = fontStyle;
        }

        public Vector2 GetLength(string text)
        {
            return TextManager.MeasureString(this.FontStyle, text);
        }
    }
}
