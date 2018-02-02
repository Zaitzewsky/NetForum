using AutoMapper;
using Mapping.Configuration;
using Microsoft.Practices.Unity;
using UoW;
using UoW.Interface;
using UserAccountFacade.Facade;
using UserAccountFacade.Interface;
using UserAccountServiceNameSpace.Interface;
using UserAccountServiceNameSpace.Service;
using System.Web.Http.Dependencies;
using System;
using System.Collections.Generic;
using Unity;
using Unity.Exceptions;

namespace IoC
{
    /// <summary>
    /// This is mapping all interfaces and classes together all over this whole application. That includes Application services, Unit of Work etc..
    /// This is instantiated in the WebAPI and will act as the DI setup for the whole application.
    /// </summary>
    public class UnityResolver : IDependencyResolver
    {
        protected IUnityContainer _container;

        public UnityResolver(IUnityContainer container)
        {
            _container = container;
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return _container.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return _container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return new List<object>();
            }
        }

        public IDependencyScope BeginScope()
        {
            var child = _container.CreateChildContainer();
            return new UnityResolver(child);
        }

        public void Dispose()
        {
            _container.Dispose();
        }
    }
}
