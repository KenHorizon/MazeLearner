using MazeLeaner.Text;
using MazeLearner.Screen.Components;
using MazeLearner.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MazeLearner.Screen.Widgets
{
    public class InputBoxEntry
    {
        public int Index { get; set; }
        public string Text { get; set; } = "";
        public Action Action { get; set; }
        public Rectangle Box { get; set; }
        public Vector2 Position { get; set; }
        public InputBoxEntry(int index, string text, Rectangle box, Action action)
        {
            Loggers.Msg($"{index} - {text}");
            this.Index = index;
            this.Text = text;
            this.Action = action;
            this.Box = box;
            this.Position = new Vector2(box.X, box.Y);
        }
    }
    public class InputBox : BaseTextbox
    {
        private static int padding = 40;
        private static int width = Main.Instance.WindowScreen.Width;
        private static int height = 132;
        public int IndexBtn = 0;
        private int boxPadding = 32;
        private int boxX = 0;
        private int boxY = 0;
        private string _getInputKey = "";
        private bool _capslock = false;
        public bool Capslock => _capslock;
        public string GetInputKey => _getInputKey;
        private int currentColumn = 0;
        private int currentRow = 0;
        public string[] keyboard = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
        public string[] numbers = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        public string[] control = new string[] { "abc", "ABC" };
        private List<InputBoxEntry>[] columns;

        public const int KeyMaxRow = 10;
        public int Size => Main.MaxTileSize;
        public int TotalLenght => keyboard.Length + numbers.Length + control.Length;
        public List<InputBoxEntry> Entries = new List<InputBoxEntry>();
        private int boxW
        {
            get
            {
                return this.game.GetScreenWidth();
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
                return this.game.GetScreenHeight() - this.Bounds.Height;
            }
            set
            {
                this.boxH = value;
            }
        }
        public Rectangle InputKeyBox
        {
            get
            {
                return new Rectangle(this.boxX + boxPadding, this.boxY + (this.Bounds.Height) + boxPadding, boxW - (boxX + (boxPadding * 2)), boxH - boxPadding - 32);
            }
            set
            {
                this.boxX = value.X;
                this.boxY = value.Y;
                this.boxW = value.Width;
                this.boxH = value.Height;
            }
        }
        private int slotRow = 0;
        private int slotCol= 0;
        private int cursorX
        {
            get
            {
                return this.InputKeyBox.X + Main.MaxTileSize * slotRow;
            }
            set
            {
                cursorX = value;
            }
        }
        private int cursorY
        {
            get
            {
                return this.InputKeyBox.Y + Main.MaxTileSize * slotCol;
            }
            set
            {
                cursorY = value;
            }
        }
        public bool IsOnKeyIndex => this.IndexBtn < keyboard.Length;
        public bool IsOnNumIndex => this.IndexBtn > keyboard.Length && this.IndexBtn < keyboard.Length + numbers.Length;
        public bool IsOnControlIndex => this.IndexBtn > keyboard.Length + numbers.Length + control.Length;
        private int krow = 0;
        private int nrow = 0;
        private int crow = 0;
        private int kcol = 0;
        private int ncol = 0;
        private int ccol = 0;
        public InputBox() : base(Fonts.DT_L, 0 + (padding / 2), 0, width - padding, height, 12)
        {
            int keyStartIndex = 0;
            int numberStartIndex = keyboard.Length;
            int controlStartIndex = keyboard.Length + numbers.Length;
            int keyX = this.InputKeyBox.X + Size;
            int keyY = this.InputKeyBox.Y + Size;
            for (int i = keyStartIndex; i < keyboard.Length; i++)
            {
                this.Entries.Add(new InputBoxEntry(i, keyboard[i], new Rectangle(
                    keyX, keyY, Size, Size), () =>
                    {
                        this._getInputKey = keyboard[i];
                        string finalizedInput = this.Capslock == true ? Utils.Capitalize(keyboard[i]) : keyboard[i];
                        this.HandleInputKeyboard(finalizedInput, Main.Keyboard);
                    }));
                keyX += Size;
                krow++;
                if (krow > KeyMaxRow)
                {
                    keyX = this.InputKeyBox.X + Size;
                    krow = 0;
                    kcol++;
                    keyY += Size;
                }
            }
            keyX = this.InputKeyBox.X + Size;
            keyY += Size * this.kcol;
            for (int i = 0; i < numbers.Length; i++)
            {
                this.Entries.Add(new InputBoxEntry(i + numberStartIndex, numbers[i], new Rectangle(
                    keyX, keyY, Size, Size), () =>
                    {
                        this._getInputKey = numbers[i];
                        this.HandleInputKeyboard(numbers[i], Main.Keyboard);
                    }));
                keyX += Size;
                this.nrow++;
                ncol++;
            }
            keyX = this.InputKeyBox.X + Size;
            keyY += Size * this.ncol;
            for (int i = 0; i < control.Length; i++)
            {
                this.Entries.Add(new InputBoxEntry(i + controlStartIndex, control[i], new Rectangle(
                    keyX, keyY, Size, Size), () =>
                {
                    this._getInputKey = control[i];
                    this._capslock = i == 0;
                }));
                keyX += Size + 24;
                this.crow++;
            }
        }

        public override void Render(SpriteBatch sprite, Vector2 mouse)
        {
            base.Render(sprite, mouse);
            sprite.DrawMessageBox(AssetsLoader.Box4.Value, InputKeyBox, Color.White, 32);
            for (int i = 0; i < this.Entries.Count; i++)
            {
                bool flag = this.Entries[i].Index == this.IndexBtn;
                Vector2 textSize = TextManager.MeasureString(Fonts.DT_XL, this.Entries[i].Text);
                int x = (int)this.Entries[i].Position.X;
                int y = (int)this.Entries[i].Position.Y;
                int w = (int)this.Entries[i].Box.Width;
                int h = (int)this.Entries[i].Box.Height;
                Vector2 textPosition = new Vector2(x + (w - textSize.X) / 2, y + (h - textSize.Y) / 2);
                if (flag)
                {
                    sprite.Draw(AssetsLoader.TextSelected.Value, this.Entries[i].Box);
                }
                if (i < keyboard.Length)
                {
                    TextManager.Text(Fonts.DT_XL, this.Entries[i].Text, textPosition);
                }
                if (i >= (keyboard.Length) && i < (numbers.Length + keyboard.Length))
                {
                    TextManager.Text(Fonts.DT_XL, this.Entries[i].Text, textPosition);
                }
                if (i >= (numbers.Length + keyboard.Length))
                {
                    TextManager.Text(Fonts.DT_XL, this.Entries[i].Text, textPosition);
                }
            }
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Loggers.Msg($"{this.IndexBtn}");
            if (Main.Keyboard.Pressed(GameSettings.KeyForward))
            {
                int rows = IsOnKeyIndex == true || IsOnNumIndex == true ? 10 : 2;
                this.IndexBtn -= rows + 1;
                this.PlaySoundClick();
                if (this.IndexBtn < 0)
                {
                    this.IndexBtn = this.Entries.Count - 1;
                }
            }
            if (Main.Keyboard.Pressed(GameSettings.KeyDownward))
            {
                int rows = IsOnKeyIndex == true || IsOnNumIndex == true ? 10 : 2;
                this.IndexBtn += rows + 1;
                this.PlaySoundClick();
                if (this.IndexBtn > this.Entries.Count - 1)
                {
                    this.IndexBtn = 0;
                }
            }
            if (Main.Keyboard.Pressed(GameSettings.KeyLeft))
            {
                this.IndexBtn -= 1;
                this.PlaySoundClick();
                if (this.IndexBtn > this.Entries.Count - 1)
                {
                    this.IndexBtn = 0;
                }
            }
            if (Main.Keyboard.Pressed(GameSettings.KeyRight))
            {
                this.IndexBtn += 1;
                this.PlaySoundClick();
                if (this.IndexBtn > this.Entries.Count - 1)
                {
                    this.IndexBtn = 0;
                }
            }
            if (Main.Keyboard.Pressed(GameSettings.KeyInteract))
            {
                foreach (InputBoxEntry entries in this.Entries)
                {
                    int btnIndex = entries.Index;
                    if (this.IndexBtn == btnIndex)
                    {
                        entries.Action?.Invoke();
                    }
                }
            }
        }

        public override bool DoSoundHovered()
        {
            return false;
        }
    }
}
