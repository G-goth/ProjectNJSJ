using System.Collections;
using System.Linq;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using ProjectNJSJ.Assets.Scripts.ServiceLocators;
using ProjectNJSJ.Assets.Scripts.Sprites;

namespace ProjectNJSJ.Assets.Scripts.Player
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] private ForceMode2D moveForceMode2D = (default);
        [SerializeField] private ForceMode2D jumpForceMode2D = (default);
        // 移動関係
        [SerializeField] private float maxSpeedLimit = (default);
        [SerializeField] private float movePower = (default);
        [SerializeField] private float dragPower = (default);
        [SerializeField] private PlayerStatus playerStatus = (default);
        // スライディング関連
        private bool isSliding = (default);
        [SerializeField] private float slidingMovePower = (default);
        [SerializeField] private int slidingMoveTime = (default);
        // ジャンプ関連
        [SerializeField] private float jumpPower = (default);
        // スプライト関係
        private SpriteRenderer spriteRend = (default);
        private SpriteBehaviour spriteBehaviour = (default);
        private IInputProvider inputProvider = (default);
        private ISpriteProvider spriteProvider = (default);

        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        void Start()
        {
            // 移動させたいオブジェクトを取得する
            var achikita = GameObject.FindGameObjectWithTag("PlayerChara");
            var achikita_Rigid = achikita.GetComponent<Rigidbody2D>();
            // キャラのスプライトを取得
            spriteRend = achikita.GetComponent<SpriteRenderer>();

            // 登録された依存関係を使用する
            inputProvider = ServiceLocatorProvider.Instance.unityCurrent.Resolve<IInputProvider>();
            spriteProvider = ServiceLocatorProvider.Instance.unitySpriteCurrent.Resolve<ISpriteProvider>();
            spriteBehaviour = GetComponent<SpriteBehaviour>();

            // 右方向へ移動
            var keyMoverRight = this.UpdateAsObservable()
                .Where(_ => inputProvider.GetKeyMoveRight())
                .AsUnitObservable()
                .BatchFrame(0, FrameCountType.FixedUpdate)
                .Subscribe(_ => {
                    Debug.Log(playerStatus.PlayerStatusLevelProp);
                    CharacterMoverRight2D(achikita_Rigid);
                    spriteBehaviour.SwitchingSprite(CharaState.Run);
                });
            // Dキーをリリースしたときの挙動
            var keyMoverRightUp = this.UpdateAsObservable()
                .Where(_ => playerStatus.PlayerStatusLevelProp == PlayerStatusLevel.Ground)
                .Where(_ => inputProvider.GetKeyMoveRightRelease())
                .AsUnitObservable()
                .BatchFrame(0, FrameCountType.FixedUpdate)
                .Subscribe(_ => {
                    // string methodName = new Func<Rigidbody2D, IEnumerator>(CharaterMoverDeceleration2D).Method.Name;
                    // StartCoroutine(methodName, achikita_Rigid);
                    CharaterMoverSuddenBraking2D(achikita_Rigid);
                });

            // 左方向への移動
            var keyMoverLeft = this.UpdateAsObservable()
                .Where(_ => inputProvider.GetKeyMoveLeft())
                .AsUnitObservable()
                .BatchFrame(0, FrameCountType.FixedUpdate)
                .Subscribe(_ => {
                    CharacterMoverLeft2D(achikita_Rigid);
                    spriteBehaviour.SwitchingSprite(CharaState.Run);
                });
            // Aキーを離したときの挙動
            var keyMoverLeftUp = this.UpdateAsObservable()
                .Where(_ => playerStatus.PlayerStatusLevelProp == PlayerStatusLevel.Ground)
                .Where(_ => inputProvider.GetKeyMoveLeftRelease())
                .AsUnitObservable()
                .BatchFrame(0, FrameCountType.FixedUpdate)
                .Subscribe(_ => {
                    CharaterMoverSuddenBraking2D(achikita_Rigid);
                });
            
            // ジャンプ
            var keyJump = this.UpdateAsObservable()
                .Where(_ => inputProvider.GetJump())
                .AsUnitObservable()
                .BatchFrame(0, FrameCountType.FixedUpdate)
                .Subscribe(_ => {
                    CharacterJump2D(achikita_Rigid);
                });

            // しゃがみ
            var crouching = this.UpdateAsObservable()
                .Where(_ => inputProvider.GetKeyMoveUnder())
                .Subscribe(_ => {
                    spriteBehaviour.SwitchingSprite(CharaState.Crouching);
                });

            // スライディング
            var sliding = this.UpdateAsObservable()
                .Where(_ => inputProvider.GetKeySliding())
                .Subscribe(_ => {
                    StartCoroutine("CharacterSliding2DTypeCoroutine", achikita_Rigid);
                });
        }

        // キー操作による右移動(2D)
        private void CharacterMoverRight2D(Rigidbody2D rigid)
        {
            Vector2 rightVec2 = new Vector2(movePower, 0.0f);
            spriteProvider.IsFlippingSprite(spriteRend, true);
            if(rigid.velocity.magnitude < maxSpeedLimit)
            {
                rigid.AddForce(rightVec2);
            }
        }
        // キー操作による左移動(2D)
        private void CharacterMoverLeft2D(Rigidbody2D rigid)
        {
            Vector2 leftVec2 = new Vector2(-movePower, 0.0f);
            spriteProvider.IsFlippingSprite(spriteRend, false);
            if(rigid.velocity.magnitude < maxSpeedLimit)
            {
                rigid.AddForce(leftVec2, moveForceMode2D);
            }
        }

        // ジャンプ(2D)
        private void CharacterJump2D(Rigidbody2D rigid)
        {
            rigid.AddForce(new Vector2(0.0f, jumpPower), jumpForceMode2D);
        }

        // スライディング
        private void CharacterSliding2D(Rigidbody2D rigid)
        {
            Debug.Log(spriteRend.flipX);
            if(spriteRend.flipX == true)
            {
                rigid.AddForce(new Vector2(slidingMovePower, 0.0f), ForceMode2D.Impulse);
            }
            else
            {
                rigid.AddForce(new Vector2(-slidingMovePower, 0.0f), ForceMode2D.Impulse);
            }
        }

        // 減速(velocityを直接操作)
        private void CharaterMoverSuddenBraking2D(Rigidbody2D rigid)
        {
            rigid.velocity = new Vector2(0.0f, 0.0f);
        }
        // 減速(コルーチン)
        private IEnumerator CharaterMoverDeceleration2D(Rigidbody2D rigid)
        {
            while(rigid.velocity.magnitude > 0.0f)
            {
                rigid.drag = dragPower;
                yield return new WaitForFixedUpdate();
            }
            rigid.drag = 0.0f;
            yield return null;
        }
        // スライディング(コルーチン)
        private IEnumerator CharacterSliding2DTypeCoroutine(Rigidbody2D rigid)
        {
            rigid.gravityScale = 0.2f;
            isSliding = true;

            for(int i = 0; i < slidingMoveTime; ++i)
            {
                if(spriteRend.flipX == true)
                {
                    rigid.AddForce(new Vector2(slidingMovePower, 0.0f), ForceMode2D.Impulse);
                }
                else
                {
                    rigid.AddForce(new Vector2(-slidingMovePower, 0.0f), ForceMode2D.Impulse);
                }
                yield return new WaitForFixedUpdate();
            }

            rigid.gravityScale = 1.0f;
            isSliding = false;
            yield return null;
        }
    }
}