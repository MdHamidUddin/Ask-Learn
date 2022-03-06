using AskNLearn.Models;
using AskNLearn.Models.Entity;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AskNLearn.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        AskNLearnEntities dbObj = new AskNLearnEntities();
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Dashboard", (string)Session["userType"]);
            }
            return View();


        }

        [HttpPost]
        public ActionResult Login(UsersLoginModel user)
        {
            if (ModelState.IsValid)
            {
                var data = (from u in dbObj.Users
                            where u.username.Equals(user.username) &&
                            u.password.Equals(user.password)
                            select u).FirstOrDefault();

                //User data = dbObj.Users.Where(x => x.username == Model.username && x.password == Model.password).FirstOrDefault();

                var config = new MapperConfiguration(cfg => cfg.CreateMap<User, UsersLoginModel>());

                var mapper = new Mapper(config);

                var Data = mapper.Map<UsersLoginModel>(data);

                if (Data != null && data.username.FirstOrDefault().Equals('A'))
                {
                    FormsAuthentication.SetAuthCookie(data.username, false);
                    Session["username"] = data.username;
                    Session["name"] = data.name;
                    Session["userType"] = "Admin";
                    return RedirectToAction("../Admin/Dashboard");
                }
                else if (Data != null && data.username.FirstOrDefault().Equals('I'))
                {
                    FormsAuthentication.SetAuthCookie(data.username, false);
                    Session["name"] = data.name;
                    Session["uid"] = data.uid;
                    Session["proPic"] = data.proPic;
                    Session["userType"] = "Instructor";
                    return RedirectToAction("../Instructor/Dashboard");
                }
                else if (Data != null && data.username.FirstOrDefault().Equals('M'))
                {
                    FormsAuthentication.SetAuthCookie(data.username, false);
                    Session["username"] = data.username;
                    Session["uid"] = data.uid;
                    Session["userType"] = "Moderator";
                    return RedirectToAction("../gg/Dashboard");
                }
                else if (Data != null && data.username.FirstOrDefault().Equals('L'))
                {
                    FormsAuthentication.SetAuthCookie(data.username, false);
                    Session["username"] = data.username;
                    Session["uid"] = data.uid;
                    Session["userType"] = "Learner";
                    return RedirectToAction("../gg/Dashboard");
                }
                else if (Data == null)
                {
                    ViewBag.Message = "Your Username Or Password May Be Incorrect";
                }
            }

            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();
            Session.RemoveAll();
            FormsAuthentication.SignOut();
            //FormsAuthentication.RedirectToLoginPage();
            return RedirectToAction("Login", "Users");
        }

    }
}