using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CPM_Scientifica.Startup))]
namespace CPM_Scientifica
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
