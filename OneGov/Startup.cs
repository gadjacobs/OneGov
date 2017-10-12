using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CBNFormQ.Startup))]
namespace CBNFormQ
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
