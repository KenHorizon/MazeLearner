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
        private MenuEntry invEntry;
        private MenuEntry settingsEntry;
        private MenuEntry saveEntry;
        private MenuEntry exitEntry;
        public BagScreen() : base("")
        {
        }
        public override void LoadContent()
        {
            Loggers.Info($"{Main.PlayerListIndex}");
            base.LoadContent();
            int entryMenuSize = AssetsLoader.BagMenu.Value.Width;
            int entryH = AssetsLoader.BagMenu.Value.Height;
            int entryX = (Main.WindowScreen.Width - entryMenuSize) / 2;
            int entryY = 180;
            int ButtonPadding = AssetsLoader.BagMenu.Value.Height + 12;
            this.invEntry = new MenuEntry(0, Resources.Inventory, new Rectangle(entryX, entryY, entryMenuSize, entryH), () =>
            {
                this.game.SetScreen(new InventoryScreen(Main.GetActivePlayer));
            }, AssetsLoader.BagMenu.Value);
            this.invEntry.TextColor = Color.White;
            entryY += ButtonPadding; 
            this.settingsEntry = new MenuEntry(1, Resources.Settings, new Rectangle(entryX, entryY, entryMenuSize, entryH), () =>
            {
                this.game.SetScreen(new OptionScreen(true));
            }, AssetsLoader.BagMenu.Value);
            this.settingsEntry.TextColor = Color.White;
            entryY += ButtonPadding;
            this.saveEntry = new MenuEntry(2, Resources.Save, new Rectangle(entryX, entryY, entryMenuSize, entryH), () =>
            {
                PlayerEntity.SavePlayer(Main.GetActivePlayer, Main.PlayerListPath[Main.PlayerListIndex]);
            }, AssetsLoader.BagMenu.Value);
            this.saveEntry.TextColor = Color.White;
            entryY += ButtonPadding;
            this.exitEntry = new MenuEntry(3, Resources.ExitToMenu, new Rectangle(entryX, entryX, entryMenuSize, entryH), () =>
            {
                Main.GameState = GameState.Title;
                Main.LoadPlayers();
                this.game.SetScreen(new TitleScreen(TitleSequence.Title));
                Main.GetActivePlayer = null;
            }, AssetsLoader.BagMenu.Value);
            this.exitEntry.TextColor = Color.White;
            this.EntryMenus.Add(this.invEntry);
            this.EntryMenus.Add(this.settingsEntry);
            this.EntryMenus.Add(this.saveEntry);
            this.EntryMenus.Add(this.exitEntry);
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
            int entryX = (Main.WindowScreen.Width - entryMenuSize) / 2;
            int entryY = 180;
            int textPadding = 32;
            int ButtonPadding = AssetsLoader.BagMenu.Value.Height + 12;
            if (this.IndexBtn == 0)
            {
                string text = $"Inventory store items such as heal, weapons ang key items";
                Texts.DrawStringBox(text, new Rectangle(entryX + entryMenuSize + 12, entryY, 200, 120), Color.White);
            }

            if (this.IndexBtn == 1)
            {
                string text = $"Settings where all configuration options such as keybinds, resolution and volume";
                Texts.DrawStringBox(text, new Rectangle(entryX + entryMenuSize + 12, entryY, 200, 120), Color.White);
            }

            if (this.IndexBtn == 2)
            {
                string text = $"Save the game progress";
                Texts.DrawStringBox(text, new Rectangle(entryX + entryMenuSize + 12, entryY, 200, 120), Color.White);
            }
            if (this.IndexBtn == 3)
            {
                string text = $"Return to title Note: Your game will not be saved when exit it, please save your progress before exiting the game";
                Texts.DrawStringBox(text, new Rectangle(entryX + entryMenuSize + 12, entryY, 240, 120), Color.White);
            }
            entryY -= ButtonPadding;
            Vector2 MMSize = Texts.MeasureString(Fonts.Text, Resources.MainMenu);
            Texts.DrawString(Resources.MainMenu, new Vector2((Main.WindowScreen.Width - MMSize.X) / 2, entryY), Color.White);
            entryY -= ButtonPadding;
            Texture2D maleOrFemale = Main.GetActivePlayer.Gender == Gender.Male ? AssetsLoader.PlayerM.Value : AssetsLoader.PlayerF.Value;
            sprite.Draw(AssetsLoader.PortfolioBox.Value, AssetsLoader.PortfolioBox.Value.Box(new Vector2(
                40 + 22, entryY + 26),
                1.5F));
            sprite.Draw(maleOrFemale, maleOrFemale.Box(new Vector2(40, entryY)));
            entryY += (ButtonPadding + (maleOrFemale.Height / 2)) + textPadding + 12;
            Texts.DrawString($"Name: {Main.GetActivePlayer.DisplayName}", new Vector2(62, entryY), Color.White);
            entryY += textPadding;
            Texts.DrawString($"HP: {Main.GetActivePlayer.Health}/{Main.GetActivePlayer.MaxHealth}", new Vector2(62, entryY), Color.White);
            entryY += textPadding;
            Texts.DrawString($"Score: {Main.GetActivePlayer.ScorePoints}", new Vector2(62, entryY), Color.White);
            entryY += textPadding;
            Texts.DrawString($"Money: {Main.GetActivePlayer.Coin}", new Vector2(62, entryY), Color.White);
            //
        }
        public override void RenderBackground(SpriteBatch sprite, Graphic graphic)
        {
            base.RenderBackground(sprite, graphic);
            this.FadeBlackScreen(sprite, 0.85F);
            sprite.Draw(AssetsLoader.BagBackground.Value, Main.WindowScreen);
        }
    }
}
