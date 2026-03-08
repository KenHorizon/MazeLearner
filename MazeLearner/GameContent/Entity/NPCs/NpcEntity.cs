using MazeLearner.Audio;
using MazeLearner.GameContent.Entity.Objects;
using MazeLearner.GameContent.Entity.Player;
using MazeLearner.Screen;
using MazeLearner.Worlds;
using Microsoft.Xna.Framework;
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
            this.Name = $"Npc";
            this.CanCollide = true;
        }
    }
}
