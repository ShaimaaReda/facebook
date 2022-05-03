using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(face.Startup))]
namespace face
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
