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
            foreach (ItemEntity item in Main.Items)
            {
                if (item != null)
                {
                    this.RenderItem(item);
                }
            }
            foreach (NPC npc in Main.NPCS)
            {
                if (npc != null)
                {
                    this.RenderNpcs(npc);
                }
            }
            foreach (PlayerEntity player in Main.Players)
            {
                if (player != null)
                {
                    this.RenderPlayer(player);
                }
            }
            Main.SpriteBatch.End();
            // UI in game
            // Need to be on above incase the will overlap between it.
            Main.Draw();
            this.RenderHeart(Main.SpriteBatch);
            if (Main.GameState == GameState.Dialog)
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

        private void RenderDialogs(SpriteBatch sprite, NPC npc)
        {
            Rectangle dialogBox = new Rectangle(10, this.game.GetScreenHeight() - 280, this.game.GetScreenWidth() - 20, 250);
            sprite.DrawFillRectangle(dialogBox, Color.White, Color.Gray);
            TextManager.Text(Fonts.Normal, npc.GetIntroDialog(), new Vector2(dialogBox.X + 10, dialogBox.Y), 1.0F);
        }

        public void RenderHeart(SpriteBatch sprite)
        {
            float health = this.game.ActivePlayer.Health;
            // Image Position and Size 
            int x = 10;
            int y = 10;
            for (int i = 0; i < health; i++)
            {
                Rectangle size = new Rectangle(x, y, this.HealthIcon.Value.Width, this.HealthIcon.Value.Height);
                sprite.Draw(this.HealthIcon.Value, size, Color.White);
                x += this.HealthIcon.Value.Width;
            }
        }

        public void RenderPlayer(PlayerEntity player)
        {
            int facingId = (int)player.Facing;
            PlayerState playerState = player.PlayerState;
            int w = player.currentFrame * player.Width;
            int h = facingId * player.Height;
            Rectangle destSprites = new Rectangle(w, h, player.Width, player.Height);
            Main.SpriteBatch.Draw(Main.FlatTexture, player.InteractionBox, Color.Green);
            Main.SpriteBatch.Draw(Main.FlatTexture, player.FacingBox, Color.Red);
            if (player.PlayerRunning())
            {
                Main.SpriteBatch.Draw(PlayerEntity.Running.Value, player.Drawing, destSprites, Color.White);
            }
            else
            {
                Main.SpriteBatch.Draw(PlayerEntity.Walking.Value, player.Drawing, destSprites, Color.White);
            }
        }
        public void RenderItem(ItemEntity items)
        {

        }
        public void RenderNpcs(NPC npc)
        {
            int facingId = (int)npc.Facing;
            string LangName = npc.langName;
            int w = npc.currentFrame * npc.Width;
            int h = facingId * npc.Height;
            Rectangle destSprites = new Rectangle(w, h, npc.Width, npc.Height);
            Rectangle srcSprites = new Rectangle(w, h, npc.Width, npc.Height);
            Main.SpriteBatch.Draw(Main.FlatTexture, npc.InteractionBox, Color.Green);
            Main.SpriteBatch.Draw(Main.FlatTexture, npc.FacingBox, Color.Red);
            Main.SpriteBatch.Draw(npc.GetTexture().Value, npc.Drawing, destSprites, Color.White);
        }
    }
}
