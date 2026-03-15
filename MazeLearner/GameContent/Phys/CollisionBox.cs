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
                var npcs = Main.Npcs[Main.MapIds][i];
                if (npcs != null)
                {
                    if (entity.whoAmI != i)
                    {
                        if (entity.InteractionBox.Intersects(npcs.InteractionBox))
                        {
                            if (isPlayer == false)
                            {
                                Loggers.Debug($"1. P {isPlayer} entity : {entity.whoAmI} target: {npcs.whoAmI}");
                            }
                            entity.Position = entity.PrevPosition;
                        }

                        if (entity.TargetInteractionBox.Intersects(npcs.InteractionBox))
                        {
                            if (isPlayer == false)
                            {
                                Loggers.Debug($"2. P {isPlayer} entity : {entity.whoAmI} target: {npcs.whoAmI}");
                            }
                            index = CollisionCheck(entity, isPlayer, index, i);
                        }
                    }
                }
            }
            var players = Main.ActivePlayer;
            if (players != null)
            {
                if (entity is PlayerEntity == false)
                {
                    if (entity.InteractionBox.Intersects(players.InteractionBox))
                    {
                        entity.Position = entity.PrevPosition;
                    }

                    if (entity.TargetInteractionBox.Intersects(players.InteractionBox))
                    {
                        index = CollisionCheck(entity, isPlayer, index, players.whoAmI);
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
                        index = CollisionCheck(entity, isPlayer, index, i);
                    }
                }
            }
            return index;
        }
        private static int CollisionCheck(NPC entity, bool isPlayer, int index, int i)
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
