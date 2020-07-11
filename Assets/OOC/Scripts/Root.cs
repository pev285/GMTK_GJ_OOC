using UnityEngine;

namespace OOC
{
    public class Root : MonoBehaviour
    {
        public static Root Instance { get; private set; }

        private DefaultControls PlayerControls;

        private void Awake()
        {
            Instance = this;

            PlayerControls = new DefaultControls();
            PlayerControls.Enable();
        }

        public DefaultControls GetPlayerInput()
        {
            return PlayerControls;
        }



        private void Pause()
        {
            PlayerControls.Player.Disable();
        }

        private void Unpause()
        {
            PlayerControls.Player.Enable();
        }
    }
}

