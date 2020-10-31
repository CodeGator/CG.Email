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
                .AddEmail()
                .Build()
                .RunDelegate(host =>
                {
                    var email = host.Services.GetRequiredService<IEmailService>();

                    // Change the addresses to something that works for you...
                    //email.Send("from@noplace.biz", "to@noplace.biz", "test email", "this is a test");

                    Console.WriteLine("Hello World!");
                });            
        }
    }
}
