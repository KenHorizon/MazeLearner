using MazeLearner.GameContent.Entity;
using MazeLearner.GameContent.Entity.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            Main.Draw();
            // UI in game
            // Need to be on above incase the will overlap between it.
            this.RenderHeart(Main.SpriteBatch);
            Main.SpriteBatch.End();
            Main.DrawSprites();
            // For entity sprites sheet
            foreach (PlayerEntity player in Main.Players)
            {
                this.RenderPlayer(player);
            }
            foreach (ItemEntity item in Main.Items)
            {
                this.RenderItem(item);
            }
            foreach (NPC ncp in Main.NPCS)
            {
                this.RenderNpcs(ncp);
            }
            Main.SpriteBatch.End();
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
            Rectangle srcSprites = new Rectangle(w, h, player.Width, player.Height);
            Main.SpriteBatch.Draw(PlayerEntity.Walking.Value, player.Hitbox, destSprites, Color.White);
        }
        public void RenderItem(ItemEntity items)
        {

        }
        public void RenderNpcs(NPC npc)
        {
            const int baseWidth = 32;
            const int baseHeight = 32;
            //int facingId = (int)npc.Facing;

        }
    }
}
