using MazeLeaner.Text;
using MazeLearner.GameContent.Animation;
using MazeLearner.GameContent.Entity;
using MazeLearner.GameContent.Entity.Player;
using MazeLearner.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Runtime.Serialization;

namespace MazeLearner
{
    public class GraphicRenderer
    {
        private static Color DialogBackgroundColor = new Color(new Vector3(
            GameSettings.DialogBoxR,
            GameSettings.DialogBoxG,
            GameSettings.DialogBoxB));
        private Assets<Texture2D> HealthIcon;
        private Assets<Texture2D> MB = Assets<Texture2D>.Request("UI/MessageBox");
        private Main game;
        public GraphicRenderer(Main game)
        {
            this.game = game;
        }
        public void Load()
        {
            this.HealthIcon = Assets<Texture2D>.Request("UI/Entity/Health");
        }
        public void Draw()
        {
            Main.DrawSprites();
            //int col = 0;
            //int row = 0;
            //int y = 0;
            //int x = 0;
            //while (col < Main.MaxScreenCol && row < Main.MaxScreenRow)
            //{
            //    Rectangle rect = new Rectangle(x, y, Main.MaxTileSize, Main.MaxTileSize);
            //    Main.SpriteBatch.DrawRectangle(rect, Color.White);
            //    col++;
            //    x += Main.MaxTileSize;
            //    if (col == Main.MaxScreenCol)
            //    {
            //        col = 0;
            //        x = 0;
            //        row++;
            //        y += Main.MaxTileSize;
            //    }
            //}
            // For entity sprites sheet
            foreach (var renderEntity in Main.AllEntity)
            {
                if (renderEntity != null)
                {
                    this.RenderNpcs(renderEntity);
                }
            }
            for (int i = 0; i < Main.AllEntity.Count; i++)
            {
                Main.AllEntity.RemoveAt(i);
            }
            Main.SpriteBatch.End();
            // UI in game
            // Need to be on above incase the will overlap between it.
            Main.DrawAlpha();
            //this.RenderOverlayKeyBinding(Main.SpriteBatch);
            this.RenderHeart(Main.SpriteBatch);
            if (Main.GameState == GameState.Pause)
            {
                if (this.game.ActivePlayer != null)
                {
                    for (int i = 0; i < Main.NPCS.Length; i++)
                    {
                        if (this.game.ActivePlayer.objectIndexs == i)
                        {
                            this.RenderDialogs(Main.SpriteBatch, Main.NPCS[i]);
                        }
                    }
                }
                //foreach (NPC npc in Main.NPCS)
                //{
                //    if (npc != null && npc.RenderDialogs())
                //    {
                //        this.RenderDialogs(Main.SpriteBatch, npc);
                //    }
                //}
                //foreach (ItemEntity item in Main.Items)
                //{
                //    if (item != null && item.RenderDialogs())
                //    {
                //        this.RenderDialogs(Main.SpriteBatch, item);
                //    }
                //}
            }
            Main.SpriteBatch.End();
        }
        private void RenderOverlayKeyBinding(SpriteBatch sprite)
        {
            int x = 0;
            int y = 100;
            TextManager.Text(Fonts.Small, "Keybinds:", new Vector2(x, y));
            y += 22;
            TextManager.Text(Fonts.Small, $"Forward: {GameSettings.KeyForward}", new Vector2(x, y));
            y += 22;
            TextManager.Text(Fonts.Small, $"Downward: {GameSettings.KeyDownward}", new Vector2(x, y));
            y += 22;
            TextManager.Text(Fonts.Small, $"Left: {GameSettings.KeyLeft}", new Vector2(x, y));
            y += 22;
            TextManager.Text(Fonts.Small, $"Right: {GameSettings.KeyRight}", new Vector2(x, y));
            y += 22;
            TextManager.Text(Fonts.Small, $"Interact: {GameSettings.KeyInteract}", new Vector2(x, y));
            y += 22;
            TextManager.Text(Fonts.Small, $"Cancel: {GameSettings.KeyBack}", new Vector2(x, y));
            y += 22;
            TextManager.Text(Fonts.Small, $"Run: {GameSettings.KeyRunning}", new Vector2(x, y));
            y += 22;
            TextManager.Text(Fonts.Small, $"Inventory: {GameSettings.KeyOpenInventory}", new Vector2(x, y));
            y += 22;

        }
        private void RenderDialogs(SpriteBatch sprite, NPC npc)
        {
            Rectangle dialogBox = new Rectangle((int)(GameSettings.DialogBoxPadding / 2), this.game.GetScreenHeight() - (GameSettings.DialogBoxSize + GameSettings.DialogBoxY), this.game.GetScreenWidth() - GameSettings.DialogBoxPadding, GameSettings.DialogBoxSize);
            //sprite.DrawFillRectangle(dialogBox, Color.White, DialogBackgroundColor * GameSettings.DialogBoxA);
            sprite.DrawMessageBox(MB.Value, dialogBox, Color.White, 12);
            string nextDialog = $"Press {GameSettings.KeyInteract} to next";
            int nextX = (dialogBox.X + ((dialogBox.Width / 2) - GameSettings.DialogBoxPadding)) - nextDialog.Length;
            int nextY = dialogBox.Y + (dialogBox.Height);
            TextManager.Text(Fonts.Normal, nextDialog, new Vector2(nextX, nextY), Color.Black);
            TextManager.Text(Fonts.DT_L, npc.GetDialog(), new Vector2(dialogBox.X + GameSettings.DialogBoxPadding, dialogBox.Y + 24), Color.Black);
        }

        public void RenderHeart(SpriteBatch sprite)
        {
            float health = this.game.ActivePlayer.Health;
            // Image Position and Size 
            int x = 10;
            int y = 10;
            TextManager.Text(Fonts.Normal, "HEALTH: ", new Vector2(x, y + 10));
            x += this.HealthIcon.Value.Width + 50;
            for (int i = 0; i < health; i++)
            {
                Rectangle size = new Rectangle(x, y, this.HealthIcon.Value.Width, this.HealthIcon.Value.Height);
                sprite.Draw(this.HealthIcon.Value, size, Color.White);
                x += this.HealthIcon.Value.Width;
                //if (i % 10 == 0)
                //{
                //    y+= this.HealthIcon.Value.Height;
                //}
            }
        }
        public void RenderNpcs(NPC npc)
        {
            Sprite sprites = new Sprite(npc.langName, npc);
            sprites.Draw(Main.SpriteBatch);
            //Main.SpriteBatch.Draw(Main.FlatTexture, npc.InteractionBox, Color.Green);
            //Main.SpriteBatch.Draw(Main.FlatTexture, npc.FacingBox, Color.Red);
            //int facingId = (int) npc.Facing;
            //string LangName = npc.langName;
            //int w = npc.currentFrames * npc.Width;
            //int h = facingId * npc.Height;
            //Rectangle destSprites = new Rectangle(w, h, npc.Width, npc.Height);
            //Main.SpriteBatch.Draw(npc.GetTexture().Value, npc.Drawing, destSprites, Color.White);

        }
    }
}
