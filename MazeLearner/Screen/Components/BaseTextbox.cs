using MazeLeaner.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace MazeLearner.Screen.Components
{
    public class BaseTextbox : BaseWidgets
    {
        private bool ShowCaret { get; set; } = true;
        private StringBuilder texts = new StringBuilder();
        private double _caretblinkTimer = 0;
        private int _caretPosition = 0;
        private int _maxCharacter;
        private int _maxW = 100;
        private int _maxH = 40;
        private Color _textColor = Color.Black;
        private float _lineSpacing;
        private List<string> _wrappedLines = new List<string>();

        private Assets<SpriteFont> _font;
        public StringBuilder Texts
        {
            get { return texts; }
            set { texts = value; }
        }
        public Assets<SpriteFont> Font
        {
            get { return _font; }
            set { _font = value; }
        }
        public string LabelText { get; set; } = "";
        public double CaretblinkTimer
        {
            get { return _caretblinkTimer; }
            set { _caretblinkTimer = value; }
        }
        public int CaretPos
        {
            get { return _caretPosition; }
            set { _caretPosition =  value; }
        }
        public int MaxCharacter
        {
            get { return _maxCharacter; }
            set { _maxCharacter = value; }
        }
        public int MaxWidth
        {
            get { return _maxW; }
            set { _maxW = value; }
        }
        public int MaxHeight
        {
            get { return _maxH; }
            set { _maxH = value; }
        }
        public Color TextColor
        {
            get { return _textColor; }
            set { _textColor = value; }
        }
        public float LineSpacing
        {
            get { return _lineSpacing; }
            set { _lineSpacing = value; }
        }
        public List<string> WrappedLines
        {
            get { return _wrappedLines; }
            set { _wrappedLines = value; }
        }
        public BaseTextbox(Assets<SpriteFont> font, int x, int y, int width, int height, int maxCharacter = 200) : base(x, y, width, height)
        {
            this.Font = font;
            this.posX = x;
            this.posY = y;
            this.Width = Math.Max(width, this.MaxWidth);
            this.Height = Math.Max(height, this.MaxHeight);
            this.MaxCharacter = maxCharacter;
        }
        public override bool MouseClicked(Vector2 mouse, MouseHandler handler)
        {
            if (handler.IsLeftClicked()) this.SetFocused(this.Bounds.Contains(mouse));

            return base.MouseClicked(mouse, handler);
        }
        /// <summary>
        /// Handle if using hardware keyboard only
        /// </summary>
        /// <param name="handler"></param>
        public void HandleInput(KeyboardHandler handler)
        {
            if (!IsFocused()) return;
            KeyboardState keyboardState = handler.CurrentState;
            foreach (Keys key in keyboardState.GetPressedKeys())
            {
                if (handler.Pressed(key))
                {
                    char c = GetCharFromKey(key, keyboardState);
                    if (c != '\0' && this.Texts.Length <= this.MaxCharacter)
                    {
                        this.Texts.Insert(this.CaretPos, c);
                        this.CaretPos++;
                    }

                    if (key == Keys.Back && this.Texts.Length > 0 && this.CaretPos > 0)
                    {
                        this.Texts.Remove(this.CaretPos - 1, 1);
                        this.CaretPos--;
                    }
                }
            }
        }
        /// <summary>
        /// Handle if using ingame keyboard only
        /// </summary>
        /// <param name="keyChar"></param>
        /// <param name="handler"></param>
        public void HandleInputKeyboard(string keyChar, KeyboardHandler handler)
        {
            if (!IsFocused()) return;
            KeyboardState keyboardState = handler.CurrentState;
            foreach (Keys key in keyboardState.GetPressedKeys())
            {
                if (handler.Pressed(key))
                {
                    if (this.Texts.Length <= this.MaxCharacter)
                    {
                        this.Texts.Insert(this.CaretPos, keyChar);
                        this.CaretPos++;
                    }

                    if (key == Keys.Back && this.Texts.Length > 0 && this.CaretPos > 0)
                    {
                        this.Texts.Remove(this.CaretPos - 1, 1);
                        this.CaretPos--;
                    }
                }
            }
        }

        private char GetCharFromKey(Keys key, KeyboardState keyboard)
        {
            bool shift = keyboard.IsKeyDown(Keys.LeftShift) || keyboard.IsKeyDown(Keys.RightShift);

            if (key >= Keys.A && key <= Keys.Z)
            {
                char c = (char)('a' + (key - Keys.A));
                return shift ? char.ToUpper(c) : c;
            }
            if (key >= Keys.D0 && key <= Keys.D9)
            {
                return (char)('0' + (key - Keys.D0));
            }
            if (key == Keys.Space)
                return ' ';

            return '\0';
        }
        public override void Render(SpriteBatch sprite, Vector2 mouse)
        {
            if (this.visible == false) return;
            sprite.DrawMessageBox(AssetsLoader.MessageBox.Value, this.Bounds, Color.White, 32);
            this.WrappedLines = Utils.WrapText(this.Font.Value, this.Texts.ToString(), Width - 32);
            bool flag = this.LabelText.IsEmpty();
            Vector2 textPos = new Vector2(this.posX + 20, this.posY + (flag == false ? 80 : 20));
            if (flag == false)
            {
                Vector2 labelPos = new Vector2(this.posX + 20, this.posY + 20);
                TextManager.Text(this.Font, this.LabelText, labelPos, this.TextColor * 0.55F);
            }
            char[] texts = this.Texts.ToString().ToCharArray();
            for (int i = 0; i < this.MaxCharacter; i++)
            {
                Vector2 pos = new Vector2(textPos.X + (i * (textPos.X / 2)), textPos.Y);
                if (i < texts.Length)
                {
                    TextManager.Text(this.Font, texts[i].ToString(), pos, this.TextColor);
                } 
                else
                {
                    TextManager.Text(this.Font, "_", pos, Color.Gray);
                }
            }

            for (int i = 0; i < texts.Length; i++)
            {
                }
            if (this.IsFocused())
            {
                //this.CaretblinkTimer += Main.Instance.DeltaTime * 1000;
                //if (this.CaretblinkTimer >= 500)
                //{
                //    this.ShowCaret = !ShowCaret;
                //    this.CaretblinkTimer = 0;
                //}

                //if (this.ShowCaret)
                //{
                //    string beforeCaret = this.Texts.ToString().Substring(0, CaretPos);
                //    var preWrap = Utils.WrapText(this.Font.Value, beforeCaret, Width - 8);

                //    int caretLine = preWrap.Count - 1;
                //    string caretLineText = preWrap.Count > 0 ? preWrap[^1] : "";
                //    float caretX = textPos.X + this.Font.Value.MeasureString(caretLineText).X + 4;
                //    float caretY = textPos.Y + 4 + caretLine * this.LineSpacing;

                //    sprite.Draw(Main.FlatTexture, new Rectangle((int) (this.posX + caretX), (int)(this.posY + caretY), 2, (int) this.Font.Value.LineSpacing), Color.Black);
                //}
            }
            base.Render(sprite, mouse);
        }
        public string GetText => this.Texts.ToString();

        public override bool DoSoundHovered()
        {
            return false;
        }
    }
}
