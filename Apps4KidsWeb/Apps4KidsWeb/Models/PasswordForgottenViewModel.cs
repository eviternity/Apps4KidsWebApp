using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Apps4KidsWeb.Models
{
    public class PasswordForgottenViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress,ErrorMessage="Bitte geben Sie Ihre vollständige E-Mail Addresse ein")]
        [Display(Name="E-Mail Addresse")]
        public string EMail { get; set; }
    }
}