using System;
using UnityEngine;
using UnityEngine.UI;

namespace MagneticMayhem
{
    public class OptionPanel : PanelManager
    {
        [SerializeField] private Slider backgroundvolumeControl;
        [SerializeField] private Slider sfxVolumeControl;

        private AudioManager audioManager;

        protected override void Awake ()
        {
            base.Awake();
        }
        private void Start ()
        {
            audioManager = AudioManager.Instance;
            backgroundvolumeControl?.onValueChanged.AddListener(ChangeVolumeGeneral);
            sfxVolumeControl?.onValueChanged.AddListener(ChangeVolumeSfx);
        }

        private void OnDestroy ()
        {
            backgroundvolumeControl?.onValueChanged.RemoveListener(ChangeVolumeGeneral);
            sfxVolumeControl?.onValueChanged.RemoveListener(ChangeVolumeSfx);
        }
        public void SwitchMute ()
        {
            audioManager.Mute(!audioManager.settings.isMuted);
        }

        public void ChangeVolumeGeneral (float value)
        {
            audioManager.SetMusicVolume(value);
        }

        public void ChangeVolumeSfx (float value)
        {
            audioManager.SetMusicVolume(value);
        }

        public void SwitchMuteMusic ()
        {
            audioManager.MuteMusic(!audioManager.settings.isMusicMute);
        }

        public void SwitchMuteSFX ()
        {
            audioManager.MuteSfx(!audioManager.settings.isSFXMute);
        }
    }
}
