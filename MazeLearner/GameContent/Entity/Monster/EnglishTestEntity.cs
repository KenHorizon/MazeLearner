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
    public class EnglishTestEntity : EnglishMonster
    {
        public static Assets<Texture2D> Texture = Assets<Texture2D>.Request("NPC/EnglishSubject");
        public override void SetDefaults()
        {
            base.SetDefaults();
            this.name = "EnglishSubject";
            this.Health = 5;
            this.Damage = 1;
            this.Dialogs[0] = "How about some EN!";
            this.Dialogs[1] = "ENGLISH TIME!!";
        }
        public override Assets<Texture2D> GetTexture()
        {
            return Texture;
        }
        public override Texture2D BattleImage()
        {
            return Assets<Texture2D>.Request($"Battle/Battler/{this.name}").Value;
        }
    }
}
