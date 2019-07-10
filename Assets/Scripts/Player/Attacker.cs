using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using ProjectNJSJ.Assets.Scripts.ServiceLocators;

namespace ProjectNJSJ.Assets.Scripts.Player
{
    public class Attacker : MonoBehaviour
    {
        private IInputProvider inputProvider = (default);
        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        void Start()
        {
            // 攻撃させるキャラクターをここで探す
            var achikita = GameObject.FindGameObjectWithTag("PlayerChara");

            // 登録された依存関係を使用する
            inputProvider = ServiceLocatorProvider.Instance.unityCurrent.Resolve<IInputProvider>();

            // 近距離攻撃その1
            var shortRangeAttack = this.UpdateAsObservable()
                .Where(_ => inputProvider.GetAttackButton())
                .AsUnitObservable()
                .BatchFrame(0, FrameCountType.FixedUpdate)
                .Subscribe(_ => {
                    bool? result = null;
                    StartCoroutine(AttackDuration(r => result = r, 3));
                });
        }
        // プレイヤーキャラの攻撃持続時間コルーチン
        private IEnumerator AttackDuration(Action<bool> callBack, int attackFrame)
        {
            for(int i = 0; i < attackFrame; ++i)
            {
                yield return new WaitForSeconds(1.0f);
            }
            Debug.Log("Attack!!");
            callBack(false);
        }
        // 連続攻撃時にコンボの猶予時間以内にボタンが押されていればなにかするメソッド
        private void CommboDelayTime()
        {
        }
    }
}