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
        public Main game;
        private int _scene;
        private int _timer;
        private bool _isFinished;
        private Action _onStart;
        private Action _onTick;
        private Action _onEnd;
        public int Scene
        {
            get {  return _scene; }
            set { _scene = value; }
        }
        public int Timer
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

        public Cutscene(Main game)
        {
            this.game = game;
        }

        public static void AddCutscene(Cutscene cutscene)
        {
            Cutscenes.Add(cutscene);
        }
        public static Cutscene Get(int id)
        {
            return (Cutscene)Cutscenes[id].MemberwiseClone();   
        }

        public void PlayIntro()
        {

        }

        private static int CreateId()
        {
            return CutsceneId++;
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch sprite)
        {

        }
    }
}
