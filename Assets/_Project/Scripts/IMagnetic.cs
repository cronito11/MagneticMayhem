using UnityEngine;

namespace MagneticMayhem
{
    public enum MagenticPole
    {
        None = 0,
        positive = 1,
        negative = 2
    }
    public interface IMagneticApply
    {
        MagenticPole pole {get;}

        void ApplyMagenist ();
        void AddMagnet (IMagneticRecieve magnet);
        void RemoveMagnet (IMagneticRecieve magnet);
    }

    public interface IMagneticRecieve
    {
        MagenticPole pole { get; }
        void TakenMagneticForce (Vector2 direction, float magnitude);
    }
}
