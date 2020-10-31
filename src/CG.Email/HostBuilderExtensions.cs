using CG.Email.Builders;
using CG.Validations;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;

namespace CG.Email
{
    /// <summary>
    /// This class contains extension methods related to the <see cref="IHostBuilder"/>
    /// type.
    /// </summary>
    public static partial class HostBuilderExtensions
    {
        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method adds a standard email service to the specified host 
        /// builder.
        /// </summary>
        /// <param name="hostBuilder">The host builder to use for the operation.</param>
        /// <returns>The value of the <paramref name="hostBuilder"/> parameter, 
        /// for chaining calls together.</returns>
        /// <exception cref="ArgumentException">This exception is thrown whenever
        /// one or more of the required parameters is missing or invalid.</exception>
        /// <remarks>
        /// <para>
        /// The idea with this method, is to create a quick and easy way to 
        /// setup email services for a hosted application by following a simple
        /// configuration convention. That convention assumes a configuration section
        /// exists like this:
        /// <code language="json">
        /// {
        ///    "Services" : {
        ///       "Email": {
        ///          "Strategy" : "Smtp",
        ///          "Assembly" : "",
        ///          "Smtp": {
        ///          "ServerAddress": "localhost",
        ///          "ServerPort": 25,
        ///          "UserName": "me@mine.biz",
        ///          "Password": "locally encrypted password"
        ///          }
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
        /// options, and is only needed when the strategy is located in an external assembly 
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
        /// which is free, open source, and is located here: https://github.com/CodeGator/CG.Tools.QuickCrypto
        /// </para>
        /// </remarks>
        public static IHostBuilder AddEmail(
            this IHostBuilder hostBuilder
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(hostBuilder, nameof(hostBuilder));

            // Setup email.
            hostBuilder.ConfigureServices((hostBuilderContext, serviceCollection) =>
            {
                // Get the configuration section from the standard location.
                var section = hostBuilderContext.Configuration.GetSection(
                    "Services:Email"
                    );

                // Does the section have anything in it?
                if (section.GetChildren().Any())
                {
                    // If we get here then we can setup an email service using 
                    //   whatever happens to be in the configutation section.

                    // Use the section to configure the email service.
                    serviceCollection.AddEmail(section);
                }
                else
                {
                    // If we get here then nothing was configured in the standard
                    //   location for an email service. Now, unlike services such 
                    //   as logging, we can't define "defaults" here, for email,
                    //   that will always work. So, we'll have to settle for 
                    //   registering an email service with a "do nothing" strategy,
                    //   so that we don't force everything to constantly check 
                    //   whether an email service exists, or not.

                    // Setup a standard email placeholder.
                    serviceCollection.AddEmail(options =>
                    {
                        options.AddDoNothingStrategy(section);
                    });
                }
            });

            // Return the builder.
            return hostBuilder;
        }

        #endregion
    }
}
