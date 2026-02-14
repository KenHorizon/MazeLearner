using MazeLeaner.Text;
using MazeLearner.GameContent.Entity.Player;
using MazeLearner.Graphics;
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
        private Dictionary<int, Rectangle> Saves = new Dictionary<int, Rectangle>();
        private int boxPadding = 32;
        private int boxX = 0;
        private int boxY = 20;
        private int boxW
        {
            get
            {
                return this.game.GetScreenWidth();
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
                return this.game.GetScreenHeight();
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
                        this.SaveSlotBoxs[i] = this.SaveSlotBox;
                        this.Saves[i] = this.SaveSlotBox;
                        Rectangle entryBox = new Rectangle(this.SaveSlotBox.X + 20, this.SaveSlotBox.Y + 20, this.SaveSlotBox.Width, this.SaveSlotBox.Height);
                        this.EntryMenus.Add(new MenuEntry(i, Resources.Empty, entryBox, () =>
                        {
                            Main.PlayerListIndex = this.IndexBtn;
                            this.game.SetScreen(new PlayerCreationScreen(PlayerCreationState.GenderPicking));
                        }));
                        this.saveSlotY += 120;
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
                if (this.textbox.Confirmed == true)
                {
                    Main.PlayerList[Main.PlayerListIndex].DisplayName = this.textbox.GetText.Trim();
                    Main.PlayerListPath[Main.PlayerListIndex] = Main.GetPlayerPathName(Main.PlayerList[Main.PlayerListIndex].DisplayName);
                    PlayerEntity.SavePlayerData(Main.PlayerList[Main.PlayerListIndex], Main.PlayerListPath[Main.PlayerListIndex]);
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
                        entry.Text = "";
                        entry.Action = () =>
                        {
                            PlayerEntity getPlayerSeleceted = Main.PlayerList[this.IndexBtn];
                            if (getPlayerSeleceted.IsLoadedNow == false)
                            {
                                Main.SpawnAtLobby(getPlayerSeleceted);
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
        protected override void EntryMenuScrolling(MenuEntry entry)
        {
            float selectedTop = this.IndexBtn * entry.Box.Height;
            float selectedBottom = this.IndexBtn + entry.Box.Height;
            float vTop = this.ScrollOffset;
            float vBottom = this.ScrollOffset + this.BoundingBox.Height;
            if (selectedBottom > vBottom)
            {
                this.ScrollOffset = selectedBottom - this.BoundingBox.Height;
            }
            if (selectedTop < vTop)
            {
                this.ScrollOffset = vTop;
            }
            float maxScroll = Math.Max(0, this.EntryMenus.Count * entry.Box.Height - this.BoundingBox.Height);
            this.ScrollOffset = MathHelper.Clamp(this.ScrollOffset, 0, maxScroll);
        }

        public override void RenderEntryMenus(SpriteBatch sprite)
        {
            foreach (var entry in this.EntryMenus)
            {
                int contentH = this.EntryMenus.Count * this.BoundingBox.Height;
                int visibleEntry = this.BoundingBox.Height / entry.Box.Height;

                float y = this.BoundingBox.Y + (entry.Index * entry.Box.Height) - this.ScrollOffset;
                bool canRenderInside = y + entry.Box.Height < this.BoundingBox.Height || y > this.BoundingBox.Height;
                if (canRenderInside == true)
                {
                    base.RenderEntryMenus(sprite);
                }
            }
        }
        public override void RenderBackground(SpriteBatch sprite, Graphic graphic)
        {
            base.RenderBackground(sprite, graphic);
            this.game.RenderBackground(sprite);
            sprite.NinePatch(AssetsLoader.Box1.Value, this.BoundingBox, Color.White, 32);
            if (this.State == PlayerCreationState.Play)
            {

                foreach (var box in this.SaveSlotBoxs)
                {
                    sprite.NinePatch(AssetsLoader.Box2.Value, box, Color.White, 16);
                    
                }
                for (int i = 0; i < Main.maxLoadPlayer; i++)
                {
                    if (Main.PlayerList[i] != null)
                    {
                        int x = this.saveSlotX + Main.MaxTileSize * 3;
                        int y = 40;
                        TextManager.Text(Fonts.DT_L, $"Name: {Main.PlayerList[i].DisplayName}", new Vector2(x, y + 10));
                        TextManager.Text(Fonts.DT_L, $"Coins: {Main.PlayerList[i].Coin}", new Vector2(x, y + 38));
                        TextManager.Text(Fonts.DT_L, $"Health: {Main.PlayerList[i].Health}/{Main.PlayerList[i].MaxHealth}", new Vector2(x + 164, y + 10));
                    }
                }

                foreach (var entry in this.Saves)
                {
                    int index = entry.Key;
                    Rectangle box = entry.Value;
                    if (Main.PlayerList[index] != null)
                    {
                        int x = this.saveSlotX + Main.MaxTileSize * 3;
                        int y = box.Y;
                        TextManager.Text(Fonts.DT_L, $"Name: {Main.PlayerList[index].DisplayName}", new Vector2(x, y + 10));
                        TextManager.Text(Fonts.DT_L, $"Coins: {Main.PlayerList[index].Coin}", new Vector2(x, y + 38));
                        TextManager.Text(Fonts.DT_L, $"Health: {Main.PlayerList[index].Health}/{Main.PlayerList[index].MaxHealth}", new Vector2(x + 164, y + 10));
                    }
                }
            }
        }
    }
}
