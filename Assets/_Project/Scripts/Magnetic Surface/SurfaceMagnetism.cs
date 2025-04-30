using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum MagnetSurfaceType
{
    NormalSurface = 0, BackSurface= 1
}

public enum SurfaceObjectType
{
    Ceiling = 0, Middle = 1, Floor = 2, Boundary = 3, None = 4  
}

public enum MagneticFieldAlignment
{
    Vertical = 0, Horizontal = 1
}

namespace MagneticMayhem
{
    public class SurfaceMagnetism : MonoBehaviour, IMagneticApply, IConfigurable
    {
        private Action<MagnetStatus> OnStatusChanged;

        //Magntic constant is a constant value that is used to calculate the force between two magnetic poles but it is not used in this script
        protected const float MAGNETIC_CONSTANT = 1.0e-7f;
        protected const int MIN_MAGNETIC_FORCE = 1;

        //a reference of transform where distnce will be calculated because if the transform is too close with player it would give 0 distance and force will be infinite
        [Tooltip("An offset is need from the actual body transform of the surface to calculate the magnetic force." +
            "In the case when surface is too thin the force of attraction might go infinity because of the less than 1 distance." +
            "This is configured through scriptable objects for floor celling and middle but not for Boundaries and None surface type")]
        [SerializeField] protected Transform distanceAnchor;

        [Tooltip("Configure this in inspector only if the suraface type is None otherwise it will configure it self through scriptable object.")]
        [SerializeField] protected MagnetStatus currentMagneticStatus;

        [Tooltip("The magnet type basically refers to either normal magnet or Black magnet (Black magnet attracts all magnetic poles.)")]
        [SerializeField] protected MagnetSurfaceType magnetType;

        [Tooltip("This surface type is to indentify the game objects for configuration if the surface is floor, celling, middle or a boundary. Selecting None will not configure the surface. It has to get configured manually through inspector.")]
        [SerializeField] protected SurfaceObjectType surfaceType;

        [SerializeField] LevelConfigurableSO levelConfiguration;

        [SerializeField] private Transform gfxTransform;

        [SerializeField] private Transform areaEffectTransform;

        [Tooltip("Changing this through inspector wont do anything because it is getting configured in the script through scriptable object.")]
        [field: SerializeField] public MagneticFieldAlignment magneticFeildAlignment { get; protected set; }

        private Dictionary<IMagneticRecieve, Transform> magnetsArround = new Dictionary<IMagneticRecieve, Transform>();
       

        public MagenticPole pole => currentMagneticStatus.pole;

        private void Awake()
        {
            Configure();    
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

            float distance = Mathf.Abs((distanceAnchor.position.y) - (target.position.y));

            float magneticForce = MathF.Max(currentMagneticStatus.poleIntensity / (distance * (distance)), MIN_MAGNETIC_FORCE);

            Vector2 direction;

            if(magneticFeildAlignment == MagneticFieldAlignment.Horizontal)

                direction = Vector2.right * Mathf.Sign(distanceAnchor.position.x - target.position.x);
            else
                direction = Vector2.up * Mathf.Sign(distanceAnchor.position.y - target.position.y);

            magnet.ReceivMagnetism(direction, (magneticForce), currentMagneticStatus.pole);
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
            method.Invoke(currentMagneticStatus);
        }

        public void RemoveListener(Action<MagnetStatus> method)
        {
            OnStatusChanged -= method;
        }

        public void Configure()
        {
            //check if the surface type is configurable or not
            if (surfaceType == SurfaceObjectType.None)
            {
                Debug.Log($"Surface is not configurable for {this.gameObject.name}");
                return;
            }

            //deserialize
            SurfaceConfigurableSO surfaceConfig = levelConfiguration.configurableSOs.OfType<SurfaceConfigurableSO>().FirstOrDefault(surfaceConfig => surfaceConfig.surfaceObjectType == this.surfaceType);

            if (surfaceConfig == null)
            {
                Debug.Log($"PlayerConfigurableGO not found for {this.surfaceType}");
                return;
            }

            //configure magnetic status
            currentMagneticStatus = surfaceConfig.surfaceStatus;

            //configure magnetic alignment
            magneticFeildAlignment = surfaceConfig.magneticFieldAlignment;

            //configure gfx transform scale and area effect size
            BoxCollider2D surfaceBoxCollider = GetComponent<BoxCollider2D>();
            BoxCollider2D areaEffect = areaEffectTransform.GetComponent<BoxCollider2D>();   

            if(magneticFeildAlignment == MagneticFieldAlignment.Vertical)
            {
                gfxTransform.localScale = surfaceConfig.gfxTransformScaleHoriziontal;
                
                //configure box collider size of the magnetic surface (not the area effect/ magnetic feild size)
                Debug.Log($"{gfxTransform.localScale} for {surfaceType}");
                surfaceBoxCollider.size = new Vector2(gfxTransform.localScale.x, gfxTransform.localScale.y);

                
                areaEffect.size = new Vector2(gfxTransform.localScale.x, surfaceConfig.areaEffectRange);

                //do not configure distanceAnchor for back surface it needs to be configured by the designer
                if (magnetType == MagnetSurfaceType.BackSurface)
                    return;
                distanceAnchor.localPosition = new Vector2(surfaceConfig.areaEffectOffsetHorizontal.x, surfaceConfig.areaEffectOffsetHorizontal.y);
            }
            else
            {
                gfxTransform.localScale = surfaceConfig.gfxTransformScaleVertical;

                //configure box collider size of the magnetic surface (not the area effect/ magnetic feild size)
                Debug.Log($"{gfxTransform.localScale} for {surfaceType}");
                surfaceBoxCollider.size = new Vector2(gfxTransform.localScale.x, gfxTransform.localScale.y);

                areaEffect.size = new Vector2(surfaceConfig.areaEffectRange, gfxTransform.localScale.y);

                if (magnetType == MagnetSurfaceType.BackSurface)
                    return;
                distanceAnchor.localPosition = new Vector2(surfaceConfig.areaEffectOffsetVertical.x, surfaceConfig.areaEffectOffsetVertical.y);
            }


            //configure magnetism on/off
            this.enabled = surfaceConfig.magnetismEnabled;
        }
    }
}
