using Microsoft.Xna.Framework;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.GameContent.BattleSystems.Questions.Math.Easy
{
    public enum CalcType
    {
        Add,
        Substract,
        Multiply,
        Divide
    }
    public class CalculatorAnswer : SubjectQuestions
    {
        private int _first;
        private int _second;
        private int[] answers = new int[4];
        private int correctAnswer;
        private CalcType _calctype;
        Random random = new Random();

        public int FirstNumber
        {
            get { return _first; }
            set { _first = value; }
        }

        public int SecondNumber
        {
            get { return _second; }
            set { _second = value; }
        }
        public CalcType CalcType
        {
            get { return _calctype; }
            set { _calctype = value; }
        }
        public CalculatorAnswer()
        {
            this.FirstNumber = random.Next(0, 100);
            this.SecondNumber = random.Next(0, 100);
            this.Pick();
        }

        public override void Randomized()
        {
            this.FirstNumber = random.Next(0, 100);
            this.SecondNumber = random.Next(0, 100);
            this.Pick();
            base.Randomized();
        }

        public override void GenerateAnswer()
        {
            this.answers = this.CreateArray(this.CreateAnswer(), new int[] { random.Next(0, this.CreateAnswer()), random.Next(0, this.CreateAnswer()), random.Next(0, this.CreateAnswer()) });
        }

        public override string[] Answers()
        {
            return this.answers.Select(i => i.ToString()).ToArray();
        }

        public int CreateAnswer()
        {
            int answer = 0;
            switch (this.CalcType)
            {
                case CalcType.Add:
                    {
                        answer = this.FirstNumber + this.SecondNumber;
                        break;
                    }
                case CalcType.Substract:
                    {
                        answer = this.FirstNumber - this.SecondNumber;
                        break;
                    }
                case CalcType.Multiply:
                    {
                        answer = this.FirstNumber * this.SecondNumber;
                        break;
                    }
                case CalcType.Divide:
                    {
                        answer = this.FirstNumber / this.SecondNumber;
                        break;
                    }
            }
            return answer;
        }
        public int[] CreateArray(int correct, int[] dummy)
        {
            if (dummy.Length < 3)
            {
                throw new ArgumentException("Need atleast 3 Answer");
            }
            int[] result = new int[4];
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

        public void Pick()
        {
            Array values = Enum.GetValues(typeof(CalcType));
            this.CalcType = (CalcType) values.GetValue(random.Next(values.Length));
        }


        public string Equation()
        {
            string val = "";
            switch (this.CalcType)
            {
                case CalcType.Add:
                    {
                        val = "+";
                        break;
                    }
                case CalcType.Substract:
                    {
                        val = "-";
                        break;
                    }
                case CalcType.Multiply:
                    {
                        val = "x";
                        break;
                    }
                case CalcType.Divide:
                    {
                        val = "/";
                        break;
                    }
            }
            return val;
        }

        public override string GenerateDescriptions()
        {

            return $"{this.FirstNumber} {this.Equation()} {this.SecondNumber}";
        }

        public override string CorrectAnswer()
        {
            var value = 0;
            switch (this.CalcType)
            {
                case CalcType.Add:
                    {
                        value = FirstNumber + SecondNumber;
                        break;
                    }
                case CalcType.Substract:
                    {
                        value = FirstNumber - SecondNumber;
                        break;
                    }
                case CalcType.Multiply:
                    {
                        value = FirstNumber * SecondNumber;
                        break;
                    }
                case CalcType.Divide:
                    {
                        value = MathHelper.Max(0, FirstNumber / SecondNumber);
                        break;
                    }
            }
            return value.ToString();
        }
    }
}
