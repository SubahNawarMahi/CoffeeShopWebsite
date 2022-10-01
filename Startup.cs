using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CoffeeShopProject.Startup))]
namespace CoffeeShopProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
