using MazeLeaner.Text;
using MazeLearner.GameContent.BattleSystems.Questions;
using MazeLearner.GameContent.Entity.Player;
using MazeLearner.Localization;
using MazeLearner.Screen.Widgets;
using MazeLearner.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using MazeLearner.Screen.Components;
using MazeLearner.GameContent.Entity;
using MazeLearner.Graphics;
using MazeLearner.Audio;
using MazeLearner.Worlds;

namespace MazeLearner.Screen
{
    [Flags]
    public enum BattleSystemSequence
    {
        Menu,
        Fight,
        Item,
        Run
    }
    public class BattleScreen : BaseScreen
    {
        public BattleSystemSequence SystemSequence = BattleSystemSequence.Menu;
        public static int QuestionIndex = 0;
        public static int ChoicesIndex = 4;
        private QuestionButton EndButton;
        private QuestionButton AutoWinButton;
        private BaseSubject Questions;
        public NPC npc;
        public PlayerEntity player;
        public Random random = new Random();
        public Rectangle DialogBox;
        private BaseSubject PrevQuestion;
        private int damageTintDuration = 0;
        
        public BattleScreen(NPC battler, PlayerEntity player, BaseSubject PrevQuestion = null, BattleSystemSequence systemSequence = BattleSystemSequence.Menu) : base("")
        {
            this.PrevQuestion = PrevQuestion;
            this.SystemSequence = systemSequence;
            this.npc = battler;
            this.player = player;
            this.Questions = this.npc.Questionaire[random.Next(npc.Questionaire.Length)];
        }
        public override void LoadContent()
        {
            base.LoadContent();
            int QBPW = 240;
            int QBPH = 40;
            int entryMenuXStart = 60;
            int entryMenuX = entryMenuXStart;
            int entryMenuYStart = (Main.WindowScreen.Height - QBPH) - (this.DialogBox.Height / 2);
            int entryMenuY = entryMenuYStart;
            int DialogBoxH = 240;
            this.DialogBox = new Rectangle(0, Main.WindowScreen.Height - DialogBoxH, Main.WindowScreen.Width, DialogBoxH);

            if (this.SystemSequence == BattleSystemSequence.Fight)
            {
                var questionnaireBox = new Rectangle(this.DialogBox.X + (this.DialogBox.Width / 2), this.DialogBox.Y, (this.DialogBox.Width / 2), this.DialogBox.Height);

                entryMenuX = questionnaireBox.X;
                entryMenuY = entryMenuYStart - 20;
                Vector2 var010 = Texts.MeasureString(Fonts.Text, this.Questions.Answers()[0]);
                int padding = (int) var010.Y + 32;
                this.EntryMenus.Add(new MenuEntry(3, "D. " + this.Questions.Answers()[3], new Rectangle(entryMenuX, entryMenuY, QBPW, QBPH), () =>
                {
                    bool flag = this.Questions.CorrectAnswer() == this.Questions.Answers()[3];
                    this.BattleImplement(flag);
                }, texture: AssetsLoader.SelectedBox.Value));
                entryMenuY -= padding;
                this.EntryMenus.Add(new MenuEntry(2, "C. " + this.Questions.Answers()[2], new Rectangle(entryMenuX, entryMenuY, QBPW, QBPH), () =>
                {
                    bool flag = this.Questions.CorrectAnswer() == this.Questions.Answers()[2];
                    this.BattleImplement(flag);
                }, texture: AssetsLoader.SelectedBox.Value));
                entryMenuY -= padding;
                this.EntryMenus.Add(new MenuEntry(1, "B. " + this.Questions.Answers()[1], new Rectangle(entryMenuX, entryMenuY, QBPW, QBPH), () =>
                {
                    bool flag = this.Questions.CorrectAnswer() == this.Questions.Answers()[1];
                    this.BattleImplement(flag);
                }, texture: AssetsLoader.SelectedBox.Value));
                entryMenuY -= padding;
                this.EntryMenus.Add(new MenuEntry(0, "A. " + this.Questions.Answers()[0], new Rectangle(entryMenuX, entryMenuY, QBPW, QBPH), () =>
                {
                    bool flag = this.Questions.CorrectAnswer() == this.Questions.Answers()[0];
                    this.BattleImplement(flag);
                }, texture: AssetsLoader.SelectedBox.Value));
            }
            if (this.SystemSequence == BattleSystemSequence.Menu)
            {
                entryMenuY -= 60;
                this.EntryMenus.Add(new MenuEntry(0, Resources.DoFight, new Rectangle(entryMenuX, entryMenuY, QBPW, QBPH), () =>
                {
                    this.game.SetScreen(new BattleScreen(this.npc, this.player, this.PrevQuestion, BattleSystemSequence.Fight));
                }, fontStyle: Fonts.Dialog));
                entryMenuX += 260;
                this.EntryMenus.Add(new MenuEntry(1, Resources.DoItem, new Rectangle(entryMenuX, entryMenuY, QBPW, QBPH), () =>
                {
                    this.game.SetScreen(new BattleScreen(this.npc, this.player, this.PrevQuestion));
                }, fontStyle: Fonts.Dialog));
                entryMenuX += 260;
                this.EntryMenus.Add(new MenuEntry(2, Resources.DoRunAway, new Rectangle(entryMenuX, entryMenuY, QBPW, QBPH), () =>
                {
                    Main.GameState = GameState.Play;
                    Main.SoundEngine.Play(World.Get(Main.MapIds).Song);
                    this.game.SetScreen(null);
                    this.player.cooldownInteraction = 10;
                    this.player.DealDamage(1);
                    if (this.player.Health <= 0)
                    {
                        Main.SoundEngine.Play(World.Get(Main.MapIds).Song);
                        Main.GetActivePlayer.ScorePoints -= (this.npc.ScorePointDrops / 2);
                        this.game.SetScreen(null);
                        Main.GetActivePlayer.PlayerWon = true;
                    }
                }, fontStyle: Fonts.Dialog));
            }

            if (this.PrevQuestion == null)
            {
                this.Questions = this.npc.Questionaire[random.Next(npc.Questionaire.Length)];
                this.PrevQuestion = this.Questions;
                this.Questions.Randomized();
            }
            else
            {
                this.Questions = this.PrevQuestion;
            }
        }

