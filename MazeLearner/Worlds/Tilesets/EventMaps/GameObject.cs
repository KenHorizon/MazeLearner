using Microsoft.Xna.Framework;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.Worlds.Tilesets.EventMaps
{
    public static class ObjectDatabase
    {
        private static List<GameObject> objectsById = new List<GameObject>();

        public static void Register(GameObject obj)
        {
            objectsById.Add(obj);
        }

        public static GameObject Get(EventMapId id)
        {
            return objectsById.FirstOrDefault(obj => int.Parse(obj.Get("EventMap").value) == (int) id);
        }
        public static void Clear()
        {
            objectsById.Clear();
        }
        public static List<GameObject> GetAll => objectsById;
    }
    public class GameObject
    {
        private Dictionary<string, TiledProperty> properties = new Dictionary<string, TiledProperty>();
        public Rectangle Bounds { get; private set; }
        public void AddProperty(TiledProperty prop)
        {
            this.properties[prop.name] = prop;
        }

        public TiledProperty Get(string name)
        {
            return properties.TryGetValue(name, out var prop) ? prop : null;
        }
        public void BuildBounds(int x, int y, int tileSize = 32)
        {
            this.Bounds = new Rectangle(x, y, tileSize, tileSize);
        }
        public override string ToString()
        {
            StringBuilder bld = new StringBuilder();
            foreach (var p in properties)
            {
                bld.Append($"Name: {p.Value.name} [{p.Value.type.ToString()}] - {p.Value.value} ");
            }
            return $"{bld.ToString()}";
        }
    }
}
