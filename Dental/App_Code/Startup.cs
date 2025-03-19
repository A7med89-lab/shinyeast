using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Dental.Startup))]
namespace Dental
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
