using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.GameContent.Entity.Monster
{
    public class Knight : HostileEntity
    {
        public Knight(int variant = 0) : base()
        {
            this.Variant = variant;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            this.DisplayName = "Knight";
            this.Name = $"Knight_{this.Variant}";
            this.AI = AIType.LookAroundAI;
            this.Health = 2;
            this.MaxHealth = 2;
            this.Damage = 1;
        }
    }
}
