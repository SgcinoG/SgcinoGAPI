using Autofac;
using Autofac.Integration.WebApi;
using ContourApiAuthentication;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using SgcinoGAPIBusinessLayer;
using SgcinoGAPIDataLayer;
using SgcinoGAPIDataLayer.SgcinoData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

[assembly: OwinStartup(typeof(SgcinoGAPI2.Startup))]
namespace SgcinoGAPI2
{
    public class Startup
    {
        public virtual void RegisterDependencies(IAppBuilder app, ContainerBuilder builder)
        {
            builder.Register(c => new SgcinoDataSettings()).As<ISgcinoDataSettings>().SingleInstance();
            //builder.RegisterType<EntityDbStringFactory>().As<EntityDbStringFactory>().SingleInstance();
            builder.RegisterType<SgcinoAPIDataLayer>().As<IApiConnectionSettings>().SingleInstance();
            builder.RegisterType<UsersBl>().As<UsersBl>().SingleInstance();
            builder.RegisterType<OrdersBl>().As<OrdersBl>().SingleInstance();
            //builder.RegisterType<TidRolloverBl>().As<TidRolloverBl>().SingleInstance();
            ////The following 2 lines are needed for Initialising Api Authentication
            builder.Register(c => new AuthApiInitialize()).As<IAuthApiInitialise>().SingleInstance();
            builder.RegisterType<AuthorizationServerProvider>().As<AuthorizationServerProvider>().SingleInstance();
        }

        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            /* Enable for additional test troubleshooting
            config.EnableSystemDiagnosticsTracing();
            SystemDiagnosticsTraceWriter traceWriter = config.EnableSystemDiagnosticsTracing();
            traceWriter.IsVerbose = false;
            traceWriter.MinimumLevel = (System.Web.Http.Tracing.TraceLevel)TraceLevel.Verbose;
            */

            var builder = new ContainerBuilder();

            // Register Web API controller in executing assembly.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            this.RegisterDependencies(app, builder);
            builder.RegisterWebApiFilterProvider(config);
            // Create and assign a dependency resolver for Web API to use.
            IContainer container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            // Register the Autofac middleware FIRST. This also adds
            // Autofac-injected middleware registered with the container.
            app.UseAutofacMiddleware(container);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            //The following 2 lines are needed for Initialising Api Authentication
            var authApiInitialize = container.Resolve<IAuthApiInitialise>();
            var apiSettings = container.Resolve<ISgcinoDataSettings>();
            authApiInitialize.InitialiseAuthTable(apiSettings.GetEntityDBConnectionString());

            var authProvider = container.Resolve<AuthorizationServerProvider>();

            var options = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(2),
                Provider = authProvider
            };

            app.UseOAuthAuthorizationServer(options);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );

            // Make sure the Autofac lifetime scope is passed to Web API.
            app.UseAutofacWebApi(config);
            app.UseWebApi(config);
        }
    }
}