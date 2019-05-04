using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebFormDemo.Startup))]
namespace WebFormDemo
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
