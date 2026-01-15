using MazeLearner.GameContent.Entity.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.GameContent.Entity.Monster
{
    public enum Type
    {
        English,
        Math,
        Science
    }
    public enum Sequence
    {
        Intro,
        Win,
        Defeated,
        Epilogue
    }
    public abstract class SubjectEntity : NPC, InteractableNPC
    {
        public int actionTime = -1;
        public int detectionRange;
        private Type _type = Type.English;
        public Type Type
        {
            get { return _type; }
            set { _type = value; }
        }
        private Sequence _sequence = Sequence.Intro;
        public Sequence Sequence
        {
            get { return _sequence; }
            set { _sequence = value; }
        }
        public override void SetDefaults()
        {
            base.SetDefaults();
        }

        public void Interacted(PlayerEntity player)
        {
            Main.GameState = GameState.Dialog;
            this.Interact(player);
        }
        public virtual void Interact(PlayerEntity player)
        {
            this.actionTime += 1;
            if (this.actionTime > 0)
            {
                this.Sequence = Sequence.Intro;
                this.NextDialog += 1;
                if (this.IntroDialogs[this.NextDialog].IsEmpty())
                {
                    this.NextDialog = 0;
                    this.actionTime = -1;
                    this.Sequence = Sequence.Epilogue;
                    player.DealDamage(this.Damage);
                    Main.GameState = GameState.Play;
                }
                Debugs.Msg($"{this.NextDialog}");
            }
        }
    }
}
