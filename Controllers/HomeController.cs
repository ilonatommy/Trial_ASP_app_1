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
        public ActionResult Index()
        {
            return View(_db.Movies.ToList());
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
                var movieToDelete = _db.Movies.Find(id);
                _db.Movies.Remove(movieToDelete);
                _db.SaveChanges();         
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
