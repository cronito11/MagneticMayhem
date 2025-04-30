using UnityEngine;

namespace MagneticMayhem
{
    [CreateAssetMenu(fileName = "PlayerConfigurableSO", menuName = "Scriptable Objects/PlayerConfigurableSO")]
    public class PlayerConfigurableSO : ConfigurableSO
    {
        [Header("Player Magnetic Properties")]
        [field: SerializeField] public Player playerIdentifier { get; private set; }
        [field: SerializeField] public MagnetStatus playerStatus { get; private set; }
        [field: SerializeField] public bool magnetismEnabled { get; private set; } = false;

    }
}
