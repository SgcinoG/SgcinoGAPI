using Hangfire;
using Owin;
using System.Configuration;

namespace SgcinoGAPIService
{
    class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var connString = ConfigurationManager.ConnectionStrings["SgcinoGAPIDB"].ConnectionString;
            GlobalConfiguration.Configuration
                .UseColouredConsoleLogProvider()
                .UseSqlServerStorage(connString);
            app.UseHangfireDashboard();
        }
    }
}
