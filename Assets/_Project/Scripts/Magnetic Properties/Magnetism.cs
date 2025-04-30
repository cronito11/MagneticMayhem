using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace MagneticMayhem
{

    public class Magnetism : MonoBehaviour, IMagneticRecieve, IMagneticApply, IMageneticPoleChangeable, IConfigurable
    {
        private Action<MagnetStatus> OnStatusChanged;
        
        //Check formula
        private const float MAGNETIC_CONSTANT = 1.0e-7f;
        private const int MIN_MAGNETIC_FORCE = 1;
        //configuration 
        [Header("Magnetism Configuration")]
        [SerializeField] LevelConfigurableSO levelConfiguration;
        //Add comment
        [SerializeField] private MagnetStatus currentStatus;


        private Dictionary<IMagneticRecieve, Transform> magnetsArround = new Dictionary<IMagneticRecieve, Transform>();
        private Rigidbody2D rb;

        //[field: SerializeField] public float actionRadio { get; private set; } = 5;

        public MagenticPole pole => currentStatus.pole;

        private PlayerController playerController;
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
            playerController = GetComponent<PlayerController>();
        }

        private void Start()
        {
            //configure magnetism
            Configure();
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

            Vector2 distance = new Vector2(( transform.position.x),( transform.position.y)) - new Vector2(( target.position.x),( target.position.y));
            

            float magneticForce = Mathf.Max(currentStatus.poleIntensity / (distance.sqrMagnitude), MIN_MAGNETIC_FORCE);
            magnet.ReceivMagnetism(distance.normalized,(magneticForce), currentStatus.pole );
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
            if (pole.Equals(MagenticPole.South))
                ChangePole(MagenticPole.North);
            else
                ChangePole(MagenticPole.South);
        }

        public void ChangePole (MagenticPole pole)
        {
            this.currentStatus.pole = pole;
            OnStatusChanged?.Invoke(currentStatus);
        }

        public void Configure()
        {
            
            //deserialize
            PlayerConfigurableSO playerConfig = levelConfiguration.configurableSOs.OfType<PlayerConfigurableSO>().FirstOrDefault(playerConfig => playerConfig.playerIdentifier == playerController.playerIdentifier);
        
            if(playerConfig == null)
            {
                Debug.Log($"PlayerConfigurableGO not found for {playerController.playerIdentifier}");
                return;
            }
            //configure magnetic status
            UpdateMagnetStatus(playerConfig.playerStatus);

            //configure magnetism
            this.enabled = playerConfig.magnetismEnabled;
        }

    }
}
