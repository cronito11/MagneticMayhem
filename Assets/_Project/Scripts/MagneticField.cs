using UnityEngine;

namespace MagneticMayhem
{
    public class MagneticField : MonoBehaviour
    {
        private IMagneticApply mageneticManager;

        private void Awake ()
        {
            mageneticManager = GetComponentInParent<IMagneticApply>();
        }

        private void OnTriggerEnter2D (Collider2D collision)
        {
            if (!collision.gameObject.TryGetComponent<IMagneticRecieve>(out var magent))
                return;
            mageneticManager.AddMagnet(magent);
        }

        private void OnTriggerExit2D (Collider2D collision)
        {
            if (!collision.gameObject.TryGetComponent<IMagneticRecieve>(out var magent))
                return;
            mageneticManager.RemoveMagnet(magent);
        }
    }
}
