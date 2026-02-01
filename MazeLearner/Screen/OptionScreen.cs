using MazeLearner.Audio;
using MazeLearner.Localization;
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
        public OptionScreen() : base("") { }

        public override void LoadContent()
        {
            int entryMenuSize = 240;
            int entryX = (this.game.GetScreenWidth() - entryMenuSize) / 2;
            int entryY = 200;
            int entryPadding = 40;
            this.EntryMenus.Add(new MenuEntry(0, Resources.OptionAudioBGM, new Rectangle(entryX, entryY, entryMenuSize, 32), () =>
            {

            }, AssetsLoader.MenuBtn0.Value));

            entryY += entryPadding;
            this.EntryMenus.Add(new MenuEntry(1, Resources.OptionAudioSFX, new Rectangle(entryX, entryY, entryMenuSize, 32), () =>
            {

            }, AssetsLoader.MenuBtn0.Value));

            entryY += entryPadding;
            this.EntryMenus.Add(new MenuEntry(2, Resources.OptionKeybinds, new Rectangle(entryX, entryY, entryMenuSize, 32), () =>
            {

            }, AssetsLoader.MenuBtn0.Value));

            entryY += entryPadding;
            this.EntryMenus.Add(new MenuEntry(3, Resources.Exit, new Rectangle(entryX, entryY, entryMenuSize, 32), () =>
            {
                this.game.SetScreen(new TitleScreen(TitleSequence.Title));
            }, AssetsLoader.MenuBtn0.Value));
        }
        public override void RenderBackground(SpriteBatch sprite, GraphicRenderer graphic)
        {
            base.RenderBackground(sprite, graphic);
            this.game.RenderBackground(sprite);
            sprite.DrawMessageBox(AssetsLoader.Box0.Value, this.BoundingBox, Color.White, 32);
        }
    }
}
