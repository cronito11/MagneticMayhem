using System;
using UnityEngine;

namespace MagneticMayhem
{
    [System.Serializable]
    public struct MagnetStatus
    {
        public MagenticPole pole;
        public float rangeOfMegneticField;
        public float poleIntensity;
    }

    public enum MagenticPole
    {
        None = 0,
        positive = 1,
        negative = 2,
        black = 3
    }

    public interface IMageneticPoleChangeable
    {
        void Switch ();
        void ChangePole (MagenticPole pole);

        void SuscribeListener (Action<MagnetStatus> method);
        void RemoveListener (Action<MagnetStatus> method);
    }

    public interface IMagneticApply
    {
        void ApplyMagnetism ();
        void AddMagnet (IMagneticRecieve magnet);
        void RemoveMagnet (IMagneticRecieve magnet);

        void SuscribeListener (Action<MagnetStatus> method);
        void RemoveListener (Action<MagnetStatus> method);
    }

    public interface IMagneticRecieve
    {
        MagenticPole pole { get; }
        void ReceivMagnetism (Vector2 direction, float magnitude, MagenticPole pole);
    }
}
