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
            var reviews = db.Reviews.ToList();
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

            db.Ratings.Add(newRating);
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

            db.Reviews.Add(newReview);
            db.SaveChanges();
            
            return RedirectToAction("Success", review);
        }

        public ActionResult Success(Models.ViewModel.ReviewModel review) {
            return View(review);
        }
        
        public ActionResult Edit(int id) {
            var review = db.Reviews.Find(id);
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

            var reviewToUpdate = db.Reviews.Find(review.idReview);
            reviewToUpdate.title = review.title;
            reviewToUpdate.description = review.description;
            reviewToUpdate.updatedAt = DateTime.Now.Date;
            reviewToUpdate.likes = review.likes;

            db.SaveChanges();

            return RedirectToAction("Success", review);

        }

        public ActionResult Delete(int id) {
            var review = db.Reviews.Find(id);
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
        public ActionResult DeleteReview(Models.ViewModel.ReviewModel review) {
            if (!ModelState.IsValid) {
                return RedirectToAction("Index", review);
            }

            var reviewToDelete = db.Reviews.Find(review.idReview);
            db.Reviews.Remove(reviewToDelete);
            db.SaveChanges();

            return RedirectToAction("Success", review);
        }

        public ActionResult Details(int id) {
            var review = db.Reviews.Find(id);
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


            ViewBag.Rating = db.Ratings.Find(reviewModel.idRating).score;
            ViewBag.UserName = db.UserEmpanadas.Find(reviewModel.idUser).userName;


            return View(reviewModel);
        }
        
        
        
    }
}