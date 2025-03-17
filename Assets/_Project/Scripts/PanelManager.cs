using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;

namespace MagneticMayhem
{
    [RequireComponent(typeof(AudioSource))]
    public abstract class PanelManager : MonoBehaviour
    {
        private const float TRANSITION_TIME = 0.2f;
        private CanvasGroup canvasGroup;
        private AudioSource audioSource;

        virtual protected void Awake ()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.playOnAwake = false; ;
            canvasGroup = GetComponent<CanvasGroup>();
            audioSource = GetComponent<AudioSource>();
        }

        public void Open ()
        {
            canvasGroup.blocksRaycasts = true;
            canvasGroup.DOFade(1, TRANSITION_TIME).OnComplete(() =>
            {
                canvasGroup.interactable = true;
            });
            audioSource.Play();
        }

        public void Close ()
        {
            canvasGroup.DOFade(0, TRANSITION_TIME).OnComplete(() =>
            {
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
            });
        }
    }
}
