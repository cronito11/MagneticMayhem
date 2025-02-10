using System;
using UnityEngine;

namespace MagneticMayhem
{
    public class GraphicMagneticField : MonoBehaviour
    {
        [SerializeField] private Transform visualArea;
        private IMagneticApply mageneticManager;

        private void Awake ()
        {
            mageneticManager = GetComponentInParent<IMagneticApply>();
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
            visualArea.localScale = Vector3.one * status.actionRadio;
        }
    }
}
