using MazeLearner.GameContent.Entity.Player;
using MazeLearner.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.GameContent.Entity.Items
{
    public class ItemBuilder
    {
        private static int ID;

        public static void Register()
        {
            Item.Add(new Item(CreateId(), Resources.AirItem));
            Item.Add(new Item(CreateId(), Resources.HealthPotion));
            Item.Add(new Item(CreateId(), Resources.BasicSword));
        }

        private static int CreateId()
        {
            return ID++;
        }
    }
}
