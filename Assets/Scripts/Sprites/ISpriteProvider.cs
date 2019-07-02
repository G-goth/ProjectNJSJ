using UnityEngine;

namespace ProjectNJSJ.Assets.Scripts.Sprites
{
    public interface ISpriteProvider
    {
        bool isFlipSpriteDirection{ get; }
        SpriteRenderer GetCharacterSprite(string spriteNameStr);
        SpriteRenderer SwtchingCharacterSprite();
        void IsFlippingSprite(SpriteRenderer spriteRenderer, bool isFilp);
    }
}