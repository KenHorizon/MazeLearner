using MazeLearner.GameContent.BattleSystems.Questions;
using MazeLearner.GameContent.BattleSystems.Questions.Math;
using MazeLearner.GameContent.Entity.Player;
using MazeLearner.Screen;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        public SubjectQuestions[] Questionaire;
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
            this.Interact(player);
        }
        public virtual void Interact(PlayerEntity player)
        {
            this.FacingAt(player);
            if (this.Dialogs[this.NextDialog].IsEmpty())
            {
                this.NextDialog = 0;
                if (this.NpcType == NpcType.NonBattle)
                {
                    Main.GameState = GameState.Play;
                }

                if (this.NpcType == NpcType.Battle)
                {
                    this.GameIsntance.SetScreen(new BattleScreen(this, player));
                    Main.GameState = GameState.Battle;
                }
            }
            Loggers.Msg($"{this.langName} {this.NextDialog} said: {this.Dialogs[this.NextDialog]}");
            
        }

        
        public override void Tick(GameTime gameTime)
        {
            base.Tick(gameTime);
        }

        public virtual Texture2D BattleImage()
        {
            return Assets<Texture2D>.Request($"Battle/Battler/{this.langName}").Value;
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
