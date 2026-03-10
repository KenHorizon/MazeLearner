using MazeLearner.Graphics.Animation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.Graphics
{
    public class Parallax
    {
        private bool _constantSpeed;
        private float _layer;
        private float _scrollingSpeed = 100.0F;
        private float _x;
        private float _y;
        private float _speed;
        private Texture2D _texture;
        public Texture2D Texture    
        {
            get { return _texture; } 
            set { _texture  = value; }
        }
        public float X
        {
            get { return _x; }
            set
            {
                _x = value;
            }
        }
        public float Y
        {
            get { return _y; }
            set
            {
                _y = value;
            }
        }
        public float Layer
        {
            get { return _layer; }
            set
            {
                _layer = value;
            }
        }
        public Parallax(Texture2D textures, float scrollingSpeed, bool constantSpeed = false)
        {
            this._texture = textures;
            this._scrollingSpeed = scrollingSpeed;
            this._constantSpeed = constantSpeed;
        }
        public void Update(GameTime gameTime)
        {
            float delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            X -= this._scrollingSpeed * delta;
            if (X <= - this.Texture.Width)
            {
                X += this.Texture.Width;
            }
            if (Y <= -this.Texture.Height)
            {
                Y += this.Texture.Height;
            }
        }
        public void Draw(SpriteBatch sprite)
        {
            sprite.Draw(this.Texture, new Rectangle((int)this.X, (int)this.Y, Main.WindowScreen.Width, Main.WindowScreen.Height), Color.White);
            sprite.Draw(this.Texture, new Rectangle((int)this.X + this.Texture.Width, (int)this.Y, Main.WindowScreen.Width, Main.WindowScreen.Height), Color.White);

        }
    }
}
