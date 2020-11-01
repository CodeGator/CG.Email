﻿using Microsoft.Extensions.Configuration;
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
                    try
                    {
                        var email = host.Services.GetRequiredService<IEmailService>();
                        email.Send(
                            "to@notreal.adx",
                            "from@notreal.adx",
                            "test email",
                            "this is a test"
                            );
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
                    
                });            
        }
    }
}
