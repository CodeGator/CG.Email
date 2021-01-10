using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CG.Email.QuickStart
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(
            IConfiguration configuration
            )
        {
            Configuration = configuration;
        }

        public void ConfigureServices(
            IServiceCollection services
            )
        {
            // I'm in the process of trying to solve an issue where
            //   the email service/strategy registrations disappear
            //   after we add them - hence, the duplicate call here.

            services.AddEmail(
                Configuration.GetSection("Services:Email"),
                ServiceLifetime.Singleton
                );

            services.AddEmail(
                Configuration.GetSection("Services:Email"),
                ServiceLifetime.Singleton
                );
        }

        public void Configure(
            IApplicationBuilder app, 
            IWebHostEnvironment env
            )
        {
            app.UseEmail(
                env,
                "Services:Email"
                );
        }
    }
}
