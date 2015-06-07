using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SecretMessageWebsite.Startup))]
namespace SecretMessageWebsite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
