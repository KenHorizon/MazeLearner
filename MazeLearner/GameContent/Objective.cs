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
                this.Name = "First day School";
                this.Description = "Go to Terminal";
            }
            if (this.ID == 1)
            {
                this.Name = "Classroom";
                this.Description = "Find your classroom";
            }

            if (this.ID == 2)
            {
                this.Name = "The Maze";
                this.Description = "Explore the maze and defeat the first boss";
            }
        }
        private static int CreateId()
        {
            return _createId++; 
        }
        public static void Add(Objective obj) 
        {
            obj.ID = CreateId();
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
