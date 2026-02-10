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
        public static Assets<SoundEffect> ClickedSFX;
        public static Assets<SoundEffect> HitSFX;
        public static Assets<SoundEffect> WarpedSFX;
        public static Assets<SoundEffect> FallSFX;
        public static Assets<Song> MainMenuBGM;
        public static Assets<Song> LobbyBGM;

        public static void LoadAll()
        {
            ClickedSFX = Assets<SoundEffect>.Request("Audio/SE/SE_0");
            HitSFX = Assets<SoundEffect>.Request("Audio/SE/SE_1");
            WarpedSFX = Assets<SoundEffect>.Request("Audio/SE/SE_2");
            FallSFX = Assets<SoundEffect>.Request("Audio/SE/SE_3");
            MainMenuBGM = Assets<Song>.Request("Audio/BGM/BGM_0");
            LobbyBGM = Assets<Song>.Request("Audio/BGM/BGM_1");
        }
    }
}
