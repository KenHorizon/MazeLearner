using MazeLearner.GameContent.BattleSystems.Questions;
using MazeLearner.GameContent.BattleSystems.Questions.English;
using Microsoft.Xna.Framework;
using System;

namespace MazeLearner.GameContent.Entity.Monster
{
    public enum QuestionType
    {
        None,
        Grammar,      // Covers parts of speech like nouns, verbs, adjectives
        Vocabulary,   // Word meanings and usage
        Structure,    // Sentence and paragraph structure
        Comprehension // Reading and writing skills
    }
    public abstract class HostileEntity : NPC
    {
        public const int ActionTimeCooldown = 100;
        public int detectionRange;
        private QuestionType _questionCategory = QuestionType.None;
        public QuestionType QuestionCategory
        {
            get { return _questionCategory; }
            set { _questionCategory = value; }
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            this.NpcType = NpcType.Battle;
            this.Questionaire = new SubjectQuestions[] { new EnglishQuestion() };
            this.QuestionCategory = Utils.Enums<QuestionType>();
        }
        public override void Tick(GameTime gameTime)
        {
            base.Tick(gameTime);
        }
        public override Vector2 ApplyMovement(Vector2 movement)
        {
            return movement;
        }
        public override void UpdateFacing()
        {
        }
    }
}
