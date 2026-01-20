using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.GameContent.Entity.Monster
{
    public class MathMonster : SubjectEntity
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            this.NpcType = NpcType.Battle;
            this.NpcCategory = NpcCategory.Math;
        }

        public override Assets<Texture2D> GetTexture()
        {
            throw new NotImplementedException();
        }
    }
}
