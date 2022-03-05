using AskNLearn.Models;
using AskNLearn.Models.Instructor;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
            if (Session["uid"] != null)
            {
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
                db.Entry(obj).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Profile");
            }
            return View(profile);
        }
        [HttpPost]
        public ActionResult UpdateProPic(InstructorProfile profile)
        {
            if (ModelState.IsValid)
            {
                if (profile.ImageFile != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(profile.ImageFile.FileName);
                    string extension = Path.GetExtension(profile.ImageFile.FileName);
                    fileName = fileName + "-" + profile.username + "-" + DateTime.Now.ToString("yyy-MM-d") + extension;
                    profile.proPic = "/Content/Instructor/ProPic/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Content/Instructor/ProPic/"), fileName);
                    profile.ImageFile.SaveAs(fileName);
                }
                else
                {
                    profile.proPic = "/Content/Instructor/ProPic/noPropic.png";
                }
                AskNLearnEntities db = new AskNLearnEntities();
                var obj = db.Users.Where(value => value.uid == profile.uid).First();
                obj.proPic = profile.proPic;
                db.Entry(obj).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Profile", "Instructor");
        }
        public ActionResult CourseView() 
        {
            return View();
        }
        public ActionResult CourseList()
        {
            AskNLearnEntities db = new AskNLearnEntities();
            var courses = db.Courses.ToList();

            return View(courses);
        }

    }
}