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
        public static Assets<SoundEffect> ClickedSFX = Assets<SoundEffect>.Request("Audio/SE/SE_0");
        public static Assets<SoundEffect> HitSFX = Assets<SoundEffect>.Request("Audio/SE/SE_1");
        public static Assets<SoundEffect> WarpedSFX = Assets<SoundEffect>.Request("Audio/SE/SE_2");
        public static Assets<SoundEffect> FallSFX = Assets<SoundEffect>.Request("Audio/SE/SE_3");
        public static Assets<Song> MainMenuBGM = Assets<Song>.Request("Audio/BGM/BGM_0");
        public static Assets<Song> LobbyBGM = Assets<Song>.Request("Audio/BGM/BGM_1");

    }
}
