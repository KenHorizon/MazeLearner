using MazeLeaner.Text;
using MazeLearner.GameContent.Entity;
using MazeLearner.GameContent.Entity.Player;
using MazeLearner.Screen.Widgets;
using MazeLearner.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MazeLearner.Screen
{
    public class BattleScreen : BaseScreen
    {
        private Assets<Texture2D> BattleBG_0 = Assets<Texture2D>.Request("Battle/BattleBG_0");
        private Assets<Texture2D> MB = Assets<Texture2D>.Request("UI/MessageBox");
        private Assets<Texture2D> HealthIcon = Assets<Texture2D>.Request("UI/Entity/Health");
        private QuestionButton StartButton;
        private QuestionButton SettingsButton;
        private QuestionButton CollectablesButton;
        private QuestionButton ExitButton;
        public NPC npc;
        public PlayerEntity player;
        public BattleScreen(NPC battler, PlayerEntity player) : base("")
        { 
            this.npc = battler;
            this.player = player;
        }
        public override void LoadContent()
        {
            base.LoadContent();
            int QBPH = 230;
            int entryMenuX = 40;
            int entryMenuY = this.game.GetScreenHeight() - QBPH;
            this.StartButton = new QuestionButton(entryMenuX, entryMenuY, 240, 40, () =>
            {
                Main.GameState = GameState.Play;
                this.game.SetScreen((BaseScreen)null);
            });
            this.StartButton.Text = "Start";
            entryMenuX += 260;
            this.CollectablesButton = new QuestionButton(entryMenuX, entryMenuY, 240, 40, () =>
            {

            });
            entryMenuY += 60;
            this.CollectablesButton.Text = "Collectables";
            this.SettingsButton = new QuestionButton(entryMenuX, entryMenuY, 240, 40, () =>
            {


            });
            entryMenuX -= 260;
            this.SettingsButton.Text = "Settings";
            this.ExitButton = new QuestionButton(entryMenuX, entryMenuY, 240, 40, () =>
            {
                this.game.QuitGame();
            });
            this.ExitButton.Text = "Exit";
            entryMenuY += 60;
            this.AddRenderableWidgets(this.StartButton);
            this.AddRenderableWidgets(this.CollectablesButton);
            this.AddRenderableWidgets(this.SettingsButton);
            this.AddRenderableWidgets(this.ExitButton);
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
            sprite.DrawSlice(MB.Value, questionBoxPlayer, Color.White, 16);
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
