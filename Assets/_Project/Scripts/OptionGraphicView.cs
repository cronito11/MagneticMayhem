using System;
using UnityEngine;
using UnityEngine.UI;

namespace MagneticMayhem
{
    public class OptionGraphicView : MonoBehaviour 
    {
        [SerializeField] private Slider slider;
        [Space(10)]
        [Header ("Music Data")]
        [SerializeField] private Image musicIconState;
        [SerializeField] private Sprite[] musicSpriteStates;

        [Header ("Music Data")]
        [SerializeField] private Image soundIconState;
        [SerializeField] private Sprite[] soundSpriteStates;

        private AudioManager audioManager;

        private void Start ()
        {
            audioManager = AudioManager.Instance;
            audioManager.OnAudioChange += OnValueChange;
        }

        private void OnValueChange (AudioSettings settings)
        {
            slider?.SetValueWithoutNotify(settings.generalVolume);
            musicIconState.sprite = settings.isMuted ? musicSpriteStates [0] : musicSpriteStates [1];
            soundIconState.sprite = settings.isSoundMute ? soundSpriteStates [0] : soundSpriteStates [1];
        }

        private void OnDestroy ()
        {
            if (audioManager == null)
                return;
            audioManager.OnAudioChange -= OnValueChange;
        }
    }
}
