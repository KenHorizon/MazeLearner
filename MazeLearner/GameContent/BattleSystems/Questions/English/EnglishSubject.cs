

using System.Collections.Generic;
using System.Linq;

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
        private QuestionLevel level;

        public EnglishSubject(QuestionLevel level = QuestionLevel.Easy)
        {
            this.level = level;
            this.Randomized();
        }

        public override void Randomized()
        {
            List<Question> matched = EnglishQuestions.Where(eq => this.level == eq.TypeLevel).ToList();
            this.Question = matched[Main.Random.Next(0, matched.Count)];
            this.GenerateAnswer();
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
            if (this.Question.Index == 0)
            {
                this.answers = this.CreateArray(this.Question.Choices[0], new string[] { this.Question.Choices[1], this.Question.Choices[2], this.Question.Choices[3] });
            }
            if (this.Question.Index == 1)
            {
                this.answers = this.CreateArray(this.Question.Choices[1], new string[] { this.Question.Choices[0], this.Question.Choices[2], this.Question.Choices[3] });
            }
            if (this.Question.Index == 2)
            {
                this.answers = this.CreateArray(this.Question.Choices[2], new string[] { this.Question.Choices[0], this.Question.Choices[1], this.Question.Choices[3] });
            }
            if (this.Question.Index == 3)
            {
                this.answers = this.CreateArray(this.Question.Choices[3], new string[] { this.Question.Choices[0], this.Question.Choices[1], this.Question.Choices[2] });
            }
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
