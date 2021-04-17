using CG.Configuration;
using CG.Email;
using CG.Email.Properties;
using CG.Validations;
using Microsoft.Extensions.Configuration;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// This class contains extension methods related to the <see cref="IServiceCollection"/>
    /// type, for registering types related to email.
    /// </summary>
    public static partial class EmailServiceCollectionExtensions
    {
        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method adds services and strategies for sending email to the
        /// specified service collection.
        /// </summary>
        /// <param name="serviceCollection">The service collection to use for 
        /// the operation.</param>
        /// <param name="configuration">the configuration to use for the operation.</param>
        /// <param name="serviceLifetime">The service lifetime to use for the operation.</param>
        /// <returns>A <see cref="IServiceCollection"/> object for building up
        /// an strategies for the service.</returns>
        /// <exception cref="ArgumentException">This exception is thrown whenever
        /// a required argument is missing or invalid.</exception>
        public static IServiceCollection AddEmail(
            this IServiceCollection serviceCollection,
            IConfiguration configuration,
            ServiceLifetime serviceLifetime = ServiceLifetime.Scoped
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(serviceCollection, nameof(serviceCollection))
                .ThrowIfNull(configuration, nameof(configuration));

            // Register the service.
            switch (serviceLifetime)
            {
                case ServiceLifetime.Singleton:
                    serviceCollection.AddSingleton<IEmailService, EmailService>();
                    break;
                case ServiceLifetime.Scoped:
                    serviceCollection.AddScoped<IEmailService, EmailService>();
                    break;
                case ServiceLifetime.Transient:
                    serviceCollection.AddTransient<IEmailService, EmailService>();
                    break;
            }

            // Register the strategy(s).
            serviceCollection.AddStrategies(
                configuration,
                serviceLifetime
                );

            // Return the service collection.
            return serviceCollection;
        }

        #endregion
    }
}
