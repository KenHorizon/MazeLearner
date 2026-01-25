using MazeLeaner.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

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
        private Color _textColor = Color.White;
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
            this.Width = Math.Max(width, this.MaxWidth);
            this.Height = Math.Max(height, this.MaxHeight);
            this.MaxCharacter = maxCharacter;
        }
        public override bool MouseClicked(Vector2 mouse, MouseHandler handler)
        {
            if (handler.IsLeftClicked()) this.SetFocused(this.Bounds.Contains(mouse));

            return base.MouseClicked(mouse, handler);
        }
        public void HandleInput(KeyboardHandler handler)
        {
            if (!IsFocused()) return;
            KeyboardState keyboardState = handler.CurrentState;
            foreach (Keys key in keyboardState.GetPressedKeys())
            {
                if (handler.Pressed(key))
                {
                    char c = GetCharFromKey(key, keyboardState);
                    if (c != '\0')
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
            sprite.DrawMessageBox(AssetsLoader.Box0.Value, this.Bounds, Color.White, 32);
            this.WrappedLines = Utils.WrapText(this.Font.Value, this.Texts.ToString(), Width - 8);
            Vector2 textPos = new Vector2(this.posX + 4, this.posY + 4);
            if (this.WrappedLines.Empty() && !this.IsFocused())
            {
                TextManager.Text(this.Font, this.Text, textPos, this.TextColor * 0.55F);
            }
            else
            {
                foreach (var line in this.WrappedLines)
                {
                    TextManager.Text(this.Font, line, textPos, this.TextColor);
                    textPos.Y += this.LineSpacing;
                }
            }
            if (this.IsFocused())
            {
                this.CaretblinkTimer += Main.Instance.DeltaTime * 1000;
                if (this.CaretblinkTimer >= 500)
                {
                    this.ShowCaret = !ShowCaret;
                    this.CaretblinkTimer = 0;
                }

                if (this.ShowCaret)
                {
                    string beforeCaret = this.Texts.ToString().Substring(0, CaretPos);
                    var preWrap = Utils.WrapText(this.Font.Value, beforeCaret, Width - 8);

                    int caretLine = preWrap.Count - 1;
                    string caretLineText = preWrap.Count > 0 ? preWrap[^1] : "";
                    float caretX = this.Font.Value.MeasureString(caretLineText).X + 4;
                    float caretY = 4 + caretLine * this.LineSpacing;

                    sprite.Draw(Main.FlatTexture, new Rectangle((int) (this.posX + caretX), (int)(this.posY + caretY), 2, (int) this.Font.Value.LineSpacing), Color.White);
                }
            }
            base.Render(sprite, mouse);
        }
        public string GetText => this.Texts.ToString();
    }
}
