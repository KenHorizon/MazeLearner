using MazeLearner.GameContent.BattleSystems.Questions;
using MazeLearner.GameContent.BattleSystems.Questions.Math;
using MazeLearner.GameContent.Entity.Player;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.GameContent.Entity.Monster
{
    public class MathTestEntity : MathMonster
    {

        public static Assets<Texture2D> Texture = Assets<Texture2D>.Request("NPC/MathSubject");
        public override void SetDefaults()
        {
            base.SetDefaults();
            this.langName = "MathSubject";
            this.Health = 5;
            this.Damage = 1;
            this.Dialogs[0] = "You look tough let's fight";
            this.Dialogs[1] = "Let's see how good you are";
            this.Dialogs[2] = "in MATH!!!!!!!!!";
        }
        public override Assets<Texture2D> GetTexture()
        {
            return Texture;
        }
    }
}
