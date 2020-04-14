using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Trial_app_1.Models;

namespace Trial_app_1.Controllers
{
    public class HomeController : Controller
    {        
        private Movies_DBEntities _db = new Movies_DBEntities();
        // GET: Home
        public ActionResult Index(string sortOrder, string searchPhrase)
        {
            ViewBag.MovieTitle = (String.IsNullOrEmpty(sortOrder)) ? "Title_desc" : "";
            ViewBag.Director = sortOrder == "Director" ? "Director_desc" : "Director";

            var movies = from m in _db.Movies select m;

            switch (sortOrder)
            {
                case "Title_desc":
                    movies = movies.OrderByDescending(m => m.Title);
                    break;
                case "Director":
                    movies = movies.OrderBy(m => m.Director);
                    break;
                case "Director_desc":
                    movies = movies.OrderByDescending(m => m.Director);
                    break;
                default:
                    movies = movies.OrderBy(m => m.Title);
                    break;
            }

             if(String.IsNullOrEmpty(searchPhrase)) return View(movies.ToList());
             
             return View(movies.Where(m => m.Title.Contains(searchPhrase) || 
                                 m.Director.Contains(searchPhrase))
                                .ToList());
             
        }

        // GET: Home/Details/5
        public ActionResult Details(int id)
        {
            var movieToDetail = _db.Movies.Find(id);
            return View(movieToDetail);
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        [HttpPost]
        public ActionResult Create([Bind(Exclude ="Id")] Movie movieToCreate)
        {
            try
            { 
                _db.Movies.Add(movieToCreate);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int id)
        {
            var movieToEdit = _db.Movies.Find(id);
            return View(movieToEdit);
        }

        // POST: Home/Edit/5
        [HttpPost]
        public ActionResult Edit(Movie movieToEdit)
        {
            try
            {
                var originalMovie = _db.Movies.Find(movieToEdit.Id);
                _db.Entry(originalMovie).CurrentValues.SetValues(movieToEdit);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int id)
        {
            var movieToDelete = _db.Movies.Find(id);
            if (movieToDelete == null)
                return View("Index");
            return View(movieToDelete);
        }

        // POST: Home/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, string confirmButton)
        {
            try
            {
                if (id == 0 || id == 3 || id == 4 || id == 8)
                    return RedirectToAction("DeleteForbidden");
                else
                {
                    var movieToDelete = _db.Movies.Find(id);
                    _db.Movies.Remove(movieToDelete);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }

        public ActionResult DeleteForbidden()
        {
            return View();
        }
    }
}
