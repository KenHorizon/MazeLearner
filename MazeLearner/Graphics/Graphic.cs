using MazeLeaner.Text;
using MazeLearner.GameContent.Entity;
using MazeLearner.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MazeLearner.Graphics
{
    public class Graphic
    {
        private int charIndex = 0;
        private string charText = "";
        private string dialogContent = "";
        private bool dialogSkipped;
        private Main game;
        public Graphic(Main game)
        {
            this.game = game;
        }
        public void Draw()
        {
            Main.DrawScreen();
            Main.Tiled.Draw(Main.SpriteBatch);

            for (int i = 0; i < Main.Particles[Main.MapIds].Length; i++)
            {
                if (Main.Particles[Main.MapIds][i] != null)
                {
                    Main.Particles[Main.MapIds][i].Draw(Main.SpriteBatch);
                }
            }
            // For entity sprites sheet
            for (int i = 0; i < Main.AllEntity.Count; i++)
            {
                Main.AllEntity.RemoveAt(i);
            }
            Main.SpriteBatch.End();
            
            // UI in game
            // Need to be on above incase the will overlap between it.
            Main.DrawUIs();
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
                int x = 20;
                int y = 100;
                int padding = 32;
                Texts.DrawString($"Game State: {Main.GameState}", new Vector2(x, y), Color.White);
                y += padding;
                Texts.DrawString($"X: {(int)Main.GetActivePlayer.TilePosition.X} Y: {(int)Main.GetActivePlayer.TilePosition.Y}", new Vector2(x, y), Color.White);
                y += padding;
                Texts.DrawString($"Facing {Main.GetActivePlayer.Facing.ToString()} ID: {(int)Main.GetActivePlayer.Facing}", new Vector2(x, y), Color.White);
                y += padding;
            }

        }
        public void RenderKeybindInstruction(SpriteBatch sprite)
        {
            int x = 60;
            int y = 32;
            int padding = 32;
            int paddingText = 21 + padding;
            sprite.Draw(AssetsLoader.InstructionBox.Value, Main.WindowScreen);
            Texts.DrawString($"Instructions", new Vector2(x, y));
            y += padding + 78;
            Texts.DrawString($"Forward:", new Vector2(x, y));
            Texts.DrawString($"{GameSettings.KeyForward}", new Vector2(this.game.GetScreenWidth() / 2, y));
            y += paddingText;
            Texts.DrawString($"Downward:", new Vector2(x, y));
            Texts.DrawString($"{GameSettings.KeyDownward}", new Vector2(this.game.GetScreenWidth() / 2, y));
            y += paddingText;
            Texts.DrawString($"Left", new Vector2(x, y));
            Texts.DrawString($"{GameSettings.KeyLeft}", new Vector2(this.game.GetScreenWidth() / 2, y));
            y += paddingText;
            Texts.DrawString($"Right:", new Vector2(x, y));
            Texts.DrawString($"{GameSettings.KeyRight}", new Vector2(this.game.GetScreenWidth() / 2, y));
            y += paddingText;
            Texts.DrawString($"Interact/Confirm:", new Vector2(x, y));
            Texts.DrawString($"{GameSettings.KeyInteract}/{GameSettings.KeyConfirm}", new Vector2(this.game.GetScreenWidth() / 2, y));
            y += paddingText;
            Texts.DrawString($"Back/Cancel:", new Vector2(x, y));
            Texts.DrawString($"{GameSettings.KeyBack}", new Vector2(this.game.GetScreenWidth() / 2, y));
            y += paddingText;
            Texts.DrawString($"Run:", new Vector2(x, y));
            Texts.DrawString($"{GameSettings.KeyRunning}", new Vector2(this.game.GetScreenWidth() / 2, y));
            y += paddingText;
            Texts.DrawString($"Inventory:", new Vector2(x, y));
            Texts.DrawString($"{GameSettings.KeyOpenInventory0}/{GameSettings.KeyOpenInventory1}", new Vector2(this.game.GetScreenWidth() / 2, y));
            y = this.game.GetScreenHeight() - 32;
            Texts.DrawString($"Press: {GameSettings.KeyInteract} to continue", new Vector2(x, y));
        }
        private Vector2 GetTileCoord(Vector2 worldPos)
        {
            return new Vector2((int) (worldPos.X / 32), (int)(worldPos.Y / 32));
        }
        private void RenderDialogs(SpriteBatch sprite, NPC npc)
        {
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
            this.RenderDialogMessage(sprite, dialogBox);
            if ((Main.Input.Pressed(GameSettings.KeyInteract) || Main.Input.Pressed(GameSettings.KeyConfirm)))
            {
                if (this.charIndex == dialogContents.Length)
                {
                    this.charIndex = 0;
                    this.charText = "";
                    if (Main.IsState(GameState.Dialog))
                    {
                        npc.DialogIndex++;
                    }
                } else
                {
                    this.dialogContent = npc.GetDialog();
                    this.charIndex = dialogContents.Length;
                }
            }
        }
        private void RenderDialogs(SpriteBatch sprite, string message)
        {
            Rectangle dialogBox = new Rectangle(
                (int)(GameSettings.DialogBoxPadding / 2),
                this.game.GetScreenHeight() - (GameSettings.DialogBoxSize + GameSettings.DialogBoxY),
                this.game.GetScreenWidth() - GameSettings.DialogBoxPadding,
                GameSettings.DialogBoxSize
                );
            char[] dialogContents = message.ToCharArray();
            if (this.charIndex < dialogContents.Length)
            {
                string dialogS = dialogContents[this.charIndex].ToString();
                this.charText = this.charText + dialogS;
                this.dialogContent = charText;
                this.charIndex++;
            }
            RenderDialogMessage(sprite, dialogBox);
        }
        public void RenderDialogMessage(SpriteBatch sprite, Rectangle dialogBox)
        {
            sprite.NinePatch(AssetsLoader.MessageBox.Value, dialogBox, Color.White, 12);
            string nextDialog = $"Press {GameSettings.KeyInteract} to next";
            int nextX = (dialogBox.X + ((dialogBox.Width / 2) - GameSettings.DialogBoxPadding)) - nextDialog.Length;
            int nextY = dialogBox.Y + (dialogBox.Height + GameSettings.DialogBoxPadding);

            Texts.DrawString(nextDialog, new Vector2(nextX, nextY), Color.Black);
            Texts.DrawStringBox(Fonts.InputBoxText, this.dialogContent, dialogBox, new Vector2(GameSettings.DialogBoxPadding, 24), Color.Black);

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
