using Microsoft.Extensions.DependencyInjection;
using System;

namespace CG.Email.Builders
{
    /// <summary>
    /// This interface represents a builder for email strategies.
    /// </summary>
    public interface IEmailStrategyBuilder
    {
        /// <summary>
        /// This property contains a collection of registered services.
        /// </summary>
        IServiceCollection Services { get; set; }
    }
}
