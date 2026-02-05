using MazeLeaner.Text;
using MazeLearner.GameContent.Animation;
using MazeLearner.GameContent.Entity;
using MazeLearner.GameContent.Entity.Monster;
using MazeLearner.Screen;
using MazeLearner.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MazeLearner
{
    public class GraphicRenderer
    {
        private int charIndex = 0;
        private string charText = "";
        private string dialogContent = "";
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
            this.RenderHeart(Main.SpriteBatch, Main.GetActivePlayer, 10, 10);
            if (Main.GameState == GameState.Dialog)
            {
                if (Main.GetActivePlayer != null)
                {
                    if (Main.GetActivePlayer.InteractedNpc != null && Main.GetActivePlayer.InteractedNpc is InteractableNPC interactable)
                    {
                        this.RenderDialogs(Main.SpriteBatch, Main.GetActivePlayer.InteractedNpc);
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
                TextManager.Text(Fonts.Small, $"X {this.GetTileCoord(Main.GetActivePlayer.Position).X} Y {this.GetTileCoord(Main.GetActivePlayer.Position).Y}", new Vector2(x, y));
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
            if (npc.Dialogs[npc.DialogIndex].IsEmpty() && npc is SubjectEntity entity)
            {
                if (entity.NpcType == NpcType.NonBattle)
                {
                    Main.GameState = GameState.Play;
                }

                if (entity.NpcType == NpcType.Battle)
                {
                    this.game.SetScreen(new BattleScreen(entity, Main.GetActivePlayer));
                    Main.GameState = GameState.Battle;
                }
                entity.DialogIndex = 0;
            }
            Rectangle dialogBox = new Rectangle(
                (int)(GameSettings.DialogBoxPadding / 2),
                this.game.GetScreenHeight() - (GameSettings.DialogBoxSize + GameSettings.DialogBoxY),
                this.game.GetScreenWidth() - GameSettings.DialogBoxPadding,
                GameSettings.DialogBoxSize
                );
            char[] dialogContents = npc.GetDialog().ToCharArray();

            if (this.charIndex < dialogContents.Length)
            {
                string dialogS = dialogContents[this.charIndex].ToString();
                this.charText = this.charText + dialogS;
                this.dialogContent = charText;
                this.charIndex++;
            }
            sprite.DrawMessageBox(AssetsLoader.MessageBox.Value, dialogBox, Color.White, 12);
            string nextDialog = $"Press {GameSettings.KeyInteract} to next";
            int nextX = (dialogBox.X + ((dialogBox.Width / 2) - GameSettings.DialogBoxPadding)) - nextDialog.Length;
            int nextY = dialogBox.Y + (dialogBox.Height + GameSettings.DialogBoxPadding);

            TextManager.Text(Fonts.Normal, nextDialog, new Vector2(nextX, nextY), Color.Black);
            TextManager.TextBox(Fonts.DT_L, this.dialogContent, dialogBox, new Vector2(GameSettings.DialogBoxPadding, 24), Color.Black);

            if (this.charIndex == dialogContents.Length && Main.Keyboard.Pressed(GameSettings.KeyInteract))
            {
                this.charIndex = 0;
                this.charText = "";
                if (Main.IsState(GameState.Dialog))
                {
                    npc.DialogIndex++;
                }
            }
        }

        public void RenderHeart(SpriteBatch sprite, NPC npc, int x, int y)
        {
            float health = npc.Health;
            float maxHealth = npc.MaxHealth;
            int x0 = x;
            int y0 = y;
            int x1 = x;
            int y1 = y;
            int row1 = 0;
            int row0 = 0;
            int col1 = 0;
            int col0 = 0;
            int maxRow = 10;
            Rectangle heartTextS = new Rectangle(x0, y0, AssetsLoader.HealthText.Value.Width / 2, AssetsLoader.HealthText.Value.Height / 2);
            sprite.Draw(AssetsLoader.HealthText.Value, heartTextS, Color.White);
            x0 += AssetsLoader.HealthText.Value.Width / 2;
            x1 += AssetsLoader.HealthText.Value.Width / 2;
            for (int i = 0; i < maxHealth; i++)
            {
                if (row0 == 0)
                {
                    Rectangle size0 = new Rectangle(x0, y0, AssetsLoader.HeartLeft.Value.Width, AssetsLoader.HeartLeft.Value.Height);
                    sprite.Draw(AssetsLoader.HeartLeft.Value, size0, Color.White);
                    x0 += AssetsLoader.HeartLeft.Value.Width;
                }
                else if (row0 == maxRow - 1)
                {
                    Rectangle size0 = new Rectangle(x0, y0, AssetsLoader.HeartRight.Value.Width, AssetsLoader.HeartRight.Value.Height);
                    sprite.Draw(AssetsLoader.HeartRight.Value, size0, Color.White);
                    x0 += AssetsLoader.HeartRight.Value.Width;
                }
                else
                {
                    Rectangle size0 = new Rectangle(x0, y0, AssetsLoader.HeartMiddle.Value.Width, AssetsLoader.HeartMiddle.Value.Height);
                    sprite.Draw(AssetsLoader.HeartMiddle.Value, size0, Color.White);
                    x0 += AssetsLoader.HeartMiddle.Value.Width;
                }
                row0++;
                if (row0 == maxRow)
                {
                    row0 = 0;
                    col0++;
                    x0 = x + AssetsLoader.HealthText.Value.Width / 2;
                    y0 += AssetsLoader.Health.Value.Height;
                }
            }
            for (int i = 0; i < health; i++)
            {
                if (row1 == 0)
                {
                    Rectangle size1 = new Rectangle(x1 + 4, y1 + 4, AssetsLoader.Heart.Value.Width, AssetsLoader.Heart.Value.Height);
                    sprite.Draw(AssetsLoader.Heart.Value, size1, Color.White);
                    x1 += AssetsLoader.HeartLeft.Value.Width;
                }
                else if (row1 == maxRow)
                {
                    Rectangle size1 = new Rectangle(x1, y1 + 4, AssetsLoader.Heart.Value.Width, AssetsLoader.Heart.Value.Height);
                    sprite.Draw(AssetsLoader.Heart.Value, size1, Color.White);
                    x1 += AssetsLoader.HeartRight.Value.Width;
                }
                else
                {
                    Rectangle size1 = new Rectangle(x1, y1 + 4, AssetsLoader.Heart.Value.Width, AssetsLoader.Heart.Value.Height);
                    sprite.Draw(AssetsLoader.Heart.Value, size1, Color.White);
                    x1 += AssetsLoader.HeartMiddle.Value.Width;
                }
                row1++;
                if (row1 == maxRow)
                {
                    row1 = 0;
                    col1++;
                    x1 = x + AssetsLoader.HealthText.Value.Width / 2;
                    y1 += AssetsLoader.Health.Value.Height;
                }
            }
        }
    }
}
