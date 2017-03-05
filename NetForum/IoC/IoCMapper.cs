using AutoMapper;
using Mapping.Configuration;
using Microsoft.Practices.Unity;
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
        private readonly IUnityContainer _unityContainer;
        private readonly IMapper _mapper;

        public IoCMapper()
        {
            _unityContainer = new UnityContainer();
            var mapperConfig = new AutoMapperConfiguration();
            _mapper = mapperConfig.Map();
        }

        public void Map()
        {
            _unityContainer.RegisterType<IUnitOfWork, UnitOfWork>();
            _unityContainer.RegisterInstance<IMapper>(_mapper);
        }
    }
}
