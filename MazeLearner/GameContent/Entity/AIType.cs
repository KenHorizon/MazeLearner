using MazeLearner.GameContent.Phys;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json.Linq;
using System;

namespace MazeLearner.GameContent.Entity
{
    public static class AIType
    {
        public static int NoAI = AIType.CreateID(); // 0
        public static int StationaryAI = AIType.CreateID();
        public static int LookAroundAI = AIType.CreateID();
        public static int WalkAroundAI = AIType.CreateID();
        public static int MoveToPlayer = AIType.CreateID();

        private static int AITypeIDs = 0;

        private static int CreateID()
        {
            return AIType.AITypeIDs++;
        }
    }
}