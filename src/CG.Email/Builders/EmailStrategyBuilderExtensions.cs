using CG.Email.Strategies;
using CG.Validations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CG.Email.Builders
{
    /// <summary>
    /// This class contains extension methods related to the <see cref="IEmailStrategyBuilder"/>
    /// type.
    /// </summary>
    public static partial class EmailStrategyBuilderExtensions
    {
        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method registers a "do nothing" strategy for sending emails.
        /// </summary>
        /// <param name="emailStrategyBuilder">The builder to use for the operation.</param>
        /// <param name="configuration">The configuration to use for the operation.</param>
        /// <returns>The value of the <paramref name="emailStrategyBuilder"/>
        /// parameter, for chaining calls together.</returns>
        /// <exception cref="ArgumentException">This exception is thrown whenever
        /// a required argument is missing or invalid.</exception>
        public static IEmailStrategyBuilder AddDoNothingStrategy(
            this IEmailStrategyBuilder emailStrategyBuilder,
            IConfiguration configuration
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(emailStrategyBuilder, nameof(emailStrategyBuilder))
                .ThrowIfNull(configuration, nameof(configuration));

            // Register the strategy.
            emailStrategyBuilder.Services.AddSingleton<IEmailStrategy, DoNothingEmailStrategy>();

            // Return the strategy builder.
            return emailStrategyBuilder;
        }

        // *******************************************************************

        /// <summary>
        /// This method registers an SMTP strategy for sending emails.
        /// </summary>
        /// <param name="emailStrategyBuilder">The builder to use for the operation.</param>
        /// <param name="configuration">The configuration to use for the operation.</param>
        /// <returns>The value of the <paramref name="emailStrategyBuilder"/>
        /// parameter, for chaining calls together.</returns>
        /// <exception cref="ArgumentException">This exception is thrown whenever
        /// a required argument is missing or invalid.</exception>
        public static IEmailStrategyBuilder AddSmtpStrategy(
            this IEmailStrategyBuilder emailStrategyBuilder,
            IConfiguration configuration
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(emailStrategyBuilder, nameof(emailStrategyBuilder))
                .ThrowIfNull(configuration, nameof(configuration));

            // Configure the strategy options.
            emailStrategyBuilder.Services.ConfigureOptions<SmtpEmailStrategyOptions>(
                configuration.GetSection("Smtp")
                );

            // Register the strategy.
            emailStrategyBuilder.Services.AddSingleton<IEmailStrategy, SmtpEmailStrategy>();

            // Return the strategy builder.
            return emailStrategyBuilder;
        }

        // *******************************************************************

        /// <summary>
        /// This method registers an SMTP strategy for sending emails.
        /// </summary>
        /// <param name="emailStrategyBuilder">The builder to use for the operation.</param>
        /// <param name="optionsAction">The options delegate to use for the operation.</param>
        /// <returns>The value of the <paramref name="emailStrategyBuilder"/>
        /// parameter, for chaining calls together.</returns>
        /// <exception cref="ArgumentException">This exception is thrown whenever
        /// a required argument is missing or invalid.</exception>
        public static IEmailStrategyBuilder AddSmtpStrategy(
            this IEmailStrategyBuilder emailStrategyBuilder,
            Action<SmtpEmailStrategyOptions> optionsAction
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(emailStrategyBuilder, nameof(emailStrategyBuilder))
                .ThrowIfNull(optionsAction, nameof(optionsAction));

            // Create the options.
            var options = new SmtpEmailStrategyOptions();

            // Allow the caller to manipulate the options.
            optionsAction(options);

            // Configure the strategy options.
            emailStrategyBuilder.Services.ConfigureOptions<SmtpEmailStrategyOptions>(
                options
                );

            // Register the strategy.
            emailStrategyBuilder.Services.AddSingleton<IEmailStrategy, SmtpEmailStrategy>();

            // Return the strategy builder.
            return emailStrategyBuilder;
        }

        #endregion
    }
}
