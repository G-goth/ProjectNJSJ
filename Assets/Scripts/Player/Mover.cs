using UnityEngine;
using UniRx;
using UniRx.Triggers;
using ProjectNJSJ.Assets.Scripts.ServiceLocators;

namespace ProjectNJSJ.Assets.Scripts.Player
{
    public class Mover : MonoBehaviour
    {
        private IInputProvider inputProvider;
        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        void Start()
        {
            // 登録された依存関係
            inputProvider = ServiceLocatorProvider.Instance.unityCurrent.Resolve<IInputProvider>();
            // 右方向へ移動
            var keyMoverRight = this.UpdateAsObservable()
                .Where(_ => inputProvider.GetKeyMove(KeyCode.D))
                .Subscribe(_ => {
                    Debug.Log("Right");
                });

            // 左方向への移動
            var keyMoverLeft = this.UpdateAsObservable()
                .Where(_ => inputProvider.GetKeyMove(KeyCode.A))
                .Subscribe(_ => {
                    Debug.Log("Left");
                });
        }
    }
}