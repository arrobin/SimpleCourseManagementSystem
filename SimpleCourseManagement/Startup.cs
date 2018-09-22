using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SimpleCourseManagement.Startup))]
namespace SimpleCourseManagement
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
