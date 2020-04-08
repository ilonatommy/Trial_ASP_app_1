using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trial_app_1.Models;

namespace Trial_app_1.Controllers
{
    public class MoviesController : Controller
    {
        // Action result has to have a View of the same name to be displayed properly
        public ActionResult Random()
        {
            var movie = new Movie() { Id = 1, Title = "Naked Gun" };
            return View(movie);
        }
    }
}