using OOC.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OOC
{
    public class Root : MonoBehaviour
    {
        public static Root Instance { get; private set; }

        [SerializeField]
        private Soul PlayerSoul;
        [SerializeField]
        public GameObject StartingPlayerBody;


        private DefaultControls PlayerInput;
        //private PlayerController PlayerController;



        private void Awake()
        {
            Instance = this;

            CreateControlsObject();
        }

        private void Start()
        {
            SetupPlayer();
        }


        public DefaultControls GetPlayerInput()
        {
            return PlayerInput;
        }

        public bool IsPlayerInTheFlesh()
        {
            return PlayerSoul.IsInTheFlesh();
        }

        public Vector3 GetPlayerPosition()
        {
            return PlayerSoul.PlayerController.GetPosition();
        }


        private void SetupPlayer()
        {
            var motor = StartingPlayerBody.GetComponent<IMotor>();
            if (motor == null)
                StartingPlayerBody.AddComponent<HumanMotor>();

            var controller = new PlayerController(PlayerInput);
            PlayerSoul.SetPlayerController(controller);
            PlayerSoul.AttachToBody(StartingPlayerBody.transform);
        }

        private void CreateControlsObject()
        {
            PlayerInput = new DefaultControls();
            PlayerInput.Enable();
        }

        private void Pause()
        {
            PlayerSoul.Pause();
            PlayerInput.Player.Disable();
        }

        private void Unpause()
        {
            PlayerSoul.Unpause();
            PlayerInput.Player.Enable();
        }
    }
}

