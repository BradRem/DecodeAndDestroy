using DataAccess.TableStorage;
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

            // init table storage
            var secretMessageInitializer = new SecretMessageInitializer();
            secretMessageInitializer.Init();
        }
    }
}
