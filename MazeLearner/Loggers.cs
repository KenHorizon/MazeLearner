using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using MazeLeaner;

namespace MazeLearner
{
    public class Loggers
    {
        private static void Message(string msg)
        {
            string formatMessage = $"[{DateTime.Now}] " + msg;
            Console.WriteLine(formatMessage);
            System.Diagnostics.Debug.WriteLine(formatMessage);
        }

        public static void Debug(string msg) => Message($"[Debug]: " + msg);
        public static void Error(string msg) => Message($"[Error]: " + msg);
        public static void Warn(string msg) => Message($"[Warn]: " + msg);
        public static void Info(string msg) => Message($"[Info]: " + msg);
    }
}
