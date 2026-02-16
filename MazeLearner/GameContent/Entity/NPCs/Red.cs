using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.GameContent.Entity.NPCs
{
    public class Red : NPC
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            this.DisplayName = "Red Akai";
            this.Name = $"Red";
            this.Health = 5;
            this.MaxHealth = 5;
            this.Damage = 1;
        }
    }
}
