using CG.Email;
using CG.Email.Builders;
using CG.Email.Options;
using CG.Email.Properties;
using CG.Reflection;
using CG.Validations;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
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
        ///       }
        ///    }
        /// }
        /// </code>
        /// </para>
        /// <para>
        /// Let's break it down. The <c>Services</c> section is where ALL services for the 
        /// application should be configured. If you're adding a CodeGator service to your
        /// application then you'll want to add that service to this section in order to 
        /// configure the service at startup.
        /// </para>
        /// <para>
        /// Under <c>Services</c>, the <c>Email</c> section is where the CodeGator email service 
        /// should be configured. This section contains at least two nodes: <c>Strategy</c> and 
        /// <c>Assembly</c>. The <c>Strategy</c> node tells the host what strategy to load
        /// for the email service, and as such, is required. The <c>Assembly</c> section is 
        /// optional, and is only needed when the strategy is located in an external assembly 
        /// that should be dynamically loaded at startup.
        /// </para>
        /// <para>
        /// The <c>Smtp</c> section is an example of a strategy section. This will vary, of
        /// course, depending on which strategy is named in the <c>Strategy</c> node. In this
        /// case, we see a made up example for an SMTP email strategy. One thing to note is, 
        /// the <c>Smtp</c> strategy expects a locally encrypted value in the <c>Password</c>
        /// node. Placing unencrypted text here will cause a runtime error.
        /// </para>
        /// <para>
        /// To locally encrypt for configuration, consider using the <c>QuickCrypt</c> tool,
        /// which is free, open source, and is located here: 
        /// <see cref="https://github.com/CodeGator/CG.Tools.QuickCrypto"/> 
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

            var methodName = "";

            // Watch for a missing or empty strategy name.
            if (string.IsNullOrEmpty(emailServiceOptions.Strategy.Name))
            {
                // Just so we have an extension method to call.
                methodName = $"AddDoNothingStrategy";
            }
            else
            {
                // Format the name of the extension method.
                methodName = $"Add{emailServiceOptions.Strategy.Name}Strategy";
            }

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
