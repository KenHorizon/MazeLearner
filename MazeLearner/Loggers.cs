using System;
using System.Diagnostics;

namespace MazeLeaner
{
    public class Loggers
    {
        public static void Msg(string msg)
        {
            Debug.WriteLine(msg);
            Console.WriteLine(msg);
        }
    }
}
