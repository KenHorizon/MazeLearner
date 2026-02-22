using MazeLearner.GameContent.Entity.Player;
using MazeLearner.Graphics;
using MazeLearner.Localization;
using MazeLearner.Screen.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.Screen
{
    public class InventoryScreen : BaseScreen
    {
        private int boxPadding = 32;
        private int boxX = 0;
        private int boxY = 20;
        private int boxW
        {
            get
            {
                return this.game.ScreenWidth;
            }
            set
            {
                this.boxW = value;
            }
        }
        private int boxH
        {
            get
            {
                return this.game.ScreenHeight;
            }
            set
            {
                this.boxH = value;
            }
        }
        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle(this.boxX + boxPadding, this.boxY, boxW - (boxX + (boxPadding * 2)), boxH - (boxY + boxPadding));
            }
            set
            {
                this.boxX = value.X;
                this.boxY = value.Y;
                this.boxW = value.Width;
                this.boxH = value.Height;
            }
        }
        private const int _itemSlotPadding = 40;
        private int itemSlotW
        {
            get
            {
                return this.BoundingBox.Width - _itemSlotPadding;
            }
            set
            {
                this.itemSlotW = value;
            }
        }
        private int itemSlotH
        {
            get
            {
                return 120;
            }
            set
            {
                this.itemSlotH = value;
            }
        }
        private int itemSlotX = 0;
        private int itemSlotY = 20;
        public Rectangle ItemSlotBox
        {
            get
            {
                return new Rectangle(this.itemSlotX + ((this.BoundingBox.Width - this.itemSlotW) / 2), this.itemSlotY, itemSlotW - (itemSlotX + (_itemSlotPadding * 2)), this.itemSlotH - _itemSlotPadding);
            }
            set
            {
                this.itemSlotX = value.X;
                this.itemSlotY = value.Y;
                this.itemSlotW = value.Width;
                this.itemSlotH = value.Height;
            }
        }
        private Dictionary<int, Rectangle> Items = new Dictionary<int, Rectangle>();
        private PlayerEntity _player;
        public PlayerEntity Player => _player;
        public InventoryScreen(PlayerEntity player) : base(Resources.Inventory)
        {
            this._player = player;
        }
        public override void LoadContent()
        {
            base.LoadContent();
            for (int i = 0; i < Main.GetActivePlayer.Inventory.Length; i++)
            {
                if (Main.GetActivePlayer.Inventory[i] != null)
                {
                    this.Items[i] = this.ItemSlotBox;
                    Rectangle entryBox = new Rectangle(this.ItemSlotBox.X + 20, this.ItemSlotBox.Y + 20, this.ItemSlotBox.Width, this.ItemSlotBox.Height);
                    this.EntryMenus.Add(new MenuEntry(i, Main.GetActivePlayer.Inventory[i].DisplayName, entryBox, () =>
                    {
                    }));
                    this.itemSlotY += 120;
                }
            }
        }
        public override void Update(GameTime gametime)
        {
            base.Update(gametime);
            if (Main.Input.Pressed(GameSettings.KeyBack))
            {
                this.game.SetScreen(new BagScreen());
            }
            for (int i = 0; i < Main.GetActivePlayer.Inventory.Length; i++)
            {
                if (Main.GetActivePlayer.Inventory[i] != null)
                {
                    Main.GetActivePlayer.Inventory[i].OnUseItem(Main.GetActivePlayer.Inventory[i].GetItemType, this.Player);
                }
            }
        }

        public override void Render(SpriteBatch sprite, Graphic graphic)
        {
            
        }

        public override void RenderBackground(SpriteBatch sprite, Graphic graphic)
        {
            base.RenderBackground(sprite, graphic);
            int row = 0;
            int col = 0;
            int padding = 72;
            int x = this.BoundingBox.X + 10;
            int y = this.BoundingBox.Y + 10;
            sprite.NinePatch(AssetsLoader.Box1.Value, this.BoundingBox, Color.White, 32);
        }
    }
}
