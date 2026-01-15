using System;
using System.Diagnostics;

namespace MazeLearner
{
    public class Debugs
    {
        public static void Msg(string msg)
        {
            Debug.WriteLine(msg);
            Console.WriteLine(msg);
        }
    }
}
