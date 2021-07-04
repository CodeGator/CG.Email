using CG.Business.Strategies;
using CG.Email.Strategies.Options;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CG.Email.Strategies
{
    /// <summary>
    /// This class is a base implementation of <see cref="IEmailStrategy"/>
    /// </summary>
    public abstract class EmailStrategyBase : StrategyBase
    {
        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <inheritdoc />
        public abstract Task<EmailResult> SendAsync(
            string fromAddress,
            IEnumerable<string> toAddresses,
            IEnumerable<string> ccAddresses,
            IEnumerable<string> bccAddresses,
            IEnumerable<string> attachments,
            string subject,
            string body,
            bool bodyIsHtml,
            CancellationToken token
            );

        #endregion
    }


    /// <summary>
    /// This class is a base implementation of <see cref="IEmailStrategy{TOptions}"/>
    /// </summary>
    /// <typeparam name="TOptions">The type of associated options.</typeparam>
    public abstract class EmailStrategyBase<TOptions> : 
        StrategyBase<TOptions>, 
        IEmailStrategy<TOptions>
        where TOptions : EmailStrategyOptions, new()
    {
        // *******************************************************************
        // Constructors.
        // *******************************************************************

        #region Constructors

        /// <summary>
        /// This constructor creates a new instance of the <see cref="EmailStrategyBase{TOptions}"/>
        /// class.
        /// </summary>
        protected EmailStrategyBase(
            IOptions<TOptions> options
            ) : base(options)
        {

        }

        #endregion

        // *******************************************************************
        // Public methods.
        // *******************************************************************

        #region Public methods

        /// <inheritdoc />
        public abstract Task<EmailResult> SendAsync(
            string fromAddress,
            IEnumerable<string> toAddresses,
            IEnumerable<string> ccAddresses,
            IEnumerable<string> bccAddresses,
            IEnumerable<string> attachments,
            string subject,
            string body,
            bool bodyIsHtml,
            CancellationToken token
            );

        #endregion
    }
}
