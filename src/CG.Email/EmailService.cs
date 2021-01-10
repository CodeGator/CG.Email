using CG.Business.Services;
using CG.Email.Properties;
using CG.Email.Strategies;
using CG.Validations;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CG.Email
{
    /// <summary>
    /// This class is an implementation of <see cref="IEmailService"/>
    /// </summary>
    public class EmailService : ServiceBase, IEmailService
    {
        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        /// <summary>
        /// This property contains a reference to an email strategy.
        /// </summary>
        private IEmailStrategy EmailStrategy { get; }

        #endregion

        // *******************************************************************
        // Constructors.
        // *******************************************************************

        #region Constructors

        /// <summary>
        /// This constructor creates a new instance of the <see cref="EmailService"/>
        /// class.
        /// </summary>
        /// <param name="emailStrategy">A reference to an email strategy.</param>
        public EmailService(
            IEmailStrategy emailStrategy
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(emailStrategy, nameof(emailStrategy));

            // Save the reference.
            EmailStrategy = emailStrategy;
        }

        #endregion

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
        /// <param name="token">An optional cancellation token.</param>
        /// <returns>A task to perform the operation.</returns>
        public async Task<IEnumerable<EmailResult>> SendAsync(
            string fromAddress,
            IEnumerable<string> toAddresses,
            IEnumerable<string> ccAddresses,
            IEnumerable<string> bccAddresses,
            IEnumerable<string> attachments,
            string subject,
            string body,
            bool bodyIsHtml = false,
            CancellationToken token = default(CancellationToken)
            )
        {
            // Validate the parameters before attempting to us them.
            Guard.Instance().ThrowIfNullOrEmpty(fromAddress, nameof(fromAddress))
                .ThrowIfNull(toAddresses, nameof(toAddresses))
                .ThrowIfNull(ccAddresses, nameof(ccAddresses))
                .ThrowIfNull(bccAddresses, nameof(bccAddresses))
                .ThrowIfNull(attachments, nameof(attachments));

            try
            {
                // Defer to the strategy.
                var retValue = await EmailStrategy.SendAsync(
                    fromAddress,
                    toAddresses,
                    ccAddresses,
                    bccAddresses,
                    attachments,
                    subject,
                    body,
                    bodyIsHtml,
                    token
                    ).ConfigureAwait(false);

                // Return the results.
                return retValue;
            }
            catch (Exception ex)
            {
                // Wrap the exception.
                throw new EmailException(
                    message: string.Format(
                        Resources.EmailService_SendAsync,
                        EmailStrategy.GetType().Name
                        ),
                    innerException: ex
                    );
            }
        }

        #endregion

        // *******************************************************************
        // Protected methods.
        // *******************************************************************

        #region Protected methods

        /// <summary>
        /// This method is called when the object is disposed.
        /// </summary>
        /// <param name="disposing">True to dispose of managed resources; false
        /// otherwise.</param>
        protected override void Dispose(bool disposing)
        {
            // Should we cleanup maanged resources?
            if (disposing)
            {
                // Cleanup the strategy.
                EmailStrategy?.Dispose();
            }

            // Give the base class a chance.
            base.Dispose(disposing);
        }

        #endregion
    }
}
