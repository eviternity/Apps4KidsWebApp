using Apps4KidsWeb.Domain;
using Apps4KidsWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace Apps4KidsWeb.Controllers
{
    /// <summary>
    /// Provides all administrator related activities
    /// </summary>
    [Authorize]
    public class AdminController : Controller
    {
        /// <summary>
        /// Sets ErrorMessage and redirects to Home/Index
        /// </summary>
        /// <returns>Redirect Home/Index</returns>
        private ActionResult NotAuthorized()
        {
            // Formsauthentication does not support Roles, therefor this little workaround had to be implemented
            TempData["ErrorMessage"] = "Sie sind nicht authorisiert!";
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Returns a view displaying all app recommendations
        /// </summary>
        /// <returns>View ViewAppRecommendations</returns>
        [HttpGet]
        public ActionResult ViewAppRecommendations()
        {
            if (!Facade.GetUser(User.Identity.Name).IsAdmin)
            {
                return NotAuthorized();
            }

            return View(Facade.GetRecommendations().Select(r => new RecommendationViewModel(r)));
        }

        /// <summary>
        /// Accepts a recommendation and redirects to AddApp filled with data from the recommendation
        /// </summary>
        /// <param name="id">the id of the recommendation to accept</param>
        /// <returns>View AddApp</returns>
        [HttpGet]
        public ActionResult AcceptRecommendation(int id)
        {
            if (!Facade.GetUser(User.Identity.Name).IsAdmin)
            {
                return NotAuthorized();
            }
            IRecommendationEx recommendation = Facade.GetRecommendation(id);

            var model = EditedAppRepository.GetInstance().CreateNewApp(recommendation);
            return View("AddApp",model);
        }

        /// <summary>
        /// Creates a new AddAppViewmodel filled with data from the existing app and redirects to the view
        /// </summary>
        /// <param name="id">The id of the app</param>
        /// <returns>View AddApp</returns>
        [HttpGet]
        public ActionResult EditApp(int id)
        {
            if (!Facade.GetUser(User.Identity.Name).IsAdmin)
            {
                return NotAuthorized();
            }
            IApp app = Facade.GetApp(id);

            var model = EditedAppRepository.GetInstance().CreateNewApp(app);
            return View("AddApp", model);
        }

        /// <summary>
        /// Rejects an recommendation
        /// </summary>
        /// <param name="id">The id of the recommendation</param>
        /// <returns>Redirect ViewAppRecommendations</returns>
        [HttpGet]
        public ActionResult RefuseRecommendation(int id)
        {
            if (!Facade.GetUser(User.Identity.Name).IsAdmin)
            {
                return NotAuthorized();
            }
            IRecommendationEx recommendation = Facade.GetRecommendation(id);
            recommendation.Refuse();

            return RedirectToAction("ViewAppRecommendations");

        }

        /// <summary>
        /// Generates a new AddAppViewModel
        /// </summary>
        /// <returns>View AddApp</returns>
        [HttpGet]
        public ActionResult AddApp()
        {
            if (!Facade.GetUser(User.Identity.Name).IsAdmin)
            {
                return NotAuthorized();
            }

            var model = EditedAppRepository.GetInstance().CreateNewApp();
            return View(model);
        }

        /// <summary>
        /// Copies data to the repository item of AddAppViewModel
        /// </summary>
        /// <param name="addApp">The (Client)ViewModel</param>
        /// <returns>Redirect AddPicture</returns>
        [HttpPost]
        public ActionResult AddApp(AddAppViewModel addApp)
        {
            if (!Facade.GetUser(User.Identity.Name).IsAdmin)
            {
                return NotAuthorized();
            }
            var viewmodel = EditedAppRepository.GetInstance().GetApp(addApp.Guid);
            viewmodel.AppName = addApp.AppName;
            viewmodel.Description = addApp.Description;
            viewmodel.Prerequisites = addApp.Prerequisites;
            viewmodel.Price = addApp.Price;
            viewmodel.URL = addApp.URL;
            viewmodel.Producer = addApp.Producer;
            return RedirectToAction("AddPicture", new { Guid = viewmodel.Guid });

        }

        /// <summary>
        /// Adds a Category to the AddAppViewModel
        /// </summary>
        /// <param name="Guid">The Guid of the item</param>
        /// <param name="Category">The id of the Category</param>
        /// <returns>PartialView _Categories</returns>
        [HttpPost]
        public ActionResult AddCategory(string Guid, int Category)
        {
            if (!Facade.GetUser(User.Identity.Name).IsAdmin)
            {
                return NotAuthorized();
            }

            var viewmodel = EditedAppRepository.GetInstance().GetApp(Guid);
            viewmodel.AddCategory(Category);
            return PartialView("_Categories", viewmodel);

        }

        /// <summary>
        /// Removes a Category from the AddAppViewModel
        /// </summary>
        /// <param name="Guid">The Guid of the item</param>
        /// <param name="id">The id of the Category</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RemoveCategory(string Guid, int id)
        {
            if (!Facade.GetUser(User.Identity.Name).IsAdmin)
            {
                return NotAuthorized();
            }

            var viewmodel = EditedAppRepository.GetInstance().GetApp(Guid);
            viewmodel.RemoveCategory(id);
            return PartialView("_Categories", viewmodel);
        }

        /// <summary>
        /// Adds an operating system to the AddAppViewModel
        /// </summary>
        /// <param name="Guid">The Guid of the item</param>
        /// <param name="OS">The id of the operating system</param>
        /// <returns>PartialView _OperatingSystems</returns>
        [HttpPost]
        public ActionResult AddOperatingSystem(string Guid, int OS)
        {
            if (!Facade.GetUser(User.Identity.Name).IsAdmin)
            {
                return NotAuthorized();
            }

            var viewmodel = EditedAppRepository.GetInstance().GetApp(Guid);
            viewmodel.AddOperatingSystem(OS);
            return PartialView("_OperatingSystems", viewmodel);

        }

        /// <summary>
        /// Removes an operating sytem from the AddAppViewModel
        /// </summary>
        /// <param name="Guid">The Guid of the item</param>
        /// <param name="id">The id of the operating system</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RemoveOperatingSystem(string Guid, int id)
        {
            if (!Facade.GetUser(User.Identity.Name).IsAdmin)
            {
                return NotAuthorized();
            }

            var viewmodel = EditedAppRepository.GetInstance().GetApp(Guid);
            viewmodel.RemoveOperatingSystem(id);
            return PartialView("_OperatingSystems", viewmodel);
        }

        /// <summary>
        /// Returns View to Add/Remove Pictures
        /// </summary>
        /// <param name="Guid">The Guid of the item</param>
        /// <returns>View AddPicture</returns>
        [HttpGet]
        public ActionResult AddPicture(string Guid)
        {
            if (!Facade.GetUser(User.Identity.Name).IsAdmin)
            {
                return NotAuthorized();
            }

            var viewmodel = EditedAppRepository.GetInstance().GetApp(Guid);

            return View(viewmodel);
        }

        /// <summary>
        /// Adds a picture to the ViewModel
        /// </summary>
        /// <param name="Guid">The Guid of the itme</param>
        /// <param name="files">The File to upload</param>
        /// <returns>View AddPicture</returns>
        [HttpPost]
        public ActionResult AddPicture(string Guid, HttpPostedFileBase files)
        {
            if (!Facade.GetUser(User.Identity.Name).IsAdmin)
            {
                return NotAuthorized();
            }

            var viewmodel = EditedAppRepository.GetInstance().GetApp(Guid);
            if (files != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {

                    files.InputStream.CopyTo(ms);
                    viewmodel.AddImage(ms.GetBuffer());
                }
            }

            return View(viewmodel);
        }

        /// <summary>
        /// Removes a picture from the ViewModel
        /// </summary>
        /// <param name="Guid">The Guid of the item</param>
        /// <param name="id">The (Repository)id of the picture to remove</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult RemovePicture(string Guid, int id)
        {
            if (!Facade.GetUser(User.Identity.Name).IsAdmin)
            {
                return NotAuthorized();
            }
            var viewmodel = EditedAppRepository.GetInstance().GetApp(Guid);
            viewmodel.RemoveImage(id);

            return PartialView("_AppImages", viewmodel);
        }

        /// <summary>
        /// Returns a picture from the AddViewModel of the repository
        /// </summary>
        /// <param name="Guid">The Guid of the item</param>
        /// <param name="id">The id of the picture</param>
        /// <returns>FileContentResult</returns>
        [HttpGet]
        public ActionResult GetAppPicture(string Guid, int id)
        {
            var viewmodel = EditedAppRepository.GetInstance().GetApp(Guid);
            return new FileContentResult(viewmodel.Images[id], "image/jpg");

        }

        /// <summary>
        /// Returns View Summary
        /// </summary>
        /// <param name="Guid">The guid of the item</param>
        /// <returns>View Summary</returns>
        [HttpGet]
        public ActionResult Summary(string Guid)
        {
            if (!Facade.GetUser(User.Identity.Name).IsAdmin)
            {
                return NotAuthorized();
            }
            var viewmodel = EditedAppRepository.GetInstance().GetApp(Guid);


            return View(viewmodel);
        }

        /// <summary>
        /// Saves the new app or the Changes
        /// </summary>
        /// <param name="Guid">The guid of the item</param>
        /// <returns>Redirect Home/Index</returns>
        [HttpGet]
        public ActionResult SaveApp(string Guid)
        {
            if (!Facade.GetUser(User.Identity.Name).IsAdmin)
            {
                return NotAuthorized();
            }

            var viewmodel = EditedAppRepository.GetInstance().GetApp(Guid);
            bool success = viewmodel.Save();
            if (!success)
            {
                TempData["Message"] = "Speichern der App ist fehlgeschlagen!";
                return RedirectToAction("Summary", new { Guid = Guid });
            }

            TempData["Message"] = "App erfolgreich gespeichert!";
            return RedirectToAction("Index", "Home");
        }
    }
}
