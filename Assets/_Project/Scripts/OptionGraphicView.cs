using System;
using UnityEngine;
using UnityEngine.UI;

namespace MagneticMayhem
{
    public class OptionGraphicView : MonoBehaviour 
    {
        [SerializeField] private AudioManager audioManager;
        [SerializeField] private Slider slider;
        [SerializeField] private Image musicIconState;
        [SerializeField] private Sprite[] musicSpriteStates;

        private void Start ()
        {
            audioManager.OnAudioChange += OnValueChange;
        }

        private void OnValueChange (AudioSettings settings)
        {
            slider.SetValueWithoutNotify(settings.generalVolume);
            musicIconState.sprite = settings.isMuted ? musicSpriteStates [0] : musicSpriteStates [1];
        }

        private void OnDestroy ()
        {
            if (audioManager == null)
                return;
            audioManager.OnAudioChange -= OnValueChange;
        }
    }
}
