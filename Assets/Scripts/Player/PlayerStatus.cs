using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace ProjectNJSJ.Assets.Scripts.Player
{
    public enum ResponsiveStickTag
    {
        PlayerChara =　0
    }

    public class PlayerStatus : MonoBehaviour
    {
        [SerializeField] private GameObject playerStatusCheckObj;
        [SerializeField] private Mover moverObject;
        [SerializeField] private ResponsiveStickTag responsiveTag;

        // Start is called before the first frame update
        void Start()
        {
            var playerChecker = playerStatusCheckObj.OnTriggerStay2DAsObservable()
                .Where(trigger => trigger.transform.tag == Enum.GetName(typeof(ResponsiveStickTag), responsiveTag))
                .Subscribe(trigger => {
                    Debug.Log("Hit Test.");
                });
        }
    }
}