using Apps4KidsWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Apps4KidsWeb.Domain;

namespace Apps4KidsWeb.Controllers
{
    /// <summary>
    /// Contains all login related actions
    /// </summary>
    public class LoginController : Controller
    {
        /// <summary>
        /// Login Page
        /// </summary>
        /// <returns>View Login</returns>
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Logs the user in
        /// </summary>
        /// <param name="login">The LoginViewModel</param>
        /// <returns>on success: Redirect Home/Index, on failure: View Login</returns>
        [HttpPost]
        public ActionResult Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                IUser user = Facade.Login(login);

                if (user != null)
                {
                    TempData["Message"] = string.Format("Willkommen {0} {1}!",user.FirstName,user.Lastname);
                    FormsAuthentication.SetAuthCookie(user.ID.ToString(), true);
                    
                    return RedirectToAction("Index", "Home");
                }

            }
            ViewBag.ErrorMessage = "Login fehlgeschlagen überprüfen Sie die Email Addresse und das Passwort";
            return View(login);

        }

        /// <summary>
        /// Logs the user out
        /// </summary>
        /// <returns>Redirect Home/Index</returns>
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            ViewBag.Message = "Auf Wiedersehen";
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Confirms an Account (with link in E-Mail)
        /// </summary>
        /// <param name="id">the id of the user</param>
        /// <param name="confirmationCode">the confirmation code</param>
        /// <returns>on success: Redirect Home/Index, on failure: Redirect Login/Login</returns>
        [HttpGet]
        public ActionResult ConfirmAccount(int id, string confirmationCode)
        {
            IUser user = Facade.AuthentificateUser(id, confirmationCode);
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(user.ID.ToString(), true);
                TempData["Message"] = "Ihr Account wurde bestätigt.";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ErrorMessage = "Account ist bereits bestätigt. Bitte loggen Sie sich ein.";
                return RedirectToAction("Login", "Login");
            }
        }

        /// <summary>
        /// Returns View PasswordForgotten
        /// </summary>
        /// <returns>View PasswordForgotten</returns>
        [HttpGet]
        public ActionResult PasswordForgotten()
        {
            return View();
        }

        /// <summary>
        /// Sends an e-mail to the User containing a link to reset the password
        /// </summary>
        /// <param name="model">the PasswordForgottenViewModel</param>
        /// <returns>on success: Redirect Home/Index on failure: View PasswordForgotten</returns>
        [HttpPost]
        public ActionResult PasswordForgotten(PasswordForgottenViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (Facade.SendPasswordForgottenMail(model.EMail))
                {
                    TempData["Message"] = "Sie erhalten in Kürze eine E-Mail mit dem Link zum Ändern Ihres Passworts.";
                    return RedirectToAction("Index","Home");
                }
            }
            ViewBag.ErrorMessage = "Passwort zurücksetzten fehlgeschlagen. Bitte überprüfen Sie Ihre Daten.";
            return View(model);
        }

        /// <summary>
        /// Returns View ResetPassword (accessed with the link from the email)
        /// </summary>
        /// <param name="id">The id of the user</param>
        /// <param name="confirmationCode">The confirmationCode</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ResetPassword(int id, string confirmationCode)
        {
            var model = new ChangePasswordViewModel()
            {
                UserID = id,
                ConfirmationCode = confirmationCode
            };

            return View(model);
        }

        /// <summary>
        /// Sets the new Password
        /// </summary>
        /// <param name="model">The ChangePasswordViewModel</param>
        /// <returns>on success: Redirect Home/Index, on failure: View ResetPassword</returns>
        [HttpPost]
        public ActionResult ResetPassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                IUser user = Facade.ChangePassword(model.UserID, model.Password, model.ConfirmationCode);
                if (user != null)
                {
                     FormsAuthentication.SetAuthCookie(user.ID.ToString(), true);
                     TempData["Message"] = "Ihr Passwort wurde erfolgreich geändert.";
                     return RedirectToAction("Index", "Home");
                }               
            }

            ViewBag.ErrorMessage = "Passwort ändern fehlgeschlagen bitte überprüfen Sie ihre Daten";
            return View(model);
        }
    }
}
