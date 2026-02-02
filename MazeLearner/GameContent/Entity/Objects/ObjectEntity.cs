using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.GameContent.Entity.Objects
{
    public class ObjectEntity : NPC
    {
        public Dictionary<int , ObjectEntity> ObjectEntities = new Dictionary<int, ObjectEntity>();

        public override Assets<Texture2D> GetTexture()
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
