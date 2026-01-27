using MazeLearner.Screen.Widgets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MazeLearner.Screen
{
    /// <summary>
    /// Option Screen 
    /// 
    /// [Graphics]
    /// VSync
    /// FPS
    /// 
    /// [Audio]
    /// Master Volume
    /// Background Volume
    /// Effect Volume
    /// 
    /// [Accessibility]
    /// Keybinds
    /// Cursor
    /// 
    /// </summary>
    public class PlayerCreationScreen : BaseScreen
    {
        
        private SimpleButton BackButton;
        public PlayerCreationScreen() : base("")
        {
        }

        public override void LoadContent()
        {
        }

        public override void RenderBackground(SpriteBatch sprite)
        {
            base.RenderBackground(sprite);
        }
    }
}
