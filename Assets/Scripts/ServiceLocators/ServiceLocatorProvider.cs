using ProjectNJSJ.Assets.Scripts;
using ProjectNJSJ.Assets.Scripts.Player;
using ProjectNJSJ.Assets.Scripts.InputProviders;

namespace ProjectNJSJ.Assets.Scripts.ServiceLocators
{
    public class ServiceLocatorProvider : SingletonMonoBehaviour<ServiceLocatorProvider>
    {
        public ServiceLocator unityCurrent{ get; private set; }
        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        protected override void Awake()
        {
            base.Awake();
            unityCurrent = new ServiceLocator();
            unityCurrent.Register<IInputProvider>(new UnityInputProvider());
        }
    }
}