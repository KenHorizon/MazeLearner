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
        public Asset<Texture2D> ItemIcon;
        public ItemEntity(Asset<Texture2D> itemIcon)
        {
            ItemIcon = itemIcon;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();        
        }
    }
}
