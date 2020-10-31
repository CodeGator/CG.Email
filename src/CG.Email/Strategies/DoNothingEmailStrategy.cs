using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CG.Email.Strategies
{
    /// <summary>
    /// This class is a "do nothing" implementation of <see cref="IEmailStrategy"/>
    /// </summary>
    internal class DoNothingEmailStrategy : 
        EmailStrategyBase, 
        IEmailStrategy
    {
        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

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
        /// <returns>A task to perform the operation.</returns>
        public override Task<EmailResult> SendAsync(
            string fromAddress,
            IEnumerable<string> toAddresses,
            IEnumerable<string> ccAddresses,
            IEnumerable<string> bccAddresses,
            IEnumerable<string> attachments,
            string subject,
            string body,
            bool bodyIsHtml,
            CancellationToken token
            )
        {
            // Create a dummy result since we don't actually send anthing in this 
            //   strategy.
            var retValue = new EmailResult()
            {
                EmailId = $"{Guid.NewGuid().GetHashCode()}"
            };

            // Return the result.
            return Task.FromResult(retValue);
        }

        #endregion
    }
}
