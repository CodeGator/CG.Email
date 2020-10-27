using CG.Business.Strategies.Options;
using System;
using System.ComponentModel.DataAnnotations;

namespace CG.Email.Options
{
    /// <summary>
    /// This class contains options information for an email strategy.
    /// </summary>
    public class EmailStrategyOptions : StrategyOptions
    {
        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        /// <summary>
        /// This property contains the strategy name.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// This property contains the strategy's assembly.
        /// </summary>
        public string Assembly { get; set; }

        #endregion

        // *******************************************************************
        // Constructors.
        // *******************************************************************

        #region Constructors

        /// <summary>
        /// This constructor creates a new instance of the <see cref="EmailStrategyOptions"/>
        /// class.
        /// </summary>
        public EmailStrategyOptions()
        {
            // Set default values here.
            Name = "Smtp";
        }

        #endregion
    }
}
