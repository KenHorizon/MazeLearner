using MazeLearner.Audio;
using MazeLearner.GameContent.BattleSystems.Questions;
using MazeLearner.GameContent.BattleSystems.Questions.English;
using MazeLearner.GameContent.Entity.AI;
using MazeLearner.GameContent.Entity.Objects;
using MazeLearner.GameContent.Entity.Player;
using MazeLearner.GameContent.Phys;
using MazeLearner.Graphics.Animation;
using MazeLearner.Graphics.Asset;
using MazeLearner.Graphics.Particle;
using MazeLearner.Graphics.Particles;
using MazeLearner.Screen;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static Assimp.Metadata;

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
        private bool _defated;
        private bool _pause;
        public bool Defeated
        {
            get { return _defated; }
            set { _defated = value; }
        }
        public bool Pause
        {
            get { return _pause; }
            set { _pause = value; }
        }
        private bool _active;
        public bool Active
        {
            get { return _active; }
            set { _active = value; }
        }
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
        public Pathfinding Pathfinding
        {
            get;
            private set;
        }
        private List<Pathfinding.PathNode> currentPath = new List<Pathfinding.PathNode>();
        private int pathIndex;
        public int cooldownInteraction = 0;
        public int tick;
        public bool isRunning;
        public bool isMoving;
        public float MovementSpeed = 64.0F;
        public float MovementProgress;
        private NpcType _npctype = NpcType.NonBattle;
        private int _scorePointDrops = 0;
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
        private int _detectionRange = 0;
        public int DetectionRange
        {
            get { return _detectionRange; }
            set { _detectionRange = value; }
        }
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
        private int _interactedTime = 0;
        public int InteractedTime => _interactedTime;

        public NPC()
        {
            this.collisionBox = new CollisionBox(Main.Instance);
            this.animationState = new AnimationState(this);
        }
        

        public virtual void SetDefaults()
        {
            for (int i = 0; i < this.MaxHealth; i++)
            {
                var subject = new EnglishSubject(QuestionLevel.Medium);
                //if (subject.Question.TypeLevel == (QuestionLevel)Enum.ToObject(typeof(QuestionLevel), this.BattleLevel))
                //{
                //    subject.Randomized();
                //    this.Questionaire.Add(subject);
                //}

                this.Questionaire.Add(subject);
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
        public void SetupDialogs(int index, string message)
        {
            this.Dialogs[index] = message;
        }
        public static List<NPC> GetAll => NPCs;
        public static int TotalNpcs => NPCs.ToArray().Length;
        public virtual void Tick(GameTime gameTime)
        {
            this.tick++;
            if (this.cooldownInteraction > 0) this.cooldownInteraction--;

            if (this is ObjectEntity == false)
            {
                if (this.NoAI == false || this.IsRemove == false)
                {
                    this.PrevFacing = this.Direction;
                    this.PrevPosition = this.Position;
                    this.CollideOn = false;
                    switch (this.MovementState)
                    {
                        case MovementState.Idle:
                            {
                                this.animationState.Stop();
                                this.HandleInput();
                                break;
                            }
                        case MovementState.Walking:
                            {
                                this.animationState.Update();
                                this.UpdateMovement();
                                break;
                            }
                    }
                    this.UpdateHitboxes();
                    this.UpdateFacing();
                    this.UpdateAI();
                    this.GetNpcInteracted(this.collisionBox.CheckNpc(this, this is PlayerEntity));
                    this.GetObjectInteracted(this.collisionBox.CheckObject(this, this is PlayerEntity));
                    
                }
            }
        }

        public void ApplyMovement()
        {
            if (Main.Tiled.IsWalkable(this) == true)
            {
                this.StartMovement();
            }
            
        }

        public void StartMovement()
        {
            this.MovementState = MovementState.Walking;
            this.TargetDirection = this.Direction;
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
                this.MovementProgress = 0.0F;
                this.Position = this.OffsetReverse(this.TargetPosition);
                this.MovementState = MovementState.Idle;
                this.TargetDirection = Direction.Up;
                this.TargetPosition = Vector2.Zero;
                this.isMoving = false;
            }
        }

        public void SetAi(int aiType)
        {
            this.AI = aiType;
        }
        public void UpdateAI()
        {
            if (this.NoAI == true) return;
            if ((Main.IsPause == true || Main.IsDialog == true || Main.IsCutscene == true) ) return;

            if (this is PlayerEntity == false)
            {
                this.PathfindNodes();
                this.DetectedPlayer();
                if (this.ActionTime++ >= this.ActionTimeLimit)
                {
                    this.ActionTime = 0;
                    this.ActionTimeLimit = Random.Next(100, 200);
                    if (this.AI == AIType.LookAroundAI && this.isMoving == false)
                    {
                        this.Direction = (Direction)Random.Next(0, 4);
                    }
                    if (this.AI == AIType.WalkAroundAI)
                    {
                        this.Direction = (Direction)Random.Next(0, 4);
                        this.TargetPosition = this.Offset(this.Position + this.GetDirectionTarget());
                        this.ApplyMovement();
                    }
                }
            }
        }

        private void DetectedPlayer()
        {
            if (this.Defeated == false && this.DetectionBox.Contains(Main.ActivePlayer.InteractionBox))
            {
                this._interactedTime++;
                if (this._interactedTime == 1)
                {
                    this.MoveTo(Main.ActivePlayer);
                    Particle.Play(ParticleType.Exclamation, this.Position);
                }
                Main.ActivePlayer.InteractedNpc = this;
                Main.ActivePlayer.FacingAt(this);
                this.FacingAt(Main.ActivePlayer);
                if (this.Hitbox.Intersects(Main.ActivePlayer.InteractionBox))
                {
                    Main.GameState = GameState.Dialog;
                    this.Interacted(Main.ActivePlayer);
                    this.DialogueIndex++;
                }
            }
        }


        private void PathfindNodes()
        {
            try
            {
                if (this.currentPath.Count > 0 && this.tick % 20 == 0)
                {
                    for (int i = 0; i < this.currentPath.Count; i++)
                    {
                        var paths = this.currentPath[i];
                        if (i == pathIndex)
                        {
                            this.PathfindingMovement(paths.X * Main.TileSize, paths.Y * Main.TileSize);
                            Loggers.Info($"{paths.X}-{paths.Y}");
                        }
                    }
                    this.pathIndex++;
                    if (this.pathIndex >= this.currentPath.Count)
                    {
                        this.pathIndex = 0;
                        this.currentPath.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                Loggers.Error($"{ex}");
            }
        }
        //private void Move()
        //{
        //    if (Main.Pathfinding.Search() == true)
        //    {
        //        if (Main.Pathfinding.PathList.Count == 0)
        //        {
        //            Loggers.Debug($"Pathfinding list is zero!");
        //            return;
        //        }
        //        var nextNode = Main.Pathfinding.PathList[0];
        //        int nextX = nextNode.X * Main.TileSize;
        //        int nextY = nextNode.Y * Main.TileSize;
        //        int left = this.InteractionBox.Left;
        //        int right = this.InteractionBox.Right;
        //        int top = this.InteractionBox.Top;
        //        int bottom = this.InteractionBox.Bottom;
        //        //Loggers.Debug($"nextX {nextX} nextY {nextY} {nextNode.Col} {nextNode.Row}");
        //        //Loggers.Debug($"nextX {nextX} nextY {nextY} | Box | Left: {left} Right: {right} Top: {top} Bottom: {bottom}");
        //        if (top > nextY && left >= nextX && right == nextX + Main.TileSize)
        //        {
        //            this.Direction = Direction.Up;
        //            PathfindingMovement(nextX, nextY);
        //        }
        //        if (top < nextY && left >= nextX && right == nextX + Main.TileSize)
        //        {
        //            this.Direction = Direction.Down;
        //            PathfindingMovement(nextX, nextY);
        //        }

        //        if (top >= nextY && bottom == nextY + Main.TileSize)
        //        {
        //            if (left > nextX)
        //            {
        //                this.Direction = Direction.Left;
        //                PathfindingMovement(nextX, nextY);
        //            }

        //            if (left < nextX)
        //            {
        //                this.Direction = Direction.Right;
        //                PathfindingMovement(nextX, nextY);
        //            }
        //        }
        //    }
        //}

        private void PathfindingMovement(int nextX, int nextY)
        {
            this.TargetPosition = new Vector2(nextX, nextY);
            this.ApplyMovement();
        }


        public void MoveTo(NPC npc)
        {
            this.MoveTo(this.Offset(npc.Position - npc.GetDirectionTarget()));
        }
        public void MoveTo(int x, int y)
        {
            this.MoveTo(new Vector2(x * Main.TileSize, y * Main.TileSize));
        }
        public void MoveTo(Vector2 targetPosition)
        {
            this.WantedPosition = this.Offset(targetPosition);
            Main.Pathfinding.SetNodes(this.Offset(this.Position), this.WantedPosition);
            if (Main.Pathfinding.Search() == true)
            {
                this.currentPath = Main.Pathfinding.PathList.ToList();
                this.pathIndex = 0;
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

        public Vector2 GetDirectionTarget()
        {
            return this.GetDirectionTarget(this.Direction);
        }

        public virtual void HandleInput()
        {
        }

        public void Interacted(PlayerEntity player)
        {
            if (this.NpcType == NpcType.Battle && this.Defeated == true) return;
            this.Interact(player);
        }
        public virtual void Interact(PlayerEntity player)
        {
            this.FacingAt(player);
            Main.TextDialog = this.Dialogs[Main.TextDialogueIndex];
            if (Main.TextDialog.IsEmpty() == true)
            {
                player.Pause = false;
                Main.TextDialog = null;
                this.DialogueIndex = 0;
                Main.TextDialogueIndex = 0;
                this.Direction = this.WantedDirection;
                if (this.NpcType == NpcType.NonBattle)
                {
                    Main.GameState = GameState.Play;
                }
                if (this.NpcType == NpcType.Battle && this.Defeated == false)
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
        private void UpdateHitboxes()
        {
            switch (this.Direction)
            {
                case Direction.Down:
                    {
                        int facingX = this.InteractionBox.X;
                        int facingY = (int)(this.InteractionBox.Y + this.HitboxH);
                        this.Hitbox = new Rectangle(facingX, facingY, this.HitboxH, this.HitboxW);
                        this.TargetHitbox = new Rectangle(
                            (int)this.TargetPosition.X,
                            (int)this.TargetPosition.Y,
                            this.HitboxH, this.HitboxW);
                        this.TargetInteractionBox = new Rectangle(facingX, this.InteractionBox.Y + Main.TileSize, Main.TileSize, Main.TileSize);
                        if (this.DetectionRange > 0)
                        {
                            //this.DetectionBox = new Rectangle(facingX, facingY,
                            //    this.DetectionRangeWidth, (Main.TileSize * this.DetectionRange));
                            for (int i = 0; i < this.DetectionRange; i++)
                            {
                                this.DetectionBox = new Rectangle(facingX, facingY, this.DetectionRangeWidth, (Main.TileSize * i));
                                if (Main.Tiled.IsWalkable(this.DetectionBox) == false) break;
                            }
                        }
                        break;
                    }
                case Direction.Up:
                    {
                        int facingX = this.InteractionBox.X;
                        int facingY = (int)(this.InteractionBox.Y - this.HitboxW);
                        this.Hitbox = new Rectangle(facingX, facingY, this.HitboxH, this.HitboxW);
                        this.TargetHitbox = new Rectangle(
                            (int)this.TargetPosition.X,
                            (int)this.TargetPosition.Y,
                            this.HitboxH, this.HitboxW);
                        this.TargetInteractionBox = new Rectangle(facingX, this.InteractionBox.Y - Main.TileSize, Main.TileSize, Main.TileSize);
                        if (this.DetectionRange > 0)
                        {
                            //this.DetectionBox = new Rectangle(facingX, facingY - (Main.TileSize * this.DetectionRange) + 4,
                            //    this.DetectionRangeWidth, (Main.TileSize * this.DetectionRange));
                            for (int i = 0; i < this.DetectionRange; i++)
                            {
                                this.DetectionBox = new Rectangle(facingX, facingY - (Main.TileSize * i) + 4, this.DetectionRangeWidth, (Main.TileSize * i));
                                if (Main.Tiled.IsWalkable(this.DetectionBox) == false) break;
                            }
                        }
                        break;
                    }
                case Direction.Left:
                    {
                        int facingX = (int)(this.InteractionBox.X - this.HitboxW);
                        int facingY = this.InteractionBox.Y;
                        this.Hitbox = new Rectangle(facingX, facingY, this.HitboxW, this.HitboxH);
                        this.TargetHitbox = new Rectangle(
                            (int)this.TargetPosition.X,
                            (int)this.TargetPosition.Y,
                            this.HitboxW, this.HitboxH);
                        this.TargetInteractionBox = new Rectangle(this.InteractionBox.X - Main.TileSize, facingY, Main.TileSize, Main.TileSize);
                        if (this.DetectionRange > 0)
                        {
                            //this.DetectionBox = new Rectangle(facingX - (Main.TileSize * this.DetectionRange), facingY,
                            //    (Main.TileSize * this.DetectionRange), this.DetectionRangeHeight);
                            for (int i = 0; i < this.DetectionRange; i++)
                            {
                                this.DetectionBox = new Rectangle(facingX - (Main.TileSize * i), facingY, this.DetectionRangeWidth, (Main.TileSize * i));
                                if (Main.Tiled.IsWalkable(this.DetectionBox) == false) break;
                            }
                        }
                        break;
                    }
                case Direction.Right:
                    {
                        int facingX = (int)(this.InteractionBox.X + this.HitboxH);
                        int facingY = this.InteractionBox.Y;
                        this.Hitbox = new Rectangle(facingX, facingY, this.HitboxW, this.HitboxH);
                        this.TargetHitbox = new Rectangle(
                            (int)this.TargetPosition.X,
                            (int)this.TargetPosition.Y,
                            this.HitboxW, this.HitboxH);
                        this.TargetInteractionBox = new Rectangle(this.InteractionBox.X + Main.TileSize, facingY, Main.TileSize, Main.TileSize);
                        if (this.DetectionRange > 0)
                        {
                            //this.DetectionBox = new Rectangle(facingX, facingY,
                            //   (Main.TileSize * this.DetectionRange), this.DetectionRangeHeight);
                            for (int i = 0; i < this.DetectionRange; i++)
                            {
                                this.DetectionBox = new Rectangle(facingX, facingY,
                                   (Main.TileSize * i), this.DetectionRangeHeight);
                                if (Main.Tiled.IsWalkable(this.DetectionBox) == false) break;
                            }
                        }
                        break;
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
                this.InteractedNpc = null;
                return;
            }
            this.InteractedNpc = Main.Npcs[Main.MapIds][id];
        }

        public string GetDialog()
        {
            var getdialog = this.Dialogs[this.DialogueIndex];
            if (this.Dialogs[this.DialogueIndex].IsEmpty())
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
            objects.DialogueIndex = 0;
            return objects;
        }
    }
}
