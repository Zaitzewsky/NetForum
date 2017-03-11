using System.Web.Http;
using Owin;
using IoC;

namespace NetForumApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);

            var ioc = new IoCMapper();
            ioc.Map();
        }
    }
}