using UnityEngine;

namespace ProjectNJSJ.Assets.Scripts.Player
{
    interface IInputProvider
    {
        bool GetKeyMove(KeyCode keyCode);
        bool GetJump();
        Vector3 GetMoveDirection();
    }
}