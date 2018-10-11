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
    public class BatchesController : Controller
    {
        private SimpleCourseManagementDbEntities db = new SimpleCourseManagementDbEntities();

        // GET: Batches
        public ActionResult Index()
        {
            var batches = db.Batches.Include(b => b.Course).Include(b => b.UserDetail);
            return View(batches.ToList());
        }
        public JsonResult GetAllBatchesByCourseId(int courseId)
        {
            //var batchList = db.Batches.Where(cs => (cs.CourseId == courseId), "BatchId", "BatchCode");
            return Json(new SelectList(db.Batches.Where(cs =>cs.CourseId == courseId), "BatchId", "BatchCode"), JsonRequestBehavior.AllowGet);
        }
        // GET: Batches/Create
        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseCode");
            var courseList = db.Courses.ToList();
            ViewBag.Courses = courseList;
            return View();
        }

        // POST: Batches/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BatchId,BatchCode,CourseId,StartDate,EndDate,Time,Instructor,CourseDetails,Duration,CourseFee,TotalSeat,Status,UserDetailsId,CreatedDateTime")] Batch batch)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(batch.BatchCode))
                {
                    ViewBag.Required = "Batch Code is Required!!!";
                    return View();
                }
                if (db.Batches.ToList().Exists(a => a.BatchCode == batch.BatchCode))
                {
                    ViewBag.CodeExist = "This Batch Code Already Exist!!!";
                    return View();
                }
                if (string.IsNullOrEmpty(batch.Time))
                {
                    ViewBag.Required = "Time is Required!!!";
                    return View();
                }
                if (string.IsNullOrEmpty(batch.Instructor))
                {
                    ViewBag.Required = "Instructor is Required!!!";
                    return View();
                }
                try
                {
                    Convert.ToDouble(batch.CourseFee);
                }
                catch
                {
                    ViewBag.Required = "Valid Course Fee Required!!!";
                    return View();
                }
                try
                {
                    Convert.ToInt32(batch.TotalSeat);
                }
                catch
                {
                    ViewBag.Required = "Valid Total Seat Required!!!";
                    return View();
                }
                try
                {
                    Convert.ToInt32(batch.Duration);
                }
                catch
                {
                    ViewBag.Required = "Valid Duration Required!!!";
                    return View();
                }
                //batch.Duration = 0;
                batch.Status = "";
                batch.UserDetailsId = Convert.ToInt32(Session["UserDetailsId"]);
                batch.CreatedDateTime = DateTime.Now;
                db.Batches.Add(batch);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseCode", batch.CourseId);
            ViewBag.UserDetailsId = new SelectList(db.UserDetails, "UserDetailsId", "UserName", batch.UserDetailsId);
            return View(batch);
        }

        // GET: Batches/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Batch batch = db.Batches.Find(id);
            if (batch == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseCode", batch.CourseId);
            ViewBag.UserDetailsId = new SelectList(db.UserDetails, "UserDetailsId", "UserName", batch.UserDetailsId);
            return View(batch);
        }

        // POST: Batches/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BatchId,BatchCode,CourseId,StartDate,EndDate,Time,Instructor,CourseDetails,Duration,CourseFee,TotalSeat,Status,UserDetailsId,CreatedDateTime")] Batch batchVM)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(batchVM.BatchCode))
                {
                    ViewBag.Required = "Batch Code is Required!!!";
                    return View();
                }
                if (db.Batches.ToList().Exists(a => a.BatchCode == batchVM.BatchCode && a.BatchId != batchVM.BatchId))
                {
                    ViewBag.CodeExist = "This Course Code Already Exist!!!";
                    return View();
                }
                if (string.IsNullOrEmpty(batchVM.Time))
                {
                    ViewBag.Required = "Time is Required!!!";
                    return View();
                }
                if (string.IsNullOrEmpty(batchVM.Instructor))
                {
                    ViewBag.Required = "Instructor is Required!!!";
                    return View();
                }
                try
                {
                    Convert.ToDouble(batchVM.CourseFee);
                }
                catch
                {
                    ViewBag.Required = "Valid Course Fee Required!!!";
                    return View();
                }
                try
                {
                    Convert.ToInt32(batchVM.TotalSeat);
                }
                catch
                {
                    ViewBag.Required = "Valid Total Seat Required!!!";
                    return View();
                }
                try
                {
                    Convert.ToInt32(batchVM.Duration);
                }
                catch
                {
                    ViewBag.Required = "Valid Duration Required!!!";
                }
                var batch = db.Batches.FirstOrDefault(x => x.BatchId == batchVM.BatchId);
                batch.BatchCode = batchVM.BatchCode;
                batch.CourseId = batchVM.CourseId;
                batch.StartDate = batchVM.StartDate;
                batch.EndDate = batchVM.EndDate;
                batch.Time = batchVM.Time;
                batch.Instructor = batchVM.Instructor;
                batch.CourseDetails = batchVM.CourseDetails;
                batch.CourseFee = batchVM.CourseFee;
                batch.TotalSeat = batchVM.TotalSeat;
                batch.Duration = batchVM.Duration;
                batch.Status = "";
                batch.UserDetailsId = Convert.ToInt32(Session["UserDetailsId"]);
                batch.CreatedDateTime = DateTime.Now;
                db.Entry(batch).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Courses, "CourseId", "CourseCode", batchVM.CourseId);
            ViewBag.UserDetailsId = new SelectList(db.UserDetails, "UserDetailsId", "UserName", batchVM.UserDetailsId);
            return View(batchVM);
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
