using UnityEngine;

namespace ProjectNJSJ.Assets.Scripts.Player
{
    interface IInputProvider
    {
        bool GetKeyMove(KeyCode keyCode);
        bool GetKeyMoveRight();
        bool GetKeyMoveLeft();
        bool GetJump();
        Vector3 GetMoveDirection();
    }
}