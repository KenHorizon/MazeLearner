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
            int entryMenuSize = 240;
            int entryX = (this.game.GetScreenWidth() - entryMenuSize) / 2;
            int entryY = 180;
            int ButtonPadding = 40;
            this.EntryMenus.Add(new MenuEntry(0, "Inventory", new Rectangle(entryX, entryY, entryMenuSize, 32), () => 
            {
                
            }));

            entryY += ButtonPadding; 
            this.EntryMenus.Add(new MenuEntry(1, "Emote", new Rectangle(entryX, entryY, entryMenuSize, 32), () => 
            {
                
            }));

            entryY += ButtonPadding;
            this.EntryMenus.Add(new MenuEntry(2, "Settings", new Rectangle(entryX, entryY, entryMenuSize, 32), () => 
            {
                
            }));

            entryY += ButtonPadding; 
            this.EntryMenus.Add(new MenuEntry(3, "Save", new Rectangle(entryX, entryY, entryMenuSize, 32), () =>
            {
                Main.GameState = GameState.Title;

                this.game.SetScreen(new TitleScreen(TitleSequence.Title));
            }));

            entryY += ButtonPadding; 
            this.EntryMenus.Add(new MenuEntry(4, "Exit to Menu", new Rectangle(entryX, entryX, entryMenuSize, 32), () => 
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
        }
    }
}
