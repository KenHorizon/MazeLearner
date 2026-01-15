using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MazeLearner
{
    public class MouseHandler
    {
        public MouseState PrevState { get; private set; }
        public MouseState CurrentState { get; private set; }

        private MouseClick mouseClick { get; set; }
        private int clickMs = 0;
        public MouseHandler()
        {
            this.PrevState = new MouseState();
            this.CurrentState = Mouse.GetState();
        }
        public void Update()
        {
            this.PrevState = this.CurrentState;
            this.mouseClick = this.SetupMouseClicked();
            this.CurrentState = Mouse.GetState();
            if (this.clickMs > 0)
            {
                this.clickMs--;
            }
        }

        private MouseClick SetupMouseClicked()
        {
            if (this.IsLeftClicked())
            {
                return MouseClick.Left;
            }
            else if (this.IsRightClicked())
            {
                return MouseClick.Right;
            }
            else if(this.IsMiddleClicked())
            {
                return MouseClick.Middle;
            } 
            else
            {
                return MouseClick.None;
            }
        }

        public int MouseClickState()
        {
            return (int) this.mouseClick;
        }

        public bool MouseDown(MouseButton button)
        {
            switch (button)
            {
                case MouseButton.Left:
                    return this.CurrentState.LeftButton == ButtonState.Pressed;
                case MouseButton.Middle:
                    return this.CurrentState.MiddleButton == ButtonState.Pressed;
                case MouseButton.Right:
                    return this.CurrentState.RightButton == ButtonState.Pressed;
                case MouseButton.XButton1:
                    return this.CurrentState.XButton1 == ButtonState.Pressed;
                case MouseButton.XButton2:
                    return this.CurrentState.XButton2 == ButtonState.Pressed;
                default:
                    return false;
            }
        }
        public bool MouseUp(MouseButton button)
        {
            switch (button)
            {
                case MouseButton.Left:
                    return this.CurrentState.LeftButton == ButtonState.Released;
                case MouseButton.Middle:
                    return this.CurrentState.MiddleButton == ButtonState.Released;
                case MouseButton.Right:
                    return this.CurrentState.RightButton == ButtonState.Released;
                case MouseButton.XButton1:
                    return this.CurrentState.XButton1 == ButtonState.Released;
                case MouseButton.XButton2:
                    return this.CurrentState.XButton2 == ButtonState.Released;
                default:
                    return false;
            }
        }

        public bool MousePressed(MouseButton button)
        {
            switch (button)
            {
                case MouseButton.Left:
                    return this.CurrentState.LeftButton == ButtonState.Pressed && this.PrevState.LeftButton == ButtonState.Released;
                case MouseButton.Middle:
                    return this.CurrentState.MiddleButton == ButtonState.Pressed && this.PrevState.MiddleButton == ButtonState.Released;
                case MouseButton.Right:
                    return this.CurrentState.RightButton == ButtonState.Pressed && this.PrevState.RightButton == ButtonState.Released;
                case MouseButton.XButton1:
                    return this.CurrentState.XButton1 == ButtonState.Pressed && this.PrevState.XButton1 == ButtonState.Released;
                case MouseButton.XButton2:
                    return this.CurrentState.XButton2 == ButtonState.Pressed && this.PrevState.XButton2 == ButtonState.Released;
                default:
                    return false;
            }
        }

        internal bool IsLeftClicked()
        {
            return this.MousePressed(MouseButton.Left);
        }
        internal bool IsRightClicked()
        {
            return this.MousePressed(MouseButton.Right);
        }
        internal bool IsMiddleClicked()
        {
            return this.MousePressed(MouseButton.Middle);
        }

        public Vector2 Position => this.CurrentState.Position.ToVector2();
        public Point PositionDelta => this.CurrentState.Position - this.PrevState.Position;

        public bool Intersact(Rectangle rectangle)
        {
            return rectangle.Contains(this.CurrentState.Position);
        }
        public Point Location => this.CurrentState.Position;
        public int X => this.CurrentState.X;
        public int Y => this.CurrentState.Y;
        public int PrevX => this.PrevState.X;
        public int PrevY => this.PrevState.Y;
        public int XDelta => this.CurrentState.X - this.PrevState.X;

        public int YDelta => this.CurrentState.Y - this.PrevState.Y;

        public bool WasMoved => PositionDelta != Point.Zero;
        public int ScrollWheel => CurrentState.ScrollWheelValue;
        public int ScrollWheelDelta => CurrentState.ScrollWheelValue - this.PrevState.ScrollWheelValue;
        public void SetPosition(int x, int y)
        {
            Mouse.SetPosition(x, y);
            CurrentState = new MouseState(x, y, CurrentState.ScrollWheelValue, CurrentState.LeftButton, CurrentState.MiddleButton,CurrentState.RightButton, CurrentState.XButton1, CurrentState.XButton2);
        }
    }
}
