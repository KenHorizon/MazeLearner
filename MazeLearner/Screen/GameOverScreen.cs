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
            this.EntryMenus.Add(new MenuEntry(0, Resources.Retry, new Rectangle(), () =>
            {
                Main.ActivePlayer.GoingSchoolCutscene = true;
                Main.ActivePlayer.InSchoolCutscene = false;
                Main.ActivePlayer.Puzzle01 = false;
                Main.ActivePlayer.FinishedMap0 = false;
                Main.GameState = GameState.Play;
                Main.ActivePlayer.SetPos(55, 30);
                Main.ActivePlayer.Direction = Direction.Left;
                Main.Tiled.LoadMap(World.Get("school"));
                PlayerEntity.SavePlayer(Main.ActivePlayer, Main.PlayerListPath[Main.PlayerListIndex]);
                GameSettings.SaveSettings();
                Main.ActivePlayer.Objective = Objective.Get(2);
                this.game.SetScreen(null);
            }, AssetsLoader.Box2.Value));
            this.EntryMenus.Add(new MenuEntry(1, Resources.MainMenu, new Rectangle(), () =>
            {
                this.game.SetScreen(new TitleScreen(TitleSequence.Title));
            }, AssetsLoader.Box2.Value));
        }

        public override void Render(SpriteBatch sprite, Graphic graphic)
        {
            base.Render(sprite, graphic);
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
