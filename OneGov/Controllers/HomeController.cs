using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CBNFormQ.Models;

namespace CBNFormQ.Controllers
{
    public class HomeController : Controller
    {
        private OneGovDbContext db = new OneGovDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Services()
        {
            return View();
        }

        public ActionResult Dashboard()
        {

            return View(db.CbnFormQViewModels.ToList());
        }
    }
}