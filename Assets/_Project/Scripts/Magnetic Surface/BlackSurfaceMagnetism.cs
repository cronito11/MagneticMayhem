using System;
using UnityEngine;

namespace MagneticMayhem
{
    public class BlackSurfaceMagnetism : SurfaceMagnetism
    {
        protected override void CalculateMagenticForce(Transform target, IMagneticRecieve magnet)
        {
            if (magnet.pole.Equals(MagenticPole.None))
                return;

            float distance = (distanceAnchor.position.y) - (target.position.y);

            float magneticForce = MathF.Max( currentMagneticStatus.poleIntensity / (distance * distance), MIN_MAGNETIC_FORCE);

            Vector2 direction;

            if (magneticFeildAlignment == MagneticFieldAlignment.Horizontal)

                direction = Vector2.right * Mathf.Sign(distanceAnchor.position.x - target.position.x);
            else
                direction = Vector2.up * Mathf.Sign(distanceAnchor.position.y - target.position.y);

            //Debug.Log(distance);
            magnet.ReceivMagnetism(direction, magneticForce, magnet.pole);
        }
    }
}
