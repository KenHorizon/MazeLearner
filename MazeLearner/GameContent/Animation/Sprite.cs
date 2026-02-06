using MazeLearner.GameContent.Entity;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MazeLearner.GameContent.Animation
{
    public class Sprite
    {
        private string _spriteName;
        private Color _color = Color.White;
        private int _height;
        private int _width;
        private Vector2 _scale;
        private Vector2 _origin;

        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }
        public string SpriteName
        {
            get { return _spriteName; }
            set { _spriteName = value; }
        }
        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }
        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }
        public Vector2 Scale
        {
            get { return _scale; }
            set { _scale = value; }
        }
        public Vector2 Origin
        {
            get { return _origin; }
            set { _origin = value; }
        }
        public NPC npc;
        public Texture2D sprite;
        public Sprite(string name, NPC npc)
        {
            this.npc = npc;
            this.sprite = npc.GetTexture().Value;
            this.SpriteName = name;
            this.Width = npc.Width;
            this.Height = npc.Height;
        }
        public void Draw(SpriteBatch sprite)
        {

            int facingId = (int) npc.Facing;
            int w = this.npc.animationState.frames * this.Width;
            int h = facingId * this.Height;
            Rectangle destSprites = new Rectangle(w, h, npc.Width, npc.Height);
            Main.SpriteBatch.Draw(npc.GetTexture().Value, npc.DrawingBox, destSprites, Color.White);
            //Main.SpriteBatch.End();
            //Main.DrawSprites(ShaderLoader.LightShaders.Value);
            //ShaderLoader.LightShaders.Value.Parameters["RingCenter"].SetValue(new Vector2(0.5F, 0.5F));
            //ShaderLoader.LightShaders.Value.Parameters["Thickness"].SetValue(0.4F);
            //ShaderLoader.LightShaders.Value.Parameters["Radius"].SetValue(0.4F);
            //ShaderLoader.LightShaders.Value.Parameters["Color"].SetValue(new Vector4(1.0F, 1.0F, 1.0F, 0.0F));
            //ShaderLoader.LightShaders.Value.Parameters["Softness"].SetValue(0.01F);
            //Main.SpriteBatch.Draw(Main.FlatTexture, npc.lightEffectBox, Color.White);
        }
    }
}
