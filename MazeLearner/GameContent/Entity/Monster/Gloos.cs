using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.GameContent.Entity.Monster
{
    public class Gloos : EnglishMonster
    {
        public static Assets<Texture2D> Texture = Assets<Texture2D>.Request("NPC/Gloos");
        public override void SetDefaults()
        {
            base.SetDefaults();
            this.langName = "Gloos";
            this.Health = 2;
            this.MaxHealth = 2;
            this.Damage = 1;
            this.Dialogs[0] = "Arrghhh!";
        }
        public override Assets<Texture2D> GetTexture()
        {
            return Texture;
        }
    }
}
