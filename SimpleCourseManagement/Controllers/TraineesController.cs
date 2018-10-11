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
    public class TraineesController : Controller
    {
        private SimpleCourseManagementDbEntities db = new SimpleCourseManagementDbEntities();

        // GET: Trainees
        public ActionResult Index()
        {
            var trainees = db.Trainees.Include(t => t.Batch).Include(t => t.UserDetail);
            var courseList = db.Courses.ToList();
            ViewBag.Trainees = trainees.ToList();
            ViewBag.Courses = courseList;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetTraineesByBatchId(int batchId)
        {
            var trainees = db.Trainees.Include(t => t.Batch).Include(t => t.UserDetail).Where(t => t.BatchId == batchId);
            var batch = db.Batches.FirstOrDefault(a => a.BatchId == batchId);
            ViewBag.StartDate = batch.StartDate.ToString("yyyy-MM-dd");
            ViewBag.EndDate = batch.EndDate.ToString("yyyy-MM-dd");
            ViewBag.Duration = batch.Duration;
            ViewBag.TotalSeat = batch.TotalSeat;
            var courseList = db.Courses.ToList();
            ViewBag.Courses = courseList;
            ViewBag.Trainees = trainees.ToList();
            return View("Index");
        }
        public JsonResult GetAllBatchesByCourseId(int courseId)
        {
            return Json(new SelectList(db.Batches.Where(cs => cs.CourseId == courseId), "BatchId", "BatchCode"), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAllTraineeByBatchId(int batchId)
        {
            var trainees = db.Trainees.Include(t => t.Batch).Include(t => t.UserDetail).Where(t => t.BatchId == batchId);
            var batch = db.Batches.FirstOrDefault(a => a.BatchId == batchId);
            ViewBag.StartDate = batch.StartDate;
            ViewBag.EndDate = batch.EndDate;
            ViewBag.Duration = batch.Duration;
            ViewBag.TotalSeat = batch.TotalSeat;
            return Json(trainees, JsonRequestBehavior.AllowGet);
        }
        // GET: Trainees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trainee trainee = db.Trainees.Find(id);
            if (trainee == null)
            {
                return HttpNotFound();
            }
            return View(trainee);
        }

        // GET: Trainees/Create
        public ActionResult Create()
        {
            ViewBag.BatchId = new SelectList(db.Batches, "BatchId", "BatchCode");
            ViewBag.UserDetailsId = new SelectList(db.UserDetails, "UserDetailsId", "UserName");
            return View();
        }

        // POST: Trainees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TraineeId,TraineeCode,BatchId,TraineeName,TraineeImage,FatherName,MotherName,Gender,Age,Address,ContactNumber,Email,NationalIdCard,Result,UserDetailsId,CreatedDateTime")] Trainee trainee)
        {
            if (ModelState.IsValid)
            {
                db.Trainees.Add(trainee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BatchId = new SelectList(db.Batches, "BatchId", "BatchCode", trainee.BatchId);
            ViewBag.UserDetailsId = new SelectList(db.UserDetails, "UserDetailsId", "UserName", trainee.UserDetailsId);
            return View(trainee);
        }
        // GET: Trainees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trainee trainee = db.Trainees.Find(id);
            if (trainee == null)
            {
                return HttpNotFound();
            }
            ViewBag.BatchId = new SelectList(db.Batches, "BatchId", "BatchCode", trainee.BatchId);
            ViewBag.UserDetailsId = new SelectList(db.UserDetails, "UserDetailsId", "UserName", trainee.UserDetailsId);
            return View(trainee);
        }

        // POST: Trainees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TraineeId,TraineeCode,BatchId,TraineeName,TraineeImage,FatherName,MotherName,Gender,Age,Address,ContactNumber,Email,NationalIdCard,Result,UserDetailsId,CreatedDateTime")] Trainee trainee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trainee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BatchId = new SelectList(db.Batches, "BatchId", "BatchCode", trainee.BatchId);
            ViewBag.UserDetailsId = new SelectList(db.UserDetails, "UserDetailsId", "UserName", trainee.UserDetailsId);
            return View(trainee);
        }

        // GET: Trainees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trainee trainee = db.Trainees.Find(id);
            if (trainee == null)
            {
                return HttpNotFound();
            }
            return View(trainee);
        }

        // POST: Trainees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Trainee trainee = db.Trainees.Find(id);
            db.Trainees.Remove(trainee);
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
