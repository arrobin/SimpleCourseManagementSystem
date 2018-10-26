using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SimpleCourseManagement.Models;

namespace SimpleCourseManagement.Controllers
{
    public class CoursesController : Controller
    {
        private SimpleCourseManagementDbEntities db = new SimpleCourseManagementDbEntities();

        // GET: Courses
        public ActionResult Index()
        {
            var courses = db.Courses.Include(c => c.UserDetail);
            return View(courses.ToList());
        }
        public JsonResult GetAllCourses()
        {
            var courseList = db.Courses.Select(a => new { a.CourseId, a.CourseCode }).ToList();
            return Json(courseList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetChartData()
        {
            List<object> chartData = new List<object>();
            chartData.Add(new object[]
            {
                "Course", "TotalTrainee"
            });
            var courseList = db.Courses.ToList();
            foreach(var course in courseList)
            {
                int totalTrainee = db.TraineeCourses.Count(a => a.Batch.CourseId == course.CourseId);
                chartData.Add(new object[]
                    {
                        course.CourseCode, totalTrainee
                    });
            }
            return Json(chartData, JsonRequestBehavior.AllowGet);
        }
        // GET: Courses/Create
        public ActionResult Create()
        {
            ViewBag.UserDetailsId = new SelectList(db.UserDetails, "UserDetailsId", "UserName");
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseId,CourseCode,CourseName,IsActive,UserDetailsId,CreatedDateTime")] Course course)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(course.CourseCode) || string.IsNullOrEmpty(course.CourseName))
                {
                    ViewBag.Required = "Course Code & Course Name is Required!!!";
                    return View();
                }
                if (db.Courses.ToList().Exists(a => a.CourseCode == course.CourseCode))
                {
                    ViewBag.CodeExist = "This Course Code Already Exist!!!";
                    return View();
                }
                course.UserDetailsId = Convert.ToInt32(Session["UserDetailsId"]);
                course.CreatedDateTime = DateTime.Now;
                db.Courses.Add(course);
                db.SaveChanges();
                ViewBag.Required = "";
                ViewBag.CodeExist = "";
                return RedirectToAction("Index");
            }

            ViewBag.UserDetailsId = new SelectList(db.UserDetails, "UserDetailsId", "UserName", course.UserDetailsId);
            return View(course);
        }

        // GET: Courses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return RedirectToAction("Index", "Login");
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserDetailsId = new SelectList(db.UserDetails, "UserDetailsId", "UserName", course.UserDetailsId);
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourseId,CourseCode,CourseName,IsActive,UserDetailsId,CreatedDateTime")] Course courseVM)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(courseVM.CourseCode) || string.IsNullOrEmpty(courseVM.CourseName))
                {
                    ViewBag.Required = "Course Code & Course Name is Required!!!";
                    return View();
                }
                if (db.Courses.ToList().Exists(a => a.CourseCode == courseVM.CourseCode && a.CourseId != courseVM.CourseId))
                {
                    ViewBag.CodeExist = "This Course Code Already Exist!!!";
                    return View();
                }
                var course = db.Courses.FirstOrDefault(x => x.CourseId == courseVM.CourseId);
                course.CourseCode = courseVM.CourseCode;
                course.CourseName = courseVM.CourseName;
                course.IsActive = courseVM.IsActive;
                course.CreatedDateTime = DateTime.Now;
                course.UserDetailsId = Convert.ToInt32(Session["UserDetailsId"]);
                int userId = Convert.ToInt32(Session["UserDetailsId"]);
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserDetailsId = new SelectList(db.UserDetails, "UserDetailsId", "UserName", courseVM.UserDetailsId);
            return View(courseVM);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
