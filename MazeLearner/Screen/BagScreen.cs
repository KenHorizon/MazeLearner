using MazeLeaner.Text;
using MazeLearner.GameContent.Entity;
using MazeLearner.Screen.Widgets;
using MazeLearner.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MazeLearner.Screen
{
    public class BagScreen : BaseScreen
    {

        private static Assets<Texture2D> BagMenuIcons = Assets<Texture2D>.Request("UI/BagMenuIcons");
        public BagScreen() : base("")
        {
        }
        public override void LoadContent()
        {
            base.LoadContent();
            this.EntryMenus.Add(new BagMenuEntry(0, "Inventory", () => { }));
            this.EntryMenus.Add(new BagMenuEntry(1, "Emote", () => { }));
            this.EntryMenus.Add(new BagMenuEntry(2, "Settings", () => { }));
            this.EntryMenus.Add(new BagMenuEntry(3, "Save", () => { }));
            this.EntryMenus.Add(new BagMenuEntry(4, "Exit to Menu", () => 
            {
                Main.GameState = GameState.Title;
                this.game.SetScreen(new TitleScreen(TitleSequence.Title));
            }));
        }
        public override void Update(GameTime gametime)
        {
            base.Update(gametime);
            if (Main.Keyboard.Pressed(GameSettings.KeyForward))
            {
                this.IndexBtn -= 1;
                if (this.IndexBtn < 0)
                {
                    this.IndexBtn = this.EntryMenus.Count - 1;
                }
            }
            if (Main.Keyboard.Pressed(GameSettings.KeyDownward))
            {
                this.IndexBtn += 1;
                if (this.IndexBtn > this.EntryMenus.Count - 1)
                {
                    this.IndexBtn = 0;
                }
            }
            if (Main.Keyboard.Pressed(GameSettings.KeyInteract))
            {
                foreach (BagMenuEntry entries in this.EntryMenus)
                {
                    int btnIndex = entries.index;
                    if (this.IndexBtn ==  btnIndex)
                    {
                        entries.action?.Invoke();
                    }
                }
            }
            if (Main.Keyboard.Pressed(GameSettings.KeyBack))
            {
                Main.GameState = GameState.Play;
                this.game.SetScreen(null);
            }
        }
        public override void Render(SpriteBatch sprite)
        {
            base.Render(sprite);
            int QBPW = this.game.GetScreenWidth() - 240;
            int QBPH = 240;
            int ButtonPadding = 40;
            Rectangle bagBox = new Rectangle(this.game.GetScreenWidth() - QBPW, QBPH, QBPW, this.game.GetScreenHeight());
            sprite.DrawMessageBox(AssetsLoader.Box0.Value, bagBox, Color.White, 32);
            TextManager.Text(Fonts.DT_L, "Menu", new Vector2(QBPW, QBPH));
            QBPH += ButtonPadding * 2;
            foreach (BagMenuEntry entries in this.EntryMenus)
            {
                int btnIndex = entries.index;
                string text = entries.text;
                TextManager.Text(Fonts.DT_L, text, new Vector2(QBPW, QBPH));
                if (this.IndexBtn == btnIndex)
                {
                    TextManager.Text(Fonts.DT_L, "> ", new Vector2(QBPW - 24, QBPH));
                }
                QBPH += ButtonPadding;
            }
            int keybindsTextPadding = 20;
            string textKeybinds = $"Next: {GameSettings.KeyForward} | Back: {GameSettings.KeyDownward} | Confirm: {GameSettings.KeyInteract} | Cancel: {GameSettings.KeyBack}";
            Vector2 textSizeKeybinds = TextManager.MeasureString(Fonts.DT_L, textKeybinds);
            TextManager.Text(Fonts.DT_L, textKeybinds, new Vector2(0 + keybindsTextPadding, this.game.GetScreenHeight() - (textSizeKeybinds.Y + 20)));
        }
    }
}
