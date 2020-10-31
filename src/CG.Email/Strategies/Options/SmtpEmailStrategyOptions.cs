using CG.Email.Strategies.Options;
using CG.Options;
using System;
using System.ComponentModel.DataAnnotations;

namespace CG.Email.Strategies
{
    /// <summary>
    /// This class contains options information for the <see cref="SmtpEmailStrategy"/>
    /// class.
    /// </summary>
    public class SmtpEmailStrategyOptions : EmailStrategyOptionsBase
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
