using UnityEngine;

namespace ProjectNJSJ.Assets.Scripts.Player
{
    public interface IInputProvider
    {
        bool GetKeyMove(KeyCode keyCode);
        bool GetKeyMoveRight();
        bool GetKeyMoveLeft();
        bool GetKeyMoveUnder();
        bool GetKeySliding();
        bool GetJump();
        float GetMoveDirectionHorizontal2D();
        Vector3 GetMoveDirection();
    }
}