using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Apps4KidsWeb.Domain;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Apps4KidsWeb.Models
{
    public class RecentionViewModel : IRecention
    {
        #region properties

        /// <summary>
        /// The id of the recention
        /// </summary>
        [ScaffoldColumn(false)]
        public int ID { get; set; }

        /// <summary>
        /// The id of the user
        /// </summary>
        [HiddenInput(DisplayValue = false)]
        public int UserID { get; private set; }

        /// <summary>
        /// The user (firstname + lastname)
        /// </summary>
        [ScaffoldColumn(false)]
        public string User { get; set; }

        /// <summary>
        /// The id of the app
        /// </summary>
        [ScaffoldColumn(false)]
        public int AppID { get; set; }

        /// <summary>
        /// Gets or sets the name of the application.
        /// </summary>
        /// <value>
        /// The name of the application.
        /// </value>
        [ScaffoldColumn(false)]
        public string AppName { get; set; }

        /// <summary>
        /// The rating
        /// </summary>
        [Display(Name = "Bewertung")]
        [DataType("Rating")]
        [Range(1, 5, ErrorMessage = "Sie müssen 1 bis 5 Sterne vergeben")]
        public int Rating { get; set; }

        /// <summary>
        /// The comment
        /// </summary>
        [Display(Name = "Kommentar")]
        [DataType(DataType.MultilineText)]
        [MaxLength(255)]
        public string Comment { get; set; }

        /// <summary>
        /// The entry date
        /// </summary>
        [ScaffoldColumn(false)]
        public DateTime Date { get; set; }
        
        #endregion

        #region constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RecentionViewModel"/> class.
        /// </summary>
        public RecentionViewModel() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="RecentionViewModel"/> class.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="user">The user.</param>
        public RecentionViewModel(IApp app, IUser user)
        {
            this.AppID = app.ID;
            this.AppName = app.Name;
            this.UserID = user.ID;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RecentionViewModel"/> class.
        /// </summary>
        /// <param name="recention">The recention.</param>
        public RecentionViewModel(IRecention recention)
        {
            this.ID = recention.ID;
            this.Date = recention.Date;
            this.Comment = recention.Comment;
            this.AppID = recention.AppID;
            this.AppName = Facade.GetApp(recention.AppID).Name;
            this.Rating = recention.Rating;
            this.UserID = recention.UserID;
        }
        
        #endregion
    }
}