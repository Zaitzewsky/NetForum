using UoW;
using UoW.Interface;

namespace IoC
{
    /// <summary>
    /// This is mapping all interfaces and classes together all over this whole application. That includes Application services, Unit of Work etc..
    /// This will later be instantiated in the WebAPI and will act as the DI setup for the whole application.
    /// </summary>
    public class IoCMapper
    {
        private readonly IoCConfigurator _configurator;

        public IoCMapper()
        {
            _configurator = new IoCConfigurator();
        }

        public void Map()
        {
            _configurator.RegisterType<IUnitOfWork, UnitOfWork>();
        }
    }
}
