using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.GameContent.Entity.NPCs
{
    public class NpcEntity : NPC
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            this.Name = $"Npc Entity";
        }
    }
}
