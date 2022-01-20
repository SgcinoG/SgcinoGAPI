using Hangfire;
using Microsoft.Owin.Hosting;
using System;
using System.Configuration;
using Topshelf;
using SgcinoGClient;
using SgcinoGAPIBusinessLayer;

namespace SgcinoGAPIService
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.SetServiceName("SgcinoAPIService");
                x.SetDisplayName("Sgcino API Service");
                x.SetDescription("Adds Orders bra");
                x.Service<HangfireService>(servconfig =>
                {
                    servconfig.ConstructUsing(() => new HangfireService());
                    servconfig.WhenStarted(s => s.Start());
                    servconfig.WhenStopped(s => s.Stop());
                });
            });
        }

        public class HangfireService
        {
            BackgroundJobServer server;
            private IDisposable _host;

            public void Start()
            {
                var options = new StartOptions
                {
                    Port = int.Parse(ConfigurationManager.AppSettings["HangfireDashboardPort"])
                };

                _host = WebApp.Start<Startup>(options);

                //Need to provide the timezone to Hangfire - If we don't, it will take a default timezone which is not SA time and will not execute jobs when we expect it to
                var timeZone = TimeZoneInfo.FindSystemTimeZoneById("South Africa Standard Time");

                //start the BackgroundJob Server
                server = new BackgroundJobServer();

                //schedule job
                RecurringJob.AddOrUpdate("Add Order", () => new OrdersBl().AddOrder(new SgcinoGAPIDataModels.Models.OrdersCtrl { Created = DateTime.Now, UserId= "4714340D-D0A1-4D74-A993-7CB8D79F51A2", Products = new System.Collections.Generic.List<int> { 1, 2 } }), "0 10 * * *", timeZone);
                RecurringJob.AddOrUpdate("Add User", () => new UsersBl().RegisterUser(new SgcinoGAPIDataModels.Models.UsersCtrl { Created = DateTime.Now, Name = "From Service", Surname = "Service", Username = "UserService" }), "10 10 * * *", timeZone);
                //# ┌───────────── minute (0 - 59)
                //# │ ┌───────────── hour (0 - 23)
                //# │ │ ┌───────────── day of the month (1 - 31)
                //# │ │ │ ┌───────────── month (1 - 12)
                //# │ │ │ │ ┌───────────── day of the week (0 - 6) (Sunday to Saturday;
                //# │ │ │ │ │                                   7 is also Sunday on some systems)
                //# │ │ │ │ │
                //# │ │ │ │ │
                //# * * * * *
            }

            public void Stop()
            {
                server?.Dispose();
                _host?.Dispose();
            }
        }
    }
}
