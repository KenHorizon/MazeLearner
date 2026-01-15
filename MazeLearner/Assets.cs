using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner
{
    public sealed class Assets<T> where T : class
    {
        public string Name { get; private set; }
        private T _value;
        public T Value
        {
            get
            {
                if (_value == null)
                {
                    _value = Get();
                }
                return _value;
            }
        }
        private static Dictionary<string, T> _cache = new Dictionary<string, T>();
        private readonly string filePath;
        public Assets(string filePath)
        {
            this.filePath = GameSettings.MediaFile + filePath;
        }

        public static Assets<T> Request(string file)
        {
            return new Assets<T>(file);
        }

        private T Get()
        {
            if (Main.Content == null)
            {
                throw new InvalidOperationException("ContentManager not initialized!");
            }
            if (_cache.TryGetValue(filePath, out T asset))
            {
                return asset;
            }
            asset = Main.Content.Load<T>(filePath);
            _cache[filePath] = asset;
            return asset;
        }
        public static void Unload(string file)
        {
            string fullPath = GameSettings.MediaFile + file;
            if (_cache.ContainsKey(fullPath))
            {
                if (_cache[fullPath] is System.IDisposable disposable)
                    disposable.Dispose();

                _cache.Remove(fullPath);
            }
        }
        public static void UnloadAll(ContentManager content)
        {
            foreach (var kvp in _cache)
            {
                if (kvp.Value is System.IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }

            _cache.Clear();
            content.Unload();
        }
    }
}
