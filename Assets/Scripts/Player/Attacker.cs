using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using ProjectNJSJ.Assets.Scripts.ServiceLocators;

namespace ProjectNJSJ.Assets.Scripts.Player
{
    // ダメージ関連の構造体
    struct DamageRelatedValues
    {
        // ダメージ量
        int damageAmount;
        // 弱点だったときのダメージ係数
        float damageCoeffect;
        // 発生フレーム数
        int startFrame;
        // 持続フレーム数
        int meatyFrame;
        // 発生後硬直フレーム数
        int hitBlockFrame;
    }
    public class Attacker : MonoBehaviour
    {
        private IInputProvider inputProvider = (default);
        private GameObject achikita;

        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        void Start()
        {
            // 攻撃をさせるキャラクターをタグ検索
            achikita = GameObject.FindGameObjectWithTag("PlayerChara");
            // 登録された依存関係を使用する
            inputProvider = ServiceLocatorProvider.Instance.unityCurrent.Resolve<IInputProvider>();

            // 近距離攻撃その1
            bool? result = true;
            var shortRangeAttack = this.UpdateAsObservable()
                .Where(_ => inputProvider.GetAttackButton() & result != false)
                .AsUnitObservable()
                .BatchFrame(0, FrameCountType.FixedUpdate)
                .Subscribe(_ => {
                    StartCoroutine(AttackDuration(r => result = r, 3));
                });
        }
        // プレイヤーキャラの攻撃持続時間コルーチン
        private IEnumerator AttackDuration(Action<bool> callBack, int attackFrame)
        {
            callBack(false);
            for(int i = 0; i < attackFrame; ++i)
            {
                yield return new WaitForEndOfFrame();
            }
            Debug.Log("Attack!!");
            callBack(true);
        }
        // 連続攻撃時にコンボの猶予時間以内にボタンが押されていればなにかするメソッド
        private void CommboDelayTime()
        {
        }
        // 攻撃判定をキャラクターの向きと合わせる
        private void AttackCollisionFlipping()
        {
        }
    }
}