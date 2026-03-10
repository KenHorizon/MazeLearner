using MazeLearner.Audio;
using MazeLearner.GameContent.Entity;
using MazeLearner.GameContent.Entity.Items;
using MazeLearner.GameContent.Entity.NPCs;
using MazeLearner.GameContent.Entity.Objects;
using MazeLearner.Graphics.Particle;
using MazeLearner.Localization;
using MazeLearner.Worlds;

namespace MazeLearner.GameContent
{
    public static class RegisterContent
    {
        public static void NPCs()
        {
            NPC.Register(new NpcEntity());
            NPC.Register(new NpcEntity());
            NPC.Register(new NpcEntity());
            NPC.Register(new NpcEntity());
            NPC.Register(new NpcEntity());
            NPC.Register(new NpcEntity());
            NPC.Register(new NpcEntity());
            NPC.Register(new NpcEntity());
            NPC.Register(new NpcEntity());
            NPC.Register(new NpcEntity());
            NPC.Register(new NpcEntity());
            NPC.Register(new NpcEntity());

            NPC.Register(new NpcEntity());// Cat 0
            NPC.Register(new NpcEntity());// Cat 1
            NPC.Register(new NpcEntity());// Dog 0
            NPC.Register(new NpcEntity());// Dog 2
            NPC.Register(new NpcEntity());// Cow 0
            NPC.Register(new NpcEntity());// Carabao 0

            NPC.Register(new NpcEntity());// Boy 0
            NPC.Register(new NpcEntity());// Boy 1
            NPC.Register(new NpcEntity());// Girl 0
            NPC.Register(new NpcEntity());// Girl 1
            NPC.Register(new NpcEntity());// Girl 2
            NPC.Register(new NpcEntity());// Girl 3
            NPC.Register(new NpcEntity());// Teacher
            Loggers.Debug("Registering Npc Completed!");
        }
        public static void Objects()
        {
            ObjectEntity.Register(new ObjectEntity());
            ObjectEntity.Register(new ObjectWarp());
            Loggers.Debug("Registering Object Completed!");
        }
        public static void Maps()
        {
            World.Add(new World("interior", AudioAssets.LobbyBGM.Value, WorldType.Indoor));
            World.Add(new World("intro0", AudioAssets.LobbyBGM.Value, WorldType.Outside));
            World.Add(new World("school", AudioAssets.LobbyBGM.Value, WorldType.Outside));
            World.Add(new World("interior_1", AudioAssets.LobbyBGM.Value, WorldType.Indoor));
            World.Add(new World("library", AudioAssets.LobbyBGM.Value, WorldType.Indoor));
            World.Add(new World("hallways", AudioAssets.LobbyBGM.Value, WorldType.Indoor));
            World.Add(new World("shadow_corridors", AudioAssets.LobbyBGM.Value, WorldType.Indoor));
            World.Add(new World("guardian_play_house", AudioAssets.LobbyBGM.Value, WorldType.Indoor));
            Loggers.Debug("Registering Maps Completed!");
        }
        public static void Particles()
        {
            Particle.Register(new Particle("grass"));
            Particle.Register(new Particle("ripple"));
            Particle.Register(new Particle("mud_ripple"));
            Particle.Register(new Particle("dust"));
            Particle.Register(new Particle("emo0"));
            Particle.Register(new Particle("emo1"));
            Particle.Register(new Particle("emo2"));
            Particle.Register(new Particle("emo3"));
            Particle.Register(new Particle("emo4"));
            Particle.Register(new Particle("emo5"));
            Particle.Register(new Particle("emo6"));
            Particle.Register(new Particle("emo7"));
            Particle.Register(new Particle("emo8"));
            Particle.Register(new Particle("emo9"));
            Particle.Register(new Particle("emo10"));
            Particle.Register(new Particle("emo11"));
            Loggers.Debug("Registering Particles Completed!");
        }
        public static void Items()
        {
            Item.Add(new Item(Resources.AirItem));
            Item.Add(new Item(Resources.HealthPotion));
            Item.Add(new Item(Resources.BasicSword));
        }
    }
}
