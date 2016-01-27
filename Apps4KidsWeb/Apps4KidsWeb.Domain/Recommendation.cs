using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps4KidsWeb.Persistence;

namespace Apps4KidsWeb.Domain
{
    internal class Recommendation : IRecommendationEx
    {
        #region properties

        /// <summary>
        /// The id
        /// </summary>
        public int ID { get; private set; }

        /// <summary>
        /// The id of the author
        /// </summary>
        public int UserID { get; private set; }

        /// <summary>
        /// The app name
        /// </summary>
        public string AppName { get; private set; }

        /// <summary>
        /// The app description
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// The id of the operating system
        /// </summary>
        public int OperatingSystem { get; private set; }

        #endregion

        #region constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Recommendation"/> class.
        /// </summary>
        /// <param name="recommendation">The recommendation.</param>
        public Recommendation(Persistence.Recommendation recommendation)
        {
            this.ID = recommendation.ID;
            this.UserID = recommendation.UserID;
            this.OperatingSystem = recommendation.OSID;
            this.Description = recommendation.Description;
            this.AppName = recommendation.AppDesignation;
        }

        #endregion

        #region methods

        /// <summary>
        /// Accepts the recommendation
        /// </summary>
        /// <returns>
        /// success
        /// </returns>
        public bool Accept()
        {
            using (var context = Connection.GetContext())
            {
                try
                {
                    Persistence.Recommendation recommendation = context.Recommendations.Single(r => r.ID == this.ID);
                    recommendation.Approved = new Approved();
                    context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;                    
                }
            }
        }

        /// <summary>
        /// Refuses the recommendation
        /// </summary>
        /// <returns>
        /// success
        /// </returns>
        public bool Refuse()
        {
            using (var context = Connection.GetContext())
            {
                try
                {
                    Persistence.Recommendation recommendation = context.Recommendations.Single(r => r.ID == this.ID);
                    recommendation.Denied = new Denied();
                    context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        #endregion
    }
}
