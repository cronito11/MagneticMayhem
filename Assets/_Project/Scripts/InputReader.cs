using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace MagneticMayhem
{
    //[CreateAssetMenu(fileName = "InputReader", menuName = "Scriptable Objects/InputReader")]
    public abstract class InputReader : ScriptableObject
    {
        public event UnityAction<Vector2> Move = delegate {};
        protected InputSystem_Actions input;

        private void OnEnable ()
        {
            InitializeInput();
        }

        protected abstract void InitializeInput ();

        public abstract void EnablePlayerActions ();

        public abstract void DisablePlayerActions ();

        public void OnMove (InputAction.CallbackContext context)
        {
            switch (context.phase)
            {
                case InputActionPhase.Performed:
                case InputActionPhase.Canceled:
                    //Move?.Invoke(Vector2.zero);
                    Move?.Invoke(context.ReadValue<Vector2>());
                    break;
            }

            InitializeInput();
        }




        public void OnAttack (InputAction.CallbackContext context)
        {

        }

        public void OnCrouch (InputAction.CallbackContext context)
        {

        }

        public void OnInteract (InputAction.CallbackContext context)
        {

        }

        public void OnJump (InputAction.CallbackContext context)
        {

        }

        public void OnLook (InputAction.CallbackContext context)
        {

        }

        public void OnNext (InputAction.CallbackContext context)
        {

        }

        public void OnPrevious (InputAction.CallbackContext context)
        {

        }

        public void OnSprint (InputAction.CallbackContext context)
        {

        }
    }
}
