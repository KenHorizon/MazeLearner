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
            TestEntity test0 = new TestEntity();
            test0.Position.X = 6 * Main.MaxTileSize;
            test0.Position.Y = 2 * Main.MaxTileSize;
            Main.AddEntity(test0);
        }
    }
}
