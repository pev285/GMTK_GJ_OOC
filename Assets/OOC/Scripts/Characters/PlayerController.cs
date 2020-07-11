using UnityEngine;

namespace OOC.Characters
{
    public class PlayerController : MonoBehaviour
    {
        private IMotor Motor;
        private DefaultControls Controls;

        private void Awake()
        {
            if (Motor == null)
                Motor = GetComponent<IMotor>();
        }

        private void Start()
        {
            SubscribeInput();
        }

        private void SubscribeInput()
        {
            Controls = Root.Instance.GetPlayerInput();
            Controls.Player.Move.performed += Move_performed;
        }

        private void Move_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            if (Motor == null)
                return;

            var direction = obj.ReadValue<Vector2>();
            Motor.Move(direction);

            Debug.Log($"Direction={direction}");
        }

    }
}

