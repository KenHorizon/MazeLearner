using MazeLeaner.Text;
using MazeLearner.Graphics;
using MazeLearner.Screen.Widgets;
using MazeLearner.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.Screen
{
    public class CollectiveScreen : BaseScreen
    {
        public record CollectiveEntry(int index, Texture2D icons, string name, string desc);
        public int collectiveIndex { get; set; }
        private CollectiveEntry NAN = new CollectiveEntry(999, Asset<Texture2D>.Request("Collective/Collective_Nan").Value, "???", "???");
        private List<CollectiveEntry> collectiveEntries = new List<CollectiveEntry>();
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
        private SimpleButton BackButton;
        public CollectiveScreen() : base("")
        {
        }

        public override void LoadContent()
        {
            int scale = 1;
            int w = 240 * scale;
            int h = 40 * scale;
            this.posX = this.boxX + w;
            this.posY = this.boxY;
            int entryMenuY = this.posY;
            //this.BackButton = new SimpleButton(this.posX, entryMenuY, w, h, () =>
            //{
            //    this.game.SetScreen(new TitleScreen(TitleSequence.Title));
            //});
            //this.BackButton.Text = "Back";
            for(int i = 0; i < Main.Collective.Length; i++)
            {
                this.collectiveEntries.Add(new CollectiveEntry(i, Asset<Texture2D>.Request($"Collective/{CollectiveItems.CollectableItem[i].IdName}").Value, CollectiveItems.CollectableItem[i].Name, CollectiveItems.CollectableItem[i].Description));
            }
            //this.AddRenderableWidgets(BackButton);
        }
        public override void Update(GameTime gametime)
        {
            base.Update(gametime);
            if (Main.Input.Pressed(GameSettings.KeyBack))
            {
                this.game.SetScreen(new TitleScreen(TitleSequence.Title));
            }
        }
        public override void RenderBackground(SpriteBatch sprite, Graphic graphic)
        {
            base.RenderBackground(sprite, graphic);
            this.game.RenderBackground(sprite);
            int row = 0;
            int col = 0;
            int padding = 72;
            int x = this.BoundingBox.X + 10;
            int y = this.BoundingBox.Y + 10;
            sprite.NinePatch(AssetsLoader.Box1.Value, this.BoundingBox, Color.White, 32);
            for (int i = 0; i < this.collectiveEntries.ToArray().Length; i++)
            {
                var entry = this.collectiveEntries[i];
                var acquired = Main.CollectiveAcquired[i];
                bool flag = acquired == true;
                Texture2D icons = flag ? entry.icons : NAN.icons;
                string name = flag ? entry.name : NAN.name;
                string desc = flag ? entry.desc : NAN.desc;
                sprite.Draw(AssetsLoader.PanelBox.Value, new Rectangle(x, y, 64, 64), Color.White);
                sprite.Draw(icons, new Rectangle(x, y, 64, 64));
                Texts.Text(Fonts.Text, name, new Vector2(x + padding, y), Color.White);
                Texts.Text(Fonts.Text, desc, new Vector2(x + padding, y + 32), Color.White);
                sprite.DrawLine(new Vector2(x, y + 68), new Vector2(x + this.BoundingBox.Width / 2, y + 68), Color.White, 1);
                y += padding;
            }
        }
    }
}
