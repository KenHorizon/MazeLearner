using MazeLeaner.Text;
using MazeLearner.Screen.Components;
using MazeLearner.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
            int entryMenuSize = AssetsLoader.BagMenu.Value.Width;
            int entryH = AssetsLoader.BagMenu.Value.Height;
            int entryX = (this.game.GetScreenWidth() - entryMenuSize) / 2;
            int entryY = 180;
            int ButtonPadding = AssetsLoader.BagMenu.Value.Height + 12;
            this.EntryMenus.Add(new MenuEntry(0, "Inventory", new Rectangle(entryX, entryY, entryMenuSize, entryH), () => 
            {
                
            }, AssetsLoader.BagMenu.Value));

            entryY += ButtonPadding; 
            this.EntryMenus.Add(new MenuEntry(1, "Emote", new Rectangle(entryX, entryY, entryMenuSize, entryH), () => 
            {
                
            }, AssetsLoader.BagMenu.Value));

            entryY += ButtonPadding;
            this.EntryMenus.Add(new MenuEntry(2, "Settings", new Rectangle(entryX, entryY, entryMenuSize, entryH), () => 
            {
                
            }, AssetsLoader.BagMenu.Value));

            entryY += ButtonPadding; 
            this.EntryMenus.Add(new MenuEntry(3, "Save", new Rectangle(entryX, entryY, entryMenuSize, entryH), () =>
            {
                Main.GameState = GameState.Title;

                this.game.SetScreen(new TitleScreen(TitleSequence.Title));
            }, AssetsLoader.BagMenu.Value));

            entryY += ButtonPadding; 
            this.EntryMenus.Add(new MenuEntry(4, "Exit to Menu", new Rectangle(entryX, entryX, entryMenuSize, entryH), () => 
            {
                Main.GameState = GameState.Title;
                this.game.SetScreen(new TitleScreen(TitleSequence.Title));
            }, AssetsLoader.BagMenu.Value));
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
        public override void Render(SpriteBatch sprite, GraphicRenderer graphic)
        {
            base.Render(sprite, graphic);
            TextManager.Text(Fonts.DT_L, Main.GetActivePlayer.name, new Vector2(Main.MaxTileSize * 2, 40));
        }
    }
}
