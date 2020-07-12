using OOC.Collectables;
using System;
using UnityEngine;

namespace OOC.Characters
{
    public interface IMotor 
    {
        event Action OnExitFound;
        event Action<Potion> OnPotionFound;

        void TurnOn(bool on);
        Vector3 GetPosition();

        void Move(Vector2 direction);
        void Attack(Vector2 direction);
    }
}

