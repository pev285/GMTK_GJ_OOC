using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace OOC.Characters.AI
{
    public class ControllerBase : MonoBehaviour, IController
    {
        private Coroutine StunCoroutineRef;
        private float UnpossessedStunTime = 2f;

        protected IMotor Motor;
        protected bool IsWorking = true;



        protected virtual void Awake()
        {
            var motor = GetComponent<IMotor>();
            Possess(motor);
        }

        public void TurnOn(bool on)
        {
            IsWorking = on;

            if (IsWorking)
            {
                if (StunCoroutineRef != null)
                    StopCoroutine(StunCoroutineRef);

                StunCoroutineRef = null;
                UnpauseMotor();
            }
            else
            {
                PauseMotor();

            }
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


        public void Stun()
        {
            StunCoroutineRef = StartCoroutine(DelayedTurnOn());
        }

        private IEnumerator DelayedTurnOn()
        {
            yield return new WaitForSeconds(UnpossessedStunTime);
            TurnOn(true);

            StunCoroutineRef = null;
        }
    }
}

