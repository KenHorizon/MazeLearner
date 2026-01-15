using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.GameContent.Entity
{
    public class ItemEntity : NPC
    {
        public Assets<Texture2D> ItemIcon;
        public ItemEntity(Assets<Texture2D> itemIcon)
        {
            ItemIcon = itemIcon;
        }

        public override Assets<Texture2D> GetTexture()
        {
            return this.ItemIcon;
        }
    }
}
