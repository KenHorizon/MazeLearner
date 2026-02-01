using MazeLeaner.Text;
using MazeLearner.GameContent.Animation;
using MazeLearner.GameContent.Entity;
using MazeLearner.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Runtime.CompilerServices;

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

        

        private void RenderDebugs(SpriteBatch sprite)
        {
            if (GameSettings.DebugScreen == true)
            {
                int x = 0;
                int y = 100;
                TextManager.Text(Fonts.Small, $"Game State: {Main.GameState}", new Vector2(x, y));
                y += 22;
                TextManager.Text(Fonts.Small, $"X {this.GetTileCoord(this.game.ActivePlayer.Position).X} Y {this.GetTileCoord(this.game.ActivePlayer.Position).Y}", new Vector2(x, y));
                y += 22;
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
        private Vector2 GetTileCoord(Vector2 worldPos)
        {
            return new Vector2((int)(worldPos.X / 32), (int)(worldPos.Y / 32));
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
            int x = 10;
            int y = 10;
            int row = 0;
            int col = 0;
            int maxRow = 10;
            Rectangle heartTextS = new Rectangle(x, y, AssetsLoader.HealthText.Value.Width / 2, AssetsLoader.HealthText.Value.Height / 2);
            sprite.Draw(AssetsLoader.HealthText.Value, heartTextS, Color.White);
            x += AssetsLoader.HealthText.Value.Width / 2;
            for (int i = 0; i < health; i++)
            {
                if (i == 0)
                {
                    Rectangle size0 = new Rectangle(x, y, AssetsLoader.HeartLeft.Value.Width, AssetsLoader.HeartLeft.Value.Height);
                    Rectangle size1 = new Rectangle(x + 4, y + 4, AssetsLoader.Heart.Value.Width, AssetsLoader.Heart.Value.Height);
                    sprite.Draw(AssetsLoader.HeartLeft.Value, size0, Color.White);
                    sprite.Draw(AssetsLoader.Heart.Value, size1, Color.White);
                    x += AssetsLoader.HeartLeft.Value.Width;
                }
                else if (i == (health - 1))
                {
                    Rectangle size0 = new Rectangle(x, y, AssetsLoader.HeartRight.Value.Width, AssetsLoader.HeartRight.Value.Height);
                    Rectangle size1 = new Rectangle(x, y + 4, AssetsLoader.Heart.Value.Width, AssetsLoader.Heart.Value.Height);
                    sprite.Draw(AssetsLoader.HeartRight.Value, size0, Color.White);
                    sprite.Draw(AssetsLoader.Heart.Value, size1, Color.White);
                    x += AssetsLoader.HeartRight.Value.Width;
                }
                else
                {
                    Rectangle size0 = new Rectangle(x, y, AssetsLoader.HeartMiddle.Value.Width, AssetsLoader.HeartMiddle.Value.Height);
                    Rectangle size1 = new Rectangle(x, y + 4, AssetsLoader.Heart.Value.Width, AssetsLoader.Heart.Value.Height);
                    sprite.Draw(AssetsLoader.HeartMiddle.Value, size0, Color.White);
                    sprite.Draw(AssetsLoader.Heart.Value, size1, Color.White);
                    x += AssetsLoader.HeartMiddle.Value.Width;
                }
                row++;
                if (row == maxRow)
                {
                    row = 0;
                    col++;
                    x = 0;
                    y += AssetsLoader.Health.Value.Height;
                }
            }
        }
    }
}
