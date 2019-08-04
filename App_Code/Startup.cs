using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ChatCube.Startup))]
namespace ChatCube
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
