using Microsoft.Owin;
using Owin;
using WebPresentation;

[assembly: OwinStartup(typeof(Startup))]

namespace WebPresentation
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}