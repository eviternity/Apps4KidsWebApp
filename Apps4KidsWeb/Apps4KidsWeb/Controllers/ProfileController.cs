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
    /// Contains all profile related actions
    /// </summary>
    [Authorize]
    public class ProfileController : Controller
    {
        /// <summary>
        /// Returns View to alter the profile
        /// </summary>
        /// <returns>View Alter Profile</returns>
        [HttpGet]
        public ActionResult AlterProfile()
        {
            IUser user = Facade.GetUser(User.Identity.Name);
            return View(new AlterProfileViewModel(user));
        }

        /// <summary>
        /// Alters the Profile
        /// </summary>
        /// <param name="model">The AlterProfileViewModel</param>
        /// <returns>on success: Redirect Home/Index, on failure View AlterProfile</returns>
        [HttpPost]
        public ActionResult AlterProfile(AlterProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                Facade.AlterProfile(model);
                TempData["Message"] = "Profiländerungen wurden gespeichert.";
                return RedirectToAction("Index", "Home");
            }
            ViewBag.ErrorMessage = "Profiländerung fehlgeschlagen. Bitte überprüfen Sie Ihre Daten.";
            return View(model);
        }

        /// <summary>
        /// Returns View ChangePassword
        /// </summary>
        /// <returns>View ChangePassword</returns>
        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        /// <summary>
        /// Changes the password
        /// </summary>
        /// <param name="changePassword">The ChangePasswordViewModel</param>
        /// <returns>on success: Redirect Profile/AlterProfile, on failure: View ChangePassword</returns>
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel changePassword)
        {
            if (ModelState.IsValid)
            {
                IUser user = Facade.GetUser(User.Identity.Name);
                user.ChangePassword(changePassword.Password);
                TempData["Message"] = "Passwort erfolgreich geändert.";
                return RedirectToAction("AlterProfile");
            }

            ViewBag.ErrorMessage = "Passwortänderung fehlgeschlagen. Bitte überprüfen Sie Ihre Daten.";
            return View(changePassword);
        }

    }
}
