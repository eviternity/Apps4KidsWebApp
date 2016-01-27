using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps4KidsWeb.Domain
{
    /// <summary>
    /// The interface ILogin
    /// </summary>
    public interface ILogin
    {
        /// <summary>
        /// The user name (e-mail address)
        /// </summary>
        string UserName { get; }

        /// <summary>
        /// The password
        /// </summary>
        string Password { get; }
    }
}
