using Microsoft.Owin;
using Owin;
using SETest.App_Start;

[assembly: OwinStartup(typeof(SETest.Startup))]

namespace SETest
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            app.ConfigureAutofac();
        }
    }
}
