using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.GameContent.BattleSystems.Questions
{
    public class Question
    {
        private string _text;
        private string[] _choices;
        private int index;

        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }
        public string[] Choices
        {
            get { return _choices; }
            set { _choices = value; }
        }
        public int Index
        {
            get { return index; }
            set { index = value; }
        }
        public Question(string text, string[] choices, int index)
        {
            this.Text = text;
            this.Choices = choices;
            this.Index = index;
        }

        public static Question Create(string text, string[] choices, int index)
        {
            return new Question(text, choices, index);
        }
    }
}
