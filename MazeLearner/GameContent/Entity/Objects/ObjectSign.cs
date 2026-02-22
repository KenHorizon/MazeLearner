using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MazeLearner.GameContent.Entity.Objects
{
    public class ObjectSign : ObjectEntity
    {
        private string _message;
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }
        public override void Tick(GameTime gameTime)
        {
            base.Tick(gameTime);

        }
    }
}
