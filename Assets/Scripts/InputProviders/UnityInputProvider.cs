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

        // 右方向へ移動
        public bool GetKeyMoveRight()
        {
            return Input.GetKey(KeyCode.D);
        }
        // 左方向へ移動
        public bool GetKeyMoveLeft()
        {
            return Input.GetKey(KeyCode.A);
        }
        // しゃがみボタン
        public bool GetKeyMoveUnder()
        {
            return Input.GetKey(KeyCode.S);
        }
        // ジャンプボタン
        public bool GetJump()
        {
            return Input.GetKeyDown(KeyCode.Space);
        }

        // スライディング
        public bool GetKeySliding()
        {
            return Input.GetKeyDown(KeyCode.LeftShift);
            // return Input.GetKeyDown(KeyCode.D) & Input.GetKeyDown(KeyCode.S);
        }
        // 移動(2D)
        public float GetMoveDirectionHorizontal2D()
        {
            return Input.GetAxis("Horizontal");
        }
        // 移動(3D)
        public Vector3 GetMoveDirection()
        {
            return new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        }
    }
}