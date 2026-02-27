

using System.Collections.Generic;

namespace MazeLearner.GameContent.BattleSystems.Questions.English
{
    public enum EnglishType
    {
        Noun = 0,
        Pronoun = 1,
        Verb = 2,
        Adjective = 3,
        Tense = 4,
        Sentence = 5,
        Synonym = 6,
        Comprehension = 7,
        Grammar = 8,
        Paragraph = 9
    }
    public class EnglishSubject : BaseSubject
    {
        private static List<Question> EnglishQuestions = new List<Question>();
        public Question Question;

        public EnglishSubject()
        {
            this.Question = EnglishQuestions[random.Next(EnglishQuestions.Count - 1)];
            base.Randomized();
        }

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
        public override QuestionLevel Level()
        {
            return this.Question.TypeLevel;
        }
        public override string GenerateDescriptions()
        {
            return this.Question.Text;
        }

        public override string Tooltip0()
        {
            return this.Question.Tips0;
        }

        public override string Tooltip1()
        {
            return this.Question.Tips1;
        }
    }
}
