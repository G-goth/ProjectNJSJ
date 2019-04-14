using ProjectNJSJ.Assets.Scripts;
using ProjectNJSJ.Assets.Scripts.Player;
using ProjectNJSJ.Assets.Scripts.InputProviders;
using ProjectNJSJ.Assets.Scripts.Sprites;
using ProjectNJSJ.Assets.Scripts.SpriteProvider;

namespace ProjectNJSJ.Assets.Scripts.ServiceLocators
{
    public class ServiceLocatorProvider : SingletonMonoBehaviour<ServiceLocatorProvider>
    {
        public ServiceLocator unityCurrent{ get; private set; }
        public ServiceLocator unitySpriteCurrent{ get; private set; }
        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        protected override void Awake()
        {
            // 依存関係を登録
            base.Awake();
            unityCurrent = new ServiceLocator();
            unityCurrent.Register<IInputProvider>(new UnityInputProvider());

            unitySpriteCurrent = new ServiceLocator();
            unitySpriteCurrent.Register<ISpriteProvider>(new UnitySpriteProvider());
        }
    }
}