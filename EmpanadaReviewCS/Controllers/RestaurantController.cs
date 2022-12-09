using EmpanadaReviewCS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmpanadaReviewCS.Controllers {
    public class RestaurantController : Controller {
        Models.EmpanadaReviewEntities db = new Models.EmpanadaReviewEntities();
        // GET: Restaurant
        public ActionResult Index() {

            var restaurants = db.Restaurant.ToList();

            return View(restaurants);
        }

        // GET: Restaurant/Details/5
        public ActionResult Details(int id) {
            var restaurant = db.Restaurant.Find(id);
            return View(restaurant);
        }

        // GET: Restaurant/Create
        public ActionResult Create(Restaurant restaurant) {
            if (restaurant == null) {
                restaurant = new Restaurant();
            }
            return View();
        }
        
        // POST: Restaurant/Create
        [HttpPost]
        public ActionResult Save(Restaurant restaurant) {

            db.Restaurant.Add(restaurant);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

    }
 }
            