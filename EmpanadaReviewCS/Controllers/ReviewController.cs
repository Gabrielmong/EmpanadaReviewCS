using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmpanadaReviewCS.Controllers
{
    public class ReviewController : Controller
    {

        Models.EmpanadaReviewEntities db = new Models.EmpanadaReviewEntities();

        // GET: Review list page
        public ActionResult Index() {
            var reviews = db.Review.ToList();
            return View(reviews);
        }


        // GET: Review
        public ActionResult Create(Models.ViewModel.ReviewModel review) {
            
            if (review == null) {
                review = new Models.ViewModel.ReviewModel();
            }

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
                idUser = review.idUser,
                idRating = newRating.idRating,
                title = review.title,
                description = review.description,
                createdAt = DateTime.Now.Date,
                updatedAt = DateTime.Now.Date,
                imageSrc = review.imageSrc,
                idRestaurant = review.idRestaurant,
                likes = review.likes
            };

            var user = db.UserEmpanada.Find(review.idUser);
            var userPostCounter = user.reviews;
            userPostCounter++;
            user.reviews = userPostCounter;
            db.Entry(user).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();


            db.Review.Add(newReview);
            db.SaveChanges();
            
            return RedirectToAction("Success", review);
        }

        public ActionResult Success(Models.ViewModel.ReviewModel review) {
            return View(review);
        }
        
        public ActionResult Edit(int id) {
            var review = db.Review.Find(id);
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

            db.SaveChanges();

            return RedirectToAction("Success", review);

        }

        public ActionResult Delete(int id) {
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

            var user = db.UserEmpanada.Find(reviewToDelete.idUser);
            var userPostCounter = user.reviews;
            userPostCounter--;
            
            user.reviews = userPostCounter;
            db.Entry(user).State = System.Data.Entity.EntityState.Modified;

            var ratingToDelete = db.Rating.Find(reviewToDelete.idRating);
            db.Rating.Remove(ratingToDelete);
            db.Review.Remove(reviewToDelete);
            db.SaveChanges();

            return RedirectToAction("Success");
        }

        public ActionResult Details(int id) {
            
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


            return View(reviewModel);
        }
        
        
        
    }
}