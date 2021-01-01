using CG.Email.Strategies.Smtp;
using CG.Options;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace CG.Email.Strategies.Options
{
    /// <summary>
    /// This class contains options information for the <see cref="SmtpEmailStrategy"/>
    /// class.
    /// </summary>
    public class SmtpEmailStrategyOptions : EmailStrategyOptions
    {
        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        /// <summary>
        /// This property contains the server address.
        /// </summary>
        [Required]
        public string ServerAddress { get; set; }

        /// <summary>
        /// This property contains a port number for the server.
        /// </summary>
        public int ServerPort { get; set; }

        /// <summary>
        /// This property contains a user name for the email account.
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// This property contains a password for the email account.
        /// </summary>
        [ProtectedProperty]
        public string Password { get; set; }

        /// <summary>
        /// This property contains a delivery method for the SMTP operations.
        /// </summary>
        public SmtpDeliveryMethod? DeliveryMethod { get; set; }

        /// <summary>
        /// This property contains a timeout for the SMTP operations.
        /// </summary>
        public int? Timeout { get; set; }

        /// <summary>
        /// This property indicates whether SSL should be used for SMTP operations.
        /// </summary>
        public bool EnableSSL { get; set; }

        #endregion

        // *******************************************************************
        // Constructors.
        // *******************************************************************

        #region Constructors

        /// <summary>
        /// This constructor creates a new instance of the <see cref="SmtpEmailStrategyOptions"/>
        /// class.
        /// </summary>
        public SmtpEmailStrategyOptions()
        {
            // Set default values here.
            ServerPort = 25;
        }

        #endregion
    }
}
