using MazeLearner.GameContent.Entity.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.GameContent.Entity.Items
{
    public class Item
    {
        public static List<Item> Items = new List<Item>();

        private static int ItemIndex = 0;
        public string langName;
        public int id;
        public string idName;
        public int maxStack;
        public Item(int id, string name)
        {
            this.id = id;
            this.idName = $"Items_{id}";
            this.langName = name;
            this.SetDefaults();
        }

        public int GetItemId => this.id;

        public Item Get(int id)
        {
            return Items[id];
        }

        public bool IsEmptyOrNull()
        {
            return this == null;
        }
        public static void Add(Item item)
        {
            Item.Items.Add(item);
        }
        public virtual void SetDefaults()
        {

        }

        public virtual void OnUseItem(int id, PlayerEntity player)
        {
        }
    }
}
