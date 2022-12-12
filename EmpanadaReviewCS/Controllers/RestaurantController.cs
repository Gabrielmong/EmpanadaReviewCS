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

            // update the rating of each restaurant 
            foreach (var restaurant in restaurants) {
                var ratings = db.Rating.Where(r => r.idRestaurant == restaurant.idRestaurant).ToList();
                var total = 0;
                foreach (var rating in ratings) {
                    total += rating.score;
                }
                if (ratings.Count > 0) {
                    restaurant.averageRating = total / ratings.Count;
                } else {
                    restaurant.averageRating = 0;
                }
            }

            // save the changes to the database
            db.SaveChanges();
            

            return View(restaurants);
        }

        // GET: Restaurant/Details/5
        public ActionResult Details(int? id) {
            var restaurant = db.Restaurant.Find(id);
            return View(restaurant);
        }

        // GET: Restaurant/Create
        public ActionResult Create(Restaurant restaurant) {
            
            if ((string)Session["role"] != "admin") {
                return RedirectToAction("Login", "Home");
            }

            if (restaurant == null) {
                restaurant = new Restaurant();
            }

            return View();
        }

        // POST: Restaurant/Create
        [HttpPost]
        public ActionResult Save(Restaurant restaurant) {

            if (!ModelState.IsValid) {
                return RedirectToAction("Create", restaurant);
            }


            db.Restaurant.Add(restaurant);

            // create a new restaurant to also include default values
            Restaurant restaurantToDB = new Restaurant();

            restaurantToDB.name = restaurant.name;
            restaurantToDB.location = restaurant.location;
            restaurantToDB.createdAt = DateTime.Now;
            restaurantToDB.averageRating = 0;
            restaurantToDB.description = restaurant.description;
            restaurantToDB.foodType = restaurant.foodType;
            restaurantToDB.restrictions = restaurant.restrictions;


            db.SaveChanges();
            return RedirectToAction("Index");

        }

        // GET: Restaurant/Edit/5
        public ActionResult Edit(int? id) {

            if ((string)Session["role"] != "admin") {
                return RedirectToAction("Login", "Home");
            }
            
            var restaurant = db.Restaurant.Find(id);

            return View(restaurant);
        }

        // POST: Restaurant/Edit/5
        [HttpPost]
        public ActionResult Edit(Restaurant restaurant) {

            
            if (!ModelState.IsValid) {
                return RedirectToAction("Edit", restaurant);
            }

            

            var restaurantToUpdate = db.Restaurant.Find(restaurant.idRestaurant);

            restaurantToUpdate.name = restaurant.name;
            restaurantToUpdate.description = restaurant.description;
            restaurantToUpdate.foodType = restaurant.foodType;
            restaurantToUpdate.hasAllergies = restaurant.hasAllergies;
            restaurantToUpdate.restrictions = restaurant.restrictions;

            db.Entry(restaurantToUpdate).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int? id) {

            if ((string)Session["role"] != "admin") {
                return RedirectToAction("Login", "Home");
            }

            
            var restaurant = db.Restaurant.Find(id);
            return View(restaurant);
        }

        [HttpPost]
        public ActionResult Delete(Restaurant restaurant) {
            var restaurantToDelete = db.Restaurant.Find(restaurant.idRestaurant);

            //delete all reviews associated with this restaurant
            var reviews = db.Review.Where(r => r.idRestaurant  == restaurant.idRestaurant);

            //delete all ratings associated with this restaurant
            var ratings = db.Rating.Where(r => r.idRestaurant == restaurant.idRestaurant);          
           

            db.Rating.RemoveRange(ratings);
            db.Review.RemoveRange(reviews);
            db.Restaurant.Remove(restaurantToDelete);

            
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
    
}