using System;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

namespace MagneticMayhem
{

    public class Magnetism : MonoBehaviour, IMagneticRecieve, IMagneticApply
    {
        //Check formula
        private const float MAGNETIC_CONSTANT = 1.0e-7f;
        //Add comment
        [SerializeField] private float intensity;
        [SerializeField] private MagenticPole _pole;

        private Dictionary<IMagneticRecieve, Transform> magnetsArround = new Dictionary<IMagneticRecieve, Transform>();
        private Rigidbody2D rb;

        public MagenticPole pole => _pole;

        private void Awake ()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        public void ApplyMagenist ()
        {
            foreach (var magnet in magnetsArround)
            {
                CalculateMagenticForce(magnet.Value, magnet.Key);
            }
        }

        private void CalculateMagenticForce (Transform target,  IMagneticRecieve magnet)
        {
            if (magnet.pole.Equals(MagenticPole.None))
                return;

            Vector2 distance = new Vector2(Mathf.Floor( transform.position.x),Mathf.Floor( transform.position.y)) - new Vector2(Mathf.Floor( target.position.x),Mathf.Floor( target.position.y));//  transform.position - target.position;
            distance.x = (float) Math.Round(distance.x,2);
            distance.y = (float) Math.Round(distance.y,2);

            float magneticForce = (intensity) / (distance.sqrMagnitude);
            magnet.TakenMagneticForce(distance.normalized, magneticForce * (pole.Equals(magnet.pole) ? -1 : 1));
        }

        public void TakenMagneticForce (Vector2 direction, float magnitude)
        {
            rb.AddForce(direction * magnitude);
            Debug.Log(rb.linearVelocity);
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
            ApplyMagenist();
        }
    }
}
