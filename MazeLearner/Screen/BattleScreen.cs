using MazeLeaner.Text;
using MazeLearner.GameContent.BattleSystems.Questions;
using MazeLearner.GameContent.Entity;
using MazeLearner.GameContent.Entity.Monster;
using MazeLearner.GameContent.Entity.Player;
using MazeLearner.Screen.Widgets;
using MazeLearner.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Runtime.CompilerServices;

namespace MazeLearner.Screen
{
    public class BattleScreen : BaseScreen
    {
        private Assets<Texture2D> BattleBG_0 = Assets<Texture2D>.Request("Battle/BattleBG_0");
        private Assets<Texture2D> MB = Assets<Texture2D>.Request("UI/MessageBox");
        private Assets<Texture2D> HealthIcon = Assets<Texture2D>.Request("UI/Entity/Health");
        private QuestionButton Abutton;
        private QuestionButton Bbutton;
        private QuestionButton Cbutton;
        private QuestionButton Dbutton;
        private SubjectQuestions SubjectQuestions;
        public SubjectEntity npc;
        public PlayerEntity player;
        public Random random = new Random();
        public BattleScreen(SubjectEntity battler, PlayerEntity player) : base("")
        { 
            this.npc = battler;
            this.player = player;
        }
        public override void LoadContent()
        {
            base.LoadContent();
            this.SubjectQuestions = npc.Questionaire[random.Next(npc.Questionaire.Length)];
            this.SubjectQuestions.Randomized();
            int QBPH = 230;
            int entryMenuX = 40;
            int entryMenuY = this.game.GetScreenHeight() - QBPH;
            this.Abutton = new QuestionButton(entryMenuX, entryMenuY, 240, 40, () =>
            {
                bool flag = this.SubjectQuestions.CorrectAnswer() == this.SubjectQuestions.Answers()[0];
                this.BattleImplement(flag);
            });
            entryMenuX += 260;
            this.Bbutton = new QuestionButton(entryMenuX, entryMenuY, 240, 40, () =>
            {
                bool flag = this.SubjectQuestions.CorrectAnswer() == this.SubjectQuestions.Answers()[1];
                this.BattleImplement(flag);
            });
            entryMenuY += 60;
            this.Cbutton = new QuestionButton(entryMenuX, entryMenuY, 240, 40, () =>
            {
                bool flag = this.SubjectQuestions.CorrectAnswer() == this.SubjectQuestions.Answers()[2];
                this.BattleImplement(flag);
            });
            entryMenuX -= 260;
            this.Dbutton = new QuestionButton(entryMenuX, entryMenuY, 240, 40, () =>
            {
                bool flag = this.SubjectQuestions.CorrectAnswer() == this.SubjectQuestions.Answers()[3];
                this.BattleImplement(flag);
            });
            entryMenuY += 60;
            this.AddRenderableWidgets(this.Abutton);
            this.AddRenderableWidgets(this.Cbutton);
            this.AddRenderableWidgets(this.Bbutton);
            this.AddRenderableWidgets(this.Dbutton);
        }

        private void BattleImplement(bool flag)
        {
            if (flag == true)
            {
                this.SubjectQuestions.Randomized();
                if (this.npc.Health <= 0)
                {
                    Main.GameState = GameState.Play;
                    this.game.SetScreen(null);
                }
                this.npc.DealDamage(1);
            }
            else
            {
                this.SubjectQuestions.Randomized();
                player.DealDamage(1);
            }
        }

        public override void Update(GameTime gametime)
        {
            base.Update(gametime);
            if (this.Abutton != null || this.Bbutton != null || this.Cbutton != null || this.Dbutton != null)
            {
                this.Abutton.Text = this.SubjectQuestions.Answers()[0];
                this.Bbutton.Text = this.SubjectQuestions.Answers()[1];
                this.Cbutton.Text = this.SubjectQuestions.Answers()[2];
                this.Dbutton.Text = this.SubjectQuestions.Answers()[3];
            }

        }
        public override void Render(SpriteBatch sprite)
        {
            base.Render(sprite);
            sprite.Draw(BattleBG_0.Value, this.game.WindowScreen);
            Vector2 battlerNameNHealth = new Vector2(60, 20);
            TextManager.Text(Fonts.Normal, $"{npc.langName}", battlerNameNHealth);
            this.RenderHeart(sprite, this.npc, battlerNameNHealth + TextManager.MeasureString(Fonts.Normal, npc.langName));

            Vector2 playerNameNHealth = new Vector2(60, 280);
            TextManager.Text(Fonts.Normal, $"{player.langName}", playerNameNHealth);
            this.RenderHeart(sprite, this.player, playerNameNHealth + TextManager.MeasureString(Fonts.Normal, player.langName));

            int QBPW = this.game.GetScreenWidth() / 2;
            int QBPH = 240;
            Rectangle questionBoxPlayer = new Rectangle(QBPW, this.game.GetScreenHeight() - QBPH, this.game.GetScreenWidth() - QBPW, QBPH);
            sprite.DrawMessageBox(MB.Value, questionBoxPlayer, Color.White, 12);
            TextManager.CenteredText(Fonts.DT_L, this.SubjectQuestions.GenerateDescriptions(), questionBoxPlayer, Color.Black);
            //sprite.DrawFillRectangle(questionBoxPlayer, Color.White, Color.Black * 0.5F);
        }
        public void RenderHeart(SpriteBatch sprite, NPC npc, Vector2 position)
        {
            float health = npc.Health;
            // Image Position and Size 
            int x = (int)position.X + 10;
            int y = (int)position.Y - this.HealthIcon.Value.Height;
            for (int i = 0; i < health; i++)
            {
                Rectangle size = new Rectangle(x, y, this.HealthIcon.Value.Width, this.HealthIcon.Value.Height);
                sprite.Draw(this.HealthIcon.Value, size, Color.White);
                x += this.HealthIcon.Value.Width;
                //if (i % 10 == 0)
                //{
                //    y+= this.HealthIcon.Value.Height;
                //}
            }
        }
    }
}
