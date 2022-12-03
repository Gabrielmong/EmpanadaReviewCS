using EmpanadaReviewCS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EmpanadaReviewCS.Controllers {
    public class HomeController : Controller {

        private readonly EmpanadaReviewEntities _db = new EmpanadaReviewEntities();
        public ActionResult Index() {
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

        public ActionResult User() {
            ViewBag.Message = "User page.";

            return View();
        }

        public ActionResult Login() {
            return View();
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
    }
}