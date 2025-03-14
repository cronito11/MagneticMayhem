using UnityEngine;

namespace MagneticMayhem
{
    [CreateAssetMenu(fileName = "LevelConfiguration", menuName = "Scriptable Objects/LevelConfiguration")]
    public class LevelConfiguration : ScriptableObject
    {
        #region PlayerOne

        [Header("Player One Config")]
        public MagenticPole PlayerOnePole;
        public float PlayerOneMagneticIntensity;
        public float PlayerOneMagneticFieldRange;

        #endregion

        #region PlayerTwo

        [Header("Player Two Config")]
        public MagenticPole PlayerTwoPole;
        public float PlayerTwoMagneticIntensity;
        public float PlayerTwoMagneticFieldRange;

        #endregion

        #region Celling

        [Header("Ceeling Config")]
        public MagenticPole CellingPole;
        public float CellingPoleIntensity;
        public Vector2 CellingMagneticFieldRange;

        #endregion

        #region Floor

        [Header("Ceeling Config")]
        public MagenticPole FloorPole;
        public float FloorPoleIntensity;
        public Vector2 FloorMagneticFieldRange;

        #endregion

    }
}
