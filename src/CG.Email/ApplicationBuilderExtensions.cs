using CG.Validations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System;

namespace CG.Email
{
    /// <summary>
    /// This class contains extension methods related to the <see cref="IApplicationBuilder"/>
    /// type.
    /// </summary>
    public static partial class ApplicationBuilderExtensions
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
        /// <returns>Ther value of the <paramref name="applicationBuilder"/>
        /// parameter, for chaining calls together.</returns>
        public static IApplicationBuilder UseEmail(
            this IApplicationBuilder applicationBuilder,
            IWebHostEnvironment hostEnvironment
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(applicationBuilder, nameof(applicationBuilder))
                .ThrowIfNull(hostEnvironment, nameof(hostEnvironment));

            // Call the use method for the strategy.
            applicationBuilder.UseStrategies(hostEnvironment);

            // Return the applicationn builder.
            return applicationBuilder;
        }

        #endregion
    }
}
