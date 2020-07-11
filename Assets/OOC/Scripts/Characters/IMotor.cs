using UnityEngine;

namespace OOC.Characters
{
    public interface IMotor 
    {
        void TurnOn(bool on);
        Vector3 GetPosition();

        void Move(Vector2 direction);
        void Attack(Vector2 direction);
    }
}

