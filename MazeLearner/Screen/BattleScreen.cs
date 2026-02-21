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
        private SubjectQuestions Questions;
        public NPC npc;
        public PlayerEntity player;
        public Random random = new Random();
        public Rectangle DialogBox;
        private SubjectQuestions PrevQuestion;
        public BattleScreen(NPC battler, PlayerEntity player, SubjectQuestions PrevQuestion = null, BattleSystemSequence systemSequence = BattleSystemSequence.Menu) : base("")
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

            // Hahahahahaha even changing this method
            // still work!!! ok let me explain, player are on the main menu and you choose the item
            // the answer still randomized!! and if player use item the player will take damage and the
            // question still gonna be randomized

            // Using Item take player 1 life
            // Running away 50% chance to take damage

            // Implementing if player choose to fight the answer will remain will not be randomized
            // if player still not pick the fight first it will be randomized
            // until they got damage, correct or use a item!

            int QBPW = 240;
            int QBPH = 40;
            int entryMenuXStart = 60;
            int entryMenuX = entryMenuXStart;
            int entryMenuYStart = (this.game.GetScreenHeight() - QBPH) - (this.DialogBox.Height / 2);
            int entryMenuY = entryMenuYStart;
            int DialogBoxH = 240;
            this.DialogBox = new Rectangle(0, this.game.GetScreenHeight() - DialogBoxH, this.game.GetScreenWidth(), DialogBoxH);

            if (this.SystemSequence == BattleSystemSequence.Fight)
            {
                entryMenuX = entryMenuXStart;
                entryMenuY = entryMenuYStart;
                Vector2 var010 = Texts.MeasureString(Fonts.Text, this.Questions.Answers()[0]);
                int padding = (int) var010.Y + 32;
                this.EntryMenus.Add(new MenuEntry(3, "D. " + this.Questions.Answers()[3], new Rectangle(entryMenuX, entryMenuY, QBPW, QBPH), () =>
                {
                    bool flag = this.Questions.CorrectAnswer() == this.Questions.Answers()[3];
                    this.BattleImplement(flag);
                }));
                entryMenuY -= padding;
                this.EntryMenus.Add(new MenuEntry(2, "C. " + this.Questions.Answers()[2], new Rectangle(entryMenuX, entryMenuY, QBPW, QBPH), () =>
                {
                    bool flag = this.Questions.CorrectAnswer() == this.Questions.Answers()[2];
                    this.BattleImplement(flag);
                }));
                entryMenuY -= padding;
                this.EntryMenus.Add(new MenuEntry(1, "B. " + this.Questions.Answers()[1], new Rectangle(entryMenuX, entryMenuY, QBPW, QBPH), () =>
                {
                    bool flag = this.Questions.CorrectAnswer() == this.Questions.Answers()[1];
                    this.BattleImplement(flag);
                }));
                entryMenuY -= padding;
                this.EntryMenus.Add(new MenuEntry(0, "A. " + this.Questions.Answers()[0], new Rectangle(entryMenuX, entryMenuY, QBPW, QBPH), () =>
                {
                    bool flag = this.Questions.CorrectAnswer() == this.Questions.Answers()[0];
                    this.BattleImplement(flag);
                }));
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
                    this.npc.cooldownInteraction = 10;
                    this.player.DealDamage(1);
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
                if (this.player.Health <= 0)
                {
                    Main.SoundEngine.Play(World.Get(Main.MapIds).Song);
                    Main.GetActivePlayer.ScorePoints -= (this.npc.ScorePointDrops / 2);
                    Main.GetActivePlayer.SpawnData();
                    this.game.SetScreen(null);
                    Main.GameState = GameState.Play;
                    Main.GetActivePlayer.PlayerWon = true;
                }
            }
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
        public override void Render(SpriteBatch sprite, Graphic graphic)
        {
            base.Render(sprite, graphic);
            sprite.Draw(AssetsLoader.BattleBG_0.Value, Main.WindowScreen);
            var questionBox = new Rectangle(this.DialogBox.X, 0, this.DialogBox.Width, this.DialogBox.Height);
            // Enemy:
            Vector2 battlerNameNHealth = new Vector2(20, questionBox.Height + 32);
            Texts.DrawString(Fonts.Dialog, $"{npc.DisplayName}", battlerNameNHealth, Color.White);
            Vector2 battlerNameSize = Texts.MeasureString(Fonts.Dialog, npc.DisplayName);
            graphic.RenderHeart(sprite, this.npc, (int)(battlerNameNHealth.X + battlerNameSize.X), (int) ((int)battlerNameNHealth.Y - battlerNameSize.Y / 2) + 14);
            // Player:
            Vector2 playerNameNHealth = new Vector2(this.DialogBox.X + 12, this.DialogBox.Y - 32);
            Vector2 playerNameSize = Texts.MeasureString(Fonts.Dialog, player.Name);
            Texts.DrawString(Fonts.Dialog, $"{player.DisplayName}", playerNameNHealth, Color.White);
            graphic.RenderHeart(sprite, this.player, (int)(playerNameNHealth.X + playerNameSize.X), (int) ((int) playerNameNHealth.Y - playerNameSize.Y / 2) + 14);
           
            if (this.SystemSequence == BattleSystemSequence.Fight)
            {
                sprite.NinePatch(AssetsLoader.Box1.Value, questionBox, Color.White, 12);
                Vector2 textS = Texts.MeasureString(Fonts.Dialog, this.Questions.GenerateDescriptions());
                Texts.DrawStringBox(Fonts.Dialog, this.Questions.GenerateDescriptions(), questionBox,
                    new Vector2(GameSettings.DialogBoxPadding, 24), Color.White);
            }
            if (npc.GetPortfolio() != null)
            {
                float scale = 3.0F;
                sprite.Draw(npc.GetPortfolio(),
                    new Rectangle(
                        (int)(Main.WindowScreen.Width - (this.npc.GetPortfolio().Width * scale)),
               Main.MaxTileSize * 3, (int)(npc.GetPortfolio().Width * scale), (int)(npc.GetPortfolio().Height * scale)));
            }

            sprite.NinePatch(AssetsLoader.MessageBox.Value, this.DialogBox, Color.White, 12);
        }

        public override bool ShowOverlayKeybinds()
        {
            return false;
        }
    }
}
