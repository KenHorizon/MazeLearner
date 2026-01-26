using MazeLeaner.Text;
using MazeLearner.GameContent.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.Text
{
    public class TypeWriterText
    {
        private string _fullText = "";
        private int _visibleCount;
        private float _timer;
        private float _charDelay = 0.035F;

        public string FullText
        {
            get { return _fullText; }
            set { _fullText = value; }
        }
        public int VisibleCount
        {
            get { return _visibleCount; }
            set { _visibleCount = value; }
        }
        public float Timer
        {
            get { return _timer; }
            set { _timer = value; }
        }
        public float CharDelay
        {
            get { return _charDelay; }
            set { _charDelay = value; }
        }
        public bool Finished => VisibleCount >= FullText.Length;
        public void Update(GameTime gameTime)
        {
            if (this.Finished) return;

            this.Timer += (float) gameTime.ElapsedGameTime.TotalSeconds;

            while (this.Timer >= CharDelay)
            {
                this.Timer -= CharDelay;
                this.VisibleCount++;

                if (this.VisibleCount >= FullText.Length)
                {
                    this.VisibleCount = FullText.Length;
                    break;
                }
            }
        }
        public void Skip()
        {
            this.VisibleCount = this.FullText.Length;
            this.Timer = 0F;
        }
        public void Draw(SpriteBatch sprite, Vector2 position, Rectangle rect, Color color)
        {
            string visibleText = this.FullText.Substring(0, this.VisibleCount);
            sprite.DrawString(Fonts.DT_L.Value, visibleText, position, color);
            TextManager.TextBox(Fonts.DT_L, visibleText, rect, new Vector2(GameSettings.DialogBoxPadding, 24), Color.Black);

        }
    }
}
