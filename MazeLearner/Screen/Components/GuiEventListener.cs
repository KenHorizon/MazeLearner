using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.Screen.Components
{
    public interface GuiEventListener : TabOrderElement
    {
        bool MouseClicked(Vector2 mouse, MouseHandler handler)
        {
            return false;
        }

        bool OnMouseDragging(Vector2 mouse)
        {
            return false;
        }

        bool OnMouseReleased(Vector2 mouse)
        {
            return false;
        }

        void SetFocused(bool focused);

        void Update(GameTime gameTime);
        bool IsFocused();

        bool KeyPressed(KeyboardHandler key)
        {
            return false;
        }
    }
}
