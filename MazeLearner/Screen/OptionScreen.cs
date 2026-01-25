using MazeLearner.Screen.Widgets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.Screen
{
    /// <summary>
    /// Option Screen 
    /// 
    /// [Graphics]
    /// VSync
    /// FPS
    /// 
    /// [Audio]
    /// Master Volume
    /// Background Volume
    /// Effect Volume
    /// 
    /// [Accessibility]
    /// Keybinds
    /// Cursor
    /// 
    /// </summary>
    public class OptionScreen : BaseScreen
    {
        public enum OptionState
        {
            None,
            Graphics,
            Audtio,
            Accessiblity
        }

        public OptionState OptionStates { get; set; } = OptionState.None;

        private int boxPadding = 32;
        private int boxX = 0;
        private int boxY = 20;
        private int boxW 
        { 
            get
            {
                return this.game.GetScreenWidth();
            }
            set
            {
                this.boxW = value;
            }
        }
        private int boxH
        {
            get
            {
                return this.game.GetScreenHeight();
            }
            set
            {
                this.boxH = value;
            }
        }
        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle(this.boxX + boxPadding, this.boxY, boxW - (boxX + (boxPadding * 2)), boxH - (boxY + boxPadding));
            }
            set
            {
                this.boxX = value.X;
                this.boxY = value.Y;
                this.boxW = value.Width;
                this.boxH = value.Height;
            }
        }
        private SimpleButton BackButton;
        public OptionScreen(OptionState optionState = OptionState.None) : base("")
        {
            this.OptionStates = optionState;
        }

        public override void LoadContent()
        {
            int scale = 1;
            int w = 240 * scale;
            int h = 40 * scale;
            this.posX = this.boxX + w;
            this.posY = this.boxY;
            int entryMenuY = this.posY;
            this.BackButton = new SimpleButton(this.posX, entryMenuY, w, h, () =>
            {
                this.game.SetScreen(new TitleScreen(TitleSequence.Title));
            });
            this.BackButton.Text = "Back";
            this.AddRenderableWidgets(BackButton);
        }

        public override void RenderBackground(SpriteBatch sprite)
        {
            base.RenderBackground(sprite);
            this.game.RenderBackground(sprite);
            sprite.DrawMessageBox(AssetsLoader.Box0.Value, this.BoundingBox, Color.White, 32);
        }
    }
}
