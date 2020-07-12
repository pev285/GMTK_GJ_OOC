using UnityEngine;

namespace OOC.Characters
{
    public interface IController
    {
        void TurnOn(bool on);

        void Unpossess();
        void Possess(IMotor motor);
    }
}

