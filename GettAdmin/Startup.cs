using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GettAdmin.Startup))]
namespace GettAdmin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
