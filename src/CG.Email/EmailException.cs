using System;
using System.Runtime.Serialization;

namespace CG.Email
{
    /// <summary>
    /// This class represents an email related exception.
    /// </summary>
    [Serializable]
    public class EmailException : Exception
    {
        // *******************************************************************
        // Constructors.
        // *******************************************************************

        #region Constructors

        /// <summary>
        /// This constructor creates a new instance of the <see cref="EmailException"/>
        /// class.
        /// </summary>
        public EmailException()
        {

        }

        // *******************************************************************

        /// <summary>
        /// This constructor creates a new instance of the <see cref="EmailException"/>
        /// class.
        /// </summary>
        /// <param name="message">The message to use for the exception.</param>
        /// <param name="innerException">An optional inner exception reference.</param>
        public EmailException(
            string message,
            Exception innerException
            ) : base(message, innerException)
        {

        }

        // *******************************************************************

        /// <summary>
        /// This constructor creates a new instance of the <see cref="EmailException"/>
        /// class.
        /// </summary>
        /// <param name="message">The message to use for the exception.</param>
        public EmailException(
            string message
            ) : base(message)
        {

        }

        // *******************************************************************

        /// <summary>
        /// This constructor creates a new instance of the <see cref="EmailException"/>
        /// class.
        /// </summary>
        /// <param name="info">The serialization info to use for the exception.</param>
        /// <param name="context">The streaming context to use for the exception.</param>
        public EmailException(
            SerializationInfo info,
            StreamingContext context
            ) : base(info, context)
        {

        }

        #endregion
    }
}
