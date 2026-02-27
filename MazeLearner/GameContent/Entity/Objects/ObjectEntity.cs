using MazeLearner.Audio;
using MazeLearner.GameContent.Entity.Player;
using MazeLearner.Screen;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.GameContent.Entity.Objects
{
    public class ObjectEntity : NPC
    {
        private static List<ObjectEntity> GameObject = new List<ObjectEntity>();
        private static int ObjectId = 0;
        private EventMapTrigger _eventMapTrigger = EventMapTrigger.None;
        public EventMapTrigger EventMapTrigger
        {
            get { return _eventMapTrigger; }
            set { _eventMapTrigger = value; }
        }
        public ObjectEntity()
        {
            this.collisionBox = new Phys.CollisionBox(this.game);
        }
        public override void SetDefaults()
        {
            this.Width = 32;
            this.Height = 32;
            this.InteractionWidth = 32;
            this.InteractionHeight = 32;
            this.Direction = Direction.Down;
        }

        public static new ObjectEntity Get(int id)
        {
            return (ObjectEntity) GameObject[id].Clone();
        }

        private static int CreateObjectID()
        {
            return ObjectId++;
        }
        public virtual bool EntityStep(PlayerEntity entity)
        {
            return this.InteractionBox.Intersects(entity.InteractionBox);
        }


        public void SetupDialogs(int index, string message)
        {
            this.Dialogs[index] = message;
        }
        public static void Register(ObjectEntity objects)
        {
            objects.type = CreateObjectID();
            Loggers.Info($"Registered {objects.type} {objects.ToString()}");
            GameObject.Add(objects);
        }
        public new static List<ObjectEntity> GetAll => GameObject;
        public static int TotalObjects => GameObject.ToArray().Length;
    }
}
