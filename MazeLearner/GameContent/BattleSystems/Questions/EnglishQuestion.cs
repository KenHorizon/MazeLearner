using MazeLearner.GameContent.BattleSystems.Questions.English;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.GameContent.BattleSystems.Questions
{
    public class EnglishQuestion : Question
    {
        private EnglishType _englishType;

        public EnglishQuestion(string text) : base(text)
        {
        }

        public EnglishQuestion(string text, string[] choices, int index) : base(text, choices, index)
        {
        }

        public EnglishType EnglishType
        {
            get { return _englishType; } 
            set { _englishType = value; }
        }

        public new static EnglishQuestion Create(string text)
        {
            return new EnglishQuestion(text);
        }
        public EnglishQuestion Type(EnglishType englishType)
        {
            this.EnglishType = englishType;
            return this;
        }
    }
}
