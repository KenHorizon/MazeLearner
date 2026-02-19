using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.GameContent.Entity.Monster
{
    public class Inky : HostileEntity
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            this.DisplayName = "Inky";
            this.Name = "Inky";
            this.AI = AIType.LookAroundAI;
            this.Health = 2;
            this.MaxHealth = 2;
            this.Damage = 1;
        }
    }
}
