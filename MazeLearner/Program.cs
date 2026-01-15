using System;
using System.IO;

namespace Solarized
{
    public class Program
    {
        public static string SavePath;

        public static void Main(string[] args)
        {
            SavePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), MazeLearner.Main.GameTitle);
            using var game = new MazeLearner.Main();
            game.gameSetter.SetupGame();
            game.Run();
        }
    }
}
