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
    /// Contains all recommendation related actions
    /// </summary>
    [Authorize]
    public class RecommendationController : Controller
    {
        /// <summary>
        /// Returns View RecommendApp
        /// </summary>
        /// <returns>View RecommendApp</returns>
        [HttpGet]
        public ActionResult RecommendApp()
        {
            return View();
        }

        /// <summary>
        /// Saves the recommendation
        /// </summary>
        /// <param name="recommendation">The RecommendationViewModel</param>
        /// <returns>on success: Redirect Home/Index, on failure: View RecommendApp</returns>
        [HttpPost]
        public ActionResult RecommendApp(RecommendationViewModel recommendation)
        {
            if (ModelState.IsValid)
            {
                Facade.GetUser(User.Identity.Name).AddRecommendation(recommendation);
                TempData["Message"] = string.Format("Der Vorschlag für '{0}' wurde an die Redaktion weitergeleitet.", recommendation.AppName);
                return RedirectToAction("Index","Home");
            }
            else
            {
                ViewBag.Message = "App empfehlen fehlgeschlagen bitte überprüfen Sie ihre Daten.";
                return View(recommendation);
            }
            
        }

    }
}
