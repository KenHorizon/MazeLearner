using MazeLearner.Audio;
using MazeLearner.Graphics;
using MazeLearner.Graphics.Animation;
using MazeLearner.Graphics.Asset;
using MazeLearner.Graphics.Particle;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.Screen
{
    public class CutsceneScreen : BaseScreen
    {
        bool debugLine = true;
        private int TimerEnd = 0;
        private int TimerNext = 2;
        private int Scene = 0;
        private int Phase = 0;
        private double Timer = 9;
        private int delayMs = 0;
        private Parallax busScene;
        private Parallax busSceneCloud0;
        private Parallax busSceneCloud1;
        private Action _onEnd;
        private int update = 0;

        string phase1 = "Year 1629, The planet earth are inhabitant of different races, humans, elf, dwarfes and many more, these races live in harmony and peace";
        string phase2 = "suddenly a dark forces arrive trembling all nation of races and destorying there homes and everything they have, the war last for 30 years";
        string phase3 = "so many people fallen in war and one day a hero arrive, with the hero's subordinate they able to fight back with the dark forces and the";
        string phase4 = "ended the war, but the hero fade before he fall the hero use last resort sealing everyone with different realms.";
        
        string mom1 = $"name=Mom:There you are {Main.ActivePlayer?.DisplayName}!";
        string mom2 = $"name=Mom:Hurry up!, the bus is waiting for you!";

        string staff1 = $"name=Staff: Hi kid!, This bus lead to Cupang Elementary School!";
        string staff2 = $"name=Player.Name: Is this the bus that lead on Cupand Elementary School?";
        string staff3 = $"name=Staff: Yes kiddo!";
        string staff4 = $"name=Player.Name: Woah! that's nice!";

        string ply001 = $"name=???: The trial will begin...";
        string ply002 = $"name=???: The forsaken truth will unravel of the truth of this world";

        private float flashTick = 0.0F;
        private float flashDurationEnd = 10.0F;
        public CutsceneScreen(int scene, Action onEnd) : base("")
        {
            this.Scene = scene;
            this._onEnd = onEnd;
            this.Timer = 0;
            this.update = 0;
            this.Phase = 0;
        }
        public override void LoadContent()
        {
            base.LoadContent();
            if (this.Scene == 0)
            {
                this.TimerNext = 2;
                this.TimerEnd = 4;
            }
            if (this.Scene == 1)
            {
                this.TimerNext = 5;
                this.busScene = new Parallax(AssetsLoader.BusCutsceneBackground0.Value, 100.0F);
                this.busSceneCloud0 = new Parallax(AssetsLoader.BusCutsceneBackground1.Value, 50.0F);
                this.busSceneCloud1 = new Parallax(AssetsLoader.BusCutsceneBackground1.Value, 25.0F);
            }
            if (this.Scene == 2)
            {
                this.TimerEnd = 4;
            }
            if (this.Scene == 3)
            {
                this.TimerNext = 2;
                this.TimerEnd = 4;
            }
            if (this.Scene == 4)
            {
                this.TimerEnd = 3;
            }
            if (this.Scene == 5)
            {
                this.TimerEnd = 2;
            }
        }
        public override void Update(GameTime gametime)
        {
            base.Update(gametime);
            if (this.debugLine)
            {
                Loggers.Info($"Scene:{this.Scene} Phase {this.Phase} Update: {this.update}");
            }
            this.Timer += gametime.ElapsedGameTime.TotalSeconds;
            this.update++;
            if (this.delayMs > 0)
            {
                this.delayMs--;
            }
            if (this.Scene == 0)
            {
                if (this.Phase == 0)
                {
                    if (this.update == 1)
                    {
                        Main.SoundEngine.Play(AudioAssets.Intro0.Value);
                    }
                }
                if (this.Phase == 1)
                {
                    if (this.update == 1)
                    {
                        Main.SoundEngine.Play(AudioAssets.Intro1.Value);
                    }
                }
                if (this.Phase == 2)
                {
                    if (this.update == 1)
                    {
                        Main.SoundEngine.Play(AudioAssets.Intro2.Value);
                    }
                }
                if (this.Phase == 3)
                {
                    if (this.update == 1)
                    {
                        Main.SoundEngine.Play(AudioAssets.Intro3.Value);
                    }
                }

                if (this.delayMs <= 0 && (Main.Input.Pressed(GameSettings.KeyInteract) || Main.Input.Pressed(GameSettings.KeyConfirm)))
                {
                    Main.SoundEngine.Play(AudioAssets.ClickedSFX.Value);
                    this.SplashStepNext();
                }
                if (this.Phase >= this.TimerEnd)
                {
                    this._onEnd?.Invoke();
                }
            }
            if (this.Scene == 1)
            {
                this.busScene.Update(gametime);
                this.busSceneCloud0.Update(gametime);
                this.busSceneCloud0.Update(gametime);
                if (this.Timer > TimerNext)
                {
                    this._onEnd?.Invoke();
                }
            }
            if (this.Scene == 2)
            {
                var momNpc = Main.FindNpc(0, 0);
                if (this.Phase == 0)
                {
                    if (this.update == 2)
                    {
                        momNpc.MoveTo(Main.ActivePlayer);
                    }
                    if (momNpc.GoalReached == true)
                    {
                        momNpc.FacingAt(Main.ActivePlayer);
                        this.SplashStepNext();
                    }
                }
                if (this.Phase == 3)
                {
                    if (this.update == 2)
                    {
                        momNpc.MoveTo(10, 18);
                    }
                    if (momNpc.GoalReached == true)
                    {
                        momNpc.Direction = Direction.Up;
                        this.SplashStepNext();
                    }
                }
                if (this.Phase == 1 || this.Phase == 2)
                {
                    if (this.delayMs <= 0 && (Main.Input.Pressed(GameSettings.KeyInteract) || Main.Input.Pressed(GameSettings.KeyConfirm)))
                    {
                        Main.SoundEngine.Play(AudioAssets.ClickedSFX.Value);
                        this.SplashStepNext();
                    }
                }
                if (this.Phase >= this.TimerEnd)
                {
                    this._onEnd?.Invoke();
                }
            }
            if (this.Scene == 3)
            {
                if (this.delayMs <= 0 && (Main.Input.Pressed(GameSettings.KeyInteract) || Main.Input.Pressed(GameSettings.KeyConfirm)))
                {
                    Main.SoundEngine.Play(AudioAssets.ClickedSFX.Value);
                    this.SplashStepNext();
                }
                if (this.Phase >= this.TimerEnd)
                {
                    this._onEnd?.Invoke();
                }
            }
            if (this.Scene == 4)
            {
                if (this.Phase == 0)
                {
                    var grd = Main.FindNpc(2, 13);
                    grd.SetPos(71, 29);
                    this.SplashStepNext();
                }
                if (this.Phase > 0)
                {
                    if (this.delayMs <= 0 && (Main.Input.Pressed(GameSettings.KeyInteract) || Main.Input.Pressed(GameSettings.KeyConfirm)))
                    {
                        Main.SoundEngine.Play(AudioAssets.ClickedSFX.Value);
                        this.SplashStepNext();
                    }
                    if (this.Phase >= this.TimerEnd)
                    {
                        this._onEnd?.Invoke();
                    }
                }
            }
            if (this.Scene == 5)
            {
                Main.Camera.Move(new Vector2(0, -1));
                var teacher = Main.FindNpc(3, 8);

                if (this.Phase == 0)
                {
                    this.SplashStepNext();
                }
                if (this.Phase == 1)
                {
                    if (this.update == 1)
                    {

                        teacher.SetPos(13, 20);
                        teacher.Direction = Direction.Right;
                        teacher.MoveTo(new Vector2(649, 269));
                        Main.ActivePlayer.Direction = Direction.Up;
                        for (int i = 0; i < Main.Npcs[1].Length; i++)
                        {
                            if (Main.Npcs[3][i] != null)
                            {
                                Main.Npcs[3][i].Direction = Direction.Up;
                            }
                        }
                    }
                }
                if (this.delayMs <= 0 && (Main.Input.Pressed(GameSettings.KeyInteract) || Main.Input.Pressed(GameSettings.KeyConfirm)))
                {
                    Main.SoundEngine.Play(AudioAssets.ClickedSFX.Value);
                    this.SplashStepNext();
                }
                if (this.Phase >= this.TimerEnd)
                {
                    this._onEnd?.Invoke();
                }
            }
        }
        private void SplashStepNext(int forceIt = 0)
        {
            if (forceIt == 0)
            {
                this.Phase++;
            }
            else
            {
                this.Phase = forceIt;
            }
            this.Timer = 0;
            this.delayMs = 10;
            this.update = 0;
        }

        public override void Render(SpriteBatch sprite, Graphic graphic)
        {
            if (this.Scene == 0)
            {
                if (this.Phase == 0)
                {
                    graphic.RenderTransparentDialogs(sprite, phase1);
                }
                if (this.Phase == 1)
                {
                    graphic.RenderTransparentDialogs(sprite, phase2);
                }
                if (this.Phase == 2)
                {
                    graphic.RenderTransparentDialogs(sprite, phase3);
                }
                if (this.Phase == 3)
                {
                    graphic.RenderTransparentDialogs(sprite, phase4);
                }

            }
            if (this.Scene == 1)
            {
                sprite.Draw(AssetsLoader.BusCutscene.Value, new Rectangle(
                    (Main.WindowScreen.Width - AssetsLoader.BusCutscene.Value.Width) / 2,
                    (Main.WindowScreen.Height - AssetsLoader.BusCutscene.Value.Height) / 2,
                    Main.WindowScreen.Width, Main.WindowScreen.Height));
            }
            if (this.Scene == 2)
            {
                if (this.Phase == 1)
                {
                    graphic.RenderDialogs(sprite, mom1);
                }
                if (this.Phase == 2)
                {
                    graphic.RenderDialogs(sprite, mom2);
                }
            }
            if (this.Scene == 3)
            {
                if (this.Phase == 0)
                {
                    graphic.RenderDialogs(sprite, staff1);
                }
                if (this.Phase == 1)
                {
                    graphic.RenderDialogs(sprite, staff2);
                }
                if (this.Phase == 2)
                {
                    graphic.RenderDialogs(sprite, staff3);
                }
                if (this.Phase == 3)
                {
                    graphic.RenderDialogs(sprite, staff4);
                }
            }
            if (this.Scene == 4)
            {
                if (this.Phase == 1)
                {
                    graphic.RenderDialogs(sprite, ply001);
                }
                if (this.Phase == 2)
                {
                    graphic.RenderDialogs(sprite, ply002);
                }
            }
        }
        public override bool ShowOverlayKeybinds()
        {
            return false;
        }
        public override void RenderBackground(SpriteBatch sprite, Graphic graphic)
        {
            base.RenderBackground(sprite, graphic);
            if (this.Scene == 0)
            {

                sprite.Draw(AssetsLoader.IntroOverlay.Value, Main.WindowScreen);
                if (this.Phase == 0)
                {
                    sprite.Draw(AssetsLoader.Intro0.Value, Main.WindowScreen);
                }
                if (this.Phase == 1)
                {
                    sprite.Draw(AssetsLoader.Intro1.Value, Main.WindowScreen);
                }
                if (this.Phase == 2)
                {
                    sprite.Draw(AssetsLoader.Intro2.Value, Main.WindowScreen);
                }

                if (this.Phase == 3)
                {
                    sprite.Draw(AssetsLoader.Intro3.Value, Main.WindowScreen);
                }
            }
            if (this.Scene == 1)
            {
                this.busScene.Draw(sprite);
                this.busSceneCloud0.Draw(sprite);
                this.busSceneCloud1.Draw(sprite);
            }
            if (this.Scene == 4)
            {
                sprite.Screen(Color.Black);
            }
        }
    }
}
