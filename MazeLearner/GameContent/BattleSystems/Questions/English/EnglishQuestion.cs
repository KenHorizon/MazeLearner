

using System.Collections.Generic;

namespace MazeLearner.GameContent.BattleSystems.Questions.English
{
    public class EnglishQuestion : SubjectQuestions
    {
        private static List<Question> EnglishQuestions = new List<Question>();
        public Question Question;
        public override void Randomized()
        {
            this.Question = EnglishQuestions[random.Next(EnglishQuestions.Count - 1)];
            base.Randomized();
        }

        public static void Add(Question question)
        {
            Loggers.Info($"Registering the questions {question.Text}");
            EnglishQuestions.Add(question); 
        }

        public override string[] Answers()
        {
            return this.Question.Choices;
        }

        public override string CorrectAnswer()
        {
            return this.Question.Choices[this.Question.Index];
        }

        public override void GenerateAnswer()
        {

        }

        public override string GenerateDescriptions()
        {
            return this.Question.Text;
        }
    }
}
