using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using MazeLeaner;

namespace MazeLearner
{
    public class Loggers
    {
        public static StringBuilder loggerHistory = new StringBuilder();
        private static void Message(string msg)
        {
            string formatMessage = $"[{DateTime.Now}] " + msg;
            Console.WriteLine(formatMessage);
            Loggers.loggerHistory.AppendLine(formatMessage);
        }

        public void Init()
        {
            Console.WriteLine("Game exiting...");
            TextWriter loggerHistory = Console.Out;
            string pathFile = Program.SavePath + Path.DirectorySeparatorChar;
            try
            {
                if (!Directory.Exists(pathFile))
                {
                    Directory.CreateDirectory(pathFile);
                }
                FileStream fs = new FileStream(Main.LogPath + ".txt", FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs);
                Console.SetOut(sw);
                Console.WriteLine(Loggers.loggerHistory);
                Console.SetOut(loggerHistory);
                sw.Close();
                fs.Close();
            }
            catch (Exception e)
            {
                Loggers.Message($"An exception occurred: {e}");
            }
        }

        public static void Debug(string msg) => Message($"[Debug]: " + msg);
        public static void Error(string msg) => Message($"[Error]: " + msg);
        public static void Warn(string msg) => Message($"[Warn]: " + msg);
        public static void Info(string msg) => Message($"[Info]: " + msg);
    }
}
