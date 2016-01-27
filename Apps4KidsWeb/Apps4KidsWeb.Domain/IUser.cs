using System;
using System.Collections.Generic;

namespace Apps4KidsWeb.Domain
{
    /// <summary>
    /// The interface IUser
    /// </summary>
    public interface IUser
    {
        /// <summary>
        /// The id
        /// </summary>
        int ID { get; }

        /// <summary>
        /// The Firstname
        /// </summary>
        string FirstName { get; }

        /// <summary>
        /// The Lastname
        /// </summary>
        string Lastname { get; }

        /// <summary>
        /// The Children
        /// </summary>
        string Children { get; }

        /// <summary>
        /// Returns whether the user is an administrator
        /// </summary>
        bool IsAdmin { get; }

        /// <summary>
        /// The id of the country of origin
        /// </summary>
        int CountryOfOriginID { get; }

        /// <summary>
        /// Adds a recention
        /// </summary>
        /// <param name="recention">The recention</param>
        void AddRecension(IRecention recention);

        /// <summary>
        /// Removes a recention
        /// </summary>
        /// <param name="recentionId">The id of the recention</param>
        void RemoveRecention(int recentionId);

        /// <summary>
        /// Alters the contents of a recention
        /// </summary>
        /// <param name="recentionId">The recention</param>
        void AlterRecention(IRecention recention);

        /// <summary>
        /// Adds a recommendation
        /// </summary>
        /// <param name="recommendation">The recommendation</param>
        void AddRecommendation(IRecommendation recommendation);

        /// <summary>
        /// Changes the password
        /// </summary>
        /// <param name="password">The new password</param>
        void ChangePassword(string password);
    }
}
