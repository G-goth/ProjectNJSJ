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