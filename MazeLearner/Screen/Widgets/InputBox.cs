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
        public InputBoxEntry(int index, int row, int column, string text, Rectangle box, Action action)
        {
            //Loggers.Msg($"Index: {index} Value:{text} R:{row} C:{column}");
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
        public int index = 0;
        private int boxPadding = 32;
        private int boxX = 0;
        private int boxY = 0;
        private string _getInputKey = "";
        private bool _capslock = false;
        public bool Capslock => _capslock;
        public string GetInputKey => _getInputKey;
        public int columnRow = 0;
        public int columnColumn = 0;
        public string[] keyboard = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
        public string[] numbers = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        public string[] control = new string[] { "abc", "ABC" };
        public string[,] columns = 
        { 
            { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" },
            { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" },
            { "abc", "ABC", "Ok", "Back", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" }
        };

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
        private int keyRow = 0;
        private int keyCol = 0;
        private int numRow = 0;
        private int numCol = 0;
        private int conRow = 0;
        private int conCol = 0;
        public InputBox() : base(Fonts.DT_L, 0 + (padding / 2), 0, width - padding, height, 12)
        {
            int numberStartIndex = keyboard.Length;
            int controlStartIndex = keyboard.Length + numbers.Length;
            int startX = this.InputKeyBox.X + Size;
            int startY = this.InputKeyBox.Y + Size;
            int keyX = startX;
            int keyY = startY;
            int numX = startX;
            int numY = startY;
            int colX = startX;
            int colY = startY;
            // Row
            for (int i = 0; i < columns.GetLength(0); i++)
            {
                //Col
                for (int j = 0; j < columns.GetLength(1); j++)
                {
                    index++;
                    if (columns[i, j].IsEmpty()) continue;
                    //Loggers.Msg($"Index: {index}");
                    if (index < keyboard.Length)
                    {
                        this.Entries.Add(new InputBoxEntry(index, i, j, columns[i, j], new Rectangle(
                        keyX, keyY, Size, Size), () =>
                        {
                            string finalizedInput = this.Capslock == true ? Utils.Capitalize(columns[i, j]) : columns[i, j];
                            this.HandleInputKeyboard(finalizedInput, Main.Keyboard);
                        }));
                        keyRow++;
                        keyX += Size;
                        if (keyRow > KeyMaxRow)
                        {
                            keyX = this.InputKeyBox.X + Size;
                            keyRow = 0;
                            keyCol++;
                            keyY += Size;
                        }
                    }
                    if (index > keyboard.Length && index <= keyboard.Length + numbers.Length)
                    {
                        this.Entries.Add(new InputBoxEntry(index, i, j, columns[i, j], new Rectangle(
                        numX, numY + (Size * (keyCol + 2)), Size, Size), () =>
                        {
                            string finalizedInput = this.Capslock == true ? Utils.Capitalize(columns[i, j]) : columns[i, j];
                            this.HandleInputKeyboard(finalizedInput, Main.Keyboard);
                        }));
                        numX += Size;
                        numRow++;
                        numCol = 1; // always zero and it not being registered although the row reach to 10 so its fixed by 1
                    }
                    if (index > keyboard.Length + numbers.Length)
                    {
                        Vector2 textSize = TextManager.MeasureString(Fonts.DT_XL, columns[i, j]);
                        this.Entries.Add(new InputBoxEntry(index, i, j, columns[i, j], new Rectangle(
                        colX, colY + (Size * (keyCol + numCol + 3)), Size, Size), () =>
                        {
                            string finalizedInput = this.Capslock == true ? Utils.Capitalize(columns[i, j]) : columns[i, j];
                            this.HandleInputKeyboard(finalizedInput, Main.Keyboard);
                        }));
                        colX += (int)((Size + ((Size + textSize.X) / 2)));
                        conRow++;
                        if (conRow > KeyMaxRow)
                        {
                            conRow = 0;
                            conCol++;
                        }
                    }
                }
            }
            
            //for (int i = keyStartIndex; i < keyboard.Length; i++)
            //{
            //    this.Entries.Add(new InputBoxEntry(i, this.krow, this.kcol, keyboard[i], new Rectangle(
            //        keyX, keyY, Size, Size), () =>
            //        {
            //            this._getInputKey = keyboard[i];
            //            string finalizedInput = this.Capslock == true ? Utils.Capitalize(keyboard[i]) : keyboard[i];
            //            this.HandleInputKeyboard(finalizedInput, Main.Keyboard);
            //        }));
            //    keyX += Size;
            //    krow++;
            //    if (krow > KeyMaxRow)
            //    {
            //        keyX = this.InputKeyBox.X + Size;
            //        krow = 0;
            //        kcol++;
            //        keyY += Size;
            //    }
            //}
            //keyX = this.InputKeyBox.X + Size;
            //keyY += Size * this.kcol;
            //for (int i = 0; i < numbers.Length; i++)
            //{
            //    this.Entries.Add(new InputBoxEntry(i + numberStartIndex, this.nrow, this.ncol, numbers[i], new Rectangle(
            //        keyX, keyY, Size, Size), () =>
            //        {
            //            this._getInputKey = numbers[i];
            //            this.HandleInputKeyboard(numbers[i], Main.Keyboard);
            //        }));
            //    keyX += Size;
            //    this.nrow++;
            //    ncol++;
            //}
            //keyX = this.InputKeyBox.X + Size;
            //keyY += Size * this.ncol;
            //for (int i = 0; i < control.Length; i++)
            //{
            //    this.Entries.Add(new InputBoxEntry(i + controlStartIndex, this.crow, this.ccol, control[i], new Rectangle(
            //        keyX, keyY, Size, Size), () =>
            //    {
            //        this._getInputKey = control[i];
            //        this._capslock = i == 0;
            //    }));
            //    keyX += Size + 24;
            //    this.crow++;
            //}
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
            if (Main.Keyboard.Pressed(GameSettings.KeyForward))
            {
                this.columnColumn--;
                this.PlaySoundClick();
                
            }
            if (Main.Keyboard.Pressed(GameSettings.KeyDownward))
            {
                this.columnColumn++;
                this.PlaySoundClick();
            }
            if (Main.Keyboard.Pressed(GameSettings.KeyLeft))
            {
                this.columnRow++;
                this.PlaySoundClick();
            }
            if (Main.Keyboard.Pressed(GameSettings.KeyRight))
            {
                this.columnRow--;
                this.PlaySoundClick();
            }
            if (Main.Keyboard.Pressed(GameSettings.KeyInteract))
            {
                Loggers.Msg($"{this.columns[this.columnColumn, this.columnRow]}");
                this.PlaySoundClick();
            }
            //this.columnRow = MathHelper.Clamp(this.columnRow, 0, this.columns.GetLength(0) - 1);
            //this.columnColumn = MathHelper.Clamp(this.columnColumn, 0, this.columns.GetLength(1) - 1);
            //foreach (var entry in this.Entries)
            //{
            //    string text = entry.Text;
            //    if (this.columns[this.columnColumn, this.columnRow] == text)
            //    {
            //        this.IndexBtn =  entry.Index;
            //    }
            //}


        }

        public override bool DoSoundHovered()
        {
            return false;
        }
    }
}
