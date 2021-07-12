using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CG.Email.Strategies.DoNothing
{
    /// <summary>
    /// This class is a "do nothing" implementation of <see cref="IEmailStrategy{TOptions}"/>
    /// </summary>
    public class DoNothingEmailStrategy : 
        EmailStrategyBase,
        IEmailStrategy
    {
        // *******************************************************************
        // Constructors.
        // *******************************************************************

        #region Constructors

        /// <summary>
        /// This constructor creates a new instance of the <see cref="DoNothingEmailStrategy"/>
        /// class.
        /// </summary>
        public DoNothingEmailStrategy()
        {

        }

        #endregion

        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <inheritdoc/>
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
            // Create a dummy result since we doesn't actually send anything.
            var retValue = new EmailResult()
            {
                EmailId = $"{Guid.NewGuid():N}"
            };

            // Return the result.
            return Task.FromResult(retValue);
        }

        #endregion
    }
}
