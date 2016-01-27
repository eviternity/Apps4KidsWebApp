using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps4KidsWeb.Persistence;

namespace Apps4KidsWeb.Domain
{
    internal class SearchResult : ISearchResult
    {
        #region fields

        /// <summary>
        /// The apps to be shown per page
        /// </summary>
        protected const int APPSPERPAGE = 10;

        #endregion

        #region properties

        /// <summary>
        /// The total count of the apps filtered
        /// </summary>
        public int ResultAppCount { get; private set; }

        /// <summary>
        /// The number of pages
        /// </summary>
        public int Pages
        {
            get
            {
                int result = Convert.ToInt32(
                    Math.Round(
                    Convert.ToDouble(ResultAppCount) / Convert.ToDouble(APPSPERPAGE),
                    MidpointRounding.AwayFromZero));
                
                return result > 0 ? result : 1;
            }
        }

        /// <summary>
        /// The page
        /// </summary>
        public int Page { get; private set; }

        /// <summary>
        /// Returns whether the searchresult has a follow up page
        /// </summary>
        public bool HasNextPage
        {
            get { return Page < Pages; }
        }

        /// <summary>
        /// Returns whether the searchresult has a previous page
        /// </summary>
        public bool HasPreviousPage
        {
            get { return Page > 1; }
        }

        /// <summary>
        /// The apps on the page
        /// </summary>
        public IEnumerable<IApp> Apps { get; private set; }

        /// <summary>
        /// The search criteria
        /// </summary>
        public ISearchCriteria Criterea { get; private set; }

        #endregion

        #region constructor
        
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchResult"/> class.
        /// </summary>
        /// <param name="criterea">The criterea.</param>
        /// <param name="page">The page.</param>
        internal SearchResult(ISearchCriteria criterea = null, int page = 1)
        {
            this.Criterea = criterea;
            using (var context = Connection.GetContext())
            {
                IQueryable<Persistence.App> query =
                    context
                    .Apps
                    .Include("Pictures")
                    .Include("Categories")
                    .Include("OperatingSystems")
                    .Include("Recentions")
                    .Include("Recentions.User")
                    .Include("Producer");
                if (criterea != null)
                {
                     query = QueryByCriterea(query, criterea);
                }
                this.ResultAppCount = query.Count();
                this.Page = page <= Pages && page > 0 ? page : 1;
                if (criterea != null)
                {
                    query = query.SortByCritera(criterea);
                }
                else
                {
                    query = query.SortByCritera();
                }
                query = query.Skip(APPSPERPAGE * (this.Page - 1)).Take(APPSPERPAGE);
                List<IApp> result = new List<IApp>();
                foreach (var item in query)
                {
                    result.Add(new App(item));
                }
                this.Apps = result.ToArray();
            }
        }

        #endregion

        #region methods

        /// <summary>
        /// Queries the by criterea.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="criterea">The criterea.</param>
        /// <returns>The Query</returns>
        private static IQueryable<Persistence.App> QueryByCriterea(IQueryable<Persistence.App> data, ISearchCriteria criterea)
        {
            if (criterea.CategoryID.HasValue && criterea.CategoryID.Value > 0)
            {
                data = data.Where(app => app.Categories.Select(cat => cat.ID).Contains(criterea.CategoryID.Value));
            }
            if (!string.IsNullOrWhiteSpace(criterea.SearchFor) )
            {
                data = data.Where(
                    app =>
                        app.Designation.ToLower().Contains(criterea.SearchFor.ToLower()) ||
                        app.Description.ToLower().Contains(criterea.SearchFor.ToLower())
                    );
            }

            if (criterea.OSID.HasValue && criterea.OSID.Value > 0)
            {
                data = data.Where(app => app.OperatingSystems.Select(os => os.ID).Contains(criterea.OSID.Value));
            }
            return data;
        }

        #endregion


    }
}
