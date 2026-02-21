using MazeLearner.GameContent.Entity;
using MazeLearner.Graphics.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.Graphics.Particle
{
    public class Particle
    {
        private static List<Particle> _particles = new List<Particle>();
        internal int whoAmI;
        internal int type;
        private bool _stayed;
        private int _lifespan;
        private int _tick;
        private bool _active;
        private int Width;
        private int Height;
        public int frames = 0;
        public int totalFrames = 1;
        public const int defaultFrame = 1;
        private float animationTimer = 0;
        private const float FrameTime = 0.15F;
        private static int particleType = 0;
        private Vector2 Position;
        private Vector2 Size;
        public Rectangle DrawingBox
        {
            get
            {
                return new Rectangle((int) this.Position.X, (int)this.Position.Y, 32, 32);
            }
            set
            {
                this.Position = new Vector2((value.X * 32) - 32 / 2, (value.Y * 32) - 32 / 2);
                this.Size = new Vector2(value.Width, value.Height);
            }
        }
        public bool Active
        {
            get { return _active; } 
            set { _active = value; }
        }
        public bool Stayed
        {
            get { return _stayed; }
            set { _stayed = value; }
        }
        public int Lifespan
        {
            get { return _lifespan; }
            set { _lifespan = value; }
        }
        public int Tick
        {
            get { return _tick; }
            set { _tick = value; }
        }
        private Texture2D _sheet;
        public Texture2D SheetTexture
        {
            get { return _sheet; }
            set { _sheet = value; }
        }
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public Particle(string name)
        {
            this.Name = name;
        }

        public virtual void SetDefaults(int type, bool overrides = false)
        {
            this.Width = 32;
            this.Height = 32;
            if (type == ParticleType.Grass)
            {
                this.Lifespan = 40;
                this.Stayed = true;
            }
            if (type == ParticleType.Ripple)
            {
                this.Lifespan = 20;
                this.Stayed = false;
            }
            if (type == ParticleType.MudRipple)
            {
                this.Lifespan = 20;
                this.Stayed = false;
            }
            if (type == ParticleType.Dust)
            {
                this.Lifespan = 60;
                this.Stayed = false;
            }
        }
        public void SetPos(int x, int y)
        {
            this.Position = new Vector2((x * 32) - 32 / 2, (y * 32) - 32 / 2);
        }
        public static void Register(Particle particle)
        {
            particle.type = CreateId();
            Particle._particles.Add(particle);
        }

        public static int CreateId()
        {
            return particleType++;
        }

        public static Particle Get(int particleType)
        {
            return (Particle) Particle._particles[particleType].MemberwiseClone();
        }


        public virtual void Update(GameTime gameTime)
        {
            int imageW = Main.ParticleTexture[this.whoAmI].Width;
            int imageH = Main.ParticleTexture[this.whoAmI].Height;
            int sizeW = this.Width;
            int sizeH = this.Height;
            this.totalFrames = imageW / sizeW;
            this.animationTimer += Main.Instance.DeltaTime;
            if (this.animationTimer >= FrameTime)
            {
                this.frames = (this.frames + 1) % this.totalFrames;
                this.animationTimer = 0;
            }
            if (this.Tick++ > this.Lifespan)
            {
                if (this.Stayed == false)
                {
                    this.Active = false;
                }
            }
        }
        public virtual void Draw(SpriteBatch sprite)
        {
            int w = this.frames * this.Width;
            int h = this.Height;
            Rectangle destSprites = new Rectangle(w, h, this.Width, this.Height);
            Main.SpriteBatch.Draw(Main.ParticleTexture[this.type], this.DrawingBox, destSprites, Color.White);
        }

        public static int GetCount => Particle._particles.Count;
        public static void Play(int particleType, Vector2 pos)
        {
            Play(particleType, (int) pos.X, (int)pos.Y);
        }
        public static void Play(int particleType, int x, int y)
        {
            var part = Particle._particles[particleType];
            part.SetPos(x, y);
            Main.AddParticle(part);
        }
    }
}
