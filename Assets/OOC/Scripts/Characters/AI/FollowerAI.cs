using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace OOC.Characters.AI
{
    public class FollowerAI : AIBase
    {
        private float Speed = 3f;
        private Vector2 Destination;

        //private Transform Transform;
        private Rigidbody2D RB;

        private Tweener CurrentTween;

        private void Awake()
        {
            RB = GetComponent<Rigidbody2D>();
            //Transform = GetComponent<Transform>();
        }

        private void Start()
        {
            Destination = RB.position;

            StartCoroutine(DecisionCorutine());
        }

        private IEnumerator DecisionCorutine()
        {
            while (true)
            {
                Destination = Root.Instance.GetPlayerPosition();

                var delta = (Destination - RB.position).magnitude;
                //if (delta == 0)
                //    return;

                var time = delta / Speed;


                if (CurrentTween != null)
                    CurrentTween.Kill();
                CurrentTween = RB.DOMove(Destination, time);
                //var direction = (Destination - Transform.position).normalized;


                yield return new WaitForSeconds(0.3f);
            }
        }

        private void Update()
        {
            if (IsWorking == false)
                return;

        }
    }
}

