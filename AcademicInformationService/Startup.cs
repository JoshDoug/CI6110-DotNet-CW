using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AcademicInformationService.Startup))]
namespace AcademicInformationService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
