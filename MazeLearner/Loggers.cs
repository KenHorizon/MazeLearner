using System;
using System.Diagnostics;

namespace MazeLearner
{
    public class Loggers
    {
        public static void Msg(string msg)
        {
            Debug.WriteLine($"[{DateTime.Now}] " + msg);
            Console.WriteLine($"[{DateTime.Now}] " + msg);
        }
    }
}
