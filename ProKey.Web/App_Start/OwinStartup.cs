using ProKey.Web;
using ProKey.Web.IocConfig;
using ProKey.Web.JsonWebTokenConfig;
using Microsoft.Owin;
using Owin;
using ProKey.ServiceLayer.Contracts;

[assembly: OwinStartup(typeof(OwinStartup))]
namespace ProKey.Web
{
    public class OwinStartup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseOAuthAuthorizationServer(SmObjectFactory.Container.GetInstance<AppOAuthOptions>());
            app.UseJwtBearerAuthentication(SmObjectFactory.Container.GetInstance<AppJwtOptions>());

            var container = SmObjectFactory.Container;
            //container.Configure(config =>
            //{
            //    config.For<IDataProtectionProvider>()
            //          .HybridHttpOrThreadLocalScoped()
            //          .Use(() => app.GetDataProtectionProvider());
            //});
            container.GetInstance<IUsersService>().SeedDataBase();
        }
    }
}