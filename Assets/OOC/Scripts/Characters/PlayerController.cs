using UnityEngine;

namespace OOC.Characters
{
    public class PlayerController
    {
        private IMotor Motor;
        private DefaultControls Controls;

        public PlayerController(DefaultControls controls)
        {
            Controls = controls;
            Controls.Player.Move.performed += Move_performed;
        }

        public void Possess(IMotor motor)
        {
            Motor = motor;
        }

        public void Unpossess()
        {
            Motor = null;
        }

        private void Move_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            if (Motor == null)
                return;

            var direction = obj.ReadValue<Vector2>();
            Motor.Move(direction);

            Debug.Log($"Direction={direction}");
        }

        public void Pause()
        {
            if (Motor == null)
                return;

            Motor.TurnOn(false);
        }

        public void Unpause()
        {
            if (Motor == null)
                return;

            Motor.TurnOn(true);
        }

        public Vector3 GetPosition()
        {
            if (Motor == null)
                return Vector3.zero;

            return Motor.GetPosition();
        }

        public bool HasMotor()
        {
            return Motor != null;
        }
    }
}

