using Platformer397;
using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;

namespace MagneticMayhem
{
    public class AudioManager : Singleton<AudioManager>
    {
        #region constants
        private const string MASTER_VOLUME = "MasterVolume";
        private const string SFX_VOLUME = "SFXVolume";
        private const string MUSIC_VOLUME = "MusicVolume";
        #endregion
        public Action<AudioSettings> OnAudioChange;

        private AudioSettings _settings;

        public AudioSettings settings => _settings;


        [SerializeField] private AudioMixer audioMixer;


        public void Mute (bool isMuted)
        {
            _settings.isMuted = isMuted;
            float volume = _settings.isMuted ? -80f : _settings.generalVolume;
            audioMixer.SetFloat(MASTER_VOLUME, volume);
            OnAudioChange?.Invoke(settings);
        }
        public void MuteMusic (bool isMuted)
        {
            _settings.isMusicMute = isMuted;
            float volume = _settings.isMusicMute ? -80f : _settings.musicVolume;
            audioMixer.SetFloat(MUSIC_VOLUME, volume);
            OnAudioChange?.Invoke(settings);
        }

        public void MuteSfx (bool isMuted)
        {
            _settings.isSFXMute = isMuted;
            float volume = _settings.isSFXMute ? -80f : _settings.sfxVolume;
            audioMixer.SetFloat(SFX_VOLUME, volume);
            OnAudioChange?.Invoke(settings);
        }

        public void SetVolume (float volume)
        {
            _settings.generalVolume = volume;

            float dB = settings.isMuted ? -80f : Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1)) * 20;
            audioMixer.SetFloat(MASTER_VOLUME, dB);
            OnAudioChange?.Invoke(settings);
        }

        public void SetSFXVolume (float volume)
        {
            _settings.sfxVolume = volume;
            float dB = settings.isSFXMute ? -80f : Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1)) * 20;
            audioMixer.SetFloat(SFX_VOLUME, dB);
            OnAudioChange?.Invoke(settings);
        }

        public void SetMusicVolume (float volume)
        {
            _settings.musicVolume = volume;
            float dB = settings.isMusicMute ? -80f : Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1)) * 20;
            audioMixer.SetFloat(MUSIC_VOLUME, dB);
            OnAudioChange?.Invoke(settings);
        }
    }

    public struct AudioSettings
    {
        public bool isMuted;
        public bool isMusicMute;
        public bool isSFXMute;
        public float generalVolume;
        public float sfxVolume;
        public float musicVolume;
    }
}
