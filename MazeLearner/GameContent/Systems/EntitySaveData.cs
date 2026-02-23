using MazeLearner.GameContent.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.GameContent.Systems
{
    public class EntitySaveData
    {
        private NPC _npc;
        public NPC Entity
        {
            get { return _npc; } 
            set { _npc = value; }
        }
        public EntitySaveData(NPC npc)
        {
            this.Entity = npc;
        }
    }
}
