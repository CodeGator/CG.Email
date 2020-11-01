using CG.Business.Services.Options;
using CG.Email.Strategies.Options;
using System;
using System.ComponentModel.DataAnnotations;

namespace CG.Email.Options
{
    /// <summary>
    /// This class contains options information for the <see cref="EmailService"/>
    /// class.
    /// </summary>
    public class EmailServiceOptions : ServiceOptions
    {
        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        /// <summary>
        /// This property contains the strategy for the email service.
        /// </summary>
        [Required]
        public EmailStrategyOptions Strategy { get; set; }

        #endregion

        // *******************************************************************
        // Constructors.
        // *******************************************************************

        #region Constructors

        /// <summary>
        /// This constructor creates a new instance of the <see cref="EmailServiceOptions"/>
        /// class.
        /// </summary>
        public EmailServiceOptions()
        {
            // Set default values here.
            Strategy = new EmailStrategyOptions();
        }

        #endregion
    }
}
