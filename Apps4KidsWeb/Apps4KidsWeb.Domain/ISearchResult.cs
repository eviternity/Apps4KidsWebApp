using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps4KidsWeb.Domain
{
    /// <summary>
    /// The interface ISearchResult
    /// </summary>
    public interface ISearchResult
    {
        /// <summary>
        /// The search criteria
        /// </summary>
        ISearchCriteria Criterea { get; }

        /// <summary>
        /// The total count of the apps filtered
        /// </summary>
        int ResultAppCount { get; }

        /// <summary>
        /// The number of pages
        /// </summary>
        int Pages { get; }
        
        /// <summary>
        /// The page
        /// </summary>
        int Page { get; }

        /// <summary>
        /// Returns whether the searchresult has a follow up page
        /// </summary>
        bool HasNextPage { get; }

        /// <summary>
        /// Returns whether the searchresult has a previous page
        /// </summary>
        bool HasPreviousPage { get; }

        /// <summary>
        /// The apps on the page
        /// </summary>
        IEnumerable<IApp> Apps { get; }
    }
}
