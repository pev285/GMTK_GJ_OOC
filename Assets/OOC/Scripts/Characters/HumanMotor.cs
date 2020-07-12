using DG.Tweening;
using System.Linq.Expressions;
using UnityEngine;

namespace OOC.Characters
{
    public class HumanMotor : MonoBehaviour, IMotor
    {
        public float Speed = 5f;
        private float RotationDuration = 0.3f;

        private Rigidbody2D RB;
        private bool IsOn = false;

        private Tweener RotationTweener;

        private Vector2 MoveDirecton;
        private Vector2 AttackDirection;

        private void Awake()
        {
            ResetControlVectors();
            RB = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            if (IsOn == false)
                return;

            CommandMove();
            CommandRotate();
        }

        private void CommandMove()
        {
            var coeff = Speed * Time.deltaTime;
            var shift = coeff * MoveDirecton;

            RB.MovePosition(RB.position + shift);
        }

        private void CommandRotate()
        {
            if (RotationTweener != null && RotationTweener.IsActive() && RotationTweener.IsComplete() == false)
                return;

            if (MoveDirecton == Vector2.zero)
                return;

            var q = Quaternion.FromToRotation(Vector3.up, MoveDirecton);

            float angle;
            Vector3 axis;

            q.ToAngleAxis(out angle, out axis);

            //Debug.Log($"angle={angle}, axis={axis}");
            //var curRotation = Quaternion.LookRotation(MoveDirecton, Vector3.back);
            //curRotation.ToAngleAxis(out angle, out axis);

            if (axis.z < 0)
                angle = 360 - angle;



            RotationTweener = RB.DORotate(angle, RotationDuration);
        }

        public void Attack(Vector2 direction)
        {
            if (IsOn == false)
                return;

            AttackDirection = direction;
        }

        public void Move(Vector2 direction)
        {
            if (IsOn == false)
                return;

            MoveDirecton = direction;
        }

        public void TurnOn(bool on)
        {
            IsOn = on;
            ResetControlVectors();
        }

        private void ResetControlVectors()
        {
            MoveDirecton = Vector2.zero;
            AttackDirection = Vector2.zero;
        }

        public Vector3 GetPosition()
        {
            return RB.position;
        }
    }
}

