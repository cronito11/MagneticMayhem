using UnityEngine;
using UnityEngine.Events;
using static InputSystem_Actions;

namespace MagneticMayhem
{
    [CreateAssetMenu(fileName = "PlayerInputReader", menuName = "Scriptable Objects/Player1InputReader")]
    public class Player1InputReader : InputReader, IPlayerActions
    {
        protected override void InitializeInput ()
        {
            if (input == null)
            {
                input = new InputSystem_Actions();
                input.Player.SetCallbacks(this);
            }
        }

        public override void EnablePlayerActions ()
        {
            input.Player.Enable();
        }

        public override void DisablePlayerActions ()
        {
            input.Player.Disable();
        }
    }
}
