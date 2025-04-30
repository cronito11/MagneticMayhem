using UnityEngine;

namespace MagneticMayhem
{
    [CreateAssetMenu(fileName = "SurfaceConfigurableSO", menuName = "Scriptable Objects/SurfaceConfigurableSO")]
    public class SurfaceConfigurableSO : ConfigurableSO
    {
        //default gfx transform scale for horizontal alignment
        private const float HORIZONTAL_X_BOUND = 30F;
        private const float HORIZONTAL_Y_BOUND = 0.3F;

        //default gfx transform scale for vertical alignment
        private const float VERTICAL_X_BOUND = 0.3F;
        private const float VERTICAL_Y_BOUND = 30F;

        //default gfx transform rotation for horizontal alignment
        //private const float HORIZONTAL_ROTATION = 0F;
        //default gfx transform rotation for vertical alignment
        //private const float VERTICAL_ROTATION = 90F;

        // Default magnetic status constants
        private const float DEFAULT_MAGNETIC_STATUS_RANGE = 9f;
        private const float DEFAULT_MAGNETIC_STATUS_INTENSITY = 70f;

        //default area effect range/depth
        private const float DEFAULT_AREA_EFFECT_RANGE = 9F;

        //default area effect offset for horizontal alignment
        private const float DEFAULT_AREA_EFFECT_OFFSET_X_HORIZONTAL = 0F;
        private const float DEFAULT_AREA_EFFECT_OFFSET_Y_HORIZONTAL = -1F;

        //default area effect offset for vertical alignment
        private const float DEFAULT_AREA_EFFECT_OFFSET_X_VERTICAL = 1F;
        private const float DEFAULT_AREA_EFFECT_OFFSET_Y_VERTICAL = 0F;

        // Default magnetic status
        private static readonly MagnetStatus DEFAULT_MAGNETIC_STATUS = new MagnetStatus
        {
            rangeOfMegneticField = DEFAULT_MAGNETIC_STATUS_RANGE,
            poleIntensity = DEFAULT_MAGNETIC_STATUS_INTENSITY,
        };


        [field: SerializeField] public MagnetStatus surfaceStatus { get; private set; } = DEFAULT_MAGNETIC_STATUS;
        [field: SerializeField] public SurfaceObjectType surfaceObjectType { get; private set; } 
        [field: SerializeField] public MagneticFieldAlignment magneticFieldAlignment { get; private set; }
        [field: SerializeField] public bool magnetismEnabled { get; private set; } = false;
        [field: SerializeField] public Vector3 gfxTransformScaleHoriziontal { get; private set; } = new Vector3(HORIZONTAL_X_BOUND, HORIZONTAL_Y_BOUND, 1);
        [field: SerializeField] public Vector3 gfxTransformScaleVertical { get; private set; } = new Vector3(VERTICAL_X_BOUND, VERTICAL_Y_BOUND, 1);
        //[field: SerializeField] public Vector3 gfxTransformRotationHorizontal { get; private set; } = new Vector3(0, 0, HORIZONTAL_ROTATION);
        //[field: SerializeField] public Vector3 gfxTransformRotationVertical { get; private set; } = new Vector3(0, 0, VERTICAL_ROTATION);
        [field: SerializeField] public float areaEffectRange { get; private set; } = DEFAULT_AREA_EFFECT_RANGE;
        [field: SerializeField] public Vector3 areaEffectOffsetHorizontal { get; private set; } = new Vector2(DEFAULT_AREA_EFFECT_OFFSET_X_HORIZONTAL, DEFAULT_AREA_EFFECT_OFFSET_Y_HORIZONTAL);
        [field: SerializeField] public Vector3 areaEffectOffsetVertical { get; private set; } = new Vector2(DEFAULT_AREA_EFFECT_OFFSET_X_VERTICAL, DEFAULT_AREA_EFFECT_OFFSET_Y_VERTICAL);

    }
}
