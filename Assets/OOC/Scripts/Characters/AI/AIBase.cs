using UnityEngine;

namespace OOC.Characters.AI
{
    public class AIBase : MonoBehaviour
    {
        protected bool IsWorking = true;


        public void TurnOn(bool on)
        {
            IsWorking = on;
        }

    }
}

