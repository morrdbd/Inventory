using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DOMoRR.Startup))]
namespace DOMoRR
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}