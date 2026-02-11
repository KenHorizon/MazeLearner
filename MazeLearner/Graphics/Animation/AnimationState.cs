using MazeLearner.GameContent.Entity;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.Graphics.Animation
{
    public class AnimationState
    {
        public NPC npc;
        public int frames = 0;
        public int totalFrames = 1;
        public const int defaultFrame = 1;
        private float animationTimer = 0;
        private const float FrameTime = 0.15F;
        public AnimationState(NPC npc) 
        { 
            this.npc = npc;
        }

        public void Update()
        {
            int imageW = npc.GetTexture().Value.Width;
            int imageH = npc.GetTexture().Value.Height;
            int sizeW = npc.Width;
            int sizeH = npc.Height;
            this.totalFrames = imageW / sizeW;
            this.animationTimer += Main.Instance.DeltaTime;
            if (this.animationTimer >= FrameTime)
            {
                this.frames = (this.frames + 1) % this.totalFrames;
                this.animationTimer = 0;
            }
        }
        public void Stop()
        {
            this.frames = 0;
            this.animationTimer = 0;
        }
    }
}
