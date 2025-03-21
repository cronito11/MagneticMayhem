using System;
using System.Collections.Generic;
using UnityEngine;

public enum MagnetSurfaceType
{
    NormalSurface = 0, BackSurface= 1
}

public enum SurfaceAlignment
{
    Vertical = 0, Horizontal = 1
}

namespace MagneticMayhem
{
    public class SurfaceMagnetism : MonoBehaviour,IMagneticApply
    {
        private Action<MagnetStatus> OnStatusChanged;

        //Magntic constant is a constant value that is used to calculate the force between two magnetic poles but it is not used in this script
        private const float MAGNETIC_CONSTANT = 1.0e-7f;
        
        //a reference of transform where distnce will be calculated because if the transform is too close with player it would give 0 distance and force will be infinite
        [SerializeField] protected Transform distanceAnchor; 
        
        [SerializeField] protected MagnetStatus currentStatus;

        [SerializeField] protected MagnetSurfaceType magnetType;

        [field: SerializeField] public SurfaceAlignment alignment { get; protected set; }

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

        protected virtual void CalculateMagenticForce(Transform target, IMagneticRecieve magnet)
        {
            if (magnet.pole.Equals(MagenticPole.None))
                return;

            float distance = Mathf.Abs(Mathf.Floor(distanceAnchor.position.y) - Mathf.Floor(target.position.y));

            float magneticForce = currentStatus.poleIntensity / (distance * distance);

            Vector2 direction;

            if(alignment == SurfaceAlignment.Horizontal)

                direction = Vector2.right * Mathf.Sign(distanceAnchor.position.x - target.position.x);
            else
                direction = Vector2.up * Mathf.Sign(distanceAnchor.position.y - target.position.y);

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
