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
                    if (entity.whoAmI == objects.whoAmI)
                    {
                        continue;
                    }
                    if (entity.InteractionBox.Intersects(objects.InteractionBox))
                    {
                        Loggers.Info($"Interacting: {entity.DisplayName} {objects.DisplayName}");
                        entity.Position = entity.PrevPosition;
                    }
                    if (entity.TargetInteractionBox.Intersects(objects.InteractionBox))
                    {
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
                    if (entity.TargetInteractionBox.Intersects(objects.InteractionBox))
                    {
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
