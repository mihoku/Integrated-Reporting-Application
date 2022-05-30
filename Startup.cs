using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ira.Startup))]
namespace ira
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
