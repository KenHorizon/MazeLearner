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
using System.Linq;
using MazeLearner.Graphics.Asset;

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
        private BaseSubject Questions;
        public NPC npc;
        public PlayerEntity player;
        public Random random = new Random();
        public Rectangle DialogBox;
        private BaseSubject PrevQuestion;
        private int damageTintDuration = 0;
        private TooltipComponents _tooltips0;
        private TooltipComponents _tooltips1; private bool _doshakescreen = false;
        private int _shakeTick = 0;
        private int _shakeDuration = 0;
        private float _shakeIntensity = 0;
        private Vector2 Position;
        public bool ShakeScreen
        {
            get { return _doshakescreen; }
            set { _doshakescreen = value; }
        }
        public float ShakeIntensity
        {
            get { return _shakeIntensity; }
            set { _shakeIntensity = value; }
        }
        public int ShakeDuration
        {
            get
            {
                return _shakeDuration;
            }
            set
            {
                _shakeDuration = value;
            }
        }
        public int ShakeTick
        {
            get
            {
                return _shakeTick;
            }
            set
            {
                _shakeTick = value;
            }
        }

        public BattleScreen(NPC battler, PlayerEntity player, BaseSubject PrevQuestion = null, BattleSystemSequence systemSequence = BattleSystemSequence.Menu) : base("")
        {
            this.PrevQuestion = PrevQuestion;
            this.SystemSequence = systemSequence;
            this.npc = battler;
            this.player = player;
            this.Questions = this.npc.Questionaire[random.Next(npc.Questionaire.Count - 1)];
            this.IndexBtn = 0;
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
            this._tooltips0 = new TooltipComponents(Fonts.Text);
            this._tooltips1 = new TooltipComponents(Fonts.Text);
            if (this.SystemSequence == BattleSystemSequence.Fight)
            {
                var questionnaireBox = new Rectangle(this.DialogBox.X + (this.DialogBox.Width / 2), this.DialogBox.Y, (this.DialogBox.Width / 2), this.DialogBox.Height);

                entryMenuX = questionnaireBox.X;
                entryMenuY = this.DialogBox.Y + 24;
                Vector2 var010 = Texts.MeasureString(Fonts.Text, this.Questions.Answers()[0]);
                int padding = (int) var010.Y + 32;

                this.EntryMenus.Add(new MenuEntry(0, "A. " + this.Questions.Answers()[0], new Rectangle(entryMenuX, entryMenuY, QBPW, QBPH), () =>
                {
                    bool flag = this.Questions.CorrectAnswer() == this.Questions.Answers()[0];
                    this.BattleImplement(flag);
                }, texture: AssetsLoader.SelectedBox.Value));
                entryMenuY += padding;

                this.EntryMenus.Add(new MenuEntry(1, "B. " + this.Questions.Answers()[1], new Rectangle(entryMenuX, entryMenuY, QBPW, QBPH), () =>
                {
                    bool flag = this.Questions.CorrectAnswer() == this.Questions.Answers()[1];
                    this.BattleImplement(flag);
                }, texture: AssetsLoader.SelectedBox.Value));
                entryMenuY += padding;

                this.EntryMenus.Add(new MenuEntry(2, "C. " + this.Questions.Answers()[2], new Rectangle(entryMenuX, entryMenuY, QBPW, QBPH), () =>
                {
                    bool flag = this.Questions.CorrectAnswer() == this.Questions.Answers()[2];
                    this.BattleImplement(flag);
                }, texture: AssetsLoader.SelectedBox.Value));
                entryMenuY += padding;

                this.EntryMenus.Add(new MenuEntry(3, "D. " + this.Questions.Answers()[3], new Rectangle(entryMenuX, entryMenuY, QBPW, QBPH), () =>
                {
                    bool flag = this.Questions.CorrectAnswer() == this.Questions.Answers()[3];
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
                    this.npc.cooldownInteraction = 10;
                    this.player.DealDamage(1);
                    if (this.player.Health <= 0)
                    {
                        Main.SoundEngine.Play(World.Get(Main.MapIds).Song);
                        Main.ActivePlayer.ScorePoints -= (this.npc.ScorePointDrops / 2);
                        this.game.SetScreen(null);
                        Main.ActivePlayer.PlayerWon = false;
                    }
                }, fontStyle: Fonts.Dialog));
            }

            if (this.PrevQuestion == null)
            {
                this.Questions = this.npc.Questionaire[random.Next(npc.Questionaire.Count)];
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
            int damage = this.random.NextDouble() <= 0.25F ? 2 : 1;
            if (this.npc.IsBoss == true)
            {
                Main.TeacherQuestion.Add(this.Questions);
            }
            if (flag == true)
            {
                Main.SoundEngine.Play(AudioAssets.HitSFX.Value);
                this.npc.DealDamage(1);
                this.damageTintDuration = 10;
                if (this.npc.Health <= 0)
                {
                    Main.SoundEngine.Play(World.Get(Main.MapIds).Song);
                    Main.ActivePlayer.ScorePoints += this.npc.ScorePointDrops;
                    this.game.SetScreen(null);
                    Main.GameState = GameState.Play;
                    Main.ActivePlayer.PlayerWon = true;
                    this.npc.Defeated = true;
                }
            }
            else
            {
                Main.SoundEngine.Play(AudioAssets.HitSFX.Value);
                this.player.DealDamage(1);
                if (this.player.Health <= 0)
                {
                    Main.SoundEngine.Play(World.Get(Main.MapIds).Song);
                    Main.ActivePlayer.ScorePoints -= (this.npc.ScorePointDrops / 2);
                    this.game.SetScreen(null);
                    Main.GameState = GameState.Play;
                }
            }
        }

        public override void Update(GameTime gametime)
        {
            base.Update(gametime);
            if (this.damageTintDuration > 0) this.damageTintDuration--;
            if (this.SystemSequence == BattleSystemSequence.Fight)
            {
                float player_hpFactor = ((float)Main.ActivePlayer.Health / Main.ActivePlayer.MaxHealth);
                if (player_hpFactor < 0.25F)
                {
                    Main.SoundEngine.Play(AudioAssets.Battle_LowHealth.Value, true);
                }
                this.EntryMenus[3].Text = "D. " + this.Questions.Answers()[3];
                this.EntryMenus[2].Text = "C. " + this.Questions.Answers()[2];
                this.EntryMenus[1].Text = "B. " + this.Questions.Answers()[1];
                this.EntryMenus[0].Text = "A. " + this.Questions.Answers()[0];
            }
        }

        protected override void EntryMenuIndex()
        {
            if (this.ShakeScreen == true)
            {
                this.ShakeTick++;
                this.Position += new Vector2(Main.Random.NextFloat(new FloatRange(-this.ShakeIntensity, this.ShakeIntensity)));
                if (this.ShakeTick > this.ShakeDuration)
                {
                    this.ShakeTick = 0;
                    this.ShakeIntensity = 0.0F;
                    this.ShakeScreen = false;
                }
            }
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
            float hpfactor = ((float) entity.Health / entity.MaxHealth);
            Rectangle healthbarOverlay = new Rectangle(healthbar.X + 2, healthbar.Y + 3,
                (int) (healthbar.Width * hpfactor), healthbar.Height);
            sprite.Draw(AssetsLoader.HealthBarOverlay.Value, healthbarOverlay);
            Texts.DrawString(Fonts.Text, $"{entity.DisplayName}", namePosition, Color.White);
            Texts.DrawString(Fonts.Text, $"HP: {entity.Health}", hpPosition, Color.White);
        }

        public override void RenderEntryMenus(SpriteBatch sprite)
        {
            if (this.SystemSequence == BattleSystemSequence.Fight)
            {
                var questionBox = new Rectangle(this.DialogBox.X, this.DialogBox.Y, this.DialogBox.Width / 2, this.DialogBox.Height);
                foreach (MenuEntry entries in this.EntryMenus)
                {
                    if (entries.IsActive == true)
                    {
                        int btnIndex = entries.Index;
                        string text = entries.Text.IsEmpty() ? "" : entries.Text;
                        bool isHovered = this.IndexBtn == btnIndex;
                        Vector2 textsize = Texts.MeasureString(Fonts.Text, text);
                        Rectangle dst = new Rectangle(entries.Box.X, (int)(entries.Box.Y - (textsize.Y / 2)), (int)(entries.Box.Width + (textsize.X / 2)), (int)(entries.Box.Height + (textsize.Y / 2)));

                        Vector2 textSize = Texts.MeasureString(Fonts.Text, entries.Text);
                        if (entries.Texture != null)
                        {
                            if (entries.Texture != null)
                            {
                                int textH = (int)(entries.Texture.Height);
                                bool flag = textH <= dst.Height;
                                if (flag == false)
                                {
                                    Rectangle src = new Rectangle(0, (entries.Texture.Height / 2) * (isHovered ? 1 : 0), entries.Box.Width, (int)(entries.Texture.Height / 2));
                                    sprite.Draw(entries.Texture, dst, src, Color.White);
                                }
                                else
                                {
                                    sprite.Draw(entries.Texture, dst, Color.White);
                                }
                            }
                        }
                        if (this.IndexBtn == btnIndex)
                        {
                            int y = entries.Text.IsEmpty() ? (entries.Box.Y + entries.Box.Height) / 2 : (int)(entries.Box.Y + ((dst.Height - textsize.Y - AssetsLoader.Arrow.Value.Height) / 2));
                            sprite.Draw(AssetsLoader.Arrow.Value, new Rectangle(entries.Box.X, y, AssetsLoader.Arrow.Value.Width, AssetsLoader.Arrow.Value.Height), Color.White);
                        }
                        int paddingText = isHovered ? 1 : 0;
                        if (entries.Text.IsEmpty() == false)
                        {
                            if (entries.Anchor == AnchorMainEntry.Center)
                            {
                                int x = (int)(dst.X + ((dst.Width - textSize.X) / 2));
                                int y = (int)(dst.Y + ((dst.Height - textsize.Y) / 2));
                                var rect = new Rectangle(x, y, questionBox.Width, 0);
                                Texts.DrawString(entries.FontStyle, text, new Vector2(x, y), entries.TextColor);
                            }
                            if (entries.Anchor == AnchorMainEntry.Left)
                            {
                                int x = dst.X + 20 + (AssetsLoader.Arrow.Value.Width * (isHovered ? 1 : 0));
                                int y = (int)(dst.Y + ((dst.Height - textsize.Y) / 2));
                                var rect = new Rectangle(x, y, questionBox.Width, 0);
                                Texts.DrawString(entries.FontStyle, text, new Vector2(x,y), entries.TextColor);
                            }
                            if (entries.Anchor == AnchorMainEntry.Right)
                            {
                                int x = (int)(dst.X + entries.Box.Width - (12 + textSize.X));
                                int y = (int)(dst.Y + ((dst.Height - textsize.Y) / 2));
                                var rect = new Rectangle(x, y, questionBox.Width, 0);
                                Texts.DrawString(entries.FontStyle, text, new Vector2(x, y), entries.TextColor);
                            }
                        }
                    }
                }
            }
            else
            {
                base.RenderEntryMenus(sprite);
            }
        }
        public override void Render(SpriteBatch sprite, Graphic graphic)
        {
            base.Render(sprite, graphic);
            float scale = 3.0F;
            sprite.Draw(AssetsLoader.BattleBG_0.Value, Main.WindowScreen);
            var questionBox = new Rectangle(this.DialogBox.X, this.DialogBox.Y, this.DialogBox.Width / 2, this.DialogBox.Height);
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
                Vector2 textS = Texts.MeasureString(Fonts.Text, this.Questions.GenerateDescriptions());
                Texts.DrawStringBox(Fonts.Text, this.Questions.GenerateDescriptions(), questionBox,
                    new Vector2(24, 24), Color.Black);
                var Tooltip0Size = Texts.MeasureString(Fonts.Text, this.Questions.Tooltip0());
                var Tooltip0Box = new Rectangle(this.DialogBox.X, 0 + this.DialogBox.Y - (160 + 12), (int)(this.DialogBox.Width * 0.70F), 142);
                if (this.Questions.Tooltip0().IsEmpty() == false)
                {
                    this._tooltips0.Width = Tooltip0Box.Width;
                    this._tooltips0.Position = Tooltip0Box.Vec2(10, 10);
                    this._tooltips0.Descriptions(this.Questions.Tooltip0());
                    this._tooltips0.Draw(sprite);
                    //sprite.NinePatch(AssetsLoader.Box4.Value, Tooltip0Box, Color.White, 12);
                    //Texts.DrawStringBox(Fonts.Text, this.Questions.Tooltip0(), Tooltip0Box,
                    //    new Vector2(24, 24), Color.Black);

                }
                var Tooltip1Size = Texts.MeasureString(Fonts.Text, this.Questions.Tooltip1());
                if (this.Questions.Tooltip1().IsEmpty() == false)
                {
                    this._tooltips1.Position = Tooltip0Box.Vec2(10, 10);
                    this._tooltips1.Descriptions(this.Questions.Tooltip1());
                    this._tooltips1.Draw(sprite);
                    int y1 =(this.Questions.Tooltip0().IsEmpty() ? 0 + this.DialogBox.Y - (140 + 12) : Tooltip0Box.Y + (140 + 12));
                    var Tooltip1Box = new Rectangle(this.DialogBox.X, y1, (int)(this.DialogBox.Width * 0.70F), 142);

                    //sprite.NinePatch(AssetsLoader.Box4.Value, Tooltip1Box, Color.White, 12);
                    //Texts.DrawStringBox(Fonts.Text, this.Questions.Tooltip1(), Tooltip1Box,
                    //    new Vector2(24, 24), Color.Black);
                }
            }
            float hpscale = 3.5F;
            int w = (int)(88 * hpscale);
            int h = (int)(44 * hpscale);
            int paddingBoxHp = 24;
            int x = 12;
            int y = 12;
            RenderUserStats(sprite, graphic, this.npc, new Vector2((Main.WindowScreen.Width - w) - paddingBoxHp, y), new Vector2(w, h));
            Vector2 playerPositionStat = new Vector2(x + paddingBoxHp, y);
            RenderUserStats(sprite, graphic, this.player, playerPositionStat + this.Position, new Vector2(w, h));

        }

        public override bool ShowOverlayKeybinds()
        {
            return false;
        }
    }
}
