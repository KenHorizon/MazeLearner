using MazeLearner.GameContent.Animation;
using MazeLearner.GameContent.Entity;
using MazeLearner.GameContent.Entity.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.Worlds.Tilesets
{
    public class TilesetManager
    {
        private Main game;
        public string mapName {  get; set; }
        private TiledMap map;
        private Dictionary<int, TiledTileset> tilesets;
        private Texture2D[] tilesetTexture = new Texture2D[20];
        private int tilesetTextureIndex = 0;
        public TilesetManager(Main game)
        {
            this.game = game;
        }

        public void LoadMap(string name, Song backgroundSound = null)
        {
            this.mapName = name;
            if (backgroundSound != null)
            {
                Main.Audio.PlaySong(backgroundSound, true);
                Main.Audio.Volume = 0.25F;
            }
            this.map = new TiledMap(Main.Content.RootDirectory + $"/Data/Tiled/Maps/{name}.tmx");
            this.tilesets = this.map.GetTiledTilesets(Main.Content.RootDirectory + "/Data/");
            foreach (var tileset in this.tilesets)
            {
                Loggers.Msg($"Loaded Tilesets! {this.tilesetTextureIndex} {tileset.Value.Name}");
                if (tileset.Value.Name == "passage") continue;
                this.tilesetTexture[this.tilesetTextureIndex] = Assets<Texture2D>.Request($"Data/Tiled/Assets/{tileset.Value.Name}").Value;
                this.tilesetTextureIndex++;
            }
        }

        public void Update(GameTime gameTime)
        {

        }

        public bool IsTilePassable(string getLayers, Rectangle rect)
        {
            var tileLayers = map.Layers.Where(x => x.type == TiledLayerType.TileLayer);
            foreach (var layer in tileLayers)
            {
                for (var y = 0; y < layer.height; y++)
                {
                    for (var x = 0; x < layer.width; x++)
                    {
                        if (layer.name == getLayers)
                        {
                            var index = (y * layer.width) + x;
                            var gid = layer.data[index];
                            var tileX = x * map.TileWidth;
                            var tileY = y * map.TileHeight;

                            if (gid == 0)
                            {
                                continue;
                            }
                            var destination = new Rectangle(tileX, tileY, map.TileWidth, map.TileHeight);
                            if (rect.Intersects(destination))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        public List<TiledOrderedLayer> CreateOrderedLayer(TiledMap tiledMap)
        {
            var result = new List<TiledOrderedLayer>();
            foreach (var layer in map.Layers)
            {
                if (layer.type != TiledLayerType.TileLayer)
                {
                    continue;
                }

                int order = 0;
                var parts = layer.name.Split('_');
                if (parts.Length > 1 && int.TryParse(parts[^1], out int parsed))
                {
                    order = parsed;
                }
                result.Add(new TiledOrderedLayer{Layer = layer, Order = order});
            }
            result.Sort((a, b) => a.Order.CompareTo(b.Order));

            return result;
        }
        public void DrawNpcs(TiledOrderedLayer layer)
        {
            foreach (var renderEntity in Main.AllEntity)
            {
                int entityLayer = (int) (renderEntity.GetY / map.TileHeight);
                if (entityLayer > map.TileHeight) break;
                if (renderEntity != null)
                {
                    Sprite sprites = new Sprite(renderEntity.langName, renderEntity);
                    sprites.Draw(Main.SpriteBatch);
                }
            }
        }
        public void Draw(SpriteBatch sprite)
        {
            var player = this.game.ActivePlayer;
            //var tileLayers = map.Layers.Where(x => x.type == TiledLayerType.TileLayer);
            foreach (var orderedLayer in this.CreateOrderedLayer(map))
            {
                var layer = orderedLayer.Layer;
                if (layer.name == "passage") continue;
                for (var y = 0; y < layer.height; y++)
                {
                    for (var x = 0; x < layer.width; x++)
                    {
                        var index = (y * layer.width) + x; // Assuming the default render order is used which is from right to bottom
                        var gid = layer.data[index]; // The tileset tile index
                        var tileX = x * map.TileWidth;
                        var tileY = y * map.TileHeight;
                        // Gid 0 is used to tell there is no tile set
                        if (gid == 0) continue;
                        // Helper method to fetch the right TieldMapTileset instance
                        // This is a connection object Tiled uses for linking the correct tileset to the gid value using the firstgid property
                        var mapTileset = map.GetTiledMapTileset(gid);
                        var tileProperty = map.Properties;

                        // Retrieve the actual tileset based on the firstgid property of the connection object we retrieved just now
                        var tileset = tilesets[mapTileset.firstgid];

                        // Use the connection object as well as the tileset to figure out the source rectangle
                        var rect = map.GetSourceRect(mapTileset, tileset, gid);

                        // Create destination and source rectangles
                        var source = new Rectangle(rect.x, rect.y, rect.width, rect.height);

                        var destination = new Rectangle(tileX, tileY, map.TileWidth, map.TileHeight);

                        // You can use the helper methods to get information to handle flips and rotations

                        // Render sprite at position tileX, tileY using the rect
                        foreach (var tile in this.tilesetTexture)
                        {
                            if (tile == null) continue;
                            sprite.Draw(tile, destination, source, Color.White, 0.0F, Vector2.Zero, SpriteEffects.None, 0.0F);
                        }
                    }
                }
                this.DrawNpcs(orderedLayer);
            }
        }
    }
}
