using UnityEngine;
using ProjectNJSJ.Assets.Scripts.Sprite;

namespace ProjectNJSJ.Assets.Scripts.SpriteProvider
{
    public class UnitySpriteProvider : MonoBehaviour, ISpriteProvider
    {
        // キャラクターのスプライトレンダラーを取得
        public SpriteRenderer GetCharacterSprite()
        {
            return (default);
        }
        // キャラクターのスプライトレンダラーをスイッチング
        public SpriteRenderer SwtchingCharacterSprite()
        {
            return (default);
        }
    }
}