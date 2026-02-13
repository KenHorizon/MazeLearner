using MazeLeaner.Text;
using MazeLearner.GameContent.Entity.Player;
using MazeLearner.Graphics;
using MazeLearner.Localization;
using MazeLearner.Screen.Components;
using MazeLearner.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MazeLearner.Screen
{
    public class BagScreen : BaseScreen
    {
        public BagScreen() : base("")
        {
        }
        public override void LoadContent()
        {
            Loggers.Info($"{Main.PlayerListIndex}");
            base.LoadContent();
            int entryMenuSize = AssetsLoader.BagMenu.Value.Width;
            int entryH = AssetsLoader.BagMenu.Value.Height;
            int entryX = (this.game.GetScreenWidth() - entryMenuSize) / 2;
            int entryY = 180;
            int ButtonPadding = AssetsLoader.BagMenu.Value.Height + 12;
            this.EntryMenus.Add(new MenuEntry(0, Resources.Inventory, new Rectangle(entryX, entryY, entryMenuSize, entryH), () => 
            {
                
            }, AssetsLoader.BagMenu.Value));

            entryY += ButtonPadding; 
            this.EntryMenus.Add(new MenuEntry(1, Resources.Settings, new Rectangle(entryX, entryY, entryMenuSize, entryH), () => 
            {
                this.game.SetScreen(new OptionScreen(true));
            }, AssetsLoader.BagMenu.Value));

            entryY += ButtonPadding; 
            this.EntryMenus.Add(new MenuEntry(2, Resources.Save, new Rectangle(entryX, entryY, entryMenuSize, entryH), () =>
            {
                PlayerEntity.SavePlayerData(Main.GetActivePlayer, Main.PlayerListPath[Main.PlayerListIndex]);
            }, AssetsLoader.BagMenu.Value));

            entryY += ButtonPadding; 
            this.EntryMenus.Add(new MenuEntry(3, Resources.ExitToMenu, new Rectangle(entryX, entryX, entryMenuSize, entryH), () => 
            {
                Main.GameState = GameState.Title;
                this.game.SetScreen(new TitleScreen(TitleSequence.Title));
            }, AssetsLoader.BagMenu.Value));
        }
        public override void Update(GameTime gametime)
        {
            base.Update(gametime);
            if (Main.Input.Pressed(GameSettings.KeyBack))
            {
                Main.GameState = GameState.Play;
                this.game.SetScreen(null);
            }
        }
        public override void Render(SpriteBatch sprite, Graphic graphic)
        {
            base.Render(sprite, graphic);
            int entryMenuSize = AssetsLoader.BagMenu.Value.Width;
            int entryH = AssetsLoader.BagMenu.Value.Height;
            int entryX = (this.game.GetScreenWidth() - entryMenuSize) / 2;
            int entryY = 180;
            int ButtonPadding = AssetsLoader.BagMenu.Value.Height + 12;
            entryY -= ButtonPadding;
            TextManager.Text(Fonts.DT_L, Resources.MainMenu, new Vector2(entryX, entryY));
            entryY -= ButtonPadding;
            TextManager.Text(Fonts.DT_L, Main.GetActivePlayer.DisplayName, new Vector2(entryX, entryY));
            sprite.Draw(AssetsLoader.CoinIcon.Value, AssetsLoader.CoinIcon.Value.Box(new Vector2(entryX - 20, entryY)));
        }
    }
}
