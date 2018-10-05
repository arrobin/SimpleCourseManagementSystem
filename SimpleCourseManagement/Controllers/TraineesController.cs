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
            return View(trainees.ToList());
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
