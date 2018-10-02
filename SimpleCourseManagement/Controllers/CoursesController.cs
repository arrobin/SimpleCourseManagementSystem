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

        // GET: Courses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
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
                    ViewBag.Required = "Course Code & Course Nameis Required!!!";
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
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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
        public ActionResult Edit([Bind(Include = "CourseId,CourseCode,CourseName,IsActive,UserDetailsId,CreatedDateTime")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserDetailsId = new SelectList(db.UserDetails, "UserDetailsId", "UserName", course.UserDetailsId);
            return View(course);
        }

        // GET: Courses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
            db.SaveChanges();
            return RedirectToAction("Index");
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
