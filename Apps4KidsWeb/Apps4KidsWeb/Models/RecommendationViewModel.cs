using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Apps4KidsWeb.Domain;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Apps4KidsWeb.Models
{
    public class RecommendationViewModel : IRecommendation
    {
        #region properties

        [ScaffoldColumn(false)]
        public int ID { get; set; }
        
        [ScaffoldColumn(false)]
        public int UserID { get; set; }

        [Required]
        [Display(Name = "Name")]
        [MaxLength(50)]
        public string AppName { get; set; }

        [Required]
        [Display(Name = "Beschreibung")]
        [DataType(DataType.MultilineText)]
        [MaxLength(4000)]
        public string Description { get; set; }

        [Display(Name = "Betriebssystem")]
        [DataType("OperatingSystem")]
        [Range(1, int.MaxValue, ErrorMessage = "Sie müssen ein Betriebssystem angeben")]
        public int OperatingSystem { get; set; }

        #endregion

        #region constructors

        public RecommendationViewModel() {}

        public RecommendationViewModel(IRecommendation recommendation)
        {
            this.ID = recommendation.ID;
            this.UserID = recommendation.UserID;
            this.AppName = recommendation.AppName;
            this.Description = recommendation.Description;
            this.OperatingSystem = recommendation.OperatingSystem;
        }

        #endregion
    }
}