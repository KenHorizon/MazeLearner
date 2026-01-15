using Microsoft.Xna.Framework.Input;

namespace MazeLearner
{
    public class KeyboardHandler
    {
        private int cooldownPress = 0;
        public KeyboardState PrevState { get; private set; }
        public KeyboardState CurrentState { get; private set; }

        public int ClickMs => this.cooldownPress;

        public bool CanClickAgain => this.cooldownPress <= 0;

        public KeyboardHandler()
        {
            this.PrevState = new KeyboardState();
            this.CurrentState = Keyboard.GetState();

        }
        public void Update()
        {
            this.PrevState = CurrentState;
            this.CurrentState = Keyboard.GetState();
            if (cooldownPress > 0)
            {
                --this.cooldownPress;
            }
        }
        public bool IsKeyDown(Keys key)
        {
            return this.CurrentState.IsKeyDown(key);
        }
        public bool IsKeyUp(Keys key)
        {
            return this.CurrentState.IsKeyUp(key);
        }
        public bool Pressed(Keys key)
        {
            if (this.IsKeyDown(key) && this.PrevState.IsKeyUp(key) && this.CanClickAgain)
            {
                this.cooldownPress = 4;
                return true;
            }
            return false;
        }
        public bool Released(Keys key)
        {
            if (this.IsKeyUp(key) && this.PrevState.IsKeyDown(key) && this.CanClickAgain)
            {
                this.cooldownPress = 4;
                return true;
            }
            return false;
        }
    }
}
