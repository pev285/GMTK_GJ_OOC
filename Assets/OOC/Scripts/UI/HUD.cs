using UnityEngine;
using UnityEngine.UI;

namespace OOC.UI
{
    public class HUD : MonoBehaviour
    {
        [SerializeField]
        private Slider JumpProgressBar;

        public void SetJumpProgress(float value)
        {
            JumpProgressBar.value = value;
        }
    }
}

