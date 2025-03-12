using System;
using UnityEngine;
using UnityEngine.UI;

namespace MagneticMayhem
{
    public class OptionPanel : PanelManager
    {
        [SerializeField] private Slider volumeControl;

        private AudioManager audioManager;

        protected override void Awake ()
        {
            base.Awake();
            audioManager = AudioManager.Instance;
        }
        private void Start ()
        {
            volumeControl?.onValueChanged.AddListener(ChangeVolume);
        }

        private void OnDestroy ()
        {
            volumeControl?.onValueChanged.RemoveListener(ChangeVolume);
        }
        public void SwitchMute ()
        {
            audioManager.Mute(!audioManager.settings.isMuted);
        }

        public void ChangeVolume (float value)
        {
            audioManager.SetVolume(value);
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
