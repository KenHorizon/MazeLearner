using MazeLeaner.Text;
using MazeLearner.GameContent.Entity;
using MazeLearner.GameContent.Entity.Player;
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
        private Assets<Texture2D> HealthIcon;
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
            // For entity sprites sheet
            foreach (var renderEntity in Main.AllEntity)
            {
                if (renderEntity != null)
                {
                    this.RenderNpcs(renderEntity);
                }
            }
            //foreach (ItemEntity item in Main.Items)
            //{
            //    if (item != null)
            //    {
            //        this.RenderItem(item);
            //    }
            //}
            //foreach (NPC npc in Main.NPCS)
            //{
            //    if (npc != null)
            //    {
            //        this.RenderNpcs(npc);
            //    }
            //}
            //foreach (PlayerEntity player in Main.Players)
            //{
            //    if (player != null)
            //    {
            //        this.RenderNpcs(player);
            //    }
            //}
            Main.SpriteBatch.End();
            // UI in game
            // Need to be on above incase the will overlap between it.
            Main.DrawAlpha();
            this.RenderOverlayKeyBinding(Main.SpriteBatch);
            this.RenderHeart(Main.SpriteBatch);
            if (Main.GameState == GameState.Pause)
            {
                foreach (NPC npc in Main.NPCS)
                {
                    if (npc != null && npc.RenderDialogs())
                    {
                        this.RenderDialogs(Main.SpriteBatch, npc);
                    }
                }
                foreach (ItemEntity item in Main.Items)
                {
                    if (item != null && item.RenderDialogs())
                    {
                        this.RenderDialogs(Main.SpriteBatch, item);
                    }
                }
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
            sprite.DrawFillRectangle(dialogBox, Color.White, DialogBackgroundColor * GameSettings.DialogBoxA);
            TextManager.Text(Fonts.Large, npc.GetIntroDialog(), new Vector2(dialogBox.X + GameSettings.DialogBoxPadding, dialogBox.Y + 24), 1.0F);
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
            int facingId = (int)npc.Facing;
            string LangName = npc.langName;
            int w = npc.currentFrame * npc.Width;
            int h = facingId * npc.Height;
            Rectangle destSprites = new Rectangle(w, h, npc.Width, npc.Height);
            Main.SpriteBatch.Draw(Main.FlatTexture, npc.InteractionBox, Color.Green);
            Main.SpriteBatch.Draw(Main.FlatTexture, npc.FacingBox, Color.Red);
            Main.SpriteBatch.Draw(npc.GetTexture().Value, npc.Drawing, destSprites, Color.White);
        }
    }
}
