using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps4KidsWeb.Domain.Email
{
    /// <summary>
    /// The interface IEmail
    /// </summary>
    public interface IEMail
    {
        /// <summary>
        /// Gets the e-mail address.
        /// </summary>
        /// <value>
        /// The e mail address.
        /// </value>
        string EMailAddress { get; }
       
        /// <summary>
        /// Gets the subject.
        /// </summary>
        /// <value>
        /// The subject.
        /// </value>
        string Subject { get; }
        
        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        string Message { get; }
    }
}
