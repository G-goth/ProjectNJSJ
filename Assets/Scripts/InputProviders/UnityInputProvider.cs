using UnityEngine;
using ProjectNJSJ.Assets.Scripts.Player;

namespace ProjectNJSJ.Assets.Scripts.InputProviders
{
    public class UnityInputProvider : IInputProvider 
    {
        // 指定されたキーが押されている間、常に値を返す
        public bool GetKeyMove(KeyCode keyCode)
        {
            return Input.GetKey(keyCode);
        }
        // 指定されたキーが押されている間、常に値を返す
        public bool GetKeyMoveRight()
        {
            return Input.GetKey(KeyCode.D);
        }        // 指定されたキーが押されている間、常に値を返す
        public bool GetKeyMoveLeft()
        {
            return Input.GetKey(KeyCode.A);
        }
        // ジャンプボタン
        public bool GetJump()
        {
            return Input.GetKeyDown(KeyCode.Space);
        }

        public Vector3 GetMoveDirection()
        {
            return new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        }
    }
}