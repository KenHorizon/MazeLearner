using MazeLeaner.Text;
using MazeLearner.GameContent.BattleSystems.Questions;
using MazeLearner.GameContent.Entity.Monster;
using MazeLearner.GameContent.Entity.Player;
using MazeLearner.Localization;
using MazeLearner.Screen.Widgets;
using MazeLearner.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using MazeLearner.Screen.Components;

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
        public SubjectEntity npc;
        public PlayerEntity player;
        public Random random = new Random();
        public Rectangle DialogBox;
        private SubjectQuestions PrevQuestion;
        public BattleScreen(SubjectEntity battler, PlayerEntity player, SubjectQuestions PrevQuestion = null, BattleSystemSequence systemSequence = BattleSystemSequence.Menu) : base("")
        { 
            this.PrevQuestion = PrevQuestion;
            this.SystemSequence = systemSequence;
            this.npc = battler;
            this.player = player;
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
            int entryMenuX = 40;
            int entryMenuY = this.game.GetScreenHeight() - QBPH;
            int DialogBoxH = 240;
            this.DialogBox = new Rectangle(0, this.game.GetScreenHeight() - DialogBoxH, this.game.GetScreenWidth(), DialogBoxH);

            if (this.PrevQuestion == null)
            {
                this.Questions = npc.Questionaire[random.Next(npc.Questionaire.Length)];
                this.PrevQuestion = this.Questions;
                this.Questions.Randomized();
            }
            else
            {
                this.Questions = this.PrevQuestion;
            }
            if (this.SystemSequence == BattleSystemSequence.Fight)
            {
                entryMenuY -= 60;
                this.EntryMenus.Add(new MenuEntry(0, this.Questions.Answers()[0], new Rectangle(entryMenuX, entryMenuY, QBPW, QBPH), () =>
                {
                    bool flag = this.Questions.CorrectAnswer() == this.Questions.Answers()[0];
                    this.BattleImplement(flag);
                }));
                entryMenuX += 260;
                this.EntryMenus.Add(new MenuEntry(1, this.Questions.Answers()[1], new Rectangle(entryMenuX, entryMenuY, QBPW, QBPH), () =>
                {
                    bool flag = this.Questions.CorrectAnswer() == this.Questions.Answers()[1];
                    this.BattleImplement(flag);
                }));
                entryMenuX += 260;
                this.EntryMenus.Add(new MenuEntry(2, this.Questions.Answers()[2], new Rectangle(entryMenuX, entryMenuY, QBPW, QBPH), () =>
                {
                    bool flag = this.Questions.CorrectAnswer() == this.Questions.Answers()[2];
                    this.BattleImplement(flag);
                }));
                entryMenuX += 260;
                this.EntryMenus.Add(new MenuEntry(3, this.Questions.Answers()[3], new Rectangle(entryMenuX, entryMenuY, QBPW, QBPH), () =>
                {
                    bool flag = this.Questions.CorrectAnswer() == this.Questions.Answers()[3];
                    this.BattleImplement(flag);
                }));
            }
            if (this.SystemSequence == BattleSystemSequence.Menu)
            {
                entryMenuY -= 60;
                this.EntryMenus.Add(new MenuEntry(0, Resources.DoFight, new Rectangle(entryMenuX, entryMenuY, QBPW, QBPH), () =>
                {
                    this.game.SetScreen(new BattleScreen(this.npc, this.player, this.PrevQuestion, BattleSystemSequence.Fight));
                }));
                entryMenuX += 260;
                this.EntryMenus.Add(new MenuEntry(1, Resources.DoItem, new Rectangle(entryMenuX, entryMenuY, QBPW, QBPH), () =>
                {
                    this.game.SetScreen(new BattleScreen(this.npc, this.player, this.PrevQuestion));
                }));
                entryMenuX += 260;
                this.EntryMenus.Add(new MenuEntry(2, Resources.DoRunAway, new Rectangle(entryMenuX, entryMenuY, QBPW, QBPH), () =>
                {
                    Main.GameState = GameState.Play;
                    this.game.SetScreen(null);
                    this.npc.cooldownInteraction = 10;
                    this.player.DealDamage(1);
                }));
            }
        }

        private void BattleImplement(bool flag)
        {
            this.Questions.Randomized();
            this.PrevQuestion = this.Questions;
            if (flag == true)
            {
                int damage = 1;
                if (this.random.NextDouble() <= 0.25F)
                {
                    damage = 2;
                }
                if (this.npc.Health < 1)
                {
                    Main.GameState = GameState.Play;
                    this.game.SetScreen(null);
                }
                this.npc.DealDamage(1);
            }
            else
            {
                int damage = 1;
                if (this.random.NextDouble() <= 0.25F)
                {
                    damage = 2;
                }
                this.player.DealDamage(1);
                if (this.player.Health < 1)
                {
                    Main.GameState = GameState.Play;
                    this.game.SetScreen(null);
                }
            }
        }
        public override void Update(GameTime gametime)
        {
            base.Update(gametime);
            if (this.SystemSequence == BattleSystemSequence.Fight)
            {
                for (int i = 0; i < this.EntryMenus.Count; i++)
                {
                    this.EntryMenus[i].Text = this.Questions.Answers()[i];
                }
            }

            if (Main.Keyboard.Pressed(GameSettings.KeyBack) && this.SystemSequence != BattleSystemSequence.Menu)
            {
                this.PlaySoundClick();
                this.IndexBtn = 0;
                this.game.SetScreen(new BattleScreen(this.npc, this.player, this.PrevQuestion, BattleSystemSequence.Menu));
            }
            if (Main.Keyboard.Pressed(GameSettings.KeyLeft))
            {
                this.IndexBtn -= 1;
                this.PlaySoundClick();
                if (this.IndexBtn < 0)
                {
                    this.IndexBtn = this.EntryMenus.Count - 1;
                }
            }
            if (Main.Keyboard.Pressed(GameSettings.KeyRight))
            {
                this.IndexBtn += 1;
                this.PlaySoundClick();
                if (this.IndexBtn > this.EntryMenus.Count - 1)
                {
                    this.IndexBtn = 0;
                }
            }
        }
        public override void Render(SpriteBatch sprite, GraphicRenderer graphic)
        {
            base.Render(sprite, graphic);

            sprite.Draw(AssetsLoader.BattleBG_0.Value, this.game.WindowScreen);
            Vector2 battlerNameNHealth = new Vector2((this.game.WindowScreen.Width - (this.npc.BattleImage().Width * 2)) / 2, 140);
            TextManager.Text(Fonts.Normal, $"{npc.name}", battlerNameNHealth, Color.White);
            Vector2 battlerNameSize = TextManager.MeasureString(Fonts.DT_L, npc.name);
            graphic.RenderHeart(sprite, this.npc, (int)(battlerNameNHealth.X + battlerNameSize.X), (int)((int)battlerNameNHealth.Y - battlerNameSize.Y / 2) + 6);
            if (npc.BattleImage() != null)
            {
                sprite.Draw(npc.BattleImage(),
                    new Rectangle(
                        (int)battlerNameNHealth.X,
               (int)battlerNameNHealth.Y + npc.BattleImage().Height, npc.BattleImage().Width * 2, npc.BattleImage().Height * 2));

            }
            Vector2 playerNameNHealth = new Vector2(this.DialogBox.X + 12, this.DialogBox.Y - 24);
            Vector2 playerNameSize = TextManager.MeasureString(Fonts.DT_L, player.name);
            TextManager.Text(Fonts.Normal, $"{player.name}", playerNameNHealth, Color.White);
            graphic.RenderHeart(sprite, this.player, (int)(playerNameNHealth.X + playerNameSize.X), (int)playerNameNHealth.Y - 8);
            sprite.DrawMessageBox(AssetsLoader.MessageBox.Value, this.DialogBox, Color.White, 12);

            if (this.SystemSequence == BattleSystemSequence.Fight)
            {
                Vector2 textS = TextManager.MeasureString(Fonts.DT_L, this.Questions.GenerateDescriptions());
                TextManager.TextBox(Fonts.DT_L, this.Questions.GenerateDescriptions(), this.DialogBox,
                    new Vector2(GameSettings.DialogBoxPadding, 24), Color.Black);

            }
        }

        public override bool ShowOverlayKeybinds()
        {
            return false;
        }
    }
}
