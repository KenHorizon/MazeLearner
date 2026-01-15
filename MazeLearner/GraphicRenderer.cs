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
        private Main game;
        public GraphicRenderer(Main game)
        {
            this.game = game;
        }

        public void Draw()
        {
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
        }

        public void RenderPlayer(PlayerEntity player)
        {
            const int baseWidth = 32;
            const int baseHeight = 32;
            const int frame = 4;
            int facingId = (int)player.Facing;
            PlayerState playerState = player.PlayerState;
            int w = player.currentFrame * player.Width;
            int h = facingId * player.Height;
            Rectangle destSprites = new Rectangle(w, h, player.Width, player.Height);
            Main.SpriteBatch.Draw(PlayerEntity.Walking.Value, player.Size, Color.White);
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
