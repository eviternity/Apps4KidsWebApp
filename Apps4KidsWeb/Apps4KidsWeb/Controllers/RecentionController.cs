using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Apps4KidsWeb.Domain;
using Apps4KidsWeb.Models;

namespace Apps4KidsWeb.Controllers
{
    /// <summary>
    /// Contains all recention related actions
    /// </summary>
    [Authorize]
    public class RecentionController : Controller
    {
        /// <summary>
        /// Returns View WriteRecention
        /// </summary>
        /// <param name="id">The id of the app</param>
        /// <returns>View WriteRecention</returns>
        public ActionResult WriteRecention(int id)
        {
            IApp app = Facade.GetApp(id);
            IUser user = Facade.GetUser(User.Identity.Name);
            return View(new RecentionViewModel(app, user));
        }

        /// <summary>
        /// Saves the recention
        /// </summary>
        /// <param name="recention">The RecentionViewModel</param>
        /// <returns>on success:Redirect View/Detail, on failure:View WriteRecention</returns>
        [HttpPost]
        public ActionResult WriteRecention(RecentionViewModel recention)
        {
            if (ModelState.IsValid)
            {
                IUser user = Facade.GetUser(User.Identity.Name);
                user.AddRecension(recention);
                TempData["Message"] = string.Format("Sie haben die App '{0}' erfolgreich bewertet.", recention.AppName);
                return RedirectToAction("Detail", "View", new { id = recention.AppID });
            }
            ViewBag.ErrorMessage = "App bewerten fehlgeschlagen. Bitte korregieren Sie ihre Daten.";
            return View(recention);
        }

        [HttpPost]
        public ActionResult DeleteRecention(int id, int appId)
        {
            IUser user = Facade.GetUser(User.Identity.Name);
            user.RemoveRecention(id);
            IApp app = Facade.GetApp(appId);
            return PartialView("_Recentions", app.Recentions);
        }

        [HttpGet]
        public ActionResult AlterRecention(int id, int appId)
        {
            IRecention recention = Facade.GetApp(appId).Recentions.Single(r => r.ID == id);
            return View("WriteRecention", new RecentionViewModel(recention));
        }

        [HttpPost]
        public ActionResult AlterRecention(RecentionViewModel model)
        {
            if (ModelState.IsValid)
            {
                IUser user = Facade.GetUser(User.Identity.Name);
                user.AlterRecention(model);
                return RedirectToAction("Detail", "View", new { id = model.AppID });
            }
            return View("WriteRecention", model);
        }



    }
}
