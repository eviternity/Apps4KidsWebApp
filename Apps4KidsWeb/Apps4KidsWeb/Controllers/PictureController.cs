using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Apps4KidsWeb.Domain;

namespace Apps4KidsWeb.Controllers
{
    /// <summary>
    /// Contains picture related actions
    /// </summary>
    public class PictureController : Controller
    {
        /// <summary>
        /// Gets a picture
        /// </summary>
        /// <param name="id">id of the picture</param>
        /// <returns>the picture</returns>
        [HttpGet]
        public ActionResult GetPicture(int id)
        {
            byte[] result = Facade.GetPicture(id);

            if (result != null)
            {
                return new FileContentResult(result, "image/jpeg");
            }

            return File(Server.MapPath("/Content/Images/DefaultAppPicture.png"),"image/png");
        }

    }
}
