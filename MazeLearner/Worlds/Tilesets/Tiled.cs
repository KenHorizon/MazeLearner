using Assimp;
using MazeLearner.Audio;
using MazeLearner.GameContent.Entity;
using MazeLearner.GameContent.Entity.AI;
using MazeLearner.GameContent.Entity.Objects;
using MazeLearner.GameContent.Entity.Player;
using MazeLearner.Graphics.Animation;
using MazeLearner.Graphics.Asset;
using MazeLearner.Screen;
using MazeLearner.Screen.Widgets;
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
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Xml;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;

namespace MazeLearner.Worlds.Tilesets
{
    public class Tiled
    {
        private Main game;
        public string mapName {  get; set; }
        private int _w;
        private int _h;
        private TiledMap map;
        private Dictionary<int, TiledTileset> tilesets;
        //private Texture2D[] tilesetTexture = new Texture2D[999];
        // Ken: Well i change this into dictionary to store the First GID of tileset and the texture of the tilesets
        // so when rendering only the the id of the tileset will be called to render
        private Dictionary<int, Texture2D> tilesetTexture = new Dictionary<int, Texture2D>();
        private bool teleport=  false;
        public Tiled(Main game)
        {
            this.game = game;
        }

        public void LoadMap(World world)
        {
            ObjectDatabase.Clear();
            this.tilesetTexture.Clear();
            Main.CurrentWorld = world;
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
                if (tileset.Value.Name == "events") continue;
                if (tileset.Value.Name == "passage") continue;
                this.tilesetTexture.Add(tileset.Key, Asset<Texture2D>.Request($"Data/Tiled/Assets/{tileset.Value.Name}").Value);

            }
            var layers = map.Layers.Where(x => x.type == TiledLayerType.TileLayer);
            foreach (var layer in layers)
            {
                int w = layer.width;
                int h = layer.height;
                this._w = w;
                this._h = h;
            }
            Main.Pathfinding = new Pathfinding(this.game);

            //this.OnLoadMap();
            this.SpawnPlayer();
            Threads.RunAsync(() =>
            {
                this.OnLoadMap();
            });
        }

