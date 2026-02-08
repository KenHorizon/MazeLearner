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
        private QuestionType _category = QuestionType.None;
        public QuestionType NpcCategory
        {
            get { return _category; }
            set { _category = value; }
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
            this.NpcCategory = (QuestionType) Main.rand.Next(0, Enum.GetNames(typeof(QuestionType)).Length);
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
