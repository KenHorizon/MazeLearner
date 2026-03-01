using MazeLeaner.Text;
using MazeLearner.GameContent.Entity;
using MazeLearner.GameContent.Entity.Player;
using MazeLearner.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Assimp.Metadata;
using static System.Net.Mime.MediaTypeNames;

namespace MazeLearner.Graphics
{
    public class Graphic
    {
        private int charIndex = 0;
        private string charText = "";
        private string dialogContent = "";
        private bool dialogSkipped = true;
        private int dialogSkippedTimer = 0;
        public BaseEntity entity;
        private Main game;
        public Graphic(Main game)
        {
            this.game = game;
        }
        public void Draw()
        {
            Main.Tiled.Draw(Main.SpriteBatch);
            
            // For entity sprites sheet
            for (int i = 0; i < Main.AllEntity.Count; i++)
            {
                Main.AllEntity.RemoveAt(i);
            }
        }

        public void DrawGameUIs()
        {
            // UI in game
            // Need to be on above incase the will overlap between it.
            this.RenderDebugs(Main.SpriteBatch);
            this.RenderPlayerUI(Main.SpriteBatch);
            if (Main.GameState == GameState.Dialog)
            {
                if (Main.TextDialog.IsEmpty() == false)
                {
                    this.RenderDialogs(Main.SpriteBatch);
                }
            }
        }

        private void RenderPlayerUI(SpriteBatch sprite)
        {
            int padding = 32;
            int x = 10;
            int y = 10;

            string playerNameAndScore = $"{Main.GetActivePlayer.DisplayName} - Score:{Main.GetActivePlayer.ScorePoints}";
            Vector2 outputKeybinds = Texts.MeasureString(Fonts.Text, playerNameAndScore);
            Vector2 outputKPos = new Vector2(x, y);
            Rectangle outputBox = new Rectangle((int)outputKPos.X - 20, (int)outputKPos.Y, (int)outputKeybinds.X + 20, (int)outputKeybinds.Y);
            sprite.NinePatch(AssetsLoader.Box1.Value, outputBox, Color.White, 32);
            Texts.Text(Fonts.Text, playerNameAndScore, outputKPos, Color.White);
            y += padding;
            this.RenderHeart(sprite, Main.GetActivePlayer, x, y);
        }

