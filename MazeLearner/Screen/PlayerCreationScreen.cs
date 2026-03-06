using MazeLeaner.Text;
using MazeLearner.GameContent.Entity.Player;
using MazeLearner.Graphics;
using MazeLearner.Graphics.Animation;
using MazeLearner.Graphics.Asset;
using MazeLearner.Localization;
using MazeLearner.Screen.Components;
using MazeLearner.Screen.Widgets;
using MazeLearner.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using static Assimp.Metadata;
using static System.Net.Mime.MediaTypeNames;

namespace MazeLearner.Screen
{
    public class PlayerCreationScreen : BaseScreen
    {
        public enum PlayerCreationState
        {
            Menu,
            Play,
            Create,
            GenderPicking,
            UsernameCreation,
            Confirmation
        }
        public PlayerCreationState State { get; set; }
        private Dictionary<int, MenuEntry> Saves = new Dictionary<int, MenuEntry>();
        private int boxPadding = 32;
        private int boxX = 0;
        private int boxY = 20;
        private int boxW
        {
            get
            {
                return Main.WindowScreen.Width;
            }
            set
            {
                this.boxW = value;
            }
        }
        private int boxH
        {
            get
            {
                return Main.WindowScreen.Height;
            }
            set
            {
                this.boxH = value;
            }
        }
        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle(this.boxX + boxPadding, this.boxY, boxW - (boxX + (boxPadding * 2)), boxH - (boxY + boxPadding));
            }
            set
            {
                this.boxX = value.X;
                this.boxY = value.Y;
                this.boxW = value.Width;
                this.boxH = value.Height;
            }
        }
        private const int saveSlotPadding = 40;
        private int saveSlotW
        {
            get
            {
                return this.BoundingBox.Width - saveSlotPadding;
            }
            set
            {
                this.saveSlotW = value;
            }
        }
        private int saveSlotH
        {
            get
            {
                return 120;
            }
            set
            {
                this.saveSlotH = value;
            }
        }
        private int saveSlotX = 0;
        private int saveSlotY = 20;
        public Rectangle SaveSlotBox
        {
            get
            {
                return new Rectangle(this.saveSlotX + ((this.BoundingBox.Width - this.saveSlotW) / 2), this.saveSlotY, saveSlotW - (saveSlotX + (saveSlotPadding * 2)), this.saveSlotH - saveSlotPadding);
            }
            set
            {
                this.saveSlotX = value.X;
                this.saveSlotY = value.Y;
                this.saveSlotW = value.Width;
                this.saveSlotH = value.Height;
            }
        }
        public Rectangle[] SaveSlotBoxs = new Rectangle[Main.maxLoadPlayer];
        private InputBox textbox;
        public PlayerCreationScreen(PlayerCreationState state = PlayerCreationState.Menu) : base("")
        {
            Loggers.Info($"Player List Index: {Main.PlayerListIndex}"); // will check if there's a player database stored!
            this.State = state;
        }
        public override void LoadContent()
        {
            Main.LoadPlayers();
            this.saveSlotX += 20;
            this.saveSlotY += 20;
            if (this.State == PlayerCreationState.Menu)
            {
                Main.LoadPlayers();
                int x = (Main.WindowScreen.Width - 240) / 2;
                int y = Main.WindowScreen.Height / 2 - 20;
                int boxW = 320;
                int boxH = 64;
                Rectangle ChooseBox0 = new Rectangle(x, y, boxW, boxH);
                Rectangle ChooseBox1 = new Rectangle(x, y + AssetsLoader.FemalePickBox.Value.Height + 20, boxW, boxH);
                this.EntryMenus.Add(new MenuEntry(0, Resources.Create, ChooseBox0, () =>
                {
                    this.game.SetScreen(new PlayerCreationScreen(PlayerCreationState.GenderPicking));
                }, AssetsLoader.Button0.Value, AnchorMainEntry.Center));
                this.EntryMenus.Add(new MenuEntry(1, Resources.PlayGame, ChooseBox1, () =>
                {
                   this.game.SetScreen(new PlayerCreationScreen(PlayerCreationState.Play));
                }, AssetsLoader.Button0.Value, AnchorMainEntry.Center));
            }
            if (this.State == PlayerCreationState.Play)
            {
                Main.LoadPlayers();
                for (int i = 0; i < Main.PlayerList.Length; i++)
                {
                    if (Main.PlayerList[i] != null)
                    { 
                        Rectangle entryBox = new Rectangle(this.SaveSlotBox.X + 20, (this.SaveSlotBox.Y + 20) + ((this.SaveSlotBox.Height + 12) * i), this.SaveSlotBox.Width, this.SaveSlotBox.Height);
                        Loggers.Info($"creating slot id:{i} box:{entryBox}");
                        var entry = new MenuEntry(i, "", entryBox, () =>
                        {
                            Main.PlayerListIndex = this.IndexBtn;
                            this.game.SetScreen(null);
                        });
                        this.Saves[i] = entry;
                        this.EntryMenus.Add(entry);
                    }
                }
            }
            if (this.State == PlayerCreationState.GenderPicking)
            {
                int x = (Main.WindowScreen.Width - 240) / 2;
                int y = Main.WindowScreen.Height / 2 - 20;
                int boxW = 320;
                int boxH = 64;
                Rectangle genderChooseBox0 = new Rectangle(x, y, boxW, boxH);
                Rectangle genderChooseBox1 = new Rectangle(x, y + AssetsLoader.FemalePickBox.Value.Height + 20, boxW, boxH);
                this.EntryMenus.Add(new MenuEntry(0, Resources.MaleButton, genderChooseBox0, () =>
                {
                    Main.PendingPlayer = new PlayerEntity();
                    Main.PendingPlayer.Gender = Gender.Male;
                    Main.PlayerList[Main.PlayerListIndex] = Main.PendingPlayer;
                    this.game.SetScreen(new PlayerCreationScreen(PlayerCreationState.UsernameCreation));
                }, AssetsLoader.MalePickBox.Value, AnchorMainEntry.Center));
                this.EntryMenus.Add(new MenuEntry(1, Resources.FemaleButton, genderChooseBox1, () =>
                {
                    Main.PendingPlayer = new PlayerEntity();
                    Main.PendingPlayer.Gender = Gender.Female;
                    Main.PlayerList[Main.PlayerListIndex] = Main.PendingPlayer;
                    this.game.SetScreen(new PlayerCreationScreen(PlayerCreationState.UsernameCreation));
                }, AssetsLoader.FemalePickBox.Value, AnchorMainEntry.Center));
            }
            if (this.State == PlayerCreationState.UsernameCreation)
            {
                this.textbox = new InputBox();
                this.textbox.LabelText = Resources.InsertName;
                this.textbox.SetFocused(true);
                this.AddRenderableWidgets(this.textbox);
            }
        }

        protected override void EntryMenuIndex()
        {
            if (this.State == PlayerCreationState.Play)
            {
                var box0 = this.Saves[this.IndexBtn == 0 ? 0 : this.IndexBtn - 1].Box;
                if (Main.Input.Pressed(GameSettings.KeyForward))
                {
                    if (this.BoundingBox.Contains(box0))
                    {
                        this.IndexBtn -= 1;
                        this.PlaySoundClick();
                    }
                }

                if (Main.Input.Pressed(GameSettings.KeyDownward))
                {
                    this.IndexBtn += 1;
                    this.PlaySoundClick();
                }
                this.IndexBtn = MathHelper.Clamp(this.IndexBtn, 0, this.Saves.Count - 1);
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
            }
        }

        #region Update
        public override void Update(GameTime gametime)
        {
            base.Update(gametime);
            if (Main.Input.Pressed(GameSettings.KeyBack) && this.State == PlayerCreationState.Menu)
            {
                this.game.SetScreen(new TitleScreen(TitleSequence.Title));
            }
            if (Main.Input.Pressed(GameSettings.KeyBack) && this.State == PlayerCreationState.Play)
            {
                this.game.SetScreen(new PlayerCreationScreen(PlayerCreationState.Menu));
            }
            if (Main.Input.Pressed(GameSettings.KeyBack) && this.State == PlayerCreationState.Create)
            {
                this.game.SetScreen(new PlayerCreationScreen(PlayerCreationState.Menu));
            }
            if (Main.Input.Pressed(GameSettings.KeyBack) && this.State == PlayerCreationState.GenderPicking)
            {
                this.game.SetScreen(new PlayerCreationScreen(PlayerCreationState.Menu));
            }
            if (this.textbox != null && this.textbox.IsFocused() == false && Main.Input.Pressed(GameSettings.KeyBack) && this.State == PlayerCreationState.UsernameCreation)
            {
                this.game.SetScreen(new PlayerCreationScreen(PlayerCreationState.GenderPicking));
            }
            if (Main.Input.Pressed(GameSettings.KeyBack) && this.State == PlayerCreationState.Confirmation)
            {
                this.game.SetScreen(new PlayerCreationScreen(PlayerCreationState.UsernameCreation));
            }
            if (this.State == PlayerCreationState.UsernameCreation)
            {
                if (Main.Input.Pressed(GameSettings.KeyBack))
                {
                    this.game.SetScreen(new PlayerCreationScreen(PlayerCreationState.Menu));
                }
                if (this.textbox.Confirmed == true)
                {
                    Main.PlayerList[Main.PlayerListIndex].DisplayName = this.textbox.GetText.Trim();
                    Main.PlayerListPath[Main.PlayerListIndex] = Main.GetPlayerPathName(Main.PlayerList[Main.PlayerListIndex].DisplayName);
                    Main.PlayerList[Main.PlayerListIndex].IsLoadedNow = false;
                    Main.PlayerList[Main.PlayerListIndex].SetDefaults();
                    PlayerEntity.SavePlayer(Main.PlayerList[Main.PlayerListIndex], Main.PlayerListPath[Main.PlayerListIndex]);
                    Main.LoadPlayers();
                    this.textbox.active = false;
                    this.game.SetScreen(new PlayerCreationScreen(PlayerCreationState.Play));
                }
            }

            if (this.State == PlayerCreationState.Play)
            {
                foreach (var entry in this.EntryMenus)
                {
                    for (int i = 0; i < Main.maxLoadPlayer; i++)
                    {
                        if (Main.PlayerList[entry.Index] == null) continue;
                        //entry.Text = Main.PlayerList[entry.Index].DisplayName;
                        entry.Text = null;
                        entry.Action = () =>
                        {
                            PlayerEntity getPlayerSeleceted = Main.PlayerList[this.IndexBtn];
                            if (getPlayerSeleceted.IsLoadedNow == false)
                            {
                                // After the player save is created the player.isLoadedNow set to false
                                // to able spawn in the lobby or in the intro
                                // after being loaded in game will be set to true
                                // therefore can able to load the save data files
                                Main.SpawnAtIntro(getPlayerSeleceted);
                            } 
                            else
                            {
                                Main.Spawn(getPlayerSeleceted);
                            }
                        };
                    }
                }
            }
        }
        #endregion
        protected override void EntryMenuScrolling(MenuEntry entry)
        {
            float selectedTop = this.IndexBtn * (entry.Box.Height + PlayerCreationScreen.saveSlotPadding);
            float selectedBottom = selectedTop + entry.Box.Height;

            float visibleTop = this.ScrollOffset;
            float visibleBottom = this.ScrollOffset + this.BoundingBox.Height;
            //Loggers.Debug($"{entry.Box.Height} Boxes :{visibleBottom}||{selectedBottom} top:{visibleTop}||{selectedTop}");

            if (selectedBottom > visibleBottom)
            {
                this.ScrollOffset = selectedBottom - this.BoundingBox.Height;
            }

            if (selectedTop < visibleTop)
            {
                this.ScrollOffset = selectedTop;
            }
            //float maxScroll = Math.Max(0, (this.BoundingBox.Height - (this.EntryMenus.Count * entry.Box.Height)));
            //this.ScrollOffset = MathHelper.Clamp(this.ScrollOffset, 0, maxScroll);
            Loggers.Debug($"Scorlling offset:{this.ScrollOffset}");
        }

        public override void RenderEntryMenus(SpriteBatch sprite)
        {
            if (this.State == PlayerCreationState.Play)
            {
                foreach (var entries in this.EntryMenus)
                {
                    Rectangle box = entries.Box;
                    if (entries.IsActive == true)
                    {
                        //int boxY = (int) (this.BoundingBox.Y + (entries.Index * box.Height) - this.ScrollOffset);
                        int boxY = (int) (box.Y - this.ScrollOffset);
                        int btnIndex = entries.Index;
                        string text = entries.Text.IsEmpty() ? "" : entries.Text;
                        bool isHovered = this.IndexBtn == btnIndex;
                        Vector2 textsize = Texts.MeasureString(Fonts.Text, text);
                        Rectangle dst = new Rectangle(entries.Box.X, (int)(boxY - (textsize.Y / 2)) , entries.Box.Width, (int) (entries.Box.Height + (textsize.Y / 2)));
                        Vector2 entryTextSize = Texts.MeasureString(Fonts.Text, entries.Text);
                        //bool flag = dst.Contains(this.BoundingBox); 
                        bool flag = this.BoundingBox.Contains(dst);
                        //if (flag == true)
                        //{

                        //}
                        if (entries.Texture != null)
                        {
                            if (entries.Texture != null)
                            {
                                int textH = (int)(entries.Texture.Height);
                                if (textH <= dst.Height)
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
                            int y = entries.Text.IsEmpty() ? (box.Y + ((dst.Height - AssetsLoader.Arrow.Value.Height) / 2)) : (int)(box.Y + ((dst.Height - textsize.Y - AssetsLoader.Arrow.Value.Height) / 2));
                            sprite.Draw(AssetsLoader.Arrow.Value, new Rectangle(entries.Box.X + 4, y, AssetsLoader.Arrow.Value.Width, AssetsLoader.Arrow.Value.Height), Color.White);
                        }
                        int paddingText = isHovered ? 1 : 0;
                        if (entries.Text.IsEmpty() == false)
                        {
                            if (entries.Anchor == AnchorMainEntry.Center)
                            {
                                int x = (int)(dst.X + ((dst.Width - entryTextSize.X) / 2));
                                int y = (int)(dst.Y + ((dst.Height - textsize.Y) / 2));
                                Texts.DrawString(entries.FontStyle, text, new Vector2(x, y), entries.TextColor);
                            }
                            if (entries.Anchor == AnchorMainEntry.Left)
                            {
                                int x = dst.X + 20 + (AssetsLoader.Arrow.Value.Width * (isHovered ? 1 : 0));
                                int y = (int)(dst.Y + ((dst.Height - textsize.Y) / 2));
                                Texts.DrawString(entries.FontStyle, text, new Vector2(x, y), entries.TextColor);
                            }
                            if (entries.Anchor == AnchorMainEntry.Right)
                            {
                                int x = (int)(dst.X + entries.Box.Width - (12 + entryTextSize.X));
                                int y = (int)(dst.Y + ((dst.Height - textsize.Y) / 2));
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

        public override void RenderBackground(SpriteBatch sprite, Graphic graphic)
        {
            base.RenderBackground(sprite, graphic);
            this.game.RenderBackground(sprite);
            sprite.NinePatch(AssetsLoader.Box1.Value, this.BoundingBox, Color.White, 32);
            if (this.State == PlayerCreationState.Play)
            {
                foreach (var entry in this.Saves)
                {
                    int index = entry.Key;
                    Rectangle box = entry.Value.Box;
                    bool flag = this.BoundingBox.Contains(box);
                    if (Main.PlayerList[index] != null)
                    {
                        //int boxY = (int) (this.BoundingBox.Y + (entry.Value.Index * box.Height) - this.ScrollOffset);
                        int boxY = (int) (box.Y - this.ScrollOffset);
                        sprite.NinePatch(AssetsLoader.Box2.Value, box, index == this.IndexBtn ? Color.GreenYellow : Color.White, 16);
                        int x = entry.Value.Box.X + 20;
                        int y = (int)(boxY);
                        Texts.DrawString($"Name: {Main.PlayerList[index].DisplayName}", new Vector2(x, y + 10), Color.White);
                        Texts.DrawString($"Score: {Main.PlayerList[index].ScorePoints}", new Vector2(x + 164, y + 10), Color.White);
                    }
                }
            }
        }
    }
}
