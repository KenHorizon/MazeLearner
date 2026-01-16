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
        public string langName;
        public int id;
        public int maxStack;
        public static int ItemId = 0;
        public Item(String langName)
        {
            this.langName = langName;
            this.id = Item.RegistryItemId();
            this.SetDefaults();
        }
        private static int RegistryItemId()
        {
            return ItemId++;
        }

        public bool IsEmptyOrNull()
        {
            return this == null;
        }
        public virtual void SetDefaults() { }

        public virtual void OnUseItem(PlayerEntity player) { }
    }
}