        public void OverlayKeybinds(SpriteBatch sprite)
        {
            int keybindsTextPadding = 20;
            string textKeybinds = $"Next: {GameSettings.KeyForward} | Back: {GameSettings.KeyDownward} | Confirm: {GameSettings.KeyInteract} | Cancel: {GameSettings.KeyBack}";
            Vector2 outputKeybinds = Texts.MeasureString(Fonts.Text, textKeybinds);
            Vector2 outputKPos = new Vector2(0 + keybindsTextPadding, Main.WindowScreen.Height - (outputKeybinds.Y + 20) + 2);
            Rectangle outputBox = new Rectangle((int)outputKPos.X - 20, (int)outputKPos.Y, (int)outputKeybinds.X, (int)outputKeybinds.Y);
            sprite.NinePatch(AssetsLoader.Box1.Value, outputBox, Color.White, 32);
            Texts.Text(Fonts.Text, textKeybinds, outputKPos, Color.White);
        }
        private void RenderDebugs(SpriteBatch sprite)
        {
            if (GameSettings.DebugScreen == true)
            {
                int x = 20;
                int y = 120;
                int padding = 32;
                Texts.DrawString($"Game State: {Main.GameState}", new Vector2(x, y), Color.White);
                y += padding;
                Texts.DrawString($"Facing {Main.GetActivePlayer.Direction.ToString()} Target {Main.GetActivePlayer.TargetDirection.ToString()} ID: {(int)Main.GetActivePlayer.Direction}", new Vector2(x, y), Color.White);
                y += padding;
                Texts.DrawString($"X {Main.GetActivePlayer.Position.ToPoint().X} Y {Main.GetActivePlayer.Position.ToPoint().Y}", new Vector2(x, y), Color.White);
                y += padding;
                Texts.DrawString($"Row {Main.GetActivePlayer.InteractionBox.X / Main.TileSize} Col {Main.GetActivePlayer.InteractionBox.Y / Main.TileSize}", new Vector2(x, y), Color.White);
                y += padding;
                Texts.DrawString($"Movement State: {Main.GetActivePlayer.MovementState}", new Vector2(x, y), Color.White);
                y += padding;
                Texts.DrawString($"Interacted Object Id: {Main.GetActivePlayer.collisionBox.CheckObject(Main.GetActivePlayer, true)}", new Vector2(x, y), Color.White);
                y += padding;
                Texts.DrawString($"Interacted Entity Id: {Main.GetActivePlayer.collisionBox.CheckNpc(Main.GetActivePlayer, true)}", new Vector2(x, y), Color.White);
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
            Texts.DrawString($"{GameSettings.KeyForward}", new Vector2(Main.WindowScreen.Width / 2, y));
            y += paddingText;
            Texts.DrawString($"Downward:", new Vector2(x, y));
            Texts.DrawString($"{GameSettings.KeyDownward}", new Vector2(Main.WindowScreen.Width / 2, y));
            y += paddingText;
            Texts.DrawString($"Left", new Vector2(x, y));
            Texts.DrawString($"{GameSettings.KeyLeft}", new Vector2(Main.WindowScreen.Width / 2, y));
            y += paddingText;
            Texts.DrawString($"Right:", new Vector2(x, y));
            Texts.DrawString($"{GameSettings.KeyRight}", new Vector2(Main.WindowScreen.Width / 2, y));
            y += paddingText;
            Texts.DrawString($"Interact/Confirm:", new Vector2(x, y));
            Texts.DrawString($"{GameSettings.KeyInteract}/{GameSettings.KeyConfirm}", new Vector2(Main.WindowScreen.Width / 2, y));
            y += paddingText;
            Texts.DrawString($"Back/Cancel:", new Vector2(x, y));
            Texts.DrawString($"{GameSettings.KeyBack}", new Vector2(Main.WindowScreen.Width / 2, y));
            y += paddingText;
            Texts.DrawString($"Run:", new Vector2(x, y));
            Texts.DrawString($"{GameSettings.KeyRunning}", new Vector2(Main.WindowScreen.Width / 2, y));
            y += paddingText;
            Texts.DrawString($"Inventory:", new Vector2(x, y));
            Texts.DrawString($"{GameSettings.KeyOpenInventory0}/{GameSettings.KeyOpenInventory1}", new Vector2(Main.WindowScreen.Width / 2, y));
            y = Main.WindowScreen.Height - 32;
            Texts.DrawString($"Press: {GameSettings.KeyInteract} to continue", new Vector2(x, y));
        }
        private void RenderDialogs(SpriteBatch sprite)
        {
            if (this.dialogSkipped == true)
            {
                this.dialogSkippedTimer++;
                if (this.dialogSkippedTimer > 100)
                {
                    this.dialogSkippedTimer = 0;
                    this.dialogSkipped = false;
                }
            }
            Rectangle dialogBox = new Rectangle(
                (int)(GameSettings.DialogBoxPadding / 2),
                Main.WindowScreen.Height - (GameSettings.DialogBoxSize + GameSettings.DialogBoxY),
                Main.WindowScreen.Width - GameSettings.DialogBoxPadding,
                GameSettings.DialogBoxSize
                );
            string var001 = Utils.EncodeAsDialog(Main.TextDialog).name;
            string var002 = Utils.EncodeAsDialog(Main.TextDialog).text;
            char[] dialogContents = var002.ToCharArray();

            if (var001.IsEmpty() == false)
            {
                Vector2 inptNameSize = Texts.MeasureString(Fonts.Dialog, var001);
                Rectangle dialogNameBox = new Rectangle(
                    20,
                    (int)(dialogBox.Y - inptNameSize.Y) - 24,
                    (int)inptNameSize.X + 120,
                    (int)inptNameSize.Y + 24
                    );
                sprite.NinePatch(AssetsLoader.Box4.Value, dialogNameBox, Color.White);
                Texts.DrawString(Fonts.Dialog, var001, dialogBox.Vec2(20, -(int)(inptNameSize.Y + 14)));
            }
            if (this.charIndex < dialogContents.Length)
            {
                this.dialogSkipped = false;
                string dialogS = dialogContents[this.charIndex].ToString();
                this.charText = this.charText + dialogS;
                this.dialogContent = charText;
                this.charIndex++;
            }
            this.RenderDialogMessage(sprite, dialogBox, AssetsLoader.MessageBox.Value);
            if (Main.Input.Pressed(GameSettings.KeyInteract) && this.dialogSkipped == false)
            {
                this.charIndex = 0;
                this.charText = "";
                Main.TextDialogueIndex++;
            }
        }
        public void RenderDialogs(SpriteBatch sprite, string message, Texture2D texture, bool textByText)
        {
            Rectangle dialogBox = new Rectangle(
                (int)(GameSettings.DialogBoxPadding / 2),
                Main.WindowScreen.Height - (GameSettings.DialogBoxSize + GameSettings.DialogBoxY),
                Main.WindowScreen.Width - GameSettings.DialogBoxPadding,
                GameSettings.DialogBoxSize
                );
            if (textByText == true)
            {
                char[] dialogContents = message.ToCharArray();
                if (this.charIndex < dialogContents.Length)
                {
                    string dialogS = dialogContents[this.charIndex].ToString();
                    this.charText = this.charText + dialogS;
                    this.dialogContent = charText;
                    this.charIndex++;
                }
            }
            RenderDialogMessage(sprite, dialogBox, AssetsLoader.MessageBox.Value);
        }
        public void RenderTransparentDialogs(SpriteBatch sprite, string message, bool textByText = false)
        {
            this.RenderDialogs(sprite, message, null, textByText);
        }
        private void RenderDialogMessage(SpriteBatch sprite, Rectangle dialogBox, Texture2D texture)
        {
            if (texture != null)
            {
                sprite.NinePatch(texture, dialogBox, Color.White, 12);
            }
            string nextDialog = $"Press {GameSettings.KeyInteract} to next";
            int nextX = dialogBox.X + GameSettings.DialogBoxPadding;
            int nextY = dialogBox.Bottom - (GameSettings.DialogBoxPadding + 12);
            Texts.DrawStringBox(Fonts.Dialog, this.dialogContent, dialogBox, new Vector2(GameSettings.DialogBoxPadding, 24), Color.Black);
            Texts.DrawString(nextDialog, new Vector2(nextX, nextY), Color.Black);
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
