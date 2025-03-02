using System;
using System.Collections.Generic;
using UnityEngine;

namespace MagneticMayhem
{
    public class SurfaceMagnetism : MonoBehaviour,IMagneticApply
    {
        private Action<MagnetStatus> OnStatusChanged;

        //Check formula
        private const float MAGNETIC_CONSTANT = 1.0e-7f;
        //Add comment
        [SerializeField] private MagnetStatus currentStatus;
        [SerializeField] private Transform areaOfEffect;

        private Dictionary<IMagneticRecieve, Transform> magnetsArround = new Dictionary<IMagneticRecieve, Transform>();
       

        public MagenticPole pole => currentStatus.pole;

        private void Awake()
        {
            
        }

        public void ApplyMagnetism()
        {
            foreach (var magnet in magnetsArround)
            {
                CalculateMagenticForce(magnet.Value, magnet.Key);
            }
        }

        private void CalculateMagenticForce(Transform target, IMagneticRecieve magnet)
        {
            if (magnet.pole.Equals(MagenticPole.None))
                return;

            float distance = Mathf.Abs(Mathf.Floor(areaOfEffect.position.y) - Mathf.Floor(target.position.y));

            float magneticForce = currentStatus.poleIntensity / (distance * distance);
            Vector2 direction = Vector2.up * Mathf.Sign(areaOfEffect.position.y - target.position.y);
            magnet.ReceivMagnetism(direction, magneticForce, currentStatus.pole);
        }
       
        public void AddMagnet(IMagneticRecieve magnet)
        {
            if (pole.Equals(MagenticPole.None) || magnetsArround.ContainsKey(magnet))
                return;
            magnetsArround.Add(magnet, (magnet as MonoBehaviour).transform);
        }

        public void RemoveMagnet(IMagneticRecieve magnet)
        {
            if (pole.Equals(MagenticPole.None) || !magnetsArround.ContainsKey(magnet))
                return;
            magnetsArround.Remove(magnet);
        }

        private void FixedUpdate()
        {
            if (magnetsArround.Count == 0)
                return;
            ApplyMagnetism();
        }

        public void SuscribeListener(Action<MagnetStatus> method)
        {
            OnStatusChanged += method;
            method.Invoke(currentStatus);
        }

        public void RemoveListener(Action<MagnetStatus> method)
        {
            OnStatusChanged -= method;
        }
    }
}
