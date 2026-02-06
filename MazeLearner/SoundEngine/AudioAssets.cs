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
        public static Assets<Song> MainMenuBGM = Assets<Song>.Request("Audio/BGM/BGM_0");
        public static Assets<Song> LobbyBGM = Assets<Song>.Request("Audio/BGM/BGM_1");

    }
}
