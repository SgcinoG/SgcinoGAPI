using Hangfire;
using Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Host.HttpListener;


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
