using MazeLeaner.Text;
using MazeLearner.GameContent.Animation;
using MazeLearner.GameContent.Entity;
using MazeLearner.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MazeLearner
{
    public class GraphicRenderer
    {
        private static Color DialogBackgroundColor = new Color(new Vector3(
            GameSettings.DialogBoxR,
            GameSettings.DialogBoxG,
            GameSettings.DialogBoxB));
        private Main game;
        public GraphicRenderer(Main game)
        {
            this.game = game;
        }
        public void Draw()
        {
            Main.DrawSprites();
            this.game.TilesetManager.Draw(Main.SpriteBatch);
            // For entity sprites sheet
            this.DrawNpcs();
            for (int i = 0; i < Main.AllEntity.Count; i++)
            {
                Main.AllEntity.RemoveAt(i);
            }
            Main.SpriteBatch.End();
            // UI in game
            // Need to be on above incase the will overlap between it.
            Main.DrawAlpha();
            this.RenderDebugs(Main.SpriteBatch);
            this.RenderHeart(Main.SpriteBatch);
            if (Main.GameState == GameState.Dialog)
            {
                if (this.game.ActivePlayer != null)
                {
                    if (this.game.ActivePlayer.InteractedNpc != null && this.game.ActivePlayer.InteractedNpc is InteractableNPC interactable)
                    {
                        this.RenderDialogs(Main.SpriteBatch, this.game.ActivePlayer.InteractedNpc);
                    }
                }
            }
            Main.SpriteBatch.End();
        }

        public void DrawNpcs()
        {
            foreach (var renderEntity in Main.AllEntity)
            {
                if (renderEntity != null)
                {
                    Sprite sprites = new Sprite(renderEntity.langName, renderEntity);
                    sprites.Draw(Main.SpriteBatch);
                }
            }
        }

        private void RenderDebugs(SpriteBatch sprite)
        {
            if (GameSettings.DebugScreen == true)
            {
                int x = 0;
                int y = 100;
                TextManager.Text(Fonts.Small, $"Game State: {Main.GameState}", new Vector2(x, y));
                y += 22;
                //TextManager.Text(Fonts.Small, $"Forward: {GameSettings.KeyForward}", new Vector2(x, y));
                //y += 22;
                //TextManager.Text(Fonts.Small, $"Downward: {GameSettings.KeyDownward}", new Vector2(x, y));
                //y += 22;
                //TextManager.Text(Fonts.Small, $"Left: {GameSettings.KeyLeft}", new Vector2(x, y));
                //y += 22;
                //TextManager.Text(Fonts.Small, $"Right: {GameSettings.KeyRight}", new Vector2(x, y));
                //y += 22;
                //TextManager.Text(Fonts.Small, $"Interact: {GameSettings.KeyInteract}", new Vector2(x, y));
                //y += 22;
                //TextManager.Text(Fonts.Small, $"Cancel: {GameSettings.KeyBack}", new Vector2(x, y));
                //y += 22;
                //TextManager.Text(Fonts.Small, $"Run: {GameSettings.KeyRunning}", new Vector2(x, y));
                //y += 22;
                //TextManager.Text(Fonts.Small, $"Inventory: {GameSettings.KeyOpenInventory}", new Vector2(x, y));
                //y += 22;
            }

        }
        private void RenderDialogs(SpriteBatch sprite, NPC npc)
        {
            Rectangle dialogBox = new Rectangle((int)(GameSettings.DialogBoxPadding / 2), this.game.GetScreenHeight() - (GameSettings.DialogBoxSize + GameSettings.DialogBoxY), this.game.GetScreenWidth() - GameSettings.DialogBoxPadding, GameSettings.DialogBoxSize);
            sprite.DrawMessageBox(AssetsLoader.MessageBox.Value, dialogBox, Color.White, 12);
            string nextDialog = $"Press {GameSettings.KeyInteract} to next";
            int nextX = (dialogBox.X + ((dialogBox.Width / 2) - GameSettings.DialogBoxPadding)) - nextDialog.Length;
            int nextY = dialogBox.Y + (dialogBox.Height + GameSettings.DialogBoxPadding);

            TextManager.Text(Fonts.Normal, nextDialog, new Vector2(nextX, nextY), Color.Black);
            TextManager.TextBox(Fonts.DT_L, npc.GetDialog(), dialogBox, new Vector2(GameSettings.DialogBoxPadding, 24), Color.Black);
        }

        public void RenderHeart(SpriteBatch sprite)
        {
            float health = this.game.ActivePlayer.Health;
            // Image Position and Size 
            int x = 10;
            int y = 10;
            Rectangle heartTextS = new Rectangle(x, y - 42, AssetsLoader.HealthText.Value.Width / 2, AssetsLoader.HealthText.Value.Height / 2);
            sprite.Draw(AssetsLoader.HealthText.Value, heartTextS, Color.White);
            x += AssetsLoader.Health.Value.Width + 100;
            for (int i = 0; i < health; i++)
            {
                Rectangle size = new Rectangle(x, y, AssetsLoader.Health.Value.Width, AssetsLoader.Health.Value.Height);
                sprite.Draw(AssetsLoader.Health.Value, size, Color.White);
                x += AssetsLoader.Health.Value.Width;
            }
        }
    }
}
