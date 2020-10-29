using CG.Options;
using System;
using System.ComponentModel.DataAnnotations;

namespace CG.Email.Options
{
    /// <summary>
    /// This class represents configuration options for sending emails.
    /// </summary>
    public class EmailOptions : OptionsBase
    {
        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        /// <summary>
        /// This property contains one or more email addresses, separated by
        /// commas, that represent the destination for an email.
        /// </summary>
        [Required]
        public string To { get; set; }

        /// <summary>
        /// This property contains an email address that represents the source
        /// for an email.
        /// </summary>
        [Required]
        public string From { get; set; }

        /// <summary>
        /// This property contains the subject for an email.
        /// </summary>
        [Required]
        public string Subject { get; set; }

        /// <summary>
        /// This property contains the body for an email.
        /// </summary>
        [Required]
        public string Body { get; set; }

        #endregion
    }
}
