using UnityEngine;

namespace OOC.Characters.AI
{
    public class ControllerBase : MonoBehaviour, IController
    {
        protected bool IsWorking = true;

        protected IMotor Motor;

        protected virtual void Awake()
        {
            var motor = GetComponent<IMotor>();
            Possess(motor);
        }

        public void TurnOn(bool on)
        {
            IsWorking = on;

            if (IsWorking)
                UnpauseMotor();
            else
                PauseMotor();
        }

        private void PauseMotor()
        {
            if (Motor == null)
                return;

            Motor.TurnOn(false);
        }

        private void UnpauseMotor()
        {
            if (Motor == null)
                return;

            Motor.TurnOn(true);
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

    }
}

