using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.Graphics
{
    public class Cutscene
    {
        protected Main game;
        private int _scene = 0;
        private double _timer = 0;
        private int _nextScene = 2; // 2 seconds
        private bool _isFinished = false;
        public int type;

        public int Scene
        {
            get {  return _scene; }
            set { _scene = value; }
        }
        public int NextScene
        {
            get { return _nextScene; }
            set { _nextScene = value; }
        }
        public double Timer
        {
            get { return _timer; }
            set { _timer = value; }
        }
        public bool IsFinished
        {
            get { return _isFinished; }
            set { _isFinished = value; }
        }
        private static int CutsceneId = 0;
        private static List<Cutscene> Cutscenes = new List<Cutscene>();

        public static void AddCutscene(Cutscene cutscene)
        {
            cutscene.type = CreateId();
            Cutscenes.Add(cutscene);
        }
        public static Cutscene Get(int id)
        {
            return (Cutscene)Cutscenes[id].MemberwiseClone();   
        }

        private static int CreateId()
        {
            return CutsceneId++;
        }

        public virtual void Update(GameTime gameTime)
        {
            this.Timer += gameTime.ElapsedGameTime.TotalSeconds;
            if (Main.Confirm == true && this.Timer > this.NextScene)
            {
                this.Scene++;
                this.Timer = 0;
            }
        }

        public virtual void Draw(SpriteBatch sprite, Graphic graphic)
        {

        }

        public void Start()
        {
            Main.CurrentScene = this;
        }
    }
}
