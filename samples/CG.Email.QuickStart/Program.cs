using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;

namespace CG.Email.QuickStart
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>(); // < -- call our startup class ...
                })
                .Build();

            try
            {
                Console.WriteLine($"Getting email service...");
                var email = host.Services.GetRequiredService<IEmailService>();

                Console.WriteLine($"About to send email...");
                var result = email.Send(
                    "to@notreal.adx",
                    "from@notreal.adx",
                    "test email",
                    "this is a test"
                    );

                Console.WriteLine($"Result: {result.First().EmailId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Press any key to exit ...");
                Console.ReadKey();
            }
        }
    }
}
