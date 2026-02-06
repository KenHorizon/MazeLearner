using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.Audio
{
    public class SoundEngine : IDisposable
    {
        private readonly List<SoundEffectInstance> _activeSoundEffectInstances;
        private float _prevVolume;
        private float _prevSoundEffectVolume;
        public bool IsMuted { get; set; }
        
        public float Volume
        {
            get
            {
                if (this.IsMuted)
                {
                    return 0.0F;
                }
                return MediaPlayer.Volume;
            }
            set
            {
                if (this.IsMuted)
                {
                    return;
                }

                MediaPlayer.Volume = Math.Clamp(value, 0.0f, 1.0f);
            }
        }
        public float SoundEffectVolume
        {
            get
            {
                if (this.IsMuted)
                {
                    return 0.0F;
                }

                return SoundEffect.MasterVolume;
            }
            set
            {
                if (this.IsMuted)
                {
                    return;
                }

                SoundEffect.MasterVolume = Math.Clamp(value, 0.0f, 1.0f);
            }
        }
        public bool IsDisposed { get; private set; }
        public SoundEngine()
        {
            this._activeSoundEffectInstances = new List<SoundEffectInstance>(); 
        }
        ~SoundEngine() => Dispose(false);
        public void Update()
        {
            for (int i = _activeSoundEffectInstances.Count - 1; i >= 0; i--)
            {
                SoundEffectInstance instance = _activeSoundEffectInstances[i];

                if (instance.State == SoundState.Stopped)
                {
                    if (!instance.IsDisposed)
                    {
                        instance.Dispose();
                    }
                    _activeSoundEffectInstances.RemoveAt(i);
                }
            }
        }
        public SoundEffectInstance Play(SoundEffect soundEffect)
        {
            return Play(soundEffect, 1.0f, 0.0f, 0.0f, false);
        }
        public SoundEffectInstance Play(SoundEffect soundEffect, float volume, float pitch, float pan, bool isLooped)
        {
            SoundEffectInstance soundEffectInstance = soundEffect.CreateInstance();
            if (soundEffectInstance == null) return null;
            soundEffectInstance.Volume = volume;
            soundEffectInstance.Pitch = pitch;
            soundEffectInstance.Pan = pan;
            soundEffectInstance.IsLooped = isLooped;
            soundEffectInstance.Play();
            _activeSoundEffectInstances.Add(soundEffectInstance);
            return soundEffectInstance;
        }
        public void Play(Song song, bool isRepeating = true)
        {
            if (MediaPlayer.State == MediaState.Playing)
            {
                MediaPlayer.Stop();
            }
            MediaPlayer.Play(song);
            MediaPlayer.IsRepeating = isRepeating;
        }
        public void PauseAudio()
        {
            MediaPlayer.Pause();
            foreach (SoundEffectInstance soundEffectInstance in _activeSoundEffectInstances)
            {
                if (soundEffectInstance == null) continue;
                soundEffectInstance.Pause();
            }
        }
        public void ResumeAudio()
        {
            MediaPlayer.Resume();
            foreach (SoundEffectInstance soundEffectInstance in _activeSoundEffectInstances)
            {
                if (soundEffectInstance == null) continue;
                soundEffectInstance.Resume();
            }
        }
        public void MuteAudio()
        {
            _prevVolume = MediaPlayer.Volume;
            _prevSoundEffectVolume = SoundEffect.MasterVolume;

            // Set all volumes to 0
            MediaPlayer.Volume = 0.0f;
            SoundEffect.MasterVolume = 0.0f;

            IsMuted = true;
        }
        public void UnmuteAudio()
        {
            MediaPlayer.Volume = _prevVolume;
            SoundEffect.MasterVolume = _prevSoundEffectVolume;

            IsMuted = false;
        }
        public void ToggleMute()
        {
            if (IsMuted)
            {
                UnmuteAudio();
            }
            else
            {
                MuteAudio();
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (IsDisposed)
            {
                return;
            }

            if (disposing)
            {
                foreach (SoundEffectInstance soundEffectInstance in _activeSoundEffectInstances)
                {
                    soundEffectInstance.Dispose();
                }
                _activeSoundEffectInstances.Clear();
            }

            IsDisposed = true;
        }
    }
}
