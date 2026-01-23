using MazeLeaner.Text;
using MazeLearner.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.Screen.Components
{
    public class BaseWidgets : Renderables, GuiEventListener
    {
        private int _width;
        private int _height;
        private Color _color = Color.White;
        public Color TextColor
        {
            get { return _color; }
            set { _color = value; }
        }
        private bool isFocused = false;
        public int Width
        {
            get { return this._width; }
            set { this._width = value; }
        }
        public int Height
        {
            get { return this._height; }
            set { this._height = value; }
        }
        private int tabOrderGroup = 0;
        private string _textWidgets = "Button";
        public string Text
        {
            get { return _textWidgets; }
            set { _textWidgets = value; }
        }
        public int posX;
        public int posY;
        protected bool IsHovered;
        public bool active = true;
        public bool visible = true;
        public bool IsActive => active;
        public Rectangle Bounds
        {
            get
            {
                return new Rectangle(this.posX, this.posY, this.Width, this.Height);
            }
            set
            {
                this.Width = value.Width;
                this.Height = value.Height;
                this.posX = value.X;
                this.posY = value.Y;
            }
        }
        public BaseWidgets(int x, int y, int width, int height)
        {
            this.posX = x;
            this.posY = y;
            this.Width = width;
            this.Height = height;
        }
        /// <summary>
        /// Use Render(SpriteBatch, Vector2) instead
        /// </summary>
        /// <param name="sprite"></param>
        /// <param name="mouse"></param>
        public void Draw(SpriteBatch sprite, Vector2 mouse)
        {
            if (this.visible == true)
            {
                this.IsHovered = this.Bounds.Contains(mouse);
            }
            this.Render(sprite, mouse);
        }
        public virtual void Render(SpriteBatch sprite, Vector2 mouse)
        {

        }

        public virtual bool MouseClicked(Vector2 mouse, MouseHandler handler)
        {
            if (this.visible && this.active)
            {
                bool flag = this.Clicked(mouse);
                if (flag)
                {
                    this.PlaySoundClick();
                    this.OnClick(mouse);
                    this.SetFocused(true);
                    return true;
                }
                return false;
            }
            else
            {
                return false;
            }
        }
        public bool Clicked(Vector2 mouse)
        {
            return this.IsActive && this.visible && this.Bounds.Contains(mouse) && Main.Mouse.IsLeftClicked();
        }
        public bool IsFocused()
        {
            return this.isFocused;
        }
        public void SetFocused(bool focused)
        {
            this.isFocused = focused;
        }
        public virtual void PlaySoundClick() {}
        public virtual void OnClick(Vector2 mouse) {}
    }
}
