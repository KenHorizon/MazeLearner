using MazeLearner.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.Audio
{
    public class AudioAssets
    {
        public static Asset<SoundEffect> PopSFX;
        public static Asset<SoundEffect> ClickedSFX;
        public static Asset<SoundEffect> HitSFX;
        public static Asset<SoundEffect> WarpedSFX;
        public static Asset<SoundEffect> FallSFX;
        public static Asset<Song> BattleBGM;
        public static Asset<Song> MainMenuBGM;
        public static Asset<Song> LobbyBGM;
        public static Asset<Song> IntroBGM;
        public static Asset<Song> Intro0;
        public static Asset<Song> Intro1;
        public static Asset<Song> Intro2;
        public static Asset<Song> Intro3;

        public static void LoadAll()
        {
            PopSFX = Asset<SoundEffect>.Request("Audio/SE/SE_4");
            ClickedSFX = Asset<SoundEffect>.Request("Audio/SE/SE_0");
            HitSFX = Asset<SoundEffect>.Request("Audio/SE/SE_1");
            WarpedSFX = Asset<SoundEffect>.Request("Audio/SE/SE_2");
            FallSFX = Asset<SoundEffect>.Request("Audio/SE/SE_3");
            MainMenuBGM = Asset<Song>.Request("Audio/BGM/BGM_0");
            LobbyBGM = Asset<Song>.Request("Audio/BGM/BGM_1");
            IntroBGM = Asset<Song>.Request("Audio/BGM/BGM_2");
            BattleBGM = Asset<Song>.Request("Audio/BGM/BGM_3");
            Intro0 = Asset<Song>.Request("Audio/Intro_0");
            Intro1 = Asset<Song>.Request("Audio/Intro_1");
            Intro2 = Asset<Song>.Request("Audio/Intro_2");
            Intro3 = Asset<Song>.Request("Audio/Intro_3");
        }
    }
}
