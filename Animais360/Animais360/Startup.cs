using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Animais360.Startup))]
namespace Animais360
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
