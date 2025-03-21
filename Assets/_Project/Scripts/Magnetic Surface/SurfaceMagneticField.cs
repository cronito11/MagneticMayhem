using Unity.VisualScripting;
using UnityEngine;

namespace MagneticMayhem
{
    public class SurfaceMagneticField : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D magneticFieldArea;
        [SerializeField] private float defaultLength = 30f;
        private IMagneticApply magneticManager;
        private SurfaceMagnetism surfaceMagnetism;

        private void Awake()
        {
            magneticManager = GetComponentInParent<IMagneticApply>();
            surfaceMagnetism = GetComponentInParent<SurfaceMagnetism>();    
            magneticManager.SuscribeListener(OnStatusChanged);
        }

        private void OnDestroy()
        {
            magneticManager.RemoveListener(OnStatusChanged);
        }

        private void OnStatusChanged(MagnetStatus status)
        {
            if(surfaceMagnetism.alignment == SurfaceAlignment.Vertical)
                magneticFieldArea.size = new Vector2(defaultLength, status.rangeOfMegneticField);
            else
                magneticFieldArea.size = new Vector2(status.rangeOfMegneticField, defaultLength);

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
