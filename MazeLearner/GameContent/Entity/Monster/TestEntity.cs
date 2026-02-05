using MazeLearner.GameContent.Entity.Player;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.GameContent.Entity.Monster
{
    public class TestEntity : SubjectEntity
    {

        public static Assets<Texture2D> Texture = Assets<Texture2D>.Request("NPC/TestSubject");
        public override void SetDefaults()
        {
            base.SetDefaults();
            this.name = "TestSubject";
            this.Health = 5;
            this.Damage = 1;
            this.Dialogs[0] = "You are ugly, Take this!";
            this.Dialogs[1] = "Fireball!";
        }
        public override Assets<Texture2D> GetTexture()
        {
            return Texture;
        }
    }
}
