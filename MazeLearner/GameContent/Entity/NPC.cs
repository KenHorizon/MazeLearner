using MazeLearner.GameContent.BattleSystems.Questions;
using MazeLearner.GameContent.Entity.Monster;
using MazeLearner.GameContent.Entity.Player;
using MazeLearner.GameContent.Phys;
using MazeLearner.Graphics.Animation;
using MazeLearner.Screen;
using MazeLearner.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;

namespace MazeLearner.GameContent.Entity
{
    public enum NpcType
    {
        NonBattle,
        Battle
    }
    public abstract class NPC : BaseEntity, InteractableNPC
    {
        private static List<NPC> NPCs = new List<NPC>();
        private static readonly UnifiedRandom Random = new UnifiedRandom((int) DateTime.Now.Ticks);
        private bool _isRemove = false;
        public bool IsRemove
        {
            get { return _isRemove; }
            set { _isRemove = value; }
        }
        public int AI { get; set; }
        public SubjectQuestions[] Questionaire;
        public NPC InteractedNpc { get; set; }
        public int DialogIndex = 0;
        private const int _limitmaxHealth = 40;
        private int _maxHealth = 20;
        private int _health = 20;
        private int _armor = 0;
        private int _damage = 1;
        private int _coin = 0;
        public int cooldownInteraction = 0;
        private int tilesize = Main.MaxTileSize;
        public int tick;
        public bool isMoving;
        public Vector2 Movement = Vector2.Zero;
        public string[] Dialogs = new string[999];
        private NpcType _npctype = NpcType.NonBattle;
        public NpcType NpcType
        {
            get { return _npctype; }
            set { _npctype = value; }
        }
        private int lightEffectX = 0;
        private int lightEffectY = 0;
        private int lightEffectW
        {
            get
            {
                return 300;
            }
            set
            {
                this.lightEffectW = value;
            }
        }
        private int lightEffectH
        {
            get
            {
                return 300;
            }
            set
            {
                this.lightEffectH = value;
            }
        }
        public Rectangle lightEffectBox
        {
            get
            {
                return new Rectangle((int)(this.Position.X - (this.lightEffectW / 2)), (int)(this.Position.Y - (this.lightEffectH / 2)), this.lightEffectW, this.lightEffectH);
            }
            set
            {
                this.lightEffectX = value.X;
                this.lightEffectY = value.Y;
                this.lightEffectW = value.Width;
                this.lightEffectH = value.Height;
            }
        }
        public bool IsPlayer { get; set; } = false;
        public int DetectionRange = 8;
        public AnimationState animationState;
        private int _deathTimer = 0;
        public int DeathTimer
        {
            get
            {
                return _deathTimer;
            }
            set
            {
                _deathTimer = value; 
            }
        }
        public bool IsAlive => this.Health > 0;
        public virtual float GetX => this.Position.X + this.InteractionBox.Width;
        public virtual float GetY => this.Position.Y + this.InteractionBox.Height;
        public static int LimitedMaxHealth => NPC._limitmaxHealth;
        public int MaxHealth
        {
            get { return _maxHealth; }
            set
            {
                if (value > _limitmaxHealth) value = _limitmaxHealth;
                if (value < 0) value = 0;
                _maxHealth = value;
            }
        }
        public int Coin
        {
            get { return _coin; }
            set
            {
                _coin = value;
            }
        }
        public int Health
        {
            get { return _health; }
            set
            {
                if (value  < 0) value = 0;
                _health = value;
            }
        }
        public int Armor
        {
            get { return _armor; }
            set
            {
                _armor = value;
            }
        }
        public int Damage
        {
            get { return _damage; }
            set
            {
                _damage = value;
            }
        }
        private int _actionTime = 0;
        public int ActionTime
        {
            get { return _actionTime; }
            set { _actionTime = value; }
        }
        public int ActionTimeLimit = 150;
        private int _varaint = 0;
        public int Variant
        {
            get { return _varaint; }
            set { _varaint = value; }
        }

