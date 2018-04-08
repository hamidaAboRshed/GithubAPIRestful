using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GithubWebApp.Startup))]
namespace GithubWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
