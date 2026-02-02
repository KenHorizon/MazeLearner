using MazeLearner.GameContent.Animation;
using MazeLearner.GameContent.Entity;
using MazeLearner.GameContent.Entity.Player;
using MazeLearner.Worlds.Tilesets.EventMaps;
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
    public class TilesetRenderer
    {
        private Main game;
        public string mapName {  get; set; }
        private TiledMap map;
        private Dictionary<int, TiledTileset> tilesets;
        private Texture2D[] tilesetTexture = new Texture2D[999];
        private int tilesetTextureIndex = 0;
        public TilesetRenderer(Main game)
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
            LoadGameObjects();

        }

        public void Update(GameTime gameTime)
        {
            var objectLayers = map.Layers.Where(x => x.type == TiledLayerType.ObjectLayer);
            foreach (var layer in objectLayers)
            {
                if (layer.objects != null)
                {
                    foreach (var objects in layer.objects)
                    {
                        foreach (EventMapId mapEventId in Enum.GetValues(typeof(EventMapId)))
                        {
                            var databaseObj = ObjectDatabase.Get(mapEventId);
                            if (databaseObj == null) continue;
                            EventMapId eventMapId = (EventMapId)Enum.ToObject(typeof(EventMapId), int.Parse(databaseObj.Get("EventMap").value));
                            if (eventMapId == EventMapId.None) return;
                            bool interacted = databaseObj.Bounds.Intersects(this.game.GetPlayer.InteractionBox);
                            if (eventMapId == EventMapId.Warp)
                            {
                                int id = int.Parse(databaseObj.Get("Id").value);
                                var map = databaseObj.Get("MapName").value;
                                int x = int.Parse(databaseObj.Get("X").value);
                                int y = int.Parse(databaseObj.Get("Y").value);
                                if (interacted == true)
                                {
                                    this.game.GetPlayer.SetPos(x, y);
                                }
                            }
                        }
                    }
                }
            }
        }

        private IEnumerable<TiledLayer> LoadGameObjects()
        {
            // Load all the objects in the maps
            var objectLayers = map.Layers.Where(x => x.type == TiledLayerType.ObjectLayer);
            foreach (var layer in objectLayers)
            {
                if (layer.objects != null)
                {
                    foreach (var objects in layer.objects)
                    {
                        GameObject objectGames = new GameObject();
                        foreach (var prop in objects.properties)
                        {
                            var props = new TiledProperty();
                            props.name = prop.name;
                            props.type = prop.type;
                            props.value = prop.value;
                            objectGames.AddProperty(prop);
                        }
                        objectGames.BuildBounds((int)objects.x, (int)objects.y, 32);
                        ObjectDatabase.Register(objectGames);
                        
                    }
                }
            }

            return objectLayers;
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
        public void DrawNpcs()
        {
            foreach (var renderEntity in Main.AllEntity)
            {
                if (renderEntity != null)
                {
                    Sprite sprites = new Sprite(renderEntity.langName, renderEntity);
                    sprites.Draw(Main.SpriteBatch);
                }
            }
        }
        public void DrawNpcs(TiledOrderedLayer layer)
        {
            foreach (var renderEntity in Main.AllEntity)
            {
                if (renderEntity != null)
                {
                    Sprite sprites = new Sprite(renderEntity.langName, renderEntity);
                    sprites.Draw(Main.SpriteBatch);
                }
            }
        }
        public void Draw(SpriteBatch sprite)
        {
            var player = this.game.GetPlayer;
            Vector2 playerPosition = this.game.Camera.Position;
            Vector2 screenBox = new Vector2(this.game.WindowScreen.Width, this.game.WindowScreen.Height);
            Rectangle boundingBoxDraw = new Rectangle((int)playerPosition.X, (int)playerPosition.Y, (int)screenBox.X, (int)screenBox.Y);
            //var tileLayers = map.Layers.Where(x => x.type == TiledLayerType.TileLayer);
            foreach (var orderedLayer in this.CreateOrderedLayer(map))
            {
                var layer = orderedLayer.Layer;
                if (layer.name == "passage") continue;
                int startX = boundingBoxDraw.Left / map.TileWidth;
                int startY = boundingBoxDraw.Top / map.TileHeight;
                int endX = (boundingBoxDraw.Right / map.TileWidth) + 1;
                int endY = (boundingBoxDraw.Bottom / map.TileHeight) + 1;
                startX = Math.Max(0, startX);
                startY = Math.Max(0, startY);
                endX = Math.Min(layer.width, endX);
                endY = Math.Min(layer.height, endY);
                for (var y = startY; y < endY; y++)
                {
                    for (var x = startX; x < endX; x++)
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
            this.DrawNpcs();
        }
    }
}
