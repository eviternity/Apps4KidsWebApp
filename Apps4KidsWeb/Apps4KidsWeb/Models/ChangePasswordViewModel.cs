using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Apps4KidsWeb.Domain;

namespace Apps4KidsWeb.Models
{
    public class ChangePasswordViewModel
    {
        [ScaffoldColumn(false)]
        public int UserID { get; set; }

        [ScaffoldColumn(false)]
        public string ConfirmationCode { get; set; }

        [Display(Name="Passwort")]
        [Required]
        [MaxLength(50)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name="Passwort bestätigen")]
        [Compare("Password")]
        [Required]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        
    }
}