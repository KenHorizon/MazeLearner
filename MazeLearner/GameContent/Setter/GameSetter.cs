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
            test0.Position.X = 2 * Main.MaxTileSize;
            test0.Position.Y = 2 * Main.MaxTileSize;
            MathTestEntity test1 = new MathTestEntity();
            test1.Position.X = 2 * Main.MaxTileSize;
            test1.Position.Y = 4 * Main.MaxTileSize;
            Main.AddEntity(test0);
            Main.AddEntity(test1);
        }
    }
}
