using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps4KidsWeb.Persistence;
using System.Diagnostics;

namespace Apps4KidsWeb.Domain
{
    internal class User : IUser
    {
        #region properties

        /// <summary>
        /// The id
        /// </summary>
        public int ID { get; protected set; }

        /// <summary>
        /// The Firstname
        /// </summary>
        public string FirstName { get; protected set; }

        /// <summary>
        /// The Lastname
        /// </summary>
        public string Lastname { get; protected set; }

        /// <summary>
        /// The Children
        /// </summary>
        public string Children { get; protected set; }

        /// <summary>
        /// Returns whether the user is an administrator
        /// </summary>
        public bool IsAdmin { get; protected set; }

        /// <summary>
        /// The id of the country of origin
        /// </summary>
        public int CountryOfOriginID {get;protected set;}

        #endregion

        #region constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <param name="login">The login.</param>
        public User(sp_Login_Result login)
        {
            this.ID = login.ID;
            this.FirstName = login.FirstName;
            this.Lastname = login.LastName;
            this.Children = login.Children;

            //todo: map countryOfOriginID though its not necessary

            this.IsAdmin = login.IsAdmin.HasValue ? login.IsAdmin.Value : false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <param name="user">The user.</param>
        public User(Persistence.User user)
        {
            this.ID = user.ID;
            this.FirstName = user.FirstName;
            this.Lastname = user.LastName;
            this.Children = user.Children;
            this.CountryOfOriginID = user.CountryOfOriginID;
            this.IsAdmin = user.Admin != null;
        }

        #endregion

        #region methods

        /// <summary>
        /// Adds a recention
        /// </summary>
        /// <param name="recention">The recention</param>
        public void AddRecension(IRecention recention)
        {
            using (var context = Connection.GetContext())
            {
                context.Recentions.Add(new Persistence.Recention()
                {
                    AppID = recention.AppID,
                    Comment = recention.Comment,
                    Rating = recention.Rating,
                    UserID = ID,
                    Date = DateTime.Now
                });
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Removes a recention
        /// </summary>
        /// <param name="recentionId">The id of the recention</param>
        public void RemoveRecention(int recentionId)
        {
            using (var context = Connection.GetContext())
            {
                Persistence.Recention _recention = context.Recentions.SingleOrDefault(r => r.ID == recentionId && r.UserID == this.ID);
                if (_recention != null)
                {
                    context.Recentions.Remove(_recention);
                    context.SaveChanges(); 
                }
            }
        }

        /// <summary>
        /// Alters the contents of a recention
        /// </summary>
        /// <param name="recention">The recention</param>
        public void AlterRecention(IRecention recention)
        {
            using (var context = Connection.GetContext())
            {
                Persistence.Recention _recention = context.Recentions.SingleOrDefault(r => r.ID == recention.ID && r.UserID == this.ID);
                if (_recention != null)
                {
                    _recention.Rating = recention.Rating;
                    _recention.Comment = recention.Comment;
                    context.SaveChanges();
                }

            }
        }

        /// <summary>
        /// Adds a recommendation
        /// </summary>
        /// <param name="recommendation">The recommendation</param>
        public void AddRecommendation(IRecommendation recommendation)
        {
            using (var context = Connection.GetContext())
            {
                context.Recommendations.Add(new Persistence.Recommendation() 
                {
                    AppDesignation = recommendation.AppName,
                    Description = recommendation.Description,
                    OSID = recommendation.OperatingSystem,
                    UserID = this.ID,
                    Date = DateTime.Now
                });
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Changes the password
        /// </summary>
        /// <param name="password">The new password</param>
        public void ChangePassword(string password)
        {
            using (var context = Connection.GetContext())
            {
                context.sp_changeUserPassword(ID, password);
            }
        }

        #endregion
    }
}
