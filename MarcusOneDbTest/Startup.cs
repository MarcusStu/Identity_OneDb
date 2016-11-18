using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MarcusOneDbTest.Startup))]
namespace MarcusOneDbTest
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}