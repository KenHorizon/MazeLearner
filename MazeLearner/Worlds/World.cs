
using System.Collections.Generic;
using System.Linq;

namespace MazeLearner.Worlds
{
    public enum WorldType
    {
        Outside,
        Indoor,
        Cave
    }
    public class World
    {
        private int _id;
        private string _name;
        private WorldType _worldType;
        public int Id { get { return _id; }  set { _id = value; } } 
        public string Name { get { return _name; }  set { _name = value; } } 
        public WorldType WorldType { get { return _worldType; }  set { _worldType = value; } }
        private static int id = 0;
        private static List<World> Maps = new List<World>();

        public World(string name, WorldType worldType)
        {
            this.Name = name;
            this.WorldType = worldType;
        }

        public static void Add(World world)
        {
            world.Id = World.CreateId();
            World.Maps.Add(world);
        }

        public static int Count => World.Maps.ToArray().Length;

        public void Clear()
        {
            World.Maps.Clear();
        }
        public static World Get(string name)
        {
            return World.Maps.FirstOrDefault(obj => obj.Name == name);
        }

        public static World Get(int id)
        {
            return World.Maps.FirstOrDefault(obj => obj.Id == id);
        }

        private static int CreateId()
        {
            return id++;
        }
    }
}