        private static int NpcID = 0;

        public NPC()
        {
            this.collisionBox = new CollisionBox(Main.Instance);
            this.animationState = new AnimationState(this);
        }

        public static NPC Get(int ncpId)
        {
            return NPCs[ncpId];
        }
        private static int CreateID()
        {
            return NpcID++;
        }
        public static void Register(NPC npc)
        {
            npc.whoAmI = CreateID();
            // Loggers.Info($"Create {npc.whoAmI} {npc.Name}");
            NPCs.Add(npc);
        }
        public static List<NPC> GetAll => NPCs;
        public static int TotalNpcs => NPCs.ToArray().Length;
        public virtual void Tick(GameTime gameTime)
        {
            this.tick++;
            if (this.cooldownInteraction > 0) this.cooldownInteraction--;
            //this.IsRemove = this.IsAlive == false;
            if (this.IsAlive == false)
            {
                this.DeathTimer++;
                if (this.DeathTimer > 60)
                {
                    this.IsRemove = true;
                }
            }
            if (this.NoAI == false || this.IsRemove == false)
            {
                this.Movement = Vector2.Zero;
                this.PrevFacing = this.Facing;
                this.InteractedNpc = null;
                this.CanCollideEachOther = false;
                this.UpdateFacingBox();
                this.UpdateFacing();
                this.UpdateAI();
                if (this.CanCollideEachOther == false)
                {
                    this.Movement = this.ApplyMovement(this.Movement);
                }
                this.collisionBox.CheckTiles(this);
                this.GetNpcInteracted(this.collisionBox.CheckObjects(this, this is PlayerEntity));
                if (this.CanCollideEachOther == true)
                {
                    this.Movement = Vector2.Zero;
                }
                this.Position += (this.Movement * tilesize) * (this.RunningSpeed() * Main.Instance.DeltaTime);
                if (!this.isMoving == true || this.PrevFacing != this.Facing)
                {
                    this.animationState.Stop();
                }
                if (this.isMoving)
                {
                    this.Movement.Normalize();
                    this.animationState.Update();
                }
            }
        }
        public void SetAi(int aiType)
        {
            this.AI = aiType;
        }
        public void UpdateAI()
        {
            if (Main.IsState(GameState.Dialog) == false && Main.IsState(GameState.Pause) == false && this is PlayerEntity == false)
            {
                if (this.ActionTime++ >= this.ActionTimeLimit)
                {
                    this.ActionTime = 0;
                    this.ActionTimeLimit = Random.Next(100, 200);
                    if (this.AI == AIType.LookAroundAI)
                    {
                        this.Facing = (Facing)Random.Next(0, 4);
                    }
                    if (this.AI == AIType.WalkAroundAI)
                    {
                        this.Facing = (Facing)Random.Next(0, 4);
                        this.Movement = this.FacingToVector(this.Facing) * 16;
                    }
                }
            }
        }
        public bool NoAI => this.AI == AIType.NoAI;
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

        public void Interacted(PlayerEntity player)
        {
            this.Interact(player);
        }
        public virtual void Interact(PlayerEntity player)
        {
            this.FacingAt(player);
            if (this.Dialogs[this.DialogIndex].IsEmpty())
            {
                if (this.NpcType == NpcType.NonBattle)
                {
                    Main.GameState = GameState.Play;
                }

                if (this.NpcType == NpcType.Battle)
                {
                    this.GameIsntance.SetScreen(new BattleScreen(this, player));
                    Main.GameState = GameState.Battle;
                }
                this.DialogIndex = 0;
            }
            Loggers.Info($"{this.Name} {this.DialogIndex} said: {this.Dialogs[this.DialogIndex]}");

        }

