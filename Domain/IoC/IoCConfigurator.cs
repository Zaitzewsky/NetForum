using Microsoft.Practices.Unity;

namespace IoC
{
    public class IoCConfigurator
    {
        private readonly IUnityContainer _unityContainer;

        public IoCConfigurator()
        {
            _unityContainer = new UnityContainer();
        }

        public void RegisterType<TFrom, TTo>() where TTo : TFrom
        {
            _unityContainer.RegisterType<TFrom, TTo>();
        }
    }
}
