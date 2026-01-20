using MazeLearner;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MazeLeaner
{
    public class GameCursorState
    {
        public static Assets<Texture2D> CursorTexture = Assets<Texture2D>.Request("UI/Cursor");
        private Main main;
        public GameCursorState(Main main)
        {
            this.main = main;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (GameSettings.CustomCursor)
            {
                Vector2 position = new Vector2(Main.Mouse.Position.X - (GameCursorState.CursorTexture.Value.Width / 2), Main.Mouse.Position.Y - (GameCursorState.CursorTexture.Value.Height / 2));
                spriteBatch.Draw(GameCursorState.CursorTexture.Value, position, Color.White);
            }
        }
        public void Update(GameTime gameTime)
        {

        }
    }
}
