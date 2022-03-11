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
            if (Session["userType"].Equals("Instructor"))
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
            else
            {
                ViewBag.Message = "You Are Authorized As " + Session["userType"] + " You Cannot Acces This Page";
                return RedirectToAction("Login", "Users");
            }
        }
        [HttpGet]
        public ActionResult Profile()
        {
            if (Session["userType"].Equals("Instructor"))
            {
                AskNLearnEntities dbObj = new AskNLearnEntities();
                InstructorProfile ip = new InstructorProfile();
                if (Session["uid"] != null)
                {
                    //var uid = (int)Session["uid"];
                    var user = (from u in dbObj.Users
                                join ui in dbObj.UsersInfoes on u.uid equals ui.uid
                                select new
                                {
                                    u.uid,
                                    u.name,
                                    u.username,
                                    u.email,
                                    u.password,
                                    u.dob,
                                    u.gender,
                                    u.proPic,
                                    u.dateTime,
                                    ui.eduInfo,
                                    ui.reputation,
                                    ui.currentPosition
                                }).ToList();
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
            else
            {
                ViewBag.Message = "You Are Authorized As " + Session["userType"] + " You Cannot Acces This Page";
                return RedirectToAction("Login", "Users");
            }
        }
        [HttpPost]
        public ActionResult Profile(InstructorProfile profile)
        {
            if (Session["userType"].Equals("Instructor"))
            {
                if (ModelState.IsValid)
                {
                    AskNLearnEntities db = new AskNLearnEntities();
                    var obj = db.Users.Where(value => value.uid == profile.uid).First();
                    obj.name = profile.name;
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
            else
            {
                ViewBag.Message = "You Are Authorized As " + Session["userType"] + " You Cannot Acces This Page";
                return RedirectToAction("Login", "Users");
            }
        }
        [HttpPost]
        public ActionResult UpdateProPic(InstructorProfile profile)
        {
            if (Session["userType"].Equals("Instructor"))
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
            else
            {
                ViewBag.Message = "You Are Authorized As " + Session["userType"] + " You Cannot Acces This Page";
                return RedirectToAction("Login", "Users");
            }
        }
        [HttpGet]
        public ActionResult AddCourse()
        {
            if (Session["userType"].Equals("Instructor"))
            {
                return View();
        }
            else
            {
                ViewBag.Message = "You Are Authorized As " + Session["userType"] + " You Cannot Acces This Page";
                return RedirectToAction("Login", "Users");
    }
}
        [HttpPost]
        public ActionResult AddCourse(CourseModel c)
        {
            if (Session["userType"].Equals("Instructor"))
            {
                if (ModelState.IsValid)
                {
                    var uid = (int)Session["uid"];
                        if (c.ImageFile != null)
                        {
                            string fileName = Path.GetFileNameWithoutExtension(c.ImageFile.FileName);
                            string extension = Path.GetExtension(c.ImageFile.FileName);
                            fileName = fileName + "-" + DateTime.Now.ToString("yyy-MM-d") + extension;
                            c.thumbnail = "Content/Instructor/Courses/Images/" + fileName;
                            fileName = Path.Combine(Server.MapPath("~/Content/Instructor/Courses/Images/"), fileName);
                            c.ImageFile.SaveAs(fileName);
                        }
                        else
                        {
                            c.thumbnail = "Content/Instructor/Courses/Images/noPropic.jpg";
                        }


                    AskNLearnEntities db = new AskNLearnEntities();
                    Cours cours = new Cours();
                    cours.uid = uid;
                    cours.title = c.title;
                    cours.details = c.details;
                    cours.price = c.price;
                    cours.thumbnail = c.thumbnail;
                    cours.upVote = 1;
                    cours.downVote = 0;
                    cours.dateTime = DateTime.Now;
                    db.Courses.Add(cours);
                    db.SaveChanges();
                    int x = cours.coid;
                    return RedirectToAction("UpdateCourse", new { id = x });
                }
                return View();
            }
            else
            {
                ViewBag.Message = "You Are Authorized As " + Session["userType"] + " You Cannot Acces This Page";
                return RedirectToAction("Login", "Users");
            }
        }
        [HttpGet]
        public ActionResult UpdateCourse(int id)
        {
            AskNLearnEntities db = new AskNLearnEntities();
            var check = (from d in db.Documents
                        where d.coid.Equals(id)
                        select d).FirstOrDefault();
            if (check != null)
            {
                List<CourseViewModel> courseModel = new List<CourseViewModel>();
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
                                  c.thumbnail,
                                  d.imageTitle,
                                  d.image,
                                  d.videoTitle,
                                  d.videoLink,
                                  d.docTitle,
                                  d.docs
                              }).ToList();
                foreach (var item in course)
                {
                    CourseViewModel cm = new CourseViewModel();
                    cm.coid = item.coid;
                    cm.uid = item.uid;
                    cm.title = item.title;
                    cm.details = item.details;
                    cm.price = item.price;
                    cm.upVote = item.upVote;
                    cm.downVote = item.downVote;
                    cm.dateTime = item.dateTime;
                    cm.thumbnail = item.thumbnail;
                    cm.imageTitle = item.imageTitle;
                    cm.image = item.image;
                    cm.videoTitle = item.videoTitle;
                    cm.videoLink = item.videoLink;
                    cm.docTitle = item.docTitle;
                    cm.docs = item.docs;
                    courseModel.Add(cm);
                }
                ViewBag.CourseDoc = courseModel;
                return View();
            }
            else
            {
                var crs = db.Courses.Find(id);
                ViewBag.Course = crs;
                return View();
            }
        }
        [HttpPost]
        public ActionResult UpdateCourse(DocumentsModel d)
        {
            int x = d.coid;
            var videoLink = "";
            if (ModelState.IsValid)
            {
                if (d.videoLink!=null)
                {
                    const string pattern = @"(?:https?:\/\/)?(?:www\.)?(?:(?:(?:youtube.com\/watch\?[^?]*v=|youtu.be\/)([\w\-]+))(?:[^\s?]+)?)";
                    const string replacement = "https://www.youtube.com/embed/$1";
                    var rgx = new Regex(pattern);
                    videoLink = rgx.Replace(d.videoLink, replacement);
                }
                //For image Upload
                if (d.ImageFile != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(d.ImageFile.FileName);
                    string extension = Path.GetExtension(d.ImageFile.FileName);
                    fileName = d.imageTitle+ "-" + DateTime.Now.ToString("yyy-MM-d") + extension;
                    d.image = "Content/Instructor/Courses/Images/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Content/Instructor/Courses/Images/"), fileName);
                    d.ImageFile.SaveAs(fileName);
                }
                else
                {
                    d.image = "Content/Instructor/Courses/Images/noPropic.jpg";
                }
                //For Documents Upload
                if (d.DocFile != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(d.DocFile.FileName);
                    string extension = Path.GetExtension(d.DocFile.FileName);
                    fileName = d.docTitle + "-" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + extension;
                    d.docs = "Content/Instructor/Courses/Documents/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/Content/Instructor/Courses/Documents/"), fileName);
                    d.DocFile.SaveAs(fileName);
                }
                else
                {
                    d.docs = "Content/Instructor/Courses/Documents/noPropic.jpg";
                }


                AskNLearnEntities db = new AskNLearnEntities();
                Document doc = new Document();
                doc.coid = d.coid;
                doc.videoTitle = d.videoTitle;
                doc.videoLink = videoLink;
                doc.imageTitle = d.imageTitle;
                doc.image = d.image;
                doc.docTitle = d.docTitle;
                doc.docs = d.docs;
                db.Documents.Add(doc);
                db.SaveChanges();
                return RedirectToAction("UpdateCourse", new { id = x });
            }
            return RedirectToAction("UpdateCourse", new { id = x });
        }
        public ActionResult CourseView(int id)
        {
            if (Session["userType"].Equals("Instructor"))
            {
                AskNLearnEntities db = new AskNLearnEntities();
                List<CourseViewModel> courseModel = new List<CourseViewModel>();
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
                                  d.imageTitle,
                                  d.image,
                                  d.videoTitle,
                                  d.videoLink,
                                  d.docTitle,
                                  d.docs
                              }).ToList();
                foreach (var item in course)
                {
                    CourseViewModel cm = new CourseViewModel();
                    cm.coid = item.coid;
                    cm.uid = item.uid;
                    cm.title = item.title;
                    cm.details = item.details;
                    cm.price = item.price;
                    cm.upVote = item.upVote;
                    cm.downVote = item.downVote;
                    cm.dateTime = item.dateTime;
                    cm.imageTitle = item.imageTitle;
                    cm.image = item.image;
                    cm.videoTitle = item.videoTitle;
                    cm.videoLink = item.videoLink;
                    cm.docTitle = item.docTitle;
                    cm.docs = item.docs;
                    courseModel.Add(cm);
                }
                return View(courseModel);
            }
            else
            {
                ViewBag.Message = "You Are Authorized As " + Session["userType"] + " You Cannot Acces This Page";
                return RedirectToAction("Login", "Users");
            }
        }
        public ActionResult CourseList()
        {
            if (Session["userType"].Equals("Instructor"))
            {
                int uid = (int)Session["uid"];
                AskNLearnEntities db = new AskNLearnEntities();
                var courses = db.Courses.Where(value => value.uid == uid).ToList();

                return View(courses);
            }
            else
            {
                ViewBag.Message = "You Are Authorized As " + Session["userType"] + " You Cannot Acces This Page";
                return RedirectToAction("Login", "Users");
            }
        }

        public ActionResult DeleteCourse(int id)
        {
            AskNLearnEntities db = new AskNLearnEntities();
            var course = db.Courses.Find(id);
            var documents = db.Documents.Where(value => value.coid == id).ToList();
                foreach (var item in documents)
                {
                    db.Documents.Remove(item);
                    db.SaveChanges();
                }
            
                db.Courses.Remove(course);
                db.SaveChanges();
            
            return RedirectToAction("CourseList");
        }

        public ActionResult PdfPreview(string doc)
        {
            string path = Path.Combine(Server.MapPath("~/"), doc);
            return File(path, "application/pdf");
        }

    }
}