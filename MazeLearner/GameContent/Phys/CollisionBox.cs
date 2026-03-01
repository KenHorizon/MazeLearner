using MazeLearner.GameContent.Entity;
using MazeLearner.GameContent.Entity.Objects;
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

        public int CheckNpc(NPC entity, bool isPlayer)
        {
            int index = 999;
            for (int i = 0; i < Main.Npcs[1].Length; i++)
            {
                var objects = Main.Npcs[Main.MapIds][i];
                if (objects != null)
                {
                    var rect = new Rectangle((int)entity.TargetPosition.X, (int)entity.TargetPosition.Y, Main.TileSize, Main.TileSize);
                    if (rect.Intersects(objects.InteractionBox) || entity.FacingBox.Intersects(objects.InteractionBox))
                    {
                        entity.Position = entity.PrevPosition;
                        index = CollisionCheck(entity, isPlayer, index, i, objects);
                    }
                }
            }
            return index;
        }
        public int CheckObject(NPC entity, bool isPlayer)
        {
            int index = 999;
            for (int i = 0; i < Main.Objects[1].Length; i++)
            {
                var objects = Main.Objects[Main.MapIds][i];
                if (objects != null)
                {
                    var rect = new Rectangle((int)entity.TargetPosition.X + 2, (int)entity.TargetPosition.Y + 2, Main.TileSize - 2, Main.TileSize - 2);
                    if (rect.Intersects(objects.InteractionBox))
                    {
                        entity.Position = entity.PrevPosition;
                        index = CollisionCheck(entity, isPlayer, index, i, objects);
                    }
                    if (entity.FacingBox.Intersects(objects.InteractionBox))
                    {
                        entity.Position = entity.PrevPosition;
                        index = CollisionCheck(entity, isPlayer, index, i, objects);
                    }
                }
            }
            return index;
        }
        private static int CollisionCheck(NPC entity, bool isPlayer, int index, int i, BaseEntity objects)
        {
            entity.CollideOn = true;
            if (isPlayer == true)
            {
                index = i;
            }

            return i;
        }
    }
}
