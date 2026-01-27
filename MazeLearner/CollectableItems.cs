using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner
{
    public class CollectableItems
    {
        public static List<CollectableItems> CollectableItem = new List<CollectableItems>();
        private string _name;
        private string _description;
        private int _id;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        public CollectableItems(int id, string name, string desc) 
        {
            this.Id = id;
            this._name = name;
            this._description = desc;
        }
        public static CollectableItems Create(int id, string name, string desc)
        {
            return new CollectableItems(id, name, desc);
        }
        public override string ToString()
        {
            return $"Collectables: {this.Id} {this.Name} {this.Description}";
        }
    }
}
