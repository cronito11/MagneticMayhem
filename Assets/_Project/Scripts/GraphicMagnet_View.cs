using UnityEngine;

namespace MagneticMayhem
{
    public class GraphicMagnet_View : MonoBehaviour
    {
        [SerializeField] SpriteRenderer spriteRender;

        [Header("MagenetState")]
        [SerializeField] private Sprite [] states;

        private IMageneticPoleChangeable mageneticManager;

        private void Awake ()
        {
            mageneticManager = GetComponentInParent<IMageneticPoleChangeable>();
        }

        private void Start ()
        {
            mageneticManager.SuscribeListener(OnStatusChanged);
        }

        private void OnDestroy ()
        {
            mageneticManager.RemoveListener(OnStatusChanged);
        }

        private void OnStatusChanged (MagnetStatus status)
        {
            spriteRender.sprite = states [(int)status.pole];
        }
    }
}
