using System;
using UnityEngine;
using UnityEngine.UI;

namespace MagneticMayhem
{
    public class OptionPanel : PanelManager
    {
        [SerializeField] private AudioManager audioManager;
        [SerializeField] private Slider volumeControl;

        private void Start ()
        {
            volumeControl.onValueChanged.AddListener(ChangeVolume);
        }

        private void OnDestroy ()
        {
            volumeControl.onValueChanged.RemoveListener(ChangeVolume);
        }
        public void SwitchMute ()
        {
            audioManager.Mute(!audioManager.settings.isMuted);
        }

        public void ChangeVolume (float value)
        {
            audioManager.SetVolume(value);
        }
    }
}
