using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(pryWeb.Startup))]
namespace pryWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
