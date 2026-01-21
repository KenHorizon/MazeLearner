using System;
using System.Diagnostics;
using System.Text;

namespace MazeLearner
{
    public class Loggers
    {
        public static StringBuilder loggerHistory = new StringBuilder();
        public static void Msg(string msg)
        {
            string formatMessage = $"[{DateTime.Now}] " + msg;
            Debug.WriteLine(formatMessage);
            Console.WriteLine(formatMessage);
            Loggers.loggerHistory.AppendLine(formatMessage);
        }
    }
}
