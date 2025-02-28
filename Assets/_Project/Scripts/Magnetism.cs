using System;
using System.Collections.Generic;
using UnityEngine;

namespace MagneticMayhem
{

    public class Magnetism : MonoBehaviour, IMagneticRecieve, IMagneticApply, IMageneticPoleChangeable
    {
        private Action<MagnetStatus> OnStatusChanged;
        
        //Check formula
        private const float MAGNETIC_CONSTANT = 1.0e-7f;
        //Add comment
        [SerializeField] private MagnetStatus currentStatus;


        private Dictionary<IMagneticRecieve, Transform> magnetsArround = new Dictionary<IMagneticRecieve, Transform>();
        private Rigidbody2D rb;

        [field: SerializeField] public float actionRadio { get; private set; } = 5;

        public MagenticPole pole => currentStatus.pole;

        #region TestCases
#if UNITY_EDITOR
        private void OnValidate ()
        {
            UpdateMagnetStatus(currentStatus);
        }
#endif
    #endregion
        private void Awake ()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public void ApplyMagnetism ()
        {
            foreach (var magnet in magnetsArround)
            {
                CalculateMagenticForce(magnet.Value, magnet.Key);
            }
        }

        private void UpdateMagnetStatus (MagnetStatus newStatus)
        { 
            currentStatus = newStatus;
            OnStatusChanged?.Invoke(currentStatus);
        }

        private void CalculateMagenticForce (Transform target,  IMagneticRecieve magnet)
        {
            if (magnet.pole.Equals(MagenticPole.None))
                return;

            Vector2 distance = new Vector2(Mathf.Floor( transform.position.x),Mathf.Floor( transform.position.y)) - new Vector2(Mathf.Floor( target.position.x),Mathf.Floor( target.position.y));
            distance.x = (float) Math.Round(distance.x,2);
            distance.y = (float) Math.Round(distance.y,2);

            float magneticForce = currentStatus.poleIntensity / (distance.sqrMagnitude);
            magnet.ReceivMagnetism(distance.normalized, magneticForce, currentStatus.pole );
        }

        public void ReceivMagnetism (Vector2 direction, float magnitude, MagenticPole pole)
        {
            magnitude *= pole.Equals(currentStatus.pole) ? -1 : 1;
            rb.AddForce(direction * magnitude);
        }

        public void AddMagnet (IMagneticRecieve magnet)
        {
            if (pole.Equals(MagenticPole.None) || magnetsArround.ContainsKey(magnet))
                return;
            magnetsArround.Add(magnet, (magnet as MonoBehaviour).transform);
        }

        public void RemoveMagnet (IMagneticRecieve magnet)
        {
            if (pole.Equals(MagenticPole.None) ||! magnetsArround.ContainsKey(magnet))
                return;
            magnetsArround.Remove(magnet);
        }

        private void FixedUpdate ()
        {
            if (magnetsArround.Count == 0)
                return;
            ApplyMagnetism();
        }

        public void SuscribeListener (Action<MagnetStatus> method)
        {
            OnStatusChanged += method;
            method.Invoke(currentStatus);
        }

        public void RemoveListener (Action<MagnetStatus> method)
        {
           OnStatusChanged -= method;
        }

        public void Switch ()
        {
            if (pole.Equals(MagenticPole.positive))
                ChangePole(MagenticPole.negative);
            else
                ChangePole(MagenticPole.positive);
        }

        public void ChangePole (MagenticPole pole)
        {
            this.currentStatus.pole = pole;
            OnStatusChanged?.Invoke(currentStatus);
        }
    }
}
