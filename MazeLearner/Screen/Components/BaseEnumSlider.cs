using MazeLeaner.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.Screen.Components
{
    public class BaseEnumSlider<T> : BaseWidgets where T : Enum
    {
        private Action _action;
        private int _indexList = 0;
        public int IndexList
        {
            get { return _indexList; } set { _indexList = value; }
        }
        private int _defVal;
        private T[] _options;
        public Action OnUpdate
        {
            get { return _action; } 
            set { _action = value; }
        }
        public int DefVal
        {
            get { return _defVal; }
            set { _defVal = value; }
        }
        public T[] Options
        {
            get { return _options; }
            set { _options = value; }
        }
        public BaseEnumSlider(int x, int y, int width, int height, int defVal) : base(x, y, width, height)
        {
            this.DefVal = defVal;
            this.Options = (T[])Enum.GetValues(typeof(T));
        }
        public string Get(int index)
        {
            return Options[index].ToString(); 
        }


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (this.IsFocused())
            {
                this.HandleInput();
            }
        }

        public void HandleInput()
        {
            if (Main.Input.Pressed(GameSettings.KeyConfirm) || Main.Input.Pressed(GameSettings.KeyInteract))
            {
                this.OnUpdate?.Invoke();
            }
            if (Main.Input.Pressed(GameSettings.KeyLeft))
            {
                this.IndexList--;
                if (this.IndexList < 0)
                {
                    this.IndexList = 0;
                }

            }
            if (Main.Input.Pressed(GameSettings.KeyRight))
            {
                this.IndexList++;
                if (this.IndexList > (this.Options.Length - 1))
                {
                    this.IndexList = this.Options.Length - 1;
                }
            }
        }

        public override void Render(SpriteBatch sprite, Vector2 mouse)
        {
            base.Render(sprite, mouse);
            sprite.NinePatch(AssetsLoader.Box4.Value, this.Bounds, Color.White);
            Texts.DrawString($"{this.Options[this.IndexList]}", this.Bounds.Vec2(10, (this.Bounds.Height / 2) - 12));
        }

        public override bool DoSoundHovered()
        {
            return false;
        }
    }
}
