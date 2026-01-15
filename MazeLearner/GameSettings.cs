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

        public static bool AutoSave = true;
        public static Keys KeyForward = Keys.Up;
        public static Keys KeyDownward = Keys.Down;
        public static Keys KeyLeft = Keys.Left;
        public static Keys KeyRight = Keys.Right;
        public static Keys KeyRunning = Keys.LeftShift;
        public static Keys KeyInteract = Keys.Z;
        public static Keys KeyBack = Keys.X;
        public static Keys KeyOpenInventory = Keys.A;

        public static bool SaveSettings()
        {
            Preferences settings = Main.Settings;
            settings.Clear();
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
            settings.Get("AutoSave", ref AutoSave);
            settings.Get("KeyForward", ref KeyForward);
            settings.Get("KeyDownward", ref KeyDownward);
            settings.Get("KeyLeft", ref KeyLeft);
            settings.Get("KeyRight", ref KeyRight);
            settings.Put("KeyRunning", KeyRunning);
            settings.Put("KeyInteract", KeyInteract);
            settings.Put("KeyBack", KeyBack);
            settings.Put("KeyOpenInventory", KeyOpenInventory);
            return false;
        }
    }
}
