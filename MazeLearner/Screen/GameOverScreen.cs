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
            int boxW = 320;
            int boxH = 64;
            int x = (Main.WindowScreen.Width - boxW) / 2;
            int y = Main.WindowScreen.Height / 2 - 20;
            Rectangle box0 = new Rectangle(x, y, boxW, boxH);
            Rectangle box1 = new Rectangle(x, y + AssetsLoader.FemalePickBox.Value.Height + 20, boxW, boxH);
            this.EntryMenus.Add(new MenuEntry(0, Resources.NewGame, box0, () =>
            {
                Main.ActivePlayer.ResetState();
                Main.GameState = GameState.Play;
                Main.ActivePlayer.SetPos(70, 12);
                Main.ActivePlayer.Direction = Direction.Down;
                Main.Tiled.LoadMap(World.Get("interior"));
                PlayerEntity.SavePlayer(Main.ActivePlayer, Main.PlayerListPath[Main.PlayerListIndex]);
                GameSettings.SaveSettings();
                Main.ActivePlayer.Objective = Objective.Get(2);
                this.game.SetScreen(null);
            }, AssetsLoader.Box2.Value));
            this.EntryMenus.Add(new MenuEntry(1, Resources.MainMenu, box1, () =>
            {
                this.game.SetScreen(new TitleScreen(TitleSequence.Title));
            }, AssetsLoader.Box2.Value));
        }

        public override void Render(SpriteBatch sprite, Graphic graphic)
        {
            base.Render(sprite, graphic);
            sprite.Screen(Color.Black);
        }
        public override void RenderBackground(SpriteBatch sprite, Graphic graphic)
        {
            base.RenderBackground(sprite, graphic);
        }

        public override bool ShowOverlayKeybinds()
        {
            return base.ShowOverlayKeybinds();
        }
    }
}
