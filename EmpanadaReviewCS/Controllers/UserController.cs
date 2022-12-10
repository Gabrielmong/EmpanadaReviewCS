using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmpanadaReviewCS.Controllers {
    public class UserController : Controller {

        Models.EmpanadaReviewEntities db = new Models.EmpanadaReviewEntities();

        // index
        public ActionResult Index() {
            var users = db.UserEmpanada.ToList();
            return View(users);
        }

        // GET: User
        public ActionResult Create(Models.ViewModel.UserModel user) {

            if (user == null) {
                user = new Models.ViewModel.UserModel();
            }

            return View(user);
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Save(Models.ViewModel.UserModel user) {
            if (!ModelState.IsValid) {
                return RedirectToAction("Create", user);
            }


            Models.UserEmpanada userEmpanada = new Models.UserEmpanada();

            userEmpanada.userName = user.userName;
            userEmpanada.firstName = user.firstName;
            userEmpanada.password = user.password;
            userEmpanada.lastName = user.lastName;
            userEmpanada.bio = user.bio;
            userEmpanada.createdAt = DateTime.Now;
            userEmpanada.imageSrc = user.imageSrc;
            userEmpanada.phoneNumber = user.phoneNumber;
            userEmpanada.email = user.email;
            userEmpanada.gender = user.gender;
            userEmpanada.reviews = 0;

            db.SaveChanges();

            return RedirectToAction("Success", user);
        }

        public ActionResult Success(Models.ViewModel.UserModel user) {
            return View(user);
        }

        // GET: User/Edit/5
        public ActionResult Edit(int? id) {

            if (id == null) {
                return RedirectToAction("Index");
            }

            var user = db.UserEmpanada.Find(id);

                if (User.Identity.IsAuthenticated == false) {
                    return RedirectToAction("Login", "Home");
                }

                if (User.Identity.Name != user.userName) {
                    return RedirectToAction("Error");
            }
            
            var userModel = new Models.ViewModel.UserModel {
                idUser = user.idUser,
                userName = user.userName,
                firstName = user.firstName,
                password = user.password,
                lastName = user.lastName,
                bio = user.bio,
                createdAt = user.createdAt,
                imageSrc = user.imageSrc,
                phoneNumber = user.phoneNumber,
                email = user.email,

            };

            return View(userModel);
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(Models.ViewModel.UserModel user) {
            if (user.password == null || user.password == "" || user.password != user.confirmPassword) {
                return RedirectToAction("Error");
            }

            var userToUpdate = db.UserEmpanada.Find(user.idUser);
            userToUpdate.userName = user.userName;
            userToUpdate.firstName = user.firstName;
            userToUpdate.password = user.password;
            userToUpdate.lastName = user.lastName;
            userToUpdate.bio = user.bio;
            userToUpdate.createdAt = user.createdAt;
            userToUpdate.imageSrc = user.imageSrc;
            userToUpdate.phoneNumber = user.phoneNumber;
            userToUpdate.email = user.email;
            if (user.gender != null) {
                userToUpdate.gender = user.gender;
            }

            db.Entry(userToUpdate).State = System.Data.Entity.EntityState.Modified;

            db.SaveChanges();

            return RedirectToAction("Index");

        }

        public ActionResult Error() {
            return View();
        }

        // GET: User/Delete/5
        public ActionResult Delete(int? id) {

            if (id == null) {
                return RedirectToAction("Index");
            }
            
            var user = db.UserEmpanada.Find(id);

            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult DeleteUser(Models.ViewModel.UserModel user) {
            var userToDelete = db.UserEmpanada.Find(user.idUser);
            // delete all reviews by that user
            var reviews = db.Review.Where(r => r.idUser == user.idUser).ToList();
            foreach (var review in reviews) {
                db.Review.Remove(review);
            }


            db.UserEmpanada.Remove(userToDelete);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Details(int id) {
            var user = db.UserEmpanada.Find(id);
            
            return View(user);

        }
    }
     
}

