using MazeLeaner.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.Graphics.Cutscenes
{
    public class IntroCutscene : Cutscene
    {
        string phase1 = "Year 1629, The planet earth are inhabitant of different races, humans, elf, dwarfes and many more, these races live in harmony and peace";
        string phase2 = "suddenly a dark forces arrive trembling all nation of races and destorying there homes and everything they have, the war last for 30 years";
        string phase3 = "so many people fallen in war and one day a hero arrive, with the hero's subordinate they able to fight back with the dark forces and the";
        string phase4 = "ended the war, but the hero fade before he fall the hero use last resort sealing everyone with different realms.";
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (this.Scene == 5)
            {
                this.IsFinished = true;
                
            }
        }

        public override void Draw(SpriteBatch sprite, Graphic graphic)
        {
            base.Draw(sprite, graphic);
            sprite.Draw(AssetsLoader.IntroOverlay.Value, Main.WindowScreen);
            if (this.Scene == 1)
            {
                graphic.RenderTransparentDialogs(sprite, phase1);
            }
            if (this.Scene == 2)
            {
                graphic.RenderTransparentDialogs(sprite, phase2);
            }
            if (this.Scene == 3)
            {
                graphic.RenderTransparentDialogs(sprite, phase3);
            }
            if (this.Scene == 4)
            {
                graphic.RenderTransparentDialogs(sprite, phase4);
            }
        }
    }
}
