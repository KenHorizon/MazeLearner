using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.GameContent.Entity.Player
{
    public class PlayerData
    {
        public string Name { get; set; }
        public PlayerEntity PlayerEntity { get; set; }

        public PlayerData(string name)
        {

        }

        public void CreatePlayer()
        {
            PlayerEntity = new PlayerEntity();
        }
    }
}
