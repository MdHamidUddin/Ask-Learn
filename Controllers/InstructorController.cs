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
            InstructorProfile ip = new InstructorProfile(); 
            var uid = (int)Session["uid"];
            var user = (from u in dbObj.Users
                        join ui in dbObj.UsersInfoes on u.uid equals ui.uid
                        select new { u.uid, u.name, u.username, u.email, u.password, u.dob, u.gender, u.proPic, u.dateTime,
                            ui.eduInfo, ui.reputation, ui.currentPosition}).ToList();
            foreach (var item in user)
            {
                ip.uid = item.uid;
                ip.name=item.name;
                ip.username=item.username;
                ip.email=item.email;
                ip.password=item.password;
                ip.dob=item.dob;
                ip.gender=item.gender;
                ip.proPic=item.proPic;
                ip.dateTime=item.dateTime;
                ip.eduInfo=item.eduInfo;
                ip.reputation=item.reputation;
                ip.currentPosition=item.currentPosition;
            }
            return View(ip);
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