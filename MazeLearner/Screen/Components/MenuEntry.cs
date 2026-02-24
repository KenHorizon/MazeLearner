using MazeLearner.Graphics;
using MazeLearner.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MazeLearner.Screen.Components
{
    public enum AnchorMainEntry
    {
        Left,
        Right,
        Center
    }
    public class MenuEntry
    {
        public bool IsActive { get; set; } = true;
        public Color TextColor { get; set; } = Color.Black;
        public int Index { get; set; }
        public string Text { get; set; }
        public Rectangle Box { get; set; }
        public Action Action { get; set; }
        public Action OnExit { get; set; }
        public Texture2D Texture { get; set; } = null;
        public AnchorMainEntry Anchor { get; set; } = AnchorMainEntry.Left;
        public Asset<SpriteFont> FontStyle { get; set; } = Fonts.Text;
        public MenuEntry(int index, string text, Rectangle box, Action action, Texture2D texture = null, AnchorMainEntry anchor = AnchorMainEntry.Left, Asset<SpriteFont> fontStyle = null)
        {
            this.IsActive = true;
            this.Index = index;
            this.Text = text;
            this.Box = box;
            this.Action = action;
            this.Texture = texture;
            this.Anchor = anchor;
            this.FontStyle = fontStyle == null ? Fonts.Text : fontStyle;
        }
    }
}
