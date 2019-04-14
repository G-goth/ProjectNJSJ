using UnityEngine;
using UniRx;
using UniRx.Triggers;
using ProjectNJSJ.Assets.Scripts.ServiceLocators;

namespace ProjectNJSJ.Assets.Scripts.Player
{
    public class Mover : MonoBehaviour
    {
        [SerializeField]
        private float movePower;
        [SerializeField]
        private float jumpPower;
        private IInputProvider inputProvider;
        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        void Start()
        {
            // 移動させたいオブジェクトを取得する
            var achikita = GameObject.FindGameObjectWithTag("PlayerChara");
            var achikita_Rigid = achikita.GetComponent<Rigidbody>();

            // 登録された依存関係を使用する
            inputProvider = ServiceLocatorProvider.Instance.unityCurrent.Resolve<IInputProvider>();

            // 右方向へ移動
            var keyMoverRight = this.UpdateAsObservable()
                .Where(_ => inputProvider.GetKeyMoveRight())
                .AsUnitObservable()
                .BatchFrame(0, FrameCountType.FixedUpdate)
                .Subscribe(_ => {
                    Debug.Log("Right");
                    CharacterMover(KeyCode.D, achikita_Rigid);
                });

            // 左方向への移動
            var keyMoverLeft = this.UpdateAsObservable()
                .Where(_ => inputProvider.GetKeyMoveLeft())
                .AsUnitObservable()
                .BatchFrame(0, FrameCountType.FixedUpdate)
                .Subscribe(_ => {
                    Debug.Log("Left");
                    CharacterMover(KeyCode.A, achikita_Rigid);
                });
            
            // ジャンプ
            var keyJump = this.UpdateAsObservable()
                .Where(_ => inputProvider.GetJump())
                .AsUnitObservable()
                .BatchFrame(0, FrameCountType.FixedUpdate)
                .Subscribe(_ => {
                    Debug.Log("Jump");
                    CharacterJump(achikita_Rigid);
                });
        }
        
        // キー操作による移動
        private void CharacterMover(KeyCode keyCode, Rigidbody rigid)
        {
            if(KeyCode.D == keyCode) rigid.AddForce(new Vector3(movePower, 0.0f, 0.0f), ForceMode.VelocityChange);

            if(KeyCode.A == keyCode) rigid.AddForce(new Vector3(-movePower, 0.0f, 0.0f), ForceMode.VelocityChange);
        }

        // ジャンプ
        private void CharacterJump(Rigidbody rigid)
        {
            rigid.AddForce(new Vector3(0.0f, jumpPower, 0.0f), ForceMode.Impulse);
        }
    }
}