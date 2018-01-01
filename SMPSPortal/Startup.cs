using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SmpsPortal.Startup))]
namespace SmpsPortal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
