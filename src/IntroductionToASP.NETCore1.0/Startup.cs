using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace IntroductionToASP.NETCore1._0
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .Build();

            var logfile = Path.Combine(env.ContentRootPath, "mylogfile.txt");
            
            Log.Logger = new LoggerConfiguration().WriteTo.File(logfile).CreateLogger();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            loggerFactory.AddSerilog();


            var startupLogger = loggerFactory.CreateLogger<Startup>();

            startupLogger.LogCritical("boo!");
            startupLogger.LogDebug("boo!");
            startupLogger.LogError("boo!");
            startupLogger.LogInformation("boo!");
            startupLogger.LogTrace("boo!");
            startupLogger.LogWarning("boo!");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseDirectoryBrowser();

            // Routing
            //var routeBuilder = new RouteBuilder(app);
            //routeBuilder.MapGet("", context => context.Response.WriteAsync("Hello from Routing"));
            //routeBuilder.MapGet("maria", context => context.Response.WriteAsync("Hello from Maria's Routing"));
            //routeBuilder.MapGet("scott/foo", context => context.Response.WriteAsync("Hello from Scott's Routing"));
            //routeBuilder.MapGet("post/{postNumber:int}", context => context.Response.WriteAsync($"Blog post id: {context.GetRouteValue("postNumber")}"));
            //app.UseRouter(routeBuilder.Build());            
            //
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World CATCH ALL");
            //});

            app.UseMvc(routes =>
            {
            routes.MapRoute(
                name: "default",
                template: "{controller=Home}/{action=Index}/{id?}");
            });

        }
    }
}
