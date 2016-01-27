using Apps4KidsWeb.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Apps4KidsWeb.Models
{
    public class AlterProfileViewModel : IProfile
    {
        [ScaffoldColumn(false)]
        public int ID { get; set; }
        
        [Display(Name="Vorname")]
        [Required]
        [MaxLength(50)]
        public string Firstname { get; set; }
        
        [Required]
        [Display(Name="Nachname")]
        [MaxLength(50)]
        public string Lastname { get; set; }
        
        [Display(Name="Kinder")]
        [DataType(DataType.MultilineText)]
        [MaxLength(100)]
        public string Children { get; set; }

        [Display(Name="Herkunftsland")]
        [DataType("CountryOfOrigin")]
        public int CountryOfOriginID { get; set; }

        public AlterProfileViewModel()
        {

        }

        public AlterProfileViewModel(IUser user)
        {
            this.ID = user.ID;
            this.Firstname = user.FirstName;
            this.Lastname = user.Lastname;
            this.Children = user.Children;
            this.CountryOfOriginID = user.CountryOfOriginID;
        }
    }
}