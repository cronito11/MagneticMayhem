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

        private void OnEnable ()
        {
            mageneticManager.SuscribeListener(OnStatusChanged);
        }

        private void OnDisable ()
        {
            mageneticManager.RemoveListener(OnStatusChanged);
        }

        private void OnStatusChanged (MagnetStatus status)
        {
            spriteRender.sprite = states[(int)status.pole];
            Debug.Log((int)status.pole);
        }
    }
}
