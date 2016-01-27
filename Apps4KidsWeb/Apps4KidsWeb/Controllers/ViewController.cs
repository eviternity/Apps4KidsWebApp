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
    /// Contains all view related actions
    /// </summary>
    public class ViewController : Controller
    {
        /// <summary>
        /// Returns View ViewApps
        /// </summary>
        /// <returns>View ViewApps</returns>
        [HttpGet]
        public ActionResult ViewApps()
        {
            return View(Facade.SearchApps());
        }

        /// <summary>
        /// Filters the Apps with given criterea
        /// </summary>
        /// <param name="criterea">The SearchCriteriaViewModel</param>
        /// <returns>View ViewApps</returns>
        [HttpPost]
        public ActionResult FilterApps(SearchCriteriaViewModel criterea)
        {
            if (ModelState.IsValid)
            {
                return View("ViewApps", Facade.SearchApps(criterea));
            }
            else
            {
                return RedirectToAction("ViewApps");
            }
        }

        /// <summary>
        /// Filters the Apps with given criterea and switches to a page
        /// </summary>
        /// <param name="criterea">The SearchCriteriaViewModel</param>
        /// <returns>View ViewApps</returns>
        [HttpGet]
        public ActionResult FilterAppsAndShowPage(SearchCriteriaViewModel criterea)
        {
            if (ModelState.IsValid)
            {
                 return View("ViewApps", Facade.SearchApps(criterea, criterea.Page));
            }
            else
            {
                return RedirectToAction("ViewApps"); 
            }
        }

        /// <summary>
        /// Returns View Detail
        /// </summary>
        /// <param name="id">The id of the App</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Detail(int id)
        {
            return View(Facade.GetApp(id));
        }

        /// <summary>
        /// Returns the PartialView _Recentions in order by date
        /// </summary>
        /// <param name="id">The id of the App</param>
        /// <returns>PartialView _Recentions</returns>
        [HttpPost]
        public ActionResult OrderByDate(int id)
        {
            IApp app = Facade.GetApp(id);
            return PartialView("_Recentions", app.Recentions.OrderByDescending(r => r.Date));
        }

        /// <summary>
        /// Returns the PartialView _Recentions in order by rating
        /// </summary>
        /// <param name="id">The id of the App</param>
        /// <returns>PartialView _Recentions</returns>
        [HttpPost]
        public ActionResult OrderByRating(int id)
        {
            IApp app = Facade.GetApp(id);
            return PartialView("_Recentions", app.Recentions.OrderByDescending(r => r.Rating));
        }
    }
}
