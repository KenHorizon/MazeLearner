using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.GameContent.Entity.Objects
{
    public class ObjectEntity : NPC
    {
        private static List<ObjectEntity> GameObject = new List<ObjectEntity>();
        private static int ObjectId = 0;
        public override void SetDefaults()
        {
            base.SetDefaults();
            this.Facing = Facing.Down;
            this.AI = AIType.StationaryAI;
        }
        public static ObjectEntity Get(int ncpId)
        {
            return GameObject[ncpId];
        }
        private static int CreateObjectID()
        {
            return ObjectId++;
        }
        public static void Register(ObjectEntity objects)
        {
            objects.whoAmI = CreateObjectID();
            GameObject.Add(objects);
        }
        public static List<ObjectEntity> GetAll => GameObject;
        public static int TotalObjects => GameObject.ToArray().Length;
        public override Asset<Texture2D> GetTexture()
        {
            return null;
        }
    }
}
