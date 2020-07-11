using System.Linq.Expressions;
using UnityEngine;

namespace OOC.Characters
{
    public class HumanMotor : MonoBehaviour, IMotor
    {
        public float Speed = 5f;

        private bool IsOn = false;
        //private Transform Transform;
        private Rigidbody2D RB;


        private Vector2 MoveDirecton;
        private Vector2 AttackDirection;

        private void Awake()
        {
            ResetControlVectors();
            //Transform = GetComponent<Transform>();
            RB = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            //Transform.position = Transform.position + (Vector3)shift;
        }

        private void FixedUpdate()
        {
            if (IsOn == false)
                return;

            var coeff = Speed * Time.deltaTime;
            var shift = coeff * MoveDirecton;

            RB.MovePosition(RB.position + shift);
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

