using MazeLearner.GameContent.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.GameContent
{
    public class Objective
    {
        public static Objective Empty = Objective.Get(0);
        private static int _createId = 0;
        private int _ID;
        private string _name;
        private string _description;
        private bool _checked;

        public int ID { get { return _ID; } private set { _ID = value; } }
        public string Name { get { return _name; } private set { _name = value; } }
        public string Description { get { return _description; } private set { _description = value; } }

        public bool Checked { get {return _checked; } set { _checked = value; } }

        public static List<Objective> Registered = new List<Objective>();
        
        public void SetDefaults()
        {
            if (this.ID == 0)
            {
                this.Name = "...";
                this.Description = "...";
            }
            if (this.ID == 1)
            {
                this.Name = "School Day";
                this.Description = "Find the bus terminal and go to school";
            }
            if (this.ID == 2)
            {
                this.Name = "Classroom";
                this.Description = "Find your Classroom (5 - Pearls)";
            }

            if (this.ID == 3)
            {
                this.Name = "The Maze";
                this.Description = "Find the exit and discover what happened";
            }

            if (this.ID == 4)
            {
                this.Name = "Door is Block!";
                this.Description = "Find the Book and Answer all question to open the door";
            }
            if (this.ID == 5)
            {
                this.Name = "Seat";
                this.Description = "Find your seat";
            }
        }
        private static int CreateId()
        {
            return _createId++; 
        }
        public static void Add(Objective obj) 
        {
            obj.ID = CreateId();
            obj.SetDefaults();
            Registered.Add(obj);
        }

        public static void Remove(Objective obj) { Registered.Remove(obj); }

        public static int Count => Registered.Count;

        public static void Clear() { Registered.Clear(); }

        public static Objective Get(int id)
        {
            return Registered[id].Clone(); 
        }
        public Objective Clone()
        {
            var objectives = (Objective)this.MemberwiseClone();
            return objectives;
        }
    }
}
