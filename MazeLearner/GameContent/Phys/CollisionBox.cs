using MazeLearner.GameContent.Entity;
using MazeLearner.GameContent.Entity.Player;
using Microsoft.Xna.Framework;

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
        }

        public int CheckObjects(NPC entity, bool isPlayer)
        {
            int index = 999;
            for (int i = 0; i < Main.NPCS.Length; i++)
            {
                NPC objects = Main.NPCS[i];
                if (objects != null)
                {
                    // Get entity's solid are position (bounding box / interaction box)
                    int entityInteractionX = (int)(entity.Position.X + entity.InteractionBox.X);
                    int entityInteractionY = (int)(entity.Position.Y + entity.InteractionBox.Y);
                    // Get object or other entity solid are position (bounding box / interaction box)
                    int objectInteractionX = (int)(objects.Position.X + objects.InteractionBox.X);
                    int objectInteractionY = (int)(objects.Position.Y + objects.InteractionBox.Y);
                    switch (entity.Facing)
                    {
                        case Facing.Down:
                            {
                                int entitySpeedX = entity.InteractionBox.X;
                                int entitySpeedY = (int)(entity.InteractionBox.Y + entity.Speed);
                                Rectangle interactionBox = new Rectangle(entitySpeedX, entitySpeedY, BaseEntity.InteractionSize, BaseEntity.InteractionSize);
                                if (interactionBox.Intersects(objects.InteractionBox))
                                {
                                    if (objects.CanCollideEachOther == true)
                                    {
                                        entity.CanCollideEachOther = true;
                                    }
                                    if (isPlayer == true)
                                    {
                                        index = i;
                                    }
                                    if (entity is PlayerEntity player)
                                    {
                                        if (player.DoInteract() && objects is InteractableNPC interactable)
                                        {
                                            interactable.Interacted(player);
                                        }
                                    }
                                    Debugs.Msg($"Down Collision");
                                }
                                break;
                            }
                        case Facing.Up:
                            {
                                int entitySpeedX = entity.InteractionBox.X;
                                int entitySpeedY = (int)(entity.InteractionBox.Y - entity.Speed);
                                Rectangle interactionBox = new Rectangle(entitySpeedX, entitySpeedY, BaseEntity.InteractionSize, BaseEntity.InteractionSize);
                                if (interactionBox.Intersects(objects.InteractionBox))
                                {
                                    if (objects.CanCollideEachOther == true)
                                    {
                                        entity.CanCollideEachOther = true;
                                    }
                                    if (isPlayer == true)
                                    {
                                        index = i;
                                    }
                                    if (entity is PlayerEntity player)
                                    {
                                        if (player.DoInteract() && objects is InteractableNPC interactable)
                                        {
                                            interactable.Interacted(player);
                                        }
                                    }
                                    Debugs.Msg($"Up Collision");
                                }
                                break;
                            }
                        case Facing.Left:
                            {
                                int entitySpeedX = (int)(entity.InteractionBox.X - entity.Speed);
                                int entitySpeedY = entity.InteractionBox.Y;
                                Rectangle interactionBox = new Rectangle(entitySpeedX, entitySpeedY, BaseEntity.InteractionSize, BaseEntity.InteractionSize);
                                if (interactionBox.Intersects(objects.InteractionBox))
                                {
                                    if (objects.CanCollideEachOther == true)
                                    {
                                        entity.CanCollideEachOther = true;
                                    }
                                    if (isPlayer == true)
                                    {
                                        index = i;
                                    }
                                    if (entity is PlayerEntity player)
                                    {
                                        if (player.DoInteract() && objects is InteractableNPC interactable)
                                        {
                                            interactable.Interacted(player);
                                        }
                                    }
                                    Debugs.Msg($"Left Collision");
                                }
                                break;
                            }
                        case Facing.Right:
                            {
                                int entitySpeedX = (int)(entity.InteractionBox.X + entity.Speed);
                                int entitySpeedY = entity.InteractionBox.Y;
                                Rectangle interactionBox = new Rectangle(entitySpeedX, entitySpeedY, BaseEntity.InteractionSize, BaseEntity.InteractionSize);
                                if (interactionBox.Intersects(objects.InteractionBox))
                                {
                                    if (objects.CanCollideEachOther == true)
                                    {
                                        entity.CanCollideEachOther = true;
                                    }
                                    if (isPlayer == true)
                                    {
                                        index = i;
                                    }
                                    if (entity is PlayerEntity player)
                                    {
                                        if (player.DoInteract() && objects is InteractableNPC interactable)
                                        {
                                            interactable.Interacted(player);
                                        }
                                    }
                                    Debugs.Msg($"Right Collision");
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
