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
            test0.SetPos(29, 31);
            MathTestEntity test1 = new MathTestEntity();
            test1.SetPos(29, 32);
            EnglishTestEntity test2 = new EnglishTestEntity();
            test2.SetPos(29, 33);
            Gloos gloos = new Gloos();
            gloos.SetPos(29, 34);
            Main.AddEntity(test0);
            Main.AddEntity(test1);
            Main.AddEntity(test2);
            Main.AddEntity(gloos);
        }
    }
}
