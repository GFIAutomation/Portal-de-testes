using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(gfi_test_landing.Startup))]
namespace gfi_test_landing
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
