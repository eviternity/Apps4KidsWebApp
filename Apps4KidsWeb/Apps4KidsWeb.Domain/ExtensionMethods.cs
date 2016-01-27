using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps4KidsWeb.Domain
{
    /// <summary>
    /// Extension methods of apps4kids.domain
    /// </summary>
    internal static class ExtensionMethods
    {
        /// <summary>
        /// Sorts a list of apps with the criteria
        /// </summary>
        /// <param name="data">The list of apps</param>
        /// <param name="criterea">The criteria</param>
        /// <returns>The sorted list</returns>
        public static IQueryable<Persistence.App> SortByCritera(this IQueryable<Persistence.App> data, ISearchCriteria criteria)
        {
            return data.SortByCritera(criteria.OrderBy);
        }

        /// <summary>
        /// Sorts a list of apps with the criteria
        /// </summary>
        /// <param name="data">The list of apps</param>
        /// <param name="orderby">The order</param>
        /// <returns>The sorted list</returns>
        public static IQueryable<Persistence.App> SortByCritera(this IQueryable<Persistence.App> data, string orderBy = "")
        {
            switch (orderBy)
            {
                case null:
                case "":
                case "Rating":
                    data = data.OrderByDescending(app => app.Recentions.Select(r => r.Rating).Average());
                    break;
                case "Rating_asc":
                    data = data.OrderBy(app => app.Recentions.Select(r => r.Rating).Average());
                    break;
                case "Name":
                    data = data.OrderBy(app => app.Designation);
                    break;
                case"Name_desc":
                    data = data.OrderByDescending(app => app.Designation);
                    break;
                case "Price":
                    data = data.OrderBy(app => app.Price);
                    break;
                case "Price_desc":
                    data = data.OrderByDescending(app => app.Price);
                    break;
                default:
                    break;
            }
            return data;
        }

    }
}
