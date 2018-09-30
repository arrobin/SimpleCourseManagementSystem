using SimpleCourseManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleCourseManagement.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            if (Session["UserDetailsId"] == null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public ActionResult Autherize(UserDetail aUser)
        {
            using (SimpleCourseManagementDbEntities db = new SimpleCourseManagementDbEntities())
            {
                var userDetails = db.UserDetails.FirstOrDefault(a => a.LoginName == aUser.LoginName && a.LoginPassword == aUser.LoginPassword);
                if (userDetails == null)
                {
                    @ViewBag.Message = "Invalid User Name or Password";
                    return View("Index", aUser);
                }
                else
                {
                    Session["UserDetailsId"] = userDetails.UserDetailsId;
                    Session["UserName"] = userDetails.UserName;
                    Session["LoginName"] = userDetails.LoginName;
                    Session["UserRoleId"] = userDetails.UserRoleId;
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}