using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GiraffRentBoat.Startup))]
namespace GiraffRentBoat
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
