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
            Vector2 playerPosition = Main.Camera.Position;
            float scale = 1.0F;
            Rectangle boundingBoxDraw = new Rectangle((int)playerPosition.X, (int)playerPosition.Y,
                (int)(Main.WindowScreen.Width * scale),
                (int)(Main.WindowScreen.Height * scale));

            if (this.npc.animationState != null && this.npc.Invisible == false && this.npc.InteractionBox.Intersects(boundingBoxDraw))
            {
                int facingId = Convert.ToInt32(this.npc.Direction);
                int w = this.npc.animationState.frames * this.Width;
                int h = facingId * this.Height;
                Rectangle destSprites = new Rectangle(w, h, npc.Width, npc.Height);
                if (this.npc is PlayerEntity player && player == Main.ActivePlayer)
                {

                    try
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
                        sprite.Draw(text, player.Sprite, destSprites, Color.White);
                        //Main.SpriteBatch.Draw(Main.FlatTexture, player.HitboxSouth, Color.Red * 0.25F);
                        //Main.SpriteBatch.Draw(Main.FlatTexture, player.HitboxNorth, Color.Red * 0.25F * 0.25F);
                        //Main.SpriteBatch.Draw(Main.FlatTexture, player.HitboxEast, Color.Red * 0.25F);
                        //Main.SpriteBatch.Draw(Main.FlatTexture, player.HitboxWest, Color.Red * 0.25F);
                    }
                    catch (Exception ex)
                    {
                        Loggers.Error($"{ex}");
                    }
                }
                else
                {
                    try
                    {
                        //Loggers.Info($"W:{this.Width} H:{this.Height} ID:{facingId} HF:{facingId * this.Height}");
                        //Loggers.Debug($"Id:{this.npc.whoAmI} Name:{this.npc.DisplayName} {destSprites}");
                        sprite.Draw(Main.NPCTexture[this.npc.type], npc.Sprite, destSprites, Color.White);
                    }
                    catch (Exception ex)
                    {
                        Loggers.Error($"{ex}");
                    }
                }
            }
        }
    }
}
