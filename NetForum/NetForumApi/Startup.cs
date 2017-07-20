 using System.Web.Http;
using Owin;
using IoC;
using Microsoft.Practices.Unity;
using UoW.Interface;
using UoW;
using UserAccountServiceNameSpace.Interface;
using UserAccountServiceNameSpace.Service;
using UserAccountFacade.Interface;
using UserAccountFacade.Facade;
using Mapping.Configuration;
using System.Web.Http.Dependencies;

namespace NetForumApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            config.DependencyResolver = SetDependencies();
            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        public IDependencyResolver SetDependencies()
        {
            var mapperConfig = new AutoMapperConfiguration();
            var mapper = mapperConfig.Map();

            var unityContainer = new UnityContainer();
            unityContainer.RegisterType<IUnitOfWork, UnitOfWork>(new PerThreadLifetimeManager());
            unityContainer.RegisterType<IRegisterService, RegisterService>();
            unityContainer.RegisterType<IRegisterFacade, RegisterFacade>();
            unityContainer.RegisterType<ILoginService, LoginService>();
            unityContainer.RegisterType<ILoginFacade, LoginFacade>();
            unityContainer.RegisterInstance(mapper);

            return new UnityResolver(unityContainer);
        }
    }
}