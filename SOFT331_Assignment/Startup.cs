using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SOFT331_Assignment.Startup))]
namespace SOFT331_Assignment
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
