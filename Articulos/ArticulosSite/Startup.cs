using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ArticulosSite.Startup))]
namespace ArticulosSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
