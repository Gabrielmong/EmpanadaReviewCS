using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmpanadaReviewCS.Controllers
{
    public class UserController : Controller
    {

        Models.EmpanadaReviewEntities db = new Models.EmpanadaReviewEntities();

        // GET: User list page
        public ActionResult Index()
        {
            return View(db.UserEmpanada.ToList());
        }

        // GET: User
        public ActionResult Create(Models.ViewModel.UserModel user)
        {

            if (user == null)
            {
                user = new Models.ViewModel.UserModel();
            }

            return View(user);
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Save(Models.ViewModel.UserModel user)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Create", user);
            }


            db.UserEmpanada.Add(user);
            db.SaveChanges();

            return RedirectToAction("Success", user);
        }




    }
}