        public virtual float RunningSpeed()
        {
            return 1.0F;
        }
        private Vector2 GetTileCoord(Vector2 worldPos)
        {
            return new Vector2((int) (worldPos.X / 32), (int) (worldPos.Y / 32));
        }
        public void UpdateDetectionRange()
        {

        }
        public void UpdateFacingBox()
        {
            switch (this.Facing)
            {
                case Facing.Down:
                    {
                        int facingX = this.InteractionBox.X;
                        int facingY = (int)(this.InteractionBox.Y + this.FacingBoxH);
                        this.FacingBox = new Rectangle(facingX, facingY, this.FacingBoxH, this.FacingBoxW);
                        //this.DetectionBox = new Rectangle(facingX, facingY, this.DetectionRangeWidth, this.DetectionRangeHeight + (32 * this.DetectionRange));
                        break;
                    }
                case Facing.Up:
                    {
                        int facingX = this.InteractionBox.X;
                        int facingY = (int)(this.InteractionBox.Y - this.FacingBoxW);
                        this.FacingBox = new Rectangle(facingX, facingY, this.FacingBoxH, this.FacingBoxW);
                        //this.DetectionBox = new Rectangle(facingX, facingY, this.DetectionRangeWidth, this.DetectionRangeHeight + (32 * this.DetectionRange));
                        break;
                    }
                case Facing.Left:
                    {
                        int facingX = (int)(this.InteractionBox.X - this.FacingBoxW);
                        int facingY = this.InteractionBox.Y;
                        this.FacingBox = new Rectangle(facingX, facingY, this.FacingBoxW, this.FacingBoxH);
                        //this.DetectionBox = new Rectangle(facingX, facingY, this.DetectionRangeWidth + (32 * this.DetectionRange), this.DetectionRangeHeight);
                        break;
                    }
                case Facing.Right:
                    {
                        int facingX = (int)(this.InteractionBox.X + this.FacingBoxH);
                        int facingY = this.InteractionBox.Y;
                        this.FacingBox = new Rectangle(facingX, facingY, this.FacingBoxW, this.FacingBoxH);
                        //this.DetectionBox = new Rectangle(facingX, facingY, this.DetectionRangeWidth + (32 * this.DetectionRange), this.DetectionRangeHeight);
                        break;
                    }
            }
        }
        public virtual void UpdateFacing()
        {

        }

        
        public virtual Vector2 ApplyMovement(Vector2 movement)
        {
            return movement;
        }

        public void DealDamage(float damage = 0.0F)
        {
            int totalDamage = this.ArmorReduceDamage(damage, this.Armor);
            this.Health -= totalDamage;
        }

        public int ArmorReduceDamage(float damage, float armor)
        {
            float damageMultiplier;
            if (armor > 0)
            {
                damageMultiplier = (100 / (100 + armor));
            } else
            {
                damageMultiplier = (100 / (100 - armor));
            }
            return (int)(damage * damageMultiplier);
        }

        public void GetNpcInteracted(int id)
        {
            if (id == 999) return;
            this.InteractedNpc = Main.Npcs[Main.MapIds][id];
        }

        public virtual void SetDefaults() 
        {
            this.Questionaire = new SubjectQuestions[this.Health];
        }

        public string GetDialog()
        {
            var getdialog = this.Dialogs[this.DialogIndex];
            if (this.Dialogs[this.DialogIndex].IsEmpty())
            {
                getdialog = "";
            }
            return getdialog;
        }
        public void FacingAt(NPC npc)
        {
            switch (npc.Facing)
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
        }


        public virtual Texture2D Portfolio()
        {
            return Asset<Texture2D>.Request($"Battle/Battler/{this.Name}").Value;
        }
        public bool RenderDialogs() 
        {
            return !this.GetDialog().IsEmpty();
        }
        public static Dictionary<int, string> EncodeMessage(string input)
        {
            var result = new Dictionary<int, string>();

            var matches = Regex.Matches(input, @"\[(\d+)\]\s*([^\[]*)");

            foreach (Match match in matches)
            {
                int index = int.Parse(match.Groups[1].Value);
                string value = match.Groups[2].Value.Trim();
                value = value.Replace("Player.Name", Main.GetActivePlayer.DisplayName);
                result[index] = value;
            }

            return result;
        }
    }
}
