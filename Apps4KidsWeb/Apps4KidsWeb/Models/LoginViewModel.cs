using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Apps4KidsWeb.Domain;

namespace Apps4KidsWeb.Models
{
    public class LoginViewModel : ILogin
    {
        [Required]
        [Display(Name="E-Mail")]
        [DataType(DataType.EmailAddress,ErrorMessage="Bitte geben Sie Ihre vollständige E-Mail Addresse ein")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name="Passwort")]
        public string Password { get; set; }

    }
}