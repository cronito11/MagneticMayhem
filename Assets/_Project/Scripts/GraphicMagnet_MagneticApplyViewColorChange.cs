using UnityEngine;

namespace MagneticMayhem
{
    public class GraphicMagnet_MagneticApplyViewColorChange : MonoBehaviour
    {
        [SerializeField] SpriteRenderer spriteRender;

        [Header("MagenetState")]
        [SerializeField] private Color [] states;

        private IMagneticApply mageneticManager;

        private void Awake ()
        {
            mageneticManager = GetComponentInParent<IMagneticApply>();
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
            spriteRender.color = states [(int)status.pole];
        }
    }
}
