using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using ProjectNJSJ.Assets.Scripts.Sprites;
using ProjectNJSJ.Assets.Scripts.ServiceLocators;

namespace ProjectNJSJ.Assets.Scripts.Sprites
{
    public class SpriteBehaviour : MonoBehaviour
    {
        [SerializeField] private string spriteRendTag;
        [SerializeField] private List<Sprite> spriteList;
        private List<CharaState> charaStateList = new List<CharaState>();
        private Dictionary<CharaState, Sprite> charaSpriteMap = new Dictionary<CharaState, Sprite>();
        private SpriteRenderer setSprite;
        private ISpriteProvider spriteProvider;

        // Start is called before the first frame update
        void Start()
        {
            // 登録された依存関係を使用する
            spriteProvider = ServiceLocatorProvider.Instance.unitySpriteCurrent.Resolve<ISpriteProvider>();
            setSprite = GameObject.FindGameObjectWithTag("PlayerChara").GetComponent<SpriteRenderer>();
            // foreach(CharaState state in Enum.GetValues(typeof(CharaState)))
            // {
            //     charaStateList.Add(state);
            // }
            // for(int i = 0; i < Enum.GetNames(typeof(CharaState)).Length; ++i)
            // {
            //     charaSpriteMap.Add(charaStateList[i], )
            // }
        }

        // 操作によってスプライトを変更する
        public void SwitchingSprite(CharaState charaState)
        {
            // 待機
            if(CharaState.Wait == charaState)
            {
                setSprite.sprite = spriteList[0];
            }
            // 歩き
            else if(CharaState.Walk == charaState)
            {
                setSprite.sprite = spriteList[0];
            }
            // 走り
            else if(CharaState.Run == charaState)
            {
                setSprite.sprite = spriteList[0];
            }
            // スライディング
            else if(CharaState.Sliding == charaState)
            {
                setSprite.sprite = spriteList[1];
            }
        }
    }
}