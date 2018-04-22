using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Alkobazar.Startup))]
namespace Alkobazar
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
