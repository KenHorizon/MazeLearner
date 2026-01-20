using MazeLearner.GameContent.Entity;
using MazeLearner.GameContent.Entity.Monster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.GameContent.BattleSystems
{
    public class QuestionSystem
    {
        private string[] question = new string[100];
        private string[] questionAnswer = new string[4];
        private int questionAnswerIndex = 0;

        private NpcCategory _npccategory;
        public NpcCategory NpcCategory
        {
            get { return _npccategory; }
            set { _npccategory = value; }
        }
        public QuestionSystem(NpcCategory category)
        {
            this._npccategory = category;
        }

        public int GetQuestionAnswerIndex()
        {
            return questionAnswerIndex;
        }

        public void Start()
        {

        }

        public void End()
        {

        }

        public bool IsCorrect()
        {
            return false;
        }
    }
}
