using CG.Validations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System;

namespace CG.Email
{
    /// <summary>
    /// This class contains extension methods related to the <see cref="IApplicationBuilder"/>
    /// type, for register types related to email.
    /// </summary>
    public static partial class EmailApplicationBuilderExtensions
    {
        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method wires up any startup logic required for the email service.
        /// </summary>
        /// <param name="applicationBuilder">The application builder to use for
        /// the operation.</param>
        /// <param name="hostEnvironment">The host environment to use for the 
        /// operation.</param>
        /// <param name="configurationSection">The configuration section to use
        /// for the operation.</param>
        /// <returns>Ther value of the <paramref name="applicationBuilder"/>
        /// parameter, for chaining calls together.</returns>
        public static IApplicationBuilder UseEmail(
            this IApplicationBuilder applicationBuilder,
            IWebHostEnvironment hostEnvironment,
            string configurationSection
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(applicationBuilder, nameof(applicationBuilder))
                .ThrowIfNull(hostEnvironment, nameof(hostEnvironment))
                .ThrowIfNullOrEmpty(configurationSection, nameof(configurationSection));

            // Call the use method for the strategy.
            applicationBuilder.UseStrategies(
                configurationSection
                );

            // Return the applicationn builder.
            return applicationBuilder;
        }

        #endregion
    }
}
