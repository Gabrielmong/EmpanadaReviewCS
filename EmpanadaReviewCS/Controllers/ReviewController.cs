using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmpanadaReviewCS.Controllers {
    public class ReviewController : Controller {

        Models.EmpanadaReviewEntities db = new Models.EmpanadaReviewEntities();

        // GET: Review list page
        public ActionResult Index() {
            var reviews = db.Review.ToList();
            var ratings = db.Rating.ToList();
            var restaurants = db.Restaurant.ToList();
            var users = db.UserEmpanada.ToList();

            // join the tables into a single list 

            var MasterList = from r in reviews
                             join ra in ratings on r.idRating equals ra.idRating
                             join re in restaurants on r.idRestaurant equals re.idRestaurant
                             join u in users on r.idUser equals u.idUser
                             select new Models.ViewModel.MasterListModel {
                                 idReview = r.idReview,
                                 idUser = r.idUser,
                                 idRating = r.idRating,
                                 title = r.title,
                                 description = r.description,
                                 createdAt = r.createdAt,
                                 imageSrc = r.imageSrc,
                                 likes = r.likes,
                                 score = ra.score,
                                 name = re.name,
                                 userName = u.userName,
                             };

            return View(MasterList);
        }


        // GET: Review
        public ActionResult Create(Models.ViewModel.ReviewModel review) {

            if (review == null) {
                review = new Models.ViewModel.ReviewModel();
            }

            if (User.Identity.IsAuthenticated == false) {
                return RedirectToAction("Login", "Home");
            }

            var restaurants = db.Restaurant.ToList();

            IEnumerable<SelectListItem> selectList = from r in restaurants
                                                     select new SelectListItem {
                                                         Value = r.idRestaurant.ToString(),
                                                         Text = r.name
                                                     };
            ViewBag.Restaurants = selectList;

            return View(review);
        }

        // POST: Review/Create
        [HttpPost]
        public ActionResult Save(Models.ViewModel.ReviewModel review) {
            if (!ModelState.IsValid) {
                return RedirectToAction("Create", review);
            }

            var newRating = new Models.Rating {
                score = review.idRating,
                createdAt = DateTime.Now.Date,
                updatedAt = DateTime.Now.Date,
                idRestaurant = review.idRestaurant
            };

            db.Rating.Add(newRating);
            db.SaveChanges();

            var newReview = new Models.Review {
                idUser = int.Parse(Session["idUser"].ToString()),
                idRating = newRating.idRating,
                title = review.title,
                description = review.description,
                createdAt = DateTime.Now.Date,
                updatedAt = DateTime.Now.Date,
                imageSrc = review.imageSrc,
                idRestaurant = review.idRestaurant,
                likes = 0
            };


            db.Review.Add(newReview);
            db.SaveChanges();

            // look for the restaurant in the database
            var restaurant = db.Restaurant.Find(review.idRestaurant);

            // get the average rating for the restaurant
            var averageRating = db.Rating.Where(r => r.idRestaurant == review.idRestaurant).Average(r => r.score);

            // update the restaurant's average rating
            restaurant.averageRating = (int?)averageRating;

            // save the changes to the database
            db.Entry(restaurant).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();



            return RedirectToAction("Success", review);
        }

        public ActionResult Success(Models.ViewModel.ReviewModel review) {
            return View(review);
        }

        public ActionResult Edit(int? id) {
            
            var review = db.Review.Find(id);
            
            if ((string)Session["role"] != "admin" && review.idUser != int.Parse(Session["idUser"].ToString())) {
                return RedirectToAction("Login", "Home");
            }
            
            var reviewModel = new Models.ViewModel.ReviewModel {
                idReview = review.idReview,
                idUser = review.idUser,
                idRating = review.idRating,
                title = review.title,
                description = review.description,
                createdAt = review.createdAt,
                updatedAt = review.updatedAt,
                imageSrc = review.imageSrc,
                idRestaurant = review.idRestaurant,
                likes = review.likes
            };
            return View(reviewModel);
        }

        [HttpPost]
        public ActionResult Update(Models.ViewModel.ReviewModel review) {

            var reviewToUpdate = db.Review.Find(review.idReview);
            reviewToUpdate.title = review.title;
            reviewToUpdate.description = review.description;
            reviewToUpdate.updatedAt = DateTime.Now.Date;
            reviewToUpdate.likes = review.likes;

            db.Entry(reviewToUpdate).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            // look for the restaurant in the database
            var restaurant = db.Restaurant.Find(review.idRestaurant);

            // get the average rating for the restaurant
            var averageRating = db.Rating.Where(r => r.idRestaurant == review.idRestaurant).Average(r => r.score);

            // update the restaurant's average rating
            restaurant.averageRating = (int?)averageRating;

            return RedirectToAction("Success", review);

        }

        public ActionResult Delete(int? id) {

            if (id == null) {
                return RedirectToAction("Index");
            }

            var review = db.Review.Find(id);
            var reviewModel = new Models.Review {
                idReview = review.idReview,
                idUser = review.idUser,
                idRating = review.idRating,
                title = review.title,
                description = review.description,
                createdAt = review.createdAt,
                updatedAt = review.updatedAt,
                imageSrc = review.imageSrc,
                idRestaurant = review.idRestaurant,
                likes = review.likes
            };


            ViewBag.Rating = db.Rating.Find(reviewModel.idRating).score;
            ViewBag.UserName = db.UserEmpanada.Find(reviewModel.idUser).userName;
            ViewBag.Location = db.Restaurant.Find(reviewModel.idRestaurant).location;




            return View(reviewModel);
        }

        [HttpPost]
        public ActionResult DeleteReview(Models.ViewModel.ReviewModel review) {

            var reviewToDelete = db.Review.Find(review.idReview);
            var ratingToDelete = db.Rating.Find(reviewToDelete.idRating);

            db.Review.Remove(reviewToDelete);
            db.Rating.Remove(ratingToDelete);
            db.SaveChanges();


            return RedirectToAction("Success");
        }

        public ActionResult Details(int? id) {

            if (id == null) {
                return RedirectToAction("Index");
            }

            var review = db.Review.Find(id);
            var reviewModel = new Models.Review {
                idReview = review.idReview,
                idUser = review.idUser,
                idRating = review.idRating,
                title = review.title,
                description = review.description,
                createdAt = review.createdAt,
                updatedAt = review.updatedAt,
                imageSrc = review.imageSrc,
                idRestaurant = review.idRestaurant,
                likes = review.likes
            };


            ViewBag.Rating = db.Rating.Find(reviewModel.idRating).score;
            ViewBag.Location = db.Restaurant.Find(reviewModel.idRestaurant).location;
            ViewBag.UserName = db.UserEmpanada.Find(reviewModel.idUser).userName;
            ViewBag.UserId = db.UserEmpanada.Find(reviewModel.idUser).idUser;
            ViewBag.createdAt = review.createdAt.ToString("MMMM dd, yyyy");
            ViewBag.updatedAt = review.updatedAt?.ToString("MMMM dd, yyyy");

            return View(reviewModel);
        }



    }
}