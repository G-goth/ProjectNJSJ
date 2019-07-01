using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace ProjectNJSJ.Assets.Scripts.Player
{
    public class PlayerStatus : MonoBehaviour
    {
        [SerializeField] private GameObject playerStatusCheckObj;
        [SerializeField] private Mover moverObject;
        [SerializeField] private ResponsiveStickTag responsiveTag;
        private PlayerStatusLevel statusLevel;
        private IDisposable playerCheckerStay;
        private IDisposable playerCheckerExit;

        public PlayerStatusLevel PlayerStatusLevelProp
        {
            private set{ statusLevel = value; }
            get{ return statusLevel; }
        }

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        void Awake()
        {
            PlayerStatusLevelProp = PlayerStatusLevel.Air;
        }

        // Start is called before the first frame update
        void Start()
        {
            playerCheckerStay = playerStatusCheckObj.OnTriggerStay2DAsObservable()
                .Where(trigger => trigger.transform.tag == Enum.GetName(typeof(ResponsiveStickTag), responsiveTag))
                .Subscribe(trigger => {
                    statusLevel = PlayerStatusLevel.Ground;
                    PlayerStatusLevelProp = PlayerStatusLevel.Ground;
                });
            
            playerCheckerExit = playerStatusCheckObj.OnTriggerExit2DAsObservable()
                .Where(trigger => trigger.transform.tag == Enum.GetName(typeof(ResponsiveStickTag), responsiveTag))
                .Subscribe(trigger => {
                    statusLevel = PlayerStatusLevel.Air;
                    PlayerStatusLevelProp = PlayerStatusLevel.Air;
                });
        }
    }
}