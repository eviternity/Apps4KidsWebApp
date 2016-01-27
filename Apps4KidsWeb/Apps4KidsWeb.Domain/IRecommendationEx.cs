using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps4KidsWeb.Domain
{
    /// <summary>
    /// The interface IRecommendationEx (extends IRecommendation)
    /// </summary>
    public interface IRecommendationEx : IRecommendation
    {
        /// <summary>
        /// Accepts the recommendation
        /// </summary>
        /// <returns>success</returns>
        bool Accept();

        /// <summary>
        /// Refuses the recommendation
        /// </summary>
        /// <returns>success</returns>
        bool Refuse();
    }
}
