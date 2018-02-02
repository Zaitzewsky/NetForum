 using System.Web.Http;
using Owin;
using IoC;
using UoW.Interface;
using UoW;
using UserAccountServiceNameSpace.Interface;
using UserAccountServiceNameSpace.Service;
using UserAccountFacade.Interface;
using UserAccountFacade.Facade;
using Mapping.Configuration;
using System.Web.Http.Dependencies;
using NetForumApi;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using System;
using Unity;
using Unity.Lifetime;

[assembly: OwinStartup(typeof(Startup))]
namespace NetForumApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            //Configure DI and Authorization provider
            var unityContainer = CreateAndRegisterUnityContainer();
            var authProvider = GetAuthProvider(unityContainer);
            config.DependencyResolver = CreateUnityResolver(unityContainer);

            ConfigureOAuth(app, authProvider);
                      
            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        private static UnityContainer CreateAndRegisterUnityContainer()
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
            return unityContainer;
        }

        public IDependencyResolver CreateUnityResolver(IUnityContainer unityContainer)
        {
            GetAuthProvider(unityContainer);

            return new UnityResolver(unityContainer);
        }

        private AuthorizationServerProvider GetAuthProvider(IUnityContainer unityContainer)
        {
            return new AuthorizationServerProvider(unityContainer.Resolve<ILoginFacade>(), unityContainer.Resolve<IUnitOfWork>());
        }

        public void ConfigureOAuth(IAppBuilder app, AuthorizationServerProvider authProvider)
        {
            var oAuthServerOptions = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = authProvider
            };

            app.UseOAuthAuthorizationServer(oAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}