using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps4KidsWeb.Domain
{
    /// <summary>
    /// The interface IRecommendation
    /// </summary>
    public interface IRecommendation
    {
        /// <summary>
        /// The id
        /// </summary>
        int ID { get; }

        /// <summary>
        /// The id of the author
        /// </summary>
        int UserID { get; }

        /// <summary>
        /// The app name
        /// </summary>
        string AppName { get; }

        /// <summary>
        /// The app description
        /// </summary>
        string Description { get; }

        /// <summary>
        /// The id of the operating system
        /// </summary>
        int OperatingSystem { get; }

     
    }
}
