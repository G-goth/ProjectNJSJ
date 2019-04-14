using UnityEngine;

namespace ProjectNJSJ.Assets.Scripts.Sprite
{
    public interface ISpriteProvider
    {
        SpriteRenderer GetCharacterSprite();
        SpriteRenderer SwtchingCharacterSprite();
    }
}