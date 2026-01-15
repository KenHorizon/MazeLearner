using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.GameContent.Entity.Monster
{
    public class TestEntity : MonsterEntity
    {
        public override void SetDefaults()
        {
            this.langName = "TestSubject";
            this.Health = 5;
            this.Damage = 1;
        }
    }
}
