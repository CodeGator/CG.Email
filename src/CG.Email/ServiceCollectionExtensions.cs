using CG.Email.Builders;
using CG.Email.Options;
using CG.Email.Properties;
using CG.Reflection;
using CG.Validations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace CG.Email
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
        /// This method adds services and strategies for sending email to the
        /// specified service collection.
        /// </summary>
        /// <param name="serviceCollection">The service collection to use for 
        /// the operation.</param>
        /// <param name="configuration">the configuration to use for the operation.</param>
        /// <returns>A <see cref="IServiceCollection"/> object for building up
        /// an strategies for the service.</returns>
        /// <exception cref="ArgumentException">This exception is thrown whenever
        /// a required argument is missing or invalid.</exception>
        /// <remarks>
        /// <para>
        /// This method builds up an email service using information read from 
        /// the specified configuration. 
        /// </para>
        /// <para>
        /// Here is an example for the JSON configuration:
        /// <code language="json">
        /// {
        ///     "Email": {
        ///       "Strategy": {
        ///           "Name": "Smtp",
        ///           "Assembly": "CG.Email"
        ///         },
        ///       "Smtp": {
        ///         "ServerAddress": "localhost",
        ///         "ServerPort": 25,
        ///         "UserName": "test@whatever.com",
        ///         "Password": "big secret"
        ///     }
        ///   }
        /// }
        /// </code>
        /// </para>
        /// </remarks>
        public static IServiceCollection AddEmail(
            this IServiceCollection serviceCollection,
            IConfiguration configuration
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(serviceCollection, nameof(serviceCollection))
                .ThrowIfNull(configuration, nameof(configuration));

            // Configure the email service options.
            serviceCollection.ConfigureOptions<EmailServiceOptions>(
                configuration,
                out var emailServiceOptions
                );

            // Register the service.
            serviceCollection.AddSingleton<IEmailService, EmailService>();

            // Should we load an assembly for the strategy?
            if (!string.IsNullOrEmpty(emailServiceOptions.Strategy.Assembly))
            {
                // Load the assembly for the strategy. 
                _ = Assembly.Load(
                    emailServiceOptions.Strategy.Assembly
                    );
            }

            // Build the name of an extension method for this strategy.
            var methodName = $"Add{emailServiceOptions.Strategy.Name}Strategy";

            // Look for specified extension method.
            var methods = AppDomain.CurrentDomain.ExtensionMethods(
                typeof(IEmailStrategyBuilder),
                methodName,
                new Type[] { typeof(IConfiguration) }
                );

            // Did we fail to find anything?
            if (false == methods.Any())
            {
                // Panic!
                throw new MissingMethodException(
                    message: string.Format(
                        Resources.ServiceCollectionExtensions_MethodNotFound,
                        methodName
                        )
                    );
            }

            // Create the strategy builder.
            var strategyBuilder = new EmailStrategyBuilder()
            {
                Services = serviceCollection
            };

            // We'll use the first matching method.
            var method = methods.First();

            // Invoke the extension method.
            method.Invoke(
                null,
                new object[] { strategyBuilder, configuration }
                );

            // Return the service collection.
            return serviceCollection;
        }

        // *******************************************************************

        /// <summary>
        /// This method adds services and strategies for sending email to the
        /// specified service collection.
        /// </summary>
        /// <param name="serviceCollection">The service collection to use for 
        /// the operation.</param>
        /// <param name="builderAction">The delegate to use for the operation.</param>
        /// <returns>A <see cref="IServiceCollection"/> object for building up
        /// an strategies for the service.</returns>
        /// <exception cref="ArgumentException">This exception is thrown whenever
        /// a required argument is missing or invalid.</exception>
        public static IServiceCollection AddEmail(
            this IServiceCollection serviceCollection,
            Action<IEmailStrategyBuilder> builderAction
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(serviceCollection, nameof(serviceCollection))
                .ThrowIfNull(builderAction, nameof(builderAction));

            // Create the strategy builder.
            var strategyBuilder = new EmailStrategyBuilder()
            {
                Services = serviceCollection
            };

            // Register the service.
            serviceCollection.AddSingleton<IEmailService, EmailService>();

            // Allow the caller to customize the builder.
            builderAction(strategyBuilder);
            
            // Return the service collection.
            return serviceCollection;
        }

        #endregion
    }
}
