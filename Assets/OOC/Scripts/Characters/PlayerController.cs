using UnityEngine;

namespace OOC.Characters
{
    public class PlayerController : IController
    {
        private IMotor Motor;
        private DefaultControls Controls;

        private bool IsWorking = true;

        public PlayerController(DefaultControls controls)
        {
            Controls = controls;
            Controls.Player.Move.performed += Move_performed;
            Controls.Player.Jump.performed += Jump_performed;
        }

        private void Jump_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            Debug.Log($"Jump act: {obj.ReadValueAsButton()}");
        }

        public void TurnOn(bool on)
        {
            IsWorking = on;

            if (IsWorking)
                UnpauseMotor();
            else
                PauseMotor();
        }


        public void Possess(IMotor motor)
        {
            Motor = motor;

            if (Motor != null)
                Motor.TurnOn(true);
        }


        public void Unpossess()
        {
            if (Motor == null)
                return;

            Motor.TurnOn(false);
            Motor = null;
        }

        public void PauseMotor()
        {
            if (Motor == null)
                return;

            Motor.TurnOn(false);
        }

        public void UnpauseMotor()
        {
            if (Motor == null)
                return;

            Motor.TurnOn(true);
        }

        private void Move_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            if (Motor == null || IsWorking == false)
                return;

            var direction = obj.ReadValue<Vector2>();
            Motor.Move(direction);

            //Debug.Log($"Control direction={direction}");
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

