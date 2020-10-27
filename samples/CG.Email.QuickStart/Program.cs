using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace CG.Email.QuickStart
{
    class Program
    {
        static void Main(string[] args)
        {
            Host.CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    var provider = services.BuildServiceProvider();
                    var configuration = provider.GetRequiredService<IConfiguration>();
                    services.AddEmail(configuration.GetSection("Email"));
                })
                .Build()
                .RunDelegate(host =>
                {
                    var email = host.Services.GetRequiredService<IEmailService>();
                    Console.WriteLine("Hello World!");
                });            
        }
    }
}
