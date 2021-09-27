using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Microsoft.Owin.Security.DataProtection;
using Owin;
using SETest.Services.Interface;
using SETest.Services;

namespace SETest.App_Start
{
    public static class AutofacConfig
    {
        public static void ConfigureAutofac(this IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            builder.Register(c => app.GetDataProtectionProvider()).InstancePerRequest();

            builder.RegisterType<LiveNationService>().As<ILiveNationService>().InstancePerLifetimeScope();
            
            var globalConfig = GlobalConfiguration.Configuration;
            
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            
            builder.RegisterWebApiFilterProvider(globalConfig);
            
            builder.RegisterWebApiModelBinderProvider();
            
            var container = builder.Build();
            
            var config = new HttpConfiguration();

            config.DependencyResolver = globalConfig.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            
            app.UseAutofacMiddleware(container);
            app.UseWebApi(config);
            app.UseAutofacWebApi(config);
        }
    }
}