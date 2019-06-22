using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(information.Startup))]
namespace information
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