        private void BattleImplement(bool flag)
        {
            this.Questions.Randomized();
            this.PrevQuestion = this.Questions;
            this.EntryMenus[0].Text = "D. " + this.Questions.Answers()[0];
            this.EntryMenus[1].Text = "C. " + this.Questions.Answers()[1];
            this.EntryMenus[2].Text = "B. " + this.Questions.Answers()[2];
            this.EntryMenus[3].Text = "A. " + this.Questions.Answers()[3];
            int damage = this.random.NextDouble() <= 0.25F ? 2 : 1;
            if (flag == true)
            {
                Main.SoundEngine.Play(AudioAssets.HitSFX.Value);
                this.npc.DealDamage(1);
                this.damageTintDuration = 10;
                if (this.npc.Health <= 0)
                {
                    Main.SoundEngine.Play(World.Get(Main.MapIds).Song);
                    Main.GetActivePlayer.ScorePoints += this.npc.ScorePointDrops;
                    this.game.SetScreen(null);
                    Main.GameState = GameState.Play;
                    Main.GetActivePlayer.PlayerWon = true;
                }
            }
            else
            {
                Main.SoundEngine.Play(AudioAssets.HitSFX.Value);
                this.player.DealDamage(1);
                Main.FadeAwayBegin = true;
                Main.FadeAwayDuration = 20;
                if (this.player.Health <= 0)
                {
                    Main.SoundEngine.Play(World.Get(Main.MapIds).Song);
                    Main.GetActivePlayer.ScorePoints -= (this.npc.ScorePointDrops / 2);
                    this.game.SetScreen(null);
                    Main.GameState = GameState.Play;
                    Main.GetActivePlayer.PlayerWon = true;
                }
            }
        }

