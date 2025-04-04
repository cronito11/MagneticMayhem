using UnityEngine;

namespace MagneticMayhem
{
    [CreateAssetMenu(fileName = "PlayerConfigurableGO", menuName = "Scriptable Objects/PlayerConfigurableGO")]
    public class PlayerConfigurableGO : ConfigurableGO
    {
        [Header("Player Magnetic Properties")]
        [field: SerializeField] public Player playerIdentifier { get; private set; }
        [field: SerializeField] public MagnetStatus playerStatus { get; private set; }
        [field: SerializeField] public bool magnetismEnabled { get; private set; } = false;

    }
}
