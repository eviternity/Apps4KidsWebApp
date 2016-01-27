using System;
namespace Apps4KidsWeb.Domain
{
    /// <summary>
    /// The interface IRecention
    /// </summary>
    public interface IRecention
    {
        /// <summary>
        /// The id of the recention
        /// </summary>
        int ID { get; }

        /// <summary>
        /// The id of the user
        /// </summary>
        int UserID { get; }

        /// <summary>
        /// The id of the app
        /// </summary>
        int AppID { get; }

        /// <summary>
        /// The user (firstname + lastname)
        /// </summary>
        string User { get; }
            
        /// <summary>
        /// The rating
        /// </summary>
        int Rating { get; }
        
        /// <summary>
        /// The comment
        /// </summary>
        string Comment { get; }

        /// <summary>
        /// The entry date
        /// </summary>
        DateTime Date { get; }
    }
}
