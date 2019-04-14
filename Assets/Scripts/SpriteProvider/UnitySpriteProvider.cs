using UnityEngine;
using ProjectNJSJ.Assets.Scripts.Sprites;

namespace ProjectNJSJ.Assets.Scripts.SpriteProvider
{
    public class UnitySpriteProvider : ISpriteProvider
    {
        private SpriteRenderer spriteRenderer;

        // キャラクターのスプライトレンダラーを取得
        public SpriteRenderer GetCharacterSprite(string spriteNameStr)
        {
            spriteRenderer = GameObject.FindGameObjectWithTag(spriteNameStr).GetComponent<SpriteRenderer>();
            return spriteRenderer;
        }

        // キャラクターのスプライトレンダラーをスイッチング
        public SpriteRenderer SwtchingCharacterSprite()
        {
            return (default);
        }

        // キャラクターのスプライトを反転させる
        public void IsFlippingSprite(SpriteRenderer spriteRenderer, bool isFlip)
        {
            spriteRenderer.flipX = isFlip;
        }
    }
}