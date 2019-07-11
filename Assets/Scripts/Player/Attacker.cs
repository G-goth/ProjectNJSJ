using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using ProjectNJSJ.Assets.Scripts.Enemy;
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
        [SerializeField] private ResponsiveStickTagOfEnemy enemyTag;
        private IDisposable rightAttack;
        private IDisposable leftAttack;
        // 攻撃ボタンの取得関連
        private IInputProvider inputProvider = (default);
        // 攻撃をするキャラクターの取得関連
        private GameObject achikita;
        private TechniqueRelatedValues damageValues = new TechniqueRelatedValues();
        // スプライト関連
        private SpriteRenderer spriteRend = (default);

        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        void Start()
        {
            // 攻撃をさせるキャラクターをタグ検索
            achikita = GameObject.FindGameObjectWithTag("PlayerChara");
            // キャラのスプライトを取得
            spriteRend = achikita.GetComponent<SpriteRenderer>();
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
                .Where(trigger => trigger.transform.tag == Enum.GetName(typeof(ResponsiveStickTagOfEnemy), enemyTag))
                .Subscribe(trigger => {
                    Debug.Log(trigger.gameObject.name);
                });
        }

        /// <summary>
        /// プレイヤーキャラの攻撃持続時間コルーチン
        /// </summary>
        /// <param name="callBack">コルーチンの結果コールバック</param>
        /// <param name="attackFrame">持続フレーム数(int)</param>
        private IEnumerator AttackDuration(Action<bool> callBack, int attackFrame)
        {
            callBack(false);
            // キャラクターの向いている方向によってアクティブにする当たり判定を変える
            if(spriteRend.flipX)
            {
                rightAttackHitObject.SetActive(true);
            }
            else
            {
                leftAttackHitObject.SetActive(true);
            }
            
            // 持続フレーム分ループを回す
            for(int i = 0; i < attackFrame; ++i)
            {
                yield return new WaitForEndOfFrame();
            }

            // 一旦、すべての攻撃判定のアクティブをオフにする
            rightAttackHitObject.SetActive(false);
            leftAttackHitObject.SetActive(false);
            callBack(true);
            yield break;
        }
        // 連続攻撃時にコンボの猶予時間以内にボタンが押されていればなにかするメソッド
        private void CommboDelayTime()
        {
        }
        // 攻撃判定をキャラクターの向きと合わせる
        private void AttackCollisionFlipping()
        {
        }
        // ヒットしたもののオブジェクト名を表示するメソッド
    }
}