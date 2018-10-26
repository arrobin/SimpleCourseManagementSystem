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
            var trainees = db.TraineeCourses.Include(t => t.Trainee).Include(t => t.Batch).Include(t => t.Course).Include(t => t.UserDetail);
            var courseList = db.Courses.ToList();
            ViewBag.Trainees = trainees.ToList();
            ViewBag.Courses = courseList;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetTraineesByBatchId(int batchId)
        {
            var trainees = db.TraineeCourses.Include(t => t.Trainee).Include(t => t.Batch).Include(t => t.Course).Include(t => t.UserDetail).Where(t=>t.BatchId==batchId);
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
            var trainees = db.TraineeCourses.Include(t => t.Trainee).Include(t => t.Batch).Include(t => t.Course).Include(t => t.UserDetail);
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
            TraineeCourse trainee = db.TraineeCourses.Find(id);
            if (trainee == null)
            {
                return HttpNotFound();
            }
            return View(trainee);
        }

        // GET: Trainees/Create
        public ActionResult Create()
        {
            //ViewBag.BatchId = new SelectList(db.Batches, "BatchId", "BatchCode");
            ViewBag.UserDetailsId = new SelectList(db.UserDetails, "UserDetailsId", "UserName");
            var courseList = db.Courses.ToList();
            ViewBag.Courses = courseList;
            return View();
        }

        // POST: Trainees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(int? TraineeId,string TraineeName,string TraineeImage,string FatherName,string MotherName,string Gender,string Age, string Address,string ContactNumber, string Email, string NationalIdCard, string TraineeCode, int? CourseId, int? BatchId, HttpPostedFileBase file)
        {
            //if (ModelState.IsValid)
            //{ TraineeCourseId,TraineeCode,TraineeId,CourseId,BatchId,Result,UserDetailsId
            //    db.Trainees.Add(trainee);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            //ViewBag.BatchId = new SelectList(db.Batches, "BatchId", "BatchCode", trainee.BatchId);
            //ViewBag.UserDetailsId = new SelectList(db.UserDetails, "UserDetailsId", "UserName", trainee.UserDetailsId);
            //return View(trainee);
            bool Status = false;
            string message = "";
            var courseList = db.Courses.ToList();
            if (ModelState.IsValid)
            {
                int totalTraining = db.TraineeCourses.Count(a => a.TraineeId == TraineeId);
                bool isThisCourseAlreadyDone = db.TraineeCourses.Any(a => a.TraineeId == TraineeId && a.CourseId == CourseId);
                if (CourseId==null)
                {
                    ViewBag.Required = "PLease Select a Course";
                    ViewBag.Courses = courseList;
                    return View();
                }
                if (BatchId == null)
                {
                    ViewBag.Required = "PLease Select a Batch";
                    ViewBag.Courses = courseList;
                    return View();
                }
                if (isThisCourseAlreadyDone)
                {
                    ViewBag.Required = "This Trainee Already Done This Course";
                    ViewBag.Courses = courseList;
                    return View();
                }
                if (totalTraining>=3)
                {
                    ViewBag.Required = "This Trainee Already Done 3(Three) Courses";
                    ViewBag.Courses = courseList;
                    return View();
                }
                bool isThisCourseCodeAlreadyHas = db.TraineeCourses.Any(a => a.TraineeCode == TraineeCode);
                if (isThisCourseCodeAlreadyHas)
                {
                    ViewBag.Required = "This Trainee Code Already Given";
                    ViewBag.Courses = courseList;
                    return View();
                }
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
                            if (file.ContentLength > 307200) //300kb
                            {
                                ViewBag.SizeConflict = true;
                                ViewBag.Courses = courseList;
                                ViewBag.Required = "Maximum Picture Size 300 KB!!!";
                                return View();
                            }
                            string fileName = Path.GetFileNameWithoutExtension(file.FileName);
                            string extention = Path.GetExtension(file.FileName);
                            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extention;
                            path = "~/Content/TraineeImages/" + fileName;
                            fileName = Path.Combine(Server.MapPath("~/Content/TraineeImages/"), fileName);
                            file.SaveAs(fileName);
                            ViewBag.UploadSuccess = true;
                        }
                    }
                    #endregion

                    Trainee aTrainee = new Trainee()
                    {
                        TraineeId = TraineeId == null ? 0 : (int)TraineeId,
                        TraineeName = TraineeName,
                        TraineeImage = path,
                        FatherName= FatherName,
                        MotherName= MotherName,
                        Gender= Gender,
                        Age= Convert.ToDouble(Age),
                        Address= Address,
                        ContactNumber = ContactNumber,
                        Email = Email,
                        NationalIdCard = NationalIdCard,
                        UserDetailsId = Convert.ToInt32(Session["UserDetailsId"]),
                        CreatedDateTime = DateTime.Now
                    };
                    #region Email and NID already Exists or Not! 

                    var isExist = aTrainee.TraineeId>=0?false:IsEmailExist(Email);
                    if (isExist)
                    {
                        ViewBag.Required = "This Email Already Used";
                        ViewBag.Courses = courseList;
                        //ModelState.AddModelError("EmailExist", "Email already exist");
                        return View();
                    }

                    var NIDExist = aTrainee.TraineeId >= 0 ? false : IsNIDExist(NationalIdCard);
                    if (NIDExist)
                    {
                        ViewBag.Required = "This National Id Card Already Used";
                        ViewBag.Courses = courseList;
                        //ModelState.AddModelError("NIDRepeat", "NID already exist");
                        return View();
                    }
                    var phoneExist = aTrainee.TraineeId >= 0 ? false : IsPhoneExist(ContactNumber);
                    if (phoneExist)
                    {
                        ViewBag.Required = "This Contact Number Already Used";                        
                        ViewBag.Courses = courseList;
                        //ModelState.AddModelError("NIDRepeat", "NID already exist");
                        return View();
                    }
                    TraineeCourse aTraineeCourse = new TraineeCourse()
                    {
                        TraineeCode= TraineeCode,
                        TraineeId = TraineeId==null?0:(int)TraineeId,
                        CourseId= CourseId == null ? 0 : (int)CourseId,
                        BatchId= BatchId == null ? 0 : (int)BatchId,
                        Result = "",
                        UserDetailsId = Convert.ToInt32(Session["UserDetailsId"]),
                        CreatedDateTime = DateTime.Now
                    };
                    if (aTrainee.TraineeId==0)
                    {
                        aTraineeCourse.Trainee = aTrainee;
                    }
                    #endregion
                    db.TraineeCourses.Add(aTraineeCourse);
                    db.SaveChanges();
                    ViewBag.Required = "Trainee Saved";
                    
                    ViewBag.Courses = courseList;
                    return View();
                }
            }
            else
            {
                message = "Invalid Request";
                ViewBag.Courses = courseList;
                return View();
            }

            //ViewBag.BatchId = new SelectList(db.Batches, "BatchId", "BatchCode", trainee.BatchId);
            //ViewBag.UserDetailsId = new SelectList(db.UserDetails, "UserDetailsId", "UserName", trainee.UserDetailsId);
            ViewBag.Required = "Please Provide All Information";
            ViewBag.Courses = courseList;
            return View();
        }
        public JsonResult GetTraineeByCode(string traineeCode)
        {
            var trainee = db.TraineeCourses.Include(t => t.Trainee).Include(t => t.Batch).Include(t => t.Course).Include(t => t.UserDetail).Where(t => t.TraineeCode == traineeCode).FirstOrDefault();
            if (trainee != null)
            {
                var result = new
                {
                    TraineeCourseId = trainee.TraineeCourseId,
                    TraineeName = trainee.Trainee.TraineeName,
                    CourseName = trainee.Course.CourseName,
                    BatchCode = trainee.Batch.BatchCode,
                    TraineeImage = trainee.Trainee.TraineeImage,
                    FatherName = trainee.Trainee.FatherName,
                    MotherName = trainee.Trainee.MotherName,
                    ContactNumber = trainee.Trainee.ContactNumber,
                    Email = trainee.Trainee.Email,
                    NationalIdCard = trainee.Trainee.NationalIdCard,
                    Result = trainee.Result,
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var result = new
                {
                    TraineeCourseId = 0,
                    TraineeName = "",
                    CourseName = "",
                    BatchCode = "",
                    TraineeImage = "",
                    FatherName = "",
                    MotherName = "",
                    ContactNumber = "",
                    Email = "",
                    NationalIdCard = "",
                    Result = "",
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetTraineeByPhone(string phone)
        {
            var trainee = db.Trainees.Include(t => t.UserDetail).Where(t => t.ContactNumber == phone).FirstOrDefault();
            if (trainee != null)
            {
                var result = new
                {
                    TraineeId = trainee.TraineeId,
                    TraineeName = trainee.TraineeName,
                    FatherName = trainee.FatherName,
                    MotherName = trainee.MotherName,
                    ContactNumber = trainee.ContactNumber,
                    Email = trainee.Email,
                    Age = trainee.Age,
                    Gender = trainee.Gender,
                    Address = trainee.Address,
                    NationalIdCard = trainee.NationalIdCard,
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var result = new
                {
                    TraineeId = 0,
                    TraineeName = "",
                    TraineeImage = "",
                    FatherName = "",
                    MotherName = "",
                    ContactNumber = "",
                    Email = "",
                    Age = "",
                    Gender = "",
                    Address = "",
                    NationalIdCard = "",
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult AddResult()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddResult(TraineeCourse aTraineeCourse)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(aTraineeCourse.Result))
                {
                    ViewBag.Result = "Result is Required!!!";                    
                    return View();
                }
                if (aTraineeCourse.TraineeCourseId == 0)
                {
                    ViewBag.Result = "Please Search a Valid Trainee";
                    return View();
                }
                var traineeCourse = db.TraineeCourses.FirstOrDefault(a => a.TraineeCourseId == aTraineeCourse.TraineeCourseId);
                traineeCourse.Result = aTraineeCourse.Result;
                db.Entry(traineeCourse).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.Result = "Result Saved";
                return View();
            }
            ViewBag.Result = "Result Not Saved";
            return View();
        }
        [HttpGet]
        public ActionResult ResultAdd()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResultAdd(TraineeCourse aTraineeCourse)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(aTraineeCourse.Result))
                {
                    ViewBag.Result = "Result is Required!!!";
                    return View();
                }
                if (aTraineeCourse.TraineeCourseId == 0)
                {
                    ViewBag.Result = "Please Search a Valid Trainee";
                    return View();
                }
                var traineeCourse = db.TraineeCourses.FirstOrDefault(a => a.TraineeCourseId == aTraineeCourse.TraineeCourseId);
                traineeCourse.Result = aTraineeCourse.Result;
                db.Entry(traineeCourse).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.Result = "Result Saved";
                return View();
            }
            ViewBag.Result = "Result Not Saved";
            return View();
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
        public bool IsPhoneExist(string phone)
        {
            var v = db.Trainees.Where(a => a.ContactNumber == phone).FirstOrDefault();
            return v != null;
        }
        public ActionResult Certificate(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TraineeCourse trainee = db.TraineeCourses.Include(t => t.Trainee).Include(t => t.Batch).Include(t => t.Course).Include(t => t.UserDetail).FirstOrDefault(a => a.TraineeCourseId == id);
            if (trainee == null)
            {
                return View("Index");
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
