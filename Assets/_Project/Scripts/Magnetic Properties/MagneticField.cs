using System;
using UnityEngine;

namespace MagneticMayhem
{
    public class MagneticField : MonoBehaviour
    {
        [SerializeField] private CircleCollider2D magneticFieldArea;
        private IMagneticApply magneticManager;


        private void Awake ()
        {
            magneticManager = GetComponentInParent<IMagneticApply>();
            magneticManager.SuscribeListener( OnStatusChanged);
        }

        private void OnDestroy ()
        {
            magneticManager.RemoveListener(OnStatusChanged);
        }

        private void OnStatusChanged (MagnetStatus status)
        {
            magneticFieldArea.radius = status.rangeOfMegneticField;
        }

        private void OnTriggerEnter2D (Collider2D collision)
        {
            if (!collision.gameObject.TryGetComponent<IMagneticRecieve>(out var magent))
                return;
            magneticManager.AddMagnet(magent);
        }

        private void OnTriggerExit2D (Collider2D collision)
        {
            if (!collision.gameObject.TryGetComponent<IMagneticRecieve>(out var magent))
                return;
            magneticManager.RemoveMagnet(magent);
        }
    }
}
