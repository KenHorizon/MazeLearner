using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.GameContent.Entity.Objects
{
    public class ObjectItem : NPC
    {
        public Dictionary<int , ObjectItem> ObjectEntities = new Dictionary<int, ObjectItem>();

        public override Asset<Texture2D> GetTexture()
        {
            return null;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
        }

        public override void Tick(GameTime gameTime)
        {
            base.Tick(gameTime);
        }
    }
}
