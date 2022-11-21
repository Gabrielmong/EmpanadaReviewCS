using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmpanadaReviewCS.Controllers {
    public class HomeController : Controller {
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

    }
}