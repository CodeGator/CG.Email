using CG.Configuration;
using CG.Email.Properties;
using CG.Email.Strategies.Options;
using CG.Validations;
using Microsoft.AspNetCore.Routing.Tree;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

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
                configuration
                );

            // Register the strategy.
            switch (serviceLifetime)
            {
                case ServiceLifetime.Scoped:
                    serviceCollection.AddScoped<IEmailStrategy, SmtpEmailStrategy>();
                    break;
                case ServiceLifetime.Singleton:
                    serviceCollection.AddSingleton<IEmailStrategy, SmtpEmailStrategy>();
                    break;
                case ServiceLifetime.Transient:
                    serviceCollection.AddTransient<IEmailStrategy, SmtpEmailStrategy>();
                    break;
            }
                                    
            // Return the service collection.
            return serviceCollection;
        }

        #endregion
    }
}
