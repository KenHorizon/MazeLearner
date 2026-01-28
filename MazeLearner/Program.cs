using System;
using System.IO;

namespace Solarized
{
    public class Program
    {
        public static string PlayerDataPath;
        public static string SavePath;
        public static string LogPath;

        public static void Main(string[] args)
        {
            PlayerDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), MazeLearner.Main.GameTitle + "/players");
            SavePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), MazeLearner.Main.GameTitle);
            LogPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), MazeLearner.Main.GameTitle + "/logs");
            using var game = new MazeLearner.Main();
            game.Run();
        }
    }
}
