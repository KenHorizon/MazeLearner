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
        public int Index { get; set; }
        public string Text { get; set; }
        public Rectangle Box { get; set; }
        public Action Action { get; set; }
        public Texture2D Texture { get; set; } = null;
        public AnchorMainEntry Anchor { get; set; } = AnchorMainEntry.Left;
        public MenuEntry(int index, string text, Rectangle box, Action action, Texture2D texture = null, AnchorMainEntry anchor = AnchorMainEntry.Left)
        {
            this.Index = index;
            this.Text = text;
            this.Box = box;
            this.Action = action;
            this.Texture = texture;
            this.Anchor = anchor;
        }
    }
}
