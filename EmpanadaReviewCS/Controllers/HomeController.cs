using EmpanadaReviewCS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EmpanadaReviewCS.Controllers {
    public class HomeController : Controller {

        private EmpanadaReviewEntities _db = new EmpanadaReviewEntities();

        public ActionResult Index() {

            if (User.Identity.Name != null) {
                var user = _db.UserEmpanada.FirstOrDefault(u => u.userName == User.Identity.Name);


                if (user != null) {
                    Session["userName"] = user.userName;
                    Session["idUser"] = user.idUser;
                    Session["role"] = user.role;
                }

            }
            // welcome message

            if (Session["userName"] != null) {
                ViewBag.Message = "Welcome, " + Session["userName"] + "!";
            } else {
                ViewBag.Message = "Welcome to Empanada Review!";
            }

            return View();
        }


        public ActionResult Review() {
            ViewBag.Message = "Review page.";

            return View();
        }

        public ActionResult Restaurant() {
            ViewBag.Message = "Restaurants page.";

            return View();
        }
        public ActionResult Login() {
            return View();
        }

        public ActionResult Register() {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserEmpanada user) {
            if (ModelState.IsValid) {

                _db.UserEmpanada.Add(user);
                _db.SaveChanges();

                // get the user just created
                var userCreated = _db.UserEmpanada.FirstOrDefault(u => u.userName == user.userName);

                updateRegisterDefaults(userCreated);
                return RedirectToAction("Login");
            }
            return View();
        }

        public void updateRegisterDefaults(UserEmpanada user) {
            // search for user
            var userFromDb = _db.UserEmpanada.FirstOrDefault(u => u.idUser == user.idUser);
            // update user

            userFromDb.bio = "No bio yet.";
            userFromDb.reviews = 0;
            userFromDb.createdAt = DateTime.Now;
            userFromDb.role = "user";

            _db.SaveChanges();


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserEmpanada user) {
            if (ModelState.IsValid) {
                bool isValid = _db.UserEmpanada.Any(x => x.userName == user.userName && x.password == user.password);

                if (isValid) {
                    var validUser = _db.UserEmpanada.Where(x => x.userName == user.userName && x.password == user.password).FirstOrDefault();
                    FormsAuthentication.SetAuthCookie(user.userName, false);
                    // set the session
                    Session["userName"] = validUser.userName;
                    Session["idUser"] = validUser.idUser;
                    Session["role"] = validUser.role;
                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError("", "Invalid username or password");
            return View();
        }

        public ActionResult Logout() {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ProfilePage() {

            if (Session["userName"] != null) {
                // get the user id from the session
                int idUser = (int)Session["idUser"];

                // get the user from the database
                var user = _db.UserEmpanada.FirstOrDefault(u => u.idUser == idUser);

                // count all the reviews for the user
                var reviews = _db.Review.Where(r => r.idUser == idUser).ToList();

                // update the user reviews count
                user.reviews = reviews.Count();

                // save the changes
                _db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();
                

                ViewBag.createdAt = user.createdAt.ToString("MMMM dd, yyyy");
                // pass the user to the view

                return View(user);
            }

            return RedirectToAction("Login");
        }

        // edit profile 
        public ActionResult EditProfile(int? id) {
            if (id == null) {
                return RedirectToAction("Index");
            }


            var user = _db.UserEmpanada.FirstOrDefault(u => u.idUser == id);
            return View(user);
        }

        [HttpPost]
        public ActionResult EditProfile(UserEmpanada user) {

            if (!ModelState.IsValid) {
                return View(user);
            }
            // check if all fields are not null 
            if (user.userName != null && user.password != null && user.firstName != null && user.lastName != null && user.email != null) {
                // get the user from the database
                var userFromDb = _db.UserEmpanada.FirstOrDefault(u => u.idUser == user.idUser);

                // update the user
                userFromDb.userName = user.userName;
                userFromDb.password = user.password;
                userFromDb.firstName = user.firstName;
                userFromDb.updatedAt = DateTime.Now;
                userFromDb.lastName = user.lastName;
                userFromDb.email = user.email;

                // save the changes
                _db.Entry(userFromDb).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();

                FormsAuthentication.SignOut();
                Session.Clear();
                return RedirectToAction("Login", "Home");
            }

            return View(user);


        }

        // GET: User/Delete/5
        public ActionResult Delete(int id) {
            var user = _db.UserEmpanada.Find(id);

            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult DeleteUser(Models.ViewModel.UserModel user) {
            var userToDelete = _db.UserEmpanada.Find(user.idUser);
            // delete all reviews by that user
            var reviews = _db.Review.Where(r => r.idUser == user.idUser).ToList();
            foreach (var review in reviews) {
                _db.Review.Remove(review);
            }


            _db.UserEmpanada.Remove(userToDelete);
            _db.SaveChanges();


            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Login", "Home");
        }

    }

}