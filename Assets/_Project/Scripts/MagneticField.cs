using System;
using UnityEngine;

namespace MagneticMayhem
{
    public class MagneticField : MonoBehaviour
    {
        [SerializeField] private CircleCollider2D poleFieldArea;
        private IMagneticApply mageneticManager;


        private void Awake ()
        {
            mageneticManager = GetComponentInParent<IMagneticApply>();
            mageneticManager.SuscribeListener( OnStatusChanged);
        }

        private void OnDestroy ()
        {
            mageneticManager.RemoveListener(OnStatusChanged);
        }

        private void OnStatusChanged (MagnetStatus status)
        {
            poleFieldArea.radius = status.actionRadio;
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
