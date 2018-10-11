using SimpleCourseManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleCourseManagement.Controllers
{
    public class HomeController : Controller
    {
        private SimpleCourseManagementDbEntities db = new SimpleCourseManagementDbEntities();
        public ActionResult Index()
        {
            int totalCourse = db.Courses.Count();
            int totalBatches = db.Batches.Count();
            int totalTrainees = db.Trainees.Count();
            ViewBag.totalCourse = totalCourse;
            ViewBag.totalBatches = totalBatches;
            ViewBag.totalTrainees = totalTrainees;
            return View();
        }
    }
}