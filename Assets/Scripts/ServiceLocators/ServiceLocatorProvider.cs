using ProjectNJSJ.Assets.Scripts;
using ProjectNJSJ.Assets.Scripts.Player;

namespace ProjectNJSJ.Assets.Scripts.ServiceLocators
{
    public class ServiceLocatorProvider : SingletonMonoBehaviour<ServiceLocatorProvider>
    {
        public ServiceLocator Current{ get; private set; }
        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        protected override void Awake()
        {
            Current = new ServiceLocator();
            // Current.Register<IInputProvider>();
        }
    }
}