using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.Graphics
{
    public sealed class Asset<T> where T : class
    {
        public string Name { get; private set; }
        private static readonly List<Asset<T>> _requested = new();
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
        public Asset(string filePath)
        {
            this.filePath = GameSettings.MediaFile + filePath;
        }

        public static Asset<T> Request(string file)
        {
            var assets = new Asset<T>(file);
            _requested.Add(assets);
            return assets;
        }

        private T Get()
        {
            try
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
            catch (Exception ex)
            {
                Loggers.Error($"{ex}");
                throw new InvalidOperationException($"{ex}");
            }

        }
        public static void LoadAll()
        {
            if (Main.Content == null)
            {
                Loggers.Error("Main.Content is null! / ContentManager not initialized!");
                throw new InvalidOperationException("ContentManager not initialized!");
            }
            foreach (var asset in _requested)
            {
                asset._value = asset.Get(); // force load
            }
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
