using AskNLearn.Models;
using AskNLearn.Models.Instructor;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AskNLearn.Controllers
{
    [Authorize]
    public class InstructorController : Controller
    {
        // GET: Instructor
        public ActionResult Dashboard()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Profile()
        {
            AskNLearnEntities dbObj = new AskNLearnEntities();
            var uid = (int)Session["uid"];
            var user = dbObj.Users.Find(uid);
            return View(user);
        }
        [HttpPost]
        public ActionResult Profile(InstructorProfile profile)
        {
            if (ModelState.IsValid)
            {
                AskNLearnEntities db = new AskNLearnEntities();
                var obj = db.Users.Where(value => value.uid == profile.uid).First();
                obj.name= profile.name;
                obj.username = profile.username;
                obj.email = profile.email;
                obj.password = profile.password;
                obj.dob = profile.dob;
                obj.gender = profile.gender;
                //obj = profile;
                db.Entry(obj).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Profile");
            }
            return View(profile);
        }
    }
}