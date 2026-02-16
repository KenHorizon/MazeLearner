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
        public static Asset<SoundEffect> ClickedSFX;
        public static Asset<SoundEffect> HitSFX;
        public static Asset<SoundEffect> WarpedSFX;
        public static Asset<SoundEffect> FallSFX;
        public static Asset<Song> MainMenuBGM;
        public static Asset<Song> LobbyBGM;
        public static Asset<Song> IntroBGM;

        public static void LoadAll()
        {
            ClickedSFX = Asset<SoundEffect>.Request("Audio/SE/SE_0");
            HitSFX = Asset<SoundEffect>.Request("Audio/SE/SE_1");
            WarpedSFX = Asset<SoundEffect>.Request("Audio/SE/SE_2");
            FallSFX = Asset<SoundEffect>.Request("Audio/SE/SE_3");
            MainMenuBGM = Asset<Song>.Request("Audio/BGM/BGM_0");
            LobbyBGM = Asset<Song>.Request("Audio/BGM/BGM_1");
            IntroBGM = Asset<Song>.Request("Audio/BGM/BGM_2");
        }
    }
}
