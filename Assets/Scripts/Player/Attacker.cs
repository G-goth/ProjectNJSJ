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
    struct TechniqueRelatedValues
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
        // 攻撃当たり判定オブジェクトの取得(かなり雑に・・・)
        [SerializeField] private GameObject rightAttackHitObject;
        [SerializeField] private GameObject leftAttackHitObject;
        [SerializeField] private ResponsiveStickTag responsiveTag;
        private IDisposable rightAttack;
        private IDisposable leftAttack;
        // 攻撃ボタンの取得関連
        private IInputProvider inputProvider = (default);
        // 攻撃をするキャラクターの取得関連
        private GameObject achikita;
        private TechniqueRelatedValues damageValues = new TechniqueRelatedValues();

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

            // 一旦、当たり判定が近づいたらそのゲームオブジェクトの名前を返すだけのやつ
            rightAttack = rightAttackHitObject.OnTriggerStay2DAsObservable()
                .Where(trigger => trigger.transform.tag == Enum.GetName(typeof(ResponsiveStickTag), responsiveTag))
                .Subscribe(trigger => {
                    Debug.Log(trigger.gameObject.name);
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