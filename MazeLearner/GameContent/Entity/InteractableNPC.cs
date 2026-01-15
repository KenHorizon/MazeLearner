using MazeLearner.GameContent.Entity.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.GameContent.Entity
{
    public interface InteractableNPC
    {
        void Interacted(PlayerEntity player);
    }
}
