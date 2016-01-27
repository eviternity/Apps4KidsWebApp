using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps4KidsWeb.Domain
{
    /// <summary>
    /// The interface IRegistration
    /// </summary>
    public interface IRegistration
    {
        /// <summary>
        /// The username (e-mail addresss)
        /// </summary>
        string Username { get; }

        /// <summary>
        /// The password
        /// </summary>
        string Password { get; }

        /// <summary>
        /// The firstname
        /// </summary>
        string Firstname { get; }

        /// <summary>
        /// The lastname
        /// </summary>
        string Lastname { get; }

        /// <summary>
        /// The children
        /// </summary>
        string Children { get; }

        /// <summary>
        /// The id of the country of origin
        /// </summary>
        int CountryOfOriginId { get; }
    }
}
