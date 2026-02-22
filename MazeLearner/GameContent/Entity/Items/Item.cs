using MazeLearner.Audio;
using MazeLearner.GameContent.Entity.Player;
using MazeLearner.Localization;
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
        public int type;
        public int id;
        public string idName;
        public int stack;
        public int maxStack;
        public Item(string name)
        {
            this.idName = $"Items_{type}";
            this.langName = name;
        }

        public int GetItemType => this.type;
        public int GetItemId => this.id;

        private string _displayName = "???";
        public string DisplayName
        {
            get
            {
                return _displayName;
            }
             set
            {
                _displayName = value; 
            }
        }

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
            item.type = CreateId();
            item.SetDefaults(item.type);
            Item.Items.Add(item);
        }
        public static int CreateId()
        {
            return ItemIndex++; 
        }
        public virtual void SetDefaults(int type)
        {
            if (type == 0)
            {
                this.DisplayName = Resources.AirItem;
                this.maxStack = 1;
            }
            if (type == 1)
            {
                this.DisplayName = Resources.HealthPotion;
                this.maxStack = 255;
            }
            if (type == 2)
            {
                this.DisplayName = Resources.BasicSword;
                this.maxStack = 1;
            }
        }

        public virtual void OnUseItem(int type, PlayerEntity player)
        {
            if (type == 0)
            {

            }
            if (type == 1)
            {
                Main.SoundEngine.Play(AudioAssets.FallSFX.Value);
                player.Health += 5;
            }
            if (type == 2)
            {
                player.Damage += 1;
            }
        }
        public virtual void SetModifiers(int type, PlayerEntity player)
        {
            if (type == 0)
            {

            }
            if (type == 2)
            {
                player.TempDamage = 1;
            }
        }
    }
}
