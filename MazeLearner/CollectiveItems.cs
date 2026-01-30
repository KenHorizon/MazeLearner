using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner
{
    public class CollectiveItems
    {
        public static List<CollectiveItems> CollectableItem = new List<CollectiveItems>();
        private string _idName;
        private string _name;
        private string _description;
        private int _id;

        public string IdName
        {
            get { return _idName; }
            set { _idName = value; }
        }
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
        public CollectiveItems(int id, string name, string desc) 
        {
            this.Id = id;
            this.Name = name;
            this.IdName = $"Collective_{id}";
            this.Description = desc;
        }
        public static CollectiveItems Create(int id, string name, string desc)
        {
            return new CollectiveItems(id, name, desc);
        }
        public override string ToString()
        {
            return $"Collective: {this.Id} {this.Name} {this.Description}";
        }
    }
}
