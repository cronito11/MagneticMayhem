using UnityEngine;

namespace MagneticMayhem
{
    public class PlayerParticleSystem : MonoBehaviour
    {
        [SerializeField] private ParticleSystem surfaceCollisionParticleSys;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private Magnetism playerMagnetism;
        [SerializeField] private Color hexColorRed = Color.red; // Default color is red
        [SerializeField] private Color hexColorBlue = Color.blue; // Default color is blue

        private void Awake()
        {
            if (playerController == null)
            {
                playerController = GetComponentInParent<PlayerController>();
            }
            if (playerMagnetism == null)
            {
                playerMagnetism = GetComponentInParent<Magnetism>();
            }
        }
        private void OnEnable()
        {
            playerController.OnCollisionWithSurface += EmmitParticlesOnCollisionWithSurface;
        }
        private void OnDisable()
        {
            playerController.OnCollisionWithSurface -= EmmitParticlesOnCollisionWithSurface;
        }

        void EmmitParticlesOnCollisionWithSurface()
        {
            if(surfaceCollisionParticleSys is null)
            {
                Debug.LogWarning("Particle system is not assigned in the inspector.");
                return;
            }

            var mainModule = surfaceCollisionParticleSys.main;

             if(playerMagnetism.pole == MagenticPole.positive)
             {
                mainModule.startColor = hexColorBlue;
                surfaceCollisionParticleSys.Play();
             }
             else
             {
                mainModule.startColor = hexColorRed;
                surfaceCollisionParticleSys.Play();
             }
            
        }
    }
}
