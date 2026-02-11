using MazeLeaner.Text;
using MazeLearner.Audio;
using MazeLearner.Graphics;
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
            Loggers.Info("All screen is loaded!");
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
            foreach (var widgets in this.renderables)
            {
                if (widgets is GuiEventListener listener)
                {
                    listener.Update(gametime);
                }
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
                    int btnIndex = entries.Index;
                    if (this.IndexBtn == btnIndex && entries.IsActive == true)
                    {
                        entries.Action?.Invoke();
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
            Main.SoundEngine.Play(AudioAssets.ClickedSFX.Value);
        }
        public void FadeBlackScreen(SpriteBatch sprite, float alpha = 1.0F)
        {
            sprite.Draw(Main.FlatTexture, Main.WindowScreen, Color.Black * alpha);
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
        public virtual void Render(SpriteBatch sprite, Graphic graphic)
        {
        }

        public virtual void RenderEntryMenus(SpriteBatch sprite)
        {
            foreach (MenuEntry entries in this.EntryMenus)
            {
                if (entries.IsActive == true)
                {
                    int btnIndex = entries.Index;
                    string text = entries.Text;
                    bool isHovered = this.IndexBtn == btnIndex;
                    // Note from Ken: The width of bounding box of entry menus will adjust to the size of the text...
                    Vector2 textsize = TextManager.MeasureString(Fonts.DT_L, text);
                    Rectangle dst = new Rectangle(entries.Box.X, (int)(entries.Box.Y - (textsize.Y / 2)), entries.Box.Width, (int)(entries.Box.Height + (textsize.Y / 2)));

                    Vector2 textSize = TextManager.MeasureString(Fonts.DT_L, entries.Text);
                    if (entries.Texture != null)
                    {
                        if (entries.Texture != null)
                        {
                            int textH = (int)(entries.Texture.Height);
                            bool flag = textH <= dst.Height;
                            if (flag == false)
                            {
                                Rectangle src = new Rectangle(0, (entries.Texture.Height / 2) * (isHovered ? 1 : 0), entries.Box.Width, (int)(entries.Texture.Height / 2));
                                sprite.Draw(entries.Texture, dst, src, Color.White);
                            }
                            else
                            {
                                sprite.Draw(entries.Texture, dst, Color.White);
                            }
                        }
                    }
                    if (this.IndexBtn == btnIndex)
                    {
                        int y = (int)(entries.Box.Y + ((dst.Height - textsize.Y - AssetsLoader.Arrow.Value.Height) / 2));
                        sprite.Draw(AssetsLoader.Arrow.Value, new Rectangle(entries.Box.X, y, AssetsLoader.Arrow.Value.Width, AssetsLoader.Arrow.Value.Height), Color.White);
                    }
                    int paddingText = isHovered ? 1 : 0;
                    if (entries.Anchor == AnchorMainEntry.Center)
                    {
                        int x = (int)(dst.X + ((dst.Width - textSize.X) / 2));
                        int y = (int)(dst.Y + ((dst.Height - textsize.Y) / 2));
                        TextManager.Text(Fonts.DT_L, text, new Vector2(x, y));
                    }
                    if (entries.Anchor == AnchorMainEntry.Left)
                    {
                        int x = dst.X + 20 + (AssetsLoader.Arrow.Value.Width * (isHovered ? 1 : 0));
                        int y = (int)(dst.Y + ((dst.Height - textsize.Y) / 2));
                        TextManager.Text(Fonts.DT_L, text, new Vector2(x, y));
                    }
                    if (entries.Anchor == AnchorMainEntry.Right)
                    {
                        int x = (int)(dst.X + entries.Box.Width - (12 + textSize.X));
                        int y = (int)(dst.Y + ((dst.Height - textsize.Y) / 2));
                        TextManager.Text(Fonts.DT_L, text, new Vector2(x, y));
                    }
                }
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
        public virtual void RenderBackground(SpriteBatch sprite, Graphic graphic)
        {
        }
        public void ExitScreen()
        {
            this.game.SetScreen((BaseScreen) null);
        }
    }
}
