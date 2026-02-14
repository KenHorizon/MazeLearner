using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.GameContent.Entity.Monster
{
    public class Gloos : HostileEntity
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            this.DisplayName = "Gloos";
            this.Name = "Gloos";
            this.AI = AIType.LookAroundAI;
            this.Health = 2;
            this.MaxHealth = 2;
            this.Damage = 1;
            this.Dialogs[0] = "Arrghhh!";
        }
    }
}
