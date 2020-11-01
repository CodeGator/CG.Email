using CG.Business.Strategies.Options;
using System;

namespace CG.Email.Strategies.Options
{
    /// <summary>
    /// This class represents configuration options for an email strategy.
    /// </summary>
    public class EmailStrategyOptions : StrategyOptions
    {
        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        /// <summary>
        /// This property contains a name for the strategy.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// This property contains an assembly name.
        /// </summary>
        public string Assembly { get; set; }
        #endregion
    }
}
