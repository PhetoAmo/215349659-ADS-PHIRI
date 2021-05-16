using System;
using MVCLogin.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCLogin.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Autherize( MVCLogin.Models.User userModel)
        {
            using (OnlineClassEntities db = new OnlineClassEntities())
            {
                var userDetils = db.Users.Where(x => x.UserName == userModel.UserName && x.Password == userModel.Password).FirstOrDefault();
                if (userDetils == null)
                {
                    userModel.LoginErrorMessage = "Wrong usernme or password";
                    return View("Index", userModel);
                }
                else
                {
                    Session["userID"] = userDetils.Id;
                    Session["Username"] = userDetils.UserName;
                    return RedirectToAction("Index", "Course");
                }
            } 
        }

        [HttpPost]
        public ActionResult checkOut(MVCLogin.Models.course courseModel)
        {
            using(OnlineClassEntities1 db = new OnlineClassEntities1())
            { var StudDetails = db.courses.Where(y => y.student_d == courseModel.student_d && y.course_id == courseModel.course_id).FirstOrDefault();
            if(StudDetails == null)
            { courseModel.CheckDetailsMessage = "Wrong Course Code or Student Number";
            return View("Index", courseModel);

            }
            else
            {
                Session["Course Code"] = courseModel.course_id;
                Session["Student_d"] = courseModel.student_d;
                return RedirectToAction("Index", "Attendance");
            }
            }


        }



        public ActionResult LogOut()
        {
            int userid = (int)Session["userID"];
            Session.Abandon();
            return RedirectToAction("Index", "Login");

        }
	}
}