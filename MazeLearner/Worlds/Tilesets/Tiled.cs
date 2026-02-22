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
using static System.Net.WebRequestMethods;

namespace MazeLearner.Worlds.Tilesets
{
    public class Tiled
    {
        private Main game;
        public string mapName {  get; set; }
        private TiledMap map;
        private Dictionary<int, TiledTileset> tilesets;
        //private Texture2D[] tilesetTexture = new Texture2D[999];
        // Ken: Well i change this into dictionary to store the First GID of tileset and the texture of the tilesets
        // so when rendering only the the id of the tileset will be called to render
        private Dictionary<int, Texture2D> tilesetTexture = new Dictionary<int, Texture2D>();
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
            this.tilesetTexture.Clear();
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
                            bool battle = databaseObj.IntValue("Battle") == 1;
                            int aiType = databaseObj.IntValue("AIType");
                            string npcName = databaseObj.StringValue("Name") ;
                            string message = databaseObj.StringValue("Dialog");
                            int Health = databaseObj.IntValue("Health");
                            int entityId = databaseObj.IntValue("Id");
                            int btimg = databaseObj.IntValue("BattleImage");
                            int x = databaseObj.IntValue("X");
                            int y = databaseObj.IntValue("Y");
                            int scorePts = databaseObj.IntValue("ScorePoints");
                            NPC npc = NPC.Get(entityId);
                            npc.SetHealth(Health);
                            npc.AI = aiType;
                            npc.NpcType = battle == true ? NpcType.Battle : NpcType.NonBattle;
                            Vector2 pos = new Vector2(databaseObj.x, databaseObj.y) / Main.TileSize;
                            npc.SetPos((int)pos.X, (int)pos.Y);
                            npc.DisplayName = npcName;
                            npc.SetDefaults();
                            npc.Portfolio = btimg;
                            npc.ScorePointDrops = scorePts;
                            foreach (var kv in Utils.EncodeAsDialogs(message, npc))
                            {
                                npc.Dialogs[kv.Key] = kv.Value;
                            }
                            Main.AddEntity(npc);
                        }
                        if (eventMapId == EventMapId.Warp)
                        {
                            int entityId = int.Parse(databaseObj.Get("Id").value);
                            int facing = databaseObj.IntValue("Facing");
                            string map = databaseObj.StringValue("MapName");
                            int x = databaseObj.IntValue("X");
                            int y = databaseObj.IntValue("Y");
                            var objectsss = ObjectEntity.Get(ObjectType.Warp);
                            ObjectWarp objectss = new ObjectWarp();
                            if (objectsss is ObjectWarp warpObject)
                            {
                                Vector2 pos = new Vector2(databaseObj.x, databaseObj.y) / Main.TileSize;
                                warpObject.SetPos((int)pos.X, (int)pos.Y);
                                warpObject.X = x;
                                warpObject.Y = y;
                                warpObject.MapName = map;
                                warpObject.Facing = (Facing)Enum.ToObject(typeof(Facing), facing);
                                Main.AddObject(warpObject);
                            }
                        }
                        if (eventMapId == EventMapId.Sign)
                        {
                            int entityId = databaseObj.IntValue("Id");
                            var message = databaseObj.StringValue("Message");
                            var sign = ObjectEntity.Get(0);
                            if (sign is ObjectSign signObject)
                            {
                                Vector2 pos = new Vector2(databaseObj.x, databaseObj.y) / Main.TileSize;
                                sign.SetPos((int)pos.X, (int)pos.Y);
                                signObject.Message = (string) message;
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
            for (int i = 0; i < Main.Particles[1].Length; i++)
            {
                if (Main.Particles[Main.MapIds][i] != null && Main.Particles[Main.MapIds][i].Active == true)
                {
                    Main.Particles[Main.MapIds][i].Draw(Main.SpriteBatch);
                }
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
