using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;

namespace MagneticMayhem
{
    public class AudioManager : MonoBehaviour
    {
        #region constants
        private const string MASTER_VOLUME = "MasterVolume";
        #endregion
        public Action<AudioSettings> OnAudioChange;

        private AudioSettings _settings;

        public AudioSettings settings => settings;


        [SerializeField] private AudioMixer audioMixer;


        public void Mute (bool isMuted)
        {
            _settings.isMuted = isMuted;
            float volume = _settings.isMuted ? -80f : _settings.generalVolume;
            audioMixer.SetFloat(MASTER_VOLUME, volume);
            OnAudioChange?.Invoke(settings);
        }

        public void SetVolume (float volume)
        {
            _settings.generalVolume = volume;

            float dB = settings.isMuted ? -80f : Mathf.Log10(Mathf.Clamp(volume, 0.0001f, 1)) * 20;
            audioMixer.SetFloat(MASTER_VOLUME, dB);
            OnAudioChange?.Invoke(settings);
        }
    }

    public struct AudioSettings
    {
        public bool isMuted;
        public float generalVolume;
        public float sfxVolume;
    }
}
