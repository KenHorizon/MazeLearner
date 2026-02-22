using MazeLearner.Audio;
using MazeLearner.GameContent.Entity.Player;
using MazeLearner.Screen;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.GameContent.Entity.Objects
{
    public abstract class ObjectEntity : BaseEntity, InteractableNPC
    {
        private static List<ObjectEntity> GameObject = new List<ObjectEntity>();
        private static int ObjectId = 0;
        public ObjectEntity InteractedNpc { get; set; }
        public int DialogIndex = 0;
        public string[] Dialogs = new string[999];
        private EventMapTrigger _eventMapTrigger = EventMapTrigger.None;
        public EventMapTrigger EventMapTrigger
        {
            get { return _eventMapTrigger; }
            set { _eventMapTrigger = value; }
        }
        public void SetDefaults()
        {
            this.Width = 32;
            this.Height = 32;
            this.InteractionWidth = 32;
            this.InteractionHeight = 32;
        }

        public static ObjectEntity Get(int id)
        {
            return GameObject[id].Clone();
        }

        public ObjectEntity Clone()
        {
            return (ObjectEntity)this.MemberwiseClone();
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

            if (this.Dialogs[this.DialogIndex].IsEmpty())
            {
                Main.GameState = GameState.Play;
                this.DialogIndex = 0;
            }
        }

        public static List<ObjectEntity> GetAll => GameObject;
        public static int TotalObjects => GameObject.ToArray().Length;
    }
}
