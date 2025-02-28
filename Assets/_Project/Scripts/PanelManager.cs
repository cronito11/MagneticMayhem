using DG.Tweening;
using UnityEngine;

namespace MagneticMayhem
{
    public abstract class PanelManager : MonoBehaviour
    {
        private const float TRANSITION_TIME = 0.2f;
        private CanvasGroup canvasGroup;

        virtual protected void Awake ()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void Open ()
        {
            canvasGroup.blocksRaycasts = true;
            canvasGroup.DOFade(1, TRANSITION_TIME).OnComplete(() =>
            {
                canvasGroup.interactable = true;
            });
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
