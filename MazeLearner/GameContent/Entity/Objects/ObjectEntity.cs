using MazeLearner.GameContent.Entity.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.GameContent.Entity.Objects
{
    public abstract class ObjectEntity : BaseEntity, InteractableNPC
    {
        private static List<ObjectEntity> GameObject = new List<ObjectEntity>();
        private static int ObjectId = 0;
        
        public void SetDefaults()
        {
            this.Width = 32;
            this.Height = 32;
            this.InteractionWidth = 32;
            this.InteractionHeight = 32;
        }

        public static ObjectEntity Get(int ncpId)
        {
            return GameObject[ncpId];
        }
        private static int CreateObjectID()
        {
            return ObjectId++;
        }
        public virtual void Tick(GameTime gameTime)
        {
        }

        public virtual bool EntityStep(PlayerEntity entity)
        {
            return this.InteractionBox.Intersects(entity.InteractionBox);
        }

        public static void Register(ObjectEntity objects)
        {
            objects.type = CreateObjectID();
            objects.SetDefaults();
            Loggers.Info($"Registeered {objects.type}");
            GameObject.Add(objects);
        }

        public void Interacted(PlayerEntity player)
        {
            this.Interact(player);
        }

        public virtual void Interact(PlayerEntity player)
        {

        }

        public static List<ObjectEntity> GetAll => GameObject;
        public static int TotalObjects => GameObject.ToArray().Length;
    }
}
