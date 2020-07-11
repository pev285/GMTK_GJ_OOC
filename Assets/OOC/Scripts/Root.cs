using OOC.Characters;
using UnityEngine;

namespace OOC
{
    public class Root : MonoBehaviour
    {
        public static Root Instance { get; private set; }

        [SerializeField]
        public GameObject StartingPlayerBody;


        //private Soul PlyaerSoul;
        private DefaultControls PlayerInput;
        private PlayerController PlayerController;

        private void Awake()
        {
            Instance = this;

            CreateControlsObject();
            SetupPlayer();
        }

        private void Start()
        {
            PlayerController.Unpause();
        }

        public DefaultControls GetPlayerInput()
        {
            return PlayerInput;
        }

        public bool IsPlayerInTheFlesh()
        {
            return PlayerController.HasMotor();
        }

        public Vector3 GetPlayerPosition()
        {
            return PlayerController.GetPosition();
        }


        private void SetupPlayer()
        {
            var motor = StartingPlayerBody.GetComponent<IMotor>();
            if (motor == null)
                motor = StartingPlayerBody.AddComponent<HumanMotor>();

            PlayerController = new PlayerController(PlayerInput);
            PlayerController.Possess(motor);
        }

        private void CreateControlsObject()
        {
            PlayerInput = new DefaultControls();
            PlayerInput.Enable();
        }

        private void Pause()
        {
            PlayerController.Pause();
            PlayerInput.Player.Disable();
        }

        private void Unpause()
        {
            PlayerController.Unpause();
            PlayerInput.Player.Enable();
        }
    }
}

