using MazeLearner.GameContent.Animation;
using MazeLearner.Screen.Widgets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MazeLearner.Screen
{
    public class PlayerCreationScreen : BaseScreen
    {
        public enum PlayerCreationState
        {
            Play,
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
                return 220;
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
                return new Rectangle(this.saveSlotX + ((this.BoundingBox.Width - this.saveSlotW) / 2), this.saveSlotY, saveSlotW - (saveSlotX + (saveSlotPadding * 2)), saveSlotH - (saveSlotY + saveSlotPadding));
            }
            set
            {
                this.saveSlotX = value.X;
                this.saveSlotY = value.Y;
                this.saveSlotW = value.Width;
                this.saveSlotH = value.Height;
            }
        }
        public Rectangle[] SaveSlotBoxs = new Rectangle[Main.SaveSlots.Length];
        public PlayerCreationScreen(PlayerCreationState state = PlayerCreationState.Play) : base("")
        {
            this.State = state;
        }

        public override void LoadContent()
        {
            if (this.State == PlayerCreationState.Play)
            {
                for (int i = 0; i < this.SaveSlotBoxs.Length; i++)
                {
                    this.saveSlotY += 40;
                    this.SaveSlotBoxs[i] = this.SaveSlotBox;
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

        public override void RenderBackground(SpriteBatch sprite)
        {
            base.RenderBackground(sprite);
            this.game.RenderBackground(sprite);
            sprite.DrawMessageBox(AssetsLoader.Box1.Value, this.BoundingBox, Color.White, 32);
            if (this.State == PlayerCreationState.Play)
            {
                for(int i = 0; i < this.SaveSlotBoxs.Length; i++)
                {
                    sprite.DrawMessageBox(AssetsLoader.Box2.Value, this.SaveSlotBoxs[i], Color.White, 32);
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
