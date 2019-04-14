using UnityEngine;
using UniRx;
using UniRx.Triggers;
using ProjectNJSJ.Assets.Scripts.ServiceLocators;
using ProjectNJSJ.Assets.Scripts.Sprites;

namespace ProjectNJSJ.Assets.Scripts.Player
{
    enum DimensionsEnum
    {
        TwoDimension,
        ThreeDimension
    }
    public class Mover : MonoBehaviour
    {
        private ForceMode moveForceMode = (default);
        private ForceMode jumpForceMode = (default);
        [SerializeField] private ForceMode2D moveForceMode2D = (default);
        [SerializeField] private ForceMode2D jumpForceMode2D = (default);
        [SerializeField] private DimensionsEnum dimensionsEnum = (default);
        [SerializeField] private float movePower = (default);
        [SerializeField] private float jumpPower = (default);
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
                    CharacterMoverRight2D(achikita_Rigid);
                    spriteBehaviour.SwitchingSprite(CharaState.Run);
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
                .Where(_ => inputProvider.GetKeyMoveLeft() || inputProvider.GetKeyMoveRight())
                .Where(_ => inputProvider.GetKeyMoveUnder())
                .AsUnitObservable()
                .BatchFrame(0, FrameCountType.FixedUpdate)
                .Subscribe(_ => {
                    spriteBehaviour.SwitchingSprite(CharaState.Sliding);
                });
        }
        
        // キー操作による右移動(2D)
        private void CharacterMoverRight2D(Rigidbody2D rigid)
        {
            spriteProvider.IsFlippingSprite(spriteRend, true);
            rigid.AddForce(new Vector2(movePower, 0.0f), moveForceMode2D);
        }
        // キー操作による左移動(2D)
        private void CharacterMoverLeft2D(Rigidbody2D rigid)
        {
            spriteProvider.IsFlippingSprite(spriteRend, false);
            rigid.AddForce(new Vector2(-movePower, 0.0f), moveForceMode2D);
        }

        // ジャンプ(2D)
        private void CharacterJump2D(Rigidbody2D rigid)
        {
            rigid.AddForce(new Vector2(0.0f, jumpPower), jumpForceMode2D);
        }
        // ジャンプ(3D)
        private void CharacterJump3D(Rigidbody rigid)
        {
            rigid.AddForce(new Vector3(0.0f, jumpPower, 0.0f), jumpForceMode);
        }

        // スライディング
        private void CharacterSliding2D(Rigidbody2D rigid)
        {}
    }
}