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
        public override void SetDefaults()
        {
            base.SetDefaults();
            this.CanCollide = true;
        }
    }
}
