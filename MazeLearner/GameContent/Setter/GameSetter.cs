using MazeLearner.GameContent.Entity.Monster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.GameContent.Setter
{
    public class GameSetter
    {
        public Main game;
        public GameSetter(Main game)
        {
            this.game = game;
        }

        public void SetupGame()
        {
            Gloos gloos = new Gloos();
            gloos.SetPos(29, 31);
            Main.AddEntity(gloos);
        }
    }
}
