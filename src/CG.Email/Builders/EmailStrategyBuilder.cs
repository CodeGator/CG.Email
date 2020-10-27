using Microsoft.Extensions.DependencyInjection;
using System;

namespace CG.Email.Builders
{
    /// <summary>
    /// This class is a default implementation of the <see cref="IEmailStrategyBuilder"/>
    /// interface.
    /// </summary>
    internal class EmailStrategyBuilder : IEmailStrategyBuilder
    {
        /// <inheritdoc/>
        public IServiceCollection Services { get; set; }
    }
}
