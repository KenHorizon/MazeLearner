using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.Screen.Components
{
    public interface Renderables
    {
        void Draw(SpriteBatch sprite, Vector2 mouse);
    }
}