        public override void Update(GameTime gametime)
        {
            base.Update(gametime);
            if (this.damageTintDuration > 0) this.damageTintDuration--;
        }

        protected override void EntryMenuIndex()
        {
            if (this.SystemSequence == BattleSystemSequence.Menu)
            {
                if (Main.Input.Pressed(GameSettings.KeyBack))
                {
                    this.PlaySoundClick();
                    this.IndexBtn = 0;
                    this.game.SetScreen(new BattleScreen(this.npc, this.player, this.PrevQuestion, BattleSystemSequence.Menu));
                }
                if (Main.Input.Pressed(GameSettings.KeyLeft))
                {
                    this.IndexBtn -= 1;
                    this.PlaySoundClick();
                    if (this.IndexBtn < 0)
                    {
                        this.IndexBtn = this.EntryMenus.Count - 1;
                    }
                }
                if (Main.Input.Pressed(GameSettings.KeyRight))
                {
                    this.IndexBtn += 1;
                    this.PlaySoundClick();
                    if (this.IndexBtn > this.EntryMenus.Count - 1)
                    {
                        this.IndexBtn = 0;
                    }
                }
                if (Main.Input.Pressed(GameSettings.KeyInteract) || Main.Input.Pressed(GameSettings.KeyConfirm))
                {
                    foreach (MenuEntry entries in this.EntryMenus)
                    {
                        int btnIndex = entries.Index;
                        if (this.IndexBtn == btnIndex && entries.IsActive == true)
                        {
                            entries.Action?.Invoke();
                        }
                    }
                }
            }
            else if (this.SystemSequence == BattleSystemSequence.Fight)
            {
                if (Main.Input.Pressed(GameSettings.KeyBack))
                {
                    this.PlaySoundClick();
                    this.IndexBtn = 0;
                    this.game.SetScreen(new BattleScreen(this.npc, this.player, this.PrevQuestion, BattleSystemSequence.Menu));
                }
                if (Main.Input.Pressed(GameSettings.KeyForward))
                {
                    this.IndexBtn -= 1;
                    this.PlaySoundClick();
                    if (this.IndexBtn < 0)
                    {
                        this.IndexBtn = this.EntryMenus.Count - 1;
                    }
                }
                if (Main.Input.Pressed(GameSettings.KeyDownward))
                {
                    this.IndexBtn += 1;
                    this.PlaySoundClick();
                    if (this.IndexBtn > this.EntryMenus.Count - 1)
                    {
                        this.IndexBtn = 0;
                    }
                }
                if (Main.Input.Pressed(GameSettings.KeyBack))
                {
                    this.game.SetScreen(new BattleScreen(this.npc, this.player, this.PrevQuestion, BattleSystemSequence.Menu));
                }
                if (Main.Input.Pressed(GameSettings.KeyInteract) || Main.Input.Pressed(GameSettings.KeyConfirm))
                {
                    foreach (MenuEntry entries in this.EntryMenus)
                    {
                        int btnIndex = entries.Index;
                        if (this.IndexBtn == btnIndex && entries.IsActive == true)
                        {
                            entries.Action?.Invoke();
                        }
                    }
                }
            }
            else
            {
                base.EntryMenuIndex();
                if (Main.Input.Pressed(GameSettings.KeyBack))
                {
                    this.game.SetScreen(new BattleScreen(this.npc, this.player, this.PrevQuestion, BattleSystemSequence.Menu));
                }
            }
            
        }
        private void RenderUserStats(SpriteBatch sprite, Graphic graphic, NPC entity, Vector2 position, Vector2 size)
        {
            Vector2 textSize = Texts.MeasureString(Fonts.Text, entity.DisplayName);
            Rectangle box = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
            sprite.Draw(AssetsLoader.Box5.Value, box);
            Vector2 namePosition = new Vector2(box.X + 24, box.Y + 24);
            Rectangle healthbar = new Rectangle(box.X + 24, (int)(box.Y + (namePosition.Y * 2)), (int) (box.Width - 48), 16);
            Vector2 hpPosition = new Vector2(healthbar.X, healthbar.Y + 24);
            sprite.Draw(AssetsLoader.HealthBar.Value, healthbar);
            Rectangle healthbarOverlay = new Rectangle(healthbar.X + 2, healthbar.Y + 3, healthbar.Width, healthbar.Height);
            float hpfactor = ((float) entity.Health / entity.MaxHealth);
            Rectangle healthbarOverlaySrc = new Rectangle(healthbarOverlay.X, healthbarOverlay.Y,
                (int)((1.0F - hpfactor) * AssetsLoader.HealthBarOverlay.Value.Width), AssetsLoader.HealthBarOverlay.Value.Height);
            sprite.Draw(AssetsLoader.HealthBarOverlay.Value, healthbarOverlaySrc);
            Texts.DrawString(Fonts.Text, $"{entity.DisplayName}", namePosition, Color.White);
            Texts.DrawString(Fonts.Text, $"HP: {entity.Health}", hpPosition, Color.White);
        }

