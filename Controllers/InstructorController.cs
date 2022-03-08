using AskNLearn.Models;
using AskNLearn.Models.Instructor;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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
            AskNLearnEntities db = new AskNLearnEntities();
            InsDashModel model = new InsDashModel();
            var data = db.Users.ToList();
            var course = db.Courses.ToList();
            foreach (var item in course)
            {
                if (item.uid.Equals(Session["uid"]))
                {
                    model.PostedCourseCount++;
                }
            }
            foreach (var item in data)
            {
                if (item.userType.Equals("Learner"))
                {
                    model.LernerCount++;
                }
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult Profile()
        {
            AskNLearnEntities dbObj = new AskNLearnEntities();
            InstructorProfile ip = new InstructorProfile();
            if (Session["uid"] != null)
            {
            //var uid = (int)Session["uid"];
            var user = (from u in dbObj.Users
                        join ui in dbObj.UsersInfoes on u.uid equals ui.uid
                        select new { u.uid, u.name, u.username, u.email, u.password, u.dob, u.gender, u.proPic, u.dateTime,
                            ui.eduInfo, ui.reputation, ui.currentPosition}).ToList();
            foreach (var item in user)
            {
                    if (item.uid.Equals(Session["uid"]))
                    {
                        ip.uid = item.uid;
                        ip.name = item.name;
                        ip.username = item.username;
                        ip.email = item.email;
                        ip.password = item.password;
                        ip.dob = item.dob;
                        ip.gender = item.gender;
                        ip.proPic = item.proPic;
                        ip.dateTime = item.dateTime;
                        ip.eduInfo = item.eduInfo;
                        ip.reputation = item.reputation;
                        ip.currentPosition = item.currentPosition;
                    }
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
                    profile.proPic = "/Content/Instructor/ProPic/noPropic.jpg";
                }
                AskNLearnEntities db = new AskNLearnEntities();
                var obj = db.Users.Where(value => value.uid == profile.uid).First();
                obj.proPic = profile.proPic;
                db.Entry(obj).State = EntityState.Modified;
                db.SaveChanges();
            return RedirectToAction("Profile", "Instructor");
        }
        public ActionResult AddCourse()
        {
            const string pattern = @"(?:https?:\/\/)?(?:www\.)?(?:(?:(?:youtube.com\/watch\?[^?]*v=|youtu.be\/)([\w\-]+))(?:[^\s?]+)?)";
            const string replacement = "https://www.youtube.com/embed/$1";

            //var rgx = new Regex(pattern);
            //var result = rgx.Replace(input, replacement);
            return View();
        }
        public ActionResult CourseView(int id) 
        {
            AskNLearnEntities db = new AskNLearnEntities();
            List<CourseModel> courseModel = new List<CourseModel>();
            var course = (from c in db.Courses
                          join d in db.Documents on c.coid equals d.coid
                          where c.coid == id
                          select new
                          {
                              c.coid,
                              c.uid,
                              c.title,
                              c.details,
                              c.price,
                              c.upVote,
                              c.downVote,
                              c.dateTime,
                              d.image,
                              d.videoLink,
                              d.docs
                          }).ToList();
            foreach (var item in course)
            {
                CourseModel cm = new CourseModel();
                cm.coid = item.coid;
                cm.uid = item.uid;
                cm.title = item.title;
                cm.details = item.details;
                cm.price = item.price;
                cm.upVote = item.upVote;
                cm.downVote = item.downVote;
                cm.dateTime = item.dateTime;
                cm.image = item.image;
                cm.videoLink = item.videoLink;
                cm.docs = item.docs;
                courseModel.Add(cm);
            }
            return View(courseModel);
        }
        public ActionResult CourseList()
        {
            int uid = (int)Session["uid"];
            AskNLearnEntities db = new AskNLearnEntities();
            var courses = db.Courses.Where(value => value.uid == uid).ToList();

            return View(courses);
        }

    }
}