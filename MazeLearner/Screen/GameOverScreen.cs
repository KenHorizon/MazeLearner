using MazeLearner.Localization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MazeLearner.Screen.Components;

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
                Main.GetActivePlayer.SetPos(29, 31);
                this.game.SetScreen(null);
            }, AssetsLoader.Box2.Value));
            this.EntryMenus.Add(new MenuEntry(1, Resources.MainMenu, new Rectangle(), () =>
            {
                this.game.SetScreen(new TitleScreen(TitleSequence.Title));
            }, AssetsLoader.Box2.Value));
        }

        public override void Render(SpriteBatch sprite, GraphicRenderer graphic)
        {
            base.Render(sprite, graphic);
        }
        public override void RenderBackground(SpriteBatch sprite, GraphicRenderer graphic)
        {
            base.RenderBackground(sprite, graphic);
        }


        public override bool ShowOverlayKeybinds()
        {
            return base.ShowOverlayKeybinds();
        }
    }
}
