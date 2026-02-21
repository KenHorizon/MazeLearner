using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MazeLearner.GameContent.BattleSystems.Questions
{
    public class Question
    {
        private string _text;
        private string[] _choices = new string[4];
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
        public Question(string text)
        {
            this.Text = text;
        }

        public Question A(string questions)
        {
            this.Choices[0] = $"{questions}";
            return this;
        }
        public Question B(string questions)
        {
            this.Choices[1] = $"{questions}";
            return this;
        }
        public Question C(string questions)
        {
            this.Choices[2] = $"{questions}";
            return this;
        }
        public Question D(string questions)
        {
            this.Choices[3] = $"{questions}";
            return this;
        }
        public Question CorrectQuestion(int index)
        {
            this.Index = index;
            return this;
        }

        public static Question Create(string text, string[] choices, int index)
        {
            return new Question(text, choices, index);
        }
        public static Question Create(string text)
        {
            return new Question(text);
        }
    }
}
