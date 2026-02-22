using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.Screen.Components
{
    public class BaseCheckbox : BaseWidgets
    {
        private Action _action;
        private bool _checked;
        public Action OnUpdate
        {
            get { return _action; } 
            set { _action = value; }
        }
        public bool Checked
        {
            get { return _checked; }
            set { _checked = value; }
        }
        public BaseCheckbox(int x, int y, int width, int height, bool defaultVal) : base(x, y, width, height)
        {
            this.Checked = defaultVal;
        }



        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (this.IsFocused() == true && (Main.Input.Pressed(GameSettings.KeyInteract) || Main.Input.Pressed(GameSettings.KeyConfirm)))
            {
                this.Checked = !this.Checked;
            }
        }

        public override void Render(SpriteBatch sprite, Vector2 mouse)
        {
            base.Render(sprite, mouse);

            Rectangle src = new Rectangle(
                0,
                (AssetsLoader.Checkbox.Value.Height / 2) * (this.Checked ? 1 : 0),
                AssetsLoader.Checkbox.Value.Width,
                (int)(AssetsLoader.Checkbox.Value.Height / 2));

            sprite.Draw(AssetsLoader.Checkbox.Value, this.Bounds, src, Color.White);
        }

        public override bool DoSoundHovered()
        {
            return false;
        }
    }
}
