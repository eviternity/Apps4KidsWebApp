using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Apps4KidsWeb.Domain;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Apps4KidsWeb.Models
{
    public class SearchCriteriaViewModel : ISearchCriteria
    {
        /// <summary>
        /// The searchstring
        /// </summary>
        [Display(Name = "Suche")]
        public string SearchFor { get; set; }

        /// <summary>
        /// The id of the category
        /// </summary>
        [DataType("AppCategory")]
        [Display(Name = "Kategorie")]
        public int? CategoryID { get; set; }

        /// <summary>
        /// The id of the operating system
        /// </summary>
        [DataType("OperatingSystem")]
        [Display(Name = "Betriebssystem")]
        public int? OSID {get;set;}

        /// <summary>
        /// Gets or sets the page.
        /// </summary>
        /// <value>
        /// The page.
        /// </value>
        [ScaffoldColumn(false)]
        public int Page { get; set; }

        /// <summary>
        /// The order { "Rating","Rating_asc","Name","Name_desc","Price","Price_desc" }
        /// </summary>
        [ScaffoldColumn(false)]
        public string OrderBy { get; set; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public SearchCriteriaViewModel()
        {
            this.Page = 1;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchCriteriaViewModel"/> class.
        /// </summary>
        /// <param name="criterea">The criterea.</param>
        public SearchCriteriaViewModel(ISearchCriteria criterea)
        {
            if (criterea != null)
            {
                this.SearchFor = criterea.SearchFor;
                this.CategoryID = criterea.CategoryID;
                this.OSID = criterea.OSID;
                this.OrderBy = criterea.OrderBy;
                this.Page = 1;
                //todo: add additional criterea here
            }
        }
    }
}