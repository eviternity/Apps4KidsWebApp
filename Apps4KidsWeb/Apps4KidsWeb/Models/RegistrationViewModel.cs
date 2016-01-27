using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Apps4KidsWeb.Domain;
using System.ComponentModel.DataAnnotations;
using Apps4KidsWeb.Validators;

namespace Apps4KidsWeb.Models
{
    public class RegistrationViewModel : IRegistration
    {
        #region properties

        /// <summary>
        /// The firstname
        /// </summary>
        [Required]
        [MaxLength(50)]
        [Display(Name = "Vorname")]
        public string Firstname { get; set; }
        /// <summary>
        /// The lastname
        /// </summary>
        [Required]
        [MaxLength(50)]
        [Display(Name = "Nachname")]
        public string Lastname { get; set; }

        /// <summary>
        /// The children
        /// </summary>
        [Display(Name = "Kinder")]
        [MaxLength(100)]
        [DataType(DataType.MultilineText)]
        public string Children { get; set; }

        /// <summary>
        /// The id of the country of origin
        /// </summary>
        [Required]
        [Display(Name = "Herkunftsland")]
        [DataType("CountryOfOrigin")]
        public int CountryOfOriginId { get; set; }

        /// <summary>
        /// The username (e-mail addresss)
        /// </summary>
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Bitte geben Sie Ihre vollständige E-Mail Addresse ein")]
        [Display(Name = "E-Mail Addresse")]
        public string Username { get; set; }

        /// <summary>
        /// The password
        /// </summary>
        [Required]
        [Display(Name = "Passwort")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the confirm password.
        /// </summary>
        /// <value>
        /// The confirm password.
        /// </value>
        [Required]
        [Display(Name = "Passwort bestätigen")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [terms accepted].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [terms accepted]; otherwise, <c>false</c>.
        /// </value>
        [ScaffoldColumn(false)]
        [RequiresToBeTrue(ErrorMessage="Sie müssen die AGBs akzeptieren")]
        public bool TermsAccepted { get; set; }

        #endregion


    }
}