using UnityEngine;

namespace ProjectNJSJ.Assets.Scripts.Player
{
    interface IInputProvider
    {
        bool GetKeyMove();
        bool GetJump();
        Vector3 GetMoveDirection();
    }
}