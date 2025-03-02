using UnityEngine;
using UnityEngine.Events;
using static InputSystem_Actions;

namespace MagneticMayhem
{
    [CreateAssetMenu(fileName = "Player2InputReader", menuName = "Scriptable Objects/Player2InputReader")]
    public class Player2InputReader : InputReader, IPlayer2Actions
    {

        protected override void InitializeInput ()
        {
            if (input == null)
            {
                input = new InputSystem_Actions();
                input.Player2.SetCallbacks(this);
            }
        }

        public override void EnablePlayerActions ()
        {
            input.Player2.Enable();
        }

        public override void DisablePlayerActions ()
        {
            input.Player2.Disable();
        }
    }
}
