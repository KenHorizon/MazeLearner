using MazeLearner.GameContent;
using MazeLearner.GameContent.Entity.Player;
using MazeLearner.Graphics;
using MazeLearner.Graphics.Asset;
using MazeLearner.Localization;
using MazeLearner.Screen.Components;
using MazeLearner.Worlds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MazeLearner.Screen
{
    public class GameOverScreen : BaseScreen
    {
        public GameOverScreen() : base("")
        {
        }
        public override void LoadContent()
        {
            base.LoadContent();
            int boxW = 240;
            int boxH = 60;
            int x = (Main.WindowScreen.Width - boxW) / 2;
            int y = Main.WindowScreen.Height / 2 + 60;
            Rectangle box0 = new Rectangle(x, y, boxW, boxH);
            Rectangle box1 = new Rectangle(x, y + boxH + 12, boxW, boxH);
            this.EntryMenus.Add(new MenuEntry(0, Resources.NewGame, box0, () =>
            {
                Main.PlayerIsDead = false;
                Main.ActivePlayer.ResetState();
                Main.GameState = GameState.Play;
                Main.ActivePlayer.SetPos(70, 12);
                Main.ActivePlayer.Direction = Direction.Down;
                Main.Tiled.LoadMap(World.Get("interior"));
                PlayerEntity.SavePlayer(Main.ActivePlayer, Main.PlayerListPath[Main.PlayerListIndex]);
                GameSettings.SaveSettings();
                this.game.SetScreen(null);
            }, AssetsLoader.MenuBtn0.Value, AnchorMainEntry.Center));
            this.EntryMenus.Add(new MenuEntry(1, Resources.MainMenu, box1, () =>
            {
                this.game.SetScreen(new TitleScreen(TitleSequence.Title));
            }, AssetsLoader.MenuBtn0.Value, AnchorMainEntry.Center));
        }

        public override void Render(SpriteBatch sprite, Graphic graphic)
        {
            base.Render(sprite, graphic);
            sprite.Draw(AssetsLoader.GameOver.Value, Main.WindowScreen);
        }
        public override void RenderBackground(SpriteBatch sprite, Graphic graphic)
        {
            base.RenderBackground(sprite, graphic);
            sprite.Screen(Color.Black);
        }

        public override bool ShowOverlayKeybinds()
        {
            return base.ShowOverlayKeybinds();
        }
    }
}
