using DG.Tweening;
using OOC.Characters.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OOC.Characters
{
    public class Soul : MonoBehaviour
    {
        public float SwitchBodyTime = 0.2f;
        public float UnpossessedStunTime = 2f;

        public float LifeInOneBodyPeriod = 3f;
        public float NewBodySearchDistance = 20f;

        public PlayerController PlayerController { get; private set; }

        public LayerMask BodiesMask;
        private ContactFilter2D ContactFilter;

        private Transform Body;
        private Transform Transform;

        private Tweener SwitchingBodyTweener;

        private void Awake()
        {
            Transform = GetComponent<Transform>();
        }

        private void Start()
        {
            ContactFilter.layerMask = BodiesMask;
        }

        private void Update()
        {
            if (Body == null)
                return;

            if (SwitchingBodyTweener.IsActive() && SwitchingBodyTweener.IsComplete() == false)
                return;

            Transform.position = Body.position;
        }

        public void SetPlayerController(PlayerController controller)
        {
            PlayerController = controller;
            StartCoroutine(BodySwitchCoroutine());
        }

        public void AttachToBody(Transform newBody)
        {
            var motor = newBody.GetComponent<IMotor>();
            if (motor == null)
                return;

            FreeBody();
            TakeOverBody(newBody, motor);
        }

        private void TakeOverBody(Transform body, IMotor motor)
        {
            Body = body;
            SwitchingBodyTweener = Transform.DOMove(Body.position, SwitchBodyTime);

            var controller = Body.GetComponent<ControllerBase>();
            if (controller != null)
                controller.TurnOn(false);
            PlayerController.Possess(motor);
        }

        private void FreeBody()
        {
            PlayerController.Unpossess();
            if (Body != null)
            {
                var aictrl = Body.GetComponent<ControllerBase>();
                if (aictrl != null)
                    StartCoroutine(DelayedTurnOn(aictrl));
            }
        }

        private IEnumerator DelayedTurnOn(ControllerBase controller)
        {
            yield return new WaitForSeconds(UnpossessedStunTime);
            controller.TurnOn(true);
        }

        public bool IsInTheFlesh()
        {
            return PlayerController.HasMotor() && Body != null;
        }

        public void Pause()
        {
            PlayerController.PauseMotor();
        }

        public void Unpause()
        {
            PlayerController.UnpauseMotor();

        }

        private IEnumerator BodySwitchCoroutine()
        {
            while (true)
            {
                SwitchBody();
                yield return new WaitForSeconds(LifeInOneBodyPeriod);
            }
        }


        private void SwitchBody()
        {
            Debug.Log("Switching");

            var results = new List<Collider2D>();
            var position = Root.Instance.GetPlayerPosition();


            Physics2D.OverlapCircle(position, NewBodySearchDistance, ContactFilter, results);

            foreach (var r in results)
            {
                var motor = r.GetComponent<IMotor>();

                if (motor == null)
                    continue;

                var newBody = r.transform;

                if (Body == newBody)
                    continue;

                AttachToBody(newBody);
                //motor.TurnOn(true);
                //PlayerController.Unpossess();
                //PlayerController.Possess(motor);
                break;
            }

        }



    }
}

