using CG.Business.Strategies;
using CG.Email.Strategies.Options;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CG.Email.Strategies
{
    /// <summary>
    /// This interface represents a strategy for sending email messages.
    /// </summary>
    public interface IEmailStrategy : IStrategy
    {
        /// <summary>
        /// This method sends an email.
        /// </summary>
        /// <param name="fromAddress">The from address to use for the operation.</param>
        /// <param name="toAddresses">The to addresses to use for the operation.</param>
        /// <param name="ccAddresses">The CC addresses to use for the operation.</param>
        /// <param name="bccAddresses">The BCC addresses to use for the operation.</param>
        /// <param name="attachments">The attachments to use for the operation.</param>
        /// <param name="subject">The subject to use for the operation.</param>
        /// <param name="body">The body to use for the operation.</param>
        /// <param name="bodyIsHtml">True if the body contains HTML; False otherwise.</param>
        /// <param name="token">A cancellation token.</param>
        /// <returns>A task to perform the operation, that returns an <see cref="EmailResult"/>
        /// object, representing the results of the operation.</returns>
        Task<EmailResult> SendAsync(
            string fromAddress,
            IEnumerable<string> toAddresses,
            IEnumerable<string> ccAddresses,
            IEnumerable<string> bccAddresses,
            IEnumerable<string> attachments,
            string subject,
            string body,
            bool bodyIsHtml,
            CancellationToken token
            );
    }


    /// <summary>
    /// This interface represents a strategy for sending email messages.
    /// </summary>
    /// <typeparam name="TOptions">The type of associated options.</typeparam>
    public interface IEmailStrategy<TOptions> : IEmailStrategy
        where TOptions : EmailStrategyOptions, new()
    {
        
    }
}
