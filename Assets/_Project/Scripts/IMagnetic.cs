using System;
using UnityEngine;

namespace MagneticMayhem
{
    [System.Serializable]
    public struct MagnetStatus
    {
        public MagenticPole pole;
        public float actionRadio;
        public float instensity;

    }

    public enum MagenticPole
    {
        None = 0,
        positive = 1,
        negative = 2
    }
    public interface IMagneticApply
    {
        void ApplyMagenism ();
        void AddMagnet (IMagneticRecieve magnet);
        void RemoveMagnet (IMagneticRecieve magnet);

        void SuscribeListener (Action<MagnetStatus> method);
        void RemoveListener (Action<MagnetStatus> method);
    }

    public interface IMagneticRecieve
    {
        MagenticPole pole { get; }
        void TakenMagneticForce (Vector2 direction, float magnitude, MagenticPole pole);
    }
}
