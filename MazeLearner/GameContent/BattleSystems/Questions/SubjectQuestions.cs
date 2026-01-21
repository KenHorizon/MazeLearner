using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.GameContent.BattleSystems.Questions
{
    public abstract class SubjectQuestions
    {
        public SubjectQuestions() { }

        public abstract void GenerateAnswer();
        public abstract string GenerateDescriptions();

        public abstract string CorrectAnswer();

        public virtual void Randomized()
        {

            this.GenerateAnswer();
        }
        public virtual string[] Answers()
        {
            return new string[0];
        }
    }
}
