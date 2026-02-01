using MazeLeaner.Text;
using MazeLearner.Audio;
using MazeLearner.Screen.Components;
using MazeLearner.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace MazeLearner.Screen
{
    public class MenuEntry
    {
        public int index { get; set; }
        public string text { get; set; }
        public Rectangle box { get; set; }
        public Action action { get; set; }
        public Texture2D texture { get; set; } = null;
        public MenuEntry(int index, string text, Rectangle box, Action action, Texture2D texture = null)
        {
            this.index = index;
            this.text = text;
            this.box = box;
            this.action = action;
            this.texture = texture;
        }
    }

    public abstract class BaseScreen
    {
        //public record MenuEntry(int index, int x, int y, string text, Action action);
        //public record MenuEntry(int index, string text, Rectangle box, Action action, Texture2D texture = null);
        //public record MenuEntry(int index, string text, Rectangle box, Action action, Texture2D texture = null)
        //{
        //    private string _text = text;
        //    public string Text { get { return _text; } set { _text = value; } }
        //}
        private int _indexBtn = 0;
        protected List<MenuEntry> EntryMenus = new List<MenuEntry>();
        public int IndexBtn
        {
            get { return _indexBtn; }
            set { _indexBtn = value; }
        }
        private List<GuiEventListener> childrens = new List<GuiEventListener>();
        private List<Renderables> renderables = new List<Renderables>();
        private GuiEventListener focusedWidget = null;
        public int screenId = 0;
        private static int screenIds = 1;
        public Vector2 TitlePosition;
        public string name;
        public Main game;
        public int posX = 0;
        public int posY = 0;
        public int tick;
        protected BaseScreen(string name) 
        {
            this.game = Main.Instance;
            this.name = name;
            this.screenId = BaseScreen.createScreenId();
        }

        private static int createScreenId()
        {
            return screenIds++;
        }

        public void Draw(SpriteBatch sprite)
        {
            this.Render(sprite, this.game.graphicRenderer);
            var sorted = renderables.OrderBy(r =>
            {
                if (r is GuiEventListener g) return g.GetTabOrderGroup();
                return 0;
            }).ToList();

            this.RenderBackground(sprite, this.game.graphicRenderer);
            foreach (var renderable in sorted)
            {
                renderable.Draw(sprite, Main.Mouse.Position);
            }
            this.Render(sprite, this.game.graphicRenderer);
            this.RenderEntryMenus(sprite);
            this.RenderTooltips(sprite);
            if (this.ShowOverlayKeybinds() == true)
            {
                this.OverlayKeybinds(sprite);
            }
        }
        public virtual void LoadContent() 
        {
            Loggers.Msg("All screen is loaded!");
        }

        protected T AddRenderableWidgets<T>(T widgets) where T : GuiEventListener, Renderables
        {
            this.renderables.Add(widgets);
            return widgets;
        }
        protected T AddWidgets<T>(T listener) where T : GuiEventListener
        {
            this.childrens.Add(listener);
            return listener;
        }
        protected void RemoveWidgets(GuiEventListener listener)
        {
            if (listener is Renderables)
            {
                this.renderables.Remove((Renderables)listener);
            }
        }

        public virtual void Update(GameTime gametime)
        {
            this.tick++;
            this.MouseClicked(Main.Mouse.Position);
            var sorted = renderables.OrderBy(r =>
            {
                if (r is GuiEventListener g) return g.GetTabOrderGroup();
                return 0;
            }).ToList();
            foreach (var widgets in this.childrens)
            {
                widgets.Update(gametime);
            }
            if (Main.Keyboard.Pressed(GameSettings.KeyForward))
            {
                this.IndexBtn -= 1;
                this.PlaySoundClick();
                if (this.IndexBtn < 0)
                {
                    this.IndexBtn = this.EntryMenus.Count - 1;
                }
            }
            if (Main.Keyboard.Pressed(GameSettings.KeyDownward))
            {
                this.IndexBtn += 1;
                this.PlaySoundClick();
                if (this.IndexBtn > this.EntryMenus.Count - 1)
                {
                    this.IndexBtn = 0;
                }
            }
            if (Main.Keyboard.Pressed(GameSettings.KeyInteract))
            {
                foreach (MenuEntry entries in this.EntryMenus)
                {
                    int btnIndex = entries.index;
                    if (this.IndexBtn == btnIndex)
                    {
                        entries.action?.Invoke();
                    }
                }
            }
        }

        public virtual bool ShowOverlayKeybinds()
        {
            return true;
        }
        public virtual void PlaySoundClick()
        {
            Main.Audio.PlaySoundEffect(AudioAssets.ClickedSFX.Value);
            Main.Audio.SoundEffectVolume = 0.15F;
        }
        public void FadeBlackScreen(SpriteBatch sprite, float alpha = 1.0F)
        {
            sprite.Draw(Main.FlatTexture, this.game.WindowScreen, Color.Black * alpha);
        }

        public virtual void MouseClicked(Vector2 pos)
        {
            var sorted = renderables.OfType<GuiEventListener>().OrderByDescending(r => r.GetTabOrderGroup()).ToList();
            foreach (var listener in sorted)
            {
                if (listener.MouseClicked(Main.Mouse.Position, Main.Mouse))
                {
                    this.SetFocusedWidget(listener);
                }
            }
        }
        private void SetFocusedWidget(GuiEventListener listener)
        {
            if (this.focusedWidget != listener)
            {
                this.focusedWidget?.SetFocused(false);
                this.focusedWidget = listener;
                this.focusedWidget?.SetFocused(true);
            }
        }
        public virtual bool KeyPressed(KeyboardHandler key)
        {
            if (key.Pressed(Keys.Escape) && this.UseEscToExit() == true)
            {
                this.ExitScreen();
                return true;
            }
            return false;
        }
        protected bool UseEscToExit()
        {
            return false;
        }

        public virtual void RenderTooltips(SpriteBatch sprite)
        {
        }
        public virtual void Render(SpriteBatch sprite, GraphicRenderer graphic)
        {
        }

        public virtual void RenderEntryMenus(SpriteBatch sprite)
        {
            foreach (MenuEntry entries in this.EntryMenus)
            {
                int btnIndex = entries.index;
                string text = entries.text;
                bool isHovered = this.IndexBtn == btnIndex;
                // Note from Ken: The width of bounding box of entry menus will adjust to the size of the text...
                Vector2 textsize = TextManager.MeasureString(Fonts.DT_L, text);
                Rectangle dst = new Rectangle(entries.box.X, (int)(entries.box.Y - (textsize.Y / 2)), entries.box.Width, (int)(entries.box.Height + (textsize.Y / 2)));
                if (entries.texture != null)
                {
                    if (entries.texture != null)
                    {
                        Rectangle src = new Rectangle(0, (entries.texture.Height / 2) * (isHovered ? 1 : 0), entries.box.Width, (int) (entries.texture.Height / 2));
                        sprite.Draw(entries.texture, dst, src, Color.White);
                    }
                }
                if (this.IndexBtn == btnIndex)
                {
                    //TextManager.Text(Fonts.DT_L, "> ", new Vector2(entries.box.X - 24, entries.box.Y));
                    sprite.Draw(AssetsLoader.Arrow.Value, new Rectangle(entries.box.X, entries.box.Y, AssetsLoader.Arrow.Value.Width, AssetsLoader.Arrow.Value.Height), Color.White);
                }
                int paddingText = isHovered ? 1 : 0;
                TextManager.Text(Fonts.DT_L, text, new Vector2(dst.X + 12 + (AssetsLoader.Arrow.Value.Width * (isHovered ? 1 : 0)), dst.Y - 3 + (textsize.Y / 2)));
            }
        }
        public void OverlayKeybinds(SpriteBatch sprite)
        {
            int keybindsTextPadding = 20;
            string textKeybinds = $"Next: {GameSettings.KeyForward} | Back: {GameSettings.KeyDownward} | Confirm: {GameSettings.KeyInteract} | Cancel: {GameSettings.KeyBack}";
            Vector2 outputKeybinds = TextManager.MeasureString(Fonts.DT_L, textKeybinds);
            Vector2 outputKPos = new Vector2(0 + keybindsTextPadding, this.game.GetScreenHeight() - (outputKeybinds.Y + 20) + 2);
            Rectangle outputBox = new Rectangle((int)outputKPos.X - 20, (int)outputKPos.Y, (int)outputKeybinds.X, (int)outputKeybinds.Y);
            sprite.DrawMessageBox(AssetsLoader.Box1.Value, outputBox, Color.White, 32);
            TextManager.Text(Fonts.DT_L, textKeybinds, outputKPos, Color.White);
        }
        public virtual void RenderBackground(SpriteBatch sprite, GraphicRenderer graphic)
        {
        }
        public void ExitScreen()
        {
            this.game.SetScreen((BaseScreen) null);
        }
    }
}
