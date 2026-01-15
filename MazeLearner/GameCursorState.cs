using MazeLearner;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MazeLeaner
{
    public class GameCursorState
    {
        public Texture2D CursorTexture = Assets<Texture2D>.Request($"UI/Cursor").Value;
        private Main main;
        public GameCursorState(Main main)
        {
            this.main = main;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (GameSettings.CustomCursor)
            {
                Vector2 position = new Vector2(Main.Mouse.Position.X - (this.CursorTexture.Width / 2), Main.Mouse.Position.Y - (this.CursorTexture.Height / 2));
                spriteBatch.Draw(this.CursorTexture, position, Color.White);
            }
        }
        public void Update(GameTime gameTime)
        {

        }
    }
}
