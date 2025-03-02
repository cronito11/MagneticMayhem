using Unity.VisualScripting;
using UnityEngine;

namespace MagneticMayhem
{
    public class SurfaceMagneticField : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D magneticFieldArea;
        private IMagneticApply magneticManager;


        private void Awake()
        {
            magneticManager = GetComponentInParent<IMagneticApply>();
            magneticManager.SuscribeListener(OnStatusChanged);
        }

        private void OnDestroy()
        {
            magneticManager.RemoveListener(OnStatusChanged);
        }

        private void OnStatusChanged(MagnetStatus status)
        {
            magneticFieldArea.size = new Vector2(30f, status.rangeOfMegneticField);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.gameObject.TryGetComponent<IMagneticRecieve>(out var magent))
                return;
            Debug.Log(magent.pole+ " Registered");  
            magneticManager.AddMagnet(magent);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!collision.gameObject.TryGetComponent<IMagneticRecieve>(out var magent))
                return;
            Debug.Log(magent.pole + " Removed");
            magneticManager.RemoveMagnet(magent);
        }
    }
}
