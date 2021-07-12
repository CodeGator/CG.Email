using CG.Email.Strategies.Options;
using CG.Validations;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace CG.Email.Strategies.Smtp
{
    /// <summary>
    /// This class is an SMTP implementation of <see cref="IEmailStrategy{TOptions}"/>
    /// </summary>
    public class SmtpEmailStrategy : 
        EmailStrategyBase<SmtpEmailStrategyOptions>, 
        IEmailStrategy<SmtpEmailStrategyOptions>
    {
        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        /// <summary>
        /// This property contains a reference to an SMTP client.
        /// </summary>
        protected SmtpClient Client { get; private set; }

        #endregion

        // *******************************************************************
        // Constructors.
        // *******************************************************************

        #region Constructors

        /// <summary>
        /// This constructor creates a new instance of the <see cref="SmtpEmailStrategy"/>
        /// class.
        /// </summary>
        /// <param name="options">The options to use for the operation.</param>
        /// <param name="client">The SMTP client to use for the operation.</param>
        public SmtpEmailStrategy(
            IOptions<SmtpEmailStrategyOptions> options,
            SmtpClient client
            ) : base(options)
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(client, nameof(client));

            // Save the references.
            Client = client;
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
        /// <param name="token">A cancellation token.</param>
        /// <returns>A task to perform the operation, that returns an <see cref="EmailResult"/>
        /// object, representing the results of the operation.</returns>
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
            // Create a new mail message.
            var message = BuildAMessage(
                fromAddress,
                toAddresses,
                ccAddresses,
                bccAddresses,
                attachments,
                subject,
                body,
                bodyIsHtml
                );

            // Send the message.
            Client.Send(message);

            // Create a dummy result since SMTP doesn't give us a real one.
            var retValue = new EmailResult() 
            { 
                EmailId = $"{Guid.NewGuid():N}"
            };

            // Return the result.
            return Task.FromResult(retValue);
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
                // Cleanup the client.
                Client?.Dispose();
                Client = null;
            }

            // Give the base class a chance.
            base.Dispose(disposing);
        }

        #endregion

        // *******************************************************************
        // Private methods.
        // *******************************************************************

        #region Private methods

        /// <summary>
        /// This method builds up a <see cref="MailMessage"/> and
        /// returns it.
        /// </summary>
        /// <param name="fromAddress">The from address to use for the operation.</param>
        /// <param name="toAddresses">The to addresses to use for the operation.</param>
        /// <param name="ccAddresses">The CC addresses to use for the operation.</param>
        /// <param name="bccAddresses">The BCC addresses to use for the operation.</param>
        /// <param name="attachments">The attachments to use for the operation.</param>
        /// <param name="subject">The subject to use for the operation.</param>
        /// <param name="body">The body to use for the operation.</param>
        /// <param name="bodyIsHtml">True if the body contains HTML; False otherwise.</param>
        /// <returns>A populated <see cref="MailMessage"/> object.</returns>
        private MailMessage BuildAMessage(
            string fromAddress,
            IEnumerable<string> toAddresses,
            IEnumerable<string> ccAddresses,
            IEnumerable<string> bccAddresses,
            IEnumerable<string> attachments,
            string subject,
            string body,
            bool bodyIsHtml
            )
        {
            // Create a new mail message.
            var message = new MailMessage()
            {
                From = new MailAddress(fromAddress)
            };

            // Loop and add the TO addresses.
            foreach (var toAddress in toAddresses)
            {
                message.To.Add(new MailAddress(toAddress.Trim()));
            }

            // Loop and add the CC addresses.
            foreach (var ccAddress in ccAddresses)
            {
                message.CC.Add(new MailAddress(ccAddress.Trim()));
            }

            // Loop and add the BCC addresses.
            foreach (var bccAddress in bccAddresses)
            {
                message.Bcc.Add(new MailAddress(bccAddress.Trim()));
            }

            // Loop and add the attachments.
            foreach (string attachment in attachments)
            {
                message.Attachments.Add(new Attachment(attachment));
            }

            // Set the subject.
            message.Subject = subject;

            // Set the body.
            message.Body = body;

            // Is the body HTML?
            message.IsBodyHtml = bodyIsHtml;

            // Return the message.
            return message;
        }

        #endregion
    }
}
