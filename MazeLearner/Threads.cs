using Microsoft.Xna.Framework;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner
{
    public class Threads
    {
        private static ConcurrentQueue<Action> _mainThreadActions = new();
        public static void RunAsync(Action action)
        {
            Task.Run(action);
        }
        public static void Run(Action action)
        {
            _mainThreadActions.Enqueue(action);
        }
        public static void Update(GameTime gameTime)
        {
            while (_mainThreadActions.TryDequeue(out var action))
            {
                action();
            }
        }
    }
}
