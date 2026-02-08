using MazeLeaner.Text;
using MazeLearner.GameContent.Animation;
using MazeLearner.GameContent.Data;
using MazeLearner.GameContent.Entity.Player;
using MazeLearner.Localization;
using MazeLearner.Screen.Components;
using MazeLearner.Screen.Widgets;
using MazeLearner.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using static System.Net.Mime.MediaTypeNames;

namespace MazeLearner.Screen
{
    public class PlayerCreationScreen : BaseScreen
    {
        public enum PlayerCreationState
        {
            Play,
            Create,
            GenderPicking,
            UsernameCreation,
            Confirmation
        }
        public PlayerCreationState State { get; set; }
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
        public PlayerCreationScreen(PlayerCreationState state = PlayerCreationState.Play) : base("")
        {
            Loggers.Msg($"Player List Index: {Main.PlayerListIndex}");
            this.State = state;
        }
        public override void LoadContent()
        {
            this.saveSlotX += 20;
            this.saveSlotY += 20;
            if (this.State == PlayerCreationState.Play)
            {
                Main.LoadPlayers();
                for (int i = 0; i < Main.maxLoadPlayer; i++)
                {
                    this.SaveSlotBoxs[i] = this.SaveSlotBox;
                    // We will seperate the box of save slot and the box of entry
                    // Why? first to put the entry inside of the of the save slot box
                    // Second to make it perfect :)
                    Rectangle entryBox = new Rectangle(this.SaveSlotBox.X + 20, this.SaveSlotBox.Y + 20, this.SaveSlotBox.Width, this.SaveSlotBox.Height);
                    this.EntryMenus.Add(new MenuEntry(i, Resources.Create, entryBox, () =>
                    {
                        Main.PlayerListIndex = this.IndexBtn;
                        this.game.SetScreen(new PlayerCreationScreen(PlayerCreationState.GenderPicking));
                    }));
                    this.saveSlotY += 120;
                }
            }
            if (this.State == PlayerCreationState.GenderPicking)
            {
                int x = (this.game.WindowScreen.Width - 240) / 2;
                int y = this.game.WindowScreen.Height / 2 - 20;
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
            if (this.State == PlayerCreationState.Confirmation)
            {

            }
        }

        public override void Update(GameTime gametime)
        {
            base.Update(gametime);
            if (Main.Keyboard.Pressed(GameSettings.KeyBack) && this.State == PlayerCreationState.Play)
            {
                this.game.SetScreen(new TitleScreen(TitleSequence.Title));
            }
            if (Main.Keyboard.Pressed(GameSettings.KeyBack) && this.State == PlayerCreationState.Create)
            {
                this.game.SetScreen(new PlayerCreationScreen(PlayerCreationState.Play));
            }
            if (Main.Keyboard.Pressed(GameSettings.KeyBack) && this.State == PlayerCreationState.GenderPicking)
            {
                this.game.SetScreen(new PlayerCreationScreen(PlayerCreationState.Play));
            }
            if (this.textbox != null && this.textbox.IsFocused() == false && Main.Keyboard.Pressed(GameSettings.KeyBack) && this.State == PlayerCreationState.UsernameCreation)
            {
                this.game.SetScreen(new PlayerCreationScreen(PlayerCreationState.GenderPicking));
            }
            if (Main.Keyboard.Pressed(GameSettings.KeyBack) && this.State == PlayerCreationState.Confirmation)
            {
                this.game.SetScreen(new PlayerCreationScreen(PlayerCreationState.UsernameCreation));
            }
            if (this.State == PlayerCreationState.UsernameCreation)
            {
                if (this.textbox.Confirmed == true)
                {
                    Main.PlayerList[Main.PlayerListIndex].Name = this.textbox.GetText.Trim();
                    Main.PlayerListPath[Main.PlayerListIndex] = Main.GetPlayerPathName(Main.PlayerList[Main.PlayerListIndex].Name);
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
                        if (Main.PlayerList[entry.Index] == null) break;
                        entry.Text = "";
                        entry.Action = () =>
                        {
                            Main.SpawnAtLobby(Main.PlayerList[this.IndexBtn]);
                        };
                    }
                }
            }
        }
        public override void RenderBackground(SpriteBatch sprite, GraphicRenderer graphic)
        {
            base.RenderBackground(sprite, graphic);
            this.game.RenderBackground(sprite);
            sprite.DrawMessageBox(AssetsLoader.Box1.Value, this.BoundingBox, Color.White, 32);
            if (this.State == PlayerCreationState.Play)
            {

                foreach (var box in this.SaveSlotBoxs)
                {
                    sprite.DrawMessageBox(AssetsLoader.Box2.Value, box, Color.White, 16);
                    
                }
                for (int i = 0; i < Main.maxLoadPlayer; i++)
                {
                    if (Main.PlayerList[i] == null) continue;
                    int x = this.saveSlotX + Main.MaxTileSize * 3;
                    int y = 40;
                    TextManager.Text(Fonts.DT_L, "Name: " + Main.PlayerList[i].DisplayName, new Vector2(x, y + 10));
                    TextManager.Text(Fonts.DT_L, "Coins: " + Main.PlayerList[i].Coin, new Vector2(x, y + 38));
                    TextManager.Text(Fonts.DT_L, $"Health: {Main.PlayerList[i].Health}/{Main.PlayerList[i].MaxHealth}", new Vector2(x + 164, y + 10));
                }
            }
            if (this.State == PlayerCreationState.GenderPicking)
            {

            }
            if (this.State == PlayerCreationState.UsernameCreation)
            {

            }
            if (this.State == PlayerCreationState.Confirmation)
            {

            }
        }
    }
}
