using Cinemachine;
using DG.Tweening;
using OOC.Characters.AI;
using OOC.Collectables;
using OOC.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OOC.Characters
{
    public class Soul : MonoBehaviour
    {
        public HUD HUD;
        public CinemachineVirtualCamera Camera;

        public float SwitchBodyTime = 0.2f;
        public float NewBodySearchDistance = 20f;

        public float OneBodyTime { get; private set; }
        public float OneBodyPeriod = 3f;

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
            if (PlayerController == null)
                return;

            if (Body == null)
                return;

            CheckAndSwitchBody();

            if (SwitchingBodyTweener!= null && SwitchingBodyTweener.IsActive() && SwitchingBodyTweener.IsComplete() == false)
                return;



            Transform.position = Vector3.Slerp(Transform.position, Body.position, 0.1f);
        }


        public void SetPlayerController(PlayerController controller)
        {
            PlayerController = controller;
        }

        private void CheckAndSwitchBody()
        {
            OneBodyTime += Time.deltaTime;
            HUD.SetJumpProgress(OneBodyTime / OneBodyPeriod);

            if (OneBodyTime < OneBodyPeriod)
                return;

            SwitchBody();
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
            motor.OnExitFound += Exit;
            motor.OnPotionFound += UsePotion;

            Body = body;

            if (Transform != null)
                SwitchingBodyTweener = Transform.DOMove(Body.position, SwitchBodyTime);

            //Transform.DOMove()

            float closeLens = 7;
            float farLens = 10;

            var sequence = DOTween.Sequence();
            sequence.Append(DOTween.To(x => Camera.m_Lens.OrthographicSize = x, closeLens, farLens, SwitchBodyTime))
                .Append(DOTween.To(x => Camera.m_Lens.OrthographicSize = x, farLens, closeLens, SwitchBodyTime));
            

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
                var motor = Body.GetComponent<IMotor>();

                motor.OnExitFound -= Exit;
                motor.OnPotionFound -= UsePotion;

                var aictrl = Body.GetComponent<ControllerBase>();
                if (aictrl != null)
                    aictrl.Stun();
            }
        }

        private void Exit()
        {
            SceneManager.LoadScene(2);
        }

        private void UsePotion(Potion potion)
        {
            OneBodyTime -= potion.OneBodyTimeDecrease;
            if (OneBodyTime < 0) OneBodyTime = 0;

            OneBodyPeriod += potion.OneBodyPeriodIncrease;

            Destroy(potion.gameObject);
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

        private void SwitchBody()
        {
            Debug.Log("Switching");
            OneBodyTime = 0;

            var results = new List<Collider2D>();
            var position = Root.Instance.GetPlayerPosition();
            Physics2D.OverlapCircle(position, NewBodySearchDistance, ContactFilter, results);

            while (results.Count > 0)
            {
                int index = UnityEngine.Random.Range(0, results.Count);
                var r = results[index];

                var motor = r.GetComponent<IMotor>();
                if (motor == null)
                    continue;

                var newBody = r.transform;
                if (Body == newBody)
                    continue;

                AttachToBody(newBody);
                return;
            }
        }
    }
}

