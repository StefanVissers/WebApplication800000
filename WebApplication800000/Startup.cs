using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebApplication800000.Startup))]
namespace WebApplication800000
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
