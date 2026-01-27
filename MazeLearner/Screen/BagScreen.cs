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
            this.EntryMenus.Add(new MenuEntry(0, "Inventory", () => { }));
            this.EntryMenus.Add(new MenuEntry(1, "Emote", () => { }));
            this.EntryMenus.Add(new MenuEntry(2, "Settings", () => { }));
            this.EntryMenus.Add(new MenuEntry(3, "Save", () => { }));
            this.EntryMenus.Add(new MenuEntry(4, "Exit to Menu", () => 
            {
                Main.GameState = GameState.Title;
                this.game.SetScreen(new TitleScreen(TitleSequence.Title));
            }));
        }
        public override void Update(GameTime gametime)
        {
            base.Update(gametime);
            if (Main.Keyboard.Pressed(GameSettings.KeyBack))
            {
                Main.GameState = GameState.Play;
                this.game.SetScreen(null);
            }
        }
        public override void Render(SpriteBatch sprite)
        {
            base.Render(sprite);
            int QBPSize = 240;
            int QBPW = this.game.GetScreenWidth() - QBPSize;
            int QBPH = 240;
            int ButtonPadding = 40;
            Rectangle bagBox = new Rectangle(QBPW - ButtonPadding, QBPH - ButtonPadding, this.game.GetScreenWidth() - QBPW, QBPH + (ButtonPadding * this.EntryMenus.Count));
            sprite.DrawMessageBox(AssetsLoader.Box0.Value, bagBox, Color.White, 32);
            TextManager.Text(Fonts.DT_L, "Menu", new Vector2(QBPW, QBPH));
            QBPH += ButtonPadding * 2;
            foreach (MenuEntry entries in this.EntryMenus)
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
            Vector2 outputKeybinds = TextManager.MeasureString(Fonts.DT_L, textKeybinds);
            Vector2 outputKPos = new Vector2(0 + keybindsTextPadding, this.game.GetScreenHeight() - (outputKeybinds.Y + 20));
            Rectangle outputBox = new Rectangle((int)outputKPos.X - 20, (int)outputKPos.Y, (int)outputKeybinds.X, (int)outputKeybinds.Y);
            sprite.DrawMessageBox(AssetsLoader.Box1.Value, outputBox, Color.White, 32);
            TextManager.Text(Fonts.DT_L, textKeybinds, outputKPos);
        }
    }
}