        public override void Render(SpriteBatch sprite, Graphic graphic)
        {
            base.Render(sprite, graphic);
            float scale = 3.0F;
            sprite.Draw(AssetsLoader.BattleBG_0.Value, Main.WindowScreen);
            var questionBox = new Rectangle(this.DialogBox.X, this.DialogBox.Y, this.DialogBox.Width / 2, this.DialogBox.Height);
            var Tooltip0Box = new Rectangle(this.DialogBox.X, 0, this.DialogBox.Width, this.DialogBox.Height);
            var Tooltip1Box = new Rectangle(this.DialogBox.X, 0 + Tooltip0Box.Y + 12, this.DialogBox.Width, this.DialogBox.Height);
            var portfolioBox = new Rectangle(
                        (int)(Main.WindowScreen.Width - (this.npc.GetPortfolio().Width * scale)),
                        Main.MaxTileSize * 3,
                        (int)(npc.GetPortfolio().Width * scale),
                        (int)(npc.GetPortfolio().Height * scale));
            if (npc.GetPortfolio() != null)
            {
                sprite.Draw(npc.GetPortfolio(), portfolioBox);
                if (this.damageTintDuration > 0 && this.damageTintDuration % 2 == 0)
                {
                    sprite.Draw(npc.GetPortfolio(), portfolioBox, Color.Red);
                }
            }

            sprite.NinePatch(AssetsLoader.MessageBox.Value, this.DialogBox, Color.White, 12);

            if (this.SystemSequence == BattleSystemSequence.Fight)
            {
                Vector2 textS = Texts.MeasureString(Fonts.Dialog, this.Questions.GenerateDescriptions());
                Texts.DrawStringBox(Fonts.Dialog, this.Questions.GenerateDescriptions(), questionBox,
                    new Vector2(24, 24), Color.Black);
                Texts.DrawStringBox(Fonts.Dialog, this.Questions.Tooltip0(), Tooltip0Box,
                    new Vector2(24, 24), Color.Black);
                Texts.DrawStringBox(Fonts.Dialog, this.Questions.Tooltip1(), Tooltip1Box,
                    new Vector2(24, 24), Color.Black);
            }
            float hpscale = 3.5F;
            int w = (int)(88 * hpscale);
            int h = (int)(44 * hpscale);
            int paddingBoxHp = 24;
            int x = 12;
            int y = 12;
            RenderUserStats(sprite, graphic, this.npc, new Vector2((Main.WindowScreen.Width - w) - paddingBoxHp, y), new Vector2(w, h));
            RenderUserStats(sprite, graphic, this.player, new Vector2(x + paddingBoxHp, y), new Vector2(w, h));

        }

        public override bool ShowOverlayKeybinds()
        {
            return false;
        }
    }
}
