using MazeLeaner;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner
{
    public class GameSettings
    {
        public static int InventorySlot { get; set; } = 255;
        public static string MediaFile { get; } = @"";
        public static int MultiplayerCap { get; set; } = 1;
        public static int Item { get; set; } = 99999;
        public static int SpawnCap { get; set; } = 200;
        public static bool FullScreen { get; set; } = false;
        public static bool CustomCursor { get; set; } = true;
        public static bool AdminDev { get; set; } = true;
        public static bool SkipSplash { get; set; } = false;
        public static bool TraceScreen { get; set; } = false;
        public static bool DebugScreen { get; set; } = false;

        public static float MasterMusic = 1.0F;
        public static float BackgroundMusic = 1.0F;
        public static float VFXMusic = 1.0F;

        public static bool AllowConsole = true;
        public static bool AutoSave = true;
        public static Keys KeyForward = Keys.Up;
        public static Keys KeyDownward = Keys.Down;
        public static Keys KeyLeft = Keys.Left;
        public static Keys KeyRight = Keys.Right;
        public static Keys KeyRunning = Keys.LeftShift;
        public static Keys KeyInteract = Keys.Z;
        public static Keys KeyBack = Keys.X;
        public static Keys KeyOpenInventory = Keys.Back;

        public static int DialogBoxPadding = 30;
        public static int DialogBoxSize = 180;
        public static int DialogBoxY = 20;
        public static float DialogBoxR = 0.0F;
        public static float DialogBoxG = 0.0F;
        public static float DialogBoxB = 0.0F;
        public static float DialogBoxA = 0.5F;
        public static bool SaveSettings()
        {
            Preferences settings = Main.Settings;
            settings.Clear();
            settings.Put("MasterMusic", MasterMusic);
            settings.Put("BackgroundMusic", BackgroundMusic);
            settings.Put("VFXMusic", VFXMusic);
            settings.Put("DialogBoxPadding", DialogBoxPadding);
            settings.Put("DialogBoxSize", DialogBoxSize);
            settings.Put("DialogBoxY", DialogBoxY);
            settings.Put("DialogBoxR", DialogBoxR);
            settings.Put("DialogBoxG", DialogBoxG);
            settings.Put("DialogBoxB", DialogBoxB);
            settings.Put("DialogBoxA", DialogBoxA);
            settings.Put("AllowConsole", AllowConsole);
            settings.Put("AutoSave", AutoSave);
            settings.Put("KeyForward", KeyForward);
            settings.Put("KeyDownward", KeyDownward);
            settings.Put("KeyLeft", KeyLeft);
            settings.Put("KeyRight", KeyRight);
            settings.Put("KeyRunning", KeyRunning);
            settings.Put("KeyInteract", KeyInteract);
            settings.Put("KeyBack", KeyBack);
            settings.Put("KeyOpenInventory", KeyOpenInventory);
            Main.Settings.Save();
            return false;
        }
        public static bool LoadSettings()
        {
            Preferences settings = Main.Settings;
            Main.Settings.Load();
            settings.Get("MasterMusic", ref MasterMusic);
            settings.Get("BackgroundMusic", ref BackgroundMusic);
            settings.Get("VFXMusic", ref VFXMusic);
            settings.Get("DialogBoxPadding", ref DialogBoxPadding);
            settings.Get("DialogBoxSize", ref DialogBoxSize);
            settings.Get("DialogBoxY", ref DialogBoxY);
            settings.Get("DialogBoxR", ref DialogBoxR);
            settings.Get("DialogBoxG", ref DialogBoxG);
            settings.Get("DialogBoxB", ref DialogBoxB);
            settings.Get("DialogBoxA", ref DialogBoxA);
            settings.Get("AllowConsole", ref AllowConsole);
            settings.Get("AutoSave", ref AutoSave);
            settings.Get("KeyForward", ref KeyForward);
            settings.Get("KeyDownward", ref KeyDownward);
            settings.Get("KeyLeft", ref KeyLeft);
            settings.Get("KeyRight", ref KeyRight);
            settings.Get("KeyRunning", ref KeyRunning);
            settings.Get("KeyInteract", ref KeyInteract);
            settings.Get("KeyBack", ref KeyBack);
            settings.Get("KeyOpenInventory", ref KeyOpenInventory);
            return false;
        }
    }
}
