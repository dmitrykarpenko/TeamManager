using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TeamManager.Web.Startup))]
namespace TeamManager.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
