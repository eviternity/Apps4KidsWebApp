using Apps4KidsWeb.Domain;
using Apps4KidsWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Apps4KidsWeb.Controllers
{
    /// <summary>
    /// Contains all registration related actions
    /// </summary>
    public class RegisterController : Controller
    {
        /// <summary>
        /// Returns View RegisterUser
        /// </summary>
        /// <returns>View RegisterUser</returns>
        public ActionResult RegisterUser()
        {
            return View();
        }

        /// <summary>
        /// Saves the registration and sends an e-mail to the user containing the link to authenticate the account
        /// </summary>
        /// <param name="registration">The RegistrationViewModel</param>
        /// <returns>on success: View ConfirmationMailIsSent, on failure: View RegisterUser, on user allready exists: Redirect Login/Login</returns>
        [HttpPost]
        public ActionResult RegisterUser(RegistrationViewModel registration)
        {
            if (ModelState.IsValid)
            {
                bool result = Facade.CreateUser(registration);
                if (result)
                {
                    return View("ConfirmationMailIsSent");
                }
                else
                {
                    TempData["ErrorMessage"] = "Registrierung fehlgeschlagen! Sie sind bereits registriert.";
                    return RedirectToAction("Login","Login");
                }
            }
            else
            {
                ViewBag.Message = "Registrierung fehlgeschlagen! Bitte überprüfen Sie Ihre Daten!";
                return View(registration);
            }

        }

    }
}
