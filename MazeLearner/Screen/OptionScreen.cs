
using MazeLearner.Graphics;
using MazeLearner.Localization;
using MazeLearner.Screen.Components;
using MazeLearner.Screen.Widgets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

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
        private Slider BGMSlider;
        private Slider SFXSlider;
        private MenuEntry SaveEntry;
        private MenuEntry ExitEntry;
        private bool openinstruction;
        private bool _ingame; // tell if player open the settings on mainmenu or in-game
        public bool InGame => this._ingame;
        public OptionScreen(bool ingame = false) : base("") 
        {
            this._ingame = ingame;
        }

        public override void LoadContent()
        {
            int entryMenuSize = AssetsLoader.MenuBtn0.Value.Width;
            int entryMenuH = AssetsLoader.MenuBtn0.Value.Height / 2;
            int entryX = ((this.BoundingBox.X + Main.MaxTileSize * 6) - entryMenuSize + 88) / 2;
            int entryY = Main.MaxTileSize * 2;
            int entryPadding = entryMenuH + 20;
            int sliderW = (this.BoundingBox.Width / 2) - this.boxPadding;
            int sliderX = (this.game.GetScreenWidth() / 2);
            this.BGMSlider = new Slider(0, 100, GameSettings.BackgroundMusic, sliderX, entryY, sliderW, entryMenuH, () =>
            {
            });
            this.BGMSlider.Index = 0;
            this.EntryMenus.Add(new MenuEntry(0, Resources.OptionAudioBGM, new Rectangle(entryX, entryY, entryMenuSize, entryMenuH), () =>
            {

            }));

            entryY += entryPadding;
            this.EntryMenus.Add(new MenuEntry(1, Resources.OptionAudioSFX, new Rectangle(entryX, entryY, entryMenuSize, entryMenuH), () =>
            {

            }));
            this.SFXSlider = new Slider(0, 100, GameSettings.SFXMusic, sliderX, entryY, sliderW, entryMenuH, () =>
            {
            });
            this.SFXSlider.Index = 1;
            entryY += entryPadding;
            this.EntryMenus.Add(new MenuEntry(2, Resources.OptionKeybinds, new Rectangle(entryX, entryY, entryMenuSize, entryMenuH), () =>
            {
                this.openinstruction = !this.openinstruction;
            }));

            entryY += entryPadding;
            this.SaveEntry = new MenuEntry(3, Resources.Save, new Rectangle(entryX, entryY, entryMenuSize, entryMenuH), () =>
            {
                Main.Settings.Save();
            });
            entryY += entryPadding;
            this.EntryMenus.Add(this.SaveEntry);
            this.ExitEntry = new MenuEntry(4, Resources.Exit, new Rectangle(entryX, entryY, entryMenuSize, entryMenuH), () =>
            {
                if (this.InGame == true)
                {
                    Main.GameState = GameState.Play;
                    this.game.SetScreen(null);
                }
                else
                {
                    this.game.SetScreen(new TitleScreen(TitleSequence.Title));
                }
            });
            this.EntryMenus.Add(this.ExitEntry);
            this.AddRenderableWidgets(this.BGMSlider);
            this.AddRenderableWidgets(this.SFXSlider);
        }

        public override void Update(GameTime gametime)
        {
            base.Update(gametime);
           
            if (this.openinstruction == true)
            {
                if (Main.Keyboard.Pressed(GameSettings.KeyBack))
                {
                    this.openinstruction = false;
                    foreach (var entry in this.EntryMenus)
                    {
                        entry.IsActive = true;
                    }
                }
            } else
            {
                if (Main.Keyboard.Pressed(GameSettings.KeyBack))
                {
                    if (this.InGame == true)
                    {
                        Main.GameState = GameState.Play;
                        this.game.SetScreen(null);
                    }
                    else
                    {
                        this.game.SetScreen(new TitleScreen(TitleSequence.Title));
                    }
                }
                int entryMenuSize = AssetsLoader.MenuBtn0.Value.Width;
                int entryMenuH = AssetsLoader.MenuBtn0.Value.Height / 2;
                int entryX = ((this.BoundingBox.X + Main.MaxTileSize * 6) - entryMenuSize + 88) / 2;
                int entryY = 200;
                int entryPadding = entryMenuH + 20;
                if (this.BGMSlider.HasChange || this.SFXSlider.HasChange)
                {
                    this.SaveEntry.IsActive = true;
                }
                GameSettings.BackgroundMusic = this.BGMSlider.Amount;
                GameSettings.SFXMusic = this.SFXSlider.Amount;
                foreach (var entry in this.EntryMenus)
                {
                    if (entry.Index == this.IndexBtn)
                    {
                        this.BGMSlider.SetFocused(this.IndexBtn == this.BGMSlider.Index);
                        this.SFXSlider.SetFocused(this.IndexBtn == this.SFXSlider.Index);
                    }
                }
            }
        }

        public override void RenderBackground(SpriteBatch sprite, Graphic graphic)
        {
            base.RenderBackground(sprite, graphic);
            this.game.RenderBackground(sprite);
            sprite.DrawMessageBox(AssetsLoader.Box2.Value, this.BoundingBox, Color.White, 32);
        }
        public override void Render(SpriteBatch sprite, Graphic graphic)
        {
            base.Render(sprite, graphic);

            if (this.openinstruction == true)
            {
                foreach (var entry in this.EntryMenus)
                {
                    entry.IsActive = false;
                }
                graphic.RenderKeybindInstruction(sprite);
            }
        }
        public override bool ShowOverlayKeybinds()
        {
            return base.ShowOverlayKeybinds() && this.openinstruction == false;
        }
    }
}
