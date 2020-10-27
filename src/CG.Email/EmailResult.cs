using System;

namespace CG.Email
{
    /// <summary>
    /// This class represents the result of an email operation.
    /// </summary>
    public class EmailResult
    {
        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        /// <summary>
        /// This property contains a strategy specific value that represents
        /// this email operation.
        /// </summary>
        public string EmailId { get; set; }

        #endregion
    }
}
