using CG.DataProtection;
using CG.Email.Strategies.Options;
using CG.Validations;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;

namespace CG.Email.Strategies.Smtp
{
    /// <summary>
    /// This class contains extension methods related to the <see cref="IServiceCollection"/>
    /// type.
    /// </summary>
    public static partial class ServiceCollectionExtensions
    {
        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method registers the <see cref="SmtpEmailStrategy"/> strategy
        /// with the specified service collection.
        /// </summary>
        /// <param name="serviceCollection">The service collection to use for
        /// the operation.</param>
        /// <param name="configuration">The configuration to use for the operation.</param>
        /// <param name="serviceLifetime">The service lifetime to use for the operation.</param>
        /// <returns>The value of the <paramref name="serviceCollection"/> 
        /// parameter, for chaining calls together.</returns>
        public static IServiceCollection AddSmtpStrategies(
            this IServiceCollection serviceCollection,
            IConfiguration configuration,
            ServiceLifetime serviceLifetime = ServiceLifetime.Scoped
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(serviceCollection, nameof(serviceCollection))
                .ThrowIfNull(configuration, nameof(configuration));

            // Configure the strategy options.
            serviceCollection.ConfigureOptions<SmtpEmailStrategyOptions>(
                DataProtector.Instance(), // <-- default data protector.
                configuration
                );

            // Create a common delegate for the SMTP client.
            Func<IServiceProvider, SmtpClient> CCD = (serviceProvider) =>
            {
                // Get the client options.
                var options = serviceProvider
                    .GetRequiredService<IOptions<SmtpEmailStrategyOptions>>();

                // Create the SMTP client.
                var client = new SmtpClient(
                    options.Value.ServerAddress,
                    options.Value.ServerPort
                    );

                // Decide what credentials to use.
                if (!string.IsNullOrWhiteSpace(options.Value.UserName) &&
                    !string.IsNullOrWhiteSpace(options.Value.Password)
                    )
                {
                    // Don't use default credentials.
                    client.UseDefaultCredentials = false;

                    // Use these credentials instead.
                    client.Credentials = new NetworkCredential(
                        options.Value.UserName,
                        options.Value.Password
                        );
                }
                else
                {
                    // Use default credentials.
                    client.UseDefaultCredentials = true;
                }

                // Should we apply a delivery method?
                if (null != options.Value.DeliveryMethod)
                {
                    client.DeliveryMethod = options.Value.DeliveryMethod.Value;
                }

                // Should we apply a timeout?
                if (null != options.Value.Timeout)
                {
                    client.Timeout = options.Value.Timeout.Value;
                }

                // Apply SSL, or not.
                client.EnableSsl = options.Value.EnableSSL;

                // Return the SMTP client.
                return client;
            };

            // Register the strategy and the client.
            switch (serviceLifetime)
            {
                case ServiceLifetime.Scoped:
                    serviceCollection.AddScoped(serviceProvider => CCD(serviceProvider));
                    serviceCollection.AddScoped<IEmailStrategy, SmtpEmailStrategy>();
                    break;
                case ServiceLifetime.Singleton:
                    serviceCollection.AddSingleton(serviceProvider => CCD(serviceProvider));
                    serviceCollection.AddSingleton<IEmailStrategy, SmtpEmailStrategy>();
                    break;
                case ServiceLifetime.Transient:
                    serviceCollection.AddTransient(serviceProvider => CCD(serviceProvider));
                    serviceCollection.AddTransient<IEmailStrategy, SmtpEmailStrategy>();
                    break;
            }

            // Return the service collection.
            return serviceCollection;
        }

        #endregion
    }
}
