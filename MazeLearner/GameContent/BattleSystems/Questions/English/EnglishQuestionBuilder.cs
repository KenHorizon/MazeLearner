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
            EnglishQuestion.EnglishQuestions.Add(Question.Create("Which punctuation ends an exclamation?",
                new string[] { ".", "?", "!", ";" }, 2));
            EnglishQuestion.EnglishQuestions.Add(Question.Create("What is the plural form of 'child'?",
                new string[] { "Childs", "Children", "Childrens", "Childes" }, 1));
            EnglishQuestion.EnglishQuestions.Add(Question.Create("Which of these is a verb?",
                new string[] { "Table", "Jump", "Blue", "Quickly" }, 2));
            EnglishQuestion.EnglishQuestions.Add(Question.Create("Which word is a noun?",
                new string[] { "Quickly", "Happy", "Mountain", "Softly" }, 2));
            EnglishQuestion.EnglishQuestions.Add(Question.Create("Which of these is a complete synonym for 'happy'?",
                new string[] { "Sad", "Joyful", "Angry", "Tired" }, 1));
            EnglishQuestion.EnglishQuestions.Add(Question.Create("Which word best completes: She ____ a sandwich.",
                new string[] { "Eats", "Eating", "Ate", "Eaten" }, 2));
            EnglishQuestion.EnglishQuestions.Add(Question.Create("Choose the correct past tense of 'run'.",
                new string[] { "Runned", "Ran", "Ranned", "Running" }, 1));
        }

        static List<Question> questions = new List<Question>()
    {
        new Question("What is the plural form of 'child'?",
            new[]{"1) childs","2) children","3) childrens","4) childes"}, 1),
        new Question("Which sentence is correct?",
            new[]{"1) She do her homework.","2) She does her homework.","3) She doing her homework.","4) She diding her homework."}, 1),
        new Question("Which word is a noun?",
            new[]{"1) quickly","2) happy","3) mountain","4) softly"}, 2),
        new Question("What is 8 × 7?",
            new[]{"1) 48","2) 54","3) 56","4) 58"}, 2),
        new Question("Which is a sentence fragment (not a complete sentence)?",
            new[]{"1) The dog barked loudly.","2) When the bell rang.","3) I finished my work.","4) She smiles."}, 1),
        new Question("Which word is the opposite of 'ancient'?",
            new[]{"1) old","2) modern","3) historic","4) antique"}, 1),
        new Question("Which of these is a right angle?",
            new[]{"1) 45°","2) 90°","3) 120°","4) 30°"}, 1),
        new Question("Choose the correct past tense of 'run'.",
            new[]{"1) runned","2) ran","3) ranned","4) running"}, 1),
        new Question("Which sentence uses a question mark correctly?",
            new[]{"1) How are you!","2) Where is the book?","3) I like apples?","4) She went to school?"}, 1),
        new Question("What is the main idea of a short story?",
            new[]{"1) The weather report","2) The main message or point","3) A list of characters' names","4) The font size"}, 1),
        new Question("Which of these is a complete synonym for 'happy'?",
            new[]{"1) sad","2) joyful","3) angry","4) tired"}, 1),
        new Question("What is 100 divided by 4?",
            new[]{"1) 20","2) 25","3) 40","4) 35"}, 1),
        new Question("Which word best completes: She ____ a sandwich.",
            new[]{"1) eats","2) eating","3) ate","4) eaten"}, 2),
        new Question("Which of these is a verb?",
            new[]{"1) table","2) jump","3) blue","4) quickly"}, 1),
        new Question("Which punctuation ends an exclamation?",
            new[]{"1) .","2) ?","3) !","4) ;"}, 2)
    };
    }
}
