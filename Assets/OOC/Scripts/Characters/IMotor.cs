using UnityEngine;

namespace OOC.Characters
{
    public interface IMotor 
    {
        void TurnOn(bool on);

        void Move(Vector2 direction);
        void Attack(Vector2 direction);
    }
}

