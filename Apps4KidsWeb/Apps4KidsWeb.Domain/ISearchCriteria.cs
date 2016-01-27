using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps4KidsWeb.Domain
{
    /// <summary>
    /// The interface ISearchCriteria
    /// </summary>
    public interface ISearchCriteria
    {
        /// <summary>
        /// The order { "Rating","Rating_asc","Name","Name_desc","Price","Price_desc" }
        /// </summary>
        string OrderBy { get; }

        /// <summary>
        /// The searchstring
        /// </summary>
        string SearchFor { get; }

        /// <summary>
        /// The id of the category
        /// </summary>
        int? CategoryID { get; }

        /// <summary>
        /// The id of the operating system
        /// </summary>
        int? OSID { get; }
    }
}
