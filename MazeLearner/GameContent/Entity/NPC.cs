using MazeLearner.Audio;
using MazeLearner.GameContent.BattleSystems.Questions;
using MazeLearner.GameContent.BattleSystems.Questions.English;
using MazeLearner.GameContent.Entity.AI;
using MazeLearner.GameContent.Entity.Objects;
using MazeLearner.GameContent.Entity.Player;
using MazeLearner.GameContent.Phys;
using MazeLearner.Graphics;
using MazeLearner.Graphics.Animation;
using MazeLearner.Screen;
using MazeLearner.Screen.Widgets;
using MazeLearner.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace MazeLearner.GameContent.Entity
{
    public enum MovementState
    {
        Idle = 0,
        Walking = 1
    }
    public enum NpcType
    {
        NonBattle,
        Battle
    }
    public enum QuestionType
    {
        None,
        Grammar,      // Covers parts of speech like nouns, verbs, adjectives
        Vocabulary,   // Word meanings and usage
        Structure,    // Sentence and paragraph structure
        Comprehension // Reading and writing skills
    }
    public class NPC : BaseEntity, InteractableNPC
    {
        private bool _isLoadedNow = false;
        public bool IsLoadedNow
        {
            get
            {
                return _isLoadedNow;
            }
            set
            {
                _isLoadedNow = value;
            }
        }

        private MovementState _movementState = MovementState.Idle;
        public MovementState MovementState
        {
            get { return _movementState; }
            set { _movementState = value; }
        }

        private static List<NPC> NPCs = new List<NPC>();
        private static readonly UnifiedRandom Random = new UnifiedRandom((int) DateTime.Now.Ticks);
        private bool _isRemove = false;
        public bool IsRemove
        {
            get { return _isRemove; }
            set { _isRemove = value; }
        }
        public int AI { get; set; }
        public List<BaseSubject> Questionaire = new List<BaseSubject>();
        public int detectionRange;
        private int _battleLevel; 
        private QuestionType _questionCategory = QuestionType.None;
        public QuestionType QuestionCategory
        {
            get { return _questionCategory; }
            set { _questionCategory = value; }
        }
        public int BattleLevel
        {
            get { return _battleLevel; }    
            set { _battleLevel = value; }
        }
        public ObjectEntity InteractedObject { get; set; } = null;
        public NPC InteractedNpc { get; set; } = null;
        private const int _limitmaxHealth = 40;
        private bool OnPath = false;
        private int _maxHealth = 20;
        private int _health = 20;
        private int _armor = 0;
        private int _damage = 1;
        private int _coin = 0;
        private int _tempMaxHealth;
        private int _tempHealth;
        private int _tempDamage;
        private int _tempArmor;
        public int TempArmor
        {
            get { return _tempArmor; }
            set { _tempArmor = value; }
        }
        public int TempMaxHealth
        {
            get { return _tempMaxHealth; }
            set { _tempMaxHealth = value; }
        }
        public int TempHealth
        {
            get { return _tempHealth; }
            set { _tempHealth = value; }
        }
        public int TempDamage
        {
            get { return _tempDamage; }
            set { _tempDamage = value; }
        }
        public int cooldownInteraction = 0;
        public int tick;
        public bool isRunning;
        public bool isMoving;
        public float MovementSpeed = 64.0F;
        public float MovementProgress;
        private NpcType _npctype = NpcType.NonBattle;
        private int _scorePointDrops = 0;
        private Pathfind _pathfind;
        public Pathfind PathFind
        {
            get { return _pathfind; }
            set { _pathfind = value; }  
        }
        public NpcType NpcType
        {
            get { return _npctype; }
            set { _npctype = value; }
        }
        private int portfolio = 0;
        public int Portfolio
        {
            get { return portfolio; }
            set
            {
                portfolio = value;
            }
        }
        public int ScorePointDrops
        {
            get { return _scorePointDrops; }
            set
            {
                _scorePointDrops = value;
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
        public static int CapMaxHealth => NPC._limitmaxHealth;
        public int MaxHealth
        {
            get { return _maxHealth + this.TempMaxHealth; }
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
            get { return _health + this.TempHealth; }
            set
            {
                if (value  < 0) value = 0;
                _health = value;
            }
        }
        public int Armor
        {
            get { return _armor + this.TempArmor; }
            set
            {
                _armor = value;
            }
        }
        public int Damage
        {
            get { return _damage + this.TempDamage; }
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
            this._pathfind = new Pathfind(this.game);
        }
        

        public virtual void SetDefaults() 
        {
            for (int i = 0; i < this.Health; i++)
            {
                var subject = new EnglishSubject();
                if (subject.Level() == (QuestionLevel)Enum.ToObject(typeof(QuestionLevel), this.BattleLevel));
                {
                    this.Questionaire.Add(subject);
                }
            }
        }


        public static NPC Get(int ncpId)
        {
            return (NPC) NPCs[ncpId].Clone();
        }
        private static int CreateID()
        {
            return NpcID++;
        }
        public static void Register(NPC npc)
        {
            npc.type = CreateID();
            Loggers.Info($"Created Entity Type:{npc.type} Name:{npc.Name}");
            NPCs.Add(npc);
        }
        public static List<NPC> GetAll => NPCs;
        public static int TotalNpcs => NPCs.ToArray().Length;
        public virtual void Tick(GameTime gameTime)
        {
            this.tick++;
            if (this.cooldownInteraction > 0) this.cooldownInteraction--;
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
                this.PrevFacing = this.Direction;
                this.PrevPosition = this.Position;
                this.InteractedNpc = null;
                this.CollideOn = false;
                switch (this.MovementState)
                {
                    case MovementState.Idle:
                        {
                            this.HandleInput();
                            break;
                        }
                    case MovementState.Walking:
                        {
                            this.UpdateMovement();
                            break;
                        }
                }
                this.UpdateFacingBox();
                this.UpdateFacing();
                this.UpdateAI();
                this.GetNpcInteracted(this.collisionBox.CheckNpc(this, this is PlayerEntity));
                if (this.MovementState == MovementState.Idle)
                {
                    this.animationState.Stop();
                }
                if (this.isMoving)
                {
                    this.animationState.Update();
                }
            }
        }

        public void ApplyMovement()
        {
            if (Main.Tiled.IsWalkable(this.TargetPosition) == true && this.CollideOn == false)
            {
                this.StartMovement();
            } 
            else
            {
                this.isMoving = false;
            }
        }

        public void StartMovement()
        {
            this.MovementState = MovementState.Walking;
            this.TargetDirection = this.Direction;

            this.isMoving = true;
            Vector2 pos = this.Position;
            this.StartPosition = this.Offset(pos);
            this.MovementProgress = 0.0F;
        }
        public void UpdateMovement()
        {
            float progressPerSeconds = this.MovementSpeed / Main.TileSize;
            this.MovementProgress += progressPerSeconds * this.RunningSpeed() * this.game.DeltaTime;
            if (this.MovementProgress >= 1.0F)
            {
                this.MovementProgress = 1.0F;
            }
            this.Position = Vector2.Lerp(this.OffsetReverse(this.StartPosition), this.OffsetReverse(this.TargetPosition), this.MovementProgress);
            if (this.MovementProgress >= 1.0F)
            {
                this.Position = this.OffsetReverse(this.TargetPosition);
                this.MovementState = MovementState.Idle;
                this.TargetDirection = Direction.Up;
            }
        }

        public void SetAi(int aiType)
        {
            this.AI = aiType;
        }
        public void UpdateAI()
        {
            if ((Main.IsPause == true || Main.IsDialog == true)) return;
            if (this is PlayerEntity == false)
            {
                if (this.ActionTime++ >= this.ActionTimeLimit)
                {
                    this.ActionTime = 0;
                    this.ActionTimeLimit = Random.Next(100, 200);
                    if (this.AI == AIType.LookAroundAI)
                    {
                        this.Direction = (Direction)Random.Next(0, 4);
                    }
                    if (this.AI == AIType.WalkAroundAI)
                    {
                        this.Direction = (Direction)Random.Next(0, 4);
                        Vector2 pos = this.Position + this.GetDirectionTarget(this.Direction);
                        this.TargetPosition = this.Offset(pos);
                    }
                }
            }
        }


        public bool NoAI => this.AI == AIType.NoAI;
        public Vector2 GetDirectionTarget(Direction facing)
        {
            return facing switch
            {
                Direction.Up => new Vector2(0, -Main.TileSize),
                Direction.Down => new Vector2(0, Main.TileSize),
                Direction.Left => new Vector2(-Main.TileSize, 0),
                Direction.Right => new Vector2(Main.TileSize, 0),
                _ => Vector2.Zero
            };
        }

        public virtual void HandleInput()
        {
        }

        public void Interacted(PlayerEntity player)
        {
            this.Interact(player);
        }
        public virtual void Interact(PlayerEntity player)
        {
            this.FacingAt(player);
            Main.TextDialog = this.Dialogs[this.DialogIndex];
            this.game.graphicRenderer.entity = this;
            if (Main.TextDialog.IsEmpty())
            {
                Main.TextDialog = null;
                this.DialogIndex = 0;
                this.Direction = this.WantedDirection;
                if (this.NpcType == NpcType.NonBattle)
                {
                    Main.GameState = GameState.Play;
                }   
                if (this.NpcType == NpcType.Battle)
                {
                    Main.SoundEngine.Play(AudioAssets.BattleBGM.Value);
                    this.game.SetScreen(new BattleScreen(this, player));
                    Main.GameState = GameState.Battle;
                }
                this.cooldownInteraction = 10;
            }
        }

        public virtual float RunningSpeed()
        {
            return this.isRunning == true ? 2.5F :  1.0F;
        }
        public void UpdateDetectionRange()
        {

        }
        public void UpdateFacingBox()
        {
            switch (this.Direction)
            {
                case Direction.Down:
                    {
                        int facingX = this.InteractionBox.X;
                        int facingY = (int)(this.InteractionBox.Y + this.FacingBoxH) + this.FacingBoxW;
                        this.FacingBox = new Rectangle(facingX, facingY, this.FacingBoxH, this.FacingBoxW);
                        //this.DetectionBox = new Rectangle(facingX, facingY, this.DetectionRangeWidth, this.DetectionRangeHeight + (32 * this.DetectionRange));
                        break;
                    }
                case Direction.Up:
                    {
                        int facingX = this.InteractionBox.X;
                        int facingY = (int)(this.InteractionBox.Y - this.FacingBoxW);
                        this.FacingBox = new Rectangle(facingX, facingY, this.FacingBoxH, this.FacingBoxW);
                        //this.DetectionBox = new Rectangle(facingX, facingY, this.DetectionRangeWidth, this.DetectionRangeHeight + (32 * this.DetectionRange));
                        break;
                    }
                case Direction.Left:
                    {
                        int facingX = (int)(this.InteractionBox.X - this.FacingBoxW);
                        int facingY = this.InteractionBox.Y;
                        this.FacingBox = new Rectangle(facingX, facingY, this.FacingBoxW, this.FacingBoxH);
                        //this.DetectionBox = new Rectangle(facingX, facingY, this.DetectionRangeWidth + (32 * this.DetectionRange), this.DetectionRangeHeight);
                        break;
                    }
                case Direction.Right:
                    {
                        int facingX = (int)(this.InteractionBox.X + this.FacingBoxH);
                        int facingY = this.InteractionBox.Y;
                        this.FacingBox = new Rectangle(facingX, facingY, this.FacingBoxW, this.FacingBoxH);
                        //this.DetectionBox = new Rectangle(facingX, facingY, this.DetectionRangeWidth + (32 * this.DetectionRange), this.DetectionRangeHeight);
                        break;
                    }
            }
        }
        public void MoveTo(Vector2 targetTile)
        {
            int x = this.Position.ToPoint().X + this.InteractionBox.X;
            int y = this.Position.ToPoint().Y + this.InteractionBox.Y;
            this.PathFind.SetNodes(targetTile, this);
            if (this.PathFind.Search() == true)
            {
                int nextX = x * Main.TileSize;
                int nextY = y * Main.TileSize;
                int targetX = ((PathNode)this.PathFind.PathList[0]).X;
                int targetY = ((PathNode)this.PathFind.PathList[0]).Y;
                int top = (int)this.GetY;
                int left = (int)this.GetX;
                int right = (int)(this.GetX + this.InteractionBox.Width);
                int bottom = (int)(this.GetY + this.InteractionBox.Height);
                if (top > nextY && left >= nextX)
                {
                    if (right < nextX + Main.TileSize)
                    {
                        this.Direction = Direction.Up;
                        return;
                    }
                }
                if (top < nextY && left >= nextX)
                {
                    if (right < nextX + Main.TileSize)
                    {
                        this.Direction = Direction.Down;
                        return;
                    }
                }

                if (top >= nextY)
                {
                    if (bottom < nextY + Main.TileSize)
                    {
                        if (left > nextX)
                        {
                            this.Direction = Direction.Left;
                        }
                        if (left < nextX)
                        {
                            this.Direction = Direction.Right;
                        }
                        return;
                    }
                }
                if (top > nextY && left > nextX)
                {
                    this.Direction = Direction.Up;
                    if (Main.Tiled.IsWalkable(new Vector2(nextX, nextY)) == true)
                    {
                        this.Direction = Direction.Left;
                    }
                } 
                else if (top > nextY && left < nextX)
                {
                    this.Direction = Direction.Up;
                    if (Main.Tiled.IsWalkable(new Vector2(nextX, nextY)) == true)
                    {
                        this.Direction = Direction.Right;
                    }
                }
                else if (top < nextY && left > nextX)
                {
                    this.Direction = Direction.Down;
                    if (Main.Tiled.IsWalkable(new Vector2(nextX, nextY)) == true)
                    {
                        this.Direction = Direction.Left ;
                    }
                }
                else if (top < nextY && left < nextX)
                {
                    this.Direction = Direction.Down;
                    if (Main.Tiled.IsWalkable(new Vector2(nextX, nextY)) == true)
                    {
                        this.Direction = Direction.Right;
                    }
                }
            }
        }
        public virtual void UpdateFacing()
        {

        }

        public void FollowTarget(NPC target, int distance, int interval)
        {
            if (this.GetDistance(target) < distance)
            {
                int i = Main.Random.Next(interval);
                if (i == 0)
                {
                    this.OnPath = true;
                }
            }

            if (this.GetDistance(target) > distance)
            {
                int i = Main.Random.Next(interval);
                if (i == 0)
                {
                    this.OnPath = false;
                }
            }
        }

        public double GetDistance(NPC target)
        {
            int xDistance = Math.Abs(this.InteractionBox.X - target.InteractionBox.X);
            int yDistance = Math.Abs(this.InteractionBox.Y - target.InteractionBox.Y);
            double var10000 = xDistance + yDistance;
            return var10000 / Main.TileSize;
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
        public void GetObjectInteracted(int id)
        {
            if (id == 999)
            {
                this.InteractedObject = null;
                return;
            }
            this.InteractedObject = Main.Objects[Main.MapIds][id];
        }

        public void GetNpcInteracted(int id)
        {
            if (id == 999)
            {
                this.InteractedObject = null;
                return;
            }
            this.InteractedNpc = Main.Npcs[Main.MapIds][id];
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
            switch (npc.Direction)
            {
                case Direction.Up:
                    {
                        this.Direction = Direction.Down; break;
                    }
                case Direction.Down:
                    {
                        this.Direction = Direction.Up; break;
                    }
                case Direction.Right:
                    {
                        this.Direction = Direction.Left; break;
                    }
                case Direction.Left:
                    {
                        this.Direction = Direction.Right; break;
                    }
            }
        }
        // Note: this will be the image of the npc when they engage in the battle
        // Make sure that image is present and complied to Content.mcgb other else will be error!
        public virtual Texture2D GetPortfolio()
        {
            return Asset<Texture2D>.Request($"Battle/Battler/{this.Portfolio}").Value;
        }
        public bool RenderDialogs() 
        {
            return !this.GetDialog().IsEmpty();
        }
        public void SetHealth(int health)
        {
            this.Health = health;
            this.MaxHealth = health;
        }
        public NPC Clone()
        {
            NPC objects = (NPC)this.MemberwiseClone();
            objects.Dialogs = new string[999];
            objects.DialogIndex = 0;
            return objects;
        }
    }
}
