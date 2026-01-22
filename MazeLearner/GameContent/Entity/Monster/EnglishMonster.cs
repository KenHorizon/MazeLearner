
using MazeLearner.GameContent.BattleSystems.Questions;
using MazeLearner.GameContent.BattleSystems.Questions.English;
using MazeLearner.GameContent.BattleSystems.Questions.Math;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.GameContent.Entity.Monster
{
    public abstract class EnglishMonster : SubjectEntity
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            this.Questionaire = new SubjectQuestions[] { new EnglishQuestion() };
            this.NpcType = NpcType.Battle;
            this.NpcCategory = NpcCategory.English;
        }
    }
}
