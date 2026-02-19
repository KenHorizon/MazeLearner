using MazeLearner.Audio;
using MazeLearner.GameContent.Entity;
using MazeLearner.GameContent.Entity.Objects;
using MazeLearner.GameContent.Entity.Player;
using MazeLearner.Graphics.Animation;
using MazeLearner.Screen;
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
    public class Tiled
    {
        private Main game;
        public string mapName {  get; set; }
        private TiledMap map;
        private Dictionary<int, TiledTileset> tilesets;
        private Texture2D[] tilesetTexture = new Texture2D[999];
        private int tilesetTextureIndex = 0;
        private Action _onLoadMap;
        private bool teleport=  false;
        public Action OnLoad
        {
            get
            {
                return _onLoadMap;
            }
            set
            {
                _onLoadMap = value;
            }
        }
        public Tiled(Main game)
        {
            this.game = game;
        }

        public void LoadMap(World world)
        {
            string name = world.Name;
            Main.MapIds = world.Id;
            Loggers.Info($"Map {name} Id:{world.Id}");
            this.mapName = name;
            if (world.Song != null)
            {
                Main.SoundEngine.Play(world.Song, true);
            }
            this.map = new TiledMap(Main.Content.RootDirectory + $"/Data/Tiled/Maps/{name}.tmx");
            this.tilesets = this.map.GetTiledTilesets(Main.Content.RootDirectory + "/Data/");
            
            foreach (var tileset in this.tilesets)
            {
                Loggers.Info($"Loaded Tilesets! {this.tilesetTextureIndex} {tileset.Value.Name}");
                if (tileset.Value.Name == "events") continue;
                if (tileset.Value.Name == "passage") continue;
                this.tilesetTexture[this.tilesetTextureIndex] = Asset<Texture2D>.Request($"Data/Tiled/Assets/{tileset.Value.Name}").Value;
                this.tilesetTextureIndex++;
            }
            ObjectDatabase.Clear();
            this.OnLoadMap();
        }
        private IEnumerable<TiledLayer> LoadGameObjects()
        {
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

        public virtual void OnLoadMap()
        {
            this.LoadGameObjects();
            this.OnLoad?.Invoke();
            var objectLayers = map.Layers.Where(x => x.type == TiledLayerType.ObjectLayer);
            foreach (var layer in objectLayers)
            {
                if (layer.objects != null)
                {
                    foreach (var databaseObj in ObjectDatabase.GetAll)
                    {
                        if (databaseObj == null) continue;
                        EventMapId eventMapId = (EventMapId)Enum.ToObject(typeof(EventMapId), int.Parse(databaseObj.Get("EventMap").value));
                        if (eventMapId == EventMapId.None) return;
                        if (eventMapId == EventMapId.Npc)
                        {
                            bool battle = int.Parse(databaseObj.Get("Battle").value) == 1;
                            int aiType = int.Parse(databaseObj.Get("AIType").value);
                            string npcName = databaseObj.Get("Name") == null ? "" : databaseObj.Get("Name").value;
                            string message = databaseObj.Get("Dialog") == null ? "" : databaseObj.Get("Dialog").value;
                            int Health = databaseObj.Get("Name") == null ? 0 : int.Parse(databaseObj.Get("Health").value);
                            int entityId = databaseObj.Get("Id") == null ? 0 : int.Parse(databaseObj.Get("Id").value);
                            int x = databaseObj.Get("X") == null ? 0 : int.Parse(databaseObj.Get("X").value);
                            int y = databaseObj.Get("Y") == null ? 0 : int.Parse(databaseObj.Get("Y").value);
                            NPC npc = NPC.Get(entityId);
                            foreach (var kv in NPC.EncodeMessage(message))
                            {
                                npc.Dialogs[kv.Key] = kv.Value;
                            }
                            npc.SetHealth(Health);
                            npc.AI = aiType;
                            npc.NpcType = battle == true ? NpcType.Battle : NpcType.NonBattle;
                            npc.SetPos(databaseObj.x / 32, databaseObj.y / 32);
                            npc.DisplayName = npcName;
                            Main.AddEntity(npc);
                        }
                        if (eventMapId == EventMapId.Warp)
                        {
                            int entityId = int.Parse(databaseObj.Get("Id").value);
                            int facing = databaseObj.Get("Facing") == null ? 0 : int.Parse(databaseObj.Get("Facing").value);
                            string map = databaseObj.Get("MapName") == null ? "" : databaseObj.Get("MapName").value;
                            int x = databaseObj.Get("X") == null ? 0 : int.Parse(databaseObj.Get("X").value);
                            int y = databaseObj.Get("Y") == null ? 0 : int.Parse(databaseObj.Get("Y").value);
                            var objectss = ObjectEntity.Get(ObjectType.Warp);
                            if (objectss is ObjectWarp warpObject)
                            {
                                warpObject.SetPos(databaseObj.x / 32, databaseObj.y / 32);
                                warpObject.X = x;
                                warpObject.Y = y;
                                warpObject.MapName = map;
                                warpObject.Facing = (Facing)Enum.ToObject(typeof(Facing), facing);
                                Main.AddObject(warpObject);
                            }
                        }
                        if (eventMapId == EventMapId.Sign)
                        {
                            int entityId = int.Parse(databaseObj.Get("Id").value);
                            int x = int.Parse(databaseObj.Get("X").value);
                            int y = int.Parse(databaseObj.Get("Y").value);
                            var message = databaseObj.Get("Message").value;
                            var sign = ObjectEntity.Get(0);
                            if (sign is ObjectSign signObject)
                            {
                                signObject.Message = (string)message;
                                sign.SetPos(x, y);
                                Loggers.Debug($"Adding Object Sign at {x} {y}, {sign.whoAmI}");
                                Main.AddObject(sign);
                            }
                        }
                        if (eventMapId == EventMapId.Spawn)
                        {
                            int entityId = int.Parse(databaseObj.Get("Id").value);
                            int x = int.Parse(databaseObj.Get("X").value);
                            int y = int.Parse(databaseObj.Get("Y").value);
                            if (Main.GetActivePlayer != null)
                            {
                                Main.GetActivePlayer.SetPos(x, y);
                            }
                        }
                    }
                }
            }
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
                            bool interacted = databaseObj.Bounds.Intersects(Main.GetActivePlayer.InteractionBox);
                            if (eventMapId == EventMapId.Warp)
                            {
                                int facing = databaseObj.Get("Facing") == null ? 0 : int.Parse(databaseObj.Get("Facing").value);
                                string map = databaseObj.Get("MapName") == null ? "" : databaseObj.Get("MapName").value ;
                                int x = databaseObj.Get("X") == null ? 0 : int.Parse(databaseObj.Get("X").value);
                                int y = databaseObj.Get("Y") == null ? 0 : int.Parse(databaseObj.Get("Y").value);
                                //var objectEntity = ObjectEntity.Get(1);
                                //if (objectEntity is ObjectWarp warpObject)
                                //{
                                //    bool interacted1 = warpObject.DrawingBox.Intersects(Main.GetActivePlayer.DrawingBox);
                                //    if (interacted1 == true  && (int)Main.GetActivePlayer.Facing == facing)
                                //    {
                                //        Main.GameState = GameState.Pause;
                                //        Main.FadeAwayBegin = true;
                                //        Main.FadeAwayOnStart = () =>
                                //        {
                                //            Main.SoundEngine.Play(AudioAssets.WarpedSFX.Value);
                                //        };
                                //        Main.FadeAwayOnEnd = () =>
                                //        {
                                //            if (World.Get(map).Id != Main.MapIds)
                                //            {
                                //                Loggers.Debug("Map loading!!");
                                //                Main.Tiled.LoadMap(World.Get(map));
                                //            }
                                //            Main.GetActivePlayer.SetPos(x, y);
                                //            Main.GameState = GameState.Play;
                                //        };
                                //    }
                                //}
                                //if (interacted == true && (int)Main.GetActivePlayer.Facing == facing)
                                //{
                                //    Main.GameState = GameState.Pause;
                                //    Main.FadeAwayBegin = true;
                                //    Main.FadeAwayOnStart = () =>
                                //    {
                                //        Main.SoundEngine.Play(AudioAssets.WarpedSFX.Value);
                                //    };
                                //    Main.FadeAwayOnEnd = () =>
                                //    {
                                //        if (World.Get(map).Id != Main.MapIds)
                                //        {
                                //            Loggers.Debug("Map loading!!");
                                //            Main.Tiled.LoadMap(World.Get(map));
                                //        }
                                //        Main.GetActivePlayer.SetPos(x, y);
                                //        Main.GameState = GameState.Play;
                                //    };
                                //}
                            }
                            if (eventMapId == EventMapId.Event)
                            {
                                int id = int.Parse(databaseObj.Get("Id").value);
                                int trigger = int.Parse(databaseObj.Get("TriggerEvent").value);
                                if (interacted == true && trigger == 2)
                                {
                                }
                            }
                        }
                    }
                }
            }
        }
        

        public (TiledMapTileset tileset, int localId) GetTileset(int gid)
        {
            var ts = map.Tilesets;
            var tileset = ts.Where(t => gid >= t.firstgid).OrderByDescending(t => t.firstgid).First();

            int localId = gid - tileset.firstgid;

            return (tileset, localId);
        }

        public bool IsTilePassable(string getLayers, Rectangle rect)
        {
            try
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
                                var (tileset, localId) = GetTileset(gid);
                                if (localId == 1)
                                {
                                    var destination = new Rectangle(tileX, tileY + map.TileHeight, map.TileWidth, 5);
                                    if (rect.Intersects(destination))
                                    {
                                        return true;
                                    }
                                }
                                if (localId == 2)
                                {
                                    var destination = new Rectangle(tileX - map.TileWidth, tileY, 5, map.TileHeight);
                                    if (rect.Intersects(destination))
                                    {
                                        return true;
                                    }
                                }
                                if (localId == 8)
                                {
                                    var destination = new Rectangle(tileX, tileY, map.TileWidth, 5);
                                    if (rect.Intersects(destination))
                                    {
                                        return true;
                                    }
                                }
                                if (localId == 15)
                                {
                                    var destination = new Rectangle(tileX, tileY, map.TileWidth, map.TileHeight);
                                    if (rect.Intersects(destination))
                                    {
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                {
                    Loggers.Error($"{ex}");
                    throw new TiledException("" + ex);
                }
            }
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
                    Sprite sprites = new Sprite(renderEntity.Name, renderEntity);
                    sprites.Draw(Main.SpriteBatch);
                }
            }
        }
        public void Draw(SpriteBatch sprite)
        {
            bool entitiesDrawn = false;
            var player = Main.GetActivePlayer;
            Vector2 playerPosition = Main.Camera.Position;
            Rectangle boundingBoxDraw = new Rectangle((int) playerPosition.X, (int) playerPosition.Y, Main.WindowScreen.Width, Main.WindowScreen.Height);
            foreach (var orderedLayer in this.CreateOrderedLayer(map))
            {
                var layer = orderedLayer.Layer;
                if (layer.name == "passage") continue;
                if (orderedLayer.Order == 1)
                {
                    this.DrawTiles(sprite, layer, boundingBoxDraw);
                    continue;
                }
                if (entitiesDrawn == false)
                {
                    this.DrawNpcs();
                    entitiesDrawn = true;
                }
                this.DrawTiles(sprite, layer, boundingBoxDraw);
            }
        }

        private void DrawTiles(SpriteBatch sprite, TiledLayer layer, Rectangle boundingBoxDraw)
        {
            //var tileLayers = map.Layers.Where(x => x.type == TiledLayerType.TileLayer);
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
        }
    }
}
