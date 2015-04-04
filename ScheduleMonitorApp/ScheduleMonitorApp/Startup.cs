using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ScheduleMonitorApp.Startup))]
namespace ScheduleMonitorApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
