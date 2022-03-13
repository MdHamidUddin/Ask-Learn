using AskNLearn.Models;
using AskNLearn.Models.Moderator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AskNLearn.Controllers
{
    public class ModeratorController : Controller
    {
        // GET: Moderator
        AskNLearnEntities dbObj = new AskNLearnEntities();
        public ActionResult Dashboard()
        {
         
            //var data = dbObj.Users.ToList();
            List<ModeratorDashBModel> dashboardList = new List<ModeratorDashBModel>();
            var data = (from u in dbObj.Users
                        join ui in dbObj.UsersInfoes on u.uid equals ui.uid
                        select new { u.name, u.uid, u.userType, u.username, u.email, u.gender, u.dateTime, ui.reputation }).ToList();


            foreach (var u in data)
            {
                ModeratorDashBModel obj = new ModeratorDashBModel();
                obj.uid = u.uid;
                obj.name = u.name;
                obj.userType = u.userType;
                obj.username = u.username;
                obj.email = u.email;
                obj.gender = u.gender;
                obj.dateTime = u.dateTime;
                obj.reputation = u.reputation;
                dashboardList.Add(obj);

            }

            return View(dashboardList);
        }



        [HttpGet]
        public ActionResult Profile()
        {


            ProfileUpdateModel ip = new ProfileUpdateModel();

            var user = (from u in dbObj.Users
                        join ui in dbObj.UsersInfoes on u.uid equals ui.uid
                        select new
                        {
                            u.uid,
                            u.name,
                            u.username,
                            u.email,
                            u.password,
                            u.userType,
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
                if (item.username.Equals(Session["username"]))
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
                    ip.userType = item.userType;
                    ip.currentPosition = item.currentPosition;
                }

            }
            return View(ip);

        }


        [HttpGet]
        public ActionResult UserList()
        {

            List<ModeratorDashBModel> UserList = new List<ModeratorDashBModel>();
            var data = (from u in dbObj.Users
                        join ui in dbObj.UsersInfoes on u.uid equals ui.uid
                        select new { u.name, u.uid, u.userType, u.username, u.email, u.approval, u.gender, u.dob, u.dateTime, ui.reputation, ui.eduInfo, ui.currentPosition }).ToList();


            foreach (var u in data)
            {
                ModeratorDashBModel obj = new ModeratorDashBModel();
                if (u.approval.Equals("active") || u.approval.Equals("blocked"))
                {
                    if(u.userType.Equals("Admin"))
                    {

                    }
                    else
                    {
                        obj.uid = u.uid;
                        obj.name = u.name;
                        obj.userType = u.userType;
                        obj.username = u.username;
                        obj.email = u.email;
                        obj.gender = u.gender;
                        obj.dateTime = u.dateTime;
                        obj.dob = u.dob;
                        obj.reputation = u.reputation;
                        obj.eduInfo = u.eduInfo;
                        obj.currentPosition = u.currentPosition;
                        obj.approval = u.approval;
                        UserList.Add(obj);
                    }
                   
                }


            }

            return View(UserList);
        }

        [HttpGet]
        public ActionResult UserDetails(int uid)
        {

            ProfileUpdateModel ip = new ProfileUpdateModel();

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
                            u.userType,
                            u.proPic,
                            u.dateTime,
                            ui.eduInfo,
                            ui.reputation,
                            ui.currentPosition
                        }).ToList();


            foreach (var item in user)
            {
                if (item.uid.Equals(uid))
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
                    ip.userType = item.userType;
                }

            }
            return View(ip);
        }

        public ActionResult BlockUser(int uid)
        {
            var u = dbObj.Users.Where(x => x.uid.Equals(uid)).FirstOrDefault();
            var ui = dbObj.UsersInfoes.Where(x => x.uid.Equals(uid)).FirstOrDefault();
            u.approval = "blocked";
            dbObj.SaveChanges();

            return RedirectToAction("UserList");
        }

        public ActionResult UnBlockUser(int uid)
        {
            var u = dbObj.Users.Where(x => x.uid.Equals(uid)).FirstOrDefault();
            var ui = dbObj.UsersInfoes.Where(x => x.uid.Equals(uid)).FirstOrDefault();
            u.approval = "active";
            dbObj.SaveChanges();

            return RedirectToAction("UserList");
        }

        [HttpGet]
        public ActionResult DeletePost(int id)
        {

            var p = dbObj.Posts.Where(x => x.pid.Equals(id)).FirstOrDefault();

            dbObj.Posts.Remove(p);
            dbObj.SaveChanges();

            return RedirectToAction("PostList");
        }




        [HttpPost]
        public ActionResult EditUser(ProfileUpdateModel profile)
        {
            int id = 0;
            if (ModelState.IsValid)
            {
                AskNLearnEntities db = new AskNLearnEntities();
                var obj = db.Users.Where(value => value.uid == profile.uid).FirstOrDefault();
                obj.name = profile.name;
                obj.username = profile.username;
                obj.email = profile.email;
                obj.password = profile.password;
                obj.dob = profile.dob;
                obj.gender = profile.gender;
                //obj = profile;
                db.Entry(obj).State = EntityState.Modified;
                db.SaveChanges();
                id = obj.uid;
                if (obj.userType.Equals("Moderator"))
                {
                    //Session["username"] = obj.name;
                    return RedirectToAction("Profile");

                }
                else
                {
                    return RedirectToAction("UserList");
                }

            }
            return View("EditUser", profile.uid);
        }

        [HttpGet]
        public ActionResult PostList()
        {
            List<PostListModel> UsersPost = new List<PostListModel>();

            var data = (from u in dbObj.Users
                        join up in dbObj.Posts on u.uid equals up.uid
                        select new { u.name, u.userType, u.email,u.approval,up.pid, up.title, up.details, up.upVote, up.downVote, up.dateTime }).ToList();

           
            foreach(var u in data)
            {
                PostListModel obj = new PostListModel();
                obj.pid = u.pid;

                obj.name = u.name;
                obj.userType = u.userType;
                obj.email = u.email;
                obj.title = u.title;
                obj.details = u.details;
                obj.upVote = u.upVote;
                obj.downVote = u.downVote;
                obj.approval = u.approval;
                obj.dateTime = u.dateTime;

                UsersPost.Add(obj);
            }
            
            
            
            
            return View(UsersPost);
        }



    }
}