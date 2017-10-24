using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebBanDongHo.Startup))]
namespace WebBanDongHo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
