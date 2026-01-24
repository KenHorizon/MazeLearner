using MazeLearner.GameContent.Entity;
using MazeLearner.GameContent.Entity.Player;
using Microsoft.Xna.Framework;
using System;

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
            int leftWorldX = (int)(npc.Position.X + npc.InteractionBox.X);
            int rightWorldX = (int)(npc.Position.X + npc.InteractionBox.X + npc.InteractionWidth);

            int topWorldY = (int)(npc.Position.Y + npc.InteractionBox.Y);
            int bottomWorldY = (int)(npc.Position.Y + npc.InteractionBox.Y + npc.InteractionHeight);

            int leftCol = leftWorldX / Main.MaxTileSize;
            int rightCol = rightWorldX / Main.MaxTileSize;
            int topCol = topWorldY / Main.MaxTileSize;
            int bottomCol = bottomWorldY / Main.MaxTileSize;
            switch (npc.Facing)
            {
            }
        }

        public int CheckObjects(NPC entity, bool isPlayer)
        {
            int index = 999;
            for (int i = 0; i < Main.NPCS.Length; i++)
            {
                NPC objects = Main.NPCS[i];
                if (objects != null)
                {
                    switch (entity.Facing)
                    {
                        case Facing.Down:
                            {
                                int x = entity.InteractionBox.X;
                                int y = (int)(entity.InteractionBox.Y + entity.FacingBoxH);
                                entity.FacingBox = new Rectangle(x, y, entity.FacingBoxH, entity.FacingBoxW);
                                if (entity.FacingBox.Intersects(objects.InteractionBox))
                                {
                                    entity.CanCollideEachOther = true;
                                    if (isPlayer == true)
                                    {
                                        index = i;
                                    }
                                    if (entity is PlayerEntity player)
                                    {
                                        if (player.DoInteract() && objects.IsAlive == true && objects is InteractableNPC interactable)
                                        {
                                            player.objectIndexs = index;
                                            interactable.Interacted(player);
                                        }
                                    }
                                }
                                break;
                            }
                        case Facing.Up:
                            {
                                int x = entity.InteractionBox.X;
                                int y = (int)(entity.InteractionBox.Y - entity.FacingBoxW);
                                entity.FacingBox = new Rectangle(x, y, entity.FacingBoxH, entity.FacingBoxW);
                                if (entity.FacingBox.Intersects(objects.InteractionBox))
                                {
                                    entity.CanCollideEachOther = true;
                                    if (isPlayer == true)
                                    {
                                        index = i;
                                    }
                                    if (entity is PlayerEntity player)
                                    {
                                        if (player.DoInteract() && objects.IsAlive == true && objects is InteractableNPC interactable)
                                        {
                                            player.objectIndexs = index;
                                            interactable.Interacted(player);
                                        }
                                    }
                                }
                                break;
                            }
                        case Facing.Left:
                            {
                                int x = (int)(entity.InteractionBox.X - entity.FacingBoxW);
                                int y = entity.InteractionBox.Y;
                                entity.FacingBox = new Rectangle(x, y, entity.FacingBoxW, entity.FacingBoxH);
                                if (entity.FacingBox.Intersects(objects.InteractionBox))
                                {
                                    entity.CanCollideEachOther = true;
                                    if (isPlayer == true)
                                    {
                                        index = i;
                                    }
                                    if (entity is PlayerEntity player)
                                    {
                                        if (player.DoInteract() && objects.IsAlive == true && objects is InteractableNPC interactable)
                                        {
                                            player.objectIndexs = index;
                                            interactable.Interacted(player);
                                        }
                                    }
                                }
                                break;
                            }
                        case Facing.Right:
                            {
                                int x = (int)(entity.InteractionBox.X + entity.FacingBoxH);
                                int y = entity.InteractionBox.Y;
                                entity.FacingBox = new Rectangle(x, y, entity.FacingBoxW, entity.FacingBoxH);
                                if (entity.FacingBox.Intersects(objects.InteractionBox))
                                {
                                    entity.CanCollideEachOther = true; 
                                    if (isPlayer == true)
                                    {
                                        index = i;
                                    }
                                    if (entity is PlayerEntity player)
                                    {
                                        if (player.DoInteract() && objects.IsAlive == true && objects is InteractableNPC interactable)
                                        {
                                            player.objectIndexs = index;
                                            interactable.Interacted(player);
                                        }
                                    }
                                }
                                break;
                            }
                    }
                }
            }
            return index;
        }
    }
}
