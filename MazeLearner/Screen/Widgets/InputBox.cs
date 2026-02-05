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
        public int Index { get; private set; }
        public string Text { get; set; } = "";
        public Rectangle Box { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 RowColumn { get; set; }
        public InputBoxEntry(int index, int row, int column, string text, Rectangle box)
        {
            Loggers.Msg($"Index: {index} Value:{text} R:{row} C:{column}");
            this.Index = index;
            this.Text = text;
            this.Box = box;
            this.Position = new Vector2(box.X, box.Y);
            this.RowColumn = new Vector2(row, column);
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
        private bool _confirmed = false;
        public bool Capslock { get; set; }
        public string GetInputKey => _getInputKey;
        public bool Confirmed => _confirmed;
        public int columnRow = 0;
        public int columnColumn = 0;
        public string[] keyboard = new string[] { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
        public string[] numbers = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        public string[] control = new string[] { "abc", "ABC" };
        public string[,] keyRowColumns = 
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
                return Main.MaxTileSize * 14;
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
                return new Rectangle((this.boxX + this.boxW) / 2 - boxPadding, this.boxY + (this.Bounds.Height) + boxPadding, boxW - (boxX + (boxPadding * 2)), boxH - boxPadding - 32);
            }
            set
            {
                this.boxX = value.X;
                this.boxY = value.Y;
                this.boxW = value.Width;
                this.boxH = value.Height;
            }
        }
        private int cursorRow = 0;
        private int cursorCol = 0;
        private Dictionary<(int row, int col), InputBoxEntry> grid = new Dictionary<(int, int), InputBoxEntry>();
        public bool IsOnKeyIndex => this.IndexBtn < keyboard.Length;
        public bool IsOnNumIndex => this.IndexBtn > keyboard.Length && this.IndexBtn < keyboard.Length + numbers.Length;
        public bool IsOnControlIndex => this.IndexBtn > keyboard.Length + numbers.Length + control.Length;
        private int keyRow = 0;
        private int keyCol = 0;
        private int numRow = 0;
        private int numCol = 0;
        private int conRow = 0;
        private int conCol = 0;
        public InputBox() : base(Fonts.DT_L, (0 + (Main.MaxTileSize * 14)) / 2 - (padding / 2), 0, (Main.MaxTileSize * 14) - (0 + (padding * 2)), height, 12)
        {
            this.IndexBtn = 0;
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
            for (int i = 0; i < keyRowColumns.GetLength(0); i++)
            {
                //Col
                for (int j = 0; j < keyRowColumns.GetLength(1); j++)
                {
                    if (keyRowColumns[i, j].IsEmpty()) continue;
                    if (index < keyboard.Length)
                    {
                        this.Entries.Add(new InputBoxEntry(index, keyRow, keyCol, keyRowColumns[i, j], new Rectangle(
                        keyX, keyY, Size, Size)));
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
                    if (index >= keyboard.Length && index < keyboard.Length + numbers.Length)
                    {
                        numCol = 1;
                        this.Entries.Add(new InputBoxEntry(index, numRow, numCol + keyCol, keyRowColumns[i, j], new Rectangle(
                        numX, numY + (Size * (keyCol + 2)), Size, Size)));
                        numX += Size;
                        numRow++;
                    }
                    if (index >= keyboard.Length + numbers.Length)
                    {
                        conCol = 1;
                        Vector2 textSize = TextManager.MeasureString(Fonts.DT_XL, keyRowColumns[i, j]);
                        this.Entries.Add(new InputBoxEntry(index, conRow, conCol + keyCol + numCol, keyRowColumns[i, j], new Rectangle(
                        colX, colY + (Size * (keyCol + numCol + 3)), Size, Size)));
                        colX += (int)((Size + ((Size + textSize.X) / 2)));
                        conRow++;
                    }
                    index++;
                }
            }
            foreach (var entry in this.Entries)
            {
                int r = (int)entry.RowColumn.X;
                int c = (int)entry.RowColumn.Y;

                this.grid[(r, c)] = entry;
            }
            // Setting a default value :)
            cursorRow = (int)Entries[0].RowColumn.X;
            cursorCol = (int)Entries[0].RowColumn.Y;
            IndexBtn = Entries[0].Index;
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
            if (Main.Keyboard.Pressed(Microsoft.Xna.Framework.Input.Keys.LeftShift))
            {
                this.Capslock = !this.Capslock;
            }
            if (Main.Keyboard.Pressed(Microsoft.Xna.Framework.Input.Keys.Enter))
            {
                this._confirmed = true;
            }
            if (Main.Keyboard.Pressed(Microsoft.Xna.Framework.Input.Keys.Back))
            {
                this.HandleInputKeyboardRemove(true);
            }
            foreach (var entry in this.Entries)
            {
                if (entry.Index < keyboard.Length + numbers.Length)
                {
                    entry.Text = entry.Text.Capitalize(this.Capslock);
                }
            }
            if (Main.Keyboard.Pressed(GameSettings.KeyForward))
            {
                this.MoveCursor(0, -1);
                this.PlaySoundClick();

            }
            if (Main.Keyboard.Pressed(GameSettings.KeyDownward))
            {
                this.MoveCursor(0, 1);
                this.PlaySoundClick();
            }
            if (Main.Keyboard.Pressed(GameSettings.KeyLeft))
            {
                this.MoveCursor(-1, 0);
                this.PlaySoundClick();
            }
            if (Main.Keyboard.Pressed(GameSettings.KeyRight))
            {
                this.MoveCursor(1, 0);
                this.PlaySoundClick();
            }
            if (Main.Keyboard.Pressed(GameSettings.KeyInteract))
            {
                foreach (var entry in this.Entries)
                {
                    if (this.IndexBtn == entry.Index)
                    {
                        if (this.IndexBtn == 37)
                        {
                            this.Capslock = true;
                        }
                        if (this.IndexBtn == 36)
                        {
                            this.Capslock = false;
                        }
                        if (this.IndexBtn >= keyboard.Length + numbers.Length)
                        {
                            this.HandleInputKeyboardRemove(39 == this.IndexBtn);
                            if (this.IndexBtn == 38)
                            {
                                this._confirmed = true;
                            }
                        } 
                        else
                        {
                            this.HandleInputKeyboardAdd(entry.Text);
                        }
                    }
                }
                this.PlaySoundClick();
            }
        }
        private void MoveCursor(int dRow, int dCol)
        {
            int newRow = cursorRow + dRow;
            int newCol = cursorCol + dCol;
            int safety = 0;
            const int MAX_STEPS = 100;
            while (safety++ < MAX_STEPS)
            {
                if (grid.TryGetValue((newRow, newCol), out var entry0))
                {
                    cursorRow = newRow;
                    cursorCol = newCol;
                    IndexBtn = entry0.Index;
                    return;
                }
                newRow += dRow;
                newCol += dCol;
                if (newRow >= keyRowColumns.GetLength(0) || newCol >= keyRowColumns.GetLength(1))
                {
                    if (grid.TryGetValue((cursorRow, cursorCol), out var currentEntry))
                    {
                        if (grid.TryGetValue((newRow, newCol), out var newEntry))
                        {
                            cursorRow = newRow;
                            cursorCol = newCol;
                            IndexBtn = newEntry.Index;
                            return;
                        }
                        newRow = 0;
                        newCol = cursorCol + dCol;
                    }
                }
                if (newRow < 0 || newCol < 0)
                {
                    newRow = 0;
                    newCol = 0;
                }
            }

        }
        public override bool DoSoundHovered()
        {
            return false;
        }
    }
}
