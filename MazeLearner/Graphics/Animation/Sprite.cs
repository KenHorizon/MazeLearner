using MazeLearner.GameContent.Entity;
using MazeLearner.GameContent.Entity.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MazeLearner.Graphics.Animation
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
            this.SpriteName = name;
            this.Width = npc.Width;
            this.Height = npc.Height;
        }
        public void Draw(SpriteBatch sprite)
        {
            if (this.npc.animationState != null)
            {
                int facingId = (int)npc.Direction;
                int w = this.npc.animationState.frames * this.Width;
                int h = facingId * this.Height;
                Rectangle destSprites = new Rectangle(w, h, npc.Width, npc.Height);
                
                if (this.npc is PlayerEntity player)
                {
                    Texture2D text;
                    if (player.Gender == Gender.Male)
                    {
                        text = player.isRunning ? PlayerEntity.RunningM.Value : PlayerEntity.WalkingM.Value;
                    }
                    else
                    {
                        text = player.isRunning ? PlayerEntity.RunningF.Value : PlayerEntity.WalkingF.Value;
                    }
                    Main.SpriteBatch.Draw(text, player.DrawingBox, destSprites, Color.White);
                    Main.SpriteBatch.Draw(Main.FlatTexture, player.FacingBox, Color.Red * 0.5F);
                    //Main.SpriteBatch.Draw(Main.FlatTexture, npc.DetectionBox, Color.White);
                }
                else
                {
                    try
                    {
                        Main.SpriteBatch.Draw(Main.NPCTexture[this.npc.type], npc.DrawingBox, destSprites, Color.White);
                        //Main.SpriteBatch.Draw(Main.FlatTexture, npc.DetectionBox, Color.White);
                    }
                    catch (
                        Exception ex)
                    {
                        Loggers.Error($"{ex}");
                    }
                }
            }
        }
    }
}
