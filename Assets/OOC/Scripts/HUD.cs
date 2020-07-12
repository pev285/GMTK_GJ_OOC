using UnityEngine;
using UnityEngine.UI;

namespace OOC
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

