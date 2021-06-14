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
            services.AddEmail(
                Configuration.GetSection("Email"),
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
                Configuration.GetSection("Email")
                );
        }
    }
}
