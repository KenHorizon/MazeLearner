using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.GameContent.BattleSystems.Questions.English
{
    public class EnglishQuestionBuilder
    {
        public static void Register()
        {
            EnglishQuestion.EnglishQuestions.Add(Question.Create(
                "Which punctuation ends an exclamation?",
                new string[] { ".", "?", "!", ";" },
                2));
            EnglishQuestion.EnglishQuestions.Add(Question.Create(
                "What is the plural form of 'child'?",
                new string[] { "Childs", "Children", "Childrens", "Childes" },
                1));
            EnglishQuestion.EnglishQuestions.Add(Question.Create(
                "Which of these is a verb?",
                new string[] { "Table", "Jump", "Blue", "Quickly" },
                2));
            EnglishQuestion.EnglishQuestions.Add(Question.Create(
                "Which word is a noun?",
                new string[] { "Quickly", "Happy", "Mountain", "Softly" },
                2));
            EnglishQuestion.EnglishQuestions.Add(Question.Create(
                "Which of these is a complete synonym for 'happy'?",
                new string[] { "Sad", "Joyful", "Angry", "Tired" },
                1));
            EnglishQuestion.EnglishQuestions.Add(Question.Create(
                "Which word best completes: She ____ a sandwich.",
                new string[] { "Eats", "Eating", "Ate", "Eaten" },
                2));
            EnglishQuestion.EnglishQuestions.Add(Question.Create(
                "Which is a sentence fragment (not a complete sentence)?",
                new string[] { "The dog barked loudly", "When the bell rang", "I finished my work", "She smiles" },
                1));
            EnglishQuestion.EnglishQuestions.Add(Question.Create(
                "Which sentence is correct?",
                new string[] { "She do her homework", "She does her homework", "She doing her homework", "She diding her homework" },
                1));
        }
    }
}
