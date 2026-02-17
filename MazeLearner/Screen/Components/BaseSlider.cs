using MazeLeaner.Text;
using MazeLearner.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MazeLearner.Screen.Components
{
    public abstract class BaseSlider : BaseWidgets
    {
        public bool HasChange { get; set; }
        private int _delayMs = 0;
        private int _defaultAmount = 0;
        private int _prevAmount = 0;
        private int _amount = 0;
        private int _min;
        private int _max;
        private float _value;
        private bool _canDragByMouse = false;
        private Texture2D _sliderOverlay = null;
        private Texture2D _slider = null;
        private Action _onUpdate;
        public Action OnUpdate
        {
            get { return _onUpdate; }
            set { _onUpdate = value; }
        }
        public int DelayMs
        {
            get { return _delayMs; }
            set { _delayMs = value; }
        }
        public int DefaultAmount
        {
            get { return _defaultAmount; }
            set { _defaultAmount = value; }
        }
        public int PrevAmount
        {
            get { return _prevAmount; }
            set { _prevAmount = value; }
        }
        public int Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
        public int Min
        {
            get { return _min; }
            set { _min = value; }
        }
        public int Max
        {
            get { return _max; }
            set { _max = value; }
        }
        public float Value
        {
            get { return _value; }
            set { _value = value; }
        }
        public bool CanDragByMouse
        {
            get { return _canDragByMouse; }
            set { _canDragByMouse = value; }
        }

        public Texture2D SliderOverlay
        {
            get { return _sliderOverlay; }
            set { _sliderOverlay = value; }
        }
        public Texture2D Slider
        {
            get { return _slider; }
            set { _slider = value; }
        }

        public BaseSlider(int min, int max, int defaultValue, Texture2D sliderOverlay, Texture2D slider, int x, int y, int width, int height, Action onUpdate = null) : base(x, y, width, height)
        {
            this.Min = min;
            this.Max = max;
            this.DefaultAmount = defaultValue;
            this.Amount = defaultValue;
            this.Value = MathHelper.Clamp(((float) this.Amount / this.Max), 0.0F, 1.0F);
            this.SliderOverlay = sliderOverlay;
            this.Slider = slider;
            this.OnUpdate = onUpdate;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (this.DelayMs > 0) this.DelayMs--;
            this.PrevAmount = this.Amount;
            if (this.IsFocused() == true && this.DelayMs <= 0)
            {
                if (Main.Input.IsKeyDown(GameSettings.KeyLeft))
                {
                    this.Amount = Math.Max(this.Amount - 1, this.Min);
                    this.DelayMs = 10;
                }
                if (Main.Input.IsKeyDown(GameSettings.KeyRight))
                {
                    this.Amount = Math.Min(this.Amount + 1, this.Max);
                    this.DelayMs = 10;
                }
                this.Value = MathHelper.Clamp(((float)this.Amount / this.Max), 0.0F, 1.0F);
            }
            this.OnUpdate?.Invoke();
            this.HasChange = this.PrevAmount != this.Amount;
        }
        
        public override void Render(SpriteBatch sprite, Vector2 mouse)
        {
            base.Render(sprite, mouse);
            sprite.NinePatch(this.Slider, this.Bounds, Color.White, 32);
            Rectangle sliderOverlayBtn = new Rectangle(this.Bounds.X, this.Bounds.Y, (int) (this.Bounds.Width * this.Value), this.Bounds.Height);
            sprite.Draw(this.SliderOverlay, sliderOverlayBtn, Color.White);
            Texts.DrawString($"{this.Amount}%", new Vector2(sliderOverlayBtn.X + 20, sliderOverlayBtn.Y + 20));
        }
    }
}
