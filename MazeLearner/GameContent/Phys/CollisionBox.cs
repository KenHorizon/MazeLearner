using MazeLearner.GameContent.Entity;
using MazeLearner.GameContent.Entity.Player;
using Microsoft.Xna.Framework;
using System;
using System.Reflection;

namespace MazeLearner.GameContent.Phys
{
    public class CollisionBox
    {
        protected Main game;
        public CollisionBox(Main game)
        {
            this.game = game;
        }

        public void CheckTiles(NPC npc)
        {
            //int leftWorldX = (int)(npc.Position.X + npc.InteractionBox.X);
            //int rightWorldX = (int)(npc.Position.X + npc.InteractionBox.X + npc.InteractionWidth);

            //int topWorldY = (int)(npc.Position.Y + npc.InteractionBox.Y);
            //int bottomWorldY = (int)(npc.Position.Y + npc.InteractionBox.Y + npc.InteractionHeight);

            //int leftCol = leftWorldX / Main.MaxTileSize;
            //int rightCol = rightWorldX / Main.MaxTileSize;
            //int topRow = topWorldY / Main.MaxTileSize;
            //int bottomRow = bottomWorldY / Main.MaxTileSize;
            var passage = Main.TilesetManager.IsTilePassable("passage", npc.FacingBox);
            npc.CanCollideEachOther = passage;
        }

        public int CheckObjects(NPC entity, bool isPlayer)
        {
            int index = 999;
            for (int i = 0; i < Main.Npcs[1].Length; i++)
            {
                NPC objects = Main.Npcs[Main.MapIds][i];
                if (objects != null)
                {
                    switch (entity.Facing)
                    {
                        case Facing.Down:
                            {
                                if (entity.FacingBox.Intersects(objects.InteractionBox))
                                {
                                    index = CollisionCheck(entity, isPlayer, index, i, objects);
                                }
                                break;
                            }
                        case Facing.Up:
                            {
                                if (entity.FacingBox.Intersects(objects.InteractionBox))
                                {
                                    index = CollisionCheck(entity, isPlayer, index, i, objects);
                                }
                                break;
                            }
                        case Facing.Left:
                            {
                                if (entity.FacingBox.Intersects(objects.InteractionBox))
                                {
                                    index = CollisionCheck(entity, isPlayer, index, i, objects);
                                }
                                break;
                            }
                        case Facing.Right:
                            {
                                if (entity.FacingBox.Intersects(objects.InteractionBox))
                                {
                                    index = CollisionCheck(entity, isPlayer, index, i, objects);
                                }
                                break;
                            }
                    }
                }
            }
            return index;
        }

        private static int CollisionCheck(NPC entity, bool isPlayer, int index, int i, NPC objects)
        {
            entity.CanCollideEachOther = true;
            if (isPlayer == true)
            {
                index = i;
            }

            return index;
        }
    }
}
