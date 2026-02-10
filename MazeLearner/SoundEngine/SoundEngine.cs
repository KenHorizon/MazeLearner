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
        
        public float BackgroundVolume
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
        public bool AudioAvailable { get; private set; } = true;
        public bool IsDisposed { get; private set; }
        public SoundEngine()
        {
            this._activeSoundEffectInstances = new List<SoundEffectInstance>(); 
        }
        ~SoundEngine() => Dispose(false);
        public void Update()
        {
            if (AudioAvailable == false) return;
            Main.SoundEngine.BackgroundVolume = 0.05F * ((float)GameSettings.BackgroundMusic / 100);
            Main.SoundEngine.SoundEffectVolume = 0.05F * ((float)GameSettings.SFXMusic / 100);
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
            return Play(soundEffect, 1.0F, 0.0F, 0.0F, false);
        }
        public SoundEffectInstance Play(SoundEffect soundEffect, float volume, float pitch, float pan, bool isLooped)
        {
            if (AudioAvailable == false || this.IsMuted || soundEffect == null) return null;
            try
            {
                SoundEffectInstance soundEffectInstance = soundEffect.CreateInstance();
                soundEffectInstance.Volume = volume;
                soundEffectInstance.Pitch = pitch;
                soundEffectInstance.Pan = pan;
                soundEffectInstance.IsLooped = isLooped;
                soundEffectInstance.Play();
                _activeSoundEffectInstances.Add(soundEffectInstance);
                return soundEffectInstance;
            }
            catch (Exception ex)
            {
                this.DisableAudio(ex);
                return null;
            }
        }
        private void DisableAudio(Exception ex)
        {
            AudioAvailable = false;
            IsMuted = true;
            Loggers.Msg($"[Audio Disabled] {ex}");
            try
            {
                MediaPlayer.Stop();
            }
            catch { }

            foreach (var sfx in _activeSoundEffectInstances)
            {
                try { sfx.Dispose(); } catch { }
            }
            _activeSoundEffectInstances.Clear();
        }
        public void Play(Song song, bool isRepeating = true)
        {
            if (AudioAvailable == false || this.IsMuted || song == null) return;
            try
            {
                if (MediaPlayer.State == MediaState.Playing)
                {
                    MediaPlayer.Stop();
                }
                MediaPlayer.Play(song);
                MediaPlayer.IsRepeating = isRepeating;
            }
            catch (Exception ex)
            {
                this.DisableAudio(ex);
            }
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
                soundEffectInstance.Resume();
            }
        }
        public void MuteAudio()
        {
            _prevVolume = MediaPlayer.Volume;
            _prevSoundEffectVolume = SoundEffect.MasterVolume;
            MediaPlayer.Volume = 0.0F;
            SoundEffect.MasterVolume = 0.0F;
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
