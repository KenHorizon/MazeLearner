using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MazeLearner.GameContent.Animation
{
    public class Sprite
    {
        private string _spriteName;
        private Color _color;
        private bool _flipHorizontally;
        private bool _flipVertically;
        private int _height;
        private int _width;
        private Vector2 _scale;
        private Vector2 _origin;
        private int _originX;
        private int _originY;

        public string SpriteName
        {
            get { return _spriteName; }
            set { _spriteName = value; }
        }
        public Texture2D sprite;
        public Sprite(string name, Texture2D sprite)
        {
            this.sprite = sprite;
            this.SpriteName = name;
        }
        public void Draw(SpriteBatch sprite, Vector2 position)
        {

        }
    }
}
