using CG.Business.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CG.Email
{
    /// <summary>
    /// This interface represents an object that sends emails.
    /// </summary>
    public interface IEmailService : IService
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
        /// <param name="token">An optional cancellation token.</param>
        /// <returns>A task to perform the operation, that returns an <see cref="EmailResult"/>
        /// object, representing the results of the operation.</returns>
        /// <exception cref="EmailException">This exception is thrown whenever the operation
        /// fails, for any reason.</exception>
        Task<EmailResult> SendAsync(
            string fromAddress,
            IEnumerable<string> toAddresses,
            IEnumerable<string> ccAddresses,
            IEnumerable<string> bccAddresses,
            IEnumerable<string> attachments,
            string subject,
            string body,
            bool bodyIsHtml = false,
            CancellationToken token = default(CancellationToken)
            );
    }
}
