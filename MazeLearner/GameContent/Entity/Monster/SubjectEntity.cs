using MazeLearner.GameContent.Entity.Player;
using MazeLearner.Screen;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.GameContent.Entity.Monster
{
    public enum NpcCategory
    {
        English,
        Math,
        Science
    }
    public enum NpcSequence
    {
        Intro,
        Win,
        Defeated,
        Epilogue
    }
    public enum NpcAction
    {
        Idle,
        Look,
        Walk
    }
    public enum NpcType
    {
        NonBattle,
        Battle
    }
    public abstract class SubjectEntity : NPC, InteractableNPC
    {
        public const int ActionTimeCooldown = 100;
        public int detectionRange;
        public int dialogActionTime = -1;
        private NpcType _npctype = NpcType.NonBattle;
        public NpcType NpcType
        {
            get { return _npctype; }
            set { _npctype = value; }
        }
        private NpcCategory _category = NpcCategory.English;
        public NpcCategory NpcCategory
        {
            get { return _category; }
            set { _category = value; }
        }
        private NpcSequence _sequence = NpcSequence.Intro;
        public NpcSequence Sequence
        {
            get { return _sequence; }
            set { _sequence = value; }
        }
        private NpcAction _action = NpcAction.Idle;
        public NpcAction NpcAction
        {
            get { return _action; }
            set { _action = value; }
        }
        private static readonly Random Random = new Random();
        private int actionTimer = 0;
        private int actionDuration = 0;
        public override void SetDefaults()
        {
            base.SetDefaults();
        }

        public void Interacted(PlayerEntity player)
        {
            Main.GameState = GameState.Pause;
            this.Interact(player);
        }
        public virtual void Interact(PlayerEntity player)
        {
            this.dialogActionTime += 1;
            switch (player.Facing)
            {
                case Facing.Up:
                    {
                        this.Facing = Facing.Down; break;
                    }
                case Facing.Down:
                    {
                        this.Facing = Facing.Up; break;
                    }
                case Facing.Right:
                    {
                        this.Facing = Facing.Left; break;
                    }
                case Facing.Left:
                    {
                        this.Facing = Facing.Right; break;
                    }
            }
            this.Sequence = NpcSequence.Intro;
            Loggers.Msg($"{this.langName} said: {this.Dialogs[this.NextDialog]}");
            if (this.dialogActionTime > 0)
            {
                this.NextDialog++;
                if (this.NpcType == NpcType.NonBattle)
                {
                    if (this.Dialogs[this.NextDialog].IsEmpty())
                    {
                        this.NextDialog = 0;
                        this.dialogActionTime = -1;
                        player.DealDamage(this.Damage);
                        Main.GameState = GameState.Play;
                    }
                }

                if (this.NpcType == NpcType.Battle)
                {
                    if (this.Dialogs[this.NextDialog].IsEmpty())
                    {
                        this.NextDialog = 0;
                        this.dialogActionTime = -1;
                        this.GameIsntance.SetScreen(new BattleScreen(this, player));
                        Main.GameState = GameState.Battle;
                    }
                }
                // TODO: Make a fully seqeunce dialog 
                // where if the npc is can battle start with -> Intro -> Challenge the player -> if Win -> Win or if Defeat
                // -> Defeat -> After the epilogue
                // if non battle npc is being interacted then the game will start the intro only even the other dialog is being
                // written it will not play! only intro dialog is destined to play in this non-battle npc!

                //if (this.WinDialogs[this.NextDialog].IsEmpty())
                //{
                //    this.NextDialog = 0;
                //    this.dialogActionTime = -1;
                //    if (this.NpcType == NpcType.NonBattle)
                //    {
                //        this.Sequence = Sequence.Epilogue;
                //    }
                //}
                //if (this.DefeatDialogs[this.NextDialog].IsEmpty())
                //{
                //    this.NextDialog = 0;
                //    this.dialogActionTime = -1;
                //    if (this.NpcType == NpcType.NonBattle)
                //    {
                //        this.Sequence = Sequence.Epilogue;
                //    }
                //}
                //if (this.EpilogueDialogs[this.NextDialog].IsEmpty())
                //{
                //    this.NextDialog = 0;
                //    this.dialogActionTime = -1;
                //    if (this.NpcType == NpcType.NonBattle)
                //    {
                //        this.Sequence = Sequence.Epilogue;
                //    }
                //    player.DealDamage(this.Damage);
                //    Main.GameState = GameState.Play;
                //}
            }
        }
        public override void Tick()
        {
            base.Tick();
        }
        public void ChooseNextAction()
        {
            if (Main.GameState == GameState.Pause) return;
            this.actionTimer = 0;
            this.actionDuration = Random.Next(30, ActionTimeCooldown);
            int roll = Random.Next(100);
            if (roll < 50)
            {
                this.NpcAction = NpcAction.Idle;
            }
            else if (roll < 75)
            {
                this.NpcAction = NpcAction.Look;
            }
            else
            {
                //this.NpcAction = Action.Walk;
            }
            if (this.NpcAction == NpcAction.Look || this.NpcAction == NpcAction.Walk)
            {
                this.Facing = (Facing)Random.Next(0, 4);
            }
        }

        private Vector2 FacingToVector(Facing facing)
        {
            return facing switch
            {
                Facing.Up => new Vector2(0, -1),
                Facing.Down => new Vector2(0, 1),
                Facing.Left => new Vector2(-1, 0),
                Facing.Right => new Vector2(1, 0),
                _ => Vector2.Zero
            };
        }
        public override Vector2 ApplyMovement(Vector2 movement)
        {
            this.actionTimer++;
            if (actionTimer >= actionDuration)
            {
                this.ChooseNextAction();
            }
            switch (this.NpcAction)
            {
                case NpcAction.Idle:
                    // do nothing
                    break;

                case NpcAction.Look:
                    // only facing changes, no movement
                    break;

                case NpcAction.Walk:
                    movement += FacingToVector(Facing);
                    break;
            }
            return movement;
        }
        public override void UpdateFacing()
        {
        }
    }
}