        public TiledMap GetCurrentMap => this.map;
        public int Width => this._w;
        public int Height => this._h;

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
            var objectLayers = map.Layers.Where(x => x.type == TiledLayerType.ObjectLayer);
            foreach (var databaseObj in ObjectDatabase.GetAll)
            {
                if (databaseObj == null) continue;
                EventMapId eventMapId = (EventMapId)Enum.ToObject(typeof(EventMapId), int.Parse(databaseObj.Get("EventMap").value));
                if (eventMapId == EventMapId.None) continue;
                if (eventMapId == EventMapId.Npc)
                {
                    bool isboss = databaseObj.IntValue("IsBoss") == 1;
                    bool battle = databaseObj.IntValue("Battle") == 1;
                    int uniqueId = databaseObj.IntValue("NpcId"); // Unique Id
                    int questionCat = databaseObj.IntValue("QuestionCategory");
                    int battleLevel = databaseObj.IntValue("BattleLevel");
                    int aiType = databaseObj.IntValue("AIType");
                    string npcName = databaseObj.StringValue("Name");
                    string message = databaseObj.StringValue("Dialog");
                    int Health = databaseObj.IntValue("Health");
                    int entityId = databaseObj.IntValue("Id");
                    int btimg = databaseObj.IntValue("BattleImage");
                    int x = databaseObj.IntValue("X");
                    int y = databaseObj.IntValue("Y");
                    int scorePts = databaseObj.IntValue("ScorePoints");
                    int facing = databaseObj.IntValue("Facing", -1);
                    int range = databaseObj.IntValue("Range");
                    int tags = databaseObj.IntValue("EventTags", -1);
                    NPC npc = NPC.Get(entityId);
                    npc.SetHealth(Health);
                    npc.Active = true;
                    npc.QuestionCategory = (QuestionType)questionCat;
                    npc.BattleLevel = battleLevel;
                    npc.whoAmI = uniqueId;
                    npc.IsBoss = isboss;
                    npc.DetectionRange = range;
                    npc.AI = aiType;
                    npc.Direction = (Direction)Enum.ToObject(typeof(Direction), facing);
                    if (facing > 0)
                    {
                        npc.WantedDirection = (Direction)Enum.ToObject(typeof(Direction), facing);
                    }
                    if (tags == 0)
                    {
                        npc.Invisible = true;
                    }
                    if (tags == 1)
                    {
                        npc.NoLook = true;
                    }
                    npc.NpcType = battle == true ? NpcType.Battle : NpcType.NonBattle;
                    Vector2 pos = new Vector2(databaseObj.x, databaseObj.y) / Main.TileSize;
                    npc.SetPos((int)pos.X, (int)pos.Y);
                    npc.DisplayName = npcName;
                    npc.SetDefaults();
                    npc.Portfolio = btimg;
                    npc.ScorePointDrops = scorePts;
                    foreach (var kv in Utils.ParseAsDialog(message))
                    {
                        npc.SetupDialogs(kv.Key, kv.Value);
                    }

                    Main.AddEntity(npc);
                }
                if (eventMapId == EventMapId.Warp)
                {
                    int uniqueId = databaseObj.IntValue("NpcId"); // Unique Id
                    int entityId = databaseObj.IntValue("Id");
                    int cutscene = databaseObj.IntValue("Cutscene", -1);
                    int facing = databaseObj.IntValue("Facing");
                    string map = databaseObj.StringValue("MapName");
                    int directionAfterTeleport = databaseObj.IntValue("DirectionAfterTeleport", -1);
                    int x = databaseObj.IntValue("X");
                    int y = databaseObj.IntValue("Y");
                    var objectsss = ObjectEntity.Get(ObjectType.Warp);
                    if (objectsss is ObjectWarp warpObject)
                    {
                        warpObject.whoAmI = uniqueId;
                        Vector2 pos = new Vector2(databaseObj.x, databaseObj.y) / Main.TileSize;
                        warpObject.SetPos((int)pos.X, (int)pos.Y);
                        warpObject.X = x;
                        warpObject.Y = y;
                        warpObject.MapName = map;
                        warpObject.Facing = (Direction)Enum.ToObject(typeof(Direction), facing);
                        warpObject.FacingAfterTeleport = directionAfterTeleport;
                        Main.AddObject(warpObject);
                    }
                }
                if (eventMapId == EventMapId.Sign)
                {
                    int uniqueId = databaseObj.IntValue("NpcId"); // Unique Id
                    var message = databaseObj.StringValue("Dialog");
                    var sign = ObjectEntity.Get(ObjectType.Sign);
                    sign.whoAmI = uniqueId;
                    Vector2 pos = new Vector2(databaseObj.x, databaseObj.y) / Main.TileSize;
                    sign.SetPos((int)pos.X, (int)pos.Y);
                    sign.SetDefaults();
                    foreach (var kv in Utils.ParseAsDialog(message))
                    {
                        sign.SetupDialogs(kv.Key, kv.Value);
                    }
                    Main.AddObject(sign);
                }
                Main.IsMapContentLoaded = true;
            }
        }
        public virtual void SpawnPlayer()
        {
            Thread.Sleep(3000);
            this.LoadGameObjects();
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
                        if (eventMapId == EventMapId.Spawn)
                        {
                            int entityId = int.Parse(databaseObj.Get("Id").value);
                            int x = int.Parse(databaseObj.Get("X").value);
                            int y = int.Parse(databaseObj.Get("Y").value);
                            if (Main.ActivePlayer != null && Main.ActivePlayer.IsLoadedNow == false)
                            {
                                Main.ActivePlayer.SetPos(x, y);
                            }
                        }
                    }
                }
            }
        }
        public void Update(GameTime gameTime)
        {
            
        }
        
        public (TiledMapTileset tileset, int localId) GetTileset(int gid)
        {
            var ts = map.Tilesets;
            var tileset = ts.Where(t => gid >= t.firstgid).OrderByDescending(t => t.firstgid).First();

            int localId = gid - tileset.firstgid;

            return (tileset, localId);
        }
        public bool IsWalkable(Rectangle rect)
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
                            if (layer.name == "passage")
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
                                    Rectangle destination = new Rectangle(tileX, tileY + map.TileHeight, map.TileWidth, 5);
                                    if (rect.Intersects(destination))
                                    {
                                        return false;
                                    }
                                }
                                else if (localId == 2)
                                {
                                    Rectangle destination = new Rectangle(tileX - map.TileWidth, tileY, 5, map.TileHeight);
                                    if (rect.Intersects(destination))
                                    {
                                        return false;
                                    }
                                }
                                else if (localId == 4)
                                {
                                    Rectangle destination = new Rectangle(tileX, tileY, 5, map.TileHeight);
                                    if (rect.Intersects(destination))
                                    {
                                        return false;
                                    }
                                }
                                else if (localId == 8)
                                {
                                    Rectangle destination = new Rectangle(tileX, tileY, map.TileWidth, 5);
                                    if (rect.Intersects(destination))
                                    {
                                        return false;
                                    }
                                }
                                else if (localId == 15)  // Full 32x32 Collision
                                {
                                    Rectangle destination = new Rectangle(tileX, tileY, map.TileWidth, map.TileHeight);
                                    if (rect.Intersects(destination))
                                    {
                                        return false;
                                    }
                                }
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                {
                    Loggers.Error($"{ex}");
                    throw new TiledException("" + ex);
                }
            }
        }
        public bool IsWalkable(Vector2 position)
        {
            try
            {
                var tileLayers = map.Layers.Where(x => x.type == TiledLayerType.TileLayer);
                Rectangle rect = new Rectangle((int)position.X, (int)position.Y, Main.TileSize, Main.TileSize);
                foreach (var layer in tileLayers)
                {
                    for (var y = 0; y < layer.height; y++)
                    {
                        for (var x = 0; x < layer.width; x++)
                        {
                            if (layer.name == "passage")
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
                                    rect = new Rectangle((int)position.X, (int)position.Y + Main.TileSize, Main.TileSize, 5);
                                    var destination = new Rectangle(tileX, tileY + map.TileHeight, map.TileWidth, 5);
                                    if (rect.Contains(destination))
                                    {
                                        return false;
                                    }
                                }
                                else if (localId == 2)
                                {
                                    rect = new Rectangle((int)position.X, (int)position.Y, 5, Main.TileSize);
                                    var destination = new Rectangle(tileX, tileY, 5, map.TileHeight);
                                    if (rect.Contains(destination))
                                    {
                                        return false;
                                    }
                                }
                                else if (localId == 4)
                                {
                                    rect = new Rectangle((int)position.X + Main.TileSize, (int)position.Y, 5, Main.TileSize);
                                    var destination = new Rectangle(tileX + Main.TileSize, tileY, 5, map.TileHeight);
                                    if (rect.Contains(destination))
                                    {
                                        return false;
                                    }
                                }
                                else if (localId == 8)
                                {
                                    rect = new Rectangle((int)position.X, (int)position.Y + Main.TileSize, Main.TileSize, 5);
                                    var destination = new Rectangle(tileX, tileY, map.TileWidth, 5);
                                    if (rect.Contains(destination))
                                    {
                                        return false;
                                    }
                                }
                                else if (localId == 15)  // Full 32x32 Collision
                                {
                                    var destination = new Rectangle(tileX, tileY, map.TileWidth, map.TileHeight);
                                    if (rect.Contains(destination))
                                    {
                                        return false;
                                    }
                                }
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                {
                    Loggers.Error($"{ex}");
                    throw new TiledException("" + ex);
                }
            }
        }
        public bool IsWalkable(NPC npc)
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
                            if (layer.name == "passage")
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
                                    var destination = new Rectangle(tileX, tileY + (map.TileHeight - 5), map.TileWidth, 5);
                                    (bool tilePassable, bool value) = IsPassable(npc, destination);
                                    if (tilePassable == false)
                                    {
                                        return value;
                                    }
                                }
                                else if (localId == 2)
                                {
                                    var destination = new Rectangle(tileX, tileY, 5, map.TileHeight);
                                    (bool tilePassable, bool value) = IsPassable(npc, destination);
                                    if (tilePassable == false)
                                    {
                                        return value;
                                    }
                                }
                                else if (localId == 4)
                                {
                                    var destination = new Rectangle(tileX + Main.TileSize, tileY, 5, map.TileHeight);
                                    (bool tilePassable, bool value) = IsPassable(npc, destination);
                                    if (tilePassable == false)
                                    {
                                        return value;
                                    }
                                }
                                else if (localId == 8)
                                {
                                    var destination = new Rectangle(tileX, tileY, map.TileWidth, 5);
                                    (bool tilePassable, bool value) = IsPassable(npc, destination);
                                    if (tilePassable == false)
                                    {
                                        return value;
                                    }
                                }
                                else if (localId == 15)  // Full 32x32 Collision
                                {
                                    var destination = new Rectangle(tileX, tileY, map.TileWidth, map.TileHeight);
                                    (bool tilePassable, bool value) = IsPassable(npc, destination);
                                    if (tilePassable == false)
                                    {
                                        return value;
                                    }
                                }
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                {
                    Loggers.Error($"{ex}");
                    throw new TiledException("" + ex);
                }
            }
        }

        private static (bool flowControl, bool value) IsPassable(NPC npc, Rectangle destination)
        {
            if (npc.Direction == Direction.Up && npc.HitboxNorth.Intersects(destination)
                                                    || npc.Direction == Direction.Down && npc.HitboxSouth.Intersects(destination)
                                                    || npc.Direction == Direction.Left && npc.HitboxEast.Intersects(destination)
                                                    || npc.Direction == Direction.Right && npc.HitboxWest.Intersects(destination))
            {
                return (flowControl: false, value: false);
            }

            return (flowControl: true, value: default);
        }

        public bool DebugIsWalkable(Vector2 position)
        {
            try
            {
                var tileLayers = map.Layers.Where(x => x.type == TiledLayerType.TileLayer);
                Rectangle rect;
                foreach (var layer in tileLayers)
                {
                    for (var y = 0; y < layer.height; y++)
                    {
                        for (var x = 0; x < layer.width; x++)
                        {
                            if (layer.name == "passage")
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
                                    var destination = new Rectangle(tileX, tileY + (map.TileHeight - 5), map.TileWidth, 5);
                                    Main.SpriteBatch.Draw(Main.FlatTexture, destination, Color.Red * 0.5F);
                                }
                                else if (localId == 2)
                                {
                                    var destination = new Rectangle(tileX , tileY, 5, map.TileHeight);
                                    Main.SpriteBatch.Draw(Main.FlatTexture, destination, Color.Red * 0.5F);
                                }
                                else if (localId == 4)
                                {
                                    var destination = new Rectangle(tileX + Main.TileSize, tileY, 5, map.TileHeight);

                                    Main.SpriteBatch.Draw(Main.FlatTexture, destination, Color.Red * 0.5F);
                                }
                                else if (localId == 8)
                                {
                                    var destination = new Rectangle(tileX, tileY, map.TileWidth, 5);
                                    Main.SpriteBatch.Draw(Main.FlatTexture, destination, Color.Red * 0.5F);
                                }
                                else if (localId == 15)  // Full 32x32 Collision
                                {
                                    var destination = new Rectangle(tileX, tileY, map.TileWidth, map.TileHeight);

                                    Main.SpriteBatch.Draw(Main.FlatTexture, destination, Color.Red * 0.5F);
                                }
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                {
                    Loggers.Error($"{ex}");
                    throw new TiledException("" + ex);
                }
            }
        }
        public Vector2 ToWorld(Point p) => new Vector2(p.X * Main.TileSize + Main.TileSize / 2.0F, p.Y * Main.TileSize + Main.TileSize / 2.0F);
        public Point ToGrid(Vector2 worldPos) =>
            new Point((int)MathF.Floor(worldPos.X / Main.TileSize),
                      (int)MathF.Floor(worldPos.Y / Main.TileSize));

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
                    Sprite sprite = new Sprite(renderEntity.DisplayName, renderEntity);
                    sprite.Draw(Main.SpriteBatch);
                    //renderEntity.Sprites.Draw(Main.SpriteBatch);
                }
            }
        }
        public void Draw(SpriteBatch sprite)
        {
            bool entitiesDrawn = false;
            var player = Main.ActivePlayer;
            Vector2 playerPosition = Main.Camera.Position;
            float sizeScale = 1.0F;
            Rectangle boundingBoxDraw = new Rectangle((int) playerPosition.X, (int) playerPosition.Y,
                (int)(Main.WindowScreen.Width * sizeScale),
                (int)(Main.WindowScreen.Height * sizeScale));
            foreach (var orderedLayer in this.CreateOrderedLayer(map))
            {
                var layer = orderedLayer.Layer;
                if (layer.name == "Objects") continue;
                if (layer.name == "passage") continue;
                if (orderedLayer.Order == 1)
                {
                    this.DrawTiles(sprite, layer, boundingBoxDraw);
                    continue;
                }
                if (entitiesDrawn == false)
                {
                    for (int i = 0; i < Main.Particles[1].Length; i++)
                    {
                        if (Main.Particles[Main.MapIds][i] != null && Main.Particles[Main.MapIds][i].Active == true)
                        {
                            if (Main.Particles[Main.MapIds][i].Top == false)
                            {
                                Main.Particles[Main.MapIds][i].Draw(Main.SpriteBatch);
                            }
                        }
                    }
                    this.DrawNpcs();
                    for (int i = 0; i < Main.Particles[1].Length; i++)
                    {
                        if (Main.Particles[Main.MapIds][i] != null && Main.Particles[Main.MapIds][i].Active == true)
                        {
                            if (Main.Particles[Main.MapIds][i].Top == true)
                            {
                                Main.Particles[Main.MapIds][i].Draw(Main.SpriteBatch);
                            }
                        }
                    }
                    entitiesDrawn = true;
                }
                this.DrawTiles(sprite, layer, boundingBoxDraw);
                //this.DebugIsWalkable(Main.ActivePlayer.TargetPosition);
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
                    var texture = this.tilesetTexture[mapTileset.firstgid];

                    // Use the connection object as well as the tileset to figure out the source rectangle
                    var rect = map.GetSourceRect(mapTileset, tileset, gid);

                    // Create destination and source rectangles
                    var source = new Rectangle(rect.x, rect.y, rect.width, rect.height);

                    var destination = new Rectangle(tileX, tileY, map.TileWidth, map.TileHeight);

                    // You can use the helper methods to get information to handle flips and rotations

                    // Render sprite at position tileX, tileY using the rect

                    sprite.Draw(texture, destination, source, Color.White, 0.0F, Vector2.Zero, SpriteEffects.None, 0.0F);
                }
            }
        }
    }
}
