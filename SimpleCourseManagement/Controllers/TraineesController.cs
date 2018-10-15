using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SimpleCourseManagement.Models;
using System.IO;

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
            var courseList = db.Courses.ToList();
            ViewBag.Courses = courseList;
            //ViewBag.BatchId = new SelectList(db.Batches, "BatchId", "BatchCode");
            ViewBag.UserDetailsId = new SelectList(db.UserDetails, "UserDetailsId", "UserName");
            return View();
        }

        // POST: Trainees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TraineeId,TraineeCode,BatchId,TraineeName,TraineeImage,FatherName,MotherName,Gender,Age,Address,ContactNumber,Email,NationalIdCard,Result,UserDetailsId,CreatedDateTime")] Trainee trainee, HttpPostedFileBase file)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Trainees.Add(trainee);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            //ViewBag.BatchId = new SelectList(db.Batches, "BatchId", "BatchCode", trainee.BatchId);
            //ViewBag.UserDetailsId = new SelectList(db.UserDetails, "UserDetailsId", "UserName", trainee.UserDetailsId);
            //return View(trainee);
            bool Status = false;
            string message = "";
            if (ModelState.IsValid)
            {
                #region Image
                var path = "";
                if (file != null)
                {
                    if (file.ContentLength > 0)
                    { //for checking uploaded file is image or not

                        if ((Path.GetExtension(file.FileName).ToLower() == ".jpg")
                                || (Path.GetExtension(file.FileName).ToLower() == ".png")
                                || (Path.GetExtension(file.FileName).ToLower() == ".gif")
                                || (Path.GetExtension(file.FileName).ToLower() == ".jpeg"))
                        {
                            path = Path.Combine(Server.MapPath("~/Content/TraineeImages"), file.FileName);
                            if (file.ContentLength > 307200) //300kb
                            {
                                ViewBag.SizeConflict = true;
                                return View();
                            }
                            else
                            {
                                trainee.TraineeImage = path;
                                ViewBag.UploadSuccess = true;
                            }
                        }
                    }
                    #endregion

                    #region Email and NID already Exists or Not! 

                    var isExist = IsEmailExist(trainee.Email);
                    if (isExist)
                    {
                        ModelState.AddModelError("EmailExist", "Email already exist");
                        return View(trainee);
                    }

                    var NIDExist = IsNIDExist(trainee.NationalIdCard);
                    if (NIDExist)
                    {
                        ModelState.AddModelError("NIDRepeat", "NID already exist");
                        return View(trainee);
                    }

                    #endregion
                    trainee.Result = "";
                    trainee.TraineeImage = path;
                    trainee.CreatedDateTime = DateTime.Now;
                    trainee.UserDetailsId = Convert.ToInt32(Session["UserDetailsId"]);
                    db.Trainees.Add(trainee);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                message = "Invalid Request";
                return View();
            }

            //ViewBag.BatchId = new SelectList(db.Batches, "BatchId", "BatchCode", trainee.BatchId);
            //ViewBag.UserDetailsId = new SelectList(db.UserDetails, "UserDetailsId", "UserName", trainee.UserDetailsId);
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
        [NonAction]
        public bool IsEmailExist(string emailID)
        {
            var v = db.Trainees.Where(a => a.Email == emailID).FirstOrDefault();
            return v != null;
        }
        [NonAction]
        public bool IsNIDExist(string nid)
        {
            var v = db.Trainees.Where(a => a.NationalIdCard == nid).FirstOrDefault();
            return v != null;
        }
        public ActionResult Certificate(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trainee trainee = db.Trainees.Include(t => t.Batch).Include(t => t.UserDetail).Include(t => t.Batch.Course).FirstOrDefault(a => a.TraineeId == id);
            if (trainee == null)
            {
                return HttpNotFound();
            }
            return View(trainee);
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
