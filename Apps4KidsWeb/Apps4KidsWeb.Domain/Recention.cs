using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps4KidsWeb.Domain
{
    /// <summary>
    /// Represents the implementation of IRecention
    /// </summary>
    internal class Recention : IRecention
    {
        #region properties

        /// <summary>
        /// The id
        /// </summary>
        public int ID { get; internal set; }

        /// <summary>
        /// The rating
        /// </summary>
        public int Rating { get; internal set; }

        /// <summary>
        /// The comment
        /// </summary>
        public string Comment { get; internal set; }

        /// <summary>
        /// The entry date
        /// </summary>
        public DateTime Date { get; internal set; }

        /// <summary>
        /// The user (firstname + lastname)
        /// </summary>
        public string User { get; internal set; }

        /// <summary>
        /// The id of the user
        /// </summary>
        public int UserID { get; private set; }

        /// <summary>
        /// The id of the app
        /// </summary>
        public int AppID { get; private set; }

        #endregion

        #region constructor

        /// <summary>
        /// Initializes a new instance of Recention
        /// </summary>
        internal Recention()
        {

        }

        /// <summary>
        /// Initializes a new instance of Recention
        /// </summary>
        /// <param name="recention">The recention (Database entity)</param>
        internal Recention(Persistence.Recention recention)
        {
            this.ID = recention.ID;
            this.Rating = recention.Rating;
            this.Comment = recention.Comment;
            this.Date = recention.Date;
            this.User = string.Format("{0} {1}", recention.User.FirstName , recention.User.LastName);
            this.UserID = recention.UserID;
            this.AppID = recention.AppID;
        }

        #endregion








       
       
    }
}
