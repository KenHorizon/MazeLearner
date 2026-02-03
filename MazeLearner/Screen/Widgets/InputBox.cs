using MazeLeaner.Text;
using MazeLearner.Screen.Components;
using MazeLearner.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace MazeLearner.Screen.Widgets
{
    public class InputBoxEntry
    {
        public int Index { get; set; }
        public string Text { get; set; } = "";
        public Action Action { get; set; }
        public InputBoxEntry(int index, string text, Action action)
        {
            this.Index = index;
            this.Text = text;
            this.Action = action;
        }
    }
    public class InputBox : BaseTextbox
    {
        private static int padding = 40;
        private static int width = Main.Instance.WindowScreen.Width;
        private static int height = 132;
        public int IndexBtn;
        private int boxPadding = 32;
        private int boxX = 0;
        private int boxY = 0;
        private string _getInputKey = "";
        private bool _capslock = false;
        public bool Capslock => _capslock;
        public string GetInputKey => _getInputKey;
        public string[] keyboard = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
        public string[] numbers = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };
        public string[] control = new string[] { "abc", "ABC" };
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
        public InputBox() : base(Fonts.DT_L, 0 + (padding / 2), 0, width - padding, height, 12)
        {
            int keyStartIndex = 0;
            int numberStartIndex = keyboard.Length;
            int controlStartIndex = keyboard.Length + numbers.Length;
            for (int i = keyStartIndex; i < keyboard.Length; i++)
            {
                this.Entries.Add(new InputBoxEntry(i, keyboard[i], () =>
                {
                    this._getInputKey = keyboard[i];
                    string finalizedInput = this.Capslock == true ? Utils.Capitalize(keyboard[i]) : keyboard[i];
                    this.HandleInputKeyboard(finalizedInput, Main.Keyboard);
                }));
            }
            for (int i = 0; i < numbers.Length; i++)
            {
                this.Entries.Add(new InputBoxEntry(i + numberStartIndex, numbers[i], () =>
                {
                    this._getInputKey = numbers[i];
                    this.HandleInputKeyboard(numbers[i], Main.Keyboard);
                }));
            }
            for (int i = 0; i < control.Length; i++)
            {
                this.Entries.Add(new InputBoxEntry(i + controlStartIndex, control[i], () =>
                {
                    this._getInputKey = control[i];
                    this._capslock = i == 0;
                }));
            }
        }

        public override void Render(SpriteBatch sprite, Vector2 mouse)
        {
            base.Render(sprite, mouse);
            sprite.DrawMessageBox(AssetsLoader.Box4.Value, InputKeyBox, Color.White, 32);
            int numberRow = 0;
            int numberCol = 0;
            int controlRow = 0;
            int controlCol = 0;
            int keyRow = 0;
            int keyCol = 0;
            int keyX = this.InputKeyBox.X + Size;
            int keyY = this.InputKeyBox.Y + Size;
            int controlX = this.InputKeyBox.X + Size;
            int controlY = this.InputKeyBox.Y + Size;
            int numberX = this.InputKeyBox.X + Size;
            int numberY = this.InputKeyBox.Y + Size;
            for (int i = 0; i < this.Entries.Count; i++)
            {
                if (i < keyboard.Length)
                {
                    TextManager.Text(Fonts.DT_XL, this.Entries[i].Text, new Vector2(keyX, keyY));
                    keyX += Size;
                    keyRow++;
                    if (keyRow > KeyMaxRow)
                    {
                        keyX = this.InputKeyBox.X + Size;
                        keyRow = 0;
                        keyCol += Size;
                        keyY += Size;
                    }
                }
                if (i > numbers.Length && i <= (numbers.Length + keyboard.Length))
                {
                    numberY += keyCol;
                    TextManager.Text(Fonts.DT_XL, this.Entries[i].Text, new Vector2(numberX, numberY));
                    numberX += Size;
                    numberRow++;
                }
            }
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (Main.Keyboard.Pressed(GameSettings.KeyForward))
            {
                this.slotRow -= 1;
                this.PlaySoundClick();
                if (this.IndexBtn < 0)
                {
                    this.IndexBtn = this.Entries.Count - 1;
                }
            }
            if (Main.Keyboard.Pressed(GameSettings.KeyDownward))
            {
                this.slotRow += 1;
                this.PlaySoundClick();
                if (this.IndexBtn > this.Entries.Count - 1)
                {
                    this.IndexBtn = 0;
                }
            }
            if (Main.Keyboard.Pressed(GameSettings.KeyLeft))
            {
                this.slotCol -= 1;
                this.PlaySoundClick();
                if (this.IndexBtn < 0)
                {
                    this.IndexBtn = this.Entries.Count - 1;
                }
            }
            if (Main.Keyboard.Pressed(GameSettings.KeyRight))
            {
                this.slotCol += 1;
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
