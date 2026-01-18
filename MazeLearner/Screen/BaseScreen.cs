using MazeLearner.Screen.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.Screen
{
    public abstract class BaseScreen
    {
        public List<GuiEventListener> childrens = new List<GuiEventListener>();
        public List<Renderables> renderables = new List<Renderables>();
        private GuiEventListener focusedWidget = null;
        public int screenId = 0;
        private static int screenIds = 1;
        public Vector2 TitlePosition;
        public string name;
        public Main game;
        public int posX = 0;
        public int posY = 0;
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
            this.Render(sprite);
            this.RenderBackground(sprite);
        }
        public virtual void LoadContent() { }
        public virtual void Update(GameTime gametime)
        { 
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

        public virtual void RenderTooltips(SpriteBatch sprite, Vector2 pos)
        {
        }
        public virtual void Render(SpriteBatch sprite)
        {
        }
        public virtual void RenderBackground(SpriteBatch sprite)
        {
        }
        public void ExitScreen()
        {
            this.game.SetScreen((BaseScreen) null);
        }
    }
}
