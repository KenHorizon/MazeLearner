using MazeLearner;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MazeLeaner
{
    public class GameCursorState
    {
        private Main main;
        public GameCursorState(Main main)
        {
            this.main = main;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (GameSettings.CustomCursor)
            {
                Vector2 position = new Vector2(Main.Mouse.Position.X - (AssetsLoader.CursorTexture.Value.Width / 2), Main.Mouse.Position.Y - (AssetsLoader.CursorTexture.Value.Height / 2));
                spriteBatch.Draw(AssetsLoader.CursorTexture.Value, position, Color.White);
            }
        }
        public void Update(GameTime gameTime)
        {

        }
    }
}
