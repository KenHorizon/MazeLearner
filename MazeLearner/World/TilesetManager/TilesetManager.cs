using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MazeLearner.World.TilesetManager
{
    public class TilesetManager
    {
        private Main game;
        public TilesetManager(Main game)
        {
            this.game = game;
        }
        public (TilesetMap map, Tilesets tileset) Load(string filePath)
        {
            string jsonText = File.ReadAllText(filePath);
            using JsonDocument doc = JsonDocument.Parse(jsonText);

            JsonElement root = doc.RootElement;

            TilesetMap map = new TilesetMap
            {
                Width = root.GetProperty("width").GetInt32(),
                Height = root.GetProperty("height").GetInt32(),
                TileWidth = root.GetProperty("tilewidth").GetInt32(),
                TileHeight = root.GetProperty("tileheight").GetInt32()
            };
            Tilesets tileset = LoadTileset(root.GetProperty("tilesets")[0]);
            foreach (var layer in root.GetProperty("layers").EnumerateArray())
            {
                string name = layer.GetProperty("name").GetString();

                if (layer.GetProperty("type").GetString() != "tilelayer")
                    continue;

                if (name == "Ground")
                    map.Ground = ReadLayers(layer, map.Width, map.Height);

                if (name == "Collision")
                    map.Collision = ReadLayers(layer, map.Width, map.Height);
            }

            return (map, tileset);
        }
        private Tilesets LoadTileset(JsonElement tilesetJson)
        {
            Tilesets tileset = new Tilesets
            {
                FirstGid = tilesetJson.GetProperty("firstgid").GetInt32()
            };

            // External tileset
            if (tilesetJson.TryGetProperty("source", out var source))
            {
                string tsPath = Path.Combine(
                    Path.GetDirectoryName(source.GetString()) ?? "",
                    source.GetString()
                );

                using JsonDocument tsDoc = JsonDocument.Parse(File.ReadAllText(tsPath));
                LoadTileset(tsDoc.RootElement, tileset);
            }
            else
            {
                LoadTileset(tilesetJson, tileset);
            }

            return tileset;
        }
        public void LoadTileset(JsonElement tilesetJson, Tilesets tileset)
        {
            foreach (var tile in tilesetJson.GetProperty("tiles").EnumerateArray())
            {
                int id = tile.GetProperty("id").GetInt32();
                var def = new TilesetDefinition { TileId = id };

                if (tile.TryGetProperty("properties", out var props))
                {
                    foreach (var prop in props.EnumerateArray())
                    {
                        string name = prop.GetProperty("name").GetString();
                        var value = prop.GetProperty("value");

                        switch (name)
                        {
                            case "passable":
                                def.Passable = value.GetBoolean();
                                break;
                            case "terrain":
                                def.Terrain = value.GetString();
                                break;
                            case "event":
                                def.EventId = value.GetString();
                                break;
                        }
                    }
                }

                tileset.Tiles[id] = def;
            }
        }
        public int[,] ReadLayers(JsonElement layer, int width, int height)
        {
            int[,] data = new int[width, height];
            var tiles = layer.GetProperty("data").EnumerateArray();

            int i = 0;
            foreach (var tile in tiles)
            {
                int x = i % width;
                int y = i / width;
                data[x, y] = tile.GetInt32() - 1; // Tiled is 1-based
                i++;
            }

            return data;
        }
        //var (map, tileset) = TilesetManager.Load("Maps/town_01.tmj");
        //public bool CanMoveTo(int x, int y)
        //{
        //    int gid = map.Collision[x, y];
        //    if (gid == 0) return true;

        //    return tileset.Get(gid).Passable;
        //}
    }
}
