using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.GameContent.BattleSystems.Questions
{
    public enum QuestionLevel
    {
        Easy = 0,
        Medium = 1,
        Hard = 2
    }
    
    public abstract class BaseSubject
    {
        protected Random random = new Random();
        protected int[] answers = new int[4];
        protected int correctAnswer;
        public BaseSubject() { }

        public abstract void GenerateAnswer();
        public abstract string Tooltip0();
        public abstract string Tooltip1();
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
