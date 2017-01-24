using Data.Repository.Repository;
using Domain.Interface;

namespace IoC
{
    public class IoCMapper
    {
        private readonly IoCConfigurator _configurator;

        public IoCMapper()
        {
            _configurator = new IoCConfigurator();
        }

        public void Map()
        {
            _configurator.RegisterType<IUserRepository, UserRepository>();
        }
    }
}
