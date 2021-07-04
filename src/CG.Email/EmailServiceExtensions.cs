using CG.Validations;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CG.Email
{
    /// <summary>
    /// This class contains extension methods related to the <see cref="IEmailService"/>
    /// type.
    /// </summary>
    public static partial class EmailServiceExtensions
    {
        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <summary>
        /// This method sends an email.
        /// </summary>
        /// <param name="service">The service to use for the operation.</param>
        /// <param name="fromAddress">The from address to use for the operation.</param>
        /// <param name="toAddresses">The to addresses to use for the operation.</param>
        /// <param name="ccAddresses">The CC addresses to use for the operation.</param>
        /// <param name="bccAddresses">The BCC addresses to use for the operation.</param>
        /// <param name="attachments">The attachments to use for the operation.</param>
        /// <param name="subject">The subject to use for the operation.</param>
        /// <param name="body">The body to use for the operation.</param>
        /// <param name="bodyIsHtml">True is the body contains HTML; False otherwise.</param>
        /// <returns>A task to perform the operation, that returns an <see cref="EmailResult"/>
        /// object, representing the results of the operation.</returns>
        public static EmailResult Send(
            this IEmailService service,
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
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(service, nameof(service));

            // Send the email.
            return service.SendAsync(
                fromAddress,
                toAddresses,
                ccAddresses,
                bccAddresses,
                attachments,
                subject,
                body,
                bodyIsHtml
                ).Result;
        }

        // *******************************************************************

        /// <summary>
        /// This method sends an email.
        /// </summary>
        /// <param name="service">The service object to use for the operation.</param>
        /// <param name="fromAddress">The from address to use for the operation.</param>
        /// <param name="toAddress">The to address to use for the operation.</param>
        /// <param name="subject">The subject to use for the operation.</param>
        /// <param name="body">The body to use for the operation.</param>
        /// <param name="bodyIsHtml">True if the body contains HTML; False otherwise.</param>
        /// <returns>A task to perform the operation, that returns an <see cref="EmailResult"/>
        /// object, representing the results of the operation.</returns>
        public static EmailResult Send(
            this IEmailService service,
            string fromAddress,
            string toAddress,
            string subject,
            string body,
            bool bodyIsHtml = false
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(service, nameof(service))
                .ThrowIfNullOrEmpty(fromAddress, nameof(fromAddress));

            // Send the email.
            return service.Send(
                fromAddress,
                toAddress.Split(','),
                new string[0],
                new string[0],
                new string[0],
                subject,
                body,
                bodyIsHtml
                );
        }

        // *******************************************************************

        /// <summary>
        /// This method sends an email.
        /// </summary>
        /// <param name="service">The service object to use for the operation.</param>
        /// <param name="fromAddress">The from address to use for the operation.</param>
        /// <param name="toAddress">The to address to use for the operation.</param>
        /// <param name="subject">The subject to use for the operation.</param>
        /// <param name="body">The body to use for the operation.</param>
        /// <param name="bodyIsHtml">True if the body contains HTML; False otherwise.</param>
        /// <param name="token">An optional cancellation token.</param>
        /// <returns>A task to perform the operation, that returns an <see cref="EmailResult"/>
        /// object, representing the results of the operation.</returns>
        public static async Task<EmailResult> SendAsync(
            this IEmailService service,
            string fromAddress,
            string toAddress,
            string subject,
            string body,
            bool bodyIsHtml = false,
            CancellationToken token = default(CancellationToken)
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(service, nameof(service))
                .ThrowIfNullOrEmpty(toAddress, nameof(toAddress));

            // Send the email.
            return await service.SendAsync(
                fromAddress,
                toAddress.Split(','),
                new string[0],
                new string[0],
                new string[0],
                subject,
                body,
                bodyIsHtml
                ).ConfigureAwait(false);
        }

        // *******************************************************************

        /// <summary>
        /// This method sends an email.
        /// </summary>
        /// <param name="service">The service object to use for the operation.</param>
        /// <param name="fromAddress">The from address to use for the operation.</param>
        /// <param name="toAddress">The to address to use for the operation.</param>
        /// <param name="ccAddress">The cc address to use for the operation.</param>
        /// <param name="subject">The subject to use for the operation.</param>
        /// <param name="body">The body to use for the operation.</param>
        /// <param name="bodyIsHtml">True if the body contains HTML; False otherwise.</param>
        /// <returns>A task to perform the operation, that returns an <see cref="EmailResult"/>
        /// object, representing the results of the operation.</returns>
        public static EmailResult Send(
            this IEmailService service,
            string fromAddress,
            string toAddress,
            string ccAddress,
            string subject,
            string body,
            bool bodyIsHtml = false
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(service, nameof(service))
                .ThrowIfNullOrEmpty(toAddress, nameof(toAddress))
                .ThrowIfNullOrEmpty(ccAddress, nameof(ccAddress));

            // Send the email.
            return service.Send(
                fromAddress,
                toAddress.Split(','),
                ccAddress.Split(','),
                new string[0],
                new string[0],
                subject,
                body,
                bodyIsHtml
                );
        }

        // *******************************************************************

        /// <summary>
        /// This method sends an email.
        /// </summary>
        /// <param name="service">The service object to use for the operation.</param>
        /// <param name="fromAddress">The from address to use for the operation.</param>
        /// <param name="toAddress">The to address to use for the operation.</param>
        /// <param name="ccAddress">The cc address to use for the operation.</param>
        /// <param name="subject">The subject to use for the operation.</param>
        /// <param name="body">The body to use for the operation.</param>
        /// <param name="bodyIsHtml">True if the body contains HTML; False otherwise.</param>
        /// <param name="token">An optional cancellation token.</param>
        /// <returns>A task to perform the operation, that returns an <see cref="EmailResult"/>
        /// object, representing the results of the operation.</returns>
        public static async Task<EmailResult> SendAsync(
            this IEmailService service,
            string fromAddress,
            string toAddress,
            string ccAddress,
            string subject,
            string body,
            bool bodyIsHtml = false,
            CancellationToken token = default(CancellationToken)
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(service, nameof(service))
                .ThrowIfNullOrEmpty(toAddress, nameof(toAddress))
                .ThrowIfNullOrEmpty(ccAddress, nameof(ccAddress));

            // Send the email.
            return await service.SendAsync(
                fromAddress,
                toAddress.Split(','),
                ccAddress.Split(','),
                new string[0],
                new string[0],
                subject,
                body,
                bodyIsHtml,
                token
                ).ConfigureAwait(false);
        }

        // *******************************************************************

        /// <summary>
        /// This method sends an email.
        /// </summary>
        /// <param name="service">The service object to use for the operation.</param>
        /// <param name="fromAddress">The from address to use for the operation.</param>
        /// <param name="toAddress">The to address to use for the operation.</param>
        /// <param name="ccAddress">The cc address to use for the operation.</param>
        /// <param name="bccAddress">The cc address to use for the operation.</param>
        /// <param name="subject">The subject to use for the operation.</param>
        /// <param name="body">The body to use for the operation.</param>
        /// <param name="bodyIsHtml">True if the body contains HTML; False otherwise.</param>
        /// <returns>A task to perform the operation, that returns an <see cref="EmailResult"/>
        /// object, representing the results of the operation.</returns>
        public static EmailResult Send(
            this IEmailService service,
            string fromAddress,
            string toAddress,
            string ccAddress,
            string bccAddress,
            string subject,
            string body,
            bool bodyIsHtml = false
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(service, nameof(service))
                .ThrowIfNullOrEmpty(toAddress, nameof(toAddress))
                .ThrowIfNullOrEmpty(ccAddress, nameof(ccAddress))
                .ThrowIfNullOrEmpty(bccAddress, nameof(bccAddress));

            // Send the email.
            return service.Send(
                fromAddress,
                toAddress.Split(','),
                ccAddress.Split(','),
                bccAddress.Split(','),
                new string[0],
                subject,
                body,
                bodyIsHtml
                );
        }

        // *******************************************************************

        /// <summary>
        /// This method sends an email.
        /// </summary>
        /// <param name="service">The service object to use for the operation.</param>
        /// <param name="fromAddress">The from address to use for the operation.</param>
        /// <param name="toAddress">The to address to use for the operation.</param>
        /// <param name="ccAddress">The cc address to use for the operation.</param>
        /// <param name="bccAddress">The bcc address to use for the operation.</param>
        /// <param name="subject">The subject to use for the operation.</param>
        /// <param name="body">The body to use for the operation.</param>
        /// <param name="bodyIsHtml">True if the body contains HTML; False otherwise.</param>
        /// <param name="token">An optional cancellation token.</param>
        /// <returns>A task to perform the operation, that returns an <see cref="EmailResult"/>
        /// object, representing the results of the operation.</returns>
        public static async Task<EmailResult> SendAsync(
            this IEmailService service,
            string fromAddress,
            string toAddress,
            string ccAddress,
            string bccAddress,
            string subject,
            string body,
            bool bodyIsHtml = false,
            CancellationToken token = default(CancellationToken)
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(service, nameof(service))
                .ThrowIfNullOrEmpty(toAddress, nameof(toAddress))
                .ThrowIfNullOrEmpty(ccAddress, nameof(ccAddress))
                .ThrowIfNullOrEmpty(bccAddress, nameof(bccAddress));

            // Send the email.
            return await service.SendAsync(
                fromAddress,
                toAddress.Split(','),
                ccAddress.Split(','),
                bccAddress.Split(','),
                new string[0],
                subject,
                body,
                bodyIsHtml,
                token
                ).ConfigureAwait(false);
        }

        // *******************************************************************

        /// <summary>
        /// This method sends an email.
        /// </summary>
        /// <param name="service">The service object to use for the operation.</param>
        /// <param name="fromAddress">The from address to use for the operation.</param>
        /// <param name="toAddress">The to address to use for the operation.</param>
        /// <param name="ccAddress">The cc address to use for the operation.</param>
        /// <param name="bccAddress">The cc address to use for the operation.</param>
        /// <param name="attachment">The attachment to use for the operation.</param>
        /// <param name="subject">The subject to use for the operation.</param>
        /// <param name="body">The body to use for the operation.</param>
        /// <param name="bodyIsHtml">True if the body contains HTML; False otherwise.</param>
        /// <returns>A task to perform the operation, that returns an <see cref="EmailResult"/>
        /// object, representing the results of the operation.</returns>
        public static EmailResult Send(
            this IEmailService service,
            string fromAddress,
            string toAddress,
            string ccAddress,
            string bccAddress,
            string attachment,
            string subject,
            string body,
            bool bodyIsHtml = false
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(service, nameof(service))
                .ThrowIfNullOrEmpty(toAddress, nameof(toAddress))
                .ThrowIfNullOrEmpty(ccAddress, nameof(ccAddress))
                .ThrowIfNullOrEmpty(bccAddress, nameof(bccAddress))
                .ThrowIfNullOrEmpty(attachment, nameof(attachment));

            // Send the email.
            return service.Send(
                fromAddress,
                toAddress.Split(','),
                ccAddress.Split(','),
                bccAddress.Split(','),
                attachment.Split(','),
                subject,
                body,
                bodyIsHtml
                );
        }

        // *******************************************************************

        /// <summary>
        /// This method sends an email.
        /// </summary>
        /// <param name="service">The service object to use for the operation.</param>
        /// <param name="fromAddress">The from address to use for the operation.</param>
        /// <param name="toAddress">The to address to use for the operation.</param>
        /// <param name="ccAddress">The cc address to use for the operation.</param>
        /// <param name="bccAddress">The bcc address to use for the operation.</param>
        /// <param name="attachment">The attachment to use for the operation.</param>
        /// <param name="subject">The subject to use for the operation.</param>
        /// <param name="body">The body to use for the operation.</param>
        /// <param name="bodyIsHtml">True if the body contains HTML; False otherwise.</param>
        /// <param name="token">An optional cancellation token.</param>
        /// <returns>A task to perform the operation, that returns an <see cref="EmailResult"/>
        /// object, representing the results of the operation.</returns>
        public static async Task<EmailResult> SendAsync(
            this IEmailService service,
            string fromAddress,
            string toAddress,
            string ccAddress,
            string bccAddress,
            string attachment,
            string subject,
            string body,
            bool bodyIsHtml = false,
            CancellationToken token = default(CancellationToken)
            )
        {
            // Validate the parameters before attempting to use them.
            Guard.Instance().ThrowIfNull(service, nameof(service))
                .ThrowIfNullOrEmpty(fromAddress, nameof(fromAddress))
                .ThrowIfNullOrEmpty(toAddress, nameof(toAddress))
                .ThrowIfNullOrEmpty(ccAddress, nameof(ccAddress))
                .ThrowIfNullOrEmpty(bccAddress, nameof(bccAddress))
                .ThrowIfNullOrEmpty(attachment, nameof(attachment));

            // Send the email.
            return await service.SendAsync(
                fromAddress,
                toAddress.Split(','),
                ccAddress.Split(','),
                bccAddress.Split(','),
                attachment.Split(','),
                subject,
                body,
                bodyIsHtml
                ).ConfigureAwait(false);
        }

        #endregion
    }
}
