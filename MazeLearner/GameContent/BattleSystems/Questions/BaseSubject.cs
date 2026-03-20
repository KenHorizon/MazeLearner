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
        protected string[] answers = new string[4];
        protected string correctAnswer;

        public string[] Answer => answers;

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

        public abstract QuestionLevel Level();

        public string[] CreateArray(string correct, string[] dummy)
        {
            if (dummy.Length < 3)
            {
                throw new ArgumentException("Need atleast 3 Answer");
            }
            string[] result = new string[4];
            int correctIndex = this.random.Next(0, 4);
            result[correctIndex] = correct;
            int dummyIndex = 0; for (int i = 0; i < 4; i++)
            {
                if (i == correctIndex) continue;

                result[i] = dummy[dummyIndex];
                dummyIndex++;
            }

            return result;
        }

    }
}
