using MazeLearner.Audio;
using MazeLearner.GameContent.BattleSystems.Questions;
using MazeLearner.GameContent.Entity.Monster;
using MazeLearner.GameContent.Entity.Objects;
using MazeLearner.GameContent.Entity.Player;
using MazeLearner.GameContent.Phys;
using MazeLearner.Graphics.Animation;
using MazeLearner.Screen;
using MazeLearner.Text;
using MazeLearner.Worlds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;

namespace MazeLearner.GameContent.Entity
{
    public static class RegisterContent
    {
        public static void NPCs()
        {
            NPC.Register(new Gloos());
            NPC.Register(new Knight(0));
            NPC.Register(new Knight(1));
            NPC.Register(new Knight(2));
            NPC.Register(new Knight(3));
            Loggers.Debug("Registering Npc Completed!");
        }
        public static void Objects()
        {
            ObjectEntity.Register(new ObjectSign());
            Loggers.Debug("Registering Object Completed!");
        }
        public static void Maps()
        {
            World.Add(new World("lobby", AudioAssets.LobbyBGM.Value, WorldType.Outside));
            World.Add(new World("cave_entrance", AudioAssets.LobbyBGM.Value, WorldType.Cave));
            Loggers.Debug("Registering Maps Completed!");
        }
    }
